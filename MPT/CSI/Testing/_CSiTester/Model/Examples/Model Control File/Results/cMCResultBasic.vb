Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Imports CSiTester.cMCModel

''' <summary>
''' A class containing results that are checked for the example to determine if results have changed, and how closely they match independent values.
''' This is the most basic results class. 
''' </summary>
''' <remarks></remarks>
Public Class cMCResultBasic
    Inherits PropertyChanger

    Implements ICloneable
    Implements IComparable(Of cMCResultBasic)
    Implements ILoggerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Methods: Property Events"
    Private Sub benchmark_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _benchmark.PropertyChanged
        SetIsBMComplete()
        UpdateResultIDFromIDComponents()
    End Sub
    Private Sub query_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _query.PropertyChanged
        UpdateResultIDFromIDComponents()
    End Sub
#End Region

#Region "Fields"
    ''' <summary>
    '''  The number of significant figures to pad the benchmark ID by.
    ''' </summary>
    ''' <remarks></remarks>
    Private _benchmarkIDSigFigs As Integer
#End Region

#Region "Properties"
    Private _idTemp As Integer = -1
    ''' <summary>
    ''' Temporary ID for uniquely identifying and matching results when editing them in a form.
    ''' These are to be reset each time results are sent to an editing session. 
    ''' -1 means new result.
    ''' 0 means new result with set IDs in the current session.
    ''' > 0 means existing result read in from a source or set for a current editing session and is the only case that can be set manually.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property idTemp As Integer
        Get
            Return _idTemp
        End Get
        Set(value As Integer)
            ' Only allow setting ID if it is an existing result. The other cases are automated.
            If value > 0 Then
                _idTemp = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' The result is new, but the ID has been synced with the existing IDs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property idNewAndSynced As Boolean
        Get
            Return (_idTemp = 0)
        End Get
    End Property

    ''' <summary>
    ''' The result is new, and the ID has been not been synced with the existing IDs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property idNewAndUnsynced As Boolean
        Get
            Return (idTemp = -1)
        End Get
    End Property

    ''' <summary>
    ''' True: The id has been set for the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property idSet As Boolean
        Get
            Return (myCDec(id) >= 0)
        End Get
    End Property

    ''' <summary>
    ''' True: The current ID is to be preserved during ID alterations in results.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property idReadOnly As Boolean
        Get
            Return idTemp > 0
        End Get
    End Property


    Property _resultType As eResultType = eResultType.excelCalculated
    ''' <summary>
    ''' If 'Excel Calculated', then the result values have been filled by the 'regtest_internal_use/excel_results' node and there is a corresponding cMCResultExcel object that has been craeted to store corresponding supporting info.
    ''' If 'Post-Processed', then the result values have been filled by the corresponding post-processed XML elements and cMCResultPostProcessed object has been created to store corresponding supporting info.
    ''' If 'regular', there is nothing special about the class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property resultType As eResultType
        Get
            Return _resultType
        End Get
    End Property

    Protected _isComplete As Boolean
    ''' <summary>
    ''' If 'True', then the minimum data is present for a complete result.
    ''' Otherwise, the example is not yet complete to minimum requirements.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isComplete As Boolean
        Get
            Return _isComplete
        End Get
    End Property

    Private _isDetailsComplete As Boolean
    ''' <summary>
    ''' If 'True', then the minimum data is present for a complete set of result details. 
    ''' Otherwise, the example is not yet complete to minimum requirements.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isDetailsComplete As Boolean
        Get
            Return _isDetailsComplete
        End Get
    End Property

    Private _isBMComplete As Boolean
    ''' <summary>
    ''' If 'True', then the minimum data is present for a complete benchmark. 
    ''' Otherwise, the example is not yet complete to minimum requirements.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isBMComplete As Boolean
        Get
            Return _isBMComplete
        End Get
    End Property
#End Region

#Region "Properties: XML File"
    Protected _id As String
    ''' <summary>
    ''' The unique ID that identifies this particular result. 
    ''' The assigned id should be unique for a given model, so the same ID should not be shared between the regular, post-processed or excel results.
    ''' If it is in decimal format, then it is derived from sub-IDs as '[idQuery].[idBenchmark]'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String
        Set(ByVal value As String)
            ' Do not set this directly from the ID components, or else an infinite event loop could be triggered. Use the backing field instead.
            If (IsNumeric(value) AndAlso
                Not _id = value) Then
                _id = value
                RaisePropertyChanged(Function() Me.id)

                UpdateComponentIDs()
            End If
        End Set
        Get
            Return _id
        End Get
    End Property

    Private _name As String
    ''' <summary>
    ''' Description of the result being checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
        Set(ByVal value As String)
            If Not _name = value Then
                _name = value
                RaisePropertyChanged("comment")

                SetIsDetailsComplete()
            End If
        End Set
        Get
            Return _name
        End Get
    End Property

    Private _units As String
    ''' <summary>
    ''' The units that the benchmark value is associated with.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property units As String
        Set(ByVal value As String)
            If Not _units = value Then
                _units = value
                RaisePropertyChanged("units")
            End If
        End Set
        Get
            Return _units
        End Get
    End Property

    Private _unitsConversion As Boolean = True
    ''' <summary>
    ''' If true, then if the units specified are different than what is output from a CSi product, the CSi product output will be converted.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property unitsConversion As Boolean
        Set(ByVal value As Boolean)
            If Not _unitsConversion = value Then
                _unitsConversion = value
                RaisePropertyChanged("unitsConversion")
            End If
        End Set
        Get
            Return _unitsConversion
        End Get
    End Property

    Private _updates As New ObservableCollection(Of cMCResultUpdate)
    ''' <summary>
    ''' Collection of logs of updates for a particular result. 
    ''' Each result update should be associated with a model update object. 
    ''' The association can be made by matching their respective IDs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property updates As ObservableCollection(Of cMCResultUpdate)
        Set(ByVal value As ObservableCollection(Of cMCResultUpdate))
            _updates = value
            RaisePropertyChanged("updates")
        End Set
        Get
            Return _updates
        End Get
    End Property

    Private WithEvents _benchmark As New cFieldOutput
    ''' <summary>
    ''' Class containing information relating to the result output, such as the field name for looking up the result, and the expected values of the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property benchmark As cFieldOutput
        Set(ByVal value As cFieldOutput)
            _benchmark = value
            RaisePropertyChanged("benchmark")
            SetIsBMComplete()
        End Set
        Get
            Return _benchmark
        End Get
    End Property

    Protected _tableName As String
    ''' <summary>
    ''' Name of the table queried to look up the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property tableName As String
        Set(ByVal value As String)

        End Set
        Get
            Return ""
        End Get
    End Property
    Protected WithEvents _query As New cMCQuery
    ''' <summary>
    ''' List of field names and values used to look up the unique output cell to check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property query As cMCQuery
        Set(ByVal value As cMCQuery)

        End Set
        Get
            Return _query
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Protected Overridable Function Create() As cMCResultBasic
        Return New cMCResultBasic()
    End Function
#End Region


#Region "Methods: Override/Overload/Implement"
    Friend Overridable Function Clone() As Object Implements ICloneable.Clone
        Dim myClone As cMCResultBasic = Create()

        Try
            With myClone
                ._idTemp = _idTemp

                ._isComplete = _isComplete
                ._isDetailsComplete = _isDetailsComplete
                ._isBMComplete = _isBMComplete

                ._resultType = _resultType

                .name = name
                .units = units
                .unitsConversion = unitsConversion

                For Each update As cMCResultUpdate In updates
                    .updates.Add(DirectCast(update.Clone, cMCResultUpdate))
                Next

                .benchmark = DirectCast(benchmark.Clone, cFieldOutput)
                .query = DirectCast(query.Clone, cMCQuery)
                .UpdateResultIDFromIDComponents()
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCResultBasic) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCResultBasic = TryCast(p_object, cMCResultBasic)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not ._isComplete = _isComplete Then Return False
            If Not ._isDetailsComplete = _isDetailsComplete Then Return False
            If Not ._isBMComplete = _isBMComplete Then Return False

            If Not .resultType = resultType Then Return False

            If Not .id = id Then Return False
            If Not .query.ID = query.ID Then Return False
            If Not .benchmark.ID = benchmark.ID Then Return False

            If Not .name = name Then Return False
            If Not .units = units Then Return False
            If Not .unitsConversion = unitsConversion Then Return False

            For Each externalUpdate As cMCResultUpdate In .updates
                isMatch = False
                For Each internalUpdate As cMCResultUpdate In updates
                    If internalUpdate.Equals(externalUpdate) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            If Not .benchmark.Equals(benchmark) Then Return False
            If Not .query.Equals(query) Then Return False
        End With

        Return True
    End Function

    ''' <summary>
    ''' Sorts first by query ID, then benchmark ID.
    ''' </summary>
    ''' <param name="other"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function CompareTo(other As cMCResultBasic) As Integer Implements IComparable(Of cMCResultBasic).CompareTo
        If query.CompareTo(other.query) <> 0 Then
            Return query.CompareTo(other.query)
        ElseIf benchmark.CompareTo(other.benchmark) <> 0 Then
            Return benchmark.CompareTo(other.benchmark)
        Else
            Return 0
        End If
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the status of the details completion state based on the current result properties and minimum requirements.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIsDetailsComplete()
        If Not String.IsNullOrEmpty(_name) Then
            _isDetailsComplete = True
        Else
            _isDetailsComplete = False
        End If

        RaisePropertyChanged("isDetailsComplete")

        Me.SetIsComplete()
    End Sub

    ''' <summary>
    ''' Sets the status of the benchmark completion state based on the current result properties and minimum requirements.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetIsBMComplete()
        If Not (String.IsNullOrEmpty(benchmark.name) OrElse
                String.IsNullOrEmpty(benchmark.valueBenchmark)) Then
            _isBMComplete = True
        Else
            _isBMComplete = False
        End If

        RaisePropertyChanged("isBMComplete")

        Me.SetIsComplete()
    End Sub

    ''' <summary>
    ''' Returns 'True' if the result has all of the required properties specified.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub SetIsComplete()

        If (_isDetailsComplete AndAlso
            _isBMComplete) Then

            _isComplete = True
        Else
            _isComplete = False
        End If

        RaisePropertyChanged("isComplete")
    End Sub

    ' ID Conversions & Updates
    ''' <summary>
    ''' Returns the query ID from the result ID.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertResultIDToQueryID() As Integer
        Return myCInt(GetPrefix(CStr(id), "."))
    End Function

    ''' <summary>
    ''' Returns the becnhmark ID from the result ID.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertResultIDToBenchmarkID() As Integer
        Dim benchmarkID As Integer = 0
        If Not ConvertResultIDToQueryID() = myCInt(id) Then benchmarkID = myCInt(GetSuffix(CStr(id), "."))
        Return benchmarkID
    End Function

    ''' <summary>
    ''' Updates the string representation of the complete ID for the result based on the component query &amp; benchmark IDs.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub UpdateResultIDFromIDComponents()
        _id = CStr(query.ID)
        SetResultNewSynced()
    End Sub

    ''' <summary>
    ''' Updates the Query and Benchmark IDs based on the current complete result ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateComponentIDs()
        query.ID = ConvertResultIDToQueryID()
        benchmark.ID = ConvertResultIDToBenchmarkID()

        SetResultNewSynced()
    End Sub

    ''' <summary>
    ''' Updates ID Temp to indicate a new result with set IDs in the current session.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub SetResultNewSynced()
        If (query.ID >= 0 AndAlso
            benchmark.ID >= 0 AndAlso
            idTemp = -1) Then
            _idTemp = 0
        End If
    End Sub
#End Region

    Protected Overridable Sub OnLogger(e As LoggerEventArgs)
        RaiseEvent Log(e)
    End Sub

End Class
