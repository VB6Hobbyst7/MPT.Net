Option Explicit On
Module WorksheetCreate

    'Sub CopySheetsW_VBA()
    '    'Takes a specified worksheet and copies the worksheet, and corresponding VBA code module (if present) into the current spreadsheet.
    '    ''To Do''''''
    '    ''Make dynamic association of filename, module name, address
    '    ''Set flag for when a module is not copied

    '    Dim ThisWkb As Workbook
    '    Dim OtherBook As Workbook
    '    Dim ImpModule As Boolean
    '    Dim modname As String
    '    Dim fromVB As VBIDE.VBProject
    '    Dim toVB As VBIDE.VBProject

    '    Dim TargetWorkbook As String
    '    Dim SourceWorkbook As String
    '    Dim Sh As Worksheet
    '    Dim wb As Workbook

    '    'Variables for target workbook
    '    ThisWkb = ThisWorkbook
    '    TargetWorkbook = ThisWkb.name

    '    ''Loop here

    '    'Variables for source workbook

    '    '''' Make workbook dynamic
    '    '        SourceWorkbook = "LS-027.xlsx"     'Worksheet without VBA
    '    SourceWorkbook = "LS-027.xlsm"     'Worksheet with VBA

    '    '''' Make address dynamic
    '    Workbooks.Open fileName:="C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\model\attachments\" & SourceWorkbook
    '    OtherBook = Workbooks(SourceWorkbook)

    '    'Copy Worksheet from target workbook to current workbook
    '    wb = Workbooks(TargetWorkbook)
    '    For Each Sh In Workbooks(SourceWorkbook).Worksheets
    '        Sh.Copy After:=wb.Sheets(wb.Sheets.Count)
    '    Next Sh

    '    'Copy VBA, if exists, from target workbook to current workbook
    '    ''''Make modname dynamic
    '    modname = "LS_027"
    '    fromVB = OtherBook.VBProject
    '    toVB = ThisWkb.VBProject
    '    ImpModule = CopyModule(modname, fromVB, toVB, False)

    '    ''''Close workbook VBA is copied from
    '    Workbooks(SourceWorkbook).Saved = True
    '    Workbooks(SourceWorkbook).Close()

    '    ''End Loop

    'End Sub

    'Function CopyModule(ModuleName As String, _
    '    FromVBProject As VBIDE.VBProject, _
    '    ToVBProject As VBIDE.VBProject, _
    '    OverwriteExisting As Boolean) As Boolean

    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ' CopyModule
    '    ' This function copies a module from one VBProject to
    '    ' another. It returns True if successful or  False
    '    ' if an error occurs.
    '    '
    '    ' Parameters:
    '    ' --------------------------------
    '    ' FromVBProject         The VBProject that contains the module
    '    '                       to be copied.
    '    '
    '    ' ToVBProject           The VBProject into which the module is
    '    '                       to be copied.
    '    '
    '    ' ModuleName            The name of the module to copy.
    '    '
    '    ' OverwriteExisting     If True, the VBComponent named ModuleName
    '    '                       in ToVBProject will be removed before
    '    '                       importing the module. If False and
    '    '                       a VBComponent named ModuleName exists
    '    '                       in ToVBProject, the code will return
    '    '                       False.
    '    '
    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '    Dim VBComp As VBIDE.VBComponent
    '    Dim FName As String
    '    Dim CompName As String
    '    Dim S As String
    '    Dim SlashPos As Long
    '    Dim ExtPos As Long
    '    Dim TempVBComp As VBIDE.VBComponent

    '    '''''''''''''''''''''''''''''''''''''''''''''
    '    ' Do some housekeeping validation.
    '    '''''''''''''''''''''''''''''''''''''''''''''
    '    If FromVBProject Is Nothing Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    If Trim(ModuleName) = vbNullString Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    If ToVBProject Is Nothing Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    If FromVBProject.Protection = vbext_pp_locked Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    If ToVBProject.Protection = vbext_pp_locked Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    On Error Resume Next
    '    ''''Flag for presence of module
    '    VBComp = FromVBProject.VBComponents(ModuleName)
    '    If Err.Number <> 0 Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ' FName is the name of the temporary file to be
    '    ' used in the Export/Import code.
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    FName = Environ("Temp") & "\" & ModuleName & ".bas"     'Environ("Temp") is the user's temp folder

    '    ''''Make location dynamic later
    '    '    FName = "C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\model\attachments\" & ModuleName & ".bas"
    '    If OverwriteExisting = True Then
    '        ''''''''''''''''''''''''''''''''''''''
    '        ' If OverwriteExisting is True, Kill
    '        ' the existing temp file and remove
    '        ' the existing VBComponent from the
    '        ' ToVBProject.
    '        ''''''''''''''''''''''''''''''''''''''
    '        If Dir(FName, vbNormal + vbHidden + vbSystem) <> vbNullString Then
    '            Err.Clear()
    '            Kill FName
    '            If Err.Number <> 0 Then
    '                CopyModule = False
    '                Exit Function
    '            End If
    '        End If
    '        With ToVBProject.VBComponents
    '            .Remove.item(ModuleName)
    '        End With
    '    Else
    '        '''''''''''''''''''''''''''''''''''''''''
    '        ' OverwriteExisting is False. If there is
    '        ' already a VBComponent named ModuleName,
    '        ' exit with a return code of False.
    '        ''''''''''''''''''''''''''''''''''''''''''
    '        Err.Clear()
    '        VBComp = ToVBProject.VBComponents(ModuleName)
    '        If Err.Number <> 0 Then
    '            If Err.Number = 9 Then
    '                ' module doesn't exist. ignore error.
    '            Else
    '                ' other error. get out with return value of False
    '                CopyModule = False
    '                Exit Function
    '            End If
    '        End If
    '    End If

    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ' Do the Export and Import operation using FName
    '    ' and then Kill FName.
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    FromVBProject.VBComponents(ModuleName).Export fileName:=FName

    '    '''''''''''''''''''''''''''''''''''''
    '    ' Extract the module name from the
    '    ' export file name.
    '    '''''''''''''''''''''''''''''''''''''
    '    SlashPos = InStrRev(FName, "\")
    '    ExtPos = InStrRev(FName, ".")
    '    CompName = Mid(FName, SlashPos + 1, ExtPos - SlashPos - 1)

    '    ''''''''''''''''''''''''''''''''''''''''''''''
    '    ' Document modules (SheetX and ThisWorkbook)
    '    ' cannot be removed. So, if we are working with
    '    ' a document object, delete all code in that
    '    ' component and add the lines of FName
    '    ' back in to the module.
    '    ''''''''''''''''''''''''''''''''''''''''''''''
    '    VBComp = Nothing
    '    VBComp = ToVBProject.VBComponents(CompName)

    '    If VBComp Is Nothing Then
    '        ToVBProject.VBComponents.Import fileName:=FName
    '    Else
    '        If VBComp.Type = vbext_ct_Document Then
    '            ' VBComp is destination module
    '            TempVBComp = ToVBProject.VBComponents.Import(FName)
    '            ' TempVBComp is source module
    '            With VBComp.CodeModule
    '                .DeleteLines(1, .CountOfLines)
    '                S = TempVBComp.CodeModule.Lines(1, TempVBComp.CodeModule.CountOfLines)
    '                .InsertLines(1, S)
    '            End With
    '            On Error GoTo 0
    '            ToVBProject.VBComponents.Remove TempVBComp
    '        End If
    '    End If
    '    Kill FName
    '    CopyModule = True
    'End Function


    'Sub CreateLinkedWorksheets()

    '    Dim i As Long
    '    Dim j As Long
    '    Dim rowStart As Long
    '    Dim row As Long
    '    Dim col As Long

    '    Dim WSName As String
    '    Dim WSFirst As String
    '    Dim WSLast As String


    '    WSFirst = "New Verification Control Layout"     'Verification Control Sheet
    '    WSLast = WSFirst                                'Last Worksheet that is not an example worksheet

    '    col = Worksheets(WSFirst).Range("ExNumStart").Column        'ExNumStart Is the first cell listing the Code & Example Number

    '    '''' Temporary +11 offset. Remove when automating
    '    rowStart = Worksheets(WSFirst).Range("ExNumStart").row + 11
    '    j = 0

    '    ''''Test Cases. Automate filling from assembled array instead of explicit cases & last i
    '    For i = rowStart To (rowStart + 4)

    '        j = j + 1
    '        Select Case j
    '            Case 1 : WSName = "My New Worksheet"
    '            Case 2 : WSName = "My Newer Worksheet"
    '            Case 3 : WSName = "My Even More New Worksheet"
    '            Case 4 : WSName = "My Newest Worksheet"
    '            Case 5 : WSName = "My Newestest Worksheet"
    '        End Select

    '        row = i + 1

    '        ' Creates Hyperlinked text to a corresponding Worksheet Tab
    '        Call HyperLink(WSFirst, row, col, WSName)

    '        '''''
    '        'Determine here whether sheet is referencing XML or copying in an Existing Excel Worksheet
    '        'Import Worksheet Tab
    '        '           CopySheetsW_VBA()

    '        'Create Worksheet Tab
    '        Worksheets.Add(After:=Worksheets(WSLast)).name = WSName
    '        Call CreateExample(WSName, WSFirst)

    '        'Format Worksheet Tab
    '        Call FormatExampleTitle()

    '        'Prepares for next iteration
    '        WSLast = WSName
    '        Sheets(WSFirst).Select()

    '    Next i

    'End Sub

    'Sub CreateExample(WSName As String, WSFirst As String)
    '    Dim row As Long
    '    Dim col As Long
    '    Dim i As Long

    '    Dim ExNum As String
    '    Dim ExClass As String
    '    Dim ExTitle As String
    '    Dim ProgramName As String
    '    Dim ProgramVersion As String
    '    Dim ProgramAlt As String

    '    Dim OutParameter As String
    '    Dim BMProgram As Long        'Reassign later for accounting for non-numerical values
    '    Dim BMRound As Long     ' Sig figs to round output to. Check that output is numerical to apply this. Else, "-"
    '    Dim BMIndependent As Long        'Reassign later for accounting for non-numerical values
    '    Dim BMProgramAlt As Long        'Reassign later for accounting for non-numerical values

    '    'Function variables
    '    Dim FunBMComparison As String
    '    Dim FunRound As String
    '    Dim FunResultComparison As String

    '    '''''Automate Later
    '    'Fetch Suite Data
    '    ProgramName = "SAP2000"
    '    ProgramVersion = "16.0.0"

    '    '''''Automate Later
    '    'Fetch XML Data
    '    ExNum = "Code & Example #"
    '    ExClass = "Example Class or Code Region"
    '    ExTitle = "Example Title"
    '    ProgramAlt = "ANSYS"

    '    'Sheet Headers
    '    With Worksheets(WSName)
    '        .Range("A1").Formula = "=SuiteTitle"        'Suite Title
    '        .Range("A2").value = ExNum          'Code & Example #
    '        .Range("A3").value = ExClass        'Example Class or Code Region
    '        .Range("A4").value = ExTitle        'Example Title
    '        .Range("G5").value = "Results Published in the Current Verification Examples"
    '        .Range("I5").value = "Results from Current Analysis"
    '        '    .Range("I5").Formula = 'Subsection Title, if present
    '        .Range("D7") = "Output Parameter"
    '        .Range("E7") = ProgramName
    '        .Range("F7") = "Independent"
    '        .Range("G7") = "Percent Difference"
    '        .Range("I7") = ProgramName & " Result"
    '        .Range("J7") = "Number of Decimal Places"
    '        .Range("K7") = "Result for Comparison"
    '        .Range("L7") = "% Diff From Version " & ProgramVersion
    '        .Range("N7") = "Notes & Calculations"
    '        .Range("N8") = "Note:"
    '    End With

    '    ' Generate Hyperlinked Text
    '    Call HyperLink(WSName, 1, 13, WSFirst)  'Creates hyperlinke to Verification Control
    '    '    Call HyperLink(WSName, 8, 15, "")      'If Documentation File present, create hyperlink to external documentation file
    '    '    Call HyperLink(WSName, 8, 15, "")      'If 'Attachments' folder is not empty, create a link that opens folder in Windows Explorer


    '    ' Fill Benchmark Values & %Diff Equations

    '    ''''Automate Loop for each entry from XML file
    '    row = 8

    '    For i = row To 14

    '        '''''Automate Later
    '        'Fetch XML Data
    '        OutParameter = "Typical Output"
    '        BMProgram = 542
    '        BMIndependent = 540
    '        BMProgramAlt = 439
    '        BMRound = 3

    '        '''''Automate Later, with Cell location generation
    '        'Create Functions
    '        FunBMComparison = "=IF(ISNUMBER(D6),(D6-E6)/E6,IF(E6=D6,0,1))"     'Non-Numerical Comparison
    '        FunRound = "=IF(H6="""",0,IF(I6=""-"",H6,ROUND(H6,I6)))"        'Round
    '        FunResultComparison = "=IF(H6="""",""Not Checked"",IF(I6=""-"",IF(H6=D6,0,1),IF(ABS(D6)<1E-20,(J6-D6)/1E-20,(J6-D6)/D6)))"   'Result Numerical Comparison

    '        'Fill Row with XML Data & Comparison Equations
    '        With Worksheets(WSName)
    '            .Range("D8").value = OutParameter
    '            .Range("E8").value = BMProgram
    '            .Range("F8").value = BMIndependent
    '            .Range("G8").Formula = FunBMComparison   'Calculates % Difference

    '            .Range("J8").value = BMRound
    '            .Range("K8").Formula = FunRound   'Calculates Rounded Value for Comparison
    '            .Range("L8").Formula = FunResultComparison   'Calculates % Difference
    '        End With

    '        Call FormatExampleRow()   'Format Entry

    '    Next i

    'End Sub

    'Sub HyperLink(WSSource As String, row As Long, col As Long, WSTarget As String)
    '    ' Creates Hyperlinked text to a corresponding Worksheet Tab
    '    Worksheets(WSSource).Cells(row, col).Select()
    '    ActiveSheet.Hyperlinks.Add(Anchor:=Selection, Address:="", SubAddress:=Chr(39) & WSTarget & Chr(39) & "!A1", TextToDisplay:=WSTarget)

    'End Sub

    'Private Sub NameCell()

    '    Dim rng As Range
    '    rng = Range("A1")
    '    rng.name = "MyUniqueName"

    'End Sub

End Module
