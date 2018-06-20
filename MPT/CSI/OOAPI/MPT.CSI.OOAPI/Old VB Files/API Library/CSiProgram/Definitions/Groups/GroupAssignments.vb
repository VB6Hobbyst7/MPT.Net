Option Strict On
Option Explicit On

''' <summary>
''' Assignments to a group, such as the name, and contained objects.
''' </summary>
''' <remarks></remarks>
Public Class GroupAssignments
#Region "Properties"
    ''' <summary>
    ''' Name of an existing group to which the assignments apply.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String

    'TODO: Create class that inherits from dictionary.
    ''' <summary>
    ''' List of [object name/object type] pairs assigned to the group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssignedObjects As New Dictionary(Of String, ObjectType)
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Changes the name of the group. 
    ''' "ALL" is a reserved group name and cannot be changed.
    ''' </summary>
    ''' <param name="nameExisting">Existing name of a defined group.</param>
    ''' <param name="nameNew">New name for the group. Cannot be "ALL"</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeName(ByVal nameExisting As String,
                               ByVal nameNew As String) As Boolean
        If ((nameExisting.CompareTo("ALL") = 0) OrElse (nameNew.CompareTo("ALL") = 0)) Then Return False

        ret = SapModel.GroupDef.ChangeName(nameExisting, nameNew)
        Return retCheck(ret, "SapModel.GroupDef.ChangeName")
    End Function

    ''' <summary>
    ''' Function clears (removes) all assignments from the specified group.
    ''' </summary>
    ''' <param name="nameGroup">Name of an existing group.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Clear(ByVal nameGroup As String) As Boolean
        ret = SapModel.GroupDef.Clear(nameGroup)
        Return retCheck(ret, "SapModel.GroupDef.Clear")
    End Function

    ''' <summary>
    ''' Retrieves the assignments to a specified group.
    ''' </summary>
    ''' <param name="nameGroup">Name of an existing group to get the assignments for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Assignments(ByVal nameGroup As String) As GroupAssignments
        Dim groupAssignment As New GroupAssignments
        Dim numberOfAssignmentsToGroup As Integer
        Dim objectType() As Integer = groupAssignment.AssignedObjects.Values.ToArray
        Dim objectNames() As String = groupAssignment.AssignedObjects.Keys.ToArray

        GetAssignments(nameGroup, numberOfAssignmentsToGroup, objectType, objectNames)

        With groupAssignment
            .Name = nameGroup
            For i = 0 To numberOfAssignmentsToGroup - 1
                .AssignedObjects.Add(objectNames(i), CType(objectType(i), ObjectType))
            Next
        End With

        Return groupAssignment
    End Function
#End Region


#Region "Methods: Private"
    ''' <summary>
    ''' Retrieves the assignments to a specified group.
    ''' </summary>
    ''' <param name="nameGroup">Name of an existing group to get the assignments for.</param>
    ''' <param name="numberOfAssignmentsToGroup">Number of assignments made to the specified group.</param>
    ''' <param name="objectType">Object type of each item in the groupc correpsonding to an integer code.</param>
    ''' <param name="objectNames">Name of each item in the group.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAssignments(ByVal nameGroup As String,
                                   ByRef numberOfAssignmentsToGroup As Integer,
                                   ByRef objectType() As Integer,
                                   ByRef objectNames() As String) As Boolean
        ret = SapModel.GroupDef.GetAssignments(nameGroup, numberOfAssignmentsToGroup, objectType, objectNames)
        Return retCheck(ret, "SapModel.GroupDef.GetAssignments")
    End Function

#End Region

End Class
