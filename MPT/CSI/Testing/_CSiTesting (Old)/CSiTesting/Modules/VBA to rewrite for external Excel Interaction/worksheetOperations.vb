Option Explicit On
Module worksheetOperations

    '    Function addressNumWLtr(row, col)
    '        '==== Converts cell address from (letter+number) format to (row, column) format
    '        exAddress = Cells(row, col).Address

    '    End Function

    '    Function addressNumWoLtr(exAddress)
    '        '==== Converts cell address from (row, column) format to (letter+number) format
    '        Dim rowStr As String
    '        Dim i As Integer

    '        For i = 1 To Len(exAddress)
    '            rowStr = Right(exAddress, i)

    '            If IsNumeric(rowStr) = False Then
    '                row = Right(exAddress, i - 1)
    '                '~~~~~            'col is defined from below
    '                Call columnLetterToNumber(Left(exAddress, Len(exAddress) - (i - 1)))
    '                Exit Function
    '            End If

    '        Next i

    '    End Function

    '    Function columnNumberToLetter(InputNumber As Long) As String
    '        Dim vArr
    '        vArr = Split(Cells(1, InputNumber).Address(True, False), "$")
    '        columnNumberToLetter = vArr(0)
    '    End Function


    '    Function columnLetterToNumber(InputLetter As String)
    '        '==== Converts column letter to corresponding number
    '        'http://exceltipsandkeys.blogspot.com/2012/05/excel-and-excel-vba-convert-column_27.html
    '        Dim OutputNumber As Integer
    '        Dim Leng As Integer
    '        Dim i As Integer

    '        Leng = Len(InputLetter)
    '        OutputNumber = 0

    '        For i = 1 To Leng
    '            OutputNumber = (Asc(UCase(Mid(InputLetter, i, 1))) - 64) + OutputNumber * 26
    '        Next i

    '        '~~~~    'sets global variable
    '        col = OutputNumber

    '    End Function

    '    Sub worksheetSearch() 'start As String)   'start is address where the line-by-line search starts
    '        '==== Searches worksheet, starting at a designated spot, moving column-by-column, then row-by-row.
    '        '     First non-blank cell enountered triggers events.
    '        '==== Second non-blank cell encountered skips remaining columns and resumes search on first column of next row
    '        Dim start As String
    '        Dim rowStart As Integer
    '        Dim colStart As Integer
    '        Dim cell As String
    '        Dim cValue As String
    '        Dim readRow As Boolean      'Declares whether non-blank entries have been encountered on the row. This causes the search to resume on the first column of the next row once the next blank cell is encountered

    '        SName = "XML_Read"
    '        '~~~~ remove start once connected with larger command
    '        start = "A2"
    '        '~~~~

    '        Call addressNumWoLtr(start)
    '        rowStart = row
    '        colStart = col

    '        For row = rowStart To 10
    '            readRow = False
    '            For col = colStart To 10
    '                Call addressNumWLtr(row, col)
    '                cell = exAddress
    '                cValue = Worksheets(SName).Range(cell).value

    '                If cValue <> Empty Or cValue = "0" Then
    '                    readRow = True
    '                    'Write_XML (cValue)
    '                    MsgBox("Do Event")

    '                ElseIf readRow = True Then
    '                    Exit For    'Begins cell-by-cell check on next line
    '                End If

    '            Next col
    '        Next row

    '    End Sub

    '    Sub clearList()
    '        '====Clears existing contiguous list on all filled rows beneath a named cell.
    '        '====Does not skip empty cells
    '        Dim cellBlank
    '        Dim i As Long
    '        Dim SName As Worksheet
    '        Dim cellText As String

    '        SName = ActiveWorkbook.ActiveSheet
    '        cellBlank = False
    '        i = 1

    '        While Not cellBlank
    '            cellText = SName.Range(namedCell).Offset(i, 0).value
    '            If cellText = "" Then cellBlank = True

    '            SName.Range(namedCell).Offset(i, 0).value = ""

    '            i = i + 1
    '        End While

    '    End Sub

    '    Sub clearColumn(ByVal namedCell As String, Optional ByVal boundingNamedCell As String = "", Optional ByVal colBoundingOffsetNum As Long = 0, Optional clearFormat As Boolean = False, Optional ByVal SheetName As String = "", Optional ByVal rowOffset As Long = 0)
    '        '====Clears all data in a column bounded by a named cell row and the lowest row that has data.
    '        'Optional boundingNamedCell will make deletions for all columns between the first and second specified named cells.
    '        'Optional colBoundingOffsetNum parameter will make deletions between the first named cell and specified offset
    '        'If both optionals are specified, the first parameter is used
    '        '====Deletion rows only based on first column.

    '        Dim i As Long
    '        Dim j As Long
    '        Dim SName As String
    '        Dim colStart, colEnd, delRange As String
    '        Dim rowStart, rowEnd As Long

    '        If Not SheetName = "" Then
    '            SName = SheetName
    '        Else
    '            SName = Range(namedCell).Worksheet.name
    '        End If

    '        colStart = columnNumberToLetter(Range(namedCell).Column)

    '        If Not boundingNamedCell = "" Then
    '            colEnd = columnNumberToLetter(Range(boundingNamedCell).Column)
    '        ElseIf Not IsNull(colBoundingOffsetNum) Then
    '            colEnd = columnNumberToLetter(Range(namedCell).Offset(0, colBoundingOffsetNum).Column)
    '        Else
    '            colEnd = colStart
    '        End If

    '        rowStart = Worksheets(SName).Range(namedCell).row + 1 + rowOffset
    '        rowEnd = Worksheets(SName).Range(colStart & "65536").End(xlUp).row + 1

    '        delRange = colStart & rowStart & ":" & colEnd & rowEnd

    '        If clearFormat Then
    '            Worksheets(SName).Range(delRange).Clear()     'Clears cell content & formatting
    '        Else
    '            Worksheets(SName).Range(delRange).ClearContents()
    '        End If

    '    End Sub


    '    Sub getCellNameHelper(ByVal i As Long, ByRef namedCell As String)
    '        'Catches null error for unnamed cells
    '        On Error Resume Next
    '        namedCell = ActiveWorkbook.ActiveSheet.Cells(i, 3).name.name ' cause an error
    '        If Err.Number <> 0 Then
    '            namedCell = "Nothing"
    '        End If
    '    End Sub

    'Sub createNamedRangeList(ByVal headerRange As String, ByVal newRangeName As String, Optional ByVal maxRowOffset As Long)
    '        'Creates a named range of a contiguous list of filled cells
    '        Dim rowMax As Long
    '        Dim colMax As String
    '        Dim newRangeAddress As String
    '        Dim SName As String

    '        colMax = columnNumberToLetter(Range(headerRange).Column)
    '        SName = Range(headerRange).Worksheet.name

    '        'Get last row of new range. Ends at first blank cell
    '        rowMax = 0
    '        While Not Range(headerRange).Offset(rowMax, 0).value = ""
    '            rowMax = rowMax + 1
    '        End While
    '        rowMax = rowMax - 1 + Range(headerRange).row + maxRowOffset

    '        newRangeAddress = Range(headerRange).Offset(1, 0).Address & ":$" & colMax & "$" & rowMax

    '        Worksheets(SName).Range(newRangeAddress).name = newRangeName
    '    End Sub

End Module
