Option Strict On
Option Explicit On

''' <summary>
''' Contains the properties of a result item in an example used in end-to-end testing.
''' </summary>
''' <remarks></remarks>
Public Class cE2EExampleResult
    Implements ICloneable

#Region "Properties"
    ''' <summary>
    ''' ID or index number of the result within the example set of results.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String
    ''' <summary>
    ''' Raw unrounded result value expected for the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property raw As String
    ''' <summary>
    ''' Rounded result value expected for the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property rounded As String
    ''' <summary>
    ''' Percent change result value expected for the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property percentChange As String
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cE2EExampleResult

        With myClone
            .id = id
            .raw = raw
            .rounded = rounded
            .percentChange = percentChange
        End With

        Return myClone
    End Function
#End Region
End Class
