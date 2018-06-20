Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.Enums.eYesNoUnknown
Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.String.ConversionLibrary


Public Class frmXMLObjectResultDetails
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"
    Private Const _BTN_ADVANCED_TT_SHOW As String = "Displays advanced properties."
    Private Const _BTN_ADVANCED_TT_HIDE As String = "Hides advanced properties."

#End Region

#Region "Variables"
    Private _shiftCalc As Double
    Private _sigFig As String
    Private _valueBenchmark As String

    Private _ttTitle As String = "Description of the result and other comments, including units for the result."
    Private _ttIsCorrect As String = "Enter 'yes' if the benchmark value is correct, such as when it matches theoretical value. Enter 'no' if the the benchmark value is known to be incorrect. Otherwise leave blank."
    Private _ttSigFigRounding As String = "Number of significant digits to which the calculated and benchmark values should be rounded before calculating percent difference. Default is the same number of significant digits in the benchmark value."
    Private _ttZeroTolerance As String = "Limiting value for results that should be treated as zero. If absolute value of a result is smaller than or equal to this limit, then the result will be essentially rounded to zero."
    Private _ttCalcShift As String = "Specifies shift when calculating percent difference. This can be useful to obtain meaningful results for benchmarks that are noisy and near zeros (such benchmarks should be ideally avoided)."
    Private _ttBenchmark As String = "Benchmark value obtained from the original program version."
    Private _ttTheoretical As String = "Expected value of the result obtained by independent calculations or from independent source. It can be theoretical value, experimental value, value obtained from other software, etc."
    Private _ttLastBest As String = "Last best value obtained in a version of the program between the original model version and current tested version."

#End Region

#Region "Properties"
    Private _resultSave As cMCResult
    ''' <summary>
    ''' Original field output class, which is updated if the form is saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultSave As cMCResult
        Set(ByVal value As cMCResult)
            _resultSave = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("resultSave"))
        End Set
        Get
            Return _resultSave
        End Get
    End Property

    Private _result As cMCResult
    ''' <summary>
    ''' Temporary field output class, used for the form.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property result As cMCResult
        Set(ByVal value As cMCResult)
            _result = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("result"))
        End Set
        Get
            Return _result
        End Get
    End Property

    Private _bmEdit As Boolean
    ''' <summary>
    ''' Trigger for whether or not the user can specify the benchmark by altering the data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property bmEdit As Boolean
        Set(ByVal value As Boolean)
            If Not value = _bmEdit Then
                _bmEdit = value
                If value Then
                    txtBxBenchmark.IsEnabled = True
                    result.benchmark.overrideBenchmark = True
                Else
                    txtBxBenchmark.IsEnabled = False
                    result.benchmark.overrideBenchmark = False
                End If

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("bmAuto"))
            End If
        End Set
        Get
            Return _bmEdit
        End Get
    End Property

#End Region


#Region "Initialization"
    Friend Sub New(ByRef myResult As cMCResult)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(myResult)
        InitializeControls()
    End Sub

    Private Sub InitializeData(ByRef myResult As cMCResult)
        result = CType(myResult.Clone, cMCResult)
        resultSave = myResult

        If myResult.benchmark.overrideBenchmark Then
            bmEdit = True
        Else
            bmEdit = False
        End If

    End Sub

    Private Sub InitializeControls()

        'Set which controls are enabled
        If Not ResultDetailsComplete() Then btnOK.IsEnabled = False
        txtBxBenchmark.IsEnabled = False
        If Not IsNumeric(result.benchmark.valueBenchmark) Then
            txtBxSigFig.IsEnabled = False
            txtBxZeroTol.IsEnabled = False
            txtBxCalcShift.IsEnabled = False
        End If

        'Set Radio Buttons
        Select Case result.benchmark.isCorrect
            Case no : radBtnNo.IsChecked = True
            Case unknown : radBtnUnknown.IsChecked = True
            Case yes : radBtnYes.IsChecked = True
        End Select


        'Assign tooltips
        lblComment.ToolTip = _ttTitle
        txtBxComment.ToolTip = _ttTitle

        lblSigFig.ToolTip = _ttSigFigRounding
        txtBxSigFig.ToolTip = _ttSigFigRounding

        lblZeroTol.ToolTip = _ttZeroTolerance
        txtBxZeroTol.ToolTip = _ttZeroTolerance

        lblCalcShift.ToolTip = _ttCalcShift
        txtBxCalcShift.ToolTip = _ttCalcShift

        grpBxIsCorrect.ToolTip = _ttIsCorrect
        radBtnUnknown.ToolTip = _ttIsCorrect
        radBtnYes.ToolTip = _ttIsCorrect
        radBtnNo.ToolTip = _ttIsCorrect

        lblBenchmark.ToolTip = _ttBenchmark
        txtBxBenchmark.ToolTip = _ttBenchmark

        lblTheoretical.ToolTip = _ttTheoretical
        txtBxTheoretical.ToolTip = _ttTheoretical

        lblLastBest.ToolTip = _ttLastBest
        txtBxLastBest.ToolTip = _ttLastBest

        btnAdvanced.ToolTip = _BTN_ADVANCED_TT_SHOW
    End Sub

#End Region

#Region "Form Controls"
    '=== Buttons
    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs) Handles btnOK.Click
        resultSave = result
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnResetBM_Click(sender As Object, e As RoutedEventArgs) Handles btnResetBM.Click
        result.benchmark.valueBenchmark = result.benchmark.valueTable
    End Sub
    Private Sub btnAdvanced_Click(sender As Object, e As RoutedEventArgs) Handles btnAdvanced.Click
        If btnAdvanced.Content.ToString = "<<" Then
            btnAdvanced.Content = ">>"
            btnAdvanced.ToolTip = _BTN_ADVANCED_TT_SHOW

            spResultAdvanced.Visibility = Windows.Visibility.Collapsed
            spBMAdvanced.Visibility = Windows.Visibility.Collapsed
            spTheoreticalAdvanced.Visibility = Windows.Visibility.Collapsed
            spLastBestAdvanced.Visibility = Windows.Visibility.Collapsed
        Else
            btnAdvanced.Content = "<<"
            btnAdvanced.ToolTip = _BTN_ADVANCED_TT_HIDE

            spResultAdvanced.Visibility = Windows.Visibility.Visible
            spBMAdvanced.Visibility = Windows.Visibility.Visible
            spTheoreticalAdvanced.Visibility = Windows.Visibility.Visible
            spLastBestAdvanced.Visibility = Windows.Visibility.Visible
        End If
    End Sub

    Private Sub btnTheoreticalUseBM_Click(sender As Object, e As RoutedEventArgs) Handles btnTheoreticalUseBM.Click
        result.benchmark.valueTheoretical = txtBxBenchmark.Text
    End Sub

    Private Sub btnLastBestUseBM_Click(sender As Object, e As RoutedEventArgs) Handles btnLastBestUseBM.Click
        result.benchmark.valueLastBest = txtBxBenchmark.Text
    End Sub

    '=== Text Boxes
    Private Sub txtBxComment_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxComment.TextChanged
        If ResultDetailsComplete() Then
            btnOK.IsEnabled = True
        Else
            btnOK.IsEnabled = False
        End If
    End Sub
    Private Sub txtBxTheoretical_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxTheoretical.TextChanged
        If ResultDetailsComplete() Then
            btnOK.IsEnabled = True
        Else
            btnOK.IsEnabled = False
        End If
    End Sub

    Private Sub txtBxSigFig_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtBxSigFig.GotFocus
        _sigFig = txtBxSigFig.Text
    End Sub
    Private Sub txtBxSigFig_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxSigFig.LostFocus
        If CheckValidEntryInteger(txtBxSigFig.Text) Then
            Dim sigFig As Integer = myCInt(txtBxSigFig.Text)

            'Round the benchmark to preview the effect
            _valueBenchmark = resultSave.benchmark.valueBenchmark
            If (sigFig < 0 OrElse sigFig > 15) Then
                result.benchmark.valueBenchmark = resultSave.benchmark.valueBenchmark
                txtBxSigFig.Text = _sigFig
            Else
                result.benchmark.valueBenchmark = RoundToSigFig(_valueBenchmark, sigFig)
            End If
        ElseIf String.IsNullOrEmpty(txtBxSigFig.Text) Then
            result.benchmark.valueBenchmark = resultSave.benchmark.valueBenchmark
        Else
            result.benchmark.roundBenchmark = _sigFig
        End If
    End Sub

    Private Sub txtBxZeroTol_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxZeroTol.LostFocus
        If Not CheckValidEntryNumeric(txtBxZeroTol.Text) Then txtBxZeroTol.Text = ""
    End Sub

    Private Sub txtBxCalcShift_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtBxCalcShift.GotFocus
        _shiftCalc = CDbl(txtBxCalcShift.Text)
    End Sub
    Private Sub txtBxCalcShift_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxCalcShift.LostFocus
        If Not CheckValidEntryNumeric(txtBxCalcShift.Text) Then result.benchmark.shiftCalc = _shiftCalc
    End Sub

    '=== Radio Buttons
    Private Sub radBtnUnknown_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnUnknown.Checked
        result.benchmark.isCorrect = unknown
    End Sub
    Private Sub radBtnNo_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnNo.Checked
        result.benchmark.isCorrect = no
    End Sub
    Private Sub radBtnYes_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnYes.Checked
        result.benchmark.isCorrect = yes
    End Sub

    '=== Checkboxes
    Private Sub chkBxBmEdit_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxBmEdit.Checked

    End Sub
    Private Sub chkBxBmEdit_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxBmEdit.Unchecked

    End Sub

#End Region

#Region "Methods: Public"

    ''' <summary>
    ''' Enforces that the form only exits with saved results when the required entries have been entered.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ResultDetailsComplete() As Boolean
        If Not String.IsNullOrEmpty(result.name) Then Return True

        Return False
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the string value rounded to the specified significant figure, including preserving trailing zeros.
    ''' </summary>
    ''' <param name="p_value">Value to round.</param>
    ''' <param name="p_sigFig">Significant decimal figures to round to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RoundToSigFig(ByVal p_value As String,
                                   ByVal p_sigFig As Integer) As String
        If p_sigFig > p_value.Length Then Return p_value

        Dim roundedNumber As String = ""
        Dim decimalReached As Boolean = False

        ' Check sign
        If myCInt(p_value) < 0 Then p_sigFig += 1

        For i = 0 To p_value.Length - 1
            If (Not decimalReached AndAlso (p_value(i) = CChar("."))) Then
                decimalReached = True
                If Not i < p_sigFig Then Exit For
            End If

            If (decimalReached AndAlso i >= p_sigFig + 1) Then
                Exit For
            ElseIf ((Not decimalReached AndAlso i < p_sigFig) OrElse
                    (decimalReached AndAlso i < p_sigFig + 1)) Then
                roundedNumber += p_value(i)
            Else
                roundedNumber += CChar("0")
            End If
        Next

        ' Check for rounding of last number
        If (roundedNumber.Length < p_value.Length AndAlso Not roundedNumber.LastOrDefault = CChar(".")) Then
            Dim tempNumCurrentIndex As Integer
            If CStr(roundedNumber).Contains(".") Then
                tempNumCurrentIndex = p_sigFig
            Else
                tempNumCurrentIndex = p_sigFig - 1
            End If
            Dim tempNumCurrent As Integer = CInt(CStr(roundedNumber(tempNumCurrentIndex)))

            Dim tempNumNext As Integer = 0
            Dim nextNumIndex As Integer = tempNumCurrentIndex + 1
            If Not p_value(nextNumIndex) = CChar(".") Then
                tempNumNext = CInt(CStr(p_value(nextNumIndex)))
            ElseIf (nextNumIndex + 1 < p_value.Length AndAlso Not p_value(nextNumIndex + 1) = CChar(".")) Then
                tempNumNext = CInt(CStr(p_value(nextNumIndex + 1)))
            End If

            If tempNumNext >= 5 Then tempNumCurrent += 1

            Mid(roundedNumber, tempNumCurrentIndex + 1, 1) = CStr(tempNumCurrent)
        End If

        Return roundedNumber
    End Function
#End Region
End Class
