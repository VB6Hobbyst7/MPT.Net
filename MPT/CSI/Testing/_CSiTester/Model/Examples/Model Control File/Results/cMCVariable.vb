Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class cMCVariable
    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _scaleFactor As Double
    ''' <summary>
    ''' Scale factor that is multiplied by a given variable.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property scaleFactor As Double
        Set(ByVal value As Double)
            If Not _scaleFactor = value Then
                _scaleFactor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("scaleFactor"))
            End If
        End Set
        Get
            Return _scaleFactor
        End Get
    End Property

    Private _range As New cMCRange
    ''' <summary>
    ''' Class that performs a function across a range of values. This applies to an overall result (such as over time).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property range As cMCRange
        Set(ByVal value As cMCRange)
            _range = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("range"))
        End Set
        Get
            Return _range
        End Get
    End Property

    Private _rangeAll As New cMCRangeAll
    ''' <summary>
    ''' Class that performs a function across a range of values. This applies to an overall result (such as over time).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property rangeAll As cMCRangeAll
        Set(ByVal value As cMCRangeAll)
            _rangeAll = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("rangeAll"))
        End Set
        Get
            Return _rangeAll
        End Get
    End Property

    Private _isQuerySpecified As Boolean
    ''' <summary>
    ''' If 'True', then a query has been specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isQuerySpecified As Boolean
        Get
            Return _isQuerySpecified
        End Get
    End Property

    Private _query As New cMCQuery
    ''' <summary>
    ''' List of field names and values used to look up the unique output cell to use as a variable in a calculation.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property query As cMCQuery
        Set(ByVal value As cMCQuery)
            _query = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fieldsLookup"))
        End Set
        Get
            Return _query
        End Get
    End Property

    Private _name As String
    ''' <summary>
    ''' Name of the column/header of the field to reference.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
        Set(ByVal value As String)
            _name = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("name"))
        End Set
        Get
            Return _name
        End Get
    End Property

    Private _queryStringPartial As String
    ''' <summary>
    ''' String of the query produced by the various class components to identify a unique row. Does not include table name or extraction value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property queryStringPartial As String
        Get
            If String.IsNullOrWhiteSpace(_queryStringPartial) Then
                _queryStringPartial = query.asString()
            End If
            Return _queryStringPartial
        End Get
    End Property
#End Region


#Region "Initialization"

    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCVariable

        With myClone
            .name = name

            For Each fieldLookup As cFieldLookup In query
                .query.Add(fieldLookup)
            Next

            .UpdateQueryString(queryStringPartial)
            ._isQuerySpecified = _isQuerySpecified

            .range = CType(range.Clone, cMCRange)
            .rangeAll = CType(rangeAll.Clone, cMCRangeAll)

            .scaleFactor = scaleFactor
        End With

        Return myClone
    End Function
#End Region

#Region "Methods:  Friend"
    ''' <summary>
    ''' Adds query string to object and updates cFieldLookup classes.
    ''' </summary>
    ''' <param name="p_query"></param>
    ''' <remarks></remarks>
    Friend Sub UpdateQueryString(ByVal p_query As String)
        If Not _queryStringPartial = p_query Then
            _queryStringPartial = p_query
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("queryStringPartial"))

            query.SetQuery(p_query)
            SetIsQuerySpecified()
        End If
    End Sub

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCVariable) Then Return False
        Dim isMatch As Boolean = True
        Dim comparedObject As cMCVariable = TryCast(p_object, cMCVariable)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not StringsMatch(.name, name) Then Return False

            For Each externalFieldLooup As cFieldLookup In comparedObject.query
                For Each fieldLookup As cFieldLookup In query
                    If externalFieldLooup.Equals(fieldLookup) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            If Not .isQuerySpecified = isQuerySpecified Then Return False
            If Not .queryStringPartial = queryStringPartial Then Return False

            If Not .range.Equals(range) Then Return False
            If Not .rangeAll.Equals(rangeAll) Then Return False

            If Not .scaleFactor = scaleFactor Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Adds a new lookup field object to the collection.
    ''' </summary>
    ''' <param name="myFieldLookup"></param>
    ''' <remarks></remarks>
    Private Sub AddFieldLookup(ByVal myFieldLookup As cFieldLookup)
        query.Add(myFieldLookup)
    End Sub


    ''' <summary>
    ''' Takes the data stored in the cFieldLookup classes and writes them into a single string search query.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WritePartialQueryFromClasses()
        Dim tempString As String = ""
        Dim i = 0
        Try
            If query IsNot Nothing Then
                For Each fieldLookup As cFieldLookup In query
                    If Not i = 0 Then tempString = tempString & " AND "
                    tempString = tempString & fieldLookup.name & " = '" & fieldLookup.valueField & "'"
                    i += 1
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the status of the query specification state based on the current result properties and minimum requirements.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIsQuerySpecified()
        If Not String.IsNullOrEmpty(queryStringPartial) Then
            _isQuerySpecified = True
        Else
            _isQuerySpecified = False
        End If

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("isQuerySpecified "))
    End Sub
#End Region
End Class
