Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter

Imports CSiTester.cMCModel
Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment

''' <summary>
''' lass representing a path to an output settings file associated with the model file.
''' </summary>
''' <remarks></remarks>
Public Class cPathOutputSettings
    Inherits cPathModelControlReference
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Shared Event SharedLog(exception As LoggerEventArgs)

#Region "Constants"
    Private Const _PATHNODE As String = "//n:filename"

    Friend Const FILENAME_SEED_XML As String = "seed outputSettings.xml"
    Friend Const FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML As String = "_OutputSettings.xml"
    Friend Const DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT As String = "outputSettings"

#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Path to the seed file used to create a new outputSettings.xml file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Property seedPathOS As String = testerSettings.seedDirectory.path & "\" & FILENAME_SEED_XML

    ''' <summary>
    ''' Model file to sync with the outputSettings file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property modelFile As cPathModel
        Get
            If (_mcModel IsNot Nothing AndAlso
                _mcModel.modelFile IsNot Nothing AndAlso
                _mcModel.modelFile.PathModelDestination IsNot Nothing) Then
                Return _mcModel.modelFile.PathModelDestination
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Model control file associated with this object. 
    ''' A cloned copy is returned, so the original cannot be altered.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property modelControlFile As cMCModel
        Get
            If _mcModel IsNot Nothing Then
                Return CType(_mcModel.Clone(), cMCModel)
            Else
                Return Nothing
            End If
        End Get
    End Property
#End Region

#Region "Event Handlers"
    Protected Sub RaiseModelNameChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If (e.PropertyName = NameOfProp(Function() _mcModel.modelFile)) Then
            If (e.PropertyName = NameOfProp(Function() _mcModel.modelFile.pathDestination.fileName) OrElse
                e.PropertyName = NameOfProp(Function() _mcModel.modelFile.PathModelDestination.importTag)) Then

                UpdateFileName()
            End If
        End If
    End Sub
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Sets properties based on the model file object provided.
    ''' Priority of properties is given to the path if provided.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to use for setting class properties, including a reference.</param>
    ''' <param name="p_pathFile">Path to an existing outputSettings file.</param>
    ''' <param name="p_setToNothing">True: The path object created will be empty of any specified path. 
    ''' A filename will be created if there is a corresponding model file..</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_pathFile As String = "",
                   Optional ByVal p_setToNothing As Boolean = False)
        _pathType = ePathType.FileWithExtension
        'Sets the binding and also the path based on the binding
        If p_bindTo IsNot Nothing Then SetMCModel(p_bindTo)

        'Overwrite the path set by the model control object if one is provided.
        If (Not String.IsNullOrEmpty(p_pathFile) AndAlso
            IO.File.Exists(p_pathFile)) Then

            MyBase.SetProperties(p_pathFile, p_bindTo)
        ElseIf p_setToNothing Then
            MyBase.SetProperties(NewFileName())
        Else
            MyBase.SetProperties(seedPathOS)
        End If

    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cPathOutputSettings = DirectCast(MyBase.Clone, cPathOutputSettings)

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathOutputSettings()
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets the file name of the outputSettings file based on the model name and import tag.
    ''' The directory is updated to the expected default if it isn't currently specified.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateFileName()
        MyBase.SetProperties(NewFileName())
    End Sub

    ''' <summary>
    ''' Updates relevant properties affected by the model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetMCModel(ByVal p_mcModel As cMCModel)
        If p_mcModel Is Nothing Then Exit Sub

        MyBase.SetMCModel(p_mcModel)
        UpdateFileName()
    End Sub

    ''' <summary>
    ''' Returns the existing outputSettings file path, if any, associated with the referenced model control file.
    ''' Returns the path to the active outputsettings file first, and otherwise returns the attachment location.
    ''' If no file exists at either location, nothing is returned.
    ''' </summary>
    ''' <param name="p_attachmentOnly">True: Only the existing attachment path will be returned, even if there is a file at the active path.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ExistingOutputSettingsFilePath(Optional ByVal p_attachmentOnly As Boolean = False) As String
        Try
            'Get name from model name - already done in cPath object
            Dim fileName As String = fileNameWithExtension
            Dim modelFileDestination As cPath = modelControlFile.modelFile.pathDestination
            Dim pathTrial As String = modelFileDestination.directory
            If String.IsNullOrEmpty(pathTrial) Then
                Return ""
            Else
                pathTrial &= "\" & fileName
            End If

            'Check model location
            If (Not p_attachmentOnly AndAlso
                IO.File.Exists(pathTrial)) Then
                Return pathTrial
            Else
                'Check attachment location
                Return ExistingOutputSettingsFilePathAttachment()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return ""
    End Function
#End Region

#Region "Methods: Friend Shared"
    ''' <summary>
    ''' Returns the path, including filename, of the outputSettings file for it to be considered in an 'active' state, whereby it interacts with the ETABS model.
    ''' </summary>
    ''' <param name="p_pathOutputSettings">Path to the file to use.</param>
    ''' <param name="p_fileName">Filename of the file to check. If not supplied, it is derived from the file path.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function GetOutputSettingsActivePath(ByVal p_pathOutputSettings As String,
                                                        Optional ByVal p_fileName As String = "") As String

        If String.IsNullOrEmpty(p_fileName) Then p_fileName = GetPathFileName(p_pathOutputSettings)

        Dim parentDirectory As String = GetPathDirectorySubStub(p_pathOutputSettings, 1)
        Dim pathToFileAtModelDir As String = parentDirectory & "\" & DIR_NAME_MODELS_DEFAULT & "\" & p_fileName

        Return pathToFileAtModelDir
    End Function

    ''' <summary>
    ''' The file name and internal values of all outputSettings files in the specified directory are updated by the model name specified.
    ''' </summary>
    ''' <param name="p_pathOutputSettingsDir">Path to the output settings XML file folder.</param>
    ''' <param name="p_fileNameModel">Name of the model file to sync with the outputSettings file. 
    ''' This updates the name of the XML file and an internal value.</param>
    ''' <remarks></remarks>
    Friend Shared Sub SyncOutputSettingsFilesToModelName(ByVal p_pathOutputSettingsDir As String,
                                                         ByVal p_fileNameModel As String)
        Try
            'Get list of XML files
            Dim xmlFilesList As List(Of String) = ListFilePathsInDirectory(p_pathOutputSettingsDir, False, , ".xml")

            'Change file names & XML content if valid
            For Each xmlFileName As String In xmlFilesList
                SyncOutputSettingsFileToModelName(xmlFileName, p_pathOutputSettingsDir, p_fileNameModel)
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Renames the outputsettings files to remove the import tag in the outputsettings.xml file name if the file is not a supporting file. 
    ''' The tag is the "_V{13}" portion of the name.
    ''' </summary>
    ''' <param name="p_parentFolderPath">Path to the parent folder, within which all files will be renamed.</param>
    ''' <remarks></remarks>
    Friend Shared Sub RemoveImportTagFromFileName(ByVal p_parentFolderPath As String)
        Dim xmlFilesList As New List(Of String)
        Dim newXmlFileName As String

        Try
            xmlFilesList = ListFilePathsInDirectory(p_parentFolderPath, True, , ".xml")

            For Each xmlFilePath As String In xmlFilesList
                newXmlFileName = ""
                'Identifying parts of filenames match
                If (IsOutputSettingsFile(xmlFilePath) AndAlso
                    Not IsSupportingFile(xmlFilePath)) Then         'Remove version tag from file name

                    newXmlFileName = GetPathFileName(FilterStringFromName(xmlFilePath, testerSettings.outputSettingsVersionSession, True, True))
                    RenameFile(xmlFilePath, newXmlFileName)
                End If
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Renames the outputsettings files to add the import tag in the outputsettings.xml file name if the file is a supporting file. 
    ''' The tag is the "_V{13}" portion of the name.
    ''' </summary>
    ''' <param name="p_parentFolderPath">Path to the parent folder, within which all files will be renamed.</param>
    ''' <remarks></remarks>
    Friend Shared Sub AddImportTagToFileName(ByVal p_parentFolderPath As String)
        Dim xmlFilesList As New List(Of String)
        Dim newXmlFileName As String

        Try
            xmlFilesList = ListFilePathsInDirectory(p_parentFolderPath, True, , ".xml")

            For Each xmlFilePath As String In xmlFilesList
                newXmlFileName = ""
                'Identifying parts of filenames match
                If (IsOutputSettingsFile(xmlFilePath) AndAlso
                    IsSupportingFile(xmlFilePath)) Then         'Add version tag from file name

                    newXmlFileName = GetPathFileName(xmlFilePath)
                    newXmlFileName = FilterStringFromName(xmlFilePath, FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML, True, False)
                    newXmlFileName = newXmlFileName & testerSettings.outputSettingsVersionSession & FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
                    RenameFile(xmlFilePath, newXmlFileName)
                End If
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determines if the file in the provided path is an outputSettings file by checking the suffix of the file name.
    ''' </summary>
    ''' <param name="p_filePath">Path to the file to be checked, or the file name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function IsOutputSettingsFile(ByVal p_filePath As String) As Boolean
        Dim tagLength As Integer = FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML.Length
        Dim fileSuffix As String = Right(p_filePath, tagLength)

        If StringsMatch(fileSuffix, FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Methods: Private Shared"
    ''' <summary>
    ''' The file name and internal values of the outputSettings file are updated by the model name specified.
    ''' </summary>
    ''' <param name="p_xmlFileName">Filename of the XML file being considered.</param>
    ''' <remarks></remarks>
    Private Shared Sub SyncOutputSettingsFileToModelName(ByVal p_xmlFileName As String,
                                                          ByVal p_pathOutputSettingsDir As String,
                                                          ByVal p_fileNameModel As String)

        If IsOutputSettingsFile(p_xmlFileName) Then
            Dim pathOutputSettings As String = p_pathOutputSettingsDir & "\" & p_xmlFileName
            Dim tablesExportExtension As String = GetTablesExportExtension(pathOutputSettings)
            Dim newFileName As String = p_fileNameModel & tablesExportExtension

            'Update node text of output file
            Dim xmlReaderWriter As New cXmlReadWrite()
            xmlReaderWriter.WriteSingleXMLNodeValue(pathOutputSettings, _PATHNODE, p_fileNameModel & tablesExportExtension)

            'SyncFileNameToModelName(pathOutputSettings)
            RenameFile(pathOutputSettings, newFileName)
        End If
    End Sub

    ''' <summary>
    ''' Gets the current exported table set file extension in the output file.
    ''' </summary>
    ''' <param name="p_pathOutputSettings">Full path to the outputSettings file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetTablesExportExtension(ByVal p_pathOutputSettings As String) As String
        Dim tablesExportExtension As String = ""

        Dim xmlReaderWriter As New cXmlReadWrite()
        xmlReaderWriter.GetSingleXMLNodeValue(p_pathOutputSettings, _PATHNODE, tablesExportExtension, , True)
        tablesExportExtension = "." & GetSuffix(tablesExportExtension, ".")

        Return tablesExportExtension
    End Function



    ''' <summary>
    ''' Determines whether or not the provided filepath is to a supporting file based on it existing in the 'model' directory.
    ''' </summary>
    ''' <param name="p_filePath">Path to the file to check.</param>
    ''' <param name="p_isFilePathActive">If 'true' then the file path is to the model directory with the appropriate file name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsSupportingFile(ByVal p_filePath As String,
                                      Optional ByVal p_isFilePathActive As Boolean = False) As Boolean
        Dim outputSettingsActivePath As String

        If Not p_isFilePathActive Then
            outputSettingsActivePath = GetOutputSettingsActivePath(p_filePath)
        Else
            outputSettingsActivePath = p_filePath
        End If

        'Check if file is a supporting file by seeing if it exists at the model level
        If IO.File.Exists(outputSettingsActivePath) Then
            Return True
        Else
            Return False
        End If
    End Function


#End Region

#Region "Methods: Private"

    ''' <summary>
    ''' Returns a new file name of the file based on the model name and import tag.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NewFileName() As String
        Dim currentPath As cPathModel = modelFile
        If (currentPath Is Nothing OrElse
            String.IsNullOrEmpty(currentPath.fileName)) Then
            Return ""
        Else
            Return currentPath.fileName & currentPath.importTag & FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
        End If
    End Function

    ''' <summary>
    ''' Returns the current or expected directory. 
    ''' If one is currently not specified, it returns the default directory.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CurrentDirectory() As String
        Dim newDirectory As String = ""

        If (_mcModel IsNot Nothing AndAlso
            String.IsNullOrEmpty(directory)) Then
            newDirectory = _mcModel.mcFile.pathDestination.directory
        Else
            newDirectory = directory
        End If

        If _mcModel Is Nothing Then
            Return newDirectory
        ElseIf _mcModel.folderStructure = eFolderStructure.Database Then
            newDirectory &= "\" & DIR_NAME_ATTACHMENTS_DEFAULT
        Else
            newDirectory &= "\" & DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT
        End If

        Return newDirectory
    End Function

    ''' <summary>
    ''' Returns the existing outputSettings attachment file path, if any, associated with the referenced model control file.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistingOutputSettingsFilePathAttachment() As String
        'Get name from model name - already done in cPath object
        Dim fileName As String = fileNameWithExtension
        Dim pathTrial As String = modelControlFile.modelFile.pathDestination.directory & "\" & fileName

        'Check model location
        If IO.File.Exists(pathTrial) Then
            Return pathTrial
        Else
            'Check attachment location
            With modelControlFile
                If .folderStructure = eFolderStructure.Database Then
                    pathTrial = .mcFile.pathDestination.directory & "\" &
                                DIR_NAME_ATTACHMENTS_DEFAULT & "\" &
                                fileName
                Else
                    pathTrial = .mcFile.pathDestination.directory & "\" &
                                DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT & "\" &
                                fileName
                End If
            End With
            If IO.File.Exists(pathTrial) Then Return pathTrial
        End If

        Return ""
    End Function

#End Region
End Class
