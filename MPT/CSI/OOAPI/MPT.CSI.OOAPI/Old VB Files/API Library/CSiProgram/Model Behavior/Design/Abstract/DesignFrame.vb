Option Explicit On
Option Strict On

Public MustInherit Class DesignFrame
    Implements IDesignCode

#Region "Fields"
    Protected _codeType As CodeType = CodeType.None
#End Region

#Region "Properties"
    Protected _Code As IDesignOverwrite
    Public Property Code As IDesignOverwrite Implements IDesignCode.Code
        Get
            Return _Code
        End Get
        Set(value As IDesignOverwrite)
            If value.CodeType = _codeType Then _Code = value
        End Set
    End Property

    Protected _ComboStrength As New List(Of String)
    Public ReadOnly Property ComboStrength As List(Of String) Implements IDesignCode.ComboStrength
        Get
            Return _ComboStrength
        End Get
    End Property
#End Region

#Region "Methods: Interface"

    Public MustOverride Function DeleteResults() As Boolean Implements IDesignCode.DeleteResults

    Public MustOverride Function ResetOverwrites() As Boolean Implements IDesignCode.ResetOverwrites

    Public MustOverride Function SetComboStrength(nameLoadCombination As String, selectLoadCombination As Boolean) As Boolean Implements IDesignCode.SetComboStrength

    Public MustOverride Function StartDesign() As Boolean Implements IDesignCode.StartDesign

    Public MustOverride Function VerifyPassed(ByRef framesFailed As FramesNotPassed) As Boolean Implements IDesignCode.VerifyPassed

    Public MustOverride Function VerifySections(ByRef namesDifferentSections As List(Of String)) As Boolean Implements IDesignCode.VerifySections

#End Region

End Class
