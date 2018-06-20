Option Strict On
Option Explicit On

''' <summary>
''' Verifies various aspects of objects.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class ObjectVerify
     ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Methods"
    ''' <summary>
    ''' Returns 'True' if object is not nothing.
    ''' </summary>
    ''' <param name="testObject">Any object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsValidObject(ByVal testObject As Object) As Boolean
        Return (testObject IsNot Nothing)
    End Function

    ''' <summary>
    ''' Returns 'True' if object is not empty or DB Null.
    ''' </summary>
    ''' <param name="testObject">Any object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsValidObjectDB(ByVal testObject As Object) As Boolean
        Return (Not IsDBNull(testObject) AndAlso 
                testObject IsNot Nothing)
    End Function

    ''' <summary>
    ''' Returns 'True' if object is not empty or DB Null, and the value is not blank.
    ''' </summary>
    ''' <param name="testObject">Any object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsValidObjectDBStringFilled(ByVal testObject As Object) As Boolean
       Return (Not IsDBNull(testObject) AndAlso
                testObject IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(testObject.ToString))
    End Function

    ''' <summary>
    ''' Returns 'True' if object is not empty or DB Null, and the value is blank.
    ''' </summary>
    ''' <param name="testObject">Any object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsValidObjectDBStringEmpty(ByVal testObject As Object) As Boolean
       Return (Not IsDBNull(testObject) AndAlso
                testObject IsNot Nothing AndAlso
                String.IsNullOrEmpty(testObject.ToString))
    End Function
#End Region
End Class
