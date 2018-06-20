Option Strict On
Option Explicit On

''' <summary>
''' Event argument that relays a message, error, or thrown exception from the libraries to an external assembly.
''' </summary>
''' <remarks></remarks>
Public Class LoggerEventArgs
    Inherits MessengerEventArgs

    Private _exception As Exception
    ''' <summary>
    ''' Exception object thrown.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Exception As Exception
        Get
            Return _exception
        End Get
    End Property

    Private _parameters As New Dictionary(Of String, String)
    ''' <summary>
    ''' Parameter name-value pairs associated with the calling method or operation within the method.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Parameters As Dictionary(Of String, String)
        Get
            Return _parameters
        End Get
    End Property

    ''' <summary>
    ''' Initializer or a message-type of log.
    ''' </summary>
    ''' <param name="message">Message associated with the event.</param>
    ''' <param name="arg">List of variables to insert into the message.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal message As String,
                   ParamArray arg() As Object)
        MyBase.New(message)
        storeParameters(arg)
    End Sub

    ''' <summary>
    ''' Initializer for an exception/error type of log.
    ''' </summary>
    ''' <param name="exception">Exception object thrown.</param>
    ''' <param name="arg">Parameter name-value pairs associated with the calling method or operation within the method.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal exception As Exception,
                   ParamArray arg() As Object)
        MyBase.New(exception.Message)
        storeParameters(arg)

        _exception = exception
    End Sub


    ''' <summary>
    ''' Initializer for an exception/error type of log.
    ''' </summary>
    ''' <param name="exception">Exception object thrown.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal exception As Exception)
        MyBase.New(exception.Message)
        _exception = exception
    End Sub


    ''' <summary>
    ''' Parses the arguments into parameter name/value pairs, accounting for the initial format of a local parameter object vs. property.
    ''' </summary>
    ''' <param name="arg"></param>
    ''' <remarks></remarks>
    Private Sub storeParameters(ParamArray arg() As Object)
        Dim parameterName As String = ""

        If arg.Count > 0 Then
            For i = 0 To arg.Count - 1 Step 2
                If i + 1 <= arg.Count - 1 Then
                    parameterName = arg(i).ToString
                    storeParameter(parameterName, arg(i + 1))
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Stores the name/value of the parameter, considering whether or not the value is a list of values.
    ''' </summary>
    ''' <param name="p_parameterName">Name of the parameter to store.</param>
    ''' <param name="p_parameterValue">Value object corresponding to the parameter name.</param>
    ''' <remarks></remarks>
    Private Sub storeParameter(ByVal p_parameterName As String,
                               ByVal p_parameterValue As Object)
        ' Determine if list of values or single value
        If (Not (TypeOf (p_parameterValue) Is String) AndAlso
            TypeOf (p_parameterValue) Is IEnumerable) Then
            Dim currentArg As IEnumerable = TryCast(p_parameterValue, IEnumerable)
            If currentArg IsNot Nothing Then
                Dim keyNumber As Integer = 0
                For Each currentArgItem As Object In currentArg
                    Parameters.Add(p_parameterName & "(" & keyNumber & ")", currentArgItem.ToString)
                    keyNumber += 1
                Next
            End If
        Else
            Parameters.Add(p_parameterName, p_parameterValue.ToString)
        End If
    End Sub
End Class
