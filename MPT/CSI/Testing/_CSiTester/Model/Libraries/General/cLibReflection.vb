Option Strict On
Option Explicit On

Imports System.Linq.Expressions

''' <summary>
''' Contains functions for working with operations that require reflection.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibReflection
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Returns the name of the property supplied.
    ''' Call this by: NameOf(Function() Me.{property})
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="p_propertyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function NameOf(Of T)(p_propertyName As Expression(Of Func(Of T))) As String
        Dim exp As MemberExpression = TryCast(p_propertyName.Body, MemberExpression)

        If exp IsNot Nothing Then
            Return exp.Member.Name
        Else
            Return Nothing
        End If
    End Function
#End Region
End Class
