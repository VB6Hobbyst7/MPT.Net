Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Xml.Serialization

''' <summary>
''' Contains methods for working with enumerations.
''' </summary>
''' <remarks></remarks>
Public Class EnumLibrary

    'For literal enum word to string
    '   See: http://msdn.microsoft.com/en-us/library/system.enum.getname(v=vs.110).aspx
    '   See: http://msdn.microsoft.com/en-us/library/16c1xs4z(v=vs.110).aspx
    'For full string-type/phrase-type associated with enum
    '   See: http://stackoverflow.com/questions/18888519/get-vb-net-enum-description-from-value
    '   See: http://bytes.com/topic/visual-basic-net/answers/353496-description-attribute-retrieval-enum-type

#Region "Methods: Public"

    '' For Convert, See: https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum

    ''' <summary>
    ''' Converts the specified enum value to anoter enum value.
    ''' </summary>
    ''' <typeparam name="TEnumFrom">The type enum to convert from.</typeparam>
    ''' <typeparam name="TEnumTo">The type of enum to conver to.</typeparam>
    ''' <param name="enumValue">The enum value to convert.</param>
    ''' <returns>TEnumTo.</returns>
    ''' <exception cref="System.ArgumentException">
    ''' TTo must be an enumerated type
    ''' or
    ''' TFrom must be an enumerated type
    ''' </exception>
    Public Shared Function Convert(Of TEnumFrom As {Structure, IConvertible}, TEnumTo As {Structure, IConvertible})(enumValue As TEnumFrom) As TEnumTo
	    If Not GetType(TEnumTo).IsEnum Then
		    Throw New ArgumentException("TTo must be an enumerated type")
	    End If
	    If Not GetType(TEnumFrom).IsEnum Then
		    Throw New ArgumentException("TFrom must be an enumerated type")
	    End If

	    Return DirectCast(DirectCast(enumValue, Object), TEnumTo)
    End Function

    ''' <summary>
    ''' Converts the specified enum value to anoter enum value.
    ''' </summary>
    ''' <typeparam name="TEnumFrom">The type enum to convert from.</typeparam>
    ''' <typeparam name="TEnumTo">The type of enum to conver to.</typeparam>
    ''' <param name="enumValueFrom">The enum value to convert.</param>
    ''' <param name="enumValueTo">Dummy enum value to indicate the enum type to convert to.</param>
    ''' <returns>TTo.</returns>
    ''' <exception cref="System.ArgumentException">
    ''' TTo must be an enumerated type
    ''' or
    ''' TFrom must be an enumerated type
    ''' </exception>
    Public Shared Function Convert(Of TEnumFrom As {Structure, IConvertible}, TEnumTo As {Structure, IConvertible})(enumValueFrom As TEnumFrom, enumValueTo As TEnumTo) As TEnumTo
	    If Not GetType(TEnumTo).IsEnum Then
		    Throw New ArgumentException("TTo must be an enumerated type")
	    End If
	    If Not GetType(TEnumFrom).IsEnum Then
		    Throw New ArgumentException("TFrom must be an enumerated type")
	    End If

	    Return DirectCast(DirectCast(enumValueFrom, Object), TEnumTo)
    End Function

    ''' <summary>
    ''' Returns the enum description (if any), otherwise returns the name of the enum value. 
    ''' </summary>
    ''' <param name="p_enumObj">Selected enumeration to convert.</param> 
    Public Shared Function GetEnumDescription(Of TEnum)(ByVal p_enumObj As TEnum) As String
        Dim fi As Reflection.FieldInfo = p_enumObj.GetType().GetField(p_enumObj.ToString())
        If (fi Is Nothing) Then Return ""

        Dim attributes As DescriptionAttribute() = CType(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
            Return attributes(0).Description
        Else
            Return p_enumObj.ToString()
        End If
    End Function

    ''' <summary>
    ''' Returns the enum XML attribute (if any), otherwise returns the name of the num value.
    ''' </summary>
    ''' <typeparam name="TEnum"></typeparam>
    ''' <param name="p_enumObj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEnumXMLAttribute(Of TEnum)(ByVal p_enumObj As TEnum) As String
        Dim fi As Reflection.FieldInfo = p_enumObj.GetType().GetField(p_enumObj.ToString())
        If (fi Is Nothing) Then Return ""

        Dim attributes As XmlEnumAttribute() = CType(fi.GetCustomAttributes(GetType(XmlEnumAttribute), False), XmlEnumAttribute())

        If (attributes IsNot Nothing AndAlso attributes.Length > 0) Then
            Return attributes(0).Name
        Else
            Return p_enumObj.ToString()
        End If
    End Function

    ''' <summary>
    ''' Returns the enum if the string matches the enum description. Returns Nothing if no match is found. 
    ''' </summary>
    ''' <param name="p_string">String item to match to the enum by enum description.</param>
    Public Shared Function ConvertStringToEnumByDescription(Of TEnum)(ByVal p_string As String) As TEnum
        Dim enumT As TEnum
        Dim enumItems As Array = System.Enum.GetValues(enumT.GetType)

        For Each enumItem As TEnum In enumItems
            If String.Compare(GetEnumDescription(enumItem), p_string, ignoreCase:=True) = 0 Then Return enumItem
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the enum if the string matches the enum XML attribute (or Enum.ToString() where no attribute exists). 
    ''' Returns Nothing if no match is found. 
    ''' </summary>
    ''' <param name="p_string">String item to match to the enum by enum description.</param>
    Public Shared Function ConvertStringToEnumByXMLAttribute(Of TEnum)(ByVal p_string As String) As TEnum
        Dim enumT As TEnum
        Dim enumItems As Array = System.Enum.GetValues(enumT.GetType)

        For Each enumItem As TEnum In enumItems
            If String.Compare(GetEnumXMLAttribute(enumItem), p_string, ignoreCase:=True) = 0 Then Return enumItem
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the list of all descriptions for an enumeration list.
    ''' Note that there is not always a description for every enumeration item.
    ''' </summary>
    ''' <typeparam name="TEnum">Type of enumeration.</typeparam>
    ''' <param name="p_enumObj">Sample enumeration object from the list of enums.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEnumDescriptionList(Of TEnum)(ByVal p_enumObj As TEnum) As List(Of String)
        Dim enumDescriptions As New List(Of String)
        Dim enumItems As Array = System.Enum.GetValues(p_enumObj.GetType)

        For Each enumItem As TEnum In enumItems
            enumDescriptions.Add(GetEnumDescription(enumItem))
        Next

        Return enumDescriptions
    End Function

    ''' <summary>
    ''' Returns the list item that matches the provided enumeration based on the XML attribute or .ToString() property of the enumeration.
    ''' Returns nothing if no match was found.
    ''' </summary>
    ''' <typeparam name="TEnum">Type of enumeration.</typeparam>
    ''' <param name="p_enum">EnumerationToMatch</param>
    ''' <param name="p_values">List of values to match to the enumeration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetListItemMatchingEnumByXMLAttribute(Of TEnum)(ByVal p_enum As TEnum,
                                                                           ByVal p_values As IList(Of String)) As String
        Dim enumValue As String = GetEnumXMLAttribute(p_enum)

        For Each value As String In p_values
            If String.Compare(value, enumValue, ignoreCase:=True) = 0 Then Return value
        Next

        Return ""
    End Function
#End Region

End Class
