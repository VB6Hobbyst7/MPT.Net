Option Strict On
Option Explicit On

Public Class AISC360_05_IBC_2006
    Implements IDesignOverwrite

    Public ReadOnly Property CodeName As String Implements IDesignOverwrite.CodeName
        Get
            Return "AISC 360-05/IBC 2006"
        End Get
    End Property

    Public ReadOnly Property CodeType As CodeType Implements IDesignOverwrite.CodeType
        Get
            Return CSiApiTester.CodeType.SteelFrame
        End Get
    End Property


    Public Function GetOverwrite(nameFrame As String,
                                 ByRef overwrite As DesignOverwriteItem) As Boolean Implements IDesignOverwrite.GetOverwrite

    End Function

    Public Function GetPreference(ByRef preference As DesignPreferenceItem) As Boolean Implements IDesignOverwrite.GetPreference

    End Function

    Public Function SetOverwrite(nameFrame As String,
                                 ByRef overwrite As DesignOverwriteItem,
                                 Optional itemType As SAP2000v16.eItemType = SAP2000v16.eItemType.Object) As Boolean Implements IDesignOverwrite.SetOverwrite

    End Function

    Public Function SetPreference(ByRef preference As DesignPreferenceItem) As Boolean Implements IDesignOverwrite.SetPreference

    End Function


End Class
