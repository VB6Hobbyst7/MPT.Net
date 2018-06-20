Option Strict On
Option Explicit On

Module mVerification
    'Friend Function IsValidObject(ByVal myObject As Object) As Boolean
    '    IsValidObject = False
    '    Try
    '        If Not IsNothing(myObject) Then
    '            IsValidObject = True
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        MessageBox.Show(ex.StackTrace)
    '    End Try
    'End Function


    'Friend Function IsValidObjectDB(ByVal myObject As Object) As Boolean
    '    IsValidObjectDB = False
    '    Try
    '        If Not IsDBNull(myObject) Then
    '            If Not IsNothing(myObject) Then
    '                IsValidObjectDB = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        MessageBox.Show(ex.StackTrace)
    '    End Try
    'End Function

    'Friend Function IsValidObjectDBStringFilled(ByVal myObject As Object) As Boolean
    '    IsValidObjectDBStringFilled = False
    '    Try
    '        If Not IsDBNull(myObject) Then
    '            If Not IsNothing(myObject) Then
    '                If Not myObject.ToString = "" Then
    '                    IsValidObjectDBStringFilled = True
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        MessageBox.Show(ex.StackTrace)
    '    End Try
    'End Function

    'Friend Function IsValidObjectDBStringEmpty(ByVal myObject As Object) As Boolean
    '    IsValidObjectDBStringEmpty = False
    '    Try
    '        If Not IsDBNull(myObject) Then
    '            If Not IsNothing(myObject) Then
    '                If myObject.ToString = "" Then
    '                    IsValidObjectDBStringEmpty = True
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        MessageBox.Show(ex.StackTrace)
    '    End Try
    'End Function
End Module
