Option Strict On
Option Explicit On

''' <summary>
''' Converts form of units between those accepted by the units library and those that might be used by a CSi program.
''' </summary>
''' <remarks></remarks>
Public Class cProgramControlUnitsAdapter

    Private Sub New()
        ' This class is currentlt treated as a module.
    End Sub

    ''' <summary>
    ''' Reconciles any difference in force units from the units library to the form accepted by CSi programs.
    ''' </summary>
    ''' <param name="p_force">Force unit value to reconcile.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReconcileForceToProgram(ByVal p_force As String) As String
        Select Case p_force
            Case "kN" : p_force = "KN"
            Case "kgf" : p_force = "Kgf"
            Case "tf" : p_force = "Tonf"
            Case "tf" : p_force = "tonf"
            Case Else ' No action needed
        End Select
        Return p_force
    End Function

    ''' <summary>
    ''' Reconciles any difference in force units from a CSi program to the form accepted by the units library.
    ''' </summary>
    ''' <param name="p_force">Force unit value to reconcile.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReconcileForceFromProgram(ByVal p_force As String) As String
        Select Case p_force
            Case "KN" : p_force = "kN"
            Case "Kgf" : p_force = "kgf"
            Case "Tonf" : p_force = "tf"
            Case "tonf" : p_force = "tf"
            Case Else ' No action needed
        End Select
        Return p_force
    End Function

    ''' <summary>
    ''' Reconciles any difference in length units from the units library to the form accepted by CSi programs.
    ''' </summary>
    ''' <param name="p_length">Length unit value to reconcile.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReconcileLengthToProgram(ByVal p_length As String) As String
        If String.Compare(p_length, "in", ignoreCase:=True) = 0 Then p_length = "inch"
        Return p_length
    End Function

    ''' <summary>
    ''' Reconciles any difference in length units from a CSi program to the form accepted by the units library.
    ''' </summary>
    ''' <param name="p_length">Length unit value to reconcile.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReconcileLengthFromProgram(ByVal p_length As String) As String
        If String.Compare(p_length, "inch", ignoreCase:=True) = 0 Then p_length = "in"
        Return p_length
    End Function
End Class
