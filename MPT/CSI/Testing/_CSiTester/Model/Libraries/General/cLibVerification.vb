Option Strict On
Option Explicit On

Public NotInheritable Class cLibVerification

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"
    Public Shared Function IsValidObject(ByVal myObject As Object) As Boolean
        IsValidObject = False
        Try
            If myObject IsNot Nothing Then
                IsValidObject = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
        End Try
    End Function


    Public Shared Function IsValidObjectDB(ByVal myObject As Object) As Boolean
        IsValidObjectDB = False
        Try
            If Not IsDBNull(myObject) Then
                If myObject IsNot Nothing Then
                    IsValidObjectDB = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
        End Try
    End Function

    Public Shared Function IsValidObjectDBStringFilled(ByVal myObject As Object) As Boolean
        IsValidObjectDBStringFilled = False
        Try
            If Not IsDBNull(myObject) Then
                If myObject IsNot Nothing Then
                    If Not String.IsNullOrEmpty(myObject.ToString) Then
                        IsValidObjectDBStringFilled = True
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
        End Try
    End Function

    Public Shared Function IsValidObjectDBStringEmpty(ByVal myObject As Object) As Boolean
        IsValidObjectDBStringEmpty = False
        Try
            If Not IsDBNull(myObject) Then
                If myObject IsNot Nothing Then
                    If String.IsNullOrEmpty(myObject.ToString) Then
                        IsValidObjectDBStringEmpty = True
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(ex.StackTrace)
        End Try
    End Function
#End Region
End Class
