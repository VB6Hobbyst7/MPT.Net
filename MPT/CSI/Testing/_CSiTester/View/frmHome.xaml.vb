Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Reporting
Imports MPT.XML.ReaderWriter.cXmlReadWrite

Public Class frmHome
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Variables"

#End Region

#Region "Properties"

#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim args As String() = Application.mArgs
    End Sub
#End Region

#Region "Form Controls"
    ''' <summary>
    ''' Loads the main part of the program. This is where a suite is viewed, run, and compared, and results are viewed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTests_Click(sender As Object, e As RoutedEventArgs) Handles btnTests.Click
        windowCSiTesterTests = New CSiTester

        windowCSiTesterTests.ShowDialog()
    End Sub

    Private Sub btnExamples_Click(sender As Object, e As RoutedEventArgs) Handles btnExamples.Click
        'Not necessary?
    End Sub

    ''' <summary>
    ''' Generates new examples for the suite.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExampleAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnExampleAdd.Click
        windowXMLTemplateGenerator = New frmXMLTemplateGenerator()

        windowXMLTemplateGenerator.ShowDialog()
    End Sub

    ''' <summary>
    ''' Edits examples for the suite.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExampleEdit_Click(sender As Object, e As RoutedEventArgs) Handles btnExampleEdit.Click
        windowExampleEditor = New frmExampleEditor

        windowExampleEditor.ShowDialog()
    End Sub

    ''' <summary>
    ''' Runs the XML Bulk editor.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBulkEditor_Click(sender As Object, e As RoutedEventArgs) Handles btnBulkEditor.Click
        LoadXMLEditor()

        myXMLEditor.MirrorAllEditorXMLS()

        'Add additional settings values to memory from the settings XML file
        testerSettings.InitializeExamplesObjects()

        'Create editor form
        windowXMLEditorBulk = New frmXMLEditorBulk
        windowXMLEditorBulk.DataContext = myXMLEditor.suiteEditorXMLObjects 'Passes relevant binding information to sub-class
        windowXMLEditorBulk.ShowDialog()

        ''If path is unchanged from load, and XML files have been modified, update the main form view
        'If myXMLEditor.xmlEditorPath = regTest.models_database_directory And myXMLEditor.XMLChanged Then
        '    RefreshForm()
        'End If

        'myXMLEditor.XMLChanged = False
    End Sub

    ''' <summary>
    ''' Performs operations to an entire suite, mostly with file IO, including renaming, moving, and XML element changes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSuite_Click(sender As Object, e As RoutedEventArgs) Handles btnSuite.Click
        windowSuiteOperations = New frmSuiteOperations

        windowSuiteOperations.ShowDialog()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the XML editor class and any referenced xml files into memory. 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXMLEditor()
        Try
            myXMLEditor = New cXMLEditor
            myXMLEditor.InitializeXMLEditorData()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region


End Class
