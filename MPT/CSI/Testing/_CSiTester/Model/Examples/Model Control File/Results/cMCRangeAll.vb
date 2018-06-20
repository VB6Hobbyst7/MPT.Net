Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cMCResultPostProcessed

Imports MPT.PropertyChanger

''' <summary>
''' Class that performs a function across a range of values. This can apply to an overall result (such as over time), or across multiple results (such as different node displacements at a given time).
''' </summary>
''' <remarks></remarks>
Public Class cMCRangeAll
    Inherits PropertyChanger
    Implements ICloneable

#Region "Properties"
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

    Private _fieldName As String
    ''' <summary>
    ''' Name of the column for which the range applies.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fieldName As String
        Set(ByVal value As String)
            If Not _fieldName = value Then
                _fieldName = value
                RaisePropertyChanged("fieldName")
            End If
        End Set
        Get
            Return _fieldName
        End Get
    End Property
#End Region

#Region "Initialization"

    Friend Sub New()

    End Sub


    Friend Overridable Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As cMCRangeAll = Create()

        With myClone
            .fieldName = fieldName
            .rangeOperation = rangeOperation
        End With

        Return myClone
    End Function
    Protected Overridable Function Create() As cMCRangeAll
        Return New cMCRangeAll()
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
        If Not (TypeOf p_object Is cMCRangeAll) Then Return False
        Dim isMatch As Boolean = True
        Dim comparedObject As cMCRangeAll = TryCast(p_object, cMCRangeAll)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .fieldName = fieldName Then Return False
            If Not .rangeOperation = rangeOperation Then Return False
        End With

        Return True
    End Function
#End Region
End Class
