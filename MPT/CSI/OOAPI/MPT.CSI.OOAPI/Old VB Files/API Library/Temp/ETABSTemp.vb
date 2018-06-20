Option Explicit On
Option Strict On

Module ETABSTemp
    Public Const WKSHT_NAME As String = "Output"

    Public Const RANGE_NAME_NAME_PROGRAM As String = "nameProgram"
    Public Const RANGE_NAME_PATH_PROGRAM As String = "pathProgram"
    Public Const RANGE_NAME_FILENAME_MODEL As String = "fileNameModel"
    Public Const RANGE_NAME_EXTENSION_MODEL As String = "extensionModel"
    Public Const RANGE_NAME_DIR_MODEL As String = "dirModel"
    Public Const RANGE_NAME_REF_NAME As String = "refName"
    Public Const RANGE_NAME_FILENAME_TABLE As String = "fileNameTable"
    Public Const RANGE_NAME_EXTENSION_TABLE As String = "extensionTable"

    Public Const RANGE_NAME_PARAMETER As String = "nameParameter"
    Public Const RANGE_NAME_INPUT As String = "valueVBA"
    Public Const RANGE_NAME_CODE As String = "codeParameter"
    Public Const RANGE_NAME_VALUE_SET As String = "valueSetParameter"
    Public Const RANGE_NAME_VALUE_GET As String = "valueGetParameter"
    Public Const RANGE_NAME_VALUE_TABLE As String = "valueTable"
    Public Const RANGE_NAME_RET_SET As String = "retSetParameter"
    Public Const RANGE_NAME_RET_GET As String = "retGetParameter"

    Public Const REGISTER_FILE As String = "RegisterETABS.exe"
    Public Const UNREGISTER_FILE As String = "UnRegisterETABS.exe"

    Public programName As String
    Public programPath As String
    Public pathModel As String
    Public wksht As Worksheet

    Dim modelName As String
    Dim modelDirectory As String

    'ETABS Only
    '1. Run cmd as admin
    '2. Unreg & hit tab
    '3. Reg & hit tab
    '4. *.tlb file will be generated from this
    '5. Select Tools>Reference & browse to the following file to set as a reference:
    '   ETABS.tlb
    ' This early binds to the API using the COM interface.

    ' Global ETABS object
    Dim SapModel As ETABS2015.cSapModel
    Dim ETABSObject As ETABS2015.cOAPI

    ' Use ret to check return values of OAPI calls
    Dim ret As Long

    Public Sub RunCheck()
        wksht = ThisWorkbook.Worksheets(WKSHT_NAME)

        Call ClearOutput()

        Call OpenProgram()
        Call SetModelDirectoryAndName()

        Dim modelOpen As Boolean
        modelOpen = False
        If OpenModel Then
            modelOpen = True
        ElseIf CreateModel Then
            modelOpen = True
        End If
        If modelOpen Then
            Call RunFunctionsTests()
            '        Call CloseProgram
        End If
    End Sub

    Private Sub RunFunctionsTests()
        Dim parameters() As String
        Dim caseRows() As Integer
        Dim parametersInputs() As String
        Dim parametersInputsStartRows() As Integer

        Call GetSheetParameters(parameters, caseRows, parametersInputs, parametersInputsStartRows)

        Dim rangeNameInputRow As Integer
        rangeNameInputRow = wksht.Range(RANGE_NAME_INPUT).row

        Call GetConcreteBeamDesignSummaryResults(parameters, caseRows, parametersInputs, parametersInputsStartRows, rangeNameInputRow)

        'UnLock model
        ret = SapModel.SetModelIsLocked(False)

        Call CloseProgram()

        If RunForTable Then
            '        If OpenTable Then
            '            Call GetConcreteBeamDesignSummaryResultsTable(parameters, caseRows, rangeNameInputRow)
            '            CloseDatabase
            '        End If
        End If
    End Sub

    Public Sub ReadTables()
        Dim parameters() As String
        Dim caseRows() As Integer
        Dim parametersInputs() As String
        Dim parametersInputsStartRows() As Integer

        wksht = ThisWorkbook.Worksheets(WKSHT_NAME)

        Call GetSheetParameters(parameters, caseRows, parametersInputs, parametersInputsStartRows)

        Dim rangeNameInputRow As Integer
        rangeNameInputRow = wksht.Range(RANGE_NAME_INPUT).row

        If OpenTable Then
            Call GetConcreteBeamDesignSummaryResultsTable(parameters, caseRows, rangeNameInputRow)
            CloseDatabase()
        End If
    End Sub

    Private Sub OpenProgram()
        'set the following flag to true to attach to an existing instance of the program
        'otherwise a new instance of the program will be started
        Dim AttachToInstance As Boolean
        AttachToInstance = False

        'full path to the program executable
        'set it to the installation folder
        programName = wksht.Range(RANGE_NAME_NAME_PROGRAM).value
        programPath = wksht.Range(RANGE_NAME_PATH_PROGRAM).value & Path.PathSeparator & programName & ".EXE"

        If AttachToInstance Then
            'attach to a running instance of ETABS
            'get the active Sap2000 object
            ETABSObject = GetObject(, "CSI.ERTABS.API.ETABSObject")
        Else
            ' Latest
            Dim myHelper As ETABS2015.cHelper
            myHelper = New ETABS2015.Helper

            ' Latest
            'Note, the path below may need to be modified if you have ETABS installed in a different location
            ETABSObject = myHelper.CreateObject(programPath)
            ' Defunct
            ''Late bind to ETABS.exe, create an instance of ETABSObject, and get a reference to cOAPI interface
            '    Set ETABSObject = CreateObject("CSI.ETABS.API.ETABSObject")

            'Start ETABS application
            ret = ETABSObject.ApplicationStart()
            'ret = ETABSObject.Hide()    'This function hides the application. When the application is hidden it is not visible on the screen or on the Windows task bar.
            'ret = ETABSObject.Unhide()  'This function unhides the application, that is, it makes it visible. When the application is hidden, it is not visible on the screen or on the Windows task bar.
        End If

        'Get a reference to cSapModel to access all OAPI classes and functions
        SapModel = ETABSObject.SapModel
    End Sub

    Private Sub SetModelDirectoryAndName()
        'full path to the model
        'set it to the desired path of your model
        modelDirectory = ActiveWorkbook.Path & Path.PathSeparator & wksht.Range(RANGE_NAME_DIR_MODEL).value

        If Len(Dir(modelDirectory, vbDirectory)) = 0 Then
            MkDir modelDirectory
        End If

        modelName = wksht.Range(RANGE_NAME_FILENAME_MODEL).value & "." & wksht.Range(RANGE_NAME_EXTENSION_MODEL).value

        pathModel = modelDirectory & Path.PathSeparator & modelName
    End Sub

    Private Function OpenModel() As Boolean
        OpenModel = True

        If Not FileExists(pathModel) Then
            OpenModel = False
        Else
            'Initialize model
            ret = SapModel.InitializeNewModel()

            If ret = 1 Then
                OpenModel = False
                Exit Function
            End If

            'open an existing file
            ret = SapModel.File.OpenFile(pathModel)

            If ret = 1 Then
                OpenModel = False
            End If
        End If
    End Function

    Private Function CreateModel() As Boolean
        CreateModel = True

        'Get a reference to cSapModel to access all OAPI classes and functions
        SapModel = ETABSObject.SapModel

        'initialize model
        '     ret = SapModel.InitializeNewModel(eUnits_kip_ft_F)
        ret = SapModel.InitializeNewModel

        If ret = 1 Then
            CreateModel = False
            Exit Function
        End If

        'create model from template
        ret = SapModel.File.New2DFrame(e2DFrameType_PortalFrame, 2, 144, 2, 288)

        If ret = 1 Then
            CreateModel = False
        End If
    End Function

    Private Sub RunAnalysis()
        'run analysis
        ret = SapModel.File.Save(pathModel)
        ret = SapModel.Analyze.RunAnalysis
    End Sub

    Private Sub RunDesignSteel()
        'start steel design
        ret = SapModel.DesignSteel.StartDesign
    End Sub

    Private Sub RunDesignConcrete()
        'start concrete design
        ret = SapModel.DesignConcrete.StartDesign
    End Sub

    Private Sub SelectFrame(ByVal p_name As String, _
                                Optional ByVal p_select As Boolean = True)
        If p_select Then
            ret = SapModel.SelectObj.Group(p_name)
        Else
            ret = SapModel.SelectObj.ClearSelection
        End If

    End Sub

    Private Sub CloseProgram()
        'Close ETABS
        ETABSObject.ApplicationExit(False)

        'Clean up variables
        SapModel = Nothing
        ETABSObject = Nothing
    End Sub

    Private Sub SetPassFailMethodStatus(ByVal p_ret As Long, ByVal p_rowOffset As Integer, ByVal p_rangeName As String)
        Dim value As String

        Select Case p_ret
            Case 0 : value = "Pass"
            Case 1 : value = "Fail"
            Case Else : value = "Fail"
        End Select

        wksht.Range(p_rangeName).Offset(p_rowOffset, 0).value = value
    End Sub

    Private Function RunForTable() As Boolean
        Dim batchName As String
        batchName = wksht.Range(RANGE_NAME_FILENAME_MODEL).value & "." & "bat"

        Dim retBatch
        retBatch = Shell(ActiveWorkbook.Path & Path.PathSeparator & batchName)

        '    If retBatch = 0 Then
        '        RunForTable = True
        '    Else
        '        RunForTable = False
        '    End If
        RunForTable = True
    End Function


    Private Function OpenTable() As Boolean
        OpenTable = False
        On Error GoTo SkipThis

        Dim fileName As String
        fileName = wksht.Range(RANGE_NAME_FILENAME_TABLE).value & "." & wksht.Range(RANGE_NAME_EXTENSION_TABLE).value
        If fileName = "" Then Exit Function

        Dim filePath As String
        filePath = GetAccessFilePath() & "\" & _
                   wksht.Range(RANGE_NAME_DIR_MODEL).value & "\" & _
                   fileName

        Dim OutputCase As String
        Dim SQLQuery As String
        Dim col As Long
        Dim row As Long

        'open database file
        If Not OpenAccessFile(filePath) Then Exit Function

        OpenTable = True
        Exit Function

SkipThis:
        Exit Function
    End Function

    '==============================
    '   Test Functions
    '==============================

    Private Sub GetConcreteBeamDesignSummaryResults(parameters() As String, _
                                                    caseRows() As Integer, _
                                                    parametersInputs() As String, _
                                                    parametersInputsStartRows() As Integer, _
                                                    rangeNameInputRow As Integer)
        Call RunAnalysis()
        Call RunDesignConcrete()

        Dim fillRange As Range
        fillRange = wksht.Range(RANGE_NAME_VALUE_GET)

        Dim rowOffset As Integer
        Dim i As Integer
        For i = 0 To UBound(caseRows)
            'dimension variables
            Dim name As String
            Dim NumberItems As Long
            Dim frameName() As String
            Dim location() As Double
            Dim TopCombo() As String
            Dim TopArea() As Double
            Dim BotCombo() As String
            Dim BotArea() As Double
            Dim VmajorCombo() As String
            Dim VmajorArea() As Double
            Dim TLCombo() As String
            Dim TLArea() As Double
            Dim TTCombo() As String
            Dim TTArea() As Double
            Dim ErrorSummary() As String
            Dim WarningSummary() As String
            Dim itemType As eItemType

            rowOffset = parametersInputsStartRows(0) + i * (UBound(parameters) + 1) - rangeNameInputRow
            name = wksht.Range(RANGE_NAME_INPUT).Offset(rowOffset, 0).value
            If name = "" Then Exit Sub

            rowOffset = parametersInputsStartRows(2) + i * (UBound(parameters) + 1) - rangeNameInputRow
            Dim itemTypeNum As Integer
            itemTypeNum = wksht.Range(RANGE_NAME_INPUT).Offset(rowOffset, 0).value

            If itemTypeNum = eItemType_Objects Or itemTypeNum = eItemType_Group Then
                Call SelectFrame(name, False)
            ElseIf itemTypeNum = eItemType_SelectedObjects Then
                Call SelectFrame(name, True)
            End If

            'get summary result data
            ret = SapModel.DesignConcrete.GetSummaryResultsBeam(name, NumberItems, frameName, location, TopCombo, TopArea, BotCombo, BotArea, VmajorCombo, VmajorArea, TLCombo, TLArea, TTCombo, TTArea, ErrorSummary, WarningSummary, itemTypeNum)

            Dim outputLocation As Double
            rowOffset = parametersInputsStartRows(1) + i * (UBound(parameters) + 1) - rangeNameInputRow
            outputLocation = wksht.Range(RANGE_NAME_INPUT).Offset(rowOffset, 0).value

            Dim frameNameItem As String
            frameNameItem = frameName(0)
            If itemTypeNum = eItemType_Group And UBound(frameName) > 0 Then
                frameNameItem = frameName(UBound(frameName))
            End If

            Dim resultIndex As Integer
            resultIndex = GetResultIndex(frameNameItem, frameName, outputLocation, location)
            If resultIndex < 0 Then Exit Sub

            Dim values(13) As String
            values(0) = frameName(resultIndex)
            values(1) = location(resultIndex)
            values(2) = TopCombo(resultIndex)
            values(3) = TopArea(resultIndex)
            values(4) = BotCombo(resultIndex)
            values(5) = BotArea(resultIndex)
            values(6) = VmajorCombo(resultIndex)
            values(7) = VmajorArea(resultIndex)
            values(8) = TLCombo(resultIndex)
            values(9) = TLArea(resultIndex)
            values(10) = TTCombo(resultIndex)
            values(11) = TTArea(resultIndex)
            values(12) = ErrorSummary(resultIndex)
            values(13) = WarningSummary(resultIndex)

            'fill summary result data
            rowOffset = 3 + i * (UBound(parameters) + 1)
            Dim j As Integer
            For j = 0 To UBound(values)
                Dim currentValue As String
                currentValue = values(j)

                If ((j = 12 Or j = 13) And currentValue = "") Then
                    currentValue = "No Message"
                End If

                fillRange.Offset(rowOffset, 0).value = currentValue
                Call SetPassFailMethodStatus(ret, rowOffset, RANGE_NAME_RET_GET)
                rowOffset = rowOffset + 1
            Next j
        Next i

    End Sub

    Private Function GetResultIndex(ByVal p_frameName As String, _
                                  p_frameNames() As String, _
                            ByVal p_location As Integer, _
                                  p_locations() As Double) As Integer
        GetResultIndex = -1

        Dim i As Integer
        For i = 0 To UBound(p_locations)
            If p_frameName = p_frameNames(i) Then
                If p_location = p_locations(i) Then
                    GetResultIndex = i
                    Exit For
                End If
            End If
        Next i
    End Function



    Private Sub GetConcreteBeamDesignSummaryResultsTable(parameters() As String, _
                                                            caseRows() As Integer, _
                                                            rangeNameInputRow As Integer)

        Dim fillRange As Range
        fillRange = wksht.Range(RANGE_NAME_VALUE_TABLE)

        Dim tableName As String
        tableName = "Concrete Beam Summary - ACI 318-14"

        Dim columnOffset As Integer
        columnOffset = wksht.Range(RANGE_NAME_VALUE_GET).Column - wksht.Range(RANGE_NAME_PARAMETER).Column

        Dim headers(13) As String
        headers(0) = "UniqueName"
        headers(1) = "Station"
        headers(2) = "AsTopCombo"
        headers(3) = "AsTop"
        headers(4) = "AsBotCombo"
        headers(5) = "AsBot"
        headers(6) = "VCombo"
        headers(7) = "VRebar"
        headers(8) = "TLngCombo"
        headers(9) = "TLngRebar"
        headers(10) = "TTrnCombo"
        headers(11) = "TTrnRebar"
        headers(12) = "ErrMsg"
        headers(13) = "WarnMsg"

        Dim rowOffset As Integer
        Dim i As Integer
        For i = 0 To UBound(caseRows)
            Dim frameName As Integer
            rowOffset = 3 + i * (UBound(parameters) + 1)
            frameName = wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, columnOffset).value

            Dim outputLocation As Double
            rowOffset = 4 + i * (UBound(parameters) + 1)
            outputLocation = wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, columnOffset).value

            'fill summary result data
            rowOffset = 3 + i * (UBound(parameters) + 1)
            Dim j As Integer
            For j = 0 To UBound(headers)
                Dim currentQueryValue As String
                currentQueryValue = QueryValue(headers(j), frameName, outputLocation, tableName)

                If ((j = 12 Or j = 13) And currentQueryValue = "") Then
                    currentQueryValue = "No Messages"
                End If

                fillRange.Offset(rowOffset, 0).value = currentQueryValue
                rowOffset = rowOffset + 1
            Next j
        Next i

    End Sub

    Private Function QueryValue(ByVal p_fieldHeader As String, _
                                ByVal p_frame As String, _
                                ByVal p_location As String, _
                                ByVal p_tableName) As String
        Dim SQLQuery As String

        'set query string
        SQLQuery = "SELECT UniqueName, Station, " & p_fieldHeader & " FROM [" & p_tableName & "] WHERE "
        SQLQuery = SQLQuery & "UniqueName LIKE " & p_frame & " AND Station LIKE " & p_location

        'get recordset
        orecordset = odatabase.OpenRecordset(SQLQuery)
        orecordset.movefirst()
        QueryValue = orecordset.Fields(p_fieldHeader).value
    End Function


    Private Sub SetSteelDesignOverwriteItemEurocode_3_2005()
        Dim rowOffset As Integer
        rowOffset = 1

        Dim item As Double
        Dim value As Double

        'set steel design code
        ret = SapModel.DesignSteel.SetCode(DESIGN_CODE)

        'set overwrite item
        While Not wksht.Range(RANGE_NAME_CODE).Offset(rowOffset, 0).value = ""
            item = wksht.Range(RANGE_NAME_CODE).Offset(rowOffset, 0).value
            value = wksht.Range(RANGE_NAME_VALUE_SET).Offset(rowOffset, 0).value

            ret = SapModel.DesignSteel.Eurocode_3_2005.SetOverwrite(FRAME_NAME, item, value)

            Call SetPassFailMethodStatus(ret, rowOffset, RANGE_NAME_RET_SET)

            rowOffset = rowOffset + 1
        End While
    End Sub

    Private Sub GetSteelDesignOverwriteItemEurocode_3_2005()
        Dim rowOffset As Integer
        rowOffset = 1

        Dim item As Double
        Dim value As Double
        Dim progDet As Boolean

        Call RunAnalysis()
        Call RunDesignSteel()

        'get overwrite item
        While Not wksht.Range(RANGE_NAME_CODE).Offset(rowOffset, 0).value = ""
            item = wksht.Range(RANGE_NAME_CODE).Offset(rowOffset, 0).value
            value = -1
            progDet = True

            ret = SapModel.DesignSteel.Eurocode_3_2005.GetOverwrite(FRAME_NAME, item, value, progDet)
            wksht.Range(RANGE_NAME_VALUE_GET).Offset(rowOffset, 0).value = value

            Call SetPassFailMethodStatus(ret, rowOffset, RANGE_NAME_RET_GET)

            rowOffset = rowOffset + 1
        End While
    End Sub


    '=============== Not Currently Used. On 'Sequence' Tab ===============

    Private Sub GetSection(ByVal name As String)
        Dim propName As String

        ret = SapModel.DesignConcrete.GetDesignSection(name, propName)

        Worksheets(wkshtName).Cells(row, col).value = ret
        Worksheets(wkshtName).Cells(row, col + 1).value = propName

        row = row + 1
    End Sub

    Private Sub SetSection(ByVal name As String, _
                            ByVal propName As String, _
                            ByVal lastAnalysis As Boolean, _
                            ByVal itemType As ETABS2015.eItemType)

        ret = SapModel.DesignConcrete.SetDesignSection(name, propName, lastAnalysis, itemType)

        Worksheets(wkshtName).Cells(row, col).value = ret
        Worksheets(wkshtName).Cells(row, col + 1).value = propName

        row = row + 1
    End Sub

    Public Sub Sequence()
        '==========
        ' Initialize
        rowStart = 4
        colStart = 6

        Dim section(1) As String
        section(0) = "ConcBm"
        section(1) = "ConcBm2"

        Dim frameName(2) As String
        frameName(0) = "72"
        frameName(1) = "72"
        frameName(2) = "Group1"

        Dim itemType(2) As ETABS2015.eItemType
        itemType(0) = eItemType_Objects
        itemType(1) = eItemType_SelectedObjects
        itemType(2) = eItemType_Group

        For i = 0 To 2
            row = rowStart
            col = colStart + 2 * i

            Call SelectFrame(frameName(i), i)

            ' Pre-Analysis
            Call GetSection(frameName(0))
            Call SetSection(frameName(i), section(1), False, itemType(i))
            Call GetSection(frameName(0))

            ' Run Analysis
            ret = SapModel.Analyze.DeleteResults("", True)
            ret = SapModel.Analyze.RunAnalysis

            ' Run Design
            ret = SapModel.DesignConcrete.StartDesign

            Call SelectFrame(frameName(i), i)

            ' Design Overwrite
            Call GetSection(frameName(0))
            Call SetSection(frameName(i), section(0), False, itemType(i))
            Call GetSection(frameName(0))

            ' Design Overwrite Reset
            'Call GetSection(frameName(i))
            Call SetSection(frameName(i), section(0), True, itemType(i))
            Call GetSection(frameName(0))
            Call SetSection(frameName(i), "", True, itemType(i))
            Call GetSection(frameName(0))

            ' Clear Analysis Results
            ret = SapModel.Analyze.DeleteResults("", True)

            ' Reset Example
            Call SetSection(frameName(i), section(0), False, itemType(i))
            Call GetSection(frameName(0))
        Next i


        Exit Sub

ErrHandler:
        MsgBox "Cannot run API script: " & Err.Description
    End Sub
End Module
