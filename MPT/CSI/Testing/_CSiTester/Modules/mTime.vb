Option Strict On
Option Explicit On

''' <summary>
''' Module that contains routines for working with times in numerical and string formats, such as conversions and summations.
''' </summary>
''' <remarks></remarks>
Module mTime
    ' ''' <summary>
    ' ''' Converts time from HH:MM:SS format as a string to time in seconds as a number
    ' ''' </summary>
    ' ''' <param name="timeHHMMSS">Time in HH:MM:SS format</param>
    ' ''' <returns>Time in seconds</returns>
    ' ''' <remarks></remarks>
    'Function ConvertTimesNumber(ByVal timeHHMMSS As String) As Long
    '    Dim hourSeconds As Long
    '    Dim minuteSeconds As Long
    '    Dim secondSeconds As Long

    '    'Initialize
    '    hourSeconds = 0
    '    minuteSeconds = 0
    '    secondSeconds = 0

    '    'Generate numerical time in seconds
    '    If Len(timeHHMMSS) - 6 > 0 Then hourSeconds = CLng(Left(timeHHMMSS, Len(timeHHMMSS) - 6)) * 3600
    '    If Len(timeHHMMSS) - 4 > 0 Then minuteSeconds = CLng(Mid(timeHHMMSS, Len(timeHHMMSS) - 4, 2)) * 60
    '    If Len(timeHHMMSS) >= 2 Then secondSeconds = CLng(Right(timeHHMMSS, 2))

    '    ConvertTimesNumber = hourSeconds + minuteSeconds + secondSeconds

    'End Function

    ' ''' <summary>
    ' ''' Converts time from seconds as a number to HH:MM:SS format as a string
    ' ''' </summary>
    ' ''' <param name="timeSeconds">Time in seconds</param>
    ' ''' <returns>Time in HH:MM:SS format</returns>
    ' ''' <remarks></remarks>
    'Function ConvertTimesString(ByVal timeSeconds As Double) As String
    '    Dim hourHour As String
    '    Dim minuteMinute As String
    '    Dim secondSeconds As String
    '    Dim timeNum As Double

    '    'Hour Conversion
    '    timeNum = Math.Floor(timeSeconds / 3600)
    '    hourHour = CStr(timeNum)
    '    If Len(hourHour) < 2 Then hourHour = "0" & hourHour 'Ensures that the hour spot has two digits

    '    'Minute Conversion
    '    timeNum = Math.Floor((timeSeconds - timeNum * 3600) / 60)
    '    minuteMinute = CStr(timeNum)
    '    If Len(minuteMinute) < 2 Then minuteMinute = "0" & minuteMinute 'Ensures that the minute spot has two digits

    '    'Seconds Conversion
    '    secondSeconds = CStr(timeSeconds - CLng(hourHour) * 3600 - CLng(minuteMinute) * 60)

    '    'Assemble String
    '    ConvertTimesString = hourHour & ":" & minuteMinute & ":" & secondSeconds
    'End Function

    ' ''' <summary>
    ' ''' Converts time from HH:MM:SS format as a string to time in minutes as a number
    ' ''' </summary>
    ' ''' <param name="timeHHMMSS">Time in HH:MM:SS format</param>
    ' ''' <returns>Time in seconds</returns>
    ' ''' <remarks></remarks>
    'Function ConvertTimesNumberMinute(ByVal timeHHMMSS As String) As Double
    '    Dim hourSeconds As Long
    '    Dim minuteSeconds As Long
    '    Dim secondSeconds As Double

    '    'Initialize
    '    hourSeconds = 0
    '    minuteSeconds = 0
    '    secondSeconds = 0

    '    'Generate numerical time in seconds
    '    If Len(timeHHMMSS) - 6 > 0 Then hourSeconds = CLng(Left(timeHHMMSS, Len(timeHHMMSS) - 6)) * 60
    '    If Len(timeHHMMSS) - 4 > 0 Then minuteSeconds = CLng(Mid(timeHHMMSS, Len(timeHHMMSS) - 4, 2))
    '    If Len(timeHHMMSS) >= 2 Then secondSeconds = CDbl(Right(timeHHMMSS, 2)) / 60 'Math.Floor(CDbl(Right(timeHHMMSS, 2)) / 60)

    '    ConvertTimesNumberMinute = hourSeconds + minuteSeconds + secondSeconds

    'End Function

    ' ''' <summary>
    ' ''' Converts time from minutes as a number to HH:MM:SS format as a string
    ' ''' </summary>
    ' ''' <param name="timeMinutes">Time in minutes</param>
    ' ''' <returns>Time in HH:MM:SS format</returns>
    ' ''' <remarks></remarks>
    'Function ConvertTimesStringMinutes(ByVal timeMinutes As Double) As String
    '    Dim hourHour As String
    '    Dim minuteMinute As String
    '    Dim secondSeconds As String
    '    Dim timeNum As Double
    '    Dim timeSeconds As Double

    '    timeSeconds = timeMinutes * 60

    '    'Hour Conversion
    '    timeNum = Math.Floor(timeSeconds / 3600)
    '    hourHour = CStr(timeNum)
    '    If Len(hourHour) < 2 Then hourHour = "0" & hourHour 'Ensures that the hour spot has at least two digits


    '    'Minute Conversion
    '    timeNum = Math.Floor((timeSeconds - CLng(hourHour) * 3600) / 60)
    '    minuteMinute = CStr(timeNum)
    '    If Len(minuteMinute) < 2 Then minuteMinute = "0" & minuteMinute 'Ensures that the minute spot has two digits

    '    'Seconds Conversion
    '    secondSeconds = CStr(Math.Floor(timeSeconds - CLng(hourHour) * 3600 - CLng(minuteMinute) * 60))
    '    If Len(secondSeconds) < 2 Then secondSeconds = "0" & secondSeconds 'Ensures that the second spot has two digits

    '    ''Minute Conversion
    '    'timeNum = Math.Floor((timeSeconds - timeNum * 3600) / 60)
    '    'minuteMinute = CStr(timeNum)
    '    'If Len(minuteMinute) < 2 Then minuteMinute = "0" & minuteMinute 'Ensures that the minute spot has two digits

    '    ''Seconds Conversion
    '    'secondSeconds = CStr(Math.Floor(timeSeconds - CLng(hourHour) * 3600 - CLng(minuteMinute) * 60))
    '    'If Len(secondSeconds) < 2 Then secondSeconds = "0" & secondSeconds 'Ensures that the second spot has two digits

    '    'Assemble String
    '    ConvertTimesStringMinutes = hourHour & ":" & minuteMinute & ":" & secondSeconds
    'End Function
End Module
