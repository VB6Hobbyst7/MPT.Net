Option Strict On
Option Explicit On

Imports CSiTester.cLibPath

Public Class frmXMLObjectResultsExcel
#Region "Properties"
    Friend Property excelResultOriginal As cMCResultExcel
    Friend Property excelResult As cMCResultExcel
    Friend Property myXmlPath As String
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetDefaults()
    End Sub

    Friend Sub New(ByRef myExcelResult As cMCResultExcel, ByVal xmlPath As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetDefaults()
        excelResultOriginal = myExcelResult
        myXmlPath = xmlPath
    End Sub

    Private Sub SetDefaults()
        With excelResult.copyAction
            .copySourceToDestination = True
            .fileDestinationAbsolute = ""
            '.fileDestinationRelative = "attachments"
            '.fileSameAsModel = ""
        End With
    End Sub
#End Region

#Region "Form Controls"
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddExcelFile_Click(sender As Object, e As RoutedEventArgs) Handles btnAddExcelFile.Click
        excelResultOriginal = CType(excelResult.Clone, cMCResultExcel)
    End Sub

    Private Sub btnExcelSource_Click(sender As Object, e As RoutedEventArgs) Handles btnExcelSource.Click
        With excelResult.copyAction
            BrowseForFile(.fileSource, "*.")

            .fileExtension = GetSuffix(.fileSource, ".")
            .fileName = FilterStringFromName(GetSuffix(.fileSource, "\"), "." & .fileExtension, True, False)
        End With
    End Sub
#End Region

#Region "Methods"

#End Region


End Class
