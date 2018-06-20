Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports MPT.String.ConversionLibrary

Friend Class ConverterIncidents

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCIncidentsTickets) As incidentsIncident()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As incidentsIncident

        For i = 0 To p_values.Count - 1
            fileArray(i).number = CStr(p_values(i))
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As incidentsIncident()) As cMCIncidentsTickets
        Dim obsCol As New cMCIncidentsTickets
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return obsCol

        For i = 0 To p_values.Count - 1
            obsCol.Add(myCInt(p_values(i).number))
        Next

        Return obsCol
    End Function
End Class
