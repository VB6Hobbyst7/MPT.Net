
Option Explicit On
Option Strict On

Public Class CSiEnumConverter

    'eItemType
#If COMPILE_ETABS2013 Then
    'eItemType
    Friend Shared Function ToCSi(ByVal enumValue As eItemType) As ETABS2013.eItemType
        Return CType(enumValue, ETABS2013.eItemType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As ETABS2013.eItemType) As eItemType
        Return CType(enumValue, eItemType)
    End Function

    ' eLoadPattern
    Friend Shared Function ToCSi(ByVal enumValue As eLoadPatternType) As ETABS2013.eLoadPatternType
        Return CType(enumValue, ETABS2013.eLoadPatternType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As ETABS2013.eLoadPatternType) As eLoadPatternType
        Return CType(enumValue, eLoadPatternType)
    End Function
#ElseIf COMPILE_ETABS2014 Then
    'eItemType
    Friend Shared Function ToCSi(ByVal enumValue As eItemType) As ETABS2014.eItemType
        Return CType(enumValue, ETABS2014.eItemType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As ETABS2014.eItemType) As eItemType
        Return CType(enumValue, eItemType)
    End Function

    ' eLoadPattern
    Friend Shared Function ToCSi(ByVal enumValue As eLoadPatternType) As ETABS2014.eLoadPatternType
        Return CType(enumValue, ETABS2014.eLoadPatternType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As ETABS2014.eLoadPatternType) As eLoadPatternType
        Return CType(enumValue, eLoadPatternType)
    End Function
#ElseIf COMPILE_SAP2000v16 Then
    'eItemType
    Friend Shared Function ToCSi(ByVal enumValue As eItemType) As SAP2000v16.eItemType
        Return CType(enumValue, SAP2000v16.eItemType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As SAP2000v16.eItemType) As eItemType
        Return CType(enumValue, eItemType)
    End Function

    ' eLoadPattern
    Friend Shared Function ToCSi(ByVal enumValue As eLoadPatternType) As SAP2000v16.eLoadPatternType
        Return CType(enumValue, SAP2000v16.eLoadPatternType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As SAP2000v16.eLoadPatternType) As eLoadPatternType
        Return CType(enumValue, eLoadPatternType)
    End Function

#ElseIf COMPILE_SAP2000v17 Then
    'eItemType
    Friend Shared Function ToCSi(ByVal enumValue As eItemType) As SAP2000v17.eItemType
        Return CType(enumValue, SAP2000v17.eItemType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As SAP2000v17.eItemType) As eItemType
        Return CType(enumValue, eItemType)
    End Function

    ' eLoadPattern
    Friend Shared Function ToCSi(ByVal enumValue As eLoadPatternType) As SAP2000v17.eLoadPatternType
        Return CType(enumValue, SAP2000v17.eLoadPatternType)
    End Function

    Friend Shared Function FromCSi(ByVal enumValue As SAP2000v17.eLoadPatternType) As eLoadPatternType
        Return CType(enumValue, eLoadPatternType)
    End Function
#End If

End Class
