Option Strict On
Option Explicit On

Imports System.Linq.Expressions

''' <summary>
''' Contains functions for working with operations that require reflection.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class ReflectionLibrary
    Private Const VB_LOCAL As String = "$VB$Local_"

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Methods"
    ''' <summary>
    ''' Returns the name of the property supplied.
    ''' Call this by: NameOf(NameOfParam(Function() Me.{property})
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="propertyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NameOfProp(Of T)(ByVal propertyName As Expression(Of Func(Of T))) As String
        Dim exp As MemberExpression = TryCast(propertyName.Body, MemberExpression)

        If exp IsNot Nothing Then
            Return exp.Member.Name
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Returns the name of the parameter supplied.
    ''' Call this by: NameOf(NameOfParam(Function() Me.{property})
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="paramNameFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NameOfParam(Of T)(ByVal paramNameFull As Expression(Of Func(Of T))) As String
        Dim exp As MemberExpression = TryCast(paramNameFull.Body, MemberExpression)

        If exp IsNot Nothing Then
            Dim paramName As String = exp.Member.Name
            If paramName.Contains(VB_LOCAL) Then paramName = paramName.Substring(VB_LOCAL.Length)
            Return paramName
        Else
            Return Nothing
        End If
    End Function

#End Region
End Class
