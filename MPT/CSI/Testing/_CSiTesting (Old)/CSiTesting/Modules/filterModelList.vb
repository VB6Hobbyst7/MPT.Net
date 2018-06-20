Option Explicit On
Module filterModelList
    Friend rowGlobal As Long
    Friend xmlDoc As Xml.XmlDocument
    Friend xmlRoot As Xml.XmlElement
    Friend xmlNode As Xml.XmlNode

    Friend programsList() As String
    Friend runTimeList() As String
    Friend classificationList() As String
    Friend keywordsList() As String
    Friend keywordsNotList() As String

    Friend skipCheck As Boolean
    Friend selectModel As Boolean
    Friend duplicateText As Boolean
    Friend SName As String

    Sub fillClassificationAndKeywords()
        '    listClassification     'Might be made into a static dropdown list
        listKeywords()
    End Sub

    Sub listModelXMLbyFilter()
        Dim colMax As String

        'Clears Columns
        clearModelXMLbyFilter_List()

        'Path = Worksheets("FilterModelList").Range("pathModels").value
        path = regTest.pathModels

        'Sets up region boundaries
        'Update   -       colMax = columnNumberToLetter(Range("list_filter_id").Column)    'Longest column, or if unknown, starting column
        'Update   -       rowGlobal = Range(colMax & "65536").End(xlUp).row + 1       'Letter should be of column with greatest number of filled rows.
        'Make this id for now as this one is guaranteed to have a number for every entry
        colMax = 1  'Update - 
        Call searchModelXMLbyFilter(path, True, rowGlobal, colMax)
        MsgBox("XML Data Filled")
    End Sub

    Sub clearModelXMLbyFilter_All()
        clearModelXMLbyFilter_List()
        'clearModelXMLbyFilter_Filter   'Might not be that useful. Will be tricky

    End Sub

    Sub clearModelXMLbyFilter_Filter()
        'Clears columns
        'Update  -        clearColumn("keyword_multi_any")
    End Sub

    Sub clearModelXMLbyFilter_List()
        'Clears columns
        'Update - clearColumn("list_filter_id_secondary")
        'Update - clearColumn("list_filter_title")
        'Update - clearColumn("list_filter_program")
        'Update - clearColumn("list_filter_run_time")
        'Update - clearColumn("list_filter_id")
    End Sub

    Sub listKeywords()
        Dim colMax As String

        'Clears Columns
        'Update -        clearColumn("dropdownList_keywordsHeader", , , , "Dropdown Lists", -1)

        path = regTest.pathModels

        'Sets up region boundaries
        'Update -         colMax = columnNumberToLetter(Range("dropdownList_keywordsHeader").Column)    'Longest column, or if unknown, starting column
        'Update -         rowGlobal = Worksheets(SName).Range(colMax & "65536").End(xlUp).row 'Letter should be of column with greatest number of filled rows

        colMax = 1 'Update - 
        Call searchAllModelXMLsKeywords(path, True, rowGlobal, colMax)

        MsgBox("Keywords Filled")
    End Sub

    Sub searchAllModelXMLsKeywords(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean, ByRef rowGlobal As Long, ByVal colMax As String)
        'Reads all XMLs within a given folder, and returns specified node values to Excel

        Dim FSO As Object
        Dim SourceFolder As Object
        Dim SubFolder As Object
        Dim FileItem As Object

        FSO = CreateObject("Scripting.FileSystemObject")
        SourceFolder = FSO.GetFolder(SourceFolderName)

        For Each FileItem In SourceFolder.Files
            If FileItem.name = "model.xml" Then
                readModelXMLKeywords(FileItem.path, rowGlobal)
                'Update - rowGlobal = Worksheets(SName).Range(colMax & "65536").End(xlUp).row + 1     ' next row number. Letter should be of column with greatest number of filled rows
            End If
        Next FileItem

        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                searchAllModelXMLsKeywords(SubFolder.path, True, rowGlobal, colMax)
            Next SubFolder
        End If

    End Sub

    Sub readModelXMLKeywords(ByVal PathName As String, Optional ByRef rowGlobal As Long = 0)

        Dim i As Long
        Dim k As Long

        Dim namedCell As String
        Dim nodeText As String
        Dim pathNode As String

        duplicateText = False

        'Initialize XML
        xmlDoc = New Xml.XmlDocument
        xmlDoc.Load(PathName)

        xmlRoot = xmlDoc.DocumentElement

        namedCell = "dropdownList_keywordsHeader"
        pathNode = "//model/keywords"

        'If used for multiple files, to keep track of global row # vs local row # i
        If rowGlobal = 0 Then
            i = 1
        Else
            i = rowGlobal '- Range(namedCell).row
        End If

        xmlNode = xmlRoot.SelectSingleNode(pathNode)
        If Not xmlNode Is Nothing Then
            'Lookup node or attribute within XML file
            For k = 0 To xmlNode.ChildNodes.Count - 1
                nodeText = xmlNode.ChildNodes(k).Value
                If i > 1 Then checkDuplicateStrings(nodeText, namedCell)
                If Not duplicateText Then
                    'Update                  Range(namedCell).Offset(i - 1, 0) = nodeText
                    '                Range(namedCell).Offset(i - 1, 1) = xmlRoot.SelectSingleNode("//model/id").Value 'Lists model ID next to keyword. Helpful for troubleshooting
                    i = i + 1
                End If
            Next k
        End If

    End Sub

    Sub searchModelXMLbyFilter(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean, ByRef rowGlobal As Long, ByVal colMax As String)
        'Reads all XMLs within a given folder, and returns specified node values to Excel

        Dim FSO As Object
        Dim SourceFolder As Object
        Dim SubFolder As Object
        Dim FileItem As Object

        FSO = CreateObject("Scripting.FileSystemObject")
        SourceFolder = FSO.GetFolder(SourceFolderName)

        For Each FileItem In SourceFolder.Files
            If FileItem.name = "model.xml" Then
                readModelXMLbyFilter(FileItem.path, rowGlobal)
                If selectModel Then rowGlobal = 1 'Update - Range(colMax & "65536").End(xlUp).row + 1 ' next row number. Letter should be of column with greatest number of filled rows
            End If
        Next FileItem

        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                searchModelXMLbyFilter(SubFolder.path, True, rowGlobal, colMax)
            Next SubFolder
        End If

    End Sub

    Sub readModelXMLbyFilter(ByVal PathName As String, Optional ByRef rowGlobal As Long = 0)
        Dim i As Long
        Dim j As Long

        Dim namedCell As String
        Dim pathNode As String

        'Initialize XML
        xmlDoc = New Xml.XmlDocument
        xmlDoc.Load(PathName)

        xmlRoot = xmlDoc.DocumentElement

        'Filter by Program
        getKeywords("filter_program", programsList)
        If Not skipCheck Then matchAllKeywords(False, "Program")
        If Not selectModel Then Exit Sub

        'Filter by Max Run Time
        getKeywords("filter_run_time_max", runTimeList)
        If Not skipCheck Then matchAllKeywords(True, "RunTimeMax")
        If Not selectModel Then Exit Sub

        'Filter by Classification
        getKeywords("filter_classification", classificationList)
        If Not skipCheck Then matchAllKeywords(False, "Classification")
        If Not selectModel Then Exit Sub

        'Filter by keyword
        getKeywords("filter_keyword_include", keywordsList)
        If Not skipCheck Then
            Select Case regTest.filter_keyword_select
                Case "All" : matchAllKeywords(True, "KeywordInclude")
                Case "Any" : matchAllKeywords(False, "KeywordInclude")
                Case Else
                    MsgBox("Please specify whether to select by 'Any' or 'All' keywords listed")
                    Exit Sub
            End Select
        End If
        If Not selectModel Then Exit Sub

        'Filter Out
        getKeywords("filter_keyword_exclude", keywordsNotList)
        xmlNode = xmlRoot.SelectSingleNode("//model/id")
        If Not skipCheck Then matchAllKeywords(False, "KeywordExclude")

        If selectModel Then

            'Iterate over desired cases and fill data into sheet
            For j = 1 To 5
                Select Case j
                    'List of all unique properties
                    Case 1
                        namedCell = "list_filter_id_secondary"
                        pathNode = "//model/id_secondary"
                    Case 2
                        namedCell = "list_filter_title"
                        pathNode = "//model/title"
                    Case 3
                        namedCell = "list_filter_program"
                        pathNode = "//model/target_program/program/name"
                    Case 4
                        namedCell = "list_filter_run_time"
                        pathNode = "//model/run_time/minutes"
                    Case 5
                        namedCell = "list_filter_id"
                        pathNode = "//model/id"
                    Case Else
                        namedCell = ""
                        pathNode = ""
                End Select

                'If used for multiple files, to keep track of global row # vs local row # i
                If rowGlobal = 0 Then
                    i = 1
                Else
                    i = rowGlobal ' - Range(namedCell).row
                End If

                xmlNode = xmlRoot.SelectSingleNode(pathNode)

                If Not xmlNode Is Nothing Then
                    'Lookup node or attribute within XML file
                    'Update - Range(namedCell).Offset(i, 0) = xmlNode.Value
                End If
            Next j
        End If
    End Sub

    Sub getKeywords(ByVal keywordCell As String, ByRef arrayList() As String)
        '==== Generates list of keywords from open-ended user input in a column
        '   If no keywords are gathered due to all cells being empty, this status is passed along

        Dim cellText As String
        Dim cellBlank As Boolean
        Dim i As Long

        'Initialize
        skipCheck = False
        cellBlank = False
        i = 0

        'Method
        While Not cellBlank
            cellText = "" 'Update - Range(keywordCell).Offset(i + 1, 0).value
            If cellText = "" Or cellText = "~ Click for Next Item" Then     'Checks for blank cell, or interactive cell at bottom of keyword column
                cellBlank = True
                If i = 0 Then skipCheck = True
            Else
                ReDim Preserve arrayList(i)
                arrayList(i) = cellText
                i = i + 1
            End If
        End While
    End Sub

    Function matchAllKeywords(ByVal filterAll As Boolean, ByVal filterCase As String) As Boolean
        '=============== checks if filter matches
        'Filter for ANY word from a list
        'Filter for COMBINATION of ALL words from a list
        Dim keyword As String
        Dim i As Long
        Dim j As Long
        Dim arrayList() As String
        Dim multi As Boolean
        Dim matchType As String
        Dim pathNode As String
        Dim arrayListNone(0) As String

        'Initialize
        selectModel = False 'As soon as the first keyword is matched to a keyword node, model is marked 'true' for selection
        'For 'any' keyword match, (filterAll = False) function is immediately exited if marked to 'true' to preserve this
        multi = False       'Assume a single node is checked. If True, search checks multiple nodes, which are shared child elements
        matchType = "include"   'Match types by default check equality. However other cases can be assigned:
        '       "exclude" sets filterAll = False if there is equality
        '       "max" sets filterAll = False if the keyword is less than the XML value. Currently only set up for single node comparisons
        arrayListNone(0) = ""

        Select Case filterCase
            Case "Program"
                arrayList = programsList
                pathNode = "//model/target_program/program/name"
            Case "RunTimeMax"
                arrayList = runTimeList
                If arrayList(0) = 0 Then arrayList(0) = 10000 'Checks if the user has "0" for max run time, and makes it a large value to make "0" a default for 'no limit'.
                pathNode = "//model/run_time/minutes"
                matchType = "max"
            Case "Classification"
                arrayList = classificationList
                pathNode = "//model/classification/value"
            Case "KeywordInclude"
                arrayList = keywordsList
                pathNode = "//model/keywords"
                multi = True
            Case "KeywordExclude"
                arrayList = keywordsNotList
                pathNode = "//model/keywords"
                matchType = "exclude"
                multi = True
            Case Else
                pathNode = ""
                arrayList = arrayListNone
        End Select

        xmlNode = xmlRoot.SelectSingleNode(pathNode)

        'Check if condition holds
        For i = 0 To UBound(arrayList)
            keyword = arrayList(i)
            'checks keyword against all keyword nodes in the XML
            If multi Then
                For j = 0 To xmlNode.ChildNodes.Count - 1
                    '                   TO DO:
                    '                    If matchType = "max" Then
                    '                        If Val(keyword) <= Val(xmlNode.ChildNodes(j).text) Then selectModel = True
                    '                    End If
                    '                    If keyword = xmlNode.ChildNodes(j).text Then
                    If matchType = "exclude" Then
                        If keyword = xmlNode.ChildNodes(j).Value Then
                            selectModel = False
                            Exit For
                        Else
                            selectModel = True
                        End If
                    Else
                        If keyword = xmlNode.ChildNodes(j).Value Then selectModel = True
                        If Not filterAll Then Exit For 'Preserves change in status to 'select' for 'any' keyword match
                    End If
                Next j
            Else
                If matchType = "max" Then
                    If Val(keyword) >= Val(xmlNode.Value) Then selectModel = True
                ElseIf keyword = xmlNode.Value Then
                    If matchType = "exclude" Then
                        selectModel = False
                    Else
                        selectModel = True
                    End If
                End If
            End If

            'Determines if next keyword needs to be checked
            If Not filterAll And selectModel Then Exit For
            If filterAll And Not selectModel Then Exit For 'For 'all' case, if a match was not found, the checks are ended.
            'For 'all' case, if a match has been made, the status is reset and the next keyword is checked, unless it is the last keyword to be checked
            If filterAll And Not i = UBound(arrayList) Then
                If filterAll And selectModel Then selectModel = False
            End If
        Next i
        matchAllKeywords = True
    End Function

    Function checkDuplicateStrings(ByVal text As String, ByVal namedRange As String)
        '=============== Checks text against values in a given range and determines if it is a duplicate
        Dim cellText As String
        Dim cellBlank As Boolean
        Dim i As Long

        'Initialize
        duplicateText = False
        cellBlank = False
        i = 0

        'Method
        While Not cellBlank And Not duplicateText
            cellText = "" 'Update - Range(namedRange).Offset(i, 0).value
            If cellText = "" Then
                cellBlank = True
            Else
                If text = cellText Then duplicateText = True
                i = i + 1
            End If
        End While

        checkDuplicateStrings = True
    End Function

    Sub selectAll()
        'Selects all listed models
        'Update - fillAll("yes", "list_filter_add", "list_filter_id")
    End Sub

    Sub selectNone()
        'Deselects all listed models
        'Update - fillAll("", "list_filter_add", "list_filter_id")
    End Sub

    Sub selectSelectedCells()
        'Sets selected cells to be added to the list considered,
        '       Or sets selected cells to be the list considered
        'Operation can be tailored to being done to a particular range based on the active sheet
        'Update -         Dim cell As Object
        'Update -         Dim rowOffset As Long
        Dim fillRange As String
        Dim referenceRange As String
        Dim booleanRange As String

        'Sets up case by worksheet
        Select Case "" 'Update - ActiveSheet.name
            Case "FilterModelList"
                fillRange = "list_filter_add"
                referenceRange = "list_filter_id"
                booleanRange = "FilterModelList_AddAppendSelection"
            Case Else
                MsgBox("Please go to a selection sheet or code new case in VBA.")
                Exit Sub
        End Select

        'Selects whether selection will add to existing selections or replace them
        'Update - Range(booleanRange).value = 2 Then fillAll("", fillRange, referenceRange) 'Clears selection before adding new one

        'Selects corresponding cells
        'Update -        For Each cell In Selection
        'Update -            rowOffset = cell.row - Range(fillRange).row
        'Update -             Range(fillRange).Offset(rowOffset, 0).value = "yes"
        'Update -         Next cell
    End Sub

    Sub addSelectedCellsRegTest()
        'Adds the selected models/examples to be selected by the RegTest XML control for test runs
        Dim modelsSelected() As Long
        Dim cellText As String
        Dim cellBlank As Boolean
        Dim i As Long   'Counts rows checked
        Dim j As Long   'Counts array index

        'Initialize
        cellBlank = False
        i = 1
        j = 0

        'Clear existing list of models
        'Update -        clearColumn("model_id")

        'Create array of rows to use
        While Not cellBlank
            cellText = "" 'Update - Range("list_filter_id").Offset(i, 0).value
            If cellText = "" Then ' Stops checking cells at the end of the models list
                cellBlank = True
            Else
                If 1 = 1 Then 'Update - Range("list_filter_add").Offset(i, 0).value = "yes" Then
                    ReDim Preserve modelsSelected(j)
                    modelsSelected(j) = "" 'Update - Range("list_filter_id").Offset(i, 0).value
                    j = j + 1
                End If
                i = i + 1
            End If
        End While

        'Place value in Excel Worksheet. Later can set directly in XML
        'Update -        For j = 0 To UBound(modelsSelected)
        'Update - Range("model_id").Offset(j + 1, 0).value = modelsSelected(j)
        'Update -        Next j

    End Sub

End Module
