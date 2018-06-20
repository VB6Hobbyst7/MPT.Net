'Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cPathSettings
Imports CSiTester.cE2ETestController

Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Public Class frmTestingE2E
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Variables"
    Private bw1 As BackgroundWorker
    Private testRunning As Boolean

    Private originalPathProgram As String
    Private originalPathDestination As String
    Private originalPathSource As String
    Private originalSingleTab As Boolean
    Private originalApplyAllTabs As Boolean
#End Region

#Region "Properties"
    Private _alwaysRevert As Boolean
    Public Property alwaysRevert As Boolean
        Set(ByVal value As Boolean)
            _alwaysRevert = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("alwaysRevert"))
        End Set
        Get
            Return _alwaysRevert
        End Get
    End Property

    Private _logSummaryOpen As Boolean
    ''' <summary>
    ''' If true, the latest summary log file will be opened after the run has completed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property logSummaryOpen As Boolean
        Set(ByVal value As Boolean)
            _logSummaryOpen = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("logSummaryOpen"))
        End Set
        Get
            Return _logSummaryOpen
        End Get
    End Property

    Private _logFullOpen As Boolean
    ''' <summary>
    ''' If true, the latest full log file will be opened after the run has completed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property logFullOpen As Boolean
        Set(ByVal value As Boolean)
            _logFullOpen = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("logFullOpen"))
        End Set
        Get
            Return _logFullOpen
        End Get
    End Property
#End Region

#Region "Initialization"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeDefaults()
        SetButtons()
        InitializeE2ETester()
        dgTests.ItemsSource = e2eTester.controller.tests
    End Sub

    Private Sub InitializeDefaults()
        alwaysRevert = True
        logFullOpen = False
        logSummaryOpen = True

        testRunning = False
    End Sub

    Private Sub SetButtons()
        btnCleanup.IsEnabled = Not testRunning
        btnClose.IsEnabled = Not testRunning
        btnInitialize.IsEnabled = Not testRunning
        btnRevert.IsEnabled = Not testRunning
        btnRun.IsEnabled = Not testRunning

        chkBxAlwaysRevert.IsEnabled = Not testRunning

        btnCancel.IsEnabled = testRunning
    End Sub

    ''' <summary>
    ''' Loads an e2eTester class from the specified XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeE2ETester()
        Dim xmlPath As String = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING & "\" & FILE_NAME_E2E_TESTING

        If IO.File.Exists(xmlPath) Then
            e2eTester = New cE2ETester(xmlPath)
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Stop),
                                                        "The '{1}' file cannot be found at the following path: {0}{0} {2} {0}{0} Form will not open.",
                                                        "File Not Found",
                                                        Environment.NewLine, FILE_NAME_E2E_TESTING, xmlPath))
            Me.Close()
        End If
    End Sub
#End Region

#Region "Form Controls"
    ''' <summary>
    ''' Copies fresh directories used in running the tests from the seed folder to the testing location.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnInitialize_Click(sender As Object, e As RoutedEventArgs) Handles btnInitialize.Click
        TestInitialize()
    End Sub

    Private Sub btnRun_Click(sender As Object, e As RoutedEventArgs) Handles btnRun.Click
        testRunning = True
        'SetButtons()

        'StartThread()
        RunE2eTests()

        testRunning = False
        'SetButtons()

        'Open results log
        If logFullOpen Then OpenFile(e2eTester.filePathFull)
        If logSummaryOpen Then OpenFile(e2eTester.filePathSummary)
    End Sub

    ''' <summary>
    ''' Cancels the run of end-to-end tests.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Try
            ' Cancel the asynchronous operation. 
            'If bw1.WorkerSupportsCancellation = True Then
            '    bw1.CancelAsync()
            'End If

            testRunning = False
            SetButtons()

            Dim strWindowToLookFor As String = GetType(CSiTester).Name
            Dim win = ( _
                  From w In Application.Current.Windows _
                  Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
                  Select w _
               ).FirstOrDefault

            If win IsNot Nothing Then
                'Reset CSiTester values back to the original values before running tests
                If alwaysRevert Then DirectCast(win, CSiTester).TesterRevert(originalPathProgram, originalPathDestination, originalPathSource, originalSingleTab, originalApplyAllTabs)
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Removes directories used in running the tests.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCleanup_Click(sender As Object, e As RoutedEventArgs) Handles btnCleanup.Click
        TestCleanup()
    End Sub

    ''' <summary>
    ''' Deletes all log files located in the logs folder.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClearLogs_Click(sender As Object, e As RoutedEventArgs) Handles btnClearLogs.Click
        LogCleanup()
    End Sub

    ''' <summary>
    ''' Reset CSiTester values back to the original values before running tests.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRevert_Click(sender As Object, e As RoutedEventArgs) Handles btnRevert.Click
        Dim strWindowToLookFor As String = GetType(CSiTester).Name
        Dim win = ( _
              From w In Application.Current.Windows _
              Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
              Select w _
           ).FirstOrDefault

        If win IsNot Nothing Then
            DirectCast(win, CSiTester).TesterRevert(originalPathProgram, originalPathDestination, originalPathSource, originalSingleTab, originalApplyAllTabs)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        e2eTester = Nothing
        Me.Close()
    End Sub

    Private Sub btnSimple_Click(sender As Object, e As RoutedEventArgs) Handles btnSimple.Click
        dgTests.ItemsSource = e2eTester.controller.tests
    End Sub

    Private Sub btnComplex_Click(sender As Object, e As RoutedEventArgs) Handles btnComplex.Click
        dgTests.ItemsSource = e2eTester.controller.testsAggregates
    End Sub
#End Region

#Region "Form Behavior"

    ''' <summary>
    ''' All actions to be done whenever the form is closed occur here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Window_Closing(sender As Object, e As CancelEventArgs)
        If e2eTester IsNot Nothing Then e2eTester = Nothing
    End Sub
#End Region

#Region "Methods: Background Worker"
    ''' <summary>
    ''' Initialize the object that the background worker calls (if any) and starts the asynchronous operation.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartThread()
        ' This method runs on the main thread. 
        bw1 = New BackgroundWorker

        ' Creates background worker thread for checking run status and filling example comparisons when ready
        CreateBackgroundWorker()

        ' Initialize the object that the background worker calls. 

        ' Start the asynchronous operation.
        bw1.RunWorkerAsync()    '(object)

        'Note: For debugging, uncomment 'finished' msgbox in bw1_RunWorkerCompleted to see when each thread ends
    End Sub

    ''' <summary>
    ''' Creates background worker thread for checking run status and filling example comparisons when ready.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBackgroundWorker()
        bw1.WorkerReportsProgress = True
        bw1.WorkerSupportsCancellation = True
        AddHandler bw1.DoWork, AddressOf bw1_DoWork
        AddHandler bw1.ProgressChanged, AddressOf bw1_ProgressChanged
        AddHandler bw1.RunWorkerCompleted, AddressOf bw1_RunWorkerCompleted
    End Sub

    '=== The following functions are needed for handling the threads in the form
    ''' <summary>
    ''' Operations done by worker thread as a background thread.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ' This event handler is where the actual work is done. 
        ' This method runs on the background thread. 

        ' Get the BackgroundWorker object that raised this event.
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        RunE2eTests()

        'End thread operations
        bw1.CancelAsync()
    End Sub

    ''' <summary>
    ''' Updates thread progress data on the main thread.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Threadsafe and delegate functions are not necessary for form items affected by this function, as it runs on the main thread</remarks>
    Private Sub bw1_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        ' This method runs on the main thread. 

        ' Update the progress bar
        ProgressBar1.Value = e.ProgressPercentage

        'Note: to update other values, a single object must be passed in that contains all of the values

    End Sub

    ''' <summary>
    ''' This event handler is called when the background thread finishes. Displays messages if necessary.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw1_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        ' This event handler is called when the background thread finishes. 
        ' This method runs on the main thread. 

        If e.Error IsNot Nothing Then
            'TODO: myLogger
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(),
                                                        "Error: {0}",
                                                        e.Error.Message))
        ElseIf e.Cancelled Then
            'TODO: myLogger
            'Doesn't really appear based on how this is coded
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), "Check canceled."))
        Else
            'MessageBox.Show("Finished process.")
        End If
    End Sub
#End Region

#Region "Methods: Threadsafe"
    'Private Delegate Sub SetLabel_Delegate(ByVal myLabel As Label, ByVal myText As String)
    ' ''' <summary>
    ' ''' Updates the label in the form.
    ' ''' </summary>
    ' ''' <param name="myLabel">Name of the label to update</param>
    ' ''' <param name="myText">Text to assign to the textblock</param>
    ' ''' <remarks></remarks>
    'Private Sub SetLabelText_ThreadSafe(ByVal myLabel As Label, ByVal myText As String)
    '    ' CheckAccess compares the thread ID of the calling thread to the thread ID of the creating thread.
    '    ' If these threads are the same, it returns true.
    '    If myLabel.Dispatcher.CheckAccess Then
    '        myLabel.Content = myText
    '    Else                                        'Threads are the different, so threadsafe procedure needed
    '        Dim myDelegate As New SetLabel_Delegate(AddressOf SetLabelText_ThreadSafe)
    '        myLabel.Dispatcher.Invoke(myDelegate, New Object() {myLabel, myText})
    '    End If
    'End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Copies fresh directories used in running the tests from the seed folder to the testing location.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TestInitialize()
        Dim e2eTestingPath As String = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING
        Dim seedPath As String = e2eTestingPath & "\seed"
        Dim dirListSeed As New List(Of String)
        Dim dirListTest As New List(Of String)

        If (IO.Directory.Exists(e2eTestingPath) AndAlso IO.Directory.Exists(seedPath)) Then
            'Get list of directories in 'seed' and the testing location
            GetDirectories(seedPath, dirListSeed, False)
            GetDirectories(e2eTestingPath, dirListTest, False)

            'If directory exists at destination, delete it
            For Each testDirPath As String In dirListTest
                If (Not StringExistInName(testDirPath, "seed") OrElse Not StringExistInName(testDirPath, "logs")) Then
                    For Each seedDirPath As String In dirListSeed
                        If GetSuffix(testDirPath, "\") = GetSuffix(seedDirPath, "\") Then DeleteAllFilesFolders(testDirPath, True)
                    Next
                End If
            Next

            'Copy all directories over
            For Each seedDirPath As String In dirListSeed
                CopyFolder(seedDirPath, e2eTestingPath & "\" & GetSuffix(seedDirPath, "\"))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Removes directories used in running the tests.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TestCleanup()
        Dim e2eTestingPath As String = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING
        Dim dirListTest As New List(Of String)

        If IO.Directory.Exists(e2eTestingPath) Then
            'Get list of directories in testing location
            GetDirectories(e2eTestingPath, dirListTest, False)

            'If the directory name is not 'seed', delete it
            For Each testDirPath As String In dirListTest
                If Not GetSuffix(testDirPath, "\") = "seed" Then DeleteAllFilesFolders(testDirPath, True)
                If Not GetSuffix(testDirPath, "\") = "logs" Then DeleteAllFilesFolders(testDirPath, True)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Deletes all log files contained within the 'logs' folder.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LogCleanup()
        DeleteAllFilesFolders(pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING & "\logs", False)
    End Sub

    ''' <summary>
    ''' Sets up the CSiTester program to be in a constant predefined state before running tests.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TestFirstAssertions()
        Dim strWindowToLookFor As String = GetType(CSiTester).Name
        Dim win = ( _
              From w In Application.Current.Windows _
              Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
              Select w _
           ).FirstOrDefault

        If win IsNot Nothing Then
            DirectCast(win, CSiTester).TestFirstAssertions()
        End If
    End Sub

    ''' <summary>
    ''' Runs the end-to-end tests.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RunE2eTests()
        If Not testRunning Then testRunning = True

        Try
            originalPathProgram = myRegTest.program_file.path
            originalPathDestination = testerSettings.testerDestination.path
            originalPathSource = myRegTest.models_database_directory.path
            originalSingleTab = testerSettings.singleTab
            originalApplyAllTabs = testerSettings.allTabsSelectRun

            e2eTester.subTests = New ObservableCollection(Of cE2ELogger)
            e2eTester.testLog = ""
            e2eTester.testSummaryLog = ""

            'Generate collection of only tests that have been selected
            e2eTester.controller.testsSelected = New ObservableCollection(Of cE2eTest)
            For Each test As cE2eTest In e2eTester.controller.tests
                If test.selected Then e2eTester.controller.testsSelected.Add(test)
            Next
            For Each test As cE2eTest In e2eTester.controller.testsAggregates
                If test.selected Then e2eTester.controller.testsSelected.Add(test)
            Next

            Dim strWindowToLookFor As String = GetType(CSiTester).Name
            'Dim win As Object = Nothing
            'Dim tempCSiTester As New CSiTester(True)
            'SetCSiTester_ThreadSafe(tempCSiTester, win, strWindowToLookFor)

            Dim win = ( _
                  From w In Application.Current.Windows _
                  Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
                  Select w _
               ).FirstOrDefault

            If win IsNot Nothing Then
                DirectCast(win, CSiTester).TestForm(e2eTester.controller.testsSelected)
                'Reset CSiTester values back to the original values before running tests
                If alwaysRevert Then DirectCast(win, CSiTester).TesterRevert(originalPathProgram, originalPathDestination, originalPathSource, originalSingleTab, originalApplyAllTabs)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            testRunning = False
        End Try
    End Sub
#End Region

#Region "Methods Threadsafe"
    ''==== The following subs are needed in order to make changes to the form without having thread conflict issues
    Private Delegate Sub SetCSiTester_Delegate(ByVal csiTester As CSiTester, ByVal win As Object, ByVal strWindowToLookFor As String)
    ''' <summary>
    '''
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCSiTester_ThreadSafe(ByVal csiTester As CSiTester, ByVal win As Object, ByVal strWindowToLookFor As String)
        ' CheckAccess compares the thread ID of the calling thread to the thread ID of the creating thread.
        ' If these threads are the same, it returns true.
        Try
            If csiTester.Dispatcher.CheckAccess Then
                win = ( _
                      From w In Application.Current.Windows _
                      Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
                      Select w _
                   ).FirstOrDefault

                If win IsNot Nothing Then
                    DirectCast(win, CSiTester).TestForm(e2eTester.controller.testsSelected)
                    'Reset CSiTester values back to the original values before running tests
                    If alwaysRevert Then DirectCast(win, CSiTester).TesterRevert(originalPathProgram, originalPathDestination, originalPathSource, originalSingleTab, originalApplyAllTabs)
                End If
            Else                                        'Threads are the different, so threadsafe procedure needed
                Dim myDelegate As New SetCSiTester_Delegate(AddressOf SetCSiTester_ThreadSafe)
                win.Dispatcher.Invoke(myDelegate, New Object() {csiTester, win, strWindowToLookFor})
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

End Class
