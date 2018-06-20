Option Strict On
Option Explicit On

Imports CSiApiTester.CSiEnumConverter

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class LoadPatterns
#Region "Properties"
    ''' <summary>
    ''' Collection of load patterns.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoadPatterns As New List(Of LoadPattern)
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Adds a new load pattern.
    ''' </summary>
    ''' <param name="Name">Name for the new load pattern.</param>
    ''' <param name="MyType">Load pattern type.</param>
    ''' <param name="SelfWTMultiplier">Self weight multiplier for the new load pattern.</param>
    ''' <param name="AddLoadCase">True: A linear static load case corresponding to the new load pattern is added.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal Name As String,
                        ByVal MyType As eLoadPatternType,
                        Optional ByVal SelfWTMultiplier As Double = 0,
                        Optional ByVal AddLoadCase As Boolean = True) As Boolean
        'An error is returned if the Name item is already used for an existing load pattern.

        ret = SapModel.LoadPatterns.Add(Name, ToCSi(MyType), SelfWTMultiplier, AddLoadCase)
        Return retCheck(ret, "SapModel.LoadPatterns.Add")
    End Function

    ''' <summary>
    ''' The number of defined load patterns.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Count() As Boolean
        ret = SapModel.LoadPatterns.Count()
        Return retCheck(ret, "SapModel.LoadPatterns.Count")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal Name As String) As Boolean
        'The load pattern is not deleted and the function returns an error if the load pattern is assigned to an load case or if it is the only defined load pattern.

        ret = SapModel.LoadPatterns.Delete(Name)
        Return retCheck(ret, "SapModel.LoadPatterns.Delete")
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="NumberNames"></param>
    ''' <param name="MyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNameList(ByRef NumberNames As Integer,
                                ByRef MyName() As String) As Boolean
        ret = SapModel.LoadPatterns.GetNameList(NumberNames, MyName)
        Return retCheck(ret, "SapModel.LoadPatterns.GetNameList")
    End Function

#End Region

End Class
