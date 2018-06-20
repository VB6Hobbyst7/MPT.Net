Option Explicit On
Option Strict On

Imports CSiTester.cLibPath
Imports System.Collections.ObjectModel

Imports CSiTester.cKeywordTags

''' <summary>
''' Class which stores a collection of unique keywords. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCKeywords
    Inherits cObsColUniqueString

#Region "Constants"
    '=== Keywords Tags
    Friend Const KEYWORD_GROUP_STANDARD As String = "standard"
    Friend Const KEYWORD_GROUP_WARNING As String = "warning"
#End Region

#Region "Properties: State"
    Private _keywordTags As New cKeywordTags
    Friend ReadOnly Property keywordTags As cKeywordTags
        Get
            Return _keywordTags
        End Get
    End Property
#End Region

#Region "Methods: Friend/Public - List"

    Friend Overloads Sub Add(ByVal p_item As String)
        If p_item Is Nothing Then Exit Sub

        If Not String.IsNullOrEmpty(p_item) Then MyBase.Add(p_item)
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As New cMCKeywords

        With myClone
            For Each item As String In InnerList
                .Add(item)
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCKeywords) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCKeywords = TryCast(p_object, cMCKeywords)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each keywordOuter As String In comparedObject
            isMatch = False
            For Each keywordInner As String In InnerList
                If keywordOuter = keywordInner Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ' Actions
    ''' <summary>
    ''' Removes any keywords in the list containing the provided string, which is normally intended to be an identifying tag.
    ''' </summary>
    ''' <param name="p_keywordTag">Keyword tag to search for which, if found, indicates the keyword is to be removed.</param>
    ''' <remarks></remarks>
    Friend Sub RemoveByTag(ByVal p_keywordTag As String)
        Dim tempList As New List(Of String)
        Dim itemRemoved As Boolean = False

        For Each keywordItem As String In InnerList
            If Not StringExistInName(keywordItem, p_keywordTag) Then
                tempList.Add(keywordItem)
            Else
                itemRemoved = True
            End If
        Next

        If itemRemoved Then
            InnerList.Clear()
            For Each item As String In tempList
                InnerList.Add(item)
            Next
        End If
    End Sub

    'Query State
    ''' <summary>
    ''' Determines whether any keywords in the list indicate a provided type.
    ''' </summary>
    ''' <param name="p_isTypeAnalysis">True: Keywords indicate an analysis type is present.</param>
    ''' <param name="p_isTypeDesign">True: Keywords indicate a design type is present.</param>
    ''' <remarks></remarks>
    Friend Sub GetExampleTypes(Optional ByRef p_isTypeAnalysis As Boolean = False,
                               Optional ByRef p_isTypeDesign As Boolean = False)
        p_isTypeAnalysis = False
        p_isTypeDesign = False
        Try
            For Each keywordItem As String In InnerList
                keywordItem = ValueWithoutMatchingTag(keywordItem, _keywordTags.exampleType)

                If StringsMatch(keywordItem, exampleTypeAnalysis) Then p_isTypeAnalysis = True
                If StringsMatch(keywordItem, exampleTypeDesign) Then p_isTypeDesign = True
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Determines whether or not any keywords in the list indicate an imported model is present.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetImportStatus(ByRef p_isImportedModel As Boolean)
        Try
            For Each keywordItem As String In InnerList
                If IsTagType(keywordItem, _keywordTags.importedModel) Then
                    p_isImportedModel = True
                    Exit For
                End If
                p_isImportedModel = False
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Determines whether an example can be grouped in a class or region, depending on keyword specification. 
    ''' This is used for a table header and possible future organization.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetClassRegions(ByRef p_codeRegion As String,
                               ByRef p_analysisClass As String)
        Try
            p_codeRegion = ""
            p_analysisClass = ""

            For Each keywordItem As String In InnerList
                Select Case True
                    Case IsTagType(keywordItem, _keywordTags.codeRegion)
                        p_codeRegion = ValueWithoutTag(keywordItem, _keywordTags.codeRegion)
                    Case IsTagType(keywordItem, _keywordTags.analysisClass)
                        p_analysisClass = ValueWithoutTag(keywordItem, _keywordTags.analysisClass)
                End Select
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Sets the design type and class of the example by searching for the corresponding keyword tags.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetDesignTagValues(ByRef p_designType As String,
                                   ByRef p_designClass As String)
        Try
            p_designType = ""
            p_designClass = ""

            For Each keywordItem As String In InnerList
                Select Case True
                    Case IsTagType(keywordItem, _keywordTags.designType)
                        p_designType = ValueWithoutTag(keywordItem, _keywordTags.designType)
                    Case IsTagType(keywordItem, _keywordTags.designClass)
                        p_designClass = ValueWithoutTag(keywordItem, _keywordTags.designClass)
                End Select
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ' Misc

#End Region

End Class
