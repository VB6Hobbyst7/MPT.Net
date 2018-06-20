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

''' <summary>
''' Implements a design interface for all metal-based frame elements.
''' </summary>
''' <remarks></remarks>
Public Interface IDesignMetal
    Inherits IDesignCode

#Region "Properties"
    ''' <summary>
    ''' Names of all load combinations selected as design combinations for deflection design.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ComboDeflection As List(Of String)

    ''' <summary>
    ''' Name of the section used for the design of a frame object. 
    ''' This might differ from the section used for analysis.
    ''' </summary>
    ''' <param name="nameFrame">Name of a frame object with a frame design procedure that corresponds to the design section returned.
    ''' If not specified, the entire list of design sections is returned.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DesignSection(Optional ByVal nameFrame As String = "") As List(Of String)

    ''' <summary>
    ''' Names of all groups selected for frame design.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Group As List(Of String)
#End Region

#Region "Methods"
    ' Get Methods
    ''' <summary>
    ''' Retrieves summary results for frame design.
    ''' </summary>
    ''' <param name="designResults">Object containing all relevant details to the frame design.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SummaryResults() As DesignResultsMetal


    ' Set Methods
    ''' <summary>
    ''' Selects or deselects a load combination for deflection design.
    ''' </summary>
    ''' <param name="nameLoadCombination">Name of an existing load combination.</param>
    ''' <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design. 
    ''' False: The combination is not selected for deflection design.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetComboDeflection(ByVal nameLoadCombination As String,
                                ByVal selectLoadCombination As Boolean) As Boolean

    ''' <summary>
    ''' Modifies the design section for all specified frame objects that have a frame design procedure.
    ''' </summary>
    ''' <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
    ''' <param name="nameFrame">Name of an existing frame section property to be used as the design section for the specified frame objects. This item applies only when resetToLastAnalysisSection = False.</param>
    ''' <param name="resetToLastAnalysisSection">True: The design section for the specified frame objects is reset to the last analysis section for the frame object. 
    ''' False: The design section is set to that specified by nameFrame.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetDesignSection(ByVal itemName As String,
                              ByVal nameFrame As String,
                              ByVal resetToLastAnalysisSection As Boolean,
                              Optional ByVal itemType As eItemType = eItemType.Object) As Boolean

    ''' <summary>
    ''' Selects or deselects a group for frame design.
    ''' </summary>
    ''' <param name="nameGroup">Name of an existing group.</param>
    ''' <param name="selectForDesign">True: The specified group is selected as a design group for steel design. 
    ''' False: The group is not selected for steel design.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetGroup(ByVal nameGroup As String,
                      ByVal selectForDesign As Boolean) As Boolean

    ''' <summary>
    ''' Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
    ''' </summary>
    ''' <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetAutoSelectNull(ByVal itemName As String,
                               Optional ByVal itemType As eItemType = eItemType.Object) As Boolean
#End Region

End Interface
