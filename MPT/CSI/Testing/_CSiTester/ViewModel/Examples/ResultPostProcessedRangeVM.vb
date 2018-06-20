Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.ComponentModel



Namespace ViewModel
    Public Class ResultPostProcessedRangeVM
        Implements INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Enums"
        Public Enum eRangeType
            <Description("None")> None = 0
            <Description("Full Range")> RangeAll
            <Description("Custom Range")> RangeCustom
        End Enum
#End Region

#Region "Properties: Public"
        Private _operations As New ObservableCollection(Of String)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property operations As ObservableCollection(Of String)
            Set(ByVal value As ObservableCollection(Of String))
                _operations = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("operations"))
            End Set
            Get
                Return _operations
            End Get
        End Property

        Private _fields As New ObservableCollection(Of String)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property fields As ObservableCollection(Of String)
            Set(ByVal value As ObservableCollection(Of String))
                _fields = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fields"))
            End Set
            Get
                Return _fields
            End Get
        End Property

        Private _minRanges As New ObservableCollection(Of String)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property minRanges As ObservableCollection(Of String)
            Set(ByVal value As ObservableCollection(Of String))
                _minRanges = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("minRanges"))
            End Set
            Get
                Return _minRanges
            End Get
        End Property

        Private _maxRanges As New ObservableCollection(Of String)
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property maxRanges As ObservableCollection(Of String)
            Set(ByVal value As ObservableCollection(Of String))
                _maxRanges = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("maxRanges"))
            End Set
            Get
                Return _maxRanges
            End Get
        End Property
#End Region

#Region "Properties: Friend"
        Private _rangeType As eRangeType = eRangeType.None
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Property rangeType As eRangeType
            Set(ByVal value As eRangeType)
                If Not _rangeType = value Then
                    _rangeType = value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("rangeType"))
                End If
            End Set
            Get
                Return _rangeType
            End Get
        End Property

        Friend Property operation As String

        Friend Property field As String

        Friend Property minRange As String

        Friend Property maxRange As String
#End Region

#Region "Initialize"
        Public Sub New()
            operation = "add"
            operations.Add(operation)
            operations.Add("srss")
            operations.Add("abs")
            operations.Add("custom_function")

            field = "StepNum"
            fields.Add(field)
            fields.Add("U1")
            fields.Add("U2")

            minRange = "0"
            maxRange = "10"
            For i = CInt(minRange) To CInt(maxRange)
                minRanges.Add(CStr(i))
                maxRanges.Add(CStr(i))
            Next
        End Sub

        Public Sub New(ByVal p_results As cMCResults)

        End Sub


#End Region



    End Class
End Namespace