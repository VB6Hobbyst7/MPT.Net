Option Strict On
Option Explicit On

Imports ProgressiveCollapse.cEnumerations

''' <summary>
''' Form for setting the various properties related to tracking a progressive collapse.
''' </summary>
''' <remarks></remarks>
Public Class frmTrackProgCol
#Region "Variables"
    Dim timeStepScaleFactorMin As Double = 0.0001
    Dim timeStepMinMin As Double = 1 * 10 ^ -12
#End Region

#Region "Properties"
    ''' <summary>
    ''' Parameter that tracks if the propagation is to be limited, which affects what controls should be available.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property limitPropagation As Boolean
    ''' <summary>
    ''' Limit to number of iterations for propagating events.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property itNumMax As Integer
    ''' <summary>
    ''' Maximum number of frames allowed to fail in a single time step before continuing on to the next iteration.
    ''' If this is exceeded, the current iteration will be re-run with smaller time steps until the number of simultaneous failed frames is below the limit.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property failureFrameLimit As Integer
    ''' <summary>
    ''' Initial factor by which the time step is reduced if the time steps are refined in order to limit the number of simultaneous failures.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeStepScaleFactorInitial As Double
    ''' <summary>
    ''' Factor by which the time step is reduced if after the initial factor is applied, the time steps are refined furhther in order to limit the number of simultaneous failures.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeStepScaleFactor As Double
    ''' <summary>
    ''' Smallest time step value allowed. If time step refinement reaches this level, the next iteration is begun even if the  failure limit is still exceeded.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeStepMin As Double
#End Region

#Region "Initialization"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetDefaults()
        SetButtons()
        SetToolTips()
    End Sub

    Sub SetDefaults()
        With ProgCol
            limitPropagation = .limitPropagation
            itNumMax = .itNumMax
            failureFrameLimit = .failureFrameLimit
            timeStepScaleFactorInitial = .timeStepScaleFactorInitial
            timeStepScaleFactor = .timeStepScaleFactor
            timeStepMin = .timeStepMin
        End With
    End Sub

    Sub SetButtons()
        'Checkboxes
        CheckBox_LimitPropagation.Checked = ProgCol.limitPropagation

        'Text Fields
        TextBox_ItNumMax.Text = CStr(itNumMax)
        TextBox_FailureFrameLimit.Text = CStr(failureFrameLimit)
        TextBox_TimeStepScaleFactorInitial.Text = CStr(timeStepScaleFactorInitial)
        TextBox_TimeStepScaleFactor.Text = CStr(timeStepScaleFactor)
        TextBox_TimeStepMin.Text = CStr(timeStepMin)
    End Sub

    ''' <summary>
    ''' Sets the tooltips for the form.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetToolTips()
        Me.ToolTip1.SetToolTip(Me.CheckBox_LimitPropagation, ttLimitPropagation)
        Me.ToolTip1.SetToolTip(Me.TextBox_ItNumMax, ttMaxFailureEvents)
        Me.ToolTip1.SetToolTip(Me.TextBox_FailureFrameLimit, ttMaxNumFailedFramePerEvent)
        Me.ToolTip1.SetToolTip(Me.TextBox_TimeStepScaleFactorInitial, ttInitialReductionFactor)
        Me.ToolTip1.SetToolTip(Me.TextBox_TimeStepScaleFactor, ttIteratedReductionFactor)
        Me.ToolTip1.SetToolTip(Me.TextBox_TimeStepMin, ttMinStepSize)
    End Sub
#End Region

#Region "Control Methods"
    'Buttons
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'Update class values
        With ProgCol
            .limitPropagation = limitPropagation
            .itNumMax = itNumMax
            .failureFrameLimit = failureFrameLimit
            .timeStepScaleFactorInitial = timeStepScaleFactorInitial
            .timeStepScaleFactor = timeStepScaleFactor
            .timeStepMin = timeStepMin
        End With

        'Close
        Me.Close()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'Checkboxes
    Private Sub CheckBox_LimitPropagation_CheckedChanged(sender As Object, e As EventArgs)
        If CheckBox_LimitPropagation.Checked = True Then
            limitPropagation = True

            TextBox_ItNumMax.Enabled = True
            TextBox_FailureFrameLimit.Enabled = True
            TextBox_TimeStepScaleFactor.Enabled = True
        Else
            limitPropagation = False

            TextBox_ItNumMax.Enabled = False
            TextBox_FailureFrameLimit.Enabled = False
            TextBox_TimeStepScaleFactor.Enabled = False
        End If
    End Sub

    'Text Fields
    Private Sub TextBox_ItNumMax_Leave(sender As Object, e As EventArgs) Handles TextBox_ItNumMax.Leave
        If CheckValidEntryRange(TextBox_ItNumMax.Text, 1, 9999, True, msgAlertInteg, msgAlertInteg, msgAlertInteg) Then
            itNumMax = CInt(TextBox_ItNumMax.Text)
        Else
            TextBox_ItNumMax.Text = CStr(itNumMax)
        End If
    End Sub
    Private Sub TextBox_FailureFrameLimit_Leave(sender As Object, e As EventArgs) Handles TextBox_FailureFrameLimit.Leave
        If CheckValidEntryRange(TextBox_FailureFrameLimit.Text, 1, 9999, True, msgAlertInteg, msgAlertInteg, msgAlertInteg) Then
            failureFrameLimit = CInt(TextBox_FailureFrameLimit.Text)
        Else
            TextBox_FailureFrameLimit.Text = CStr(failureFrameLimit)
        End If
    End Sub
    Private Sub TextBox_TimeStepScaleFactorInitial_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeStepScaleFactorInitial.Leave
        If CheckValidEntryRange(TextBox_TimeStepScaleFactorInitial.Text, timeStepScaleFactorMin, 1, False, msgAlertInteg, msgAlertInteg, msgAlertInteg) Then
            timeStepScaleFactorInitial = CInt(TextBox_TimeStepScaleFactorInitial.Text)
        Else
            TextBox_TimeStepScaleFactorInitial.Text = CStr(timeStepScaleFactorInitial)
        End If
    End Sub
    Private Sub TextBox_TimeStepScaleFactor_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeStepScaleFactor.Leave
        If CheckValidEntryRange(TextBox_TimeStepScaleFactor.Text, timeStepScaleFactorMin, 1, False, msgAlertInteg, msgAlertInteg, msgAlertInteg) Then
            timeStepScaleFactor = CInt(TextBox_TimeStepScaleFactor.Text)
        Else
            TextBox_TimeStepScaleFactor.Text = CStr(timeStepScaleFactor)
        End If
    End Sub
    Private Sub TextBox_TimeStepMin_Leave(sender As Object, e As EventArgs) Handles TextBox_TimeStepMin.Leave
        If CheckValidEntryRange(TextBox_TimeStepMin.Text, timeStepMinMin, 1, False, msgAlertInteg, msgAlertInteg, msgAlertInteg) Then
            timeStepMin = CInt(TextBox_TimeStepMin.Text)
        Else
            TextBox_TimeStepMin.Text = CStr(timeStepMin)
        End If
    End Sub
#End Region


End Class