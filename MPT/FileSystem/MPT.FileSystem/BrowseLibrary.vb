Option Explicit On
Option Strict On

Imports System.IO

Imports WinForms = System.Windows.Forms
Imports Win32Forms = Microsoft.Win32 

Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

Imports MPT.FileSystem.PathLibrary

''' <summary>
''' Contains functions for browsing for file or folder paths.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class BrowseLibrary
    Shared Event Log(exception As LoggerEventArgs)
    Shared Event Messenger(message As MessengerEventArgs)

     ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Methods: Public"
    ''' <summary>
    ''' Creates an object to browse for a file or files.
    ''' </summary>
    ''' <param name="pathDefaultStart">Starting directory. 
    ''' Do not include filename.</param>
    ''' <param name="fileTypeLabelDescriptions">Single label that describes the file type(s) provided.</param>
    ''' <param name="fileTypes">List of file types to find. 
    ''' Multiple file types will apply to the same single label.</param>
    ''' <param name="multiSelect">True: User can select multiple files, and multiple file paths will be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function BrowseForFilesFactory(Optional ByVal pathDefaultStart As String = "",
                                                Optional ByVal fileTypeLabelDescriptions As String = "",
                                                Optional ByVal fileTypes As List(Of String) = Nothing,
                                                Optional ByVal multiSelect As Boolean = False) As Win32Forms.OpenFileDialog
        pathDefaultStart = CleanPath(pathDefaultStart)

        'Create filter property
        Dim fileFilter As String = ""
        If (fileTypes Is Nothing OrElse 
            fileTypes.Count = 0) Then                                           
            'Show all files
            If String.IsNullOrWhiteSpace(fileTypeLabelDescriptions) Then
                fileFilter = "All Files"
            Else
                fileFilter = fileTypeLabelDescriptions
            End If
            fileFilter &= "|*.*"
        Else                                                                
            'Filter files by extension 
            Dim secondaryType As Boolean = False
            If String.IsNullOrWhiteSpace(fileTypeLabelDescriptions) Then fileTypeLabelDescriptions = "Files"
            For Each fileType As String In fileTypes
                If secondaryType Then fileFilter &= "|"
                secondaryType = True
                fileFilter &= fileTypeLabelDescriptions & $" (*.{fileType})|*.{fileType}"
            Next
        End If
        
        Dim dialog As New Win32Forms.OpenFileDialog()
        If (fileTypes IsNot Nothing AndAlso 
            fileTypes.Count > 0) Then dialog.DefaultExt = fileTypes(0) ' Default file extension 
        dialog.FilterIndex = 1
        dialog.Filter = fileFilter                       ' Filter files by extension 
        dialog.InitialDirectory = pathDefaultStart
        dialog.Multiselect = multiSelect
        dialog.Title = "Browse for File"
        If multiSelect Then dialog.Title &= "s"

        'Validate User Manual Entries
        dialog.CheckFileExists = True
        dialog.CheckPathExists = True

        Return dialog
    End Function
    
    ''' <summary>
    ''' Creates an object to browse for folders.
    ''' </summary>
    ''' <param name="description">Description to be provided in the folder browser.</param>
    ''' <param name="pathDefaultStart">Default starting directory.</param>
    ''' <param name="canCreateNewFolders">True: A button appears for creating new folders.</param>
    ''' <remarks></remarks>
    Public Shared Function BrowseForFolderFactory(Optional ByVal description As String = "",
                                                  Optional ByVal pathDefaultStart As String = "",
                                                  Optional ByVal canCreateNewFolders As Boolean = False) As WinForms.FolderBrowserDialog
        pathDefaultStart = CleanPath(pathDefaultStart)

        Dim dialog As New WinForms.FolderBrowserDialog()
        dialog.Description = description
        dialog.SelectedPath = pathDefaultStart
        dialog.ShowNewFolderButton = canCreateNewFolders
        
        Return dialog
    End Function

    ''' <summary>
    ''' Opens Windows Explorer at the specified directory.
    ''' </summary>
    ''' <param name="folderPath">Path to the folder to be opened.</param>
    ''' <param name="errorMessage">Error message if the folder does not exist at the specified location.</param>
    ''' <remarks></remarks>
    Public Shared Sub OpenExplorerAtFolder(ByVal folderPath As String,
                                           Optional ByVal errorMessage As String = "Folder Does Not Exist")
        If Not Directory.Exists(folderPath) Then 
            RaiseEvent Messenger(New MessengerEventArgs(errorMessage))
            Exit Sub
        End If
        ''ncrunch: no coverage start
        Try
            Process.Start("explorer.exe", folderPath)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() folderPath), folderPath))
        End Try
        ''ncrunch: no coverage end
    End Sub

    ''' <summary>
    ''' Opens Windows Explorer at the specified directory and selects the specified file.
    ''' </summary>
    ''' <param name="filePath">Path to the file to be selected.</param>
    ''' <param name="errorMessageFolder">Error message if the folder does not exist at the specified location.</param>
    ''' <param name="errorMessageFile">Error message if the file does not exist at the specified location.</param>
    ''' <remarks></remarks>
    Public Shared Sub OpenExplorerAtFile(ByVal filePath As String,
                                         Optional ByVal errorMessageFolder As String = "Folder Does Not Exist",
                                         Optional ByVal errorMessageFile As String = "File Does Not Exist")
        'TODO: Test if path needs adjustment for the first line check
        If Not Directory.Exists(PathDirectoryStub(filePath)) Then 
            RaiseEvent Messenger(New MessengerEventArgs(errorMessageFolder))
            Exit Sub
        End If
        If Not File.Exists(filePath) Then 
            RaiseEvent Messenger(New MessengerEventArgs(errorMessageFile))
            Exit Sub
        End If
        ''ncrunch: no coverage start
        Try
            Dim argument As String = $"/select, ""{filePath}"""
            Process.Start("explorer.exe", argument)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() filePath), filePath))
        End Try
        ''ncrunch: no coverage end
    End Sub

    ''ncrunch: no coverage start
    ''' <summary>
    ''' Browses for a single file and updates the file path. Returns 'False' if canceled.
    ''' </summary>
    ''' <param name="path">Variable to write the file path to.</param>
    ''' <param name="pathDefaultstart">Starting directory. Do not include filename.</param>
    ''' <param name="fileTypeLabelDescriptions">Single label that describes the file type(s) provided.</param>
    ''' <param name="fileTypes">List of file types to find. Multiple file types will apply to the same single label.</param>
    ''' <returns>True if filename is chosen and valid. False otherwise.</returns>
    ''' <remarks>dirPath currently is not working with this dialog.</remarks>
    Public Shared Function BrowseForFile(ByRef path As String,
                                         Optional ByVal pathDefaultstart As String = "",
                                         Optional ByVal fileTypeLabelDescriptions As String = "",
                                         Optional ByVal fileTypes As List(Of String) = Nothing) As Boolean
        Dim dialog As Win32Forms.OpenFileDialog = BrowseForFilesFactory(pathDefaultstart, 
                                                                     fileTypeLabelDescriptions, 
                                                                     fileTypes)
        Dim result? As Boolean = dialog.ShowDialog()
        If result Then
            path = dialog.FileName
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Browses for multiple files and updates the file paths list provided. Returns 'False' if canceled.
    ''' </summary>
    ''' <param name="paths">List to populate with one or more selected file paths.</param>
    ''' <param name="pathDefaultstart">Starting directory. Do not include filename.</param>
    ''' <param name="fileTypeLabelDescriptions">Single label that describes the file type(s) provided.</param>
    ''' <param name="fileTypes">List of file types to find. Multiple file types will apply to the same single label.</param>
    ''' <returns>True if filename is chosen and valid. False otherwise.</returns>
    ''' <remarks>dirPath currently is not working with this dialog.</remarks>
    Public Shared Function BrowseForFiles(ByRef paths As List(Of String),
                                          Optional ByVal pathDefaultstart As String = "",
                                          Optional ByVal fileTypeLabelDescriptions As String = "",
                                          Optional ByVal fileTypes As List(Of String) = Nothing) As Boolean
        Dim dialog As Win32Forms.OpenFileDialog = BrowseForFilesFactory(pathDefaultstart, 
                                                                     fileTypeLabelDescriptions, 
                                                                     fileTypes, 
                                                                     multiSelect:= True)
        Dim result? As Boolean = dialog.ShowDialog()
        If result Then
            paths.AddRange(dialog.FileNames)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' User selects a folder location. 
    ''' Folder location is returned as a string.
    ''' </summary>
    ''' <param name="description">Description to be provided in the file browser.</param>
    ''' <param name="pathDefaultStart">Default starting directory.</param>
    ''' <param name="canCreateNewFolders">True: A button appears for creating new folders.</param>
    ''' <remarks></remarks>
    Public Shared Function BrowseForFolder(Optional ByVal description As String = "",
                                           Optional ByVal pathDefaultStart As String = "",
                                           Optional ByVal canCreateNewFolders As Boolean = False) As String
        Dim dialog As WinForms.FolderBrowserDialog = BrowseForFolderFactory(description, 
                                                                               pathDefaultStart, 
                                                                               canCreateNewFolders)
        
        ' Open the file dialog
        If (dialog.ShowDialog = WinForms.DialogResult.OK) Then    'Path selected
            Return dialog.SelectedPath
        Else                                'Path not selected
            Return ""
        End If
    End Function
    ''ncrunch: no coverage end
#End Region

End Class
