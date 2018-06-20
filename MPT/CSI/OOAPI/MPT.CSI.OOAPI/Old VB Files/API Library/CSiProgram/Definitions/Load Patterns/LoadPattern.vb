Option Strict On
Option Explicit On

Imports CSiApiTester.CSiEnumConverter

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class LoadPattern

#Region "Fields"
#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
    Private _sapModel As SAP2000v16.cSapModel
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If
#End Region

#Region "Initialization"
#If COMPILE_ETABS2013 Then
    Public Sub New(ByRef sapModel As ETABS2013.cSapModel)
        _sapModel = sapModel
    End Sub
#ElseIf COMPILE_ETABS2014 Then
    Public Sub New(ByRef sapModel As ETABS2014.cSapModel)
        _sapModel = sapModel
    End Sub
#ElseIf COMPILE_SAP2000v16 Then
    Public Sub New(ByRef sapModel As SAP2000v16.cSapModel)
        _sapModel = sapModel
    End Sub
#ElseIf COMPILE_SAP2000v17 Then
    Public Sub New(ByRef sapModel As SAP2000v17.cSapModel)
        _sapModel = sapModel
    End Sub
#End If
#End Region


#Region "Methods"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="NewName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeName(ByVal Name As String,
                               ByVal NewName As String) As Boolean
        ret = SapModel.LoadPatterns.ChangeName(Name, NewName)
        Return retCheck(ret, "SapModel.LoadPatterns.ChangeName")
    End Function

    ' Get/Set Cases
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="MyType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLoadType(ByVal Name As String,
                                ByRef MyType As eLoadPatternType) As Boolean
        ret = SapModel.LoadPatterns.GetLoadType(Name, ToCSi(MyType))
        Return retCheck(ret, "SapModel.LoadPatterns.GetLoadType")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="MyType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetLoadType(ByVal Name As String,
                                ByVal MyType As eLoadPatternType) As Boolean
        ret = SapModel.LoadPatterns.SetLoadType(Name, ToCSi(MyType))
        Return retCheck(ret, "SapModel.LoadPatterns.SetLoadType")
    End Function


    ' -----
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="SelfWTMultiplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSelfWtMultiplier(ByVal Name As String,
                                        ByRef SelfWTMultiplier As Double) As Boolean
        ret = SapModel.LoadPatterns.GetSelfWTMultiplier(Name, SelfWTMultiplier)
        Return retCheck(ret, "SapModel.LoadPatterns.GetSelfWtMultiplier")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="SelfWTMultiplier"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSelfWtMultiplier(ByVal Name As String,
                                        ByVal SelfWTMultiplier As Double) As Boolean
        ret = SapModel.LoadPatterns.SetSelfWTMultiplier(Name, SelfWTMultiplier)
        Return retCheck(ret, "SapModel.LoadPatterns.SetSelfWtMultiplier")
    End Function
#End Region
End Class
