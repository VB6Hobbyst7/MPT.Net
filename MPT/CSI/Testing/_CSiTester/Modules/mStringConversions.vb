Option Strict On
Option Explicit On

Imports CSiTester.cEnumerations
Imports System.ComponentModel.DescriptionAttribute
Imports System.Collections.ObjectModel

''' <summary>
''' Converts between a string value and another value, such as a boolean or enumeration.
''' </summary>
''' <remarks></remarks>
Module mStringConversions

    ''Half gone
    ' ''' <summary>
    ' ''' Takes the string value of a yes/no/unknown classification and converts it to an enumeration, or converts from an enumeration to a string.
    ' ''' </summary>
    ' ''' <param name="yesNoUnknownString">Calculation operation as a string. To be read from or written to.</param>
    ' ''' <remarks></remarks>
    'Function ConvertYesNoUnknownEnum(ByRef yesNoUnknownString As String) As eYesNoUnknown
    '    Select Case yesNoUnknownString
    '        Case "no" : Return eYesNoUnknown.no
    '        Case "yes" : Return eYesNoUnknown.yes
    '        Case "" : Return eYesNoUnknown.unknown
    '        Case Else : Return eYesNoUnknown.unknown
    '    End Select
    'End Function
    ''^^^^

    ''=== Booleans
    ' ''' <summary>
    ' ''' Converts a yes/no string into a true/false boolean value. If input is not yes/no, function will return false.
    ' ''' </summary>
    ' ''' <param name="yesNo">Parameter to convert. Capitalization does not matter.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ConvertYesTrueBoolean(ByVal yesNo As String) As Boolean
    '    Select Case yesNo.ToUpper
    '        Case "YES" : ConvertYesTrueBoolean = True
    '        Case "NO" : ConvertYesTrueBoolean = False
    '        Case Else : ConvertYesTrueBoolean = False
    '    End Select
    'End Function

    ' ''' <summary>
    ' ''' Converts a true/false boolean into a yes/no string.
    ' ''' </summary>
    ' ''' <param name="trueFalse">True/false boolean to convert.</param>
    ' ''' <param name="capitalization">Capitalization effect desired for returned string.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ConvertYesTrueString(ByVal trueFalse As Boolean, ByVal capitalization As eCapitalization) As String
    '    Dim yesNoTemp As String

    '    If trueFalse Then
    '        yesNoTemp = "yes"
    '    Else
    '        yesNoTemp = "no"
    '    End If

    '    Select Case capitalization
    '        Case eCapitalization.ALLCAPS : ConvertYesTrueString = yesNoTemp.ToUpper
    '        Case eCapitalization.alllower : ConvertYesTrueString = yesNoTemp.ToLower
    '        Case eCapitalization.Firstupper : ConvertYesTrueString = Left(yesNoTemp, 1).ToUpper & Right(yesNoTemp, Len(yesNoTemp) - 1).ToLower
    '        Case Else : ConvertYesTrueString = yesNoTemp.ToUpper
    '    End Select
    'End Function

    ' ''' <summary>
    ' ''' Converts a true/false string into a true/false boolean value. If input is not true/false, function will return false.
    ' ''' </summary>
    ' ''' <param name="trueFalse">Parameter to convert. Capitalization does not matter.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ConvertTrueTrueBoolean(ByVal trueFalse As String) As Boolean
    '    Select Case trueFalse.ToUpper
    '        Case "TRUE" : ConvertTrueTrueBoolean = True
    '        Case "FALSE" : ConvertTrueTrueBoolean = False
    '        Case Else : ConvertTrueTrueBoolean = False
    '    End Select
    'End Function

    ' ''' <summary>
    ' ''' Converts a true/false boolean into a true/false string.
    ' ''' </summary>
    ' ''' <param name="trueFalse">True/false boolean to convert.</param>
    ' ''' <param name="capitalization">Capitalization effect desired for returned string.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ConvertTrueTrueString(ByVal trueFalse As Boolean, ByVal capitalization As eCapitalization) As String
    '    Dim trueFalseTemp As String

    '    If trueFalse Then
    '        trueFalseTemp = "true"
    '    Else
    '        trueFalseTemp = "false"
    '    End If

    '    Select Case capitalization
    '        Case eCapitalization.ALLCAPS : ConvertTrueTrueString = trueFalseTemp.ToUpper
    '        Case eCapitalization.alllower : ConvertTrueTrueString = trueFalseTemp.ToLower
    '        Case eCapitalization.Firstupper : ConvertTrueTrueString = Left(trueFalseTemp, 1).ToUpper & Right(trueFalseTemp, Len(trueFalseTemp) - 1).ToLower
    '        Case Else : ConvertTrueTrueString = trueFalseTemp.ToUpper
    '    End Select
    'End Function

    ''TODO:
    ''1. relative vs. absolute? as boolean or enum?

    ''=== Other
    ' ''' <summary>
    ' ''' Takes a list of test types and sets the corresponding values in the test types object that is returned.
    ' ''' </summary>
    ' ''' <param name="testsList">List of test types applicable to a given example, e.g. 'run as is'.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ConvertTestsStringToObject(ByVal testsList As ObservableCollection(Of String)) As cTestTypes
    '    Dim mytests As New cTestTypes

    '    With mytests
    '        For Each test As String In testsList
    '            If test = .runAsIsString Then
    '                .runAsIs = True
    '            Else
    '                .runAsIs = False
    '            End If
    '            If test = .updateBridgeString Then
    '                .updateBridge = True
    '            Else
    '                .updateBridge = False
    '            End If
    '            If test = .updateBridgeAndRunString Then
    '                .updateBridgeAndRun = True
    '            Else
    '                .updateBridgeAndRun = False
    '            End If
    '            If test = .runAsIsDiffAnalyParamsString Then
    '                .runAsIsDiffAnalyParams = True
    '            Else
    '                .runAsIsDiffAnalyParams = False
    '            End If
    '        Next
    '    End With

    '    Return mytests
    'End Function

    ' ''' <summary>
    ' ''' Takes an object with values set for the presence of various test types, and adds the 'true' ones to a list.
    ' ''' </summary>
    ' ''' <param name="myTests">Object that contains true/false values for the applicable test types, e.g. 'run as is'.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ConvertTestsObjectToString(ByVal myTests As cTestTypes) As ObservableCollection(Of String)
    '    Dim testsList As New ObservableCollection(Of String)
    '    With myTests
    '        If .runAsIs Then testsList.Add(.runAsIsString)
    '        If .updateBridge Then testsList.Add(.updateBridgeString)
    '        If .updateBridgeAndRun Then testsList.Add(.updateBridgeAndRunString)
    '        If .runAsIsDiffAnalyParams Then testsList.Add(.runAsIsDiffAnalyParamsString)
    '    End With

    '    Return testsList
    'End Function

    ' ''' <summary>
    ' ''' Converts a string to an integer if the string is numeric. Otherwise, returns 0.
    ' ''' </summary>
    ' ''' <param name="myString">String to convert.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function myCInt(ByVal myString As String) As Integer
    '    If IsNumeric(myString) Then
    '        Return CInt(myString)
    '    Else
    '        Return 0
    '    End If
    'End Function

    ' ''' <summary>
    ' ''' Converts a string to a double if the string is numeric. Otherwise, returns 0.
    ' ''' </summary>
    ' ''' <param name="myString">String to convert.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function myCDbl(ByVal myString As String) As Double
    '    If IsNumeric(myString) Then
    '        Return CDbl(myString)
    '    Else
    '        Return 0
    '    End If
    'End Function

    ' ''' <summary>
    ' ''' Converts a string to an decimal if the string is numeric. Otherwise, returns 0.
    ' ''' </summary>
    ' ''' <param name="myString">String to convert.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function myCDec(ByVal myString As String) As Decimal
    '    If IsNumeric(myString) Then
    '        Return CDec(myString)
    '    Else
    '        Return 0
    '    End If
    'End Function

    ' ''' <summary>
    ' ''' Returns the table name with or without brackets, depending on what is specified.
    ' ''' </summary>
    ' ''' <param name="p_tableName">Table name to check.</param>
    ' ''' <param name="p_removeBrackets">If 'True', then any existing brackets will be removed. Otherwise, brackets will be added if they aren't already present.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ParseTableName(ByVal p_tableName As String,
    '                        ByVal p_removeBrackets As Boolean) As String
    '    Dim tableName As String = p_tableName

    '    If p_removeBrackets Then
    '        If Left(p_tableName, 1) = "[" Then tableName = Right(tableName, Len(tableName) - 1)
    '        If Right(p_tableName, 1) = "]" Then tableName = Left(tableName, Len(tableName) - 1)
    '    Else
    '        If Not Left(p_tableName, 1) = "[" Then tableName = "[" & tableName
    '        If Not Right(p_tableName, 1) = "]" Then tableName = tableName & "]"
    '    End If

    '    Return tableName
    'End Function
End Module
