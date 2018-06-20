Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports CSiTester.cMCModel
Imports CSiTester.cPathModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

''' <summary>
''' Class representing a path to a table file for a model file.
''' </summary>
''' <remarks></remarks>
Public Class cPathDataSource
    Inherits cPathModelControlReference
    Implements IMessengerEvent
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Constants: Private"
    Private Const TITLE_DIFFERENT_DATABASE_FILE_NAME As String = "File Name Different than Default"
    Private Const PROMPT_DIFFERENT_DATABASE_FILE_NAME_START As String = "File name of the database file used for the results queries is different than the default file name assumed:" & vbNewLine & vbNewLine
    Private Const PROMPT_DIFFERENT_DATABASE_FILE_NAME_END As String = "Would you like to use the following file name as your output database file?" & vbNewLine & vbNewLine
#End Region

#Region "Fields"
    ''' <summary>
    ''' Default file extension that is assumed to be the most common or ideal one used for the table files allowed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _defaultFileExtension As String

    ''' <summary>
    ''' Used for queries where it is desired to avoid changing the current object.
    ''' </summary>
    ''' <remarks></remarks>
    Private _tempProgramControl As New cProgramControl

    ''' <summary>
    ''' If true, no user prompt will be given to correct incorrect path data.
    ''' </summary>
    ''' <remarks></remarks>
    Private _suppressUserInput As Boolean
#End Region

#Region "Properties"
    Private _allowedFileExtensions As New List(Of String)
    ''' <summary>
    ''' List of the file extensions allowed for database files that are associated with the current object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property allowedFileExtensions As List(Of String)
        Get
            Return New List(Of String)(_allowedFileExtensions)
        End Get
    End Property

    Private _isValidDataSource As Boolean = False
    ''' <summary>
    ''' True if the path points to an existing file of a valid table export from a CSi program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isValidDataSource As Boolean
        Get
            Return _isValidDataSource
        End Get
        Private Set(ByVal value As Boolean)
            If Not _isValidDataSource = value Then
                _isValidDataSource = value
                RaisePropertyChanged(NameOfProp(Function() Me.isValidDataSource))
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns 'true' if the current set file has a valid extension based on those allowed for the database tables.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property hasValidExtension As Boolean
        Get
            Return _allowedFileExtensions.Contains(fileExtension)
        End Get
    End Property


    ''' <summary>
    ''' Model file destination to sync with the exported table file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property modelFileDestination As cPathModel
        Get
            If (_mcModel IsNot Nothing AndAlso
                _mcModel.modelFile IsNot Nothing) Then
                Return _mcModel.modelFile.PathModelDestination
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' Model file source to sync with the exported table file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property modelFileSource As cPathModel
        Get
            If (_mcModel IsNot Nothing AndAlso
                _mcModel.modelFile IsNot Nothing) Then
                Return _mcModel.modelFile.PathModelSource
            Else
                Return Nothing
            End If
        End Get
    End Property

    ''' <summary>
    ''' True: The table file path used is the default assumed for a CSi program, which is the mode file name with an appropriate extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property isPathDefault As Boolean
        Get
            Return (_mcModel IsNot Nothing AndAlso
                    _mcModel.modelFile IsNot Nothing AndAlso
                    _mcModel.modelFile.pathDestination IsNot Nothing AndAlso
                    _mcModel.modelFile.pathDestination.fileName = fileName)
        End Get
    End Property
#End Region

#Region "Event Handlers"
    ''' <summary>
    ''' Updates the expected path to the table file based on changes in the model control folder structure.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RaiseMCFolderStructureChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.folderStructure) Then
            SyncPathWithModelControl()
        End If
    End Sub

    ''' <summary>
    ''' Updates the expected path and name of the table file based on changes in the expected model file name or path.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RaiseModelNameChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If (e.PropertyName = NameOfProp(Function() _mcModel.modelFile.pathDestination.fileName) OrElse
            e.PropertyName = NameOfProp(Function() _mcModel.modelFile.pathDestination.directory)) Then
            SetDefaultPath()
        End If
    End Sub
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Depending on parameters given, sets all properties, including the reference and initializes based on an existing file. 
    ''' Priority of path properties is given to the path if it is provided, otherwise it is set by the model control file.
    ''' The model object determines if the data source is valid.
    ''' Note: The model object has its Program Control object set during this check if it is not yet set and a table file exists.
    ''' </summary>
    ''' <param name="p_bindTo"></param>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByRef p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_path As String = "")
        _pathType = ePathType.FileWithExtension
        If p_bindTo IsNot Nothing Then
            MyBase.SetMCModel(p_bindTo)
            _allowedFileExtensions = AllowedExportedTableFileTypes(_mcModel.targetProgram.primary)
        End If

        If String.IsNullOrWhiteSpace(p_path) Then
            SetDefaultPath()
        Else
            SetProperties(p_path, p_bindTo, p_suppressUserInput:=True)
        End If

        If _allowedFileExtensions.Count = 0 Then
            _allowedFileExtensions = AllowedExportedTableFileTypes(eCSiProgram.None)
        End If
        If _allowedFileExtensions.Count > 0 Then
            _defaultFileExtension = _allowedFileExtensions(0)
        End If
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cPathDataSource = DirectCast(MyBase.Clone, cPathDataSource)

        With myClone
            ._isValidDataSource = _isValidDataSource
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathDataSource()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathDataSource) Then Return False

        Dim comparedObject As cPathDataSource = TryCast(p_object, cPathDataSource)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not ._isValidDataSource = _isValidDataSource Then Return False
            If Not .isPathDefault = isPathDefault Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ' Set Paths
    ''' <summary>
    ''' Sets the class properties using the provided path. The model object determines if the data source is valid.
    ''' Note: The model object has its Program Control object set during this check if it is not yet set.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_bindTo">Model object used.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetProperties(ByVal p_path As String,
                                       ByRef p_bindTo As cMCModel,
                                       Optional ByVal p_suppressUserInput As Boolean = False)
        MyBase.SetProperties(p_path, p_bindTo)
        SetDataSourceOnInitialization(p_path, p_suppressUserInput)
    End Sub
    ''' <summary>
    ''' Sets the class properties using the provided path. The model object determines if the data source is valid.
    ''' Note: The model object has its Program Control object set during this check if it is not yet set.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetProperties(ByVal p_path As String,
                                       Optional ByVal p_suppressUserInput As Boolean = False)
        MyBase.SetProperties(p_path)
        SetDataSourceOnInitialization(p_path, p_suppressUserInput)
    End Sub

    ''' <summary>
    ''' Returns a valid data source path if one can be determined. 
    ''' Otherwise, returns a blank string.
    ''' If the table name is different than the default, the user is given a choice as to whether or not to overwrite the default name, but the resulting path is still returned.
    ''' If a component is not provided, default values will attempt to be determined.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table file. 
    ''' If no extension is provided, a valid one will be assigned.</param>
    ''' <param name="p_tableNameDirectory">Directory path containing the table file. 
    ''' If none is provided, the default of the source model file location will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetValidDataSource(Optional ByVal p_tableName As String = "",
                                        Optional ByVal p_tableNameDirectory As String = "") As String
        Try
            ' Assemble expected table file name & path and save result for comparison
            Dim dataSourceCurrent As String = FormulateNewPath(p_tableName, p_tableNameDirectory)
            Return SetValidDataSourceByPath(dataSourceCurrent)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Return ""
    End Function
    ''' <summary>
    ''' Returns a valid data source path if one can be determined. 
    ''' Otherwise, returns a blank string.
    ''' If the table name is different than the default, the user is given a choice as to whether or not to overwrite the default name, but the resulting path is still returned.
    ''' </summary>
    ''' <param name="p_path">Path to set if it is or can be modified to be valid.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetValidDataSourceByPath(ByVal p_path As String) As String
        Try
            Dim dataSourceOriginal = p_path

            ' Get an existing file if the assumed path does not exist or is invalid
            p_path = GetExistingDataSourceWithValidExtension(p_path)

            ' Perform additional validation 
            If GetValidDataSourceByPath(p_path) Then
                ' If the program name is different, ask the user if this should be applied as an overwrite
                If (Not PromptFileNameDifferentFromDefault(dataSourceOriginal, p_path)) Then
                    SetDataSourceIsValid(p_path, p_pathAlreadyValidated:=True)
                End If
                Return p_path
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Return ""
    End Function

    ' Assignment
    ' ''' <summary>
    ' ''' Sets the new data table path to the associated model control file if a valid one can be determined. 
    ' ''' Returns the chosen path, or nothing if no valid path was chosen.
    ' ''' </summary>
    ' ''' <param name="p_tableName">Name to be used for the table file. This may be provided with or without the full file path.</param>
    ' ''' <param name="p_tableNameDirectory">Directory where the table file is expected.
    ' ''' If not specified, the model file source directory is used.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Friend Function SetNewDataSource(Optional ByVal p_tableName As String = "",
    '                                 Optional ByVal p_tableNameDirectory As String = "") As String
    '    Try
    '        'Assemble expected table file name & path
    '        Dim dataSourceCurrent As String = FormulateNewPath(p_tableName, p_tableNameDirectory)
    '        Dim dataSourceOriginal = dataSourceCurrent

    '        'Get an existing file if the assumed path does not exist or is invalid
    '        dataSourceCurrent = GetExistingDataSourceWithValidExtension(dataSourceCurrent)

    '        'Set recorded name of the database file in the MC file as an overwrite if it is valid
    '        If IO.File.Exists(dataSourceCurrent) Then
    '            If (modelFileSource Is Nothing OrElse
    '                Not PromptFileNameDifferentFromDefault(dataSourceOriginal, dataSourceCurrent)) Then
    '                SetProperties(dataSourceCurrent)
    '            Else
    '                ' Final validation step if not setting properties of current object
    '                Dim tableFilePath As cPathDataSource = DirectCast(Me.Clone, cPathDataSource)
    '                tableFilePath.SetProperties(dataSourceCurrent)

    '                If (String.IsNullOrEmpty(dataSourceCurrent) OrElse
    '                    Not tableFilePath.isValidPath OrElse
    '                    Not tableFilePath.isValidDataSource) Then

    '                    Return ""
    '                End If
    '            End If

    '            Return dataSourceCurrent
    '        End If
    '    Catch ex As Exception
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    End Try

    '    Return ""
    'End Function

    ' Tools
    ''' <summary>
    ''' Creates a file browsing dialog for specifying an exported table file.
    ''' </summary>
    ''' <param name="p_dataSource">Path that is changed by the file browsing dialog.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function BrowseForTableFile(ByRef p_dataSource As String) As Boolean
        Dim exportedTablesLabel As String = "Exported Tables File"

        If BrowseForFile(p_dataSource, modelFileDestination.directory, exportedTablesLabel, _allowedFileExtensions) Then
            Return True
        Else
            Return False
        End If
    End Function

    ' Queries
    ''' <summary>
    ''' Gets a list of the possible file extensions available for table sets exported from a CSi analysis program.
    ''' Extensions are NOT preceded by ".".
    ''' </summary>
    ''' <param name="p_targetProgram">Program that the exported table type is expected to come from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AllowedExportedTableFileTypes(Optional ByVal p_targetProgram As eCSiProgram = eCSiProgram.None) As List(Of String)
        Dim fileTypes As New List(Of String)
        Try
            Dim xml As String = "xml"
            Dim mdb As String = "mdb"

            ' Set target program
            If p_targetProgram = eCSiProgram.None Then
                If modelFileDestination IsNot Nothing Then p_targetProgram = modelFileDestination.program
            End If

            ' Create list based on target program
            If Not p_targetProgram = eCSiProgram.SAFE Then
                fileTypes.Add(xml)
            End If
            fileTypes.Add(mdb)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return fileTypes
    End Function

    ''' <summary>
    ''' Returns allowable file names based on the current data source object state.
    ''' If the overwritten file name has an invalid extension, this will be replaced with a valid file extension.
    ''' </summary>
    ''' <param name="p_fileNameOverwrite">If not blank, will be returned as the overwrite name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AssumedDatabaseFileNameWithExtension(ByVal p_fileNameOverwrite As String) As String
        Dim databaseFileName As String = AssumedDatabaseFileName(p_fileNameOverwrite)
        databaseFileName = FileNameWithValidFileExtenion(databaseFileName)

        Return databaseFileName
    End Function

    ' Query/Validation
    ''' <summary>
    ''' Returns status of whether or not the path is to a valid data source file based on the model control object associated with this data source object.
    ''' Program name compatibilities are checked.
    ''' In addition to checking if the path in general is valid,  this function also checks that the path is to a file, of an approved extension, and not to a directory.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCurrentDataSourceValid(ByVal p_path As String) As Boolean
        If IO.File.Exists(p_path) Then
            Return DataSourceIsOfMatchingProgram(p_path)
        Else
            Return False
        End If
    End Function

    ' Validation
    ''' <summary>
    ''' Returns a valid data source path by components if one can be determined. 
    ''' Otherwise, returns a blank string.
    ''' If a component provided is valid, the path is returned unaltered.
    ''' If a component is not provided, default values will attempt to be determined.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table file. 
    ''' If no extension is provided, a valid one will be assigned.</param>
    ''' <param name="p_tableNameDirectory">Directory path containing the table file. 
    ''' If none is provided, the default of the source model file location will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetValidDataSource(Optional ByRef p_tableName As String = "",
                                       Optional ByRef p_tableNameDirectory As String = "") As Boolean
        ' Assemble expected table file name & path and save result for comparison
        Dim dataSourceCurrent As String = FormulateNewPath(p_tableName, p_tableNameDirectory)

        ' Get an existing file if the assumed path does not exist or is invalid
        dataSourceCurrent = GetExistingDataSourceWithValidExtension(dataSourceCurrent)

        If GetValidDataSourceByPath(dataSourceCurrent) Then
            p_tableName = IO.Path.GetFileName(dataSourceCurrent)
            p_tableNameDirectory = IO.Path.GetDirectoryName(dataSourceCurrent)
            Return True
        Else
            p_tableName = ""
            p_tableNameDirectory = ""
            Return False
        End If
    End Function
    ''' <summary>
    ''' Returns a valid data source path if one can be determined. 
    ''' Otherwise, returns a blank string.
    ''' If the path provided is valid, the path is returned unaltered.
    ''' </summary>
    ''' <param name="p_path">Path to check for table validity.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetValidDataSourceByPath(ByRef p_path As String) As Boolean
        If (_mcModel Is Nothing) Then Return False

        ' Check if file exists, and is of matching program. 
        ' File extension compatibility is inferred as a valid extension is required to get the program name.
        Dim currentDataSourceIsValid As Boolean = IsCurrentDataSourceValid(p_path)
        If Not _suppressUserInput Then
            While Not currentDataSourceIsValid
                If Not GetExistingFilePathFromUser(p_path) Then Exit While

                ' If the table is not of a matching program, the user is notified and given a choice to select a new file.
                p_path = PromptNonMatchingProgram(_mcModel, p_path)
                If String.IsNullOrEmpty(p_path) Then Exit While

                currentDataSourceIsValid = IsCurrentDataSourceValid(p_path)
            End While
        End If

        Return currentDataSourceIsValid
    End Function
    ' ''' <summary>
    ' ''' Returns a valid data source path if one can be determined and updates the provided object to contain the valid path. 
    ' ''' Otherwise, returns a blank string.
    ' ''' If the path provided is valid, the path is returned unaltered.
    ' ''' </summary>
    ' ''' <param name="p_path">Path to check for table validity.</param>
    ' ''' <param name="p_dataSource">Table source object to alter for any validation enforcement.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Friend Function SetValidDataSource(ByVal p_path As String,
    '                                   ByRef p_dataSource As cPathDataSource) As String
    '    ' This updates the dataSource object provided to have a valid data source path
    '    Dim dataSourceCurrent As String = p_dataSource.SetNewDataSource(p_path)

    '    If (String.IsNullOrEmpty(dataSourceCurrent) OrElse
    '        Not p_dataSource.isValidPath OrElse
    '        Not p_dataSource.isValidDataSource) Then

    '        Return ""
    '    End If

    '    Return dataSourceCurrent
    'End Function
#End Region

#Region "Methods: Private"
    ' Paths & File Names
    ''' <summary>
    ''' Sets the default path to the model file destination directory.
    ''' If the table file name has not been specified, sets the name as the default, which is the name of the model file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDefaultPath()
        If _mcModel Is Nothing Then Exit Sub
        Dim modelPath As cPath = _mcModel.modelFile.pathSource

        Dim newPath As String = modelPath.directory

        If String.IsNullOrEmpty(fileName) Then
            If String.IsNullOrEmpty(modelPath.fileName) Then Exit Sub

            newPath &= "\" & modelPath.fileName & "." & _defaultFileExtension
        Else
            newPath &= "\" & fileNameWithExtension
        End If

        SetProperties(newPath, _mcModel, p_suppressUserInput:=True)
    End Sub

    ''' <summary>
    ''' Changes the current path properties to update based on the state of the model control object.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SyncPathWithModelControl()
        If _mcModel Is Nothing Then Exit Sub

        Dim newPath As String = _mcModel.mcFile.pathDestination.directory

        If _mcModel.folderStructure = eFolderStructure.Database Then
            newPath &= "\" & DIR_NAME_MODELS_DEFAULT
        End If
        newPath = TrimPathSlash(newPath)

        If Not String.IsNullOrEmpty(fileNameWithExtension) Then newPath &= "\" & fileNameWithExtension
        MyBase.SetProperties(newPath)
    End Sub

    ''' <summary>
    ''' Updates the path of the object to the one provided, and updates associated properties in the referenced model control object.
    ''' This method assumes validation of the path has already been performed.
    ''' </summary>
    ''' <param name="p_path">A validated path pointing to an existing and valid table file for the associated program.</param>
    ''' <remarks></remarks>
    Private Sub UpdateProgramControl(ByVal p_path As String)
        If (isValidDataSource AndAlso
            _mcModel IsNot Nothing AndAlso
            _tempProgramControl IsNot Nothing) Then

            ' Fill the program control data based on the path.
            _tempProgramControl = New cProgramControl(_mcModel, p_path)
            _mcModel.programControl = DirectCast(_tempProgramControl.Clone, cProgramControl)
        End If
    End Sub

    ''' <summary>
    ''' Generates a new path based on the provided parameters and defaults.
    ''' </summary>
    ''' <param name="p_fileName">File name, with extension, to use with the path.</param>
    ''' <param name="p_directory">Directory path to use. If not specified, the default expected directory is used, which is to the same directory as the corresponding model file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormulateNewPath(Optional ByVal p_fileName As String = "",
                                      Optional ByVal p_directory As String = "") As String
        ' Determine expected file name
        If String.IsNullOrEmpty(p_fileName) Then
            p_fileName = AssumedDatabaseFileName(p_fileName)
        End If

        ' Determine expected path
        Dim dataSourceOriginal As String = ""
        If Not String.IsNullOrEmpty(p_directory) Then
            dataSourceOriginal = p_directory
        ElseIf modelFileSource IsNot Nothing Then
            dataSourceOriginal = modelFileSource.directory
        Else
            Return ""
        End If

        Return dataSourceOriginal & "\" & p_fileName
    End Function

    ''' <summary>
    ''' Returns allowable file names based on the current data source object state.
    ''' If the overwritten name has no extension, no extension will be added.
    ''' Order of priority is: 1. Provided file name, 2. Model file name, 3. Current object file name property.
    ''' </summary>
    ''' <param name="p_fileNameOverwrite">If not blank, will be returned as the overwrite name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AssumedDatabaseFileName(Optional ByVal p_fileNameOverwrite As String = "") As String
        Dim fileName As String = ""

        ' In case a path was provided, get only the filename
        p_fileNameOverwrite = GetSuffix(p_fileNameOverwrite, "\")

        If Not String.IsNullOrEmpty(p_fileNameOverwrite) Then
            fileName = FileNameWithValidFileExtenion(p_fileNameOverwrite)
        ElseIf String.IsNullOrEmpty(fileNameWithExtension) Then
            fileName = modelFileDestination.fileName & "." & _defaultFileExtension
        ElseIf Not String.IsNullOrEmpty(fileNameWithExtension) Then
            fileName = fileNameWithExtension
        End If

        Return fileName
    End Function

    ''' <summary>
    ''' Enforces that the filename is of one of the file extension types listed. 
    ''' If not, the first extension listed is used for an assumed filename.
    ''' </summary>
    ''' <param name="p_databaseFileName">The actual or assumed name of the database file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FileNameWithValidFileExtenion(ByVal p_databaseFileName As String) As String
        Dim fileExtension As String = GetSuffix(p_databaseFileName, ".")
        Dim validExtension As Boolean = False

        For Each currentFileType As String In _allowedFileExtensions
            If fileExtension = currentFileType Then validExtension = True
        Next

        If Not validExtension Then
            If Not String.IsNullOrEmpty(fileExtension) Then p_databaseFileName = IO.Path.GetFileNameWithoutExtension(p_databaseFileName)

            p_databaseFileName &= "." & _defaultFileExtension
        End If

        Return p_databaseFileName
    End Function

    '= Existing Files
    ''' <summary>
    ''' Validates that the path to the database file points to an existing file of a valid extension type. 
    ''' If the specified path does not, other valid extensions are tried with the file name. 
    ''' If this still fails, the user is prompted to specify the file location, filtered by the valid extension types.
    ''' Function returns the first valid file path, or empty if it fails to find one.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the database file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExistingDataSourceWithValidExtension(ByVal p_dataSource As String) As String

        Dim dataSourceAssumed As String = ExistingFileWithValidExtension(p_dataSource)
        If Not String.IsNullOrEmpty(dataSourceAssumed) Then
            Return dataSourceAssumed
        Else    'Since no file is found with the specified list of possible extensions, the user is prompted to browse for the file, limited by valid extensions.
            If Not GetExistingFilePathFromUser(p_dataSource) Then
                Return ""
            Else
                Return p_dataSource
            End If
        End If
    End Function

    ''' <summary>
    ''' Checks assumed table file names. Returns the first existing file that matches the assumed name. Otherwise, returns nothing.
    ''' </summary>
    ''' <param name="p_dataSource">Original data source path used to begin assembling the assumed path.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistingFileWithValidExtension(ByVal p_dataSource As String) As String
        Dim fileExtensionTemp As String = GetSuffix(p_dataSource, ".")

        'Validate that the datasource is not empty, and refers to an existing file.
        If (Not String.IsNullOrEmpty(p_dataSource) AndAlso
            Not IO.File.Exists(p_dataSource)) Then
            'Check the same filename with the remaining file extensions to see if it exists
            For Each fileExtension As String In _allowedFileExtensions
                Dim dataSourceAssume As String = FilterStringFromName(p_dataSource, "." & fileExtensionTemp, p_retainPrefix:=True, p_retainSuffix:=False) & "." & fileExtension

                If IO.File.Exists(dataSourceAssume) Then Return dataSourceAssume

                fileExtensionTemp = fileExtension
            Next
        End If

        Return ""
    End Function


    ' User browsing to new file
    ''' <summary>
    ''' Checks if the path to the database file is empty or invalid. If it is, the user is prompted to correct this. Otherwise, returns 'False'.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the database file to use.</param>
    ''' <remarks></remarks>
    Private Function GetExistingFilePathFromUser(ByRef p_dataSource As String) As Boolean
        Dim tempPath As String

        tempPath = PromptEmptyFilePath(p_dataSource)
        If String.IsNullOrWhiteSpace(tempPath) Then Return False

        tempPath = PromptFileNotExist(tempPath)
        If String.IsNullOrWhiteSpace(tempPath) Then Return False

        p_dataSource = tempPath
        Return True
    End Function

    ''' <summary>
    ''' Returns a non-empty file path, unless the user cancels out of the resulting empty path prompt.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the database file to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PromptEmptyFilePath(ByVal p_dataSource As String) As String
        Dim titleDataSourceNone As String = "No Table File Specified"

        While String.IsNullOrEmpty(p_dataSource)
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                            String.Format("Warning! No path has been specified to an {0} table file." & Environment.NewLine & Environment.NewLine &
                                                          "Please select a table file to work with in order to continue.", ConcatenateListToMessage(_allowedFileExtensions, "or", False, "*.")),
                                            titleDataSourceNone)
                Case eMessageActions.OK
                    If Not BrowseForTableFile(p_dataSource) Then Return ""
                Case eMessageActions.Cancel : Return ""
            End Select
        End While

        Return p_dataSource
    End Function

    ''' <summary>
    ''' Returns a file path to an existing file, unless the user cancels out of the resulting non-existing file prompt.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the database file to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PromptFileNotExist(ByVal p_dataSource As String) As String
        Dim titleDataSourceNotExist As String = "File Does Not Exist"

        While Not IO.File.Exists(p_dataSource)
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                              String.Format("Warning! The default or specified path does not exist: " & Environment.NewLine & Environment.NewLine &
                                                            "{0}" & Environment.NewLine & Environment.NewLine &
                                                            "Please select a table file to work with in order to continue.", p_dataSource),
                                                titleDataSourceNotExist)
                Case eMessageActions.OK
                    If Not BrowseForTableFile(p_dataSource) Then Return ""
                Case eMessageActions.Cancel : Return ""
            End Select
        End While

        Return p_dataSource
    End Function

    ''' <summary>
    ''' If the selected table file type differs from the tables allowed for the matching CSi program, this prompt alerts the user.
    ''' Returns 'True' if the user selects a new file.
    ''' </summary>
    ''' <param name="p_dataSource">Path and name of the program name derived from the specified table.</param>
    ''' <remarks></remarks>
    Private Function PromptNonMatchingProgram(ByVal p_mcModel As cMCModel,
                                              ByVal p_dataSource As String) As String
        If p_mcModel Is Nothing Then Return ""
        Dim tableProgram As String = p_mcModel.programControl.programName
        Dim allowedProgram As String = ""

        Dim targetProgramsList As String = ""
        For Each program As eCSiProgram In p_mcModel.targetProgram
            allowedProgram = GetEnumDescription(program)
            If StringExistInName(tableProgram, allowedProgram) Then Return p_dataSource
            targetProgramsList &= "Valid Program: " & allowedProgram & Environment.NewLine
        Next

        RaiseEvent Messenger(New MessengerEventArgs("The selected or assumed table file is from an incompatible CSi program: " & Environment.NewLine & Environment.NewLine &
                        "Table Program: " & tableProgram & Environment.NewLine &
                        targetProgramsList & Environment.NewLine & Environment.NewLine &
                        "Please choose a valid table file."))

        If Not BrowseForTableFile(p_dataSource) Then
            Return ""
        Else
            Return p_dataSource
        End If
    End Function


    ' User decides whether to save new path
    ''' <summary>
    ''' If the filename is different than the default, the user is prompted as to what action, if any, should be taken.
    ''' Files of the same name but different extensions are considered to be the same.
    ''' Actions are taken if desired.
    ''' Returns 'true' if the prompt was called (i.e. the file name is different from the default).
    ''' </summary>
    ''' <param name="p_pathSourceOriginal">Path to the originally assumed database file.</param>
    ''' <param name="p_dataSource">Path to the specified database file to read into the form.</param>
    ''' <remarks></remarks>
    Private Function PromptFileNameDifferentFromDefault(ByVal p_pathSourceOriginal As String,
                                                        ByVal p_dataSource As String) As Boolean
        Dim fileNameOriginal As String = GetPathFileName(p_pathSourceOriginal, True)
        Dim fileNameNew As String = GetPathFileName(p_dataSource)
        Dim fileType As String = GetSuffix(fileNameNew, ".")

        If Not StringsMatch(fileNameNew, fileNameOriginal & "." & fileType) Then
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Question),
                                        PROMPT_DIFFERENT_DATABASE_FILE_NAME_START &
                                        "File Name Assumed: " & fileNameOriginal & "." & fileType & Environment.NewLine &
                                        "File Name Specified: " & fileNameNew & Environment.NewLine & Environment.NewLine &
                                        PROMPT_DIFFERENT_DATABASE_FILE_NAME_END & fileNameNew,
                                                TITLE_DIFFERENT_DATABASE_FILE_NAME)
                Case eMessageActions.Yes
                    SetProperties(p_dataSource)
            End Select
                Return True
        Else
                Return False
        End If
    End Function

    '= Path Validation
    ''' <summary>
    ''' Sets the path property and its isValid property, allowing for suppression of user interaction.
    ''' </summary>
    ''' <param name="p_path">Path to the database file.</param>
    ''' <param name="p_suppressUserInput">True: No prompts will be given to correct the path, and an invalid path can be assigned.</param>
    ''' <remarks></remarks>
    Private Sub SetDataSourceOnInitialization(ByVal p_path As String,
                                              ByVal p_suppressUserInput As Boolean)
        _suppressUserInput = p_suppressUserInput
        SetDataSourceIsValid(p_path)
        _suppressUserInput = False
    End Sub

    ''' <summary>
    ''' If the supplied path is valid, or it can be changed to be valid, then this updates the object path to the new path.
    ''' The user is able to alter the path during this method if the initial path is not valid.
    ''' </summary>
    ''' <param name="p_path">Path to the data source to check.</param>
    ''' <param name="p_pathAlreadyValidated">True: No validation check will be performed. Specified to avoid redundant checks.</param>
    ''' <remarks></remarks>
    Private Sub SetDataSourceIsValid(ByVal p_path As String,
                                     Optional ByVal p_pathAlreadyValidated As Boolean = False)
        If (p_pathAlreadyValidated OrElse GetValidDataSourceByPath(p_path)) Then
            isValidDataSource = True
            UpdateProgramControl(p_path)
        Else
            isValidDataSource = False
        End If

        ' Update class properties if the path has changed.
        If (Not p_path.CompareTo(path) = 0) Then MyBase.SetProperties(p_path, _mcModel)
    End Sub

    ''' <summary>
    ''' If true, then the program associated with the table file selected matches the valid programs allowed for the referenced model control file.
    ''' </summary>
    ''' <param name="p_path">Path to the data source.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DataSourceIsOfMatchingProgram(ByVal p_path As String) As Boolean
        ' Try loading the program name from the specified table file.
        _tempProgramControl = New cProgramControl(_mcModel, p_path)
        Dim tableProgram As String = _tempProgramControl.programName
        If String.IsNullOrEmpty(tableProgram) Then Return False

        ' Check whether or not the program name in the table is of a matching program.
        For Each program As eCSiProgram In _mcModel.targetProgram
            If StringExistInName(tableProgram, GetEnumDescription(program)) Then Return True
        Next

        Return False
    End Function

#End Region

#Region "Notes"
    '==== Cases ====
    'The following steps are tried, in the order listed, to generate and verify a path before opening a model table file:
    '1.	Currently specified database name is verified at the current model path
    '2.	Default table name, with any valid extension, is formulated & verified at the model path, assuming the name is the same as the model name.
    '3.	User selects a path, which is verified. If the new name is different than the default, then either:
    '   a.	New name is saved, causing case #1 to be triggered next time a path is validated.
    '   b.	New name is returned, but not saved
#End Region

End Class
