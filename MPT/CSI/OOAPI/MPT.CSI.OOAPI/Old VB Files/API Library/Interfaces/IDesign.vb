Option Strict On
Option Explicit On

''' <summary>
'''  Implements a design interface for all frame elements.
''' </summary>
''' <remarks></remarks>
Public Interface IDesignCode
#Region "Properties"
    ''' <summary>
    ''' Design code name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As IDesignOverwrite

    ''' <summary>
    ''' Load combinations selected as design combinations for strength design.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ComboStrength As List(Of String)
#End Region

#Region "Methods"
    ''' <summary>
    ''' Deletes all frame design results.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteResults() As Boolean

    ''' <summary>
    ''' Resets all frame design overwrites to default values.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ResetOverwrites() As Boolean

    ''' <summary>
    ''' Starts the frame design.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function StartDesign() As Boolean

    ''' <summary>
    ''' Retrieves the names of the frame objects that did not pass the design check or have not yet been checked, if any.
    ''' Returns 'True' if all frames were checked and none failed.
    ''' </summary>
    ''' <param name="framesFailed">Object containing all relevant information about the frames that failed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerifyPassed(ByRef framesFailed As FramesNotPassed) As Boolean

    ''' <summary>
    ''' Retrieves the names of the frame objects that have different analysis and design sections, if any.
    ''' Returns 'True' if there are no design sections that are different from analysis sections.
    ''' </summary>
    ''' <param name="namesDifferentSections">The name of each frame object that has different analysis and design sections.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function VerifySections(ByRef namesDifferentSections As List(Of String)) As Boolean

    'Set Methods
    ''' <summary>
    ''' Selects or deselects a load combination for strength design.
    ''' </summary>
    ''' <param name="nameLoadCombination">Name of an existing load combination.</param>
    ''' <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for strength design. 
    ''' False: The combination is not selected for strength design.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetComboStrength(ByVal nameLoadCombination As String,
                              ByVal selectLoadCombination As Boolean) As Boolean
#End Region

End Interface
