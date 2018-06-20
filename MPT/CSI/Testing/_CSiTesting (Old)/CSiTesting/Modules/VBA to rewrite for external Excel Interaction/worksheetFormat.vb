Option Explicit On
Module worksheetFormat
    'Sub FormatExampleRow()
    '    'Formats the example entry lines
    '    '''''To Do: Make locations dynamic

    '    'Highlight Cream Cells
    '    Range("I8").Select()  'Program Result
    '    With Selection.Interior
    '        .Pattern = xlSolid
    '        .Color = 10092543
    '    End With
    '    Range("L8").Select()  '% Diff From Version
    '    With Selection.Interior
    '        .Pattern = xlSolid
    '        .Color = 10092543
    '    End With

    '    'Formats % and right-alignment of Differences
    '    Range("G8,L8").Select()
    '    Selection.NumberFormat = "0.00%"
    '    With Selection
    '        .HorizontalAlignment = xlRight
    '    End With

    '    'Centers # Decimal Places
    '    Range("J8").Select()
    '    Selection.HorizontalAlignment = xlCenter

    'End Sub

    'Sub FormatExampleTitle()
    '    'Formats the Example Worksheet, apart from the example entry lines
    '    '''''To Do: Make locations dynamic

    '    'Bolds the Titles
    '    Range("A1:A4").Select()
    '    Selection.Font.Bold = True

    '    'Font Size of Titles
    '    Range("A1:A2").Select()
    '    Selection.Font.Size = 12

    '    'Underlines the Results Titles
    '    Range("G5").Select()      'Results Published in the Current Verification Examples
    '    Selection.Font.Bold = True
    '    Selection.Font.Underline = xlUnderlineStyleSingle
    '    Selection.HorizontalAlignment = xlRight

    '    Range("I5").Select()      'Results from Current Analysis
    '    Selection.Font.Bold = True
    '    Selection.Font.Underline = xlUnderlineStyleSingle

    '    Range("N7").Select()  'Notes & Calculations
    '    Selection.Font.Bold = True
    '    Selection.Font.Underline = xlUnderlineStyleSingle

    '    Range("N8").Select()  'Note
    '    Selection.Font.Bold = True

    '    Range("A7:L7").Select() 'Section Title
    '    Selection.Font.Bold = True

    '    'Format Underline Border
    '    Range("D7:L7").Select()
    '    With Selection.Borders(xlEdgeBottom)
    '        .LineStyle = xlContinuous
    '        .ColorIndex = xlAutomatic
    '        .TintAndShade = 0
    '        .Weight = xlMedium
    '    End With

    '    'Wrap & Align Long Headers
    '    Range("A7:L7").Select()  'Percent Difference
    '    Selection.HorizontalAlignment = xlCenter
    '    Selection.WrapText = True

    '    'Size Columns to Fit Data
    '    Columns("D:D").ColumnWidth = 16.71
    '    Columns("E:G").ColumnWidth = 15
    '    Columns("I:L").ColumnWidth = 15

    '    Columns("N:N").EntireColumn.AutoFit()

    '    'Adjusts Row Heights
    '    Rows("7:7").Select()
    '    Selection.RowHeight = 28.5

    '    'Removes Gridline to create all-white background
    '    ActiveWindow.DisplayGridlines = False

    'End Sub

End Module
