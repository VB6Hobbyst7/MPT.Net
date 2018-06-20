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

Public Class CSiModel

#Region "Properties"
    Private _AnalysisModel As New AnalysisModel()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AnalysisModel As AnalysisModel
        Get
            Return _AnalysisModel
        End Get
    End Property

    Private _Analyze As New Analyzer()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Analyze As Analyzer
        Get
            Return _Analyze
        End Get
    End Property

    Private _Application As New CSiApplication()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Application As CSiApplication
        Get
            Return _Application
        End Get
    End Property

    Private _Definitions As New Definitions()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Definitions As Definitions
        Get
            Return _Definitions
        End Get
    End Property

    Private _Design As New Designer()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Design As Designer
        Get
            Return _Design
        End Get
    End Property

    Private _Editor As New Editor()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Editor As Editor
        Get
            Return _Editor
        End Get
    End Property


    Private _File As CSiFile = New CSiFile()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property File As CSiFile
        Get
            Return _File
        End Get
    End Property

    ''' <summary>
    ''' True: Model is unlocked.
    ''' With some exceptions, definitions and assignments can not be changed in a model while the model is locked. 
    ''' If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsModelLocked As Boolean
        Get
            Return GetModelIsLocked()
        End Get
    End Property


    Private _ObjectModel As New ObjectModel()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ObjectModel As ObjectModel
        Get
            Return _ObjectModel
        End Get
    End Property

    Private _Options As New Options()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Options As Options
        Get
            Return _Options
        End Get
    End Property


    Private _Path As String
    ''' <summary>
    ''' Path to the CSi application that the class manipulates.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Path As String
        Get
            Return _Path
        End Get
    End Property

    Private _Results As New Results()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Results As Results
        Get
            Return _Results
        End Get
    End Property

    Private _Selector As New Selector()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Selector As Selector
        Get
            Return _Selector
        End Get
    End Property

    Private _Viewer As New Viewer()
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Editor As Viewer
        Get
            Return _Viewer
        End Get
    End Property
#End Region

#Region "Initialization"
    Public Sub New(ByVal path As String)
        _Path = path
    End Sub

    ''' <summary>
    ''' Opens a fresh instance of the CSi program. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeProgram() As Boolean
        Try
            If Not IO.File.Exists(_Path) Then Throw New IO.IOException("The following CSi program path is invalid: " & _Path)
#If COMPILE_ETABS2013 Then
        'Create program object
        myAssembly = System.Reflection.Assembly.LoadFrom(_Path)

        'Create an instance of ETABSObject and get a reference to cOAPI interface 
        ETABSObject = DirectCast(myAssembly.CreateInstance("CSI.ETABS.API.ETABSObject"), cOAPI)

        'start ETABS application
        ETABSObject.ApplicationStart()

        'create SapModel object
        SapModel = ETABSObject.SapModel

#ElseIf COMPILE_ETABS2014 Then
        ''Old Method: 32bit OAPI clients can only call 32bit ETABS 2014 and 64bit OAPI clients can only call 
        '   64bit ETABS 2014. Currently only used in ETABS 2013.
        ''Create an instance of ETABSObject and get a reference to cOAPI interface 
        'ETABSObject = DirectCast(myAssembly.CreateInstance("CSI.ETABS.API.ETABSObject"), cOAPI)

        ''New Method: 32bit & 64bit API clients can call 32 & 64bit ETABS 2014
        'Use the new OAPI helper class to get a reference to cOAPI interface
        helper = New Helper
        ETABSObject = helper.CreateObject(_Path)

        'start ETABS application
        ETABSObject.ApplicationStart()

        'create SapModel object
        SapModel = ETABSObject.SapModel
#ElseIf COMPILE_SAP2000v16 Then
            'NOTE: No path is needed for SAP2000v16. Instead, CSiProgressiveCollapse will automatically use the
            '   version currently installed. To change the version, say for testing, run the desired v16 version as 
            '   administrator first in order to register.
            SapObject = New SAP2000v16.SapObject

            'start Sap2000 application
            SapObject.ApplicationStart()

            'create SapModel object
            SapModel = SapObject.SapModel
#ElseIf COMPILE_SAP2000v17 Then
        ''Old Method: 32bit OAPI clients can only call 32bit ETABS 2014 and 64bit OAPI clients can only call 64bit ETABS 2014
        ''Create program object
        'SapObject = DirectCast(myAssembly.CreateInstance("CSI.SAP2000.API.SapObject"), cOAPI)

        ''New Method: 32bit & 64bit API clients can call 32 & 64bit ETABS 2014
        'Use the new OAPI helper class to get a reference to cOAPI interface
        helper = New Helper
        SapObject = helper.CreateObject(_Path)

        'start Sap2000 application
        SapObject.ApplicationStart()

        'create SapModel object
        SapModel = SapObject.SapModel
#End If
            Return InitializeNewModel()
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return False
    End Function

    ''' <summary>This function clears the previous model and initializes the program for a new model. 
    ''' If it is later needed, you should save your previous model prior to calling this function.
    ''' After calling the InitializeNewModel function, it is not necessary to also call the ApplicationStart function because the functionality of the ApplicationStart function is included in the InitializeNewModel function.
    ''' </summary>
    ''' <param name="units">The database units for the new model. 
    ''' All data is internally stored in the model in these units.</param>
    ''' <remarks></remarks>
    Public Function InitializeNewModel(Optional units As eUnits = eUnits.kip_in_F) As Boolean
        ret = SapModel.InitializeNewModel(units)

        Return retCheck(ret, "SapModel.InitializeNewModel")
    End Function
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Save and Run Model.
    ''' </summary>
    ''' <param name="filePath">File path to the file as it is to be saved (can save a model under a different name).
    ''' If not specified, then the file will be saved under the current file path.</param>
    ''' <remarks></remarks>
    Public Sub RunAnalysis(Optional ByVal filePath As String = "")
        If String.IsNullOrEmpty(filePath) Then filePath = File.FilePath

        'File is to be saved before analysis
        If Not String.IsNullOrEmpty(filePath) Then File.Save(filePath)

        Analyze.RunAnalysis(filePath)
    End Sub

    'Model States
    ''' <summary>
    ''' Locks the model.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LockModel()
        SetModelIsLocked(lockModel:=True)
    End Sub

    ''' <summary>
    ''' Unlocks the model.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UnlockModel()
        SetModelIsLocked(lockModel:=False)
    End Sub

    ''' <summary>
    ''' Remove analysis and design results and unlock model.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UnlockModelAndClearResults()
        Design.DeleteResults()

        Analyze.DeleteResults()

        UnlockModel()
    End Sub

    ''' <summary>
    ''' Removes load patterns, cases &amp; combos that have been created in prior runs.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearPriorModelState()
        UnlockModelAndClearResults()
        ClearLoadCombos()
        ClearLoadCases()
        ClearLoadPatterns()
    End Sub


    ' User Comment
    ''' <summary>
    ''' This function sets the user comments and log data.
    ''' </summary>
    ''' <param name="commentUser">The data to be added to the user comments and log.</param>
    ''' <param name="numberLinesBlank">The number of carriage return and line feeds to be included before the specified comment. 
    ''' This item is ignored if there are no existing comments.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AppendUserComment(ByRef commentUser As String,
                                      Optional ByVal numberLinesBlank As Integer = 1) As Boolean
        Return SetUserComment(commentUser, numberLinesBlank)
    End Function

    ''' <summary>
    ''' This function sets the user comments and log data.
    ''' </summary>
    ''' <param name="commentUser">The data to be added to the user comments and log.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReplaceUserComment(ByRef commentUser As String) As Boolean
        Return SetUserComment(commentUser, Replace:=True)
    End Function


    'Get/Set Methods

    ' -----
    'Units
    ''' <summary>
    ''' Returns the database units for the model. 
    ''' All data is internally stored in the model in these units and converted to the present units as needed.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDatabaseUnits() As eUnits
        Return SapModel.GetDatabaseUnits
    End Function


    ''' <summary>
    ''' Returns the units presently specified for the model.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPresentUnits() As eUnits
        Return SapModel.GetPresentUnits
    End Function

    ''' <summary>
    ''' Sets the units presently specified for the model.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPresentUnits(ByVal units As eUnits) As Boolean
        ret = SapModel.SetPresentUnits(units)

        Return retCheck(ret, "SapModel.SetPresentUnits")
    End Function


    ' -----
    ''' <summary>
    ''' Retrieves the value of the program auto merge tolerance.
    ''' </summary>
    ''' <param name="mergeTolerance">The program auto merge tolerance. [L]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMergeTolerance(ByRef mergeTolerance As Double) As Boolean
        ret = SapModel.GetMergeTol(mergeTolerance)
        Return retCheck(ret, "SapModel.GetMergeTol")
    End Function

    ''' <summary>
    ''' Sets the value of the program auto merge tolerance.
    ''' </summary>
    ''' <param name="mergeTolerance">The program auto merge tolerance. [L]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetMergeTolerance(ByVal mergeTolerance As Double) As Boolean
        ret = SapModel.SetMergeTol(mergeTolerance)
        Return retCheck(ret, "SapModel.SetMergeTol")
    End Function


    ' -----
    ''' <summary>
    ''' Returns the name of the present coordinate system.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPresentCoordSystem() As String
        Return SapModel.GetPresentCoordSystem()
    End Function

    ''' <summary>
    ''' Sts the present coordinate system.
    ''' </summary>
    ''' <param name="nameCoordinateSystem">The name of a defined coordinate system.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetPresentCoordSystem(ByVal nameCoordinateSystem As String) As Boolean
        ret = SapModel.SetPresentCoordSystem(nameCoordinateSystem)
        Return retCheck(ret, "SapModel.SetPresentCoordSystem")
    End Function


    ' -----
    ''' <summary>
    ''' Retrieves the project information data.
    ''' </summary>
    ''' <param name="projectInfoItemsAndData">List of 'Name'/'Data' pairs for the project information.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetProjectInfo(ByRef projectInfoItemsAndData As Dictionary(Of String, String)) As Boolean
        Dim numberItems As Integer = projectInfoItemsAndData.Count
        Dim projectInfoItems() As String = projectInfoItemsAndData.Keys.ToArray
        Dim projectInfoData() As String = projectInfoItemsAndData.Values.ToArray


        If GetProjectInfo(numberItems, projectInfoItems, projectInfoData) Then
            For i = 0 To UBound(projectInfoItems)
                projectInfoItemsAndData.Add(projectInfoItems(i), projectInfoData(i))
            Next

            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Sets the data for all items in the project information list.
    ''' </summary>
    ''' <param name="projectInfoItemsAndData">List of 'Name'/'Data' pairs for the project information.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetProjectInfo(ByRef projectInfoItemsAndData As Dictionary(Of String, String)) As Boolean
        Dim success As Boolean = True

        For Each projectInfoItem As String In projectInfoItemsAndData.Keys
            If Not SetProjectInfo(projectInfoItem, projectInfoItemsAndData(projectInfoItem)) AndAlso success Then success = False
        Next

        Return success
    End Function

    ''' <summary>
    ''' Sets the data for an item in the project information.
    ''' </summary>
    ''' <param name="projectInfoItem">Name of the project information item.</param>
    ''' <param name="projectInfoData">Data for the specified project information item.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetProjectInfo(ByVal projectInfoItem As String,
                                   ByVal projectInfoData As String) As Boolean
        ret = SapModel.SetProjectInfo(projectInfoItem, projectInfoData)
        Return retCheck(ret, "SapModel.SetProjectInfo")
    End Function

#End Region


#Region "Methods: Private"
    'Get/Set Methods

    ' -----
    ''' <summary>
    ''' True: Model is unlocked.
    ''' With some exceptions, definitions and assignments can not be changed in a model while the model is locked. 
    ''' If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetModelIsLocked() As Boolean
        Return SapModel.GetModelIsLocked()
    End Function

    ''' <summary>
    ''' Sets whether or not the model is locked.
    ''' With some exceptions, definitions and assignments can not be changed in a model while the model is locked. 
    ''' If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
    ''' </summary>
    ''' <param name="lockModel">True: Model will be locked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetModelIsLocked(ByVal lockModel As Boolean) As Boolean
        ret = SapModel.SetModelIsLocked(lockModel)
        Return retCheck(ret, "SapModel.SetModelIsLocked")
    End Function


    ' -----
    ''' <summary>
    ''' Retrieves the data in the user comments and log.
    ''' </summary>
    ''' <param name="commentUser">The data in the user comments and log.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUserComment(ByRef commentUser As String) As Boolean
        ret = SapModel.GetUserComment(commentUser)
        Return retCheck(ret, "SapModel.GetUserComment")
    End Function

    ''' <summary>
    ''' This function sets the user comments and log data.
    ''' </summary>
    ''' <param name="commentUser">The data to be added to the user comments and log.</param>
    ''' <param name="numberLinesBlank">The number of carriage return and line feeds to be included before the specified comment. 
    ''' This item is ignored if there are no existing comments.</param>
    ''' <param name="replace">True: All existing comments are replaced with the specified comment.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetUserComment(ByRef commentUser As String,
                                    Optional ByVal numberLinesBlank As Integer = 1,
                                    Optional ByVal replace As Boolean = False) As Boolean
        ret = SapModel.SetUserComment(commentUser, numberLinesBlank, replace)
        Return retCheck(ret, "SapModel.SetUserComment")
    End Function


    ' -----
    ''' <summary>
    ''' Retrieves the project information data.
    ''' </summary>
    ''' <param name="numberItems">The number of project info items returned.</param>
    ''' <param name="projectInfoItems">This is an array that includes the name of the project information item.</param>
    ''' <param name="projectInfoData">This is an array that includes the data for the specified project information item.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProjectInfo(ByRef numberItems As Integer,
                                   ByRef projectInfoItems() As String,
                                   ByRef projectInfoData() As String) As Boolean
        ret = SapModel.GetProjectInfo(numberItems, projectInfoItems, projectInfoData)
        Return retCheck(ret, "SapModel.GetProjectInfo")
    End Function
#End Region

End Class
