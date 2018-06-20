Option Strict On
Option Explicit On

''' <summary>
''' Contains routines for working with times in numerical and string formats, such as conversions and summations.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class TimeLibrary
    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Methods"
    ''' <summary>
    ''' Converts time from HH:MM:SS format as a string to time in seconds as a number.
    ''' </summary>
    ''' <param name="timeHHMMSS">Time in HH:MM:SS format.</param>
    ''' <returns>Time in seconds.</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertHHMMSSToSeconds(ByVal timeHHMMSS As String) As Double
        If (String.IsNullOrWhiteSpace(timeHHMMSS)) Then Return 0

        Dim timesByType As String() = TimeHHMMSSAbs(timeHHMMSS).Split(CChar(":"))
        
        Dim secondSeconds As Double = timeComponent(timesByType, timesByType.Length-1)
        Dim minuteSeconds As Double  = timeComponent(timesByType, timesByType.Length-2) * 60
        Dim hourSeconds As Double = timeComponent(timesByType, timesByType.Length-3) * 3600

        Return Sign(timeHHMMSS) * (hourSeconds + minuteSeconds + secondSeconds)
    End Function

    ''' <summary>
    ''' Converts time from seconds as a number to HH:MM:SS format as a string.
    ''' </summary>
    ''' <param name="timeSeconds">Time in seconds.</param>
    ''' <returns>Time in HH:MM:SS format.</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertSecondsToHHMMSS(ByVal timeSeconds As Double) As String        
        'Hour Conversion
        Dim timeNum As Double = Math.Floor(Math.Abs(timeSeconds) / 3600)
        Dim hourHour As String = TimeAsString(timeNum)

        'Minute Conversion
        timeNum = Math.Floor((Math.Abs(timeSeconds) - timeNum * 3600) / 60)
        Dim minuteMinute As String = TimeAsString(timeNum)

        'Seconds Conversion
        timeNum = Math.Abs(timeSeconds) - CDbl(hourHour) * 3600 - CDbl(minuteMinute) * 60 
        Dim secondSeconds As String = TimeAsString(timeNum)

        'Assemble String
        Dim sign As String = ""
        If (timeSeconds < 0) Then sign = "-"

        Return sign & hourHour & ":" & minuteMinute & ":" & secondSeconds
    End Function

    ''' <summary>
    ''' Converts time from HH:MM:SS format as a string to time in minutes as a number.
    ''' </summary>
    ''' <param name="timeHHMMSS">Time in HH:MM:SS format.</param>
    ''' <returns>Time in seconds.</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertHHMMSSToMinutes(ByVal timeHHMMSS As String) As Double
        If (String.IsNullOrWhiteSpace(timeHHMMSS)) Then Return 0

        Dim timesByType As String() = TimeHHMMSSAbs(timeHHMMSS).Split(CChar(":"))
        
        Dim secondSeconds As Double = timeComponent(timesByType, timesByType.Length-1) / 60
        Dim minuteSeconds As Double  = timeComponent(timesByType, timesByType.Length-2) 
        Dim hourSeconds As Double = timeComponent(timesByType, timesByType.Length-3) * 60

        Return Sign(timeHHMMSS) * (hourSeconds + minuteSeconds + secondSeconds)
    End Function

    ''' <summary>
    ''' Converts time from minutes as a number to HH:MM:SS format as a string.
    ''' </summary>
    ''' <param name="timeMinutes">Time in minutes.</param>
    ''' <returns>Time in HH:MM:SS format.</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertMinutesToHHMMSS(ByVal timeMinutes As Double) As String
        Dim timeSeconds As Double = timeMinutes * 60
        Return ConvertSecondsToHHMMSS(timeSeconds)
    End Function
#End Region

#Region "Private"
     Private Shared Function timeComponent(ByVal timesByType As String(), 
                                           ByVal index As Integer)  As Double
        If (index >= 0 AndAlso
            index <= timesByType.Length - 1 AndAlso
            IsNumeric(timesByType(index))) Then
            Return CLng(timesByType(index))
        Else
            Return 0
        End If
    End Function

     Private Shared Function Sign(ByVal timeHHMMSS As String) As Double
        If IsNegative(timeHHMMSS) Then
            Return -1
        Else
            Return 1
        End If 
    End Function

    Private Shared Function IsNegative(ByVal timeHHMMSS As String) As Boolean
        Return (timeHHMMSS(0) = "-")
    End Function

    Private Shared Function TimeHHMMSSAbs(ByVal timeHHMMSS As String) As String
        If IsNegative(timeHHMMSS) Then
            Return Mid(timeHHMMSS, 1,timeHHMMSS.Length - 1)
        Else
            Return timeHHMMSS
        End If
    End Function

    ''' <summary>
    ''' Returns time as a string with at least two digits. e.g. 01, 50, etc.
    ''' </summary>
    ''' <param name="time"></param>
    ''' <returns></returns>
    Private Shared Function TimeAsString(ByVal time As Double) As String
        Dim newTime As String = CStr(time)
        If Len(newTime) < 2 Then newTime = "0" & newTime 

        Return newTime
    End Function
#End Region
End Class
