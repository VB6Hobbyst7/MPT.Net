Option Strict On
Option Explicit On

Imports System.Drawing

Imports MPT.Reporting
Imports MPT.Verification.StructValidation

Public Class frmLongListPrompt
#Region "Variables"
    Private _promptActionSet As eMessageActionSets
    Private _iconType As MessageBoxImage
#End Region

#Region "Properties"
    Public Property promptAction As eMessageActions

    Public Property myPromptTitle As String
    Public Property myPromptMessage As String
    Public Property myPromptFooter As String
    Public Property myPromptList As String
#End Region


#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    ''' <summary>
    ''' Generates a form similar to standard MessageBox forms, but with a scrollable center section for long lists.
    ''' Always appears on top.
    ''' </summary>
    ''' <param name="promptActionSet">Sets of actions for common prompts. To be used for custom forms in the program.</param>
    ''' <param name="promptTitle">Message that appears in the title bar of the form.</param>
    ''' <param name="promptMessage">Message that appears at the top of the form.</param>
    ''' <param name="promptFooter">Message that appears at the bottom of the form.</param>
    ''' <param name="promptList">Message that becomes scrollable once beyond a certain size.</param>
    ''' <param name="iconType">Icon to display on the form.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal promptActionSet As eMessageActionSets,
                   ByVal promptTitle As String,
                   ByVal promptMessage As String,
                   ByVal promptFooter As String,
                   Optional ByVal promptList As String = "",
                   Optional ByVal iconType As MessageBoxImage = MessageBoxImage.None)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(promptActionSet, promptTitle, promptMessage, promptFooter, promptList, iconType)
        InitializeControls()
        Me.Topmost = True
    End Sub

    Private Sub InitializeData(ByVal promptActionSet As eMessageActionSets,
                               ByVal promptTitle As String,
                               ByVal promptMessage As String,
                               ByVal promptFooter As String,
                               ByVal promptList As String,
                               ByVal iconType As MessageBoxImage)
        _promptActionSet = promptActionSet
        _iconType = iconType

        myPromptTitle = promptTitle
        myPromptMessage = promptMessage
        myPromptFooter = promptFooter
        myPromptList = promptList

    End Sub

    Private Sub InitializeControls()
        Dim icon As IntPtr

        Me.MaxHeight = System.Windows.SystemParameters.WorkArea.Height
        Me.MaxWidth = System.Windows.SystemParameters.WorkArea.Width


        Me.Title = myPromptTitle
        Me.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

        txtBxHeader.Text = myPromptMessage
        txtBxFooter.Text = myPromptFooter
        txtBxMain.Text = myPromptList

        'Set Button Views
        btnAbort.Visibility = Windows.Visibility.Collapsed
        btnCancel.Visibility = Windows.Visibility.Collapsed
        btnIgnore.Visibility = Windows.Visibility.Collapsed
        btnNo.Visibility = Windows.Visibility.Collapsed
        btnOk.Visibility = Windows.Visibility.Collapsed
        btnRetry.Visibility = Windows.Visibility.Collapsed
        btnYes.Visibility = Windows.Visibility.Collapsed

        Select Case _promptActionSet
            Case eMessageActionSets.AbortRetryIgnore
                btnAbort.Visibility = Windows.Visibility.Visible
                btnRetry.Visibility = Windows.Visibility.Visible
                btnIgnore.Visibility = Windows.Visibility.Visible
            Case eMessageActionSets.OkCancel
                btnOk.Visibility = Windows.Visibility.Visible
                btnCancel.Visibility = Windows.Visibility.Visible
            Case eMessageActionSets.OkOnly
                btnOk.Visibility = Windows.Visibility.Visible
            Case eMessageActionSets.RetryCancel
                btnRetry.Visibility = Windows.Visibility.Visible
                btnCancel.Visibility = Windows.Visibility.Visible
            Case eMessageActionSets.YesNo
                btnYes.Visibility = Windows.Visibility.Visible
                btnNo.Visibility = Windows.Visibility.Visible
            Case eMessageActionSets.YesNoCancel
                btnYes.Visibility = Windows.Visibility.Visible
                btnNo.Visibility = Windows.Visibility.Visible
                btnCancel.Visibility = Windows.Visibility.Visible
        End Select

        'Set message box icon
        Select Case _iconType
            Case MessageBoxImage.Asterisk : icon = SystemIcons.Asterisk.Handle
            Case MessageBoxImage.Error : icon = SystemIcons.Error.Handle
            Case MessageBoxImage.Exclamation : icon = SystemIcons.Exclamation.Handle
            Case MessageBoxImage.Hand : icon = SystemIcons.Hand.Handle
            Case MessageBoxImage.Information : icon = SystemIcons.Information.Handle
            Case MessageBoxImage.Question : icon = SystemIcons.Question.Handle
            Case MessageBoxImage.None
        End Select

        If Not IntPtrIsEmpty(icon) Then imgIcon.Source = Interop.Imaging.CreateBitmapSourceFromHIcon(icon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
        If String.IsNullOrEmpty(myPromptList) Then txtBxMain.Visibility = Windows.Visibility.Collapsed

        txtBxMain.MaxHeight = Me.MaxHeight - 3 * 10 - 32 - rowButton.ActualHeight - rowFooter.ActualHeight - rowHeader.ActualHeight

    End Sub
#End Region


#Region "Form Controls"
    Private Sub btnYes_Click(sender As Object, e As RoutedEventArgs) Handles btnYes.Click
        promptAction = eMessageActions.Yes
        Me.Close()
    End Sub
    Private Sub btnNo_Click(sender As Object, e As RoutedEventArgs) Handles btnNo.Click
        promptAction = eMessageActions.No
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        promptAction = eMessageActions.Cancel
        Me.Close()
    End Sub
    Private Sub btnOk_Click(sender As Object, e As RoutedEventArgs) Handles btnOk.Click
        promptAction = eMessageActions.OK
        Me.Close()
    End Sub
    Private Sub btnAbort_Click(sender As Object, e As RoutedEventArgs) Handles btnAbort.Click
        promptAction = eMessageActions.Abort
        Me.Close()
    End Sub
    Private Sub btnRetry_Click(sender As Object, e As RoutedEventArgs) Handles btnRetry.Click
        promptAction = eMessageActions.Retry
        Me.Close()
    End Sub
    Private Sub btnIgnore_Click(sender As Object, e As RoutedEventArgs) Handles btnIgnore.Click
        promptAction = eMessageActions.Ignore
        Me.Close()
    End Sub
#End Region


#Region "Methods"
    '=== Dynamically sets the maximum height of the textBox so that scrollbars appear if not all rows are visible
    ''' <summary>
    ''' Dynamically sets the maximum height of the textBox so that scrollbars appear if the window is made too small to display all rows.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        'txtBxMain.MaxHeight = Me.MaxHeight - 3 * 10 - 32 - rowButton.ActualHeight - rowFooter.ActualHeight - rowHeader.ActualHeight
        'txtBxMain.Height = rowBody.ActualHeight ' - 3 * 10 - 32 - rowButton.ActualHeight - rowFooter.ActualHeight - rowHeader.ActualHeight
        txtBxMain.Width = columnBody.ActualWidth

        rowButton.Height = New GridLength(rowButton.ActualHeight)
        rowFooter.Height = New GridLength(rowFooter.ActualHeight)
        rowHeader.Height = New GridLength(rowHeader.ActualHeight)

        Me.MinWidth = columnSide.ActualWidth + _
                    btnYes.Width + btnYes.Margin.Left + btnYes.Margin.Right + _
                    btnNo.Width + btnNo.Margin.Left + btnNo.Margin.Right + _
                    btnOk.Width + btnOk.Margin.Left + btnOk.Margin.Right + _
                    btnCancel.Width + btnCancel.Margin.Left + btnCancel.Margin.Right + _
                    btnAbort.Width + btnAbort.Margin.Left + btnAbort.Margin.Right + _
                    btnRetry.Width + btnRetry.Margin.Left + btnRetry.Margin.Right + _
                    btnIgnore.Width + btnIgnore.Margin.Left + btnIgnore.Margin.Right
        Me.MinHeight = 3 * 10 + 32 + rowButton.ActualHeight + rowFooter.ActualHeight + rowHeader.ActualHeight


    End Sub
#End Region
End Class
