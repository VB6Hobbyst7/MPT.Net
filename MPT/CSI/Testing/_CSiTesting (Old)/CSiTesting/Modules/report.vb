Option Explicit On
Module report

    'Public ModelNum As Long
    'Public ModelTitle As String
    'Public PercDiffMax As Double

    'Public RowRSStart As Long       'Starting row of Report Summary
    'Public RowRS As Long            'Report Summary row
    'Public RowRStart As Long        'Starting row of first Detailed Report
    'Public RowReport As Long        'Detailed Report row

    'Public RowTop As Long           'Region top row number
    'Public RowBot As Long           'Region bottom row number

    'Public ColSLNum As Integer          'Summary Region Column number, left boundary
    'Public ColSRNum As Integer          'Summary Region Column number, right boundary
    'Public ColLNum As Integer          'Detailed Report Region Column number, left boundary
    'Public ColRNum As Integer          'Detailed Report Region Column number, right boundary

    'Public ColSL As String              'Summary Region Column letter, left boundary
    'Public ColSR As String              'Summary Region Column letter, right boundary
    'Public ColL As String              'Detailed Report Region Column letter, left boundary
    'Public ColR As String              'Detailed Report Region Column letter, right boundary

    'Public ColumnLetter As String
    'Public SName As String
    'Public RName As String

    'Public RoundValue As Integer
    'Public NoMatchArray() As Object

    'Public blnCancelled As Boolean      'Cancel state for forms
    'Public DeleteFiles As Boolean       'Sets analysis files to be deleted
    'Public PrepareReport As Boolean     'Sets non-matching examples to be copied into separate folder
    'Public ZipAll As Boolean            'Zips all non-matching examples together
    'Public ZipIndividual As Boolean     'Zips all non-matching examples separately



    'Sub CompleteReport()

    '    RoundValue = 7  'Accuracy of rounding for % difference outputs

    '    Call DefineRegions()      'Sets numeric and letter values to bounding columns of the summary and detailed reports

    '    Call ClearReport()        'Clears the report

    '    '===
    '    'Write and format reports
    '    '===
    '    Call Summary() 'Creates summary report and accompanying detailed reports, which are formatted

    '    Call SummaryFormat() 'Formats summary report

    '    Call ParameterHighlight() 'Formats detailed report parameter lines that have non-matching values

    '    ' Re-aligns sheet
    '    Range("A1").Select()

    'End Sub

    'Sub Summary()
    '    ' Writes summary report

    '    Dim RowSum As Long
    '    Dim RowSumStart As Long
    '    Dim RowSumEnd As Long

    '    Dim row As Long
    '    Dim col As Long

    '    Dim item As String
    '    Dim i As Long
    '    Dim j As Long
    '    Dim k As Long

    '    'Dim NoMatchArray() As Variant

    '    SName = "Verification Summary"
    '    RName = "Report"

    '    RowRSStart = 4
    '    RowRS = RowRSStart

    '    RowRStart = 4
    '    RowReport = RowRStart

    '    '=====
    '    'Find percent difference values not equal to 0%, and place certain values in the report
    '    '=====
    '    ' Specify first & last rows in examples list
    '    RowSumStart = 8
    '    RowSumEnd = Sheets(SName).Cells.Find("*", Range("C1"), , , xlByRows, xlPrevious).row

    '    'Headers for Values
    '    j = 1
    '    For i = 2 To 4

    '        Select Case j
    '            Case 1 : item = "Model #"
    '            Case 2 : item = "Model Title"
    '            Case 3 : item = "Max % Diff"
    '        End Select

    '        Sheets(RName).Cells(RowRS, i).value = item
    '        j = j + 1
    '    Next i

    '    k = -1

    '    For row = RowSumStart To RowSumEnd


    '        PercDiffMax = Round(Sheets(SName).Cells(row, 10).value, RoundValue)

    '        If PercDiffMax <> 0 Then
    '            RowRS = RowRS + 1

    '            ModelNum = Sheets(SName).Cells(row, 3).value
    '            ModelTitle = Sheets(SName).Cells(row, 4).value

    '            Sheets(RName).Cells(RowRS, ColSLNum).value = ModelNum
    '            Sheets(RName).Cells(RowRS, ColSLNum + 1).value = ModelTitle
    '            Sheets(RName).Cells(RowRS, ColSLNum + 2).value = PercDiffMax

    '            k = k + 1
    '            ReDim Preserve NoMatchArray(k)
    '            NoMatchArray(k) = ModelNum
    '            'NoMatchArray(i) = Sheets(RName).Cells(RowRS, ColSLNum).Value
    '            '             NoMatchArray() = Sheets(RName).Cells(RowRS, ColSLNum).Value
    '            '===
    '            'Creates detailed report for each summary report line
    '            '===
    '            Call Report()

    '            'Formats detailed report
    '            Call ReportFormat()

    '        End If

    '    Next row

    '    'ReDim Preserve NoMatchArray(k, 2)

    'End Sub

    'Sub Report()

    '    Dim RowResStart As Long
    '    Dim RowResEnd As Long

    '    Dim row As Long
    '    Dim j As Long
    '    Dim i As Long

    '    Dim OutParam As String
    '    Dim CalcValue As Double
    '    Dim CalcRoundValue As Double
    '    Dim Benchvalue As Double
    '    Dim PercDiff As Double
    '    Dim Query As String

    '    Dim item As String
    '    Dim ResName As String

    '    RName = "Report"
    '    ResName = "Results"

    '    RowResEnd = Sheets(ResName).Cells.Find("*", Range("C1"), , , xlByRows, xlPrevious).row
    '    RowResStart = 8

    '    '===
    '    'Creates Example Title
    '    '===
    '    RowTop = RowReport  ' For Formatting

    '    'Model Title
    '    Sheets(RName).Cells(RowReport, ColLNum).value = "Model Title:"
    '    Sheets(RName).Cells(RowReport, ColLNum + 1).value = ModelTitle

    '    RowReport = RowReport + 1

    '    'Model #
    '    Sheets(RName).Cells(RowReport, ColLNum).value = "Model #:"
    '    Sheets(RName).Cells(RowReport, ColLNum + 1).value = ModelNum

    '    RowReport = RowReport + 1

    '    'Max % Diff
    '    Sheets(RName).Cells(RowReport, ColLNum).value = "Max % Diff:"
    '    Sheets(RName).Cells(RowReport, ColLNum + 1).value = PercDiffMax

    '    RowReport = RowReport + 1


    '    'Headers for Values
    '    j = 1
    '    For i = ColLNum To ColRNum

    '        Select Case j
    '            Case 1 : item = "Output Parameter"
    '            Case 2 : item = "Calculated Value"
    '            Case 3 : item = "Rounded Value"
    '            Case 4 : item = "Benchmark Value"
    '            Case 5 : item = "% Difference"
    '            Case 6 : item = "Table Query"
    '        End Select

    '        Sheets(RName).Cells(RowReport, i).value = item
    '        j = j + 1
    '    Next i

    '    RowReport = RowReport + 1

    '    '===
    '    'Fills Example Results
    '    '===

    '    For row = RowResStart To RowResEnd

    '        If Sheets(ResName).Cells(row, 3).value = ModelNum Then
    '            If IsEmpty(Sheets(ResName).Cells(row, 12).value) Then
    '                MsgBox("Mismatch between 'Verification Summary Sheet' and 'Results' sheet. Please re-run results check.")
    '                End
    '            Else
    '                'Gather Relevant values
    '                OutParam = Sheets(ResName).Cells(row, 7).value
    '                CalcValue = Sheets(ResName).Cells(row, 12).value
    '                CalcRoundValue = Sheets(ResName).Cells(row, 19).value
    '                Benchvalue = Sheets(ResName).Cells(row, 16).value
    '                PercDiff = Round(Sheets(ResName).Cells(row, 20).value, RoundValue)
    '                Query = Sheets(ResName).Cells(row, 33).value

    '                'Add Relevant Values to Report Sheet
    '                Sheets(RName).Cells(RowReport, ColLNum).value = OutParam
    '                Sheets(RName).Cells(RowReport, ColLNum + 1).value = CalcValue
    '                Sheets(RName).Cells(RowReport, ColLNum + 2).value = CalcRoundValue
    '                Sheets(RName).Cells(RowReport, ColLNum + 3).value = Benchvalue
    '                Sheets(RName).Cells(RowReport, ColLNum + 4).value = PercDiff
    '                Sheets(RName).Cells(RowReport, ColLNum + 5).value = Query

    '                RowReport = RowReport + 1
    '            End If
    '        End If

    '    Next row

    '    RowBot = RowReport - 1  ' For formatting

    '    RowReport = RowReport + 1

    'End Sub

    'Sub DefineRegions()

    '    ColSLNum = Range("SumColL").Column
    '    Call GetColumnLetter(ColSLNum)
    '    ColSL = ColumnLetter

    '    ColSRNum = Range("SumColR").Column
    '    Call GetColumnLetter(ColSRNum)
    '    ColSR = ColumnLetter

    '    ColLNum = Range("RepColL").Column
    '    Call GetColumnLetter(ColLNum)
    '    ColL = ColumnLetter

    '    ColRNum = Range("RepColR").Column
    '    Call GetColumnLetter(ColRNum)
    '    ColR = ColumnLetter

    'End Sub

    'Sub ClearReport()
    '    '
    '    ' ClearReport Macro
    '    ' Clears report

    '    Sheets("Report").Activate()
    '    Columns(ColSL & ":" & ColR).Select()      'Selects all rows from left side of summary region to right side of detailed report region
    '    Selection.ClearContents()
    '    Selection.HorizontalAlignment = xlGeneral
    '    Selection.Font.Bold = True
    '    Selection.Font.Bold = False

    '    With Selection.Font
    '        .name = "Arial"
    '        .Size = 10
    '        .Strikethrough = False
    '        .Superscript = False
    '        .Subscript = False
    '        .OutlineFont = False
    '        .Shadow = False
    '        .Underline = xlUnderlineStyleNone
    '        .ColorIndex = xlAutomatic
    '        .TintAndShade = 0
    '        .ThemeFont = xlThemeFontNone
    '    End With
    '    Selection.Borders(xlDiagonalDown).LineStyle = xlNone
    '    Selection.Borders(xlDiagonalUp).LineStyle = xlNone
    '    Selection.Borders(xlEdgeLeft).LineStyle = xlNone
    '    Selection.Borders(xlEdgeTop).LineStyle = xlNone
    '    Selection.Borders(xlEdgeBottom).LineStyle = xlNone
    '    Selection.Borders(xlEdgeRight).LineStyle = xlNone
    '    Selection.Borders(xlInsideVertical).LineStyle = xlNone
    '    Selection.Borders(xlInsideHorizontal).LineStyle = xlNone
    '    With Selection.Interior
    '        .Pattern = xlNone
    '        .TintAndShade = 0
    '        .PatternTintAndShade = 0
    '    End With
    'End Sub

    'Sub SummaryFormat()
    '    '
    '    ' SummaryFormat Macro
    '    ' Formats the Summary Report
    '    '
    '    Dim TitleRange As String
    '    Dim SummaryRange As String

    '    '===
    '    'Define Format Region
    '    '===

    '    RowTop = RowRSStart
    '    RowBot = RowRS

    '    TitleRange = ColSL & RowTop & ":" & ColSR & RowTop
    '    SummaryRange = ColSL & RowTop & ":" & ColSR & RowBot

    '    '===
    '    'Begin Title Format
    '    '===
    '    Range(TitleRange).Select()
    '    Selection.Font.Bold = True

    '    'Format Title Borders
    '    With Selection.Borders(xlEdgeLeft)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeTop)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeBottom)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeRight)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With

    '    'Highlights Interior of Title
    '    With Selection.Interior
    '        .Pattern = xlSolid
    '        .ThemeColor = xlThemeColorAccent3
    '        .TintAndShade = 0.799981688894314
    '    End With

    '    '===
    '    'Begin Summary Format
    '    '===

    '    Range(SummaryRange).Select()

    '    'Creates Bounding Border
    '    With Selection.Borders(xlEdgeLeft)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeTop)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeBottom)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeRight)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With

    '    'Creates Interior Vertical Borders
    '    With Selection.Borders(xlInsideVertical)
    '        .LineStyle = xlContinuous
    '        .Weight = xlThin
    '    End With

    'End Sub

    'Sub ReportFormat()
    '    '
    '    ' ReportFormat Macro
    '    ' Formats the detailed report

    '    Dim ColOffNum As Integer        'Column number, offset from left boundary

    '    Dim Col2 As String              'Region Column letter, internal column
    '    Dim Col3 As String              'Region Column letter, internal column
    '    Dim Col5 As String              'Region Column letter, internal column

    '    Dim TitleRange As String        'Range of title region
    '    Dim TitleRangeLCol As String    'Range of title left column
    '    Dim TitleRangeModNum As String  'Cell where model # is listed
    '    Dim TitleRangeMaxDiff As String     'Cell Where Max%Diff is
    '    Dim TitleRangeSub As String     'Range of region for detailed report headers
    '    Dim ReportRange As String       'Range of detailed report region, beneath the title
    '    Dim ReportReg1 As String        'Formatted region within the report
    '    Dim ReportReg2 As String        'Formatted region within the report

    '    '===
    '    'Define Format Region
    '    'Later adjust code so that program automatically knows how many row items are in the tiitle region
    '    '===

    '    ColOffNum = ColLNum + 1
    '    Call GetColumnLetter(ColOffNum)
    '    Col2 = ColumnLetter

    '    ColOffNum = ColLNum + 2
    '    Call GetColumnLetter(ColOffNum)
    '    Col3 = ColumnLetter

    '    ColOffNum = ColLNum + 4
    '    Call GetColumnLetter(ColOffNum)
    '    Col5 = ColumnLetter

    '    TitleRange = ColL & RowTop & ":" & ColR & RowTop + 2
    '    TitleRangeLCol = ColL & RowTop & ":" & ColL & RowTop + 2
    '    TitleRangeModNum = Col2 & RowTop + 1
    '    TitleRangeMaxDiff = Col2 & RowTop + 2
    '    TitleRangeSub = ColL & RowTop + 3 & ":" & ColR & RowTop + 3

    '    ReportRange = ColL & RowTop & ":" & ColR & RowBot
    '    ReportReg1 = Col2 & RowTop + 3 & ":" & Col3 & RowBot
    '    ReportReg2 = Col5 & RowTop + 3 & ":" & Col5 & RowBot

    '    '===
    '    'Begin Title Format
    '    '===
    '    'Bold left column
    '    Range(TitleRangeLCol).Select()
    '    Selection.Font.Bold = True

    '    'Max%Diff value align & format
    '    Range(TitleRangeMaxDiff).Select()
    '    Selection.NumberFormat = "0.00000%"
    '    With Selection
    '        .HorizontalAlignment = xlLeft
    '    End With

    '    'Model # value align
    '    Range(TitleRangeModNum).Select()
    '    With Selection
    '        .HorizontalAlignment = xlLeft
    '    End With


    '    'Format Title Region
    '    Range(TitleRange).Select()

    '    'Format Title Borders
    '    With Selection.Borders(xlEdgeLeft)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeTop)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeBottom)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeRight)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With

    '    'Highlights Interior of Title
    '    With Selection.Interior
    '        .Pattern = xlSolid
    '        .ThemeColor = xlThemeColorAccent3
    '        .TintAndShade = 0.799981688894314
    '    End With

    '    'Formats Font Size of Title
    '    With Selection.Font
    '        .Size = 12
    '    End With

    '    '===
    '    'Begin Report Format
    '    '===
    '    Range(ReportRange).Select()

    '    'Create bounding border
    '    With Selection.Borders(xlEdgeLeft)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeTop)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeBottom)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With
    '    With Selection.Borders(xlEdgeRight)
    '        .LineStyle = xlContinuous
    '        .Weight = xlMedium
    '    End With

    '    'Format report headers
    '    Range(TitleRangeSub).Select()
    '    Selection.Font.Bold = True
    '    With Selection.Borders(xlEdgeBottom)
    '        .LineStyle = xlDouble
    '        .Weight = xlThick
    '    End With

    '    'Format report sub-regions
    '    Range(ReportReg1).Select()    'Program values region
    '    With Selection.Borders(xlEdgeLeft)
    '        .LineStyle = xlContinuous
    '        .Weight = xlThin
    '    End With
    '    With Selection.Borders(xlEdgeRight)
    '        .LineStyle = xlContinuous
    '        .Weight = xlThin
    '    End With


    '    Range(ReportReg2).Select()    '% Difference region
    '    With Selection.Borders(xlEdgeLeft)
    '        .LineStyle = xlDouble
    '        .Weight = xlThick
    '    End With
    '    With Selection.Borders(xlEdgeRight)
    '        .LineStyle = xlContinuous
    '        .Weight = xlThin
    '    End With

    'End Sub

    'Sub ParameterHighlight()
    '    'Formats detailed report parameter lines that have non-matching values

    '    Dim row As Long
    '    Dim RowREnd As Long

    '    RowREnd = RowReport - 2

    '    For row = RowRStart To RowREnd

    '        '        If Round(Sheets(RName).Cells(Row, Range("RepColL").Column + 4).Value, 7) <> 0 Then
    '        '        If Sheets(RName).Cells(Row, Range("RepColL").Column + 4).Value <> 0 Then

    '        If IsNumeric(Sheets(RName).Cells(row, Range("RepColL").Column + 4).value) Then
    '            If Round(Sheets(RName).Cells(row, Range("RepColL").Column + 4).value, RoundValue) <> 0 Then
    '                Range(ColL & row & ":" & ColR & row).Select()
    '                With Selection.Interior
    '                    .Pattern = xlSolid
    '                    .Color = 65535
    '                End With
    '            End If
    '        End If

    '    Next row

    'End Sub

    'Sub GetColumnLetter(ColumnNumber As Integer)
    '    'Returns the Excel column letter from a provided column number

    '    Dim n As Integer
    '    Dim c As Byte
    '    Dim S As String

    '    n = ColumnNumber
    '    Do
    '        c = ((n - 1) Mod 26)
    '        S = Chr(c + 65) & S
    '        n = (n - c) \ 26
    '    Loop While n > 0
    '    ColumnLetter = S
    'End Sub

    'Sub ReportDialog()
    '    frmVerificationReport.ShowDialog()
    '    frmVerificationReport.Dispose()

    '    If blnCancelled = True Then
    '        MsgBox("Operation Cancelled!", vbExclamation)
    '    End If
    'End Sub

End Module
