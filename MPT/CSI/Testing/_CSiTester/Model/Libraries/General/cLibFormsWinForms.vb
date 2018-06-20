Option Strict On
Option Explicit On

Imports System.Windows.Forms    'NOTE: Use this sparingly in this project. There are namespace ambiguities with other System.Windows namespaces needed for a WPF project

Imports CSiTester.cLibPath

''' <summary>
''' Contains procedures using libraries specific to WinForms. Use these sparingly in this project. There are namespace ambiguities with other System.Windows namespaces needed for a WPF project 
''' </summary>
''' <remarks>Currently none of the procedures are used in the project</remarks>
Public NotInheritable Class cLibFormsWinForms

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"
    ''' <summary>
    ''' User selects an arbitrary set of files and file names and absolute paths are stored in a provided list object.
    ''' </summary>
    ''' <param name="ftype">File type to find</param>
    ''' <param name="dirPath">Optional starting directory</param>
    ''' <param name="pathsList">List to be populated with the file path names.</param>
    ''' <remarks></remarks>
    Public Shared Function BrowseForFilesWinForm(ByRef pathsList As List(Of String), ByVal ftype As String, Optional ByVal dirPath As String = "") As Boolean
        Dim diaFolder As New System.Windows.Forms.OpenFileDialog
        'Dim FName As Object
        Dim FName As String()
        'Dim j As Long

        BrowseForFilesWinForm = False
        If String.IsNullOrEmpty(ftype) Then
            diaFolder.Filter = "All Files (*.*)| *.*"
        Else
            ftype = GetSuffix(ftype, ".")
            diaFolder.Filter = "Files (*." & ftype & ")| *." & ftype
        End If

        diaFolder.FilterIndex = 1
        diaFolder.InitialDirectory = dirPath
        diaFolder.Multiselect = True
        diaFolder.Title = "Browse for File"

        ' Open the file dialog
        If diaFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            FName = diaFolder.FileNames
        Else
            'FName = String.Empty
            ReDim FName(0)
            FName(0) = ""
        End If

        diaFolder = Nothing

        Try
            If Not String.IsNullOrEmpty(FName(0)) Then
                For Each myPath As String In FName
                    pathsList.Add(myPath)
                Next
                'pathsList = CType(FName, List(Of String))
                BrowseForFilesWinForm = True
                'j = UBound(CType(FName, Array))   'Determines the number of files selected
            End If

        Catch ex As Exception
            'CLogger
            Exit Function
        End Try

    End Function

    ''' <summary>
    ''' User selects a file based on a provided file type to get its path
    ''' </summary>
    ''' <param name="ftype">File type to apply to the filter</param>
    ''' <param name="dirPath">Default starting directory path</param>
    ''' <remarks>dirPath currently is not working with this dialog</remarks>
    Public Shared Function BrowseForFileWinForm(ByRef myPath As String, ByVal ftype As String, Optional ByVal dirPath As String = "") As Boolean
        Dim diaFolder As New System.Windows.Forms.OpenFileDialog

        diaFolder.Filter = "Files (*." & ftype & ")| *." & ftype
        diaFolder.FilterIndex = 1
        diaFolder.InitialDirectory = dirPath
        diaFolder.Multiselect = False
        diaFolder.Title = "Browse for File"

        'Validate User Manual Entries
        diaFolder.CheckFileExists = True
        diaFolder.CheckPathExists = True

        ' Open the file dialog
        If diaFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            myPath = diaFolder.FileName
            BrowseForFileWinForm = True
        Else
            myPath = ""
            BrowseForFileWinForm = False

            diaFolder = Nothing
        End If

    End Function

    ''' <summary>
    ''' User selects a folder location. Folder location is stored as a string
    ''' </summary>
    ''' <param name="description">Description to be provided in the file browser</param>
    ''' <param name="startupDir">Default starting directory</param>
    ''' <remarks></remarks>
    Public Shared Sub BrowseForFolderWinForm(Optional ByVal description As String = "", Optional ByVal startupDir As String = "")

        Dim diaFolder As New System.Windows.Forms.FolderBrowserDialog

        diaFolder.Description = description
        diaFolder.SelectedPath = startupDir
        diaFolder.ShowNewFolderButton = False

        ' Open the file dialog
        If diaFolder.ShowDialog = 1 Then    'Path selected
            path = diaFolder.SelectedPath
        Else                                'Path not selected
            path = String.Empty
        End If

        diaFolder = Nothing

    End Sub
#End Region
End Class
