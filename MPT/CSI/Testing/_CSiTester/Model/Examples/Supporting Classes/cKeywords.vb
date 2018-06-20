Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cExample
Imports CSiTester.cKeywordTags

''' <summary>
''' Class which stores a collection of unique keywords. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cKeywords
    Inherits System.Collections.CollectionBase
    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

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

#Region "Properties - List"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cKeyword
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified keyword name.
    ''' </summary>
    ''' <param name="p_keyword"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_keyword As String) As cKeyword
        Get
            Return GetItemByName(p_keyword)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified keyword name.
    ''' </summary>
    ''' <param name="p_keyword"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property itemByName(ByVal p_keyword As String) As cKeyword
        Get
            Return GetItemByName(p_keyword)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified keyword tag. If more than one tag exists, only the first one is returned.
    ''' </summary>
    ''' <param name="p_keywordTag"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property itemByTag(ByVal p_keywordTag As String) As cKeyword
        Get
            Return GetItemByTag(p_keywordTag)
        End Get
    End Property

    ''' <summary>
    ''' Gets all elements of the specified keyword tag.
    ''' </summary>
    ''' <param name="p_keywordTag"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property itemsByTag(ByVal p_keywordTag As String) As List(Of cKeyword)
        Get
            Return GetItemsByTag(p_keywordTag)
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cKeywords

        With myClone
            For Each item As cKeyword In InnerList
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
        If Not (TypeOf p_object Is cKeywords) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cKeywords = TryCast(p_object, cKeywords)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each itemOuter As cKeyword In comparedObject
            isMatch = False
            For Each itemInner As cKeyword In InnerList
                If itemOuter.Equals(itemInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Friend - Collection"
    ' Adding items
    ''' <summary>
    ''' Adds every keyword of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As List(Of String))
        If p_items Is Nothing Then Exit Sub

        For Each item As String In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As List(Of cKeyword))
        If p_items Is Nothing Then Exit Sub

        For Each item As cKeyword In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every keyword of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of String))
        If p_items Is Nothing Then Exit Sub

        For Each item As String In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of cKeyword))
        If p_items Is Nothing Then Exit Sub

        For Each item As cKeyword In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new keyword item to the list if it is unique by name.
    ''' </summary>
    ''' <param name="p_item">Full name of the keyword, including any tags.</param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_item As String) As Boolean
        If String.IsNullOrEmpty(p_item) Then Return False

        Dim keyword As New cKeyword(p_item)
        If Add(keyword) Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Adds a new keyword item to the list if it is unique by name.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_item As cKeyword) As Boolean
        If (p_item Is Nothing OrElse
            String.IsNullOrEmpty(p_item.name)) Then Return False

        Try
            Dim itemUnique As Boolean = True

            ' Ensure that the item is unique to the list
            For Each item As cKeyword In InnerList
                If p_item.Equals(item) Then
                    itemUnique = False
                    Exit For
                End If
            Next

            If itemUnique Then
                If TagWithinCountLimit(p_item) Then
                    InnerList.Add(p_item)
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                    Return True
                Else
                    ' No action taken
                End If
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function

    'Removing items
    ''' <summary>
    ''' Removes the item at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Sub Remove(ByVal p_index As Integer)
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            InnerList.RemoveAt(p_index)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Removes the item that has the specified keyword name.
    ''' </summary>
    ''' <param name="p_keyword">Keyword name that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Sub RemoveByName(ByVal p_keyword As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cKeyword)

            If String.IsNullOrWhiteSpace(p_keyword) Then Throw New ArgumentException("{0} is not specified.", p_keyword)

            For Each item As cKeyword In InnerList
                If Not StringsMatch(item.name, p_keyword) Then
                    tempList.Add(item)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cKeyword In tempList
                    InnerList.Add(item)
                Next

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Removes any keywords in the list containing the provided string, which is normally intended to be an identifying tag.
    ''' </summary>
    ''' <param name="p_keywordTag">Keyword tag to search for which, if found, indicates the keyword is to be removed.</param>
    ''' <remarks></remarks>
    Friend Sub RemoveByTag(ByVal p_keywordTag As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cKeyword)

            If String.IsNullOrWhiteSpace(p_keywordTag) Then Throw New ArgumentException("{0} is not specified.", p_keywordTag)

            For Each item As cKeyword In InnerList
                If Not StringsMatch(item.tag, p_keywordTag) Then
                    tempList.Add(item)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cKeyword In tempList
                    InnerList.Add(item)
                Next

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ' Replacing items
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As List(Of String))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As List(Of cKeyword))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of String))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of cKeyword))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ' Converting Lists
    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cKeyword)
        Dim templist As New ObservableCollection(Of cKeyword)

        For Each item As cKeyword In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function
    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cKeyword)
        Dim templist As New List(Of cKeyword)

        For Each item As cKeyword In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns a collection of all keyword full names.
    ''' </summary>
    ''' <param name="p_suppressTags">True: Keyword tags will not be included in the names returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function NamesToObservableCollection(Optional ByVal p_suppressTags As Boolean = False) As ObservableCollection(Of String)
        Dim templist As New ObservableCollection(Of String)

        For Each item As cKeyword In InnerList
            If Not p_suppressTags Then
                templist.Add(item.nameWithTag)
            Else
                templist.Add(item.name)
            End If
        Next

        Return templist
    End Function
    ''' <summary>
    '''  Returns a list of all keyword full names.
    ''' </summary>
    ''' <param name="p_suppressTags">True: Keyword tags will not be included in the names returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function NamesToList(Optional ByVal p_suppressTags As Boolean = False) As List(Of String)
        Dim templist As New List(Of String)

        For Each item As cKeyword In InnerList
            If Not p_suppressTags Then
                templist.Add(item.nameWithTag)
            Else
                templist.Add(item.name)
            End If
        Next

        Return templist
    End Function
#End Region

#Region "Methods: Friend"

    ' Query
    ''' <summary>
    ''' Determines if the current item can be added to the list based on if the maximum number of tag occurrences have occurred.
    ''' </summary>
    ''' <param name="p_item">Keyword to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function TagWithinCountLimit(ByVal p_item As cKeyword) As Boolean
        If String.IsNullOrEmpty(p_item.tag) Then
            Return True
        Else
            If NumberOfMatchingTags(p_item) <= p_item.maxTagOccurrence Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ''' <summary>
    ''' Returns the number of keywords that contain a matching tag to the keyword objet provided.
    ''' </summary>
    ''' <param name="p_item">Keyword object to be used as a basis for tag counting.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function NumberOfMatchingTags(ByVal p_item As cKeyword) As Integer
        Dim count As Integer = 0
        For Each item As cKeyword In InnerList
            If IsTagType(item.nameWithTag, p_item.tag) Then count += 1
        Next

        Return count
    End Function

    ''' <summary>
    ''' Returns a list of keyword objects of the matching tag.
    ''' </summary>
    ''' <param name="p_tag">Tag by which to collect keywords.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetKeywordsByTag(ByVal p_tag As String) As List(Of cKeyword)
        Dim keywordsOfTag As New List(Of cKeyword)

        For Each item As cKeyword In InnerList
            If IsTagType(item.nameWithTag, p_tag) Then keywordsOfTag.Add(item)
        Next

        Return keywordsOfTag
    End Function

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
            For Each keywordItem As cKeyword In InnerList
                Dim keywordName As String = ValueWithoutMatchingTag(keywordItem.name, _keywordTags.exampleType)

                If StringsMatch(keywordName, TYPE_ANALYSIS) Then p_isTypeAnalysis = True
                If StringsMatch(keywordName, TYPE_DESIGN) Then p_isTypeDesign = True
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determines whether or not any keywords in the list indicate an imported model is present.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetImportStatus(ByRef p_isImportedModel As Boolean)
        Try
            For Each keywordItem As cKeyword In InnerList
                If IsTagType(keywordItem.name, _keywordTags.importedModel) Then
                    p_isImportedModel = True
                    Exit For
                End If
                p_isImportedModel = False
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determines the names of any keywords of the code region or analysis class types. 
    ''' This is used for a table header and possible future organization.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetClassRegions(Optional ByRef p_codeRegion As String = "",
                               Optional ByRef p_analysisClass As String = "")
        Try
            p_codeRegion = ""
            p_analysisClass = ""

            For Each keywordItem As cKeyword In InnerList
                Select Case True
                    Case IsTagType(keywordItem.name, _keywordTags.codeRegion)
                        p_codeRegion = ValueWithoutTag(keywordItem.name, _keywordTags.codeRegion)
                    Case IsTagType(keywordItem.name, _keywordTags.analysisClass)
                        p_analysisClass = ValueWithoutTag(keywordItem.name, _keywordTags.analysisClass)
                End Select
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determins the names of any keywords of the design type or design class.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetDesignTagValues(Optional ByRef p_designType As String = "",
                                  Optional ByRef p_designClass As String = "")
        Try
            p_designType = ""
            p_designClass = ""

            For Each keywordItem As cKeyword In InnerList
                Select Case True
                    Case IsTagType(keywordItem.name, _keywordTags.designType)
                        p_designType = ValueWithoutTag(keywordItem.name, _keywordTags.designType)
                    Case IsTagType(keywordItem.name, _keywordTags.designClass)
                        p_designClass = ValueWithoutTag(keywordItem.name, _keywordTags.designClass)
                End Select
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private - Collection"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetItem(ByVal p_index As Integer) As cKeyword
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cKeyword)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by keyword name.
    ''' </summary>
    ''' <param name="p_keyword"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetItemByName(ByVal p_keyword As String) As cKeyword
        Try
            If String.IsNullOrEmpty(p_keyword) Then Throw New ArgumentException("Parameter {0} is not specified.", p_keyword)

            For Each item As cKeyword In InnerList
                If StringsMatch(item.name, p_keyword) Then
                    Return item
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the item specified by keyword tag.
    ''' </summary>
    ''' <param name="p_keywordTag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetItemByTag(ByVal p_keywordTag As String) As cKeyword
        Try
            If String.IsNullOrEmpty(p_keywordTag) Then Throw New ArgumentException("Parameter {0} is not specified.", p_keywordTag)

            For Each item As cKeyword In InnerList
                If StringsMatch(item.tag, p_keywordTag) Then
                    Return item
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns a list of items specified by keyword tag.
    ''' </summary>
    ''' <param name="p_keywordTag"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetItemsByTag(ByVal p_keywordTag As String) As List(Of cKeyword)
        Try
            Dim items As New List(Of cKeyword)

            If String.IsNullOrEmpty(p_keywordTag) Then Throw New ArgumentException("Parameter {0} is not specified.", p_keywordTag)

            For Each item As cKeyword In InnerList
                If StringsMatch(item.tag, p_keywordTag) Then
                    items.Add(item)
                End If
            Next

            Return items
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function
#End Region
End Class
