Option Strict On
Option Explicit On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If

Public Interface IDesignOverwrite

#Region "Properties"
    ''' <summary>
    ''' Name of the design code used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property CodeName As String

    ''' <summary>
    ''' Design type that the code applies to.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property CodeType As CodeType
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Retrieves the value of a design overwrite item.
    ''' </summary>
    ''' <param name="nameFrame">Name of an object with a corresponding design procedure.</param>
    ''' <param name="overwrite">Object containing the paired overwrite name/value codes.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOverwrite(ByVal nameFrame As String,
                          ByRef overwrite As DesignOverwriteItem) As Boolean

    ''' <summary>
    ''' Sets the value of a design overwrite item.
    ''' </summary>
    ''' <param name="nameFrame">Name of an object with a corresponding design procedure.</param>
    ''' <param name="overwrite">Object containing the paired overwrite name/value codes.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetOverwrite(ByVal nameFrame As String,
                          ByRef overwrite As DesignOverwriteItem,
                          Optional ByVal itemType As eItemType = eItemType.Object) As Boolean


    ''' <summary>
    ''' Retrieves the value of a design preference item.
    ''' </summary>
    ''' <param name="preference">Object containing the paired preference name/value codes.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPreference(ByRef preference As DesignPreferenceItem) As Boolean

    ''' <summary>
    ''' Sets the value of a design preference item.
    ''' </summary>
    ''' <param name="preference">Object containing the paired preference name/value codes.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetPreference(ByRef preference As DesignPreferenceItem) As Boolean
#End Region
End Interface
