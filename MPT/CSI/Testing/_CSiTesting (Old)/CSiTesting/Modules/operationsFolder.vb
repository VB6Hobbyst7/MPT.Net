Option Explicit On
Module operationsFolder
    Sub ListFilesInFolder_Click()
        ''Not Used
        path = "" 'regTest.PathMapSource

        Call ListFilesInFolder(path, True)

    End Sub


    Sub listModelFilesInFolder(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean)
        'Modified from
        ' Leith Ross
        ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
        '
        ' lists information about the files in SourceFolder
        ' example: ListFilesInFolder "C:\FolderName\", True
        '
        Dim FSO As Object
        Dim SourceFolder As Object
        Dim SubFolder As Object
        Dim FileItem As Object
        Dim r As Long

        FSO = CreateObject("Scripting.FileSystemObject")
        SourceFolder = FSO.GetFolder(SourceFolderName)

        'Update - r = Range("A65536").End(xlUp).row + 1
        'r = 6
        For Each FileItem In SourceFolder.Files
            'display file properties
            If FileItem.name = "model.xml" Then
                'Update - Cells(r, 1).value = FileItem.name
                'Update - Cells(r, 2).value = FileItem.path

                '***** Remove the single ' character in the below lines to see this information *****
                'FileItem.Size
                'FileItem.DateCreated
                'FileItem.DateLastModified

                r = r + 1 ' next row number
            End If
        Next FileItem
        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                listModelFilesInFolder(SubFolder.path, True)
            Next SubFolder
        End If

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub




    Sub ListFilesInFolder(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean)
        'Modified from
        ' Leith Ross
        ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
        '
        ' lists information about the files in SourceFolder
        ' example: ListFilesInFolder "C:\FolderName\", True
        '
        Dim FSO As Object
        Dim SourceFolder As Object
        Dim SubFolder As Object
        Dim FileItem As Object
        Dim r As Long

        FSO = CreateObject("Scripting.FileSystemObject")
        SourceFolder = FSO.GetFolder(SourceFolderName)
        'Update - r = Range("A65536").End(xlUp).row + 1
        For Each FileItem In SourceFolder.Files
            'display file properties
            'Update -   Cells(r, 1).Formula = FileItem.Name

            '***** Remove the single ' character in the below lines to see this information *****
            'FileItem.Path
            'FileItem.Size
            'FileItem.DateCreated
            'FileItem.DateLastModified

            r = r + 1 ' next row number
        Next FileItem
        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                ListFilesInFolder(SubFolder.path, True)
            Next SubFolder
        End If

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub

    Sub AllFilesNotReadOnly(SourceFolderName As String)
        'Modified from
        ' Leith Ross
        ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
        '
        ' sets all files in a folder and subfolders to not be read only.

        Dim FSO As Object
        Dim SourceFolder As Object
        Dim SubFolder As Object
        Dim FileItem As Object

        FSO = CreateObject("Scripting.FileSystemObject")
        SourceFolder = FSO.GetFolder(SourceFolderName)

        For Each FileItem In SourceFolder.Files
            SetAttr(FileItem.path, vbNormal)       'Sets each file to not be read only
        Next FileItem
        For Each SubFolder In SourceFolder.SubFolders
            ListFilesInFolder(SubFolder.path, True)
        Next SubFolder

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub




End Module
