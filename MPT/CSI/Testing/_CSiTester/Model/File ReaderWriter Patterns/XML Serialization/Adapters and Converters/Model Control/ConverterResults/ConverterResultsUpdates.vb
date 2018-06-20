Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports CSiTester.ModelControl

Imports MPT.String.ConversionLibrary

Public Class ConverterResultsUpdates

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As ObservableCollection(Of cMCResultUpdate)) As result_updatesUpdate()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As result_updatesUpdate

        For i = 0 To p_values.Count - 1
            fileArray(i) = ConvertToFile(p_values(i))
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cMCResultUpdate) As result_updatesUpdate
        Return New result_updatesUpdate() With {.comment = p_value.comment,
                                                .id = CStr(p_value.id)
                                               }
    End Function


    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As result_updatesUpdate()) As ObservableCollection(Of cMCResultUpdate)
        Dim resultUpdates As New ObservableCollection(Of cMCResultUpdate)
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return resultUpdates

        For i = 0 To p_values.Count - 1
            resultUpdates.Add(ConvertFromFile(p_values(i)))
        Next

        Return resultUpdates
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As result_updatesUpdate) As cMCResultUpdate
        Return New cMCResultUpdate() With {.comment = p_value.comment,
                                           .id = myCInt(p_value.id)
                                          }
    End Function
End Class
