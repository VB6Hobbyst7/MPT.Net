Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cMCResultPostProcessed

''' <summary>
''' Class that performs calculations on 1 or more result variables to get a calculated result. This may or may not also involve range calculations within the variables.
''' </summary>
''' <remarks></remarks>
Public Class cMCFormula
    Inherits cFieldOutput

#Region "Properties"
    Private _operation As eCalcOperations
    ''' <summary>
    ''' Calculation operation that is to be done on the class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property operation As eCalcOperations
        Set(ByVal value As eCalcOperations)
            If Not _operation = value Then
                _operation = value
                RaisePropertyChanged("operation")
            End If
        End Set
        Get
            Return _operation
        End Get
    End Property

    Private _variables As ObservableCollection(Of cMCVariable)
    ''' <summary>
    ''' Collection of variables which are used in the calculation operation to return a post-processed result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property variables As ObservableCollection(Of cMCVariable)
        Set(ByVal value As ObservableCollection(Of cMCVariable))
            _variables = value
            RaisePropertyChanged("variables")
        End Set
        Get
            Return _variables
        End Get
    End Property
#End Region

#Region "Initialization"

    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        variables = New ObservableCollection(Of cMCVariable)
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cMCFormula = DirectCast(MyBase.Clone, cMCFormula)

        With myClone
            .operation = operation

            For Each variable As cMCVariable In variables
                .variables.Add(CType(variable.Clone, cMCVariable))
            Next
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cFieldOutput
        Return New cMCFormula()
    End Function
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCFormula) Then Return False
        Dim isMatch As Boolean = True
        Dim comparedObject As cMCFormula = TryCast(p_object, cMCFormula)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not .operation = operation Then Return False

            For Each externalVariable As cMCVariable In .variables
                isMatch = False
                For Each internalVariable As cMCVariable In variables
                    If externalVariable.Equals(internalVariable) Then
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
