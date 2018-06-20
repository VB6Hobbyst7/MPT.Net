Option Strict On
Option Explicit On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If

Public Class DesignResultMetal
    Inherits DesignResultFrame
#Region "Properties"
    ''' <summary>
    ''' Controlling stress or capacity ratio for the frame object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Ratio As Double

    ''' <summary>
    ''' Indicates the controlling stress or capacity ratio type for each frame object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RatioType As RatioType

    ''' <summary>
    ''' The name of the design combination for which the controlling stress or capacity ratio occurs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ComboName As String
#End Region
End Class
