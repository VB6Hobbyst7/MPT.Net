Option Strict On
Option Explicit On

Imports System.Windows

''' <summary>
''' Verifies aspects of various Structs.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class StructVerify
    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

    ''' <summary>
    ''' Determines whether the coordinate struct is empty.
    ''' </summary>
    ''' <param name="testPoint"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PointIsEmpty(ByVal testPoint As Point) As Boolean
        Return (testPoint.X = 0 AndAlso testPoint.Y = 0 )
    End Function

    ''' <summary>
    ''' Determines if struct IntPtr is empty.
    ''' </summary>
    ''' <param name="testIntPtr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IntPtrIsEmpty(ByVal testIntPtr As IntPtr) As Boolean
        Return (testIntPtr.ToInt32 = 0)
    End Function

End Class
