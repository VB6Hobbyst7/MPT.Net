Option Strict On
Option Explicit On

Imports MPT.Enums
Imports MPT.Enums.EnumLibrary

''' <summary>
''' Converts between a string value and another value, such as a boolean or enumeration.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class ConversionLibrary

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Methods"

    ''' <summary>
    ''' Takes the string value of a yes/no/unknown classification and converts it to an enumeration, or converts from an enumeration to a string.
    ''' </summary>
    ''' <param name="yesNoUnknownString">Calculation operation as a string.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesNoUnknownEnum(ByVal yesNoUnknownString As String) As eYesNoUnknown
        If (yesNoUnknownString Is Nothing) Then Return eYesNoUnknown.unknown
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
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueYesNoUnknownString(ByVal value As Boolean,
                                                         Optional ByVal capitalization As eCapitalization = eCapitalization.AllLower) As String
        Dim yesNoTemp As String = GetEnumDescription(ConvertTrueYesNoUnknownEnum(value))
        Return CapitalizationSingleWord(yesNoTemp, capitalization)
    End Function

    ''' <summary>
    ''' Converts a boolean to a YesNoUnknown enumeration.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueYesNoUnknownEnum(ByVal value As Boolean) As eYesNoUnknown
        Dim yesTrueString As String = ConvertYesTrueString(value, eCapitalization.AllLower)

        Return ConvertYesNoUnknownEnum(yesTrueString)
    End Function

    ''' <summary>
    ''' Converts a YesNoUnknown enumeration description to the closest representation of a boolean. 'Unknown' = false.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesNoUnknownStringBoolean(ByVal value As String) As Boolean
        Dim enumFromString As eYesNoUnknown = ConvertStringToEnumByDescription(Of eYesNoUnknown)(value)

        Return ConvertYesNoUnknownBoolean(enumFromString)
    End Function

    ''' <summary>
    ''' Converts a YesNoUnkonwn enumeration to the closes representation of a boolean. 'Unknown' = false.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesNoUnknownBoolean(ByVal value As eYesNoUnknown) As Boolean
        Return ConvertYesTrueBoolean(GetEnumDescription(value))
    End Function


    '=== Booleans
    ''' <summary>
    ''' Converts a yes/no string into a true/false boolean value. If input is not yes/no, function will return false.
    ''' </summary>
    ''' <param name="yesNo">Parameter to convert. Capitalization does not matter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesTrueBoolean(ByVal yesNo As String) As Boolean
        If (yesNo Is Nothing) Then Return False
        Select Case yesNo.ToUpper
            Case "YES" : ConvertYesTrueBoolean = True
            Case "NO" : ConvertYesTrueBoolean = False
            Case Else : ConvertYesTrueBoolean = False
        End Select
    End Function

    ''' <summary>
    ''' Converts a true/false boolean into a yes/no string.
    ''' </summary>
    ''' <param name="trueFalse">True/false boolean to convert.</param>
    ''' <param name="capitalization">Capitalization effect desired for returned string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertYesTrueString(ByVal trueFalse As Boolean,
                                                ByVal capitalization As eCapitalization) As String
        Dim yesNoTemp As String

        If trueFalse Then
            yesNoTemp = "yes"
        Else
            yesNoTemp = "no"
        End If

        Return CapitalizationSingleWord(yesNoTemp, capitalization)
    End Function

    ''' <summary>
    ''' Converts a single word to the specified capitalization format.
    ''' </summary>
    ''' <param name="word">Word to alter.</param>
    ''' <param name="capitalization">Capitalization pattern to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CapitalizationSingleWord(ByVal word As String,
                                                     ByVal capitalization As eCapitalization) As String
        If (word Is Nothing) Then Return ""
        Select Case capitalization
            Case eCapitalization.ALLCAPS
                Return word.ToUpper
            Case eCapitalization.AllLower
                Return word.ToLower
            Case eCapitalization.Firstupper
                Return Left(word, 1).ToUpper & Right(word, Len(word) - 1).ToLower
            Case Else
                Return word.ToUpper
        End Select
    End Function

    ''' <summary>
    ''' Converts a true/false string into a true/false boolean value. If input is not true/false, function will return false.
    ''' </summary>
    ''' <param name="trueFalse">Parameter to convert. Capitalization does not matter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueTrueBoolean(ByVal trueFalse As String) As Boolean
        If (trueFalse Is Nothing) Then Return False
        Select Case trueFalse.ToUpper
            Case "TRUE" : ConvertTrueTrueBoolean = True
            Case "FALSE" : ConvertTrueTrueBoolean = False
            Case Else : ConvertTrueTrueBoolean = False
        End Select
    End Function

    ''' <summary>
    ''' Converts a true/false boolean into a true/false string.
    ''' </summary>
    ''' <param name="trueFalse">True/false boolean to convert.</param>
    ''' <param name="capitalization">Capitalization effect desired for returned string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertTrueTrueString(ByVal trueFalse As Boolean,
                                                 ByVal capitalization As eCapitalization) As String
        Dim trueFalseTemp As String

        If trueFalse Then
            trueFalseTemp = "true"
        Else
            trueFalseTemp = "false"
        End If

        Return CapitalizationSingleWord(trueFalseTemp, capitalization)
    End Function

    'TODO: relative vs. absolute? as boolean or enum?

    ''' <summary>
    ''' Converts a string to an integer if the string is numeric. Otherwise, returns 0.
    ''' </summary>
    ''' <param name="stringConvert">String to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function myCInt(ByVal stringConvert As String) As Integer
        If IsNumeric(stringConvert) Then
            Return CInt(stringConvert)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Converts a string to a double if the string is numeric. Otherwise, returns 0.
    ''' </summary>
    ''' <param name="stringConvert">String to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function myCDbl(ByVal stringConvert As String) As Double
        If IsNumeric(stringConvert) Then
            Return CDbl(stringConvert)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Converts a string to an decimal if the string is numeric. Otherwise, returns 0.
    ''' </summary>
    ''' <param name="stringConvert">String to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function myCDec(ByVal stringConvert As String) As Decimal
        If IsNumeric(stringConvert) Then
            Return CDec(stringConvert)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Returns the table name with or without brackets, depending on what is specified.
    ''' </summary>
    ''' <param name="tableName">Table name to check.</param>
    ''' <param name="removeBrackets">True: Any existing brackets will be removed. 
    ''' False: Brackets will be added if they aren't already present.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParseTableName(ByVal tableName As String,
                                          ByVal removeBrackets As Boolean) As String
        If (String.IsNullOrWhiteSpace(tableName)) Then Return ""
        Dim newTableName As String = tableName

        If removeBrackets Then
            If Left(tableName, 1) = "[" Then newTableName = Right(newTableName, Len(newTableName) - 1)
            If Right(tableName, 1) = "]" Then newTableName = Left(newTableName, Len(newTableName) - 1)
        Else
            If Not Left(tableName, 1) = "[" Then newTableName = "[" & newTableName
            If Not Right(tableName, 1) = "]" Then newTableName = newTableName & "]"
        End If

        Return newTableName
    End Function
#End Region
End Class
