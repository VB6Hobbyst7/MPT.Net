Option Explicit On
Module regTestEXEExcel
    'Sub getCellNames(ByRef namesList() As String)
    '    'Gets all named range names in the entire workbook
    '    Dim oNM As name
    '    Dim i As Long

    '    i = 0
    '    For Each oNM In ActiveWorkbook.Names
    '        ReDim Preserve namesList(i)
    '        namesList(i) = oNM.name
    '        i = i + 1
    '    Next oNM
    '    '    For Each oNM In ActiveSheet.Names   'Could this return all names just in the sheet?
    '    '        ReDim Preserve namesList(i)
    '    '        namesList(i) = oNM.name
    '    '        i = i + 1
    '    '    Next oNM

    'End Sub
    Sub testSuiteSetup()
        frmTestSuiteSetup.ShowDialog()
        frmTestSuiteSetup.Dispose()
    End Sub

    Sub testRunSetup()
        frmRunSetup.ShowDialog()
        frmRunSetup.Dispose()
    End Sub

    Sub readXML()

        Call regtestXML_read_write_regTest(True)
        Call regtestXML_read_write_List(True)
        Call regtestXML_read_write_Objects(True)

    End Sub

    Sub writeXML()

        Call regtestXML_read_write_regTest(False)
        Call regtestXML_read_write_List(False)
        Call regtestXML_read_write_Objects(False)

        MsgBox("XML Updated")

    End Sub
End Module
