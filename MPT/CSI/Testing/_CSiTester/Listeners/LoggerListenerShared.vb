Option Explicit On
Option Strict On

Imports MPT.Cursors
Imports MPT.Database
Imports MPT.Enums
Imports MPT.Excel
Imports MPT.Files
Imports MPT.FileSystem
Imports MPT.Forms
Imports MPT.Lists
Imports MPT.PropertyChanger
Imports MPT.Reflections
Imports MPT.Reporting
Imports MPT.String
Imports MPT.Time
Imports MPT.Units
Imports MPT.Verification
Imports MPT.XML
Imports MPT.XML.ReaderWriter

''' <summary>
''' Global listener for all logger events that are to be handled.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class LoggerListenerShared
    Inherits LoggerListener

    Public Shared Sub SubscribeSharedListenerToConsole()
        AddHandler QueryLibrary.Log, AddressOf ReportLogToConsole
        AddHandler ExcelLibrary.Log, AddressOf ReportLogToConsole
        AddHandler FileLibrary.Log, AddressOf ReportLogToConsole
        AddHandler FoldersLibrary.Log, AddressOf ReportLogToConsole
        AddHandler PathLibrary.Log, AddressOf ReportLogToConsole
        AddHandler DataGridLibrary.Log, AddressOf ReportLogToConsole
        AddHandler FormsLibrary.Log, AddressOf ReportLogToConsole
        AddHandler WinFormsLibrary.Log, AddressOf ReportLogToConsole
        AddHandler ListLibrary.Log, AddressOf ReportLogToConsole
        AddHandler ObjectValidation.Log, AddressOf ReportLogToConsole
        AddHandler cXmlReadWrite.Log, AddressOf ReportLogToConsole

        AddHandler cPathOutputSettings.SharedLog, AddressOf ReportLogToConsole
        AddHandler SetterRegTest.SharedLog, AddressOf ReportLogToConsole
        AddHandler cXmlReadWriteRegTest.SharedLog, AddressOf ReportLogToConsole
        AddHandler cXmlReadWriteModelControl.SharedLog, AddressOf ReportLogToConsole
        AddHandler AdapterModelControl.SharedLog, AddressOf ReportLogToConsole
        AddHandler AdapterRegTest.SharedLog, AddressOf ReportLogToConsole
        AddHandler AdapterOutputSettings.SharedLog, AddressOf ReportLogToConsole
        AddHandler AdapterModelControl.SharedLog, AddressOf ReportLogToConsole
    End Sub

    ''' <summary>
    ''' Subscribes the listener to the provided object and displays a standard message box.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeListenerToCSiLogger(ByVal subscriberEvent As ILoggerEvent)
        AddHandler subscriberEvent.Log, AddressOf ReportMessageToCSiLogger
    End Sub

    ''' <summary>
    ''' Unsubscribes the listener from the provided object.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub UnsubscribeListenerToCSiLogger(ByVal subscriberEvent As ILoggerEvent)
        RemoveHandler subscriberEvent.Log, AddressOf ReportMessageToCSiLogger
    End Sub

    ''' <summary>
    ''' Subscribes shared classes to the listener and displays a standard message box.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeSharedListenerToCSiLogger()
        AddHandler QueryLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler ExcelLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler FileLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler FoldersLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler PathLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler DataGridLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler FormsLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler WinFormsLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler ListLibrary.Log, AddressOf ReportMessageToCSiLogger
        AddHandler ObjectValidation.Log, AddressOf ReportMessageToCSiLogger
        AddHandler cXmlReadWrite.Log, AddressOf ReportMessageToCSiLogger

        AddHandler cPathOutputSettings.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler SetterRegTest.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler cXmlReadWriteRegTest.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler cXmlReadWriteModelControl.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler AdapterModelControl.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler AdapterRegTest.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler AdapterOutputSettings.SharedLog, AddressOf ReportMessageToCSiLogger
        AddHandler AdapterModelControl.SharedLog, AddressOf ReportMessageToCSiLogger
    End Sub

    ''' <summary>
    ''' Writes the messenger title and message to the console.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Shared Sub ReportMessageToCSiLogger(ByVal e As LoggerEventArgs)
        If Not e.Handled Then
            ' TODO Note: Currently just showing the message box. Need to decide how to work CSiLogger in debug vs. released mode, and to run more quietly.
            If (e2eTestingRunning AndAlso
                e2eTester IsNot Nothing) Then

                e2eTester.SubTestExceptionCustom(GetMessage(e))
            Else
                MessageBox.Show(GetMessage(e), GetTitle(e))
            End If

            csiLogger.ExceptionAction(e.Exception, newSuppressExStates:=True)
        End If
    End Sub
End Class
