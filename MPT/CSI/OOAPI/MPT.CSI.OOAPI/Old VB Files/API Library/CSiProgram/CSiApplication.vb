Option Explicit On
Option Strict On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If
Imports CSiApiTester.cEnumerations

''' <summary>
''' Manipulates the CSi application, such as opening, closing, setting visiblity and active object, etc.
''' </summary>
''' <remarks></remarks>
Public Class CSiApplication

#Region "Properties"
    ''' <summary>
    ''' True: The application is visible on the screen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsVisible() As Boolean
        Get
            Return SapObject.Visible
        End Get
    End Property

#End Region

#Region "Initialization"
    Public Sub New()

    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' This function starts the application.
    ''' When the model is not visible it does not appear on screen and it does not appear in the Windows task bar. 
    ''' If no filename is specified, you can later open a model or create a model through the API.
    ''' The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension. 
    ''' Files with .sdb extensions are opened as standard SAP2000 files. 
    ''' Files with .$2k and .s2k extensions are imported as text files. 
    ''' Files with .xls extensions are imported as Microsoft Excel files. 
    ''' Files with .mdb extensions are imported as Microsoft Access files.
    ''' </summary>
    ''' <param name="units">The database units used when a new model is created. 
    ''' Data is internally stored in the program in the database units.</param>
    ''' <param name="visible">True: The application is visible when started.  False: The application is hidden when started.</param>
    ''' <param name="fileName">The full path of a model file to be opened when the application is started. 
    ''' If no file name is specified, the application starts without loading an existing model.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ApplicationStart(Optional ByVal units As eUnits = eUnits.kip_in_F,
                            Optional ByVal visible As Boolean = True,
                            Optional ByVal fileName As String = "") As Boolean
        ret = SapObject.ApplicationStart(units, visible, fileName)

        Return retCheck(ret, "SapObject.ApplicationStart")
    End Function

    ''' <summary>
    ''' This function closes the application. 
    ''' If the model file is saved then it is saved with its current name.
    ''' </summary>
    ''' <param name="fileSave">True: The existing model file is saved prior to closing program.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ApplicationExit(ByVal fileSave As Boolean) As Boolean
        'close application
#If COMPILE_ETABS2013 Or COMPILE_ETABS2014 Then
        ret = ETABSObject.ApplicationExit(fileSave)

        SapModel = Nothing
        ETABSObject = Nothing
#ElseIf COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
        ret = SapObject.ApplicationExit(fileSave)

        SapModel = Nothing
        SapObject = Nothing
#End If

        Return retCheck(ret, "SapObject.ApplicationExit")
    End Function

    ''' <summary>
    ''' This function hides the application. 
    ''' When the application is hidden it is not visible on the screen or on the Windows task bar.
    ''' If the application is already hidden, no action is taken.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Hide() As Boolean
        If Not IsVisible Then Return True

        'hide application
        ret = SapObject.Hide
        Return retCheck(ret, "SapObject.Hide")
    End Function

    ''' <summary>
    ''' This function unhides the application. 
    ''' When the application is hidden it is not visible on the screen or on the Windows task bar.
    ''' If the application is already visible, no action is taken.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UnHide() As Boolean
        If IsVisible Then Return True

        'unhide application
        ret = SapObject.Unhide
        Return retCheck(ret, "SapObject.Unhide")
    End Function

    ''' <summary>
    ''' This function sets the active instance of a SapObject in the system Running Object Table (ROT), replacing the previous instance(s).
    ''' When a new SapObject is created using the OAPI, it is automatically added to the system ROT if none is already present. 
    ''' Subsequent instances of the SapObject do not alter the ROT as long as at least one active instance of a SapObject is present in the ROT.
    ''' The Windows API call GetObject() can be used to attach to the active SapObject instance registered in the ROT. 
    ''' When the application is started normally (i.e. not from the OAPI), it does not add an instance of the SapObject to the ROT, hence the GetObject() call cannot be used to attach to the active SapObject instance.
    ''' The Windows API call CreateObject() or New Sap2000v16.SapObject command can be used to attach to an instance of SAP2000 that is started normally (i.e. not from the OAPI). 
    ''' If there are multiple such instances, the first instance that will be attached to is the one that is started first.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAsActiveObject() As Boolean
        'set as active SapObject instance
        ret = SapObject.SetAsActiveObject

        Return retCheck(ret, "SapObject.SetAsActiveObject")
    End Function

    ''' <summary>
    ''' This function removes the current instance of a SapObject from the system Running Object Table (ROT).
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UnsetAsActiveObject() As Boolean
        'unset as active SapObject instance
        ret = SapObject.UnsetAsActiveObject

        Return retCheck(ret, "SapObject.SetAsActiveObject")
    End Function

#End Region

End Class
