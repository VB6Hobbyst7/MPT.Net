Option Explicit On
Module Unused

    'Sub TabHyperlink()
    '    '
    '    ' TabHyperlink Macro
    '    ' Hyperlinks text to a corresponding Worksheet Tab
    '    '

    '    '
    '    ActiveSheet.Hyperlinks.Add(Anchor:=Selection, Address:="", SubAddress:= _
    '        "'My New Worksheet'!A1", TextToDisplay:="My New Worksheet")
    'End Sub

    'Public GSName As String

    'Sub Dev_CopySheetsW_VBA()
    '    'Takes a specified worksheet and copies the tab, and corresponding VBA code module (if present) into the current spreadsheet.

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
    '    '    TargetWorkbook = "Report11-WorksheetImport.xlsm"

    '    ''Loop here

    '    'Variables for source workbook
    '    '''' Make workbook dynamic
    '    '        SourceWorkbook = "LS-027.xlsx"
    '    SourceWorkbook = "LS-027.xlsm"
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


    'Sub Dev_CopyVBA()
    '    Dim ThisWkb As Workbook
    '    Dim OtherBook As Workbook
    '    Dim ImpModule As Boolean
    '    Dim modname As String
    '    Dim fromVB As VBIDE.VBProject
    '    Dim toVB As VBIDE.VBProject

    '    ThisWkb = ThisWorkbook

    '    Dim SourceWorkbook As String

    '    Dim Sh As Worksheet
    '    Dim wb As Workbook

    '    SourceWorkbook = "LS-027.xlsm" 'Note: Workbook must be open. Write subroutine to open workbook first

    '    '''' Make address dynamic
    '    Workbooks.Open fileName:="C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\model\attachments\" & SourceWorkbook

    '    OtherBook = Workbooks(SourceWorkbook)

    '    modname = "LS_027"
    '    fromVB = OtherBook.VBProject
    '    toVB = ThisWkb.VBProject
    '    ImpModule = CopyModule(modname, fromVB, toVB, False)

    '    ''''Close workbook VBA is copied from
    '    Workbooks(SourceWorkbook).Close()

    'End Sub


    'Function Dev_CopyModule(ModuleName As String, _
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

    'Sub Dev_ImportModule()
    '    Dim ThisWkb As Workbook
    '    Dim ImpModule As Boolean
    '    Dim modname As String
    '    '    Dim fromVB As VBIDE.VBProject
    '    Dim toVB As VBIDE.VBProject

    '    ThisWkb = ThisWorkbook

    '    modname = "LS_027"
    '    '    Set fromVB = NewBook.VBProject
    '    toVB = ThisWkb.VBProject
    '    '    ImpModule = CopyModule(modname, fromVB, toVB, False)
    '    ImpModule = ImportModule(modname, toVB)

    'End Sub

    'Function DevF_ImportModule(ModuleName As String, _
    '    ToVBProject As VBIDE.VBProject) As Boolean

    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ' CopyModule
    '    ' This function copies a module from one VBProject to
    '    ' another. It returns True if successful or  False
    '    ' if an error occurs.
    '    '
    '    ' Parameters:
    '    ' --------------------------------
    '    '
    '    ' ToVBProject           The VBProject into which the module is
    '    '                       to be copied.
    '    '
    '    ' ModuleName            The name of the module to copy.
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
    '    If Trim(ModuleName) = vbNullString Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    If ToVBProject Is Nothing Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    If ToVBProject.Protection = vbext_pp_locked Then
    '        CopyModule = False
    '        Exit Function
    '    End If

    '    On Error Resume Next

    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ' FName is the name of the temporary file to be
    '    ' used in the Export/Import code.
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    '    FName = Environ("Temp") & "\" & ModuleName & ".bas"     'Environ("Temp") is the user's temp folder

    '    ''''Make location dynamic later
    '    FName = "C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\model\attachments\" & ModuleName & ".bas"

    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''
    '    ' Do the Import operation using FName
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
    'End Function

    'Sub Dev_CopyWorkbook()

    '    Dim TargetWorkbook As String
    '    Dim SourceWorkbook As String

    '    Dim Sh As Worksheet
    '    Dim wb As Workbook

    '    TargetWorkbook = "Report11-WorksheetImport.xlsm"
    '    SourceWorkbook = "LS-027.xlsx" 'Note: Workbook must be open. Write subroutine to open workbook first

    '    '''' Make address dynamic
    '    Workbooks.Open fileName:="C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\model\attachments\" & SourceWorkbook

    '    wb = Workbooks(TargetWorkbook)
    '    For Each Sh In Workbooks(SourceWorkbook).Worksheets
    '        Sh.Copy After:=wb.Sheets(wb.Sheets.Count)
    '    Next Sh

    '    Workbooks(SourceWorkbook).Saved = True
    '    Workbooks(SourceWorkbook).Close()

    'End Sub


    'Sub Dev_CreateLinkedWorksheets()

    '    Dim i As Long
    '    Dim j As Long
    '    Dim rowStart As Long
    '    Dim row As Long
    '    Dim col As Long

    '    Dim WSName As String
    '    Dim WSFirst As String
    '    Dim WSLast As String


    '    WSFirst = "New Verification Control Layout"
    '    WSLast = WSFirst

    '    col = Worksheets(WSFirst).Range("ExNumStart").Column
    '    rowStart = Worksheets(WSFirst).Range("ExNumStart").row + 11
    '    j = 0

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
    '        Worksheets(WSFirst).Cells(row, col).Select()
    '        ActiveSheet.Hyperlinks.Add(Anchor:=Selection, Address:="", SubAddress:=Chr(39) & WSName & Chr(39) & "!A1", TextToDisplay:=WSName)

    '        'Create Worksheet Tab
    '        Worksheets.Add(After:=Worksheets(WSLast)).name = WSName

    '        'Prepares for next iteration
    '        WSLast = WSName
    '        Sheets(WSFirst).Select()

    '    Next i

    'End Sub


    'Sub Dev_CreateWorksheet()

    '    Dim WSName As String

    '    WSName = "My New Worksheet"

    '    Worksheets.Add(After:=Worksheets("New Verification Control Layout")).name = WSName

    'End Sub

    'Sub Dev_CreateWorksheets()

    '    Dim i As Long
    '    Dim WSName As String
    '    Dim WSLast As String

    '    WSLast = "New Verification Control Layout"

    '    For i = 1 To 5

    '        Select Case i
    '            Case 1 : WSName = "My New Worksheet"
    '            Case 2 : WSName = "My Newer Worksheet"
    '            Case 3 : WSName = "My Even More New Worksheet"
    '            Case 4 : WSName = "My Newest Worksheet"
    '            Case 5 : WSName = "My Newestest Worksheet"
    '        End Select

    '        Worksheets.Add(After:=Worksheets(WSLast)).name = WSName

    '        WSLast = WSName

    '    Next i

    'End Sub

    'Sub Dev_Copy_One_File()
    '    FileCopy("C:\Users\Ron\SourceFolder\Test.xls", "C:\Users\Ron\DestFolder\Test.xls")
    'End Sub


    'Sub Dev_Move_Rename_One_File()
    '    'You can change the path and file name
    'Name "C:\Users\Ron\SourceFolder\Test.xls" As "C:\Users\Ron\DestFolder\TestNew.xls"
    'End Sub

    'Sub Dev_Copy_Folder()
    '    'Copies all files and subfolders from FromPath to ToPath.
    '    'Note: If ToPath already exist it will overwrite existing files in this folder
    '    'if ToPath not exist it will be made for you.
    '    Dim FSO As Object
    '    Dim FromPath As String  'Location of folders containing original models to run
    '    Dim ToPath As String    'Location of fodlers containing active models to run

    '    FromPath = GFromPath  '<< Change
    '    ToPath = GToPath    '<< Change

    '    'If you want to create a backup of your folder every time you run this macro
    '    'you can create a unique folder with a Date/Time stamp.
    '    'ToPath = "C:\Users\Ron\" & Format(Now, "yyyy-mm-dd h-mm-ss")

    '    If Right(FromPath, 1) = "\" Then
    '        FromPath = Left(FromPath, Len(FromPath) - 1)
    '    End If

    '    If Right(ToPath, 1) = "\" Then
    '        ToPath = Left(ToPath, Len(ToPath) - 1)
    '    End If

    '    FSO = CreateObject("scripting.filesystemobject")

    '    If FSO.FolderExists(FromPath) = False Then
    '        MsgBox FromPath & " doesn't exist"
    '        Exit Sub
    '    End If

    '    FSO.CopyFolder(Source:=FromPath, Destination:=ToPath)
    '    MsgBox "You can find the files and subfolders from " & FromPath & " in " & ToPath

    'End Sub

    'Sub Dev_Read_XML()
    '    Dim xmlDoc As DOMDocument
    '    Dim xmlNode As IXMLDOMNode
    '    Dim xmlRoot As IXMLDOMElement
    '    Dim xmlelement As IXMLDOMElement
    '    Dim xmlAttributes As IXMLDOMAttribute
    '    Dim xmlChildren As IXMLDOMNodeList
    '    Dim xmlChild As IXMLDOMNode
    '    Dim xmltext As IXMLDOMText

    '    Dim nName As String
    '    Dim nValue As String
    '    Dim cell As String
    '    Dim i As Integer

    '    Dim SName As String
    '    Dim Location As String
    '    Dim fileName As String

    '    SName = "XML_Read"
    '    Location = "C:\Users\Mark\Documents\Projects\Work - CSI\Remote Work - Offline\Overall Suite Development"
    '    fileName = "model.xml"
    '    i = 1

    '    '    xmlDoc.LoadXML (Location & "\" & Filename)
    '    xmlDoc = New DOMDocument
    '    xmlDoc.Load(Location & "\" & fileName)

    '    '    For Each objXMLNode In xmlDoc.SelectNodes()
    '    '        nName = objXMLNode.Attributes.getNamedItem().Text
    '    '        Worksheets(SName).Cells(i, 1).value = nName
    '    '        i = i + 1
    '    '    Next
    '    '



    '    xmlRoot = xmlDoc.DocumentElement
    '    xmlChildren = xmlRoot.ChildNodes

    '    For Each xmlNode In xmlChildren
    '        '        Set xmlAttributes = xmlNode.Attributes
    '        '       Set xmlName = xmlAttributes.getNamedItem("name")

    '        nName = xmlNode.nodeName
    '        nValue = xmlNode.text
    '        Worksheets(SName).Cells(i, 1).value = nName
    '        'Worksheets(SName).Cells(i, 2).value = nValue
    '        i = i + 1
    '    Next

    'End Sub
#Region "Operations Folder"


    'Public Function ExcelFileCount(dir_path As String) As Long
    '    'Checks Folder and determines how many files there are of the following extensions: .xlsm, .xlsx, .xls
    '    Dim fil As IO.File

    '    With New System.Object
    '        With .GetFolder(dir_path)
    '            For Each fil In .Files
    '                If LCase(Right(fil.name, 5)) = ".xlsm" Then
    '                    XLSMFileCount = XLSMFileCount + 1
    '                    If LCase(Right(fil.name, 5)) = ".xlsx" Then
    '                        XLSXFileCount = XLSXFileCount + 1
    '                        If LCase(Right(fil.name, 4)) = ".xls" Then
    '                            XLSFileCount = XLSFileCount + 1
    '                        End If
    '        Next
    '        End With
    '    End With

    '    fil = Nothing

    'End Function
#End Region

End Module
