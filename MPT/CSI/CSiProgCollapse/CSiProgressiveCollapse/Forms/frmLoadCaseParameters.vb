Option Explicit On
Option Strict On

Imports ProgressiveCollapse.cEnumerations

''' <summary>
''' Form for setting overwrites for nonlinear time history load cases.
''' </summary>
''' <remarks></remarks>
Public Class frmLoadCaseParameters
#Region "Variables"
    Dim maxNumberInfinity As Double = 999999
    Dim minTimeStep As Double
    Dim minTimeVibrate As Double
#End Region

#Region "Properties"
    ''' <summary>
    ''' Geometric nonlinearity type to apply in the nonlinear direct integration time history analyses or initial static nonlinear case.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property geometricNonlinearity As eGeometricNonlinearity

    ''' <summary>
    ''' Time step size to use in the nonlinear direct integration time history analyses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeStepSize As Double
    ''' <summary>
    ''' Time over which a the reactions of a failed member are reduced.
    ''' Indirectly used in the nonlinear direct integration time history analyses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeUnloadMember As Double
    ''' <summary>
    ''' Time over which the structure is allowed to vibrate from a given failed member after the member's reactions have been unloaded.
    ''' Indirectly used in the nonlinear direct integration time history analyses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeVibrateMember As Double
    ''' <summary>
    ''' Total time over which the nonlinear direct integration time history case is to last.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeTotal As Double
    ''' <summary>
    ''' Number of steps used in order to achieve the total time using the specified time step size.
    ''' Used in the nonlinear direct integration time history analyses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeNumberSteps As Integer
    ''' <summary>
    ''' Ratio of the time the structure is allowed to vibrate vs. the time a failed frame element takes to unload its reactions from the structure.
    ''' Indirectly used in the nonlinear direct integration time history analyses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeVibrateUnloadRatio As Double

    ''' <summary>
    ''' Damping ratio to apply in the nonlinear direct integration time history analyses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property dampingRatio As Double
#End Region

#Region "Initialization"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetDefaults()
        SetButtons()
    End Sub

    Sub SetDefaults()
        With ProgCol
            geometricNonlinearity = .geometricNonlinearity

            timeStepSize = .timeStepSize
            timeUnloadMember = .timeUnloadMember
            timeVibrateMember = .timeVibrateMember
            timeTotal = .timeTotal
            timeNumberSteps = .timeNumberSteps
            timeVibrateUnloadRatio = .timeVibrateUnloadRatio

            dampingRatio = .dampingRatio
        End With

        minTimeVibrate = timeUnloadMember
    End Sub

    Sub SetButtons()
        'Radio Buttons
        Select Case geometricNonlinearity
            Case eGeometricNonlinearity.None : btnRad_None.Checked = True
            Case eGeometricNonlinearity.PDelta : btnRad_PDelta.Checked = True
            Case eGeometricNonlinearity.PDeltaLargeDisp : btnRad_PDeltaLargeDisp.Checked = True
        End Select

        btnRad_Ratio.Checked = True

        'Text Boxes
        TextBox_TimeStepSize.Text = CStr(timeStepSize)
        TextBox_TimeUnload.Text = CStr(timeUnloadMember)
        TextBox_TimeTotal.Text = CStr(timeTotal)
        InputTimeTotal()

        TextBox_DampingRatio.Text = CStr(dampingRatio)
    End Sub

#End Region

#Region "Control Methods"

    'Buttons
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'Assign properties to class
        With ProgCol
            .geometricNonlinearity = geometricNonlinearity

            .timeStepSize = timeStepSize
            .timeUnloadMember = timeUnloadMember
            .timeVibrateMember = timeVibrateMember
            .timeTotal = timeTotal
            .timeNumberSteps = timeNumberSteps
            .timeVibrateUnloadRatio = timeVibrateUnloadRatio

            .dampingRatio = dampingRatio
        End With

        'Close Form
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    'Radio Buttons
    '=Geometric Nonlinearity Group
    Private Sub btnRad_None_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_None.CheckedChanged
        geometricNonlinearity = eGeometricNonlinearity.None
    End Sub
    Private Sub btnRad_PDelta_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_PDelta.CheckedChanged
        geometricNonlinearity = eGeometricNonlinearity.PDelta
    End Sub
    Private Sub btnRad_PDeltaLargeDisp_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_PDeltaLargeDisp.CheckedChanged
        geometricNonlinearity = eGeometricNonlinearity.PDeltaLargeDisp
    End Sub


    'Time Parameters Group
    Private Sub btnRad_Ratio_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_Ratio.CheckedChanged
        TextBox_TimeRatios.Enabled = True
        TextBox_TimeFreeVibration.Enabled = False
        TextBox_TimeTotal.Enabled = False
        TextBox_TimeNumberStep.Enabled = False
    End Sub
    Private Sub btnRad_TimeFreeVibration_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_TimeFreeVibration.CheckedChanged
        TextBox_TimeRatios.Enabled = False
        TextBox_TimeFreeVibration.Enabled = True
        TextBox_TimeTotal.Enabled = False
        TextBox_TimeNumberStep.Enabled = False
    End Sub
    Private Sub btnRad_TimeTotal_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_TimeTotal.CheckedChanged
        TextBox_TimeRatios.Enabled = False
        TextBox_TimeFreeVibration.Enabled = False
        TextBox_TimeTotal.Enabled = True
        TextBox_TimeNumberStep.Enabled = False
    End Sub
    Private Sub btnRad_TimeNumOutputSteps_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_TimeNumOutputSteps.CheckedChanged
        TextBox_TimeRatios.Enabled = False
        TextBox_TimeFreeVibration.Enabled = False
        TextBox_TimeTotal.Enabled = False
        TextBox_TimeNumberStep.Enabled = True
    End Sub

    'Text Boxes
    Private Sub TextBox_TimeStepSize_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeStepSize.Leave
        'Time step cannot be greater than the time to unload the member
        Dim maxTimeStep As Double = timeUnloadMember

        'Check if value is valid, and handle exception
        If CheckValidEntryRange(TextBox_TimeStepSize.Text, 0, maxTimeStep, False, msgAlertRangeTimeStepSize, msgAlertNumberLC, msgAlertNumberLC) Then
            timeStepSize = CDbl(TextBox_TimeStepSize.Text)
            timeNumberSteps = CInt(timeTotal / timeStepSize)
            TextBox_TimeNumberStep.Text = CStr(timeNumberSteps)
            InputTimeNumberSteps()
        Else
            TextBox_TimeStepSize.Text = CStr(timeStepSize)
        End If
    End Sub
    Private Sub TextBox_TimeUnload_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeUnload.Leave
        'Time to unload the member cannot be less than the time step size or the free vibration time
        Dim maxTimeUnload As Double = timeVibrateMember
        minTimeStep = timeStepSize


        'Check if value is valid, and handle exception
        If CheckValidEntryRange(TextBox_TimeUnload.Text, minTimeStep, maxTimeUnload, False, msgAlertRangeUnload, msgAlertNumberLC, msgAlertNumberLC) Then
            timeUnloadMember = CDbl(TextBox_TimeUnload.Text)
            TextBox_TimeTotal.Text = CStr(timeStepSize * timeNumberSteps)
            InputTimeTotal()
        Else
            TextBox_TimeUnload.Text = CStr(timeUnloadMember)
        End If

        'Update minimum time for vibration
        minTimeVibrate = timeUnloadMember
    End Sub

    Private Sub TextBox_TimeRatios_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeRatios.Leave
        Dim timeRatioMin As Double = minTimeVibrate / timeUnloadMember
        Dim myAlert As String = msgAlertNumberLC & timeRatioMin & "."

        'Check if value is being input or autocalculated
        If btnRad_Ratio.Checked Then
            'Check if value is valid, and handle exception
            If CheckValidEntryRange(TextBox_TimeRatios.Text, timeRatioMin, maxNumberInfinity, False, myAlert, myAlert, myAlert) Then
                timeVibrateUnloadRatio = CDbl(TextBox_TimeRatios.Text)
                InputTimeRatio()
            Else
                TextBox_TimeRatios.Text = CStr(timeVibrateUnloadRatio)
            End If
        End If
    End Sub
    Private Sub TextBox_TimeFreeVibration_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeFreeVibration.Leave
        Dim myAlert As String = msgAlertNumberLC & minTimeVibrate & "."

        'Check if value is being input or autocalculated
        If btnRad_TimeFreeVibration.Checked Then
            'Check if value is valid, and handle exception
            If CheckValidEntryRange(TextBox_TimeFreeVibration.Text, minTimeVibrate, maxNumberInfinity, False, myAlert, myAlert, myAlert) Then
                timeVibrateMember = CDbl(TextBox_TimeFreeVibration.Text)
                InputTimeFreeVibration()
            Else
                TextBox_TimeFreeVibration.Text = CStr(timeVibrateMember)
            End If
        End If
    End Sub
    Private Sub TextBox_TimeTotal_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeTotal.Leave
        Dim timeTotalMin As Double = minTimeVibrate + timeUnloadMember
        Dim myAlert As String = msgAlertNumberLC & timeTotalMin & "."

        'Check if value is being input or autocalculated
        If btnRad_TimeTotal.Checked Then
            'Check if value is valid, and handle exception
            If CheckValidEntryRange(TextBox_TimeTotal.Text, timeTotalMin, maxNumberInfinity, False, myAlert, myAlert, myAlert) Then
                timeTotal = CDbl(TextBox_TimeTotal.Text)
                InputTimeTotal()
            Else
                TextBox_TimeTotal.Text = CStr(timeTotal)
            End If
        End If
    End Sub
    Private Sub TextBox_TimeNumberStep_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeNumberStep.Leave
        Dim timeNumStepMin As Double = (minTimeVibrate + timeUnloadMember) / timeStepSize
        Dim myAlert As String = msgAlertIntegerLC & timeNumStepMin & "."

        'Check if value is being input or autocalculated
        If btnRad_TimeNumOutputSteps.Checked Then
            'Check if value is valid, and handle exception
            If CheckValidEntryRange(TextBox_TimeNumberStep.Text, timeNumStepMin, maxNumberInfinity, True, myAlert, myAlert, myAlert) Then
                timeNumberSteps = CInt(TextBox_TimeNumberStep.Text)
                InputTimeNumberSteps()
            Else
                TextBox_TimeNumberStep.Text = CStr(timeNumberSteps)
            End If
        End If
    End Sub

    Private Sub TextBox_DampingRatio_Leave(sender As Object, e As EventArgs) Handles TextBox_DampingRatio.Leave
        Dim myAlertNumber As String = "Please enter a positive number between 0 and 1."

        'Check if value is valid, and handle exception
        If CheckValidEntryRange(TextBox_DampingRatio.Text, 0, 1, , myAlertNumber, myAlertNumber, myAlertNumber) Then
            dampingRatio = CDbl(TextBox_DampingRatio.Text)
        Else
            TextBox_DampingRatio.Text = CStr(dampingRatio)
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Calculates the other form parameters based on the specified input. Updates appropriate form fields to new value.
    ''' </summary>
    ''' <remarks></remarks>
    Sub InputTimeRatio()
        'Calculate properties
        timeVibrateMember = timeUnloadMember * timeVibrateUnloadRatio
        timeTotal = timeUnloadMember + timeVibrateMember
        timeNumberSteps = CInt(timeTotal / timeStepSize)

        'Update Form
        TextBox_TimeFreeVibration.Text = CStr(timeVibrateMember)
        TextBox_TimeTotal.Text = CStr(timeTotal)
        TextBox_TimeNumberStep.Text = CStr(timeNumberSteps)
    End Sub

    ''' <summary>
    ''' Calculates the other form parameters based on the specified input. Updates appropriate form fields to new value.
    ''' </summary>
    ''' <remarks></remarks>
    Sub InputTimeFreeVibration()
        'Calculate properties
        timeTotal = timeUnloadMember + timeVibrateMember
        timeNumberSteps = CInt(timeTotal / timeStepSize)
        timeVibrateUnloadRatio = timeVibrateMember / timeUnloadMember

        'Update Form
        TextBox_TimeRatios.Text = CStr(timeVibrateUnloadRatio)
        TextBox_TimeTotal.Text = CStr(timeTotal)
        TextBox_TimeNumberStep.Text = CStr(timeNumberSteps)
    End Sub

    ''' <summary>
    ''' Calculates the other form parameters based on the specified input. Updates appropriate form fields to new value.
    ''' </summary>
    ''' <remarks></remarks>
    Sub InputTimeTotal()
        'Calculate properties
        timeNumberSteps = CInt(timeTotal / timeStepSize)
        timeVibrateMember = timeTotal - timeUnloadMember
        timeVibrateUnloadRatio = timeVibrateMember / timeUnloadMember

        'Update Form
        TextBox_TimeRatios.Text = CStr(timeVibrateUnloadRatio)
        TextBox_TimeFreeVibration.Text = CStr(timeVibrateMember)
        TextBox_TimeNumberStep.Text = CStr(timeNumberSteps)
    End Sub

    ''' <summary>
    ''' Calculates the other form parameters based on the specified input. Updates appropriate form fields to new value.
    ''' </summary>
    ''' <remarks></remarks>
    Sub InputTimeNumberSteps()
        'Calculate properties
        timeTotal = timeStepSize * timeNumberSteps
        timeVibrateMember = timeTotal - timeUnloadMember
        timeVibrateUnloadRatio = timeVibrateMember / timeUnloadMember

        'Update Form
        TextBox_TimeRatios.Text = CStr(timeVibrateUnloadRatio)
        TextBox_TimeFreeVibration.Text = CStr(timeVibrateMember)
        TextBox_TimeTotal.Text = CStr(timeTotal)
    End Sub


#End Region

End Class