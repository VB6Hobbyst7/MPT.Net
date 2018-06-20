Option Strict On
Option Explicit On

Imports MPT.Reporting

''' <summary>
''' Creates a custom message box. 
''' This message box can display lists.
''' </summary>
''' <remarks></remarks>
Public Class MessageBoxLong

    ''' <summary>
    ''' Displays the form by which the user is informed and able to select an action. The form can diplay list content.
    ''' </summary>
    ''' <param name="promptActionSet"></param>
    ''' <param name="promptTitle"></param>
    ''' <param name="promptMessage"></param>
    ''' <param name="promptFooter"></param>
    ''' <param name="promptList"></param>
    ''' <param name="iconType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Show(ByVal promptActionSet As eMessageActionSets,
                         ByVal promptTitle As String,
                         ByVal promptMessage As String,
                         ByVal promptFooter As String,
                         Optional ByVal promptList As String = "",
                         Optional ByVal iconType As MessageBoxImage = MessageBoxImage.None) As eMessageActions
        Dim windowPrompt As New frmLongListPrompt(promptActionSet, promptTitle, promptMessage, promptFooter, promptList, iconType)

        windowPrompt.ShowDialog()

        Return windowPrompt.promptAction
    End Function
End Class
