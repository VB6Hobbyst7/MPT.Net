Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports MPT.String.ConversionLibrary

Friend Class ConverterUpdates

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCUpdates) As updatesUpdate()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As updatesUpdate

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
    Friend Shared Function ConvertToFile(ByVal p_value As cMCUpdate) As updatesUpdate
        Dim updateDate As New [date]() With
            {
                .day = CStr(p_value.updateDate.numDay),
                .month = CStr(p_value.updateDate.numMonth),
                .year = CStr(p_value.updateDate.numYear)
            }

        Return New updatesUpdate() With
            {
                .build = p_value.build,
                .comment = p_value.comment,
                .date = updateDate,
                .id = CStr(p_value.id),
                .person = p_value.person,
                .ticket = CStr(p_value.ticket)
            }
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <param name="p_mcModel">Model Control object to add the converted values to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As updatesUpdate(),
                                           Optional ByRef p_mcModel As cMCModel = Nothing) As cMCUpdates
        Dim updates As New cMCUpdates
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return updates

        If p_mcModel IsNot Nothing Then p_mcModel.ClearUpdates()

        For i = 0 To p_values.Count - 1
            Dim update As cMCUpdate = ConvertFromFile(p_values(i))
            updates.Add(update)
            If p_mcModel IsNot Nothing Then p_mcModel.AddUpdate(update)
        Next

        Return updates
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As updatesUpdate) As cMCUpdate
        Dim updateDate As New cMCDate() With
            {
                .numDay = myCInt(p_value.date.day),
                .numMonth = myCInt(p_value.date.month),
                .numYear = myCInt(p_value.date.year)
            }

        Return New cMCUpdate() With
            {
                .build = p_value.build,
                .comment = p_value.comment,
                .updateDate = updateDate,
                .id = myCInt(p_value.id),
                .person = p_value.person,
                .ticket = myCInt(p_value.ticket)
            }
    End Function

End Class
