Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel


Public Class cMCResultPostProcessed
    Inherits cMCResult
   
#Region "Enumerations"
    ''' <summary>
    ''' List of functions that can be specified for range and function operations in post-processed results.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eCalcOperations
        <Description("Sum")> sum = 1
        <Description("Sum Absolute Values")> sumabs = 2
        <Description("SRSS")> srss = 3
        <Description("Max")> max = 4
        <Description("Min")> min = 5
        <Description("Average")> avg = 6
        <Description("Max Absolute Values")> maxabs = 7
        <Description("Min Absolute Values")> minabs = 8
        <Description("Average Absolute Values")> avgabs = 9
    End Enum
#End Region

#Region "Properties"
    Private _variables As New ObservableCollection(Of cMCVariable)
    Public Property variables As ObservableCollection(Of cMCVariable)
        Set(ByVal value As ObservableCollection(Of cMCVariable))
            _variables = value
            RaisePropertyChanged("variables")
        End Set
        Get
            Return _variables
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
            RaisePropertyChanged("range")
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
            RaisePropertyChanged("rangeAll")
        End Set
        Get
            Return _rangeAll
        End Get
    End Property

    Private _rangeOperation As eCalcOperations
    ''' <summary>
    ''' Calculation operation that is to be done on the class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property rangeOperation As eCalcOperations
        Set(ByVal value As eCalcOperations)
            If Not _rangeOperation = value Then
                _rangeOperation = value
                RaisePropertyChanged("rangeOperation")
            End If
        End Set
        Get
            Return _rangeOperation
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        _resultType = cMCModel.eResultType.postProcessed
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cMCResultPostProcessed = DirectCast(MyBase.Clone, cMCResultPostProcessed)

        With myClone
            .range = DirectCast(range.Clone, cMCRange)
            .rangeAll = DirectCast(rangeAll.Clone, cMCRangeAll)

            For Each variable As cMCVariable In variables
                .variables.Add(variable)
            Next
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cMCResultBasic
        Return New cMCResultPostProcessed()
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCResultPostProcessed) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCResultPostProcessed = TryCast(p_object, cMCResultPostProcessed)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not .range.Equals(range) Then Return False
            If Not .rangeAll.Equals(rangeAll) Then Return False
            For Each variableOuter As cMCVariable In variables
                isMatch = False
                For Each variableInner As cMCVariable In .variables
                    If variableInner.Equals(variableOuter) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next
        End With

        Return True
    End Function
#End Region

End Class
