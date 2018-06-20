Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Public Class frmSettingsTableExport
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _outputSettingsUsedAll As Boolean
    ''' <summary>
    ''' If true, all examples are set to run with outputSettings.xml files, if they exist in the attachments folder.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputSettingsUsedAll As Boolean
        Set(ByVal value As Boolean)
            If Not _outputSettingsUsedAll = value Then
                _outputSettingsUsedAll = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("outputSettingsUsedAll"))
            End If
        End Set
        Get
            Return _outputSettingsUsedAll
        End Get
    End Property

    Private _tableExportFileExtensionAll As String
    ''' <summary>
    ''' If all examples are set to be run with a uniform file extension type for the exported table files, it is imposed by assigning the global file extension to this property.
    ''' For this to be used, outputSettingsUsedAll = True.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableExportFileExtensionAll As String
        Set(ByVal value As String)
            If Not _tableExportFileExtensionAll = value Then
                _tableExportFileExtensionAll = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tableExportFileExtensionAll"))
            End If
        End Set
        Get
            Return _tableExportFileExtensionAll
        End Get
    End Property

    Private _updateAttachments As Boolean
    ''' <summary>
    ''' If true, when outputSettings file is modified with new file extension value, both the activated file and the attachments file will be modified.
    ''' If false, only the activated file will be modified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property updateAttachments As Boolean
        Set(ByVal value As Boolean)
            If Not _updateAttachments = value Then
                _updateAttachments = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("updateAttachments"))
            End If
        End Set
        Get
            Return _updateAttachments
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData()
        InitializeControls()
    End Sub

    Private Sub InitializeData()
        outputSettingsUsedAll = testerSettings.outputSettingsUsedAll
        tableExportFileExtensionAll = testerSettings.tableExportFileExtensionAll
    End Sub

    Private Sub InitializeControls()
        Select Case tableExportFileExtensionAll
            Case "" : radBtnAsSpecified.IsChecked = True
            Case "xml" : radBtnMDB.IsChecked = True
            Case "mdb" : radBtnXML.IsChecked = True
            Case Else : radBtnAsSpecified.IsChecked = True
        End Select
        If outputSettingsUsedAll = True Then
            grpBxTableFileType.IsEnabled = True
        Else
            grpBxTableFileType.IsEnabled = False
            tableExportFileExtensionAll = ""
        End If

    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons
    Private Sub btnApply_Click(sender As Object, e As RoutedEventArgs) Handles btnApply.Click
        testerSettings.outputSettingsUsedAll = outputSettingsUsedAll
        testerSettings.tableExportFileExtensionAll = tableExportFileExtensionAll

        ApplyOutputSettingsActions()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '=== Checkbox
    Private Sub chkBxUseOutPutSettings_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxUseOutPutSettings.Checked
        grpBxTableFileType.IsEnabled = True
    End Sub
    Private Sub chkBxUseOutPutSettings_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxUseOutPutSettings.Unchecked
        grpBxTableFileType.IsEnabled = False
    End Sub

    '=== Radio Buttons
    Private Sub radBtnAsSpecified_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnAsSpecified.Checked
        tableExportFileExtensionAll = ""
    End Sub
    Private Sub radBtnXML_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnXML.Checked
        tableExportFileExtensionAll = "xml"
    End Sub
    Private Sub radBtnMDB_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnMDB.Checked
        tableExportFileExtensionAll = "mdb"
    End Sub
#End Region

#Region "Methods"
    Private Sub ApplyOutputSettingsActions()
        myCsiTester.ApplyOutputSettingsActions(outputSettingsUsedAll, tableExportFileExtensionAll, updateAttachments)
    End Sub
#End Region

End Class
