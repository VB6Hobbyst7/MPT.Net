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
''' Global listener for all messenger events that are to be displayed to the user.
''' </summary>
''' <remarks></remarks>
Public Class MessengerListenerShared
    Inherits MessengerPromptListener

    ''' <summary>
    ''' Subscribes shared classes to the listener.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeSharedListenerToConsole()
        AddHandler QueryLibrary.Message, AddressOf ReportMessageToConsole
        AddHandler ExcelLibrary.Message, AddressOf ReportMessageToConsole
        AddHandler FileLibrary.Message, AddressOf ReportMessageToConsole
        AddHandler DataGridLibrary.Message, AddressOf ReportMessageToConsole
        AddHandler FormsLibrary.Message, AddressOf ReportMessageToConsole
        AddHandler WinFormsLibrary.Message, AddressOf ReportMessageToConsole
        AddHandler cXmlReadWrite.Message, AddressOf ReportMessageToConsole
    End Sub

    ''' <summary>
    ''' Subscribes shared classes to the listener and displays a standard message box.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeSharedListenerToMessageBox()
        SubscribeLibraryListenerToMessageBox()

        AddHandler QueryLibrary.Message, AddressOf ReportMessageToMessageBox
        AddHandler ExcelLibrary.Message, AddressOf ReportMessageToMessageBox
        AddHandler FileLibrary.Message, AddressOf ReportMessageToMessageBox
        AddHandler DataGridLibrary.Message, AddressOf ReportMessageToMessageBox
        AddHandler FormsLibrary.Message, AddressOf ReportMessageToMessageBox
        AddHandler WinFormsLibrary.Message, AddressOf ReportMessageToMessageBox
        AddHandler cXmlReadWrite.Message, AddressOf ReportMessageToMessageBox
    End Sub

    ''' <summary>
    ''' Subscribes shared classes to the listener and displays a standard message box.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeSharedListenerToCSiMessageBox()
        SubscribeLibraryListenerToMessageBox()

        AddHandler QueryLibrary.Message, AddressOf ReportMessageToCSiMessageBox
        AddHandler ExcelLibrary.Message, AddressOf ReportMessageToCSiMessageBox
        AddHandler FileLibrary.Message, AddressOf ReportMessageToCSiMessageBox
        AddHandler DataGridLibrary.Message, AddressOf ReportMessageToCSiMessageBox
        AddHandler FormsLibrary.Message, AddressOf ReportMessageToCSiMessageBox
        AddHandler WinFormsLibrary.Message, AddressOf ReportMessageToCSiMessageBox
        AddHandler cXmlReadWrite.Message, AddressOf ReportMessageToCSiMessageBox
    End Sub


    ''' <summary>
    ''' Subscribes the listener to the provided object and displays a standard message box.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeListenerToCSiMessageBox(ByVal subscriberEvent As IMessengerEvent)
        AddHandler subscriberEvent.Messenger, AddressOf ReportMessageToCSiMessageBox
    End Sub

    ''' <summary>
    ''' Unsubscribes the listener from the provided object.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub UnsubscribeListenerToCSiMessageBox(ByVal subscriberEvent As IMessengerEvent)
        RemoveHandler subscriberEvent.Messenger, AddressOf ReportMessageToCSiMessageBox
    End Sub

    ''' <summary>
    ''' Writes the messenger title and message to the console.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Shared Sub ReportMessageToCSiMessageBox(ByVal e As MessengerEventArgs)
        If Not e.Handled Then
            If (e2eTestingRunning AndAlso
                e2eTester IsNot Nothing) Then

                e2eTester.SubTestExceptionCustom(GetMessage(e))
            Else
                ReportMessageToMessageBox(e)
            End If
        End If
    End Sub

End Class
