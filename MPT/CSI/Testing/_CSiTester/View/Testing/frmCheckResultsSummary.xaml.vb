Option Strict On
Option Explicit On

Public Class frmCheckResultsSummary
    Friend Const CLASS_STRING As String = "frmCheckResultsSummary"

#Region "Constants: Private"
    Private Const checkResultsSummaryPass As String = "All Examples Passed!"
    Private Const checkResultsSummaryFail As String = "See 'Failed Examples' Tab for More Details"
    Private Const checkResultsSummaryNotRun As String = "Examples Failed to Run"
#End Region

#Region "Properties"
    Public Property failedSet As cExampleTestSet
#End Region

#Region "Initialization"
    Friend Sub New(ByVal failedTestSet As cExampleTestSet)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        failedSet = failedTestSet
        InitializeData()
        InitializeControls()
    End Sub

    Private Sub InitializeData()
        With failedSet
            lblTestId.Content = .testID
            lblStarted.Content = .timeStarted
            lblCompleted.Content = .timeCompleted
            lblTimeElapsed.Content = .timeElapsed

            lblNumExamplesRun.Content = .numExamplesRun
            lblNumExamplesCompared.Content = .numExamplesCompared
            lblNumExamplesPassed.Content = .numExamplesPassed
            lblNumExamplesFailed.Content = .numExamplesFailed

            lblOverallResultInternal.Content = .overallResult
            lblOverallResultPublished.Content = .overallResult
            If IsNumeric(Strings.Left(.maxPercentDifference, Len(.maxPercentDifference) - 1)) AndAlso .maxPercentDifference = "0%" Then
                lblOverallResultInternal.Foreground = Brushes.Green
                lblOverallResultPublished.Foreground = Brushes.Green
            Else
                lblOverallResultInternal.Foreground = Brushes.Red
                lblOverallResultPublished.Foreground = Brushes.Red
            End If
        End With
    End Sub

    Private Sub InitializeControls()

        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            resultsSummaryLabels.Visibility = Windows.Visibility.Collapsed
            lblOverallResultInternal.Visibility = Windows.Visibility.Collapsed
            lblCheckCompleted.Visibility = Windows.Visibility.Collapsed
            'Me.Height = 380
        Else
            lblOverallResultPublished.Visibility = Windows.Visibility.Collapsed
            If (failedSet.numExamplesRun = 0 AndAlso failedSet.numExamplesCompared = 0) Then
                lblCheckResultsSummary.Content = checkResultsSummaryNotRun
                lblCheckResultsSummary.Foreground = Brushes.Black
            ElseIf failedSet.checkPassed Then
                lblCheckResultsSummary.Content = checkResultsSummaryPass
                lblCheckResultsSummary.Foreground = Brushes.Green
            Else
                lblCheckResultsSummary.Content = checkResultsSummaryFail
                lblCheckResultsSummary.Foreground = Brushes.Red
            End If
        End If
    End Sub

#End Region

#Region "Form Controls"
    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs)
        UpdateControlsAndSelectTabs()

        Me.Close()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Creates a new tab for displaying only examples that have failed. Selects the failed examples tab. Also updates any other relevant parts of the form.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateControlsAndSelectTabs()
        Dim tabCounter As Integer = 0

        'Method for calling another window without a named class instantiation: http://www.vbforums.com/showthread.php?628245-RESOLVED-Call-a-function-in-one-form-from-another
        Dim strWindowToLookFor As String = GetType(CSiTester).Name
        Dim win = ( _
              From w In Application.Current.Windows _
              Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
              Select w _
           ).FirstOrDefault

        If win IsNot Nothing Then
            DirectCast(win, CSiTester).InitializeExampleSchemaValidationControls()  'Example Schema Validation Buttons
            DirectCast(win, CSiTester).InitializeDataGridTabs(True)                 'Generate new tabs with the summary tab of failed examples
        End If

        'Select summary tab of failed examples if it exists
        'get the tab index that the failed summary tab is
        For Each examplesListItem As cExampleTestSet In examplesTestSetList
            If Not String.IsNullOrEmpty(examplesListItem.maxPercentDifference) Then Exit For
            tabCounter += 1
        Next

        'If a failed tab summary was generated, the following condition is true, and select the tab by index
        If Not tabCounter > examplesTestSetList.Count - 1 Then
            If win IsNot Nothing Then
                DirectCast(win, CSiTester).myTabControlSummary.SelectedIndex = tabCounter
            End If
        End If
    End Sub
#End Region

#Region "Test Components"
    ''' <summary>
    ''' Used for automated testing. Cancels the form by 'clicking' the OK button.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TestFctnOKClick()
        e2eTester.ButtonClick(btnOK)
    End Sub
#End Region

End Class
