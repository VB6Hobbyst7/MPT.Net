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

Public Class CSiFile

#Region "Fields"
    ''' <summary>
    ''' File name of the current model, as tracked by the program and not obtained from the API.
    ''' </summary>
    ''' <remarks></remarks>
    Private _FileName As String
    ''' <summary>
    ''' File path of the current model, as tracked by the program and not obtained from the API.
    ''' </summary>
    ''' <remarks></remarks>
    Private _FilePath As String
#End Region

#Region "Properties"
    ''' <summary>
    ''' Filename of the current model file, with or without the full path.
    ''' </summary>
    ''' <param name="includePath">True: Returned filename includes the full path where the file is located.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName(Optional ByVal includePath As Boolean = True) As String
        Get
            Return GetModelFileName(includePath)
        End Get
    End Property

    ''' <summary>
    ''' Path to the current model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilePath As String
        Get
            Return GetModelFilePath()
        End Get
        Private Set(value As String)
            _FilePath = value
            _FileName = IO.Path.GetFileName(value)
        End Set
    End Property
#End Region

#Region "Initialization"
    Public Sub New()

    End Sub
#End Region

#Region "Methods: Public"

    ''' <summary>
    ''' Opens the specified model file if it exists.
    ''' </summary>
    ''' <param name="filePath">The full path of a model file to be opened in the application.</param>
    ''' <remarks> The file name must have an sdb, $2k, s2k, xlsx, xls, or mdb extension. 
    ''' Files with sdb extensions are opened as standard Sap2000 files. 
    ''' Files with $2k and s2k extensions are imported as text files. 
    ''' Files with xlsx and xls extensions are imported as Microsoft Excel files. 
    ''' Files with mdb extensions are imported as Microsoft Access files.
    ''' This function returns zero if the file is successfully opened and nonzero if it is not opened.
    ''' The function is only applicable when you are accessing the application API from an external application. 
    ''' It will return an error if you call it from VBA inside Sap2000.
    '''</remarks>
    Public Function Open(ByVal filePath As String) As Boolean
        Try
            If Not IO.File.Exists(filePath) Then Throw New IO.IOException("The following file does not exist: " & filePath)

            'TODO: Add checks for appropriate file types for the given open program.

            ret = SapModel.File.OpenFile(filePath)
            If ret = 0 Then _FileName = IO.Path.GetFileName(filePath)
            Return retCheck(ret, "SapModel.File.OpenFile")
        Catch ex As IO.IOException
            csiLogger.ExceptionAction(ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Saves the model to a file. 
    ''' If no file name is specified, the file is saved using its current name.
    ''' </summary>
    ''' <param name="filePath">The full path to which the model file is saved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(Optional ByVal filePath As String = "") As Boolean
        Try
            If String.IsNullOrEmpty(_FileName) Then Throw New IO.IOException("The current model has not been previously saved. Please provide a file name.")

            'save file
            ret = SapModel.File.Save(filePath)
            If (ret = 0 AndAlso Not String.IsNullOrEmpty(filePath)) Then
                Me.FilePath = filePath
            End If
            Return retCheck(ret, "SapModel.File.Save")
        Catch ex As IO.IOException
            csiLogger.ExceptionAction(ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Creates a new blank model from template.
    ''' Do not use this function to add to an existing model. 
    ''' This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NewBlank() As Boolean
        'create a blank model from template
        ret = SapModel.File.NewBlank
        Return retCheck(ret, "SapModel.File.NewBlank")
    End Function

    ''' <summary>
    ''' Creates a new template model of a 2D Frame.
    ''' Do not use this function to add to an existing model. 
    ''' This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    ''' </summary>
    ''' <param name="templateModel">Template model object representing the template to be created.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function New2DFrame(ByVal templateModel As TemplateFrame2D) As Boolean
        'create a 2D frame model from template
        With templateModel
            ret = SapModel.File.New2DFrame(.TemplateType, .NumberStories, .StoryHeight, .NumberBays, .BayWidth, .Restraint, .Beam, .Column, .Brace)
        End With

        Return retCheck(ret, "SapModel.File.New2DFrame")
    End Function

    ''' <summary>
    ''' Creates a new template model of a 3D Frame.
    ''' Do not use this function to add to an existing model. 
    ''' This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    ''' </summary>
    ''' <param name="templateModel">Template model object representing the template to be created.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function New3DFrame(ByVal templateModel As TemplateFrame3D) As Boolean
        'create a 3D frame model from template
        With templateModel
            ret = SapModel.File.New3DFrame(.TemplateType, .NumberStories, .StoryHeight, .NumberBaysX, .BayWidthX, .NumberBaysY, .BayWidthY, .Restraint, .Beam, .Column, .Area, .NumberFloorXDivisions, .NumberFloorYDivisions)
        End With

        Return retCheck(ret, "SapModel.File.New3DFrame")
    End Function

    ''' <summary>
    ''' Creates a new template model of a Beam.
    ''' Do not use this function to add to an existing model. 
    ''' This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    ''' </summary>
    ''' <param name="templateModel">Template model object representing the template to be created.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NewBeam(ByVal templateModel As TemplateBeam) As Boolean

        'create a beam from template
        With templateModel
            ret = SapModel.File.NewBeam(.NumberSpans, .SpanLength, .Restraint, .Beam)
        End With
 
        Return retCheck(ret, "SapModel.File.NewBeam")
    End Function

    ''' <summary>
    ''' Creates a new template model of a Wall.
    ''' Do not use this function to add to an existing model. 
    ''' This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    ''' </summary>
    ''' <param name="templateModel">Template model object representing the template to be created.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NewWall(ByVal templateModel As TemplateWall) As Boolean
        'create a wall model from template
        With templateModel
            ret = SapModel.File.NewWall(.NumberMeshX, .WidthMeshX, .NumberMeshZ, .HeightMeshZ, .Restraint, .Area)
        End With

        Return retCheck(ret, "SapModel.File.NewWall")
    End Function

    ''' <summary>
    ''' Creates a new template model of a Solid.
    ''' Do not use this function to add to an existing model. 
    ''' This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    ''' </summary>
    ''' <param name="templateModel">Template model object representing the template to be created.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NewSolidBlock(ByVal templateModel As TemplateSolidBlock) As Boolean
        'create a solid model from template
        With templateModel
            ret = SapModel.File.NewSolidBlock(.WidthMeshX, .WidthMeshY, .HeightMeshZ, .Restraint, .Solid, .NumberMeshX, .NumberMeshY, .NumberMeshZ)
        End With

        Return retCheck(ret, "SapModel.File.NewSolidBlock")
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' The function returns a string that represents the filename of the current model, with or without the full path.
    ''' </summary>
    ''' <param name="includePath">True: Returned filename includes the full path where the file is located.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetModelFileName(Optional ByVal includePath As Boolean = True) As String
        Return SapModel.GetModelFilename
    End Function

    ''' <summary>
    ''' Returns a string that represents the filepath of the current model.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetModelFilePath() As String
        Return SapModel.GetModelFilepath
    End Function
#End Region
End Class
