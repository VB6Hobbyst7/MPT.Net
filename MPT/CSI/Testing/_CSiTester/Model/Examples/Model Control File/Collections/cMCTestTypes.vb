Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Reporting

''' <summary>
''' Class for the various types of tests that regTest can perform with the analysis programs.
''' </summary>
''' <remarks></remarks>
Public Class cMCTestTypes
    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"
    Private Const RUN_AS_IS As String = "run as is"
    Private Const RUN_AS_IS_PSB As String = "run as is with different sets of analysis parameters"
    Private Const UPDATE_BRIDGE As String = "update bridge"
    Private Const UPDATE_BRIDGE_AND_RUN As String = "update bridge and run"
#End Region

#Region "Variables"
    Private _convertList As New Dictionary(Of String, String)
#End Region

#Region "Properties"
    'Private _runAsIs As Boolean
    ' ''' <summary>
    ' ''' The model runs without any changes of the model prior to running it. This applies to the vast majority of the models.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property runAsIs As Boolean
    '    Set(ByVal value As Boolean)
    '        If Not _runAsIs = value Then
    '            _runAsIs = value
    '            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("runAsIs"))
    '        End If
    '    End Set
    '    Get
    '        Return _runAsIs
    '    End Get
    'End Property
    ' ''' <summary>
    ' ''' The model runs without any changes of the model prior to running it. This applies to the vast majority of the models.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property runAsIsString As String

    'Private _runAsIsDiffAnalyParams As Boolean
    ' ''' <summary>
    ' ''' This test applies only to CSiBridge and serves to verify whether all bridge objects can be successfully updated. If the program gets stuck while updating the bridge objects, it will time out and the model will be subjected to further scrutiny.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property runAsIsDiffAnalyParams As Boolean
    '    Set(ByVal value As Boolean)
    '        If Not _runAsIsDiffAnalyParams = value Then
    '            _runAsIsDiffAnalyParams = value
    '            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("runAsIsDiffAnalyParams"))
    '        End If
    '    End Set
    '    Get
    '        Return _runAsIsDiffAnalyParams
    '    End Get
    'End Property
    ' ''' <summary>
    ' ''' This test applies only to CSiBridge and serves to verify whether all bridge objects can be successfully updated. If the program gets stuck while updating the bridge objects, it will time out and the model will be subjected to further scrutiny.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property runAsIsDiffAnalyParamsString As String

    'Private _updateBridge As Boolean
    ' ''' <summary>
    ' ''' This test applies only to CSiBridge and serves to verify whether the benchmarks remain the same when the model is run after updating bridge objects.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property updateBridge As Boolean
    '    Set(ByVal value As Boolean)
    '        If Not _updateBridge = value Then
    '            _updateBridge = value
    '            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("updateBridge"))
    '        End If
    '    End Set
    '    Get
    '        Return _updateBridge
    '    End Get
    'End Property
    ' ''' <summary>
    ' ''' This test applies only to CSiBridge and serves to verify whether the benchmarks remain the same when the model is run after updating bridge objects.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property updateBridgeString As String

    'Private _updateBridgeAndRun As Boolean
    ' ''' <summary>
    ' ''' Will run 9 different combinations of analysis parameters while saving the test results into separate subdirectories in the output directory.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Property updateBridgeAndRun As Boolean
    '    Set(ByVal value As Boolean)
    '        If Not _updateBridgeAndRun = value Then
    '            _updateBridgeAndRun = value
    '            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("updateBridgeAndRun"))
    '        End If
    '    End Set
    '    Get
    '        Return _updateBridgeAndRun
    '    End Get
    'End Property
    ' ''' <summary>
    ' ''' Will run 9 different combinations of analysis parameters while saving the test results into separate subdirectories in the output directory.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property updateBridgeAndRunString As String

    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As eTestType
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified type.
    ''' </summary>
    ''' <param name="p_testType"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_testType As eTestType) As eTestType
        Get
            Return GetItem(p_testType)
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        'Initialize test type conversion list
        Dim testType As String

        testType = GetEnumDescription(eTestType.runAsIs)
        _convertList.Add(testType, RUN_AS_IS)

        testType = GetEnumDescription(eTestType.runAsIsDiffAnalyParams)
        _convertList.Add(testType, RUN_AS_IS_PSB)

        testType = GetEnumDescription(eTestType.updateBridge)
        _convertList.Add(testType, UPDATE_BRIDGE)

        testType = GetEnumDescription(eTestType.updateBridgeAndRun)
        _convertList.Add(testType, UPDATE_BRIDGE_AND_RUN)
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Adds a list of test types to the class.
    ''' </summary>
    ''' <param name="p_testTypes"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_testTypes As List(Of eTestType))
        If p_testTypes Is Nothing Then Exit Sub

        For Each testType As String In p_testTypes
            Me.Add(testType)
        Next
    End Sub
    ''' <summary>
    ''' Adds a list of test types to the class.
    ''' </summary>
    ''' <param name="p_testTypes"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_testTypes As ObservableCollection(Of eTestType))
        If p_testTypes Is Nothing Then Exit Sub

        For Each testType As String In p_testTypes
            Me.Add(testType)
        Next
    End Sub

    ''' <summary>
    ''' Adds a new test type item to the list if it is unique.
    ''' </summary>
    ''' <param name="p_testType"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_testType As eTestType) As Boolean
        Try
            If (p_testType = eTestType.myError OrElse p_testType = eTestType.none) Then Return False

            'If update entry is unique, add it
            If ItemIsUnique(p_testType) Then
                InnerList.Add(p_testType)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Adds a list of test types to the class.
    ''' </summary>
    ''' <param name="p_testTypes"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_testTypes As List(Of String))
        If p_testTypes Is Nothing Then Exit Sub

        For Each testType As String In p_testTypes
            Me.Add(testType)
        Next
    End Sub
    ''' <summary>
    ''' Adds a list of test types to the class.
    ''' </summary>
    ''' <param name="p_testTypes"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_testTypes As ObservableCollection(Of String))
        If p_testTypes Is Nothing Then Exit Sub

        For Each testType As String In p_testTypes
            Me.Add(testType)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new test type item to the list if it is unique.
    ''' </summary>
    ''' <param name="p_testType"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_testType As String) As Boolean
        Try
            Dim testType As eTestType = ConvertStringToTestType(p_testType)
            If (testType = eTestType.none OrElse testType = eTestType.myError) Then Return False

            'If update entry is unique, add it
            If ItemIsUnique(testType) Then
                InnerList.Add(testType)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Removes the item at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_index As Integer)
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
    ''' Removes the specified test type item.
    ''' </summary>
    ''' <param name="p_testType"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_testType As eTestType)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of eTestType)

            For Each item As eTestType In InnerList
                If Not item = p_testType Then
                    tempList.Add(item)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As eTestType In tempList
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
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of eTestType)
        Dim templist As New ObservableCollection(Of eTestType)

        For Each item As eTestType In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as an observable collection, converted to regTest terms.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollectionAsRegTestTerm() As ObservableCollection(Of String)
        Dim templist As New ObservableCollection(Of String)

        For Each item As eTestType In InnerList
            Dim regTestTerm As String = ConvertTestTypeEnumToRegTest(item)
            templist.Add(regTestTerm)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list, converted to regTest terms.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToListAsRegTestTerm() As List(Of String)
        Dim templist As New List(Of String)

        For Each item As eTestType In InnerList
            Dim regTestTerm As String = ConvertTestTypeEnumToRegTest(item)
            templist.Add(regTestTerm)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCTestTypes

        With myClone
            For Each item As eTestType In InnerList
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
        If Not (TypeOf p_object Is cMCTestTypes) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCTestTypes = TryCast(p_object, cMCTestTypes)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each itemOuter As eTestType In comparedObject
            isMatch = False
            For Each itemInner As eTestType In InnerList
                If itemOuter = itemInner Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Checks whether the minimum required information has been specified for the object.
    ''' </summary>
    ''' <returns>True if all required data is filled. Otherwise, false</returns>
    ''' <remarks></remarks>
    Friend Function RequiredDataFilled() As Boolean
        If InnerList.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As eTestType
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), eTestType)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    Private Function ItemIsUnique(ByVal p_item As eTestType) As Boolean
        Dim itemUnique As Boolean = True

        For Each item As eTestType In InnerList
            If item = p_item Then
                itemUnique = False
                Exit For
            End If
        Next

        Return itemUnique
    End Function

    ''' <summary>
    ''' Converts the supplied string to a test type if it matches either the enum description or regTest term.
    ''' </summary>
    ''' <param name="p_testType">Test type to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertStringToTestType(ByVal p_testType As String) As eTestType
        'Start by assuming string is an enum description, and if that fails, try it as a regTest term.
        Dim testType As eTestType = ConvertStringToEnumByDescription(Of eTestType)(p_testType)
        If testType = eTestType.myError Then testType = ConvertTestTypeRegTestToEnum(p_testType)

        Return testType
    End Function

    ''' <summary>
    ''' Converts the test type string as an enumeration to the term that regTest uses.
    ''' </summary>
    ''' <param name="p_testType">Test type enumeration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertTestTypeEnumToRegTest(ByVal p_testType As eTestType) As String
        If (p_testType = eTestType.none OrElse p_testType = eTestType.none) Then Return ""

        Dim regTestTerm As String = ConvertTestTypeEnumDescriptionToRegTest(GetEnumDescription(p_testType))

        Return regTestTerm
    End Function
    ''' <summary>
    ''' Converts the test type string as an enum description to the term that regTest uses.
    ''' </summary>
    ''' <param name="p_testType">Test type written as a string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertTestTypeEnumDescriptionToRegTest(ByVal p_testType As String) As String
        For Each kvp As KeyValuePair(Of String, String) In _convertList
            Dim enumDescription As String = kvp.Key
            Dim regTestTerm As String = kvp.Value

            If p_testType = enumDescription Then Return regTestTerm
        Next

        Return ""
    End Function

    ''' <summary>
    ''' Converts the test type string as the term that regTest uses to an enumeration.
    ''' </summary>
    ''' <param name="p_testType">Test type written as a string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertTestTypeRegTestToEnum(ByVal p_testType As String) As eTestType
        Dim enumDescription As String = ConvertTestTypeRegTestToEnumDescription(p_testType)
        Dim enumTestType As eTestType = ConvertStringToEnumByDescription(Of eTestType)(enumDescription)

        Return enumTestType
    End Function
    ''' <summary>
    ''' Converts the test type string as the term that regTest uses to an enum description.
    ''' </summary>
    ''' <param name="p_testType">Test type written as a string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertTestTypeRegTestToEnumDescription(ByVal p_testType As String) As String
        For Each kvp As KeyValuePair(Of String, String) In _convertList
            Dim enumDescription As String = kvp.Key
            Dim regTestTerm As String = kvp.Value

            If p_testType = regTestTerm Then Return enumDescription
        Next

        Return ""
    End Function
#End Region


End Class
