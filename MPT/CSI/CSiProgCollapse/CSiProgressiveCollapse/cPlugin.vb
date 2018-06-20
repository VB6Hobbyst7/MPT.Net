''' <summary>
''' All plugin functionality must be implemented in a class called cPlugin.
''' </summary>
''' <remarks></remarks>
<ComClass(cPlugin.ClassId, cPlugin.InterfaceId, cPlugin.EventsId)> _
Public Class cPlugin

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.

    'SAP2000 is not looking for specific GUID’s. Choose unique GUIDs for the plug in.
    Public Const ClassId As String = "9503be48-a66f-4a7e-81e0-b35d85db8d20"
    Public Const InterfaceId As String = "8bee4c2e-cf7a-4da2-9352-8513e5ed3124"
    Public Const EventsId As String = "169057fd-358c-4506-88fc-72977bdabd1a"

    'Before SAP2000 can call the plug in, the plug in must be registered for COM (or for COM interop for .NET DLLs) 
    'on the user’s system. It is your responsibility to instruct the user how to install the plug in. 
    'This should be done after SAP2000 has been installed.

#End Region
#If COMPILE_ETABS2013 Or COMPILE_ETABS2014 Then
    'N/A
#ElseIf COMPILE_SAP2000v16 Then
    Private my_sapModel As SAP2000v16.cSapModel
    Private my_sapPlugIn As SAP2000v16.cSapPlugin

    Public ReadOnly Property sapModel() As SAP2000v16.cSapModel
        Get
            Return my_sapModel
        End Get
    End Property

    Public ReadOnly Property sapPlugIn() As SAP2000v16.cSapPlugin
        Get
            Return my_sapPlugIn
        End Get
    End Property
#ElseIf COMPILE_SAP2000v17 Then
    Private my_sapModel As SAP2000v17.cSapModel
    Private my_sapPlugIn As SAP2000v17.cPluginCallback

    Public ReadOnly Property sapModel() As SAP2000v17.cSapModel
        Get
            Return my_sapModel
        End Get
    End Property

    Public ReadOnly Property sapPlugIn() As SAP2000v17.cPluginCallback
        Get
            Return my_sapPlugIn
        End Get
    End Property

#End If

    
    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Class cPlugin may have an optional function cPlugin.Info() that expects a reference to a string, and returns a long.  
    ''' The return value should be zero if successful. 
    ''' The string is to be filled in by the function, and may be plain text or rich text. 
    ''' If this function is found and returns zero, the string will be displayed when the user first adds the plug in to SAP2000. 
    ''' You can use this string to tell the user the purpose and author of the plug in. 
    ''' This is in addition to any information you may provide when the user executes the plug in.
    ''' </summary>
    ''' <param name="Text">String to be filled in by the function and displayed to the user when they first add the plug in to the program.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Info(ByRef Text As String) As Integer

        Text = String.Format("CSiProgressiveCollapse v1.0.0. {0}" + _
                             "Copyright (C) Mark P. Thomas, 2014{0}{0}" + _
                             "This tool determines they key elements that will initiate progressive collapse based on strength or ductility criteria. It can also track a collapse using either criteria.", Environment.NewLine)

        Return 0

    End Function
#If COMPILE_ETABS2013 Or COMPILE_ETABS2014 Then
    'N/A
#ElseIf COMPILE_SAP2000v16 Then
    ''' <summary>
    ''' Class cPlugin must have a subroutine  cPlugin.Main() that expects a reference to Sap2000.cSapModel and Sap2000.cSapPlugin.
    ''' </summary>
    ''' <param name="SapModel"></param>
    ''' <param name="ISapPlugin"></param>
    ''' <remarks></remarks>
    Public Sub Main(ByRef SapModel As SAP2000v16.cSapModel, ByRef ISapPlugin As SAP2000v16.cSapPlugin)
        Try
            my_sapPlugIn = ISapPlugin
            my_sapModel = SapModel

            InitializePlugIn()

        Catch ex As Exception
            MsgBox("The following error terminated the Plug-In:" & vbCrLf & ex.Message)

            ' call Finish to inform SAP2000 that the PlugIn has terminated
            Try
                ISapPlugin.Finish(1)
            Catch ex1 As Exception
            End Try
        End Try

        Return

    End Sub
#ElseIf COMPILE_SAP2000v17 Then
    ''' <summary>
    ''' Class cPlugin must have a subroutine  cPlugin.Main() that expects a reference to Sap2000.cSapModel and Sap2000.cPluginCallback.
    ''' </summary>
    ''' <param name="SapModel"></param>
    ''' <param name="ISapPlugin"></param>
    ''' <remarks></remarks>
    Public Sub Main(ByRef SapModel As SAP2000v17.cSapModel, ByRef ISapPlugin As SAP2000v17.cPluginCallback)
        Try
            my_sapPlugIn = ISapPlugin
            my_sapModel = SapModel

            InitializePlugIn()

        Catch ex As Exception
            MsgBox("The following error terminated the Plug-In:" & vbCrLf & ex.Message)

            ' call Finish to inform SAP2000 that the PlugIn has terminated
            Try
                ISapPlugin.Finish(1)
            Catch ex1 As Exception
            End Try
        End Try

        Return

    End Sub
#End If

    Sub InitializePlugIn()
#If COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
        myPCControl = New frmPCControl(Me)

        ProgCol.filePathOriginal = sapModel.GetModelFilepath
        ProgCol.fileNameOriginal = sapModel.GetModelFilename

        ' Non-modal call, allows graphics referesh operations in SAP2000, 
        ' but Main will return to SAP2000 before the form is closed.
        myPCControl.Show()

        ' Modal forms will not return to SAP2000 until form is closed,
        ' but causes errors when refreshing the view.
        '''' aForm.ShowDialog()
#End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    '=== General Instructions

    'For builds:
    '   1. Select v.4.0 of .Net
    '   2. Register for Com Interop
    '   3. Assembly Information > Make Com Visible

    'For adding plugin: 
    '   1. DLL does not need registration when DLL comes directly from a build.
    '       1a. Else, run a batch file with the following command
    REM     see https://wiki.csiamerica.com/display/kb/Registering+plugins
    '       C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm /tlb /verbose  [AssemblyName].dll
    '   2. When specifying the plugin name, use the name provided for the Root Namespace and NOT the Assembly Name.

    '=== cPlugin.Main() 

    'aForm.setParentPluginObject(Me)
    'aForm.setSapModel(SapModel, ISapPlugin)
    'or
    'my_sapPlugIn = ISapPlugin
    'my_sapModel = SapModel

    ' Non-modal call, allows graphics referesh operations in SAP2000, 
    ' but Main will return to SAP2000 before the form is closed.
    '''' aForm.Show()

    ' Modal forms will not return to SAP2000 until form is closed,
    ' but causes errors when refreshing the view.
    '''' aForm.ShowDialog()

    ' It is very important to call ISapPlugin.Finish(iError) when form closes, !!!
    ' otherwise, SAP2000 will wait and be hung !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    ' this must be done inside the closing event for the form itself, not here !!!

    ' if you simply have algorithmic code here without any forms, 
    ' then call ISapPlugin.Finish(iError) here before returning to SAP2000

    ' if your code will run for more than a few seconds, you should exercise
    ' the Windows messaging loop to keep the program responsive. You may 
    ' also want to provide an opportunity for the user to cancel operations.

    '=== Modal Forms
    'Our testing has indicated that modal forms in .NET DLL’s are problematic when shown within a SAP2000 external plug in, 
    'especially if you try to perform refresh operations to the views or windows.

    'If you want to provide multiple functionality in your plug in, you can provide options for the user when subroutine Main is called. 
    'Options for the user to obtain information about the product, developer, technical support, and help should be provided. 
    'Support for your plug in will not be provided by Computers and Structures, Inc.

    '=== Closing the Plugin
    'Sap2000.cSapPlugin contains a Finish() subroutine that is to be called right before the plug in is ready to close 
    '   (e.g., if the plug in has a single main window, at the end of the close event of that form). 
    'It expects an error flag (0 meaning no errors) to let SA2000 know if the operation was successful or not. 
    'SAP2000 will wait indefinitely for Sap2000.cSapPlugin.Finish() to be called, so the plug in programmer must make sure 
    '   that it is called when the execution of the plug in code is completed.

    'It is OK for cPlugin.Main() to return before the actual work is completed. (e.g., return after displaying a form 
    '   where the functionally implemented in the plug in can be accessed through different command buttons). 
    '   However, it is imperative to remember to call Sap2000.cSapPlugin.Finish() to return the control back to SAP2000 
    '   when the plug in is ready to close.

    '=== cPLugin & Daving Data
    'As currently implemented, the cPlugin object will be destroyed between invocations from the SAP2000 Tools menu command 
    '   that calls it, so DATA CANNOT BE SAVED. All operations in the plug in must be completed before the user can perform 
    '   any other operations within SAP2000 itself.
End Class


