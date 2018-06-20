Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Forms.FormsLibrary
Imports MPT.String
Imports MPT.String.ConversionLibrary

Imports CSiTester.cMCModel


Public Class frmXMLTemplateGeneratorAdvanced
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Variables"
    Private myMCModelTemp As cMCModel
    Private exampleAction As eExampleAction
#End Region

#Region "Properties"
    Private _myMCModel As cMCModel
    ''' <summary>
    ''' Class representing the model control XML file to be generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myMCModel As cMCModel
        Set(ByVal value As cMCModel)
            _myMCModel = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myMCModel"))
        End Set
        Get
            Return _myMCModel
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New(ByRef MCModel As cMCModel, ByVal myAction As eExampleAction)

        InitializeData(MCModel, myAction)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeControls()
        CheckRequiredDataFilled()
    End Sub

    Private Sub InitializeData(ByRef MCModel As cMCModel, ByVal myAction As eExampleAction)
        myMCModel = MCModel
        myMCModelTemp = CType(MCModel.Clone, cMCModel)

        exampleAction = myAction
    End Sub

    Private Sub InitializeControls()
        btnOK.IsEnabled = False

        InitializeDateComboBoxes()

        For Each testType As eTestType In myMCModelTemp.tests
            Select Case testType
                Case eTestType.runAsIs : chkBxRunAsIs.IsChecked = True
                Case eTestType.runAsIsDiffAnalyParams : chkBxRunAsIsPSB.IsChecked = True
                Case eTestType.updateBridge : chkBxUpdateBridge.IsChecked = True
                Case eTestType.updateBridgeAndRun : chkBxUpdateBridgeAndRun.IsChecked = True
            End Select
        Next

        If exampleAction = eExampleAction.edit Then
            Me.Title = "Model Control XML: Advanced Options"
        End If
    End Sub

    Private Sub InitializeDateComboBoxes()
        '===Combo Boxes
        'Is Public
        cmbBxIsPublic.ItemsSource = testerSettings.yesNoBooleans
        cmbBxIsPublic.SelectedIndex = GetSelectedIndex(ConvertYesTrueString(myMCModelTemp.isPublic, eCapitalization.Firstupper), testerSettings.yesNoBooleans)

        'Is Bug
        cmbBxIsBug.ItemsSource = testerSettings.yesNoBooleans
        cmbBxIsBug.SelectedIndex = GetSelectedIndex(ConvertYesTrueString(myMCModelTemp.isBug, eCapitalization.Firstupper), testerSettings.yesNoBooleans)

        'Example Status
        cmbBxExampleStatus.ItemsSource = testerSettings.statusTypes
        cmbBxExampleStatus.SelectedIndex = GetSelectedIndex(myMCModelTemp.statusExample, testerSettings.statusTypes)

        'Documentation Status
        cmbBxDocumenationStatus.ItemsSource = testerSettings.documentationStatusTypes
        cmbBxDocumenationStatus.SelectedIndex = GetSelectedIndex(myMCModelTemp.statusDocumentation, testerSettings.documentationStatusTypes)

    End Sub


#End Region

#Region "Form Controls"
    '===Buttons
    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs) Handles btnOK.Click
        myMCModel = CType(myMCModelTemp.Clone, cMCModel)
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    '=== Combo Boxes
    Private Sub cmbBxIsPublic_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxIsPublic.SelectionChanged
        myMCModelTemp.isPublic = ConvertYesTrueBoolean(CStr(cmbBxIsPublic.SelectedItem))
    End Sub
    Private Sub cmbBxIsBug_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxIsBug.SelectionChanged
        myMCModelTemp.isBug = ConvertYesTrueBoolean(CStr(cmbBxIsBug.SelectedItem))
    End Sub
    Private Sub cmbBxExampleStatus_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxExampleStatus.SelectionChanged
        myMCModelTemp.statusExample = CStr(cmbBxExampleStatus.SelectedItem)
    End Sub
    Private Sub cmbBxDocumenationStatus_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxDocumenationStatus.SelectionChanged
        myMCModelTemp.statusDocumentation = CStr(cmbBxDocumenationStatus.SelectedItem)
    End Sub

    '=== Check Boxes
    Private Sub chkBxRunAsIs_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxRunAsIs.Checked
        myMCModelTemp.tests.Add(eTestType.runAsIs)
        CheckRequiredDataFilled()
    End Sub
    Private Sub chkBxRunAsIs_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxRunAsIs.Unchecked
        myMCModelTemp.tests.Remove(eTestType.runAsIs)
        CheckRequiredDataFilled()
    End Sub

    Private Sub chkBxRunAsIsPSB_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxRunAsIsPSB.Checked
        myMCModelTemp.tests.Add(eTestType.runAsIsDiffAnalyParams)
        CheckRequiredDataFilled()
    End Sub
    Private Sub chkBxRunAsIsPSB_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxRunAsIsPSB.Unchecked
        myMCModelTemp.tests.Remove(eTestType.runAsIsDiffAnalyParams)
        CheckRequiredDataFilled()
    End Sub

    Private Sub chkBxUpdateBridge_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxUpdateBridge.Checked
        myMCModelTemp.tests.Add(eTestType.updateBridge)
        CheckRequiredDataFilled()
    End Sub
    Private Sub chkBxUpdateBridge_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxUpdateBridge.Unchecked
        myMCModelTemp.tests.Remove(eTestType.updateBridge)
        CheckRequiredDataFilled()
    End Sub

    Private Sub chkBxUpdateBridgeAndRun_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxUpdateBridgeAndRun.Checked
        myMCModelTemp.tests.Add(eTestType.updateBridgeAndRun)
        CheckRequiredDataFilled()
    End Sub
    Private Sub chkBxUpdateBridgeAndRun_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxUpdateBridgeAndRun.Unchecked
        myMCModelTemp.tests.Remove(eTestType.updateBridgeAndRun)
        CheckRequiredDataFilled()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Checks whether the minimum required information is present and performs operations based on that state.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckRequiredDataFilled()
        If btnOK IsNot Nothing Then
            If myMCModelTemp.tests.RequiredDataFilled() Then
                btnOK.IsEnabled = True
            Else
                btnOK.IsEnabled = False
            End If
        End If
    End Sub
#End Region

End Class
