Option Strict On
Option Explicit On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If
Imports ProgressiveCollapse.cEnumerations

''' <summary>
''' Form for properties related to material type, such as ductility limitations and overstrength values.
''' </summary>
''' <remarks></remarks>
Public Class frmMaterialTypeParameters
#Region "Variables"
    Dim defaultOverStrength As Double
    Dim myDesignCode As cDesignCode
    Dim myOverStrengthUser As Double
    Dim myOverStrengthType As eOverStrength
#End Region
    
#Region "Initialization"
    ''' <summary>
    ''' Initializes new form for setting design code parameters. No values are populated from this sub.
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    ''' <summary>
    ''' Initializes new form for setting design code parameters.
    ''' </summary>
    ''' <param name="p_CodeType">Type of code, by material.</param>
    ''' <remarks></remarks>
    Sub New(ByVal p_CodeType As eCodeType)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(p_CodeType)
        SetFormControls()
        SetToolTips()
    End Sub


    Sub InitializeData(ByVal p_codeType As eCodeType)


        Me.Text = "Material Type Parameters - " & GetEnumDescription(p_codeType)

        myDesignCode = ProgCol.designCodes.GetDesignCodeByType(p_codeType)

    End Sub

    ''' <summary>
    ''' Sets the tooltips for the form.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetToolTips()
        Me.ToolTip1.SetToolTip(Me.GrpBxStrengthCriteria, ttStrengthCriteria)
        Me.ToolTip1.SetToolTip(Me.ComboBox_CodesList, ttDesignCode)
        Me.ToolTip1.SetToolTip(Me.CheckBox_UseSeismic, ttUseSeismicCode)
        Me.ToolTip1.SetToolTip(Me.GrpBxDuctilityCriteria, ttDuctilityCriteria)
    End Sub

    Sub SetFormControls()
        With myDesignCode
            CheckBox_UseSeismic.Checked = .useSeismic
            chkBxHingeState.Checked = .useHingeState
            chkBxAccptCriteria.Checked = .useAccptCriteria

            If .overStrengthType = eOverStrength.None Then btnRad_OverStrengthNone.Checked = True
            If .overStrengthType = eOverStrength.CodeDefault Then btnRad_OverStrengthCodeDefault.Checked = True
            If .overStrengthType = eOverStrength.UserSpecified Then btnRad_OverStrengthUserSpecified.Checked = True

            TextBox_TableName.Text = ProgCol.filePathTable

            TextBox_OverStrength.Text = CStr(.overStrengthUser)
        End With

        SetComboBoxes()
    End Sub

    ''' <summary>
    ''' Sets the list of options for the various combo boxes.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetComboBoxes()
        Dim hingeStateList As New List(Of String)
        Dim hingeAccptCriteria As New List(Of String)

        With hingeStateList
            .Add(GetEnumDescription(eHingeState.toB))
            .Add(GetEnumDescription(eHingeState.toC))
            .Add(GetEnumDescription(eHingeState.toD))
            .Add(GetEnumDescription(eHingeState.toE))
            .Add(GetEnumDescription(eHingeState.toEPlus))
        End With

        With hingeAccptCriteria
            .Add(GetEnumDescription(eHingeAcceptanceCriteria.toIO))
            .Add(GetEnumDescription(eHingeAcceptanceCriteria.toLS))
            .Add(GetEnumDescription(eHingeAcceptanceCriteria.toCP))
            .Add(GetEnumDescription(eHingeAcceptanceCriteria.toCPPlus))
        End With

        With myDesignCode
            ComboBox_CodesList.DataSource = .codeList
            ComboBox_CodesList.SelectedItem = .codeList.Item(0)

            ComboBox_HingeState.DataSource = hingeStateList
            ComboBox_HingeState.SelectedItem = .hingeState
            ComboBox_HingeState.Enabled = .useHingeState

            ComboBox_AccptCriteria.DataSource = hingeAccptCriteria
            ComboBox_AccptCriteria.SelectedItem = .accptCriteria
            ComboBox_AccptCriteria.Enabled = .useAccptCriteria
        End With
    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        With myDesignCode
            'Strength Parameters
            .useSeismic = CheckBox_UseSeismic.Checked
            .setCodeName(.codeType, .codeName)
            .setDesignStepByStep(.codeType, .codeName)  'For next iteration, design will be checked for each time step to locate failure.
            .codeName = CStr(ComboBox_CodesList.SelectedItem)
            .overStrengthUser = myOverStrengthUser
            .overStrengthType = myOverStrengthType


            'Ductility Parameters
            .useHingeState = chkBxHingeState.Checked
            .hingeState = ConvertStringtoEnumHingeState(CStr(ComboBox_HingeState.SelectedItem))

            .useAccptCriteria = chkBxAccptCriteria.Checked
            .accptCriteria = ConvertStringtoEnumHingeAcceptanceCriteria(CStr(ComboBox_AccptCriteria.SelectedItem))
        End With

        If TextBox_TableName.Text = "" Then
            ProgCol.filePathTable = Microsoft.VisualBasic.Left(ProgCol.filePathCurrent, Len(ProgCol.filePathCurrent) - 3) & "mdb"
        Else
            ProgCol.filePathTable = GetPathDirectoryStub(ProgCol.filePathCurrent) & "\" & TextBox_TableName.Text
        End If

        Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    '=== Textboxes
    Private Sub TextBox_U1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox_U2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox_U3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox_R1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox_R2_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox_R3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox_TableName_TextChanged(sender As Object, e As EventArgs) Handles TextBox_TableName.TextChanged

    End Sub

    Private Sub TextBox_OverStrength_TextChanged(sender As Object, e As EventArgs) Handles TextBox_OverStrength.TextChanged
        'Check for valid entry
        Dim myEntry As Double
        Dim myAlert As String = "Please enter a positive number."

        If Not IsNumeric(TextBox_OverStrength.Text) Then
            MsgBox(myAlert)
            TextBox_OverStrength.Text = CStr(defaultOverStrength)
            Exit Sub
        End If

        myEntry = CDbl(TextBox_OverStrength.Text)
        If myEntry < 0 Then
            MsgBox(myAlert)
            TextBox_OverStrength.Text = CStr(defaultOverStrength)
            Exit Sub
        End If

        myOverStrengthUser = myEntry
    End Sub

    '=== Checkboxes
    Private Sub chkBxAccptCriteria_CheckedChanged(sender As Object, e As EventArgs) Handles chkBxAccptCriteria.CheckedChanged
        If chkBxAccptCriteria.Checked Then
            ComboBox_AccptCriteria.Enabled = True
        Else
            ComboBox_AccptCriteria.Enabled = False
        End If
    End Sub
    Private Sub chkBxHingeState_CheckedChanged(sender As Object, e As EventArgs) Handles chkBxHingeState.CheckedChanged
        If chkBxHingeState.Checked Then
            ComboBox_HingeState.Enabled = True
        Else
            ComboBox_HingeState.Enabled = False
        End If
    End Sub

    '=== Radio Buttons
    Private Sub btnRad_None_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_OverStrengthNone.CheckedChanged
        TextBox_OverStrength.Enabled = False
        myOverStrengthType = eOverStrength.None
    End Sub
    Private Sub btnRad_CodeDefault_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_OverStrengthCodeDefault.CheckedChanged
        TextBox_OverStrength.Enabled = False
        myOverStrengthType = eOverStrength.CodeDefault
    End Sub
    Private Sub btnRad_UserSpecified_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_OverStrengthUserSpecified.CheckedChanged
        TextBox_OverStrength.Enabled = True
        myOverStrengthType = eOverStrength.UserSpecified
    End Sub

    '=== Combo Boxes
    Private Sub ComboBox_HingeState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_HingeState.SelectedIndexChanged

    End Sub

    Private Sub ComboBox_AccptCriteria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_AccptCriteria.SelectedIndexChanged

    End Sub
#End Region

#Region "Methods"
   



#End Region

End Class