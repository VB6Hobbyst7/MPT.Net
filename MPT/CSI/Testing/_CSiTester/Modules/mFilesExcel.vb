Option Explicit On
Option Strict On


'Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office

''' <summary>
''' Module that contains routines for working specifically with Microsoft Excel Files and Microsoft Excel
''' </summary>
''' <remarks></remarks>
Module mFilesExcel
    ''For writing a new Excel file See: http://support.microsoft.com/default.aspx?scid=kb%3B[LN]%3BQ316934

    ''See: http://www.daniweb.com/software-development/vbnet/threads/29055/opening-an-excel-file-in-vb.net
    'Sub OpenExcel(ByVal FileName As String)
    '    'Dim xlsApp As Excel.Application
    '    'Dim xlsWB As Excel.Workbook
    '    'Dim xlsSheet As Excel.Worksheet
    '    'Dim xlsCell As Excel.Range
    '    'Dim xlsDatei As String

    '    'xlsApp = New Excel.Application
    '    'xlsApp.Visible = True
    '    'xlsWB = xlsApp.Workbooks.Open(FileName)
    '    'xlsSheet = xlsWB.Worksheets(0)
    '    'xlsCell = xlsSheet.Range("A1")
    'End Sub

    ''See: http://social.msdn.microsoft.com/Forums/en-US/4fe0c8c2-e952-4196-96d7-b833292a9c2e/open-an-excel-file-using-vbnet
    ''Public Sub OpenExcelDemo(ByVal FileName As String, ByVal SheetName As String)
    ''    If IO.File.Exists(FileName) Then
    ''        Dim Proceed As Boolean = False
    ''        Dim xlApp As Excel.Application = Nothing
    ''        Dim xlWorkBooks As Excel.Workbooks = Nothing
    ''        Dim xlWorkBook As Excel.Workbook = Nothing
    ''        Dim xlWorkSheet As Excel.Worksheet = Nothing
    ''        Dim xlWorkSheets As Excel.Sheets = Nothing
    ''        Dim xlCells As Excel.Range = Nothing
    ''        xlApp = New Excel.Application
    ''        xlApp.DisplayAlerts = False
    ''        xlWorkBooks = xlApp.Workbooks
    ''        xlWorkBook = xlWorkBooks.Open(FileName)
    ''        xlApp.Visible = True
    ''        xlWorkSheets = xlWorkBook.Sheets
    ''        For x As Integer = 1 To xlWorkSheets.Count
    ''            xlWorkSheet = CType(xlWorkSheets(x), Excel.Worksheet)
    ''            If xlWorkSheet.Name = SheetName Then
    ''                Console.WriteLine(SheetName)
    ''                Proceed = True
    ''                Exit For
    ''            End If
    ''            Runtime.InteropServices.Marshal.FinalReleaseComObject(xlWorkSheet)
    ''            xlWorkSheet = Nothing
    ''        Next
    ''        If Proceed Then
    ''            xlWorkSheet.Activate()
    ''            MessageBox.Show("File is open, if you close Excel just opened outside of this program we will crash-n-burn.")
    ''        Else
    ''            MessageBox.Show(SheetName & " not found.")
    ''        End If
    ''        xlWorkBook.Close()
    ''        xlApp.UserControl = True
    ''        xlApp.Quit()
    ''        ReleaseComObject(xlCells)
    ''        ReleaseComObject(xlWorkSheets)
    ''        ReleaseComObject(xlWorkSheet)
    ''        ReleaseComObject(xlWorkBook)
    ''        ReleaseComObject(xlWorkBooks)
    ''        ReleaseComObject(xlApp)
    ''    Else
    ''        MessageBox.Show("'" & FileName & "' not located. Try one of the write examples first.")
    ''    End If
    ''End Sub
    'Public Sub ReleaseComObject(ByVal obj As Object)
    '    Try
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
    '        obj = Nothing
    '    Catch ex As Exception
    '        obj = Nothing
    '    End Try
    'End Sub


End Module
