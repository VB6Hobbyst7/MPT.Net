Option Explicit On
Imports System.Xml
Module FillModelData

    Sub searchAllModelXMLs(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean, ByRef rowGlobal As Long, ByVal colMax As String)
        'Reads all XMLs within a given folder, and returns specified node values to Excel

        Dim FSO As Object
        Dim SourceFolder As Object
        Dim SubFolder As Object
        Dim FileItem As Object

        FSO = CreateObject("Scripting.FileSystemObject")
        SourceFolder = FSO.GetFolder(SourceFolderName)

        For Each FileItem In SourceFolder.Files
            If FileItem.name = "model.xml" Then
                readModelXML(FileItem.path, rowGlobal)
                'UPDATE                rowGlobal = Range(colMax & "65536").End(xlUp).row + 1     ' next row number. Letter should be of column with greatest number of filled rows
            End If
        Next FileItem

        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                searchAllModelXMLs(SubFolder.path, True, rowGlobal, colMax)
            Next SubFolder
        End If

    End Sub

    Sub readModelXML(ByVal PathName As String, Optional ByRef rowGlobal As Long = 0)
        Dim i As Long
        Dim j As Long
        Dim k As Long

        Dim namedCell As String
        Dim pathNode As String
        Dim multi As Boolean

        'Initialize XML
        xmlDoc = New XmlDocument
        xmlDoc.Load(PathName)

        xmlRoot = xmlDoc.DocumentElement

        'Iterate over desired cases
        For j = 1 To 5
            multi = False
            Select Case j
                'List of all unique properties
                Case 1
                    namedCell = "id_1"
                    pathNode = "//model/id"
                Case 2
                    namedCell = "id_Secondary_1"
                    pathNode = "//model/id_secondary"
                Case 3
                    namedCell = "title_1"
                    pathNode = "//model/title"
                Case 4
                    namedCell = "program_1"
                    pathNode = "//model/target_program/program/name"
                Case 5
                    namedCell = "keyword_1"
                    pathNode = "//model/keywords"
                    multi = True
                Case Else
                    namedCell = "Nothing"
                    pathNode = "Nothing"
            End Select

            'If used for multiple files, to keep track of global row # vs local row # i
            If rowGlobal = 0 Then
                i = 1
            Else
                i = rowGlobal 'UPDATE - Range(namedCell).row
            End If

            If Not namedCell = "Nothing" Then
                xmlNode = xmlRoot.SelectSingleNode(pathNode)
                If Not xmlNode Is Nothing Then
                    'Lookup node or attribute within XML file
                    If multi Then
                        For k = 0 To xmlNode.ChildNodes.Count - 1
                            'UPDATE -  Range(namedCell).Offset(i, 0) = xmlNode.ChildNodes(k).Value
                            i = i + 1
                        Next k
                    Else
                        'UPDATE - Range(namedCell).Offset(i, 0) = xmlNode.Value
                    End If
                End If
            End If
        Next j

    End Sub

End Module
