Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Forms.FormsLibrary

Public Class frmXMLObjectBenchmarkVersion
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _myBenchmark As cMCBenchmarkRef
    ''' <summary>
    ''' Class representing the benchmark data of the model control XML file to be generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myBenchmark As cMCBenchmarkRef
        Set(ByVal value As cMCBenchmarkRef)
            _myBenchmark = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myBenchmark"))
        End Set
        Get
            Return _myBenchmark
        End Get
    End Property

    Private _myBenchmarkSave As cMCBenchmarkRef
    ''' <summary>
    ''' Temporary storage property for the base example benchmark class that might be updated from the form input.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myBenchmarkSave As cMCBenchmarkRef
        Set(ByVal value As cMCBenchmarkRef)
            _myBenchmarkSave = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myBenchmarkSave"))
        End Set
        Get
            Return _myBenchmarkSave
        End Get
    End Property

    Private _applyToGroup As Boolean
    ''' <summary>
    ''' Item defined will be applied to all models in the same example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property applyToGroup As Boolean
        Set(ByVal value As Boolean)
            If Not _applyToGroup = value Then
                _applyToGroup = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("applyToGroup"))
            End If
        End Set
        Get
            Return _applyToGroup
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeControls(p_isExcelMultiModel:=False, p_applyToGroup:=False)

    End Sub

    ''' <summary>
    ''' Initializes form with an existing object's data.
    ''' </summary>
    ''' <param name="p_benchmark">Benchmark object to fill initial results and to modify in the form.</param>
    ''' <param name="p_isExcelMultiModel">True: The example correspdonding to the benchmark has an Excel result and is part of a multimodel example.
    ''' This will affect certain form defaults and control views for additional options.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef p_benchmark As cMCBenchmarkRef,
                   Optional ByVal p_isExcelMultiModel As Boolean = False,
                   Optional ByVal p_applyToGroup As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(p_benchmark)
        InitializeControls(p_isExcelMultiModel, p_applyToGroup)

        CheckRequiredDataFilled()
    End Sub

    Private Sub InitializeData(ByRef p_benchmark As cMCBenchmarkRef)
        myBenchmark = CType(p_benchmark.Clone, cMCBenchmarkRef)
        myBenchmarkSave = p_benchmark
    End Sub

    Private Sub InitializeControls(ByVal p_isExcelMultiModel As Boolean,
                                   ByVal p_applyToGroup As Boolean)
        btnOK.IsEnabled = False

        '===Combo Boxes

        'Programs
        cmbBxProgram.ItemsSource = testerSettings.programs
        If myBenchmark.programName = eCSiProgram.CSiBridge Then
            cmbBxProgram.SelectedIndex = 0
        Else
            Dim tempList As New ObservableCollection(Of String)
            For Each program As String In testerSettings.programs
                tempList.Add(program)
            Next
            cmbBxProgram.SelectedIndex = GetSelectedIndex(GetEnumDescription(myBenchmark.programName), tempList)
        End If

        'Version
        cmbBxVersion.ItemsSource = testerSettings.programVersions 'TODO: Assign list dynamically based on program
        If myBenchmark.programVersion Is Nothing Then
            cmbBxVersion.SelectedIndex = 0
        Else
            cmbBxVersion.Text = CStr(myBenchmark.programVersion)
        End If

        'Version Last Best
        cmbBxVersionLastBest.ItemsSource = testerSettings.programVersions 'TODO: Assign list dynamically based on program
        If myBenchmark.programVersionLastBest Is Nothing Then
            ' No action
        Else
            cmbBxVersionLastBest.Text = CStr(myBenchmark.programVersionLastBest)
        End If

        ' === Checkboxes
        If p_isExcelMultiModel Then
            chkBxApplyToGroup.Visibility = Windows.Visibility.Visible
            chkBxApplyToGroup.IsChecked = p_applyToGroup
            _applyToGroup = p_applyToGroup
        Else
            chkBxApplyToGroup.Visibility = Windows.Visibility.Collapsed
            _applyToGroup = False
        End If
    End Sub

#End Region

#Region "Form Controls"
    '=== Combo Boxes
    Private Sub cmbBxProgram_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxProgram.SelectionChanged
        myBenchmark.programName = ConvertStringToEnumByDescription(Of eCSiProgram)(cmbBxProgram.SelectedItem.ToString)
        CheckRequiredDataFilled()
    End Sub

    Private Sub cmbBxVersion_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxVersion.SelectionChanged
        myBenchmark.programVersion = CStr(cmbBxVersion.SelectedItem)
        CheckRequiredDataFilled()
    End Sub
    Private Sub cmbBxVersion_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles cmbBxVersion.LostKeyboardFocus
        myBenchmark.programVersion = CStr(cmbBxVersion.Text)
        CheckRequiredDataFilled()
    End Sub
    Private Sub cmbBxVersion_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbBxVersion.KeyUp
        If myBenchmark.IsValidVersion(cmbBxVersion.Text) Then
            myBenchmark.programVersion = CStr(cmbBxVersion.Text)
            CheckRequiredDataFilled()
        Else
            CheckRequiredDataFilled(p_checkBMObject:=False)
        End If
    End Sub

    Private Sub cmbBxVersionLastBest_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxVersionLastBest.SelectionChanged
        myBenchmark.programVersionLastBest = CStr(cmbBxVersionLastBest.SelectedItem)
    End Sub
    Private Sub cmbBxVersionLastBest_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles cmbBxVersionLastBest.LostKeyboardFocus
        myBenchmark.programVersionLastBest = CStr(cmbBxVersionLastBest.Text)
    End Sub

    '=== Buttons
    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs) Handles btnOK.Click
        myBenchmarkSave = myBenchmark
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Checks whether the minimum required information is present and performs operations based on that state.
    ''' </summary>
    ''' <param name="p_checkBMObject">False: The BM object will not be checked and the status is based on the form data.</param>
    ''' <remarks></remarks>
    Private Sub CheckRequiredDataFilled(Optional ByVal p_checkBMObject As Boolean = True)
        If p_checkBMObject Then
            btnOK.IsEnabled = myBenchmark.RequiredDataFilled()
        Else
            btnOK.IsEnabled = False
        End If
    End Sub
#End Region

End Class
