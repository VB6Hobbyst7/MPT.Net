Option Strict On
Option Explicit On

''' <summary>
''' Contains helper classes, such as wait cursors.
''' </summary>
''' <remarks></remarks>
Public Class cHelper

End Class

Public Class cCursorWait
#Region "Variables"
    Private waitCursor As Boolean
#End Region

#Region "Initialization"
    Friend Sub New(Optional ByVal start As Boolean = True)
        If start Then StartCursor()
    End Sub
#End Region

#Region "Methods"
   
    ''' <summary>
    ''' Starts the wait cursor if one is not currently set. This status is recorded in the class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartCursor()
        If Not Mouse.OverrideCursor Is Cursors.Wait Then
            Mouse.OverrideCursor = Cursors.Wait
            waitCursor = True
        Else
            waitCursor = False
        End If

    End Sub

    ''' <summary>
    ''' If the class has started a wait cursor, the class will then end it.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub EndCursor()
        If waitCursor Then Mouse.OverrideCursor = Cursors.Arrow
    End Sub
#End Region

    

End Class