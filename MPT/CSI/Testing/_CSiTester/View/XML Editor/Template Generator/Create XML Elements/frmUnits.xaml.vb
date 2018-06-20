Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.Units

Class frmUnits

#Region "Properties: Private"
    ''' <summary>
    ''' List of all base units available for selection in the schema combo boxes.
    ''' </summary>
    ''' <remarks></remarks>
    Private _schemaListFull As New List(Of String)

    ''' <summary>
    ''' A subset list of the most commonly used items from '_allUnitTypes', for which a predefined schema is available.
    ''' </summary>
    ''' <remarks></remarks>
    Private _quickUnitTypes As New List(Of String)

    ''' <summary>
    ''' List of all of the unit types for which a predefined schema is available.
    ''' </summary>
    ''' <remarks></remarks>
    Private _allUnitTypes As New List(Of String)

    ''' <summary>
    ''' Contains a list of units controller objects that each contain a single unit with the name to be used for its corresponding type.
    ''' This is typically filled from the [Program Control] table exported by a CSi program.
    ''' </summary>
    ''' <remarks></remarks>
    Private _defaultUnitNames As New List(Of cUnitsController)
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Dummy units object to use for testing units conversion.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property unitsConvert As New cUnits

    ''' <summary>
    ''' Main units object that is manipulated by the form and whos results are displayed in the form.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property unitsController As New cUnitsController

    ''' <summary>
    ''' Units controll that is passed in to the form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property unitsControllerSave As New cUnitsController

    ''' <summary>
    ''' Property that specifies whether or not the form was canceled out, or if new values were potentially saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property formCancel As Boolean = True
#End Region

#Region "Initialization"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeControls()
        SetDefaults()
    End Sub

    Public Sub New(ByVal p_unitsController As cUnitsController,
                   Optional p_programControlUnits As List(Of cUnitsController) = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If p_programControlUnits IsNot Nothing Then _defaultUnitNames = p_programControlUnits

        unitsControllerSave = p_unitsController

        InitializeControls()
        SetDefaults()
    End Sub

    Private Sub InitializeControls()
        'Custom Units
        _schemaListFull = GetEnumDescriptionList(eUnitType.none)

        '= Numerator Controls
        cmbBox_r0c0.ItemsSource = _schemaListFull
        cmbBox_r0c1.ItemsSource = _schemaListFull
        cmbBox_r0c2.ItemsSource = _schemaListFull
        cmbBox_r0c3.ItemsSource = _schemaListFull

        '= Denominator Controls
        cmbBox_r1c0.ItemsSource = _schemaListFull
        cmbBox_r1c1.ItemsSource = _schemaListFull
        cmbBox_r1c2.ItemsSource = _schemaListFull
        cmbBox_r1c3.ItemsSource = _schemaListFull

        'Unit Types (must follow the Custom Units Controls Initialization)
        _quickUnitTypes = unitsController.quickUnitTypes
        cmbBoxQuickType.ItemsSource = _quickUnitTypes

        _allUnitTypes = unitsController.allUnitTypes
        cmbBoxAllTypes.ItemsSource = _allUnitTypes
    End Sub

    Private Sub SetDefaults()
        ' These change unitsController
        cmbBoxAllTypes.SelectedIndex = 0
        cmbBoxQuickType.SelectedIndex = 0

        ' So unitsController is set to the saved object here
        unitsController = DirectCast(unitsControllerSave.Clone, cUnitsController)
        If unitsControllerSave.units.unitsAll.Count = 0 Then
            SetDefaultsStandard()
        Else
            SetDefaultsByModel()
        End If

        chkBxProduction.IsChecked = True
        btnCombineUnits.IsEnabled = False
        grpBxTesting.Visibility = Windows.Visibility.Collapsed
        grpBxTestingSaved.Visibility = Windows.Visibility.Collapsed
        grpBxTestingPreview.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Sub SetDefaultsStandard()
        radBtnQuickType.IsChecked = True
        grpBxSchema.IsEnabled = False
    End Sub

    Private Sub SetDefaultsByModel()
        Dim fillControlsFromSavedUnits As Boolean = True

        If unitsControllerSave.type = cUnitsController.eUnitTypeStandard.custom Then
            radBtnCustomType.IsChecked = True
            grpBxSchema.IsEnabled = True
        Else
            radBtnCustomType.IsChecked = False
            grpBxSchema.IsEnabled = False
            If unitsControllerSave.typeShorthand = cUnitsController.eUnitTypeShorthand.none Then
                chkBxUseShorthandUnits.IsChecked = False

                Dim unitTypeUsed As String = GetEnumDescription(unitsControllerSave.type)
                If _quickUnitTypes.Contains(unitTypeUsed) Then
                    radBtnQuickType.IsChecked = True
                    SetComboBoxSelection(cmbBoxQuickType, unitTypeUsed, p_useIndex:=True)
                Else
                    radBtnAllTypes.IsChecked = True
                    SetComboBoxSelection(cmbBoxAllTypes, unitTypeUsed, p_useIndex:=True)
                End If
            Else
                radBtnAllTypes.IsChecked = True
                SetComboBoxSelection(cmbBoxAllTypes, GetEnumDescription(unitsControllerSave.type), p_useIndex:=True)
                chkBxUseShorthandUnits.IsChecked = True
                SetComboBoxSelection(cmbBoxShorthand, unitsControllerSave.units.shorthandLabel, p_useIndex:=True)
                fillControlsFromSavedUnits = False
            End If
        End If

        If fillControlsFromSavedUnits Then FillControls()
    End Sub

    Private Sub FillControls()
        Dim unitSet As List(Of cUnit)
        Dim maxControlIndex As Integer = 3

        ' Numerators
        unitSet = unitsControllerSave.units.unitsNumerator
        For i = 0 To Math.Min(maxControlIndex, unitSet.Count - 1)
            FillControl(unitSet, i, p_isNumerator:=True)
        Next

        ' Denominators
        unitSet = unitsControllerSave.units.unitsDenominator
        For i = 0 To Math.Min(maxControlIndex, unitSet.Count - 1)
            FillControl(unitSet, i, p_isNumerator:=False)
        Next
    End Sub

    Private Sub FillControl(ByVal p_unitSet As List(Of cUnit),
                            ByVal p_controlNumber As Integer,
                            ByVal p_isNumerator As Boolean)
        If p_unitSet.Count > p_controlNumber Then
            Dim unit As cUnit = p_unitSet(p_controlNumber)
            Dim row As Integer
            If p_isNumerator Then
                row = 0
            Else
                row = 1
            End If
            Dim controlSuffix As String = "_r" & row & "c" & p_controlNumber

            SetUnitComboBox("cmbBox" & controlSuffix, GetEnumDescription(unit.type))
            SetUnitTextBox("txtBxPower" & controlSuffix, unit.power)
            SetStackPanelVisible("stackPanelSchema" & controlSuffix)

            SetUnitComboBox("cmbBoxValues" & controlSuffix, unit.name)
            SetStackPanelVisible("stackPanelValue" & controlSuffix)
        End If
    End Sub

    Private Sub SetUnitComboBox(ByVal p_name As String,
                                ByVal p_value As String)
        Dim comboBoxSchema As Object = FindName(p_name)
        If TypeOf (comboBoxSchema) Is ComboBox Then
            Dim comboBox As ComboBox = DirectCast(comboBoxSchema, ComboBox)
            SetComboBoxSelection(comboBox, p_value, p_useIndex:=True)
        End If
    End Sub

    Private Sub SetUnitTextBox(ByVal p_name As String,
                               ByVal p_value As String)
        Dim textBox As Object = FindName(p_name)
        If TypeOf (textBox) Is TextBox Then
            Dim textBoxPower As TextBox = DirectCast(textBox, TextBox)
            textBoxPower.Text = p_value
        End If
    End Sub

    Private Sub SetStackPanelVisible(ByVal p_name As String)
        Dim stackPanel As Object = FindName(p_name)
        If TypeOf (stackPanel) Is StackPanel Then
            Dim visibleStackPanel As StackPanel = DirectCast(stackPanel, StackPanel)
            visibleStackPanel.Visibility = Windows.Visibility.Visible
        End If
    End Sub

#End Region


#Region "Form Controls"
    'Buttons
    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        formCancel = False
        unitsControllerSave = unitsController
        Me.Close()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        formCancel = True
        Me.Close()
    End Sub

    'Radio Buttons
    Private Sub radBtnQuickType_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnQuickType.Checked
        cmbBoxQuickType.IsEnabled = True
        UpdateControls(cmbBoxQuickType)
    End Sub
    Private Sub radBtnQuickType_Unchecked(sender As Object, e As RoutedEventArgs) Handles radBtnQuickType.Unchecked
        cmbBoxQuickType.IsEnabled = False
    End Sub

    Private Sub radBtnAllTypes_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnAllTypes.Checked
        cmbBoxAllTypes.IsEnabled = True
        UpdateControls(cmbBoxAllTypes)
    End Sub
    Private Sub radBtnAllTypes_Unchecked(sender As Object, e As RoutedEventArgs) Handles radBtnAllTypes.Unchecked
        cmbBoxAllTypes.IsEnabled = False
    End Sub

    Private Sub radBtnCustomType_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnCustomType.Checked
        grpBxSchema.IsEnabled = True
        newSchemaNumerator.Visibility = Windows.Visibility.Visible
        newSchemaDenominator.Visibility = Windows.Visibility.Visible

        unitsController.type = cUnitsController.eUnitTypeStandard.custom
        LoadSchemaAndValues()

        If unitsController.units.unitsDenominator.Count > 0 Then btnRemoveSchemaDenominator.IsEnabled = True
    End Sub
    Private Sub radBtnCustomType_Unchecked(sender As Object, e As RoutedEventArgs) Handles radBtnCustomType.Unchecked
        grpBxSchema.IsEnabled = False
        newSchemaNumerator.Visibility = Windows.Visibility.Collapsed
        newSchemaDenominator.Visibility = Windows.Visibility.Collapsed
    End Sub

    'Combo Boxes
    Private Sub cmbBoxQuickType_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxQuickType.SelectionChanged
        UpdateControls(cmbBoxQuickType)
    End Sub

    Private Sub cmbBoxAllTypes_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxAllTypes.SelectionChanged
        UpdateControls(cmbBoxAllTypes)
    End Sub

    Private Sub cmbBoxShorthand_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxShorthand.SelectionChanged
        Dim schemaType As String = ""

        'Sets schema type for ambiguous cases where shorthand label applies to multiple schema types
        If radBtnQuickType.IsChecked Then
            schemaType = CStr(cmbBoxQuickType.SelectedItem)
        ElseIf radBtnAllTypes.IsChecked Then
            schemaType = CStr(cmbBoxAllTypes.SelectedItem)
        End If


        unitsController.AssignShorthandUnits(CStr(cmbBoxShorthand.SelectedItem), schemaType)
        LoadSchemaAndValues()
        UpdateUnitsLabelPreview()
    End Sub

    'Check Boxes
    Private Sub chkBxUseShorthandUnits_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxUseShorthandUnits.Checked
        cmbBoxShorthand.IsEnabled = True
        cmbBoxShorthand.ItemsSource = unitsController.shorthandUnitsAvailable

        grpBxMenu.IsEnabled = False
        grpBxValues.IsEnabled = False
        grpBxSchema.IsEnabled = False
    End Sub
    Private Sub chkBxUseShorthandUnits_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxUseShorthandUnits.Unchecked
        cmbBoxShorthand.IsEnabled = False
        cmbBoxShorthand.SelectedIndex = 0

        grpBxMenu.IsEnabled = True
        grpBxValues.IsEnabled = True
        If radBtnCustomType.IsChecked Then grpBxSchema.IsEnabled = True
    End Sub

    'Custom Unit Controls
    '= Numerator Controls
    Private Sub btnRemoveSchemaNumerator_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveSchemaNumerator.Click
        Dim isVisible As Boolean = True

        Select Case isVisible
            Case stackPanelSchema_r0c3.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r0c3.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r0c3.Visibility = Windows.Visibility.Collapsed
                cmbBox_r0c3.SelectedValue = ""
                cmbBox_r0c3_SelectionChanged(Nothing, Nothing)

                btnAddSchemaNumerator.IsEnabled = True

            Case stackPanelSchema_r0c2.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r0c2.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r0c2.Visibility = Windows.Visibility.Collapsed
                cmbBox_r0c2.SelectedValue = ""
                cmbBox_r0c2_SelectionChanged(Nothing, Nothing)

                btnAddSchemaNumerator.IsEnabled = True

            Case stackPanelSchema_r0c1.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r0c1.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r0c1.Visibility = Windows.Visibility.Collapsed
                cmbBox_r0c1.SelectedValue = ""
                cmbBox_r0c1_SelectionChanged(Nothing, Nothing)

                btnAddSchemaNumerator.IsEnabled = True
                btnRemoveSchemaNumerator.IsEnabled = False
        End Select
    End Sub
    Private Sub btnAddSchemaNumerator_Click(sender As Object, e As RoutedEventArgs) Handles btnAddSchemaNumerator.Click
        Dim isVisible As Boolean = True

        Select Case isVisible
            Case stackPanelSchema_r0c1.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r0c1.Visibility = Windows.Visibility.Visible
                stackPanelValue_r0c1.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaNumerator.IsEnabled = True
            Case stackPanelSchema_r0c2.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r0c2.Visibility = Windows.Visibility.Visible
                stackPanelValue_r0c2.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaNumerator.IsEnabled = True
            Case stackPanelSchema_r0c3.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r0c3.Visibility = Windows.Visibility.Visible
                stackPanelValue_r0c3.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaNumerator.IsEnabled = True
                btnAddSchemaNumerator.IsEnabled = False
        End Select
    End Sub

    '== r0c0
    Private Sub cmbBox_r0c0_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r0c0.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 0

        cmbBoxSchemaSelectionChanged(cmbBox_r0c0, txtBxPower_r0c0, cmbBoxValues_r0c0, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r0c0_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r0c0.TextChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 0

        txtBxPowerTextChanged(txtBxPower_r0c0, lblValuesPower_r0c0, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r0c0_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r0c0.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 0

        cmbBoxValueSelectionChanged(cmbBoxValues_r0c0, unitIndex, isNumerator)
    End Sub

    '== r0c1
    Private Sub cmbBox_r0c1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r0c1.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 1

        cmbBoxSchemaSelectionChanged(cmbBox_r0c1, txtBxPower_r0c1, cmbBoxValues_r0c1, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r0c1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r0c1.TextChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 1

        txtBxPowerTextChanged(txtBxPower_r0c1, lblValuesPower_r0c1, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r0c1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r0c1.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 1

        cmbBoxValueSelectionChanged(cmbBoxValues_r0c1, unitIndex, isNumerator)
    End Sub

    '== r0c2
    Private Sub cmbBox_r0c2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r0c2.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 2

        cmbBoxSchemaSelectionChanged(cmbBox_r0c2, txtBxPower_r0c2, cmbBoxValues_r0c2, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r0c2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r0c2.TextChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 2

        txtBxPowerTextChanged(txtBxPower_r0c2, lblValuesPower_r0c2, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r0c2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r0c2.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 2

        cmbBoxValueSelectionChanged(cmbBoxValues_r0c2, unitIndex, isNumerator)
    End Sub

    '== r0c3
    Private Sub cmbBox_r0c3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r0c3.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 3

        cmbBoxSchemaSelectionChanged(cmbBox_r0c3, txtBxPower_r0c3, cmbBoxValues_r0c3, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r0c3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r0c3.TextChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 3

        txtBxPowerTextChanged(txtBxPower_r0c3, lblValuesPower_r0c3, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r0c3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r0c3.SelectionChanged
        Dim isNumerator As Boolean = True
        Dim unitIndex As Integer = 3

        cmbBoxValueSelectionChanged(cmbBoxValues_r0c3, unitIndex, isNumerator)
    End Sub

    '= Denominator Controls
    Private Sub btnRemoveSchemaDenominator_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveSchemaDenominator.Click
        Dim isVisible As Boolean = True

        Select Case isVisible
            Case stackPanelSchema_r1c3.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r1c3.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r1c3.Visibility = Windows.Visibility.Collapsed
                cmbBox_r1c3.SelectedValue = ""
                cmbBox_r1c3_SelectionChanged(Nothing, Nothing)

                btnAddSchemaDenominator.IsEnabled = True

            Case stackPanelSchema_r1c2.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r1c2.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r1c2.Visibility = Windows.Visibility.Collapsed
                cmbBox_r1c2.SelectedValue = ""
                cmbBox_r1c2_SelectionChanged(Nothing, Nothing)

                btnAddSchemaDenominator.IsEnabled = True

            Case stackPanelSchema_r1c1.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r1c1.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r1c1.Visibility = Windows.Visibility.Collapsed
                cmbBox_r1c1.SelectedValue = ""
                cmbBox_r1c1_SelectionChanged(Nothing, Nothing)

                btnAddSchemaDenominator.IsEnabled = True

            Case stackPanelSchema_r1c0.Visibility = Windows.Visibility.Visible
                stackPanelSchema_r1c0.Visibility = Windows.Visibility.Collapsed
                stackPanelValue_r1c0.Visibility = Windows.Visibility.Collapsed
                cmbBox_r1c0.SelectedValue = ""
                cmbBox_r1c0_SelectionChanged(Nothing, Nothing)

                btnAddSchemaDenominator.IsEnabled = True
                btnRemoveSchemaDenominator.IsEnabled = False
        End Select
    End Sub
    Private Sub btnAddSchemaDenominator_Click(sender As Object, e As RoutedEventArgs) Handles btnAddSchemaDenominator.Click
        Dim isVisible As Boolean = True

        Select Case isVisible
            Case stackPanelSchema_r1c0.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r1c0.Visibility = Windows.Visibility.Visible
                stackPanelValue_r1c0.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaDenominator.IsEnabled = True

            Case stackPanelSchema_r1c1.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r1c1.Visibility = Windows.Visibility.Visible
                stackPanelValue_r1c1.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaDenominator.IsEnabled = True

            Case stackPanelSchema_r1c2.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r1c2.Visibility = Windows.Visibility.Visible
                stackPanelValue_r1c2.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaDenominator.IsEnabled = True

            Case stackPanelSchema_r1c3.Visibility = Windows.Visibility.Collapsed
                stackPanelSchema_r1c3.Visibility = Windows.Visibility.Visible
                stackPanelValue_r1c3.Visibility = Windows.Visibility.Visible

                btnRemoveSchemaDenominator.IsEnabled = True
                btnAddSchemaDenominator.IsEnabled = False
        End Select
    End Sub

    '== r1c0
    Private Sub cmbBox_r1c0_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r1c0.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 0

        cmbBoxSchemaSelectionChanged(cmbBox_r1c0, txtBxPower_r1c0, cmbBoxValues_r1c0, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r1c0_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r1c0.TextChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 0

        txtBxPowerTextChanged(txtBxPower_r1c0, lblValuesPower_r1c0, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r1c0_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r1c0.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 0

        cmbBoxValueSelectionChanged(cmbBoxValues_r1c0, unitIndex, isNumerator)
    End Sub

    '== r1c1
    Private Sub cmbBox_r1c1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r1c1.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 1

        cmbBoxSchemaSelectionChanged(cmbBox_r1c1, txtBxPower_r1c1, cmbBoxValues_r1c1, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r1c1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r1c1.TextChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 1

        txtBxPowerTextChanged(txtBxPower_r1c1, lblValuesPower_r1c1, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r1c1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r1c1.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 1

        cmbBoxValueSelectionChanged(cmbBoxValues_r1c1, unitIndex, isNumerator)
    End Sub

    '== r1c2
    Private Sub cmbBox_r1c2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r1c2.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 2

        cmbBoxSchemaSelectionChanged(cmbBox_r1c2, txtBxPower_r1c2, cmbBoxValues_r1c2, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r1c2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r1c2.TextChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 2

        txtBxPowerTextChanged(txtBxPower_r1c2, lblValuesPower_r1c2, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r1c2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r1c2.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 2

        cmbBoxValueSelectionChanged(cmbBoxValues_r1c2, unitIndex, isNumerator)
    End Sub

    '== r1c3
    Private Sub cmbBox_r1c3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_r1c3.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 3

        cmbBoxSchemaSelectionChanged(cmbBox_r1c3, txtBxPower_r1c3, cmbBoxValues_r1c3, unitIndex, isNumerator)
    End Sub
    Private Sub txtBxPower_r1c3_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxPower_r1c3.TextChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 3

        txtBxPowerTextChanged(txtBxPower_r1c3, lblValuesPower_r1c3, unitIndex, isNumerator)
    End Sub
    Private Sub cmbBoxValues_r1c3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxValues_r1c3.SelectionChanged
        Dim isNumerator As Boolean = False
        Dim unitIndex As Integer = 3

        cmbBoxValueSelectionChanged(cmbBoxValues_r1c3, unitIndex, isNumerator)
    End Sub
#End Region

#Region "Form Behavior"
    ''' <summary>
    ''' Adjusts the length of the divisor elements as the width of the form changes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridUnitsSchema_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridUnitsSchema.SizeChanged
        schemaDivisor.X2 = gridUnitsSchema.ActualWidth - 50
        valueDivisor.X2 = gridUnitsSchema.ActualWidth - 50
    End Sub
#End Region

#Region "Form Controls: Testing"
    'Check Boxes
    Private Sub chkBxProduction_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxProduction.Checked
        grpBxTesting.Visibility = Windows.Visibility.Collapsed
        grpBxTestingSaved.Visibility = Windows.Visibility.Collapsed
        grpBxTestingPreview.Visibility = Windows.Visibility.Collapsed
    End Sub
    Private Sub chkBxProduction_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxProduction.Unchecked
        grpBxTesting.Visibility = Windows.Visibility.Visible
        grpBxTestingSaved.Visibility = Windows.Visibility.Visible
        grpBxTestingPreview.Visibility = Windows.Visibility.Visible
    End Sub

    'Buttons
    ''' <summary>
    ''' Tests the 'SwapNumeratorsDenominators' cUnits method. 
    ''' Note that this does not affect the assignments in the controls. 
    ''' Effects are only shown in the previews!
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSwapNumeratorDenominatorInPreview_Click(sender As Object, e As RoutedEventArgs) Handles btnSwapNumeratorDenominatorInPreview.Click

        unitsController.units.SwapNumeratorsDenominators()

        txtBxSchemaPreview.Text = unitsController.units.GetSchemaLabel
        txtBxValuesPreview.Text = unitsController.units.GetUnitsLabel
    End Sub

    Private Sub btnPreviewLists_Click(sender As Object, e As RoutedEventArgs) Handles btnPreviewLists.Click
        txtBxSchemaPreview.Text = ParseSchemaListToString()
        txtBxValuesPreview.Text = ParseValuesListToString()
    End Sub

    Private Sub btnParseStringToUnits_Click(sender As Object, e As RoutedEventArgs) Handles btnParseStringToUnits.Click
        Dim unitsLabel As String = txtBxStringToUnits.Text

        unitsController.ParseStringToUnits(unitsLabel)

        LoadSchemaAndValues()
        UpdateUnitsLabelPreview()
    End Sub

    Private Sub btnSaveForTest_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveForTest.Click
        unitsConvert = CType(unitsController.units.Clone, cUnits)
        txtBxValuesSavedPreview.Text = unitsConvert.GetUnitsLabel
    End Sub

    ''' <summary>
    ''' Tests the 'Convert' cUnits method. 
    ''' Only works if a units object has been saved, which then appears in the 'Saved Preview' group box.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnConvert_Click(sender As Object, e As RoutedEventArgs) Handles btnConvert.Click
        Dim conversionValue As Double

        If String.IsNullOrEmpty(txtBxValuesSavedPreview.Text) Then Exit Sub

        conversionValue = unitsController.units.Convert(unitsConvert)

        txtBxValuesConverted.Text = CStr(conversionValue)
    End Sub

    Private Sub btnCombineUnits_Click(sender As Object, e As RoutedEventArgs) Handles btnCombineUnits.Click

    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Updates the units object and properly resets controls to display the new object state.
    ''' </summary>
    ''' <param name="p_comboBox"></param>
    ''' <remarks></remarks>
    Private Sub UpdateControls(ByVal p_comboBox As ComboBox)
        ClearComboBoxSelections()
        unitsController.SetTypeByDescription(CStr(p_comboBox.SelectedItem))
        LoadSchemaAndValues()
        UpdateUnitsLabelPreview()
    End Sub

    ''' <summary>
    ''' Updates the value displayed in the units label preview on the form.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateUnitsLabelPreview()
        Dim unitLabel As String = unitsController.units.shorthandLabel
        If String.IsNullOrEmpty(unitLabel) Then unitLabel = unitsController.units.GetUnitsLabel

        txtBxUnitLabel.Text = unitLabel
    End Sub

    ''' <summary>
    ''' Updates the list of available schema items for selection in a given combo box within a given set of combo boxes. 
    ''' This is dependent on current selections within the set.
    ''' </summary>
    ''' <param name="p_isNumerator">If true, then the new lists to be generated based on state are for the numerator controls. Else, it is for the denominator controls.</param>
    ''' <remarks></remarks>
    Private Sub UpdateComboBoxLists(ByVal p_isNumerator As Boolean)
        If p_isNumerator Then
            'Numerator Controls
            cmbBox_r0c0.ItemsSource = CurrentSchemaListNumerator(cmbBox_r0c0)
            cmbBox_r0c1.ItemsSource = CurrentSchemaListNumerator(cmbBox_r0c1)
            cmbBox_r0c2.ItemsSource = CurrentSchemaListNumerator(cmbBox_r0c2)
            cmbBox_r0c3.ItemsSource = CurrentSchemaListNumerator(cmbBox_r0c3)
        Else
            'Denominator Controls
            cmbBox_r1c0.ItemsSource = CurrentSchemaListDenominator(cmbBox_r1c0)
            cmbBox_r1c1.ItemsSource = CurrentSchemaListDenominator(cmbBox_r1c1)
            cmbBox_r1c2.ItemsSource = CurrentSchemaListDenominator(cmbBox_r1c2)
            cmbBox_r1c3.ItemsSource = CurrentSchemaListDenominator(cmbBox_r1c3)
        End If
    End Sub

    ''' <summary>
    ''' Clears the combo box selections of all schema combo boxes to ensure a fresh starting state.
    ''' Note that this also clears the units object.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearComboBoxSelections()

        'Numerator Controls
        If cmbBox_r0c0.Items.Count > 0 Then cmbBox_r0c0.SelectedIndex = 0
        If cmbBox_r0c1.Items.Count > 0 Then cmbBox_r0c1.SelectedIndex = 0
        If cmbBox_r0c2.Items.Count > 0 Then cmbBox_r0c2.SelectedIndex = 0
        If cmbBox_r0c3.Items.Count > 0 Then cmbBox_r0c3.SelectedIndex = 0

        'Denominator Controls
        If cmbBox_r1c0.Items.Count > 0 Then cmbBox_r1c0.SelectedIndex = 0
        If cmbBox_r1c1.Items.Count > 0 Then cmbBox_r1c1.SelectedIndex = 0
        If cmbBox_r1c2.Items.Count > 0 Then cmbBox_r1c2.SelectedIndex = 0
        If cmbBox_r1c3.Items.Count > 0 Then cmbBox_r1c3.SelectedIndex = 0
    End Sub

    Private Function CurrentSchemaListNumerator(ByVal p_cmbBox As ComboBox) As List(Of String)
        Dim newSchemaList As New List(Of String)
        newSchemaList = GetEnumDescriptionList(eUnitType.none)

        'Remove all items that are currently selected, except for the specified combo box
        If (Not (p_cmbBox Is cmbBox_r0c0) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r0c0.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r0c0.SelectedItem))
        If (Not (p_cmbBox Is cmbBox_r0c1) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r0c1.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r0c1.SelectedItem))
        If (Not (p_cmbBox Is cmbBox_r0c2) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r0c2.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r0c2.SelectedItem))
        If (Not (p_cmbBox Is cmbBox_r0c3) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r0c3.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r0c3.SelectedItem))

        Return newSchemaList
    End Function
    Private Function CurrentSchemaListDenominator(ByVal p_cmbBox As ComboBox) As List(Of String)
        Dim newSchemaList As New List(Of String)
        newSchemaList = GetEnumDescriptionList(eUnitType.none)

        'Remove all items that are currently selected, except for the specified combo box
        If (Not (p_cmbBox Is cmbBox_r1c0) AndAlso
           Not String.IsNullOrEmpty(CStr(cmbBox_r1c0.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r1c0.SelectedItem))
        If (Not (p_cmbBox Is cmbBox_r1c1) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r1c1.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r1c1.SelectedItem))
        If (Not (p_cmbBox Is cmbBox_r1c2) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r1c2.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r1c2.SelectedItem))
        If (Not (p_cmbBox Is cmbBox_r1c3) AndAlso
            Not String.IsNullOrEmpty(CStr(cmbBox_r1c3.SelectedItem))) Then newSchemaList.Remove(CStr(cmbBox_r1c3.SelectedItem))

        Return newSchemaList
    End Function

    ''' <summary>
    ''' Operations to be done whenever a combo box for selection is changed for unit schema items.
    ''' </summary>
    ''' <param name="p_cmbBox">The combo box containing the unit schema selection.</param>
    ''' <param name="p_txtBxPower">The power text box corresponding to the schema combo box.</param>
    ''' <param name="p_cmbBoxValue">The combo box containing the unit value corresponding to the schema combo box.</param>
    ''' <param name="p_unitIndex">The index of the unit corresponding to the value selection.</param>
    ''' <param name="p_isNumerator">If true, the index is for the unit object in the numerator position.</param>
    ''' <remarks></remarks>
    Private Sub cmbBoxSchemaSelectionChanged(ByRef p_cmbBox As ComboBox,
                                            ByRef p_txtBxPower As TextBox,
                                            ByRef p_cmbBoxValue As ComboBox,
                                            ByVal p_unitIndex As Integer,
                                            ByVal p_isNumerator As Boolean)
        Dim unitTemp As New cUnit

        'Update unit power
        p_txtBxPower.Text = ""
        If Not String.IsNullOrEmpty(CStr(p_cmbBox.SelectedValue)) Then
            p_txtBxPower.IsEnabled = True
        Else
            p_txtBxPower.IsEnabled = False
        End If

        'Update unit object & values
        unitTemp.type = ConvertStringToEnumByDescription(Of eUnitType)(CStr(p_cmbBox.SelectedValue))
        unitTemp.numerator = p_isNumerator

        ' Assign default unit name if possible
        unitTemp.SetUnitName(GetDefaultName(unitTemp))

        ''Set unit values list & text (if applicable)
        p_cmbBoxValue.ItemsSource = unitTemp.unitsList
        If Not String.IsNullOrWhiteSpace(unitTemp.name) Then
            SetComboBoxSelection(p_cmbBoxValue, unitTemp.name, p_useIndex:=True)
        End If

        ''Update units object for new unit
        unitsController.units.RemoveUnit(p_unitIndex, p_isNumerator)
        If Not unitTemp.type = eUnitType.none Then
            unitsController.units.AddUnit(unitTemp, p_unitIndex)
        End If

        'Update Preview
        txtBxSchemaPreview.Text = unitsController.units.GetSchemaLabel
        txtBxValuesPreview.Text = unitsController.units.GetUnitsLabel
        UpdateUnitsLabelPreview()
        Me.UpdateLayout()

        UpdateComboBoxLists(p_isNumerator)
        SetShorthandUnitsAvailable()
    End Sub

    ''' <summary>
    ''' Return default unit name corresponding to the unit type of the unit provided.
    ''' </summary>
    ''' <param name="p_unit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDefaultName(ByVal p_unit As cUnit) As String
        If (_defaultUnitNames IsNot Nothing AndAlso
            _defaultUnitNames.Count > 0) Then
            For Each unitCtrl As cUnitsController In _defaultUnitNames
                For Each unit As cUnit In unitCtrl.units.unitsAll
                    If p_unit.type = unit.type Then
                        Return unit.name
                    End If
                Next
            Next
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Operations to be done whenever the text is changed in a 'power' text box control within the schema controls.
    ''' </summary>
    ''' <param name="p_txtBxPower">The power text box that has been changed.</param>
    ''' <param name="p_label">Label control displaying the power in the values controls, which corresponds with the text box control.</param>
    ''' <param name="p_unitIndex">The index of the unit corresponding to the power change.</param>
    ''' <param name="p_isNumerator">>If true, the index is for the unit object in the numerator position.</param>
    ''' <remarks></remarks>
    Private Sub txtBxPowerTextChanged(ByRef p_txtBxPower As TextBox,
                                  ByRef p_label As Label,
                                  ByVal p_unitIndex As Integer,
                                  ByVal p_isNumerator As Boolean)
        Dim unitsCollection As List(Of cUnit) = GetUnitsCollection(p_isNumerator)

        'Validate
        p_txtBxPower.Text = ValidatePowerText(p_txtBxPower.Text)

        'Assign values to class       
        If unitsCollection.Count > p_unitIndex Then unitsCollection(p_unitIndex).power = p_txtBxPower.Text

        'Cascade result to label
        If Not p_txtBxPower.Text = "1" Then
            p_label.Content = p_txtBxPower.Text
        Else
            p_label.Content = ""
        End If

        'Update Preview
        txtBxSchemaPreview.Text = unitsController.units.GetSchemaLabel
        txtBxValuesPreview.Text = unitsController.units.GetUnitsLabel
        UpdateUnitsLabelPreview()
        Me.UpdateLayout()

        SetShorthandUnitsAvailable()
    End Sub

    ''' <summary>
    ''' Validates that the string is only positive numeric, possibly with one a divisor.
    ''' </summary>
    ''' <param name="p_powerText">Text to check and modify. Invalid characters are removed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePowerText(ByVal p_powerText As String) As String
        Dim slashIsPresent As Boolean = False
        Dim validatedText As String = ""

        For Each charString As Char In p_powerText
            If (charString = "-" OrElse
                (Not IsNumeric(charString) AndAlso Not charString = "/") OrElse
                (charString = "/" AndAlso slashIsPresent)) Then
                'Character not recorded
            ElseIf charString = "/" Then
                slashIsPresent = True
                validatedText &= charString
            Else
                validatedText &= charString
            End If
        Next

        Return validatedText
    End Function

    ''' <summary>
    ''' Operations to be done whenever a combo box for selection is changed for unit values.
    ''' </summary>
    ''' <param name="p_cmbBox">The combo box containing the unit value selection.</param>
    ''' <param name="p_unitIndex">The index of the unit corresponding to the value selection.</param>
    ''' <param name="p_isNumerator">If true, the index is for the unit object in the numerator position.</param>
    ''' <remarks></remarks>
    Private Sub cmbBoxValueSelectionChanged(ByRef p_cmbBox As ComboBox,
                                            ByVal p_unitIndex As Integer,
                                            ByVal p_isNumerator As Boolean)
        Dim unitsCollection As List(Of cUnit) = GetUnitsCollection(p_isNumerator)

        If unitsCollection.Count > p_unitIndex Then unitsCollection(p_unitIndex).SetUnitName(CStr(p_cmbBox.SelectedValue))

        'Update Preview
        txtBxValuesPreview.Text = unitsController.units.GetUnitsLabel
        UpdateUnitsLabelPreview()
        Me.UpdateLayout()
    End Sub

    ''' <summary>
    ''' Determines if there are shorthand units available to be selected and performs relevant form actions.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetShorthandUnitsAvailable() As Boolean
        If unitsController.IsShorthandTypesAvailable Then
            chkBxUseShorthandUnits.IsEnabled = True
            Return True
        Else
            chkBxUseShorthandUnits.IsEnabled = False
            Return False
        End If
    End Function

    ''' <summary>
    ''' Updates the controls for setting and displaying the schema and values of the units.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadSchemaAndValues()
        Dim isNumerator As Boolean

        If unitsController Is Nothing Then Exit Sub

        'Load Numerators
        isNumerator = True
        UpdateComboBoxLists(isNumerator)
        LoadSchemaAndValues(0, isNumerator, cmbBox_r0c0, txtBxPower_r0c0, cmbBoxValues_r0c0, stackPanelSchema_r0c0, stackPanelValue_r0c0, False)
        LoadSchemaAndValues(1, isNumerator, cmbBox_r0c1, txtBxPower_r0c1, cmbBoxValues_r0c1, stackPanelSchema_r0c1, stackPanelValue_r0c1)
        LoadSchemaAndValues(2, isNumerator, cmbBox_r0c2, txtBxPower_r0c2, cmbBoxValues_r0c2, stackPanelSchema_r0c2, stackPanelValue_r0c2)
        LoadSchemaAndValues(3, isNumerator, cmbBox_r0c3, txtBxPower_r0c3, cmbBoxValues_r0c3, stackPanelSchema_r0c3, stackPanelValue_r0c3)

        'Load Denominators
        isNumerator = False
        UpdateComboBoxLists(isNumerator)
        LoadSchemaAndValues(0, isNumerator, cmbBox_r1c0, txtBxPower_r1c0, cmbBoxValues_r1c0, stackPanelSchema_r1c0, stackPanelValue_r1c0)
        LoadSchemaAndValues(1, isNumerator, cmbBox_r1c1, txtBxPower_r1c1, cmbBoxValues_r1c1, stackPanelSchema_r1c1, stackPanelValue_r1c1)
        LoadSchemaAndValues(2, isNumerator, cmbBox_r1c2, txtBxPower_r1c2, cmbBoxValues_r1c2, stackPanelSchema_r1c2, stackPanelValue_r1c2)
        LoadSchemaAndValues(3, isNumerator, cmbBox_r1c3, txtBxPower_r1c3, cmbBoxValues_r1c3, stackPanelSchema_r1c3, stackPanelValue_r1c3)

        SetShorthandUnitsAvailable()
    End Sub

    ''' <summary>
    ''' Loads the class properties relating to the schema and values into the appropriate control.
    ''' </summary>
    ''' <param name="p_unitIndex">Index of the unit object in the collection of unit objects in either the numerator or denominator groups.</param>
    ''' <param name="p_unitNumerator">If true, the unit is in the numerator group.</param>
    ''' <param name="p_comboBoxSchema">Combo box for the schema 'type' property.</param>
    ''' <param name="p_txtBxPower">Text box for the power of the unit 'type' or 'value'.</param>
    ''' <param name="p_comboBoxValue">Combo box for the schema 'value' property.</param>
    ''' <param name="p_stackPanelSchema">Stack panel containing the controls for setting the units schema.</param>
    ''' <param name="p_stackPanelValue">Stack panel containing the controls for setting the units values.</param>
    ''' <param name="p_collapseStackPanels">For if there is no corresponding object to load values from. 
    ''' If true, the stack panels containing the corresponding controls will be collapsed.
    ''' If false, the corresponding controls will remain visible but will be cleared.
    ''' </param>
    ''' <remarks></remarks>
    Private Sub LoadSchemaAndValues(ByVal p_unitIndex As Integer,
                                    ByVal p_unitNumerator As Boolean,
                                    ByVal p_comboBoxSchema As ComboBox,
                                    ByVal p_txtBxPower As TextBox,
                                    ByVal p_comboBoxValue As ComboBox,
                                    ByVal p_stackPanelSchema As StackPanel,
                                    ByVal p_stackPanelValue As StackPanel,
                                    Optional ByVal p_collapseStackPanels As Boolean = True)
        Dim unit As cUnit
        Dim unitsCollection As List(Of cUnit) = GetUnitsCollection(p_unitNumerator)


        If unitsCollection.Count > p_unitIndex Then
            unit = unitsCollection(p_unitIndex)

            SetComboBoxSelection(p_comboBoxSchema, GetEnumDescription(unit.type))
            If Not unit.power = "1" Then p_txtBxPower.Text = unit.power

            ' Re-assign unit object as changes related to schema selection are not updated to it. e.g. unit name.
            unit = GetUnitsCollection(p_unitNumerator)(p_unitIndex)

            ' Case sensitivity maintained for compound units starting in 'm' in case it is mN vs. MN.
            ' Case sensitivity not maintained otherwise since units may be inconsistent in capitalization.
            If (unit.name.Length > 1 AndAlso Mid(unit.name, 1, 1).ToUpper = "M") Then
                SetComboBoxSelection(p_comboBoxValue, unit.name, p_useIndex:=True)
            Else
                SetComboBoxSelection(p_comboBoxValue, unit.name, p_useIndex:=True, p_caseSensitive:=False)
            End If

            p_stackPanelSchema.Visibility = Windows.Visibility.Visible
            p_stackPanelValue.Visibility = Windows.Visibility.Visible
        Else
            If p_collapseStackPanels Then   'Collapse panels if they are visible
                If p_stackPanelSchema.Visibility = Windows.Visibility.Visible Then p_stackPanelSchema.Visibility = Windows.Visibility.Collapsed
                If p_stackPanelValue.Visibility = Windows.Visibility.Visible Then p_stackPanelValue.Visibility = Windows.Visibility.Collapsed
            Else    'This control should not be collapsed, just reset to blank.
                p_comboBoxSchema.SelectedIndex = 0
                p_txtBxPower.Text = ""
                p_comboBoxValue.SelectedIndex = 0
            End If
        End If
    End Sub

    ''' <summary>
    ''' Returns a collection of units objects based on the numerator designation.
    ''' </summary>
    ''' <param name="p_isNumerator">IF true, returns the numerator units. Else, returns the denominator units.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitsCollection(ByVal p_isNumerator As Boolean) As List(Of cUnit)
        If p_isNumerator Then
            Return unitsController.units.unitsNumerator
        Else
            Return unitsController.units.unitsDenominator
        End If
    End Function

    ''' <summary>
    ''' Returns the list of schema entries as a single single that is comma delimited.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseSchemaListToString() As String
        Dim listSchema As List(Of String) = unitsController.units.GetSchemaList
        Dim listSchemaString As String = ""

        For Each item As String In listSchema
            If String.IsNullOrEmpty(listSchemaString) Then
                listSchemaString = item
            Else
                listSchemaString &= ", " & item
            End If
        Next

        Return listSchemaString
    End Function
    ''' <summary>
    ''' Returns the list of unit value entries as a single single that is comma delimited.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseValuesListToString() As String
        Dim listValues As List(Of String) = unitsController.units.GetUnitsList
        Dim listValuesString As String = ""

        For Each item As String In listValues
            If String.IsNullOrEmpty(listValuesString) Then
                listValuesString = item
            Else
                listValuesString &= ", " & item
            End If
        Next

        Return listValuesString
    End Function

#End Region
End Class
