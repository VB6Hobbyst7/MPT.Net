Option Strict On
Option Explicit On


Public Class Groups
#Region "Properties"
    ''' <summary>
    ''' Collection of groups.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Groups As New List(Of Group)
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Return the number of defined groups.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Count() As Boolean
        ret = SapModel.GroupDef.Count
        Return retCheck(ret, "SapModel.GroupDef.Count")
    End Function

    ''' <summary>
    ''' Retrieves the names of all defined groups.
    ''' </summary>
    ''' <param name="numberGroupNames">Number of group names retrieved by the program.</param>
    ''' <param name="groupNames">Names of all defined groups.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNameList(ByRef numberGroupNames As Integer,
                                ByRef groupNames() As String) As Boolean
        ret = SapModel.GroupDef.GetNameList(numberGroupNames, groupNames)
        Return retCheck(ret, "SapModel.GroupDef.GetNameList")
    End Function

    ''' <summary>
    ''' Deletes the specified group.
    ''' "ALL" is a reserved group name and cannot be deleted.
    ''' </summary>
    ''' <param name="nameGroup">Name of an existing group. Cannot be "ALL"</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal nameGroup As String) As Boolean
        If (nameGroup.CompareTo("ALL") = 0) Then Return False
        ret = SapModel.GroupDef.Delete(nameGroup)
        Return retCheck(ret, "SapModel.GroupDef.Delete")
    End Function
#End Region



End Class
