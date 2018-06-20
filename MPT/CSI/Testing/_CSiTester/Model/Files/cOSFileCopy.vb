Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment
Imports CSiTester.cFileAttachment

''' <summary>
''' Checks a directory for all outputSettings files that are in 'attachments' folders and copies them to the active path of the model file.
''' </summary>
''' <remarks></remarks>
Public Class cOSFileCopy
    Implements ICloneable
    Implements IMessengerEvent
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Constants: Private"
    Private Const TITLE_INVALID_FILE_NAME As String = "Invalid File Name"
    Private Const PROMPT_INVALID_FILE_NAME As String = "Warning! The following file is not named properly for its intended use: "
    Private Const PROMPT_INVALID_FILE_NAME_ACTION As String = "File will be skipped"

    Private Const TITLE_FILE_COPY_FAILURE As String = "File Copy Failure"
    Private Const PROMPT_FILE_COPY_FAILURE As String = "The following file was not copied: "

    Private Const TITLE_FILE_NOT_EXIST As String = "File Does Not Exist"
    Private Const PROMPT_FILE_NOT_EXIST As String = "The following file does not exist and no action will be taken: "
#End Region

#Region "Properties: Private"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _outputSettingsPaths As New ObservableCollection(Of String)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _outputSettingsFileNameNew As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _outputSettingsFileNameOld As String
#End Region

#Region "Properties: Friend"

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cPathOutputSettings

        With myClone
            'No properties to clone
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathOutputSettings) Then Return False

        Dim comparedObject As cPathOutputSettings = TryCast(p_object, cPathOutputSettings)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            'If Not .fileNameModel = fileNameModel Then isMatch = False

            'No properties to check
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Copies all of the outputSettings.xml files from a source folder to the stored attachments folder.
    ''' </summary>
    ''' <param name="p_sourceDirectory">Directory that contains the outputSettings.xml files.</param>
    ''' <param name="p_destinationDirectory">Directory that contains all examples, which contain an 'attachments' folder within which to copy the files.</param>
    ''' <param name="p_createAttachmentTag">True: An attachment tag will be added to the model control XML file.</param>
    ''' <param name="p_createAttSupportingFileTag">True: A supporting file attachment tag will be added to the model control XML file.</param>
    ''' <remarks></remarks>
    Friend Sub CopyOutputSettingsFilesToAttachmentsDir(ByVal p_sourceDirectory As String,
                                                         ByVal p_destinationDirectory As String,
                                                         ByVal p_createAttachmentTag As Boolean,
                                                         ByVal p_createAttSupportingFileTag As Boolean)
        SetOutputSettingsPaths(p_sourceDirectory)

        'Get list of attachment folders from destination directory
        Dim attachmentDirPaths As New List(Of String)
        attachmentDirPaths = ListFoldersInFolder(p_destinationDirectory, DIR_NAME_ATTACHMENTS_DEFAULT)

        If attachmentDirPaths.Count > 0 Then
            For Each filePath As String In _outputSettingsPaths
                CopyOutputSettingsFileToAttachmentsDir(attachmentDirPaths, filePath, p_createAttachmentTag, p_createAttSupportingFileTag)
            Next
        End If
    End Sub

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_sourceDirectory"></param>
    ''' <remarks></remarks>
    Private Sub SetOutputSettingsPaths(ByVal p_sourceDirectory As String)
        Dim outputSettingsPathsTemp As New List(Of String)
        _outputSettingsPaths = New ObservableCollection(Of String)

        'Get list of outputSettings.xml files from source directory
        outputSettingsPathsTemp = ListFilePathsInDirectory(p_sourceDirectory, True, , "xml")
        For Each filePath As String In outputSettingsPathsTemp
            If StringExistInName(filePath, cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML) Then
                _outputSettingsPaths.Add(filePath)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Copies the specified outputSettings.xml file from a source folder to the stored attachments folder.
    ''' </summary>
    ''' <param name="p_attachmentDirPaths"></param>
    ''' <param name="p_filePath"></param>
    ''' <param name="p_createAttachmentTag"></param>
    ''' <param name="p_createAttSupportingFileTag"></param>
    ''' <remarks></remarks>
    Private Sub CopyOutputSettingsFileToAttachmentsDir(ByVal p_attachmentDirPaths As List(Of String),
                                                         ByVal p_filePath As String,
                                                         ByVal p_createAttachmentTag As Boolean,
                                                         ByVal p_createAttSupportingFileTag As Boolean)
        Dim fileCopied As Boolean
        Dim pathCopied As String = ""

        'Get the example directory name
        For Each dirPath As String In p_attachmentDirPaths
            fileCopied = False
            pathCopied = ""

            'Get the path stub {parentDir}\{Example Name}\{attachments}. 
            'This avoid issues where the {Example name} might occur in different directories
            Dim dirDestinationName As String = FilterStringFromName(dirPath, GetPathDirectorySubStub(dirPath, 3), False, True)

            If UpdateOutputSettingsFileComponent(dirPath, dirDestinationName, p_filePath, pathCopied) Then
                fileCopied = True
                Exit For
            End If
        Next

        If fileCopied Then  'Create attachment tags to MC XML, if specified
            If p_createAttachmentTag Then CreateOutputSettingsAttachmentTags(p_createAttSupportingFileTag, pathCopied)
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                        PROMPT_FILE_COPY_FAILURE & Environment.NewLine & Environment.NewLine & p_filePath,
                                                        TITLE_FILE_COPY_FAILURE))
        End If
    End Sub

    ''' <summary>
    ''' Creates attachment tags for outputSettings files that are attachments, and that are supporting files.
    ''' </summary>
    ''' <param name="p_createAttSupportingFileTag">If true, a supporting file attachment tag will be added to the model control XML file.</param>
    ''' <param name="p_pathCopied">Returns the path used to copy a file.</param>
    ''' <remarks></remarks>
    Private Sub CreateOutputSettingsAttachmentTags(ByVal p_createAttSupportingFileTag As Boolean,
                                                    ByVal p_pathCopied As String)
        Dim attachment As cFileAttachment
        Dim fileName As String
        Dim modelName As String
        Dim mcXmlPath As String
        Dim mcModel As cMCModel

        If Not String.IsNullOrEmpty(p_pathCopied) Then
            'Determine likely file paths
            fileName = GetPathFileName(p_pathCopied)
            modelName = FilterStringFromName(fileName, "_" & GetSuffix(fileName, "_"), True, False)
            modelName = FilterStringFromName(modelName, "_" & GetSuffix(modelName, "_"), True, False)

            mcXmlPath = GetPathDirectorySubStub(p_pathCopied, 1) & "\" & modelName & cPathModelControl.FILE_NAME_SUFFIX_MC_XML

            If Not IO.File.Exists(mcXmlPath) Then
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Error),
                                                            PROMPT_FILE_NOT_EXIST & Environment.NewLine & Environment.NewLine & mcXmlPath,
                                                            TITLE_FILE_NOT_EXIST))
                Exit Sub
            End If

            mcModel = New cMCModel(mcXmlPath)

            'Create basic attachment tag
            attachment = New cFileAttachment
            attachment.title = TAG_ATTACHMENT_TABLE_SET_FILE & fileName
            attachment.PathAttachment.SetProperties(p_pathCopied)
            mcModel.attachments.Add(CType(attachment.Clone, cFileAttachment))

            If p_createAttSupportingFileTag Then
                'Create supporting file attachment tag
                attachment = New cFileAttachment
                attachment.title = TAG_ATTACHMENT_SUPPORTING_FILE & fileName
                attachment.PathAttachment.SetProperties(GetPathDirectorySubStub(p_pathCopied, 1) & "\" & DIR_NAME_MODELS_DEFAULT & "\" & fileName)
                mcModel.attachments.Add(CType(attachment.Clone, cFileAttachment))
            End If

            mcModel.SaveFile()
        End If
    End Sub

    ''' <summary>
    ''' Checks for a matching file destination from a file source and takes action, such as copying, renaming, and deleting files.
    ''' </summary>
    ''' <param name="p_dirPath">Path to the destination directory being checked.</param>
    ''' <param name="p_dirDestinationName">Path directory stub that uniquely identifies the destination.</param>
    ''' <param name="p_filePathSource">Path to the file source being checked.</param>
    ''' <param name="p_pathCopied">Returns the path used to copy a file.</param>
    ''' <remarks></remarks>
    Private Function UpdateOutputSettingsFileComponent(ByVal p_dirPath As String,
                                                       ByVal p_dirDestinationName As String,
                                                       ByVal p_filePathSource As String,
                                                       ByRef p_pathCopied As String) As Boolean

        'Get the model file name associated with the outputSettings.xml file
        UpdateOutputSettingsFileNames(p_filePathSource)

        If Not UpdateOutputSettingVersionImportTags() Then Return False

        If Not UpdateOutputSettingsFiles(p_filePathSource, p_dirPath, p_dirDestinationName, p_pathCopied) Then
            Return False
        Else
            Return True
        End If
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_filePathSource"></param>
    ''' <remarks></remarks>
    Private Sub UpdateOutputSettingsFileNames(ByVal p_filePathSource As String)
        _outputSettingsFileNameOld = GetPathFileName(p_filePathSource)
        _outputSettingsFileNameNew = _outputSettingsFileNameOld
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutputSettingVersionImportTags() As Boolean
        'Check if import tag exists, and if so, record it
        Dim versionOutputSettingsOld As String = GetVersionOutputSettingsOld()

        'Determine model name based on outputSettings file name
        Dim outputSettingsModelName As String = GetOutputSettingsModelName(versionOutputSettingsOld)
        If String.IsNullOrEmpty(outputSettingsModelName) Then Return False

        Dim outputSettingsFileNameNew = UpdateVersionImportTag(versionOutputSettingsOld, outputSettingsModelName)
        If Not String.IsNullOrEmpty(outputSettingsFileNameNew) Then
            _outputSettingsFileNameNew = outputSettingsFileNameNew
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns the outputsettings import version of the current XML file.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetVersionOutputSettingsOld() As String
        Dim newVersionTag As String = GetSuffix(_outputSettingsFileNameNew, "_")
        Dim versionOutputSettingsOld As String = FilterStringFromName(_outputSettingsFileNameNew, "_" & newVersionTag, True, False)

        If Not versionOutputSettingsOld = GetSuffix(versionOutputSettingsOld, "_") Then
            versionOutputSettingsOld = GetSuffix(versionOutputSettingsOld, "_")
        Else
            versionOutputSettingsOld = ""
        End If

        Return versionOutputSettingsOld
    End Function

    ''' <summary>
    ''' Determines the model name associated with the old outputSettings file.
    ''' </summary>
    ''' <param name="p_versionOutputSettingsOld">Import version of the current outputSettings file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutputSettingsModelName(ByVal p_versionOutputSettingsOld As String) As String
        Dim outputSettingsModelName As String = GetPrefix(_outputSettingsFileNameNew, "_" & p_versionOutputSettingsOld & "_")

        If outputSettingsModelName = _outputSettingsFileNameNew Then     'Account for "_V13_" not being present
            outputSettingsModelName = GetPrefix(_outputSettingsFileNameNew, "_")
            If outputSettingsModelName = _outputSettingsFileNameNew Then
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                            PROMPT_INVALID_FILE_NAME & Environment.NewLine & Environment.NewLine &
                                                            outputSettingsModelName & Environment.NewLine & Environment.NewLine &
                                                            PROMPT_INVALID_FILE_NAME_ACTION,
                                                            TITLE_INVALID_FILE_NAME))
                Return ""
            End If
        End If

        Return outputSettingsModelName
    End Function

    ''' <summary>
    ''' Updates version import tag part of filename, if it exists and returns the new filename.
    ''' </summary>
    ''' <param name="p_outputSettingsVersion">Import version of the current outputSettings file.</param>
    ''' <param name="p_outputSettingsModelName">Name of the model file associated with the outputSettings import version.</param>
    ''' <remarks></remarks>
    Private Function UpdateVersionImportTag(ByVal p_outputSettingsVersion As String,
                                            ByVal p_outputSettingsModelName As String) As String
        If (Not String.IsNullOrEmpty(p_outputSettingsVersion) AndAlso
            Not StringsMatch(p_outputSettingsVersion, myCsiTester.versionOutputSettings)) Then

            'TODO: Currently this is always using the default V13. For saving V14, finalize Friend definition initializing and updating.
            Return p_outputSettingsModelName & testerSettings.outputSettingsVersionSession & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
        End If

        Return ""
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_filePathSource"></param>
    ''' <param name="p_dirPath"></param>
    ''' <param name="p_dirDestinationName"></param>
    ''' <param name="p_pathCopied"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutputSettingsFiles(ByVal p_filePathSource As String,
                                               ByVal p_dirPath As String,
                                               ByVal p_dirDestinationName As String,
                                               ByRef p_pathCopied As String) As Boolean
        'Note paths of source file
        Dim dirsParentExampleAttachments As String = GetDirsParentExampleChild(p_filePathSource)
        Dim filePathUse As String = p_filePathSource

        ''If source XML file only resides at the model level, adjust path-matching name adjust the file location.
        Dim pathsUpdated As Boolean = PathsChangedModelsToAttachments(p_filePathSource, dirsParentExampleAttachments, filePathUse)
        If pathsUpdated Then
            If IO.File.Exists(filePathUse) Then
                p_pathCopied = ""
                Return True
            Else
                InitializeAttachmentFiles(filePathUse, p_filePathSource)
            End If
        End If

        Dim outputSettingsFileUpdated As Boolean = UpdateAttachmentFiles(p_dirPath, p_dirDestinationName, dirsParentExampleAttachments, filePathUse)
        If outputSettingsFileUpdated Then p_pathCopied = p_dirPath & "\" & _outputSettingsFileNameNew

        UpdateSupportingFiles(p_dirPath, filePathUse)
        Return False
    End Function

    ''' <summary>
    ''' Gets the path stub {parentDir}\{Example Name}\{attachments ... or models ... or ...}. 
    ''' This avoid issues where the {Example name} might occur in different directories.
    ''' </summary>
    ''' <param name="p_filePathSource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDirsParentExampleChild(ByVal p_filePathSource As String) As String
        Dim dirPathSource As String = GetPathDirectoryStub(p_filePathSource)
        Dim parentOfParentDirectories As String = GetPathDirectorySubStub(p_filePathSource, 3)

        dirPathSource = FilterStringFromName(dirPathSource, parentOfParentDirectories, False, True)

        Return dirPathSource
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_filePathSource"></param>
    ''' <param name="dirsParentExampleAttachments"></param>
    ''' <param name="filePathUse"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathsChangedModelsToAttachments(ByVal p_filePathSource As String,
                                                     ByRef dirsParentExampleAttachments As String,
                                                     ByRef filePathUse As String) As Boolean
        Dim filePathSourceParentDir As String = GetPathDirectoryStub(p_filePathSource)
        If SourceFileIsInModelDir(filePathSourceParentDir) Then
            ChangePathsFromModelsToAttachmentsDir(filePathSourceParentDir, dirsParentExampleAttachments, filePathUse)
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_filePathSourceParentDir"></param>
    ''' <param name="p_dirsParentExampleAttachments"></param>
    ''' <param name="p_filePathUse"></param>
    ''' <remarks></remarks>
    Private Sub ChangePathsFromModelsToAttachmentsDir(ByVal p_filePathSourceParentDir As String,
                                                     ByRef p_dirsParentExampleAttachments As String,
                                                     ByRef p_filePathUse As String)

        'Set the child directory to be for the attachments
        p_dirsParentExampleAttachments = FilterStringFromName(p_dirsParentExampleAttachments, DIR_NAME_MODELS_DEFAULT, True, False)
        p_dirsParentExampleAttachments &= DIR_NAME_ATTACHMENTS_DEFAULT

        'Set OS file path to attachments directory
        p_filePathUse = FilterStringFromName(p_filePathSourceParentDir, DIR_NAME_MODELS_DEFAULT, True, False)
        p_filePathUse &= DIR_NAME_ATTACHMENTS_DEFAULT & "\" & _outputSettingsFileNameOld
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_filePathParentDir"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SourceFileIsInModelDir(ByVal p_filePathParentDir As String) As Boolean
        If GetSuffix(p_filePathParentDir, "\") = DIR_NAME_MODELS_DEFAULT Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Copies the file to the attachments folder.
    ''' </summary>
    ''' <param name="p_filePathUse"></param>
    ''' <param name="p_filePathSource"></param>
    ''' <remarks></remarks>
    Private Sub InitializeAttachmentFiles(ByVal p_filePathUse As String,
                                        ByVal p_filePathSource As String)
        p_filePathUse = FilterStringFromName(p_filePathUse, GetSuffix(p_filePathUse, "\"), True, False) & _outputSettingsFileNameNew
        CopyFile(p_filePathSource, p_filePathUse, True)
    End Sub

    ''' <summary>
    ''' Updates the attachment files, including copying to new locatons and removing old files.
    ''' </summary>
    ''' <param name="p_dirPathDestination">Path to the destination directory being checked.</param>
    ''' <param name="p_dirNameDestination"></param>
    ''' <param name="p_dirNameSource"></param>
    ''' <param name="p_filePathSource">Path to the file source being copied.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateAttachmentFiles(ByVal p_dirPathDestination As String,
                                           ByVal p_dirNameDestination As String,
                                           ByVal p_dirNameSource As String,
                                           ByVal p_filePathSource As String) As Boolean
        Try
            If p_dirNameSource = p_dirNameDestination Then
                CopyFile(p_filePathSource, p_dirPathDestination & "\" & _outputSettingsFileNameNew, True)
                DeleteFileSupportingIfNameChanged(p_dirPathDestination)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates the supporting file, including copying to new locatons and removing old files.
    ''' </summary>
    ''' <param name="p_dirPathDestination">Path to the destination directory being checked.</param>
    ''' <param name="p_filePathSource">Path to the file source being copied.</param>
    ''' <remarks></remarks>
    Private Sub UpdateSupportingFiles(ByVal p_dirPathDestination As String,
                                      ByVal p_filePathSource As String)
        Dim oldActivePath As String = cPathOutputSettings.GetOutputSettingsActivePath(p_dirPathDestination, _outputSettingsFileNameOld)

        If IO.File.Exists(oldActivePath) Then
            CopyFileToModelDestinationIfSupporting(p_dirPathDestination, p_filePathSource)
            DeleteFileSupportingIfNameChanged(p_dirPathDestination)
        End If
    End Sub

    ''' <summary>
    ''' If the outputSettings file is being used as a supporting file, the specified file will be copied over it.
    ''' </summary>
    ''' <param name="p_dirPathDestination">Path to the destination directory being checked.</param>
    ''' <param name="p_filePathSource">Path to the file source being checked.</param>
    ''' <remarks></remarks>
    Private Sub CopyFileToModelDestinationIfSupporting(ByVal p_dirPathDestination As String,
                                                       ByVal p_filePathSource As String)
        Dim newActivePath As String = cPathOutputSettings.GetOutputSettingsActivePath(p_dirPathDestination, _outputSettingsFileNameNew)
        CopyFile(p_filePathSource, newActivePath, True)
    End Sub

    ''' <summary>
    ''' If the file name of the supporting file has been changed, the old supporting file will be deleted.
    ''' </summary>
    ''' <param name="p_dirPathDestination">Path to the destination directory being checked.</param>
    ''' <remarks></remarks>
    Private Sub DeleteFileSupportingIfNameChanged(ByVal p_dirPathDestination As String)
        If Not _outputSettingsFileNameOld = _outputSettingsFileNameNew Then DeleteFile(p_dirPathDestination & "\" & _outputSettingsFileNameOld)
    End Sub
#End Region

End Class
