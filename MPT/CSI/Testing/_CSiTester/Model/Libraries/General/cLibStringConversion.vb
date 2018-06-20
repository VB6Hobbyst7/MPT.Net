Option Strict On
Option Explicit On

Imports System.ComponentModel.DescriptionAttribute
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports CSiTester.cEnumerations

Imports CSiTester.cLibPath

''' <summary>
''' Converts between a string value and another value, such as a boolean or enumeration.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibStringConversion

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Takes the string value of a yes/no/unknown classification and converts it to an enumeration, or converts from an enumeration to a string.
    ''' </summary>
    ''' <param name="yesNoUnknownString">Calculation operation as a string.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesNoUnknownEnum(ByRef yesNoUnknownString As String) As eYesNoUnknown
        Select Case yesNoUnknownString.ToUpper
            Case "NO" : Return eYesNoUnknown.no
            Case "YES" : Return eYesNoUnknown.yes
            Case "" : Return eYesNoUnknown.unknown
            Case Else : Return eYesNoUnknown.unknown
        End Select
    End Function

    ''' <summary>
    ''' Converts a boolean to a descritpion of a YesNoUnknown enumeration.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueYesNoUnknownString(ByVal p_value As Boolean) As String
        Return GetEnumDescription(ConvertTrueYesNoUnknownEnum(p_value))
    End Function

    ''' <summary>
    ''' Converts a boolean to a YesNoUnknown enumeration.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueYesNoUnknownEnum(ByVal p_value As Boolean) As eYesNoUnknown
        Dim yesTrueString As String = ConvertYesTrueString(p_value, cEnumerations.eCapitalization.alllower)

        Return ConvertYesNoUnknownEnum(yesTrueString)
    End Function

    ''' <summary>
    ''' Converts a YesNoUnkonwn enumeration description to the closes representation of a boolean. 'Unknown' = false.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesNoUnknownStringBoolean(ByVal p_value As String) As Boolean
        Dim enumFromString As eYesNoUnknown = ConvertStringToEnumByDescription(Of eYesNoUnknown)(p_value)

        Return ConvertYesNoUnknownBoolean(enumFromString)
    End Function

    ''' <summary>
    ''' Converts a YesNoUnkonwn enumeration to the closes representation of a boolean. 'Unknown' = false.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesNoUnknownBoolean(ByVal p_value As eYesNoUnknown) As Boolean
        Return ConvertYesTrueBoolean(GetEnumDescription(p_value))
    End Function


    '=== Booleans
    ''' <summary>
    ''' Converts a yes/no string into a true/false boolean value. If input is not yes/no, function will return false.
    ''' </summary>
    ''' <param name="p_yesNo">Parameter to convert. Capitalization does not matter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesTrueBoolean(ByVal p_yesNo As String) As Boolean
        Select Case p_yesNo.ToUpper
            Case "YES" : ConvertYesTrueBoolean = True
            Case "NO" : ConvertYesTrueBoolean = False
            Case Else : ConvertYesTrueBoolean = False
        End Select
    End Function

    ''' <summary>
    ''' Converts a true/false boolean into a yes/no string.
    ''' </summary>
    ''' <param name="p_trueFalse">True/false boolean to convert.</param>
    ''' <param name="p_capitalization">Capitalization effect desired for returned string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesTrueString(ByVal p_trueFalse As Boolean,
                                                ByVal p_capitalization As eCapitalization) As String
        Dim yesNoTemp As String

        If p_trueFalse Then
            yesNoTemp = "yes"
        Else
            yesNoTemp = "no"
        End If

        Return CapitalizationSingleWord(yesNoTemp, p_capitalization)
    End Function

    ''' <summary>
    ''' Converts a single word to the specified capitalization format.
    ''' </summary>
    ''' <param name="p_word">Word to alter.</param>
    ''' <param name="p_capitalization">Capitalization pattern to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function CapitalizationSingleWord(ByVal p_word As String,
                                                     ByVal p_capitalization As eCapitalization) As String
        Select Case p_capitalization
            Case eCapitalization.ALLCAPS
                Return p_word.ToUpper
            Case eCapitalization.alllower
                Return p_word.ToLower
            Case eCapitalization.Firstupper
                Return Left(p_word, 1).ToUpper & Right(p_word, Len(p_word) - 1).ToLower
            Case Else
                Return p_word.ToUpper
        End Select
    End Function

    ''' <summary>
    ''' Converts a true/false string into a true/false boolean value. If input is not true/false, function will return false.
    ''' </summary>
    ''' <param name="p_trueFalse">Parameter to convert. Capitalization does not matter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueTrueBoolean(ByVal p_trueFalse As String) As Boolean
        Select Case p_trueFalse.ToUpper
            Case "TRUE" : ConvertTrueTrueBoolean = True
            Case "FALSE" : ConvertTrueTrueBoolean = False
            Case Else : ConvertTrueTrueBoolean = False
        End Select
    End Function

    ''' <summary>
    ''' Converts a true/false boolean into a true/false string.
    ''' </summary>
    ''' <param name="p_trueFalse">True/false boolean to convert.</param>
    ''' <param name="p_capitalization">Capitalization effect desired for returned string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueTrueString(ByVal p_trueFalse As Boolean,
                                                 ByVal p_capitalization As eCapitalization) As String
        Dim trueFalseTemp As String

        If p_trueFalse Then
            trueFalseTemp = "true"
        Else
            trueFalseTemp = "false"
        End If

        Select Case p_capitalization
            Case eCapitalization.ALLCAPS : ConvertTrueTrueString = trueFalseTemp.ToUpper
            Case eCapitalization.alllower : ConvertTrueTrueString = trueFalseTemp.ToLower
            Case eCapitalization.Firstupper : ConvertTrueTrueString = Left(trueFalseTemp, 1).ToUpper & Right(trueFalseTemp, Len(trueFalseTemp) - 1).ToLower
            Case Else : ConvertTrueTrueString = trueFalseTemp.ToUpper
        End Select
    End Function

    'TODO:
    '1. relative vs. absolute? as boolean or enum?

    ''' <summary>
    ''' Converts a string to an integer if the string is numeric. Otherwise, returns 0.
    ''' </summary>
    ''' <param name="p_stringConvert">String to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function myCInt(ByVal p_stringConvert As String) As Integer
        If IsNumeric(p_stringConvert) Then
            Return CInt(p_stringConvert)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Converts a string to a double if the string is numeric. Otherwise, returns 0.
    ''' </summary>
    ''' <param name="p_stringConvert">String to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function myCDbl(ByVal p_stringConvert As String) As Double
        If IsNumeric(p_stringConvert) Then
            Return CDbl(p_stringConvert)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Converts a string to an decimal if the string is numeric. Otherwise, returns 0.
    ''' </summary>
    ''' <param name="p_stringConvert">String to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function myCDec(ByVal p_stringConvert As String) As Decimal
        If IsNumeric(p_stringConvert) Then
            Return CDec(p_stringConvert)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Returns the table name with or without brackets, depending on what is specified.
    ''' </summary>
    ''' <param name="p_tableName">Table name to check.</param>
    ''' <param name="p_removeBrackets">If 'True', then any existing brackets will be removed. Otherwise, brackets will be added if they aren't already present.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParseTableName(ByVal p_tableName As String,
                            ByVal p_removeBrackets As Boolean) As String
        Dim tableName As String = p_tableName

        If p_removeBrackets Then
            If Left(p_tableName, 1) = "[" Then tableName = Right(tableName, Len(tableName) - 1)
            If Right(p_tableName, 1) = "]" Then tableName = Left(tableName, Len(tableName) - 1)
        Else
            If Not Left(p_tableName, 1) = "[" Then tableName = "[" & tableName
            If Not Right(p_tableName, 1) = "]" Then tableName = tableName & "]"
        End If

        Return tableName
    End Function
#End Region
End Class
