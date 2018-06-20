Option Explicit On
Option Strict On


Public MustInherit Class DesignFrameMetal
    Inherits DesignFrame
    Implements IDesignMetal

#Region "Properties"
    Protected _ComboDeflection As New List(Of String)
    Public ReadOnly Property ComboDeflection As List(Of String) Implements IDesignMetal.ComboDeflection
        Get
            Return _ComboDeflection
        End Get
    End Property

    Protected _DesignSection As New List(Of String)
    Public ReadOnly Property DesignSection(Optional nameFrame As String = "") As List(Of String) Implements IDesignMetal.DesignSection
        Get
            If String.IsNullOrEmpty(nameFrame) Then
                Return _ComboStrength
            Else
                Return New List(Of String) From {GetDesignSection(nameFrame)}
            End If
        End Get
    End Property

    Protected _Group As New List(Of String)
    Public ReadOnly Property Group As List(Of String) Implements IDesignMetal.Group
        Get
            Return _Group
        End Get
    End Property
#End Region

#Region "Methods: Interface"
    Public MustOverride Function SetAutoSelectNull(itemName As String, Optional itemType As eItemType = eItemType.Object) As Boolean Implements IDesignMetal.SetAutoSelectNull

    Public MustOverride Function SetDesignSection(itemName As String, nameFrame As String, resetToLastAnalysisSection As Boolean, Optional itemType As eItemType = eItemType.Object) As Boolean Implements IDesignMetal.SetDesignSection


    Public MustOverride Function SetComboDeflection(nameLoadCombination As String, selectLoadCombination As Boolean) As Boolean Implements IDesignMetal.SetComboDeflection

    Public MustOverride Function SetGroup(nameGroup As String, selectForDesign As Boolean) As Boolean Implements IDesignMetal.SetGroup

    Public MustOverride Function SummaryResults() As DesignResultsMetal Implements IDesignMetal.SummaryResults

#End Region

#Region "Methods: Private Interface"
    ''' <summary>
    ''' Retrieves the design section for a specified frame object.
    ''' </summary>
    ''' <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function GetDesignSection(nameFrame As String) As String

#End Region


End Class
