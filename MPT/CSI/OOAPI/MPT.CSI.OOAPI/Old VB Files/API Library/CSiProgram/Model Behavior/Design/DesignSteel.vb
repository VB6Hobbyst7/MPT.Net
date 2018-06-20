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

Imports CSiApiTester.CSiEnumConverter

Public Class DesignSteel
    Inherits DesignFrameMetal

#Region "Initialization"
    Public Sub New()
        _codeType = CodeType.SteelFrame
    End Sub
#End Region

#Region "Methods: Interface"
    Public Overrides Function DeleteResults() As Boolean
        ret = SapModel.DesignSteel.DeleteResults
        Return retCheck(ret, "SapModel.DesignSteel.DeleteResults")
    End Function

    Public Overrides Function StartDesign() As Boolean
        ret = SapModel.DesignSteel.StartDesign
        Return retCheck(ret, "SapModel.DesignSteel.StartDesign")
    End Function

    Public Overrides Function ResetOverwrites() As Boolean
        ret = SapModel.DesignSteel.ResetOverwrites
        Return retCheck(ret, "SapModel.DesignSteel.ResetOverwrites")
    End Function

    Public Overrides Function VerifyPassed(ByRef framesFailed As FramesNotPassed) As Boolean
        Dim frameNames() As String = framesFailed.NamesNotPassed.ToArray
        With framesFailed
            ret = SapModel.DesignSteel.VerifyPassed(.NumberNotPassed, .NumberFailed, .NumberNotChecked, frameNames)
        End With
        framesFailed.NamesNotPassed = frameNames.ToList()
        retCheck(ret, "SapModel.DesignSteel.VerifyPassed")

        Return (framesFailed.NumberNotPassed = 0)
    End Function

    Public Overrides Function VerifySections(ByRef namesDifferentSections As List(Of String)) As Boolean
        Dim numberItems As Integer
        Dim nameSections() As String = namesDifferentSections.ToArray

        ret = SapModel.DesignSteel.VerifySections(numberItems, nameSections)

        namesDifferentSections = nameSections.ToList
        retCheck(ret, "SapModel.DesignSteel.VerifySections")

        Return (namesDifferentSections.Count = 0)
    End Function

    Public Overrides Function SummaryResults() As DesignResultsMetal
        Dim designResults As New DesignResultsMetal
        With designResults
            Dim numberItems As Integer = .ItemResults.Count
            Dim frameNames() As String = .FrameNames.ToArray
            Dim ratios() As Double = .Ratios.ToArray
            Dim ratiosType() As Integer = .RatiosType.ToArray
            Dim locations() As Double = .Locations.ToArray
            Dim comboNames() As String = .ComboNames.ToArray
            Dim errorSummaries() As String = .ErrorSummaries.ToArray
            Dim warningSummaries() As String = .WarningSummaries.ToArray

            GetSummaryResults(.ItemName, numberItems,
                              frameNames,
                              ratios, ratiosType,
                              locations,
                              comboNames,
                              errorSummaries, warningSummaries,
                              .ItemType)

            For index = 0 To UBound(frameNames)
                designResults.ItemResults.Add(New DesignResultMetal() With {.ComboName = comboNames(index),
                                                                           .ErrorSummary = errorSummaries(index),
                                                                           .FrameName = frameNames(index),
                                                                           .Location = locations(index),
                                                                           .Ratio = ratios(index),
                                                                           .RatioType = CType(ratiosType(index), RatioType),
                                                                           .WarningSummary = warningSummaries(index)
                                                                          }
                                                                      )
            Next
        End With

        Return designResults
    End Function

    ' Set Methods
    Public Overrides Function SetComboStrength(nameLoadCombination As String,
                                     selectLoadCombination As Boolean) As Boolean
        ret = SapModel.DesignSteel.SetComboStrength(nameLoadCombination, selectLoadCombination)
        Return retCheck(ret, "SapModel.DesignSteel.SetComboStrength")
    End Function

    Public Overrides Function SetAutoSelectNull(itemName As String,
                                      Optional itemType As eItemType = eItemType.Object) As Boolean
        ret = SapModel.DesignSteel.SetAutoSelectNull(itemName, ToCSi(itemType))
        Return retCheck(ret, "SapModel.DesignSteel.SetAutoSelectNull")
    End Function

    Public Overrides Function SetComboDeflection(nameLoadCombination As String,
                                       selectLoadCombination As Boolean) As Boolean
        ret = SapModel.DesignSteel.SetComboDeflection(nameLoadCombination, selectLoadCombination)
        Return retCheck(ret, "SapModel.DesignSteel.SetComboDeflection")
    End Function

    Public Overrides Function SetDesignSection(itemName As String,
                                     nameFrame As String,
                                     resetToLastAnalysisSection As Boolean,
                                     Optional itemType As eItemType = eItemType.Object) As Boolean
        ret = SapModel.DesignSteel.SetDesignSection(itemName, nameFrame, resetToLastAnalysisSection, ToCSi(itemType))
        Return retCheck(ret, "SapModel.DesignSteel.SetDesignSection")
    End Function

    Public Overrides Function SetGroup(nameGroup As String,
                             selectForDesign As Boolean) As Boolean
        ret = SapModel.DesignSteel.SetGroup(nameGroup, selectForDesign)
        Return retCheck(ret, "SapModel.DesignSteel.SetGroup")
    End Function
#End Region

#Region "Methods: Public"
    ' Get/Set Methods
    ' ------
    ''' <summary>
    ''' This function retrieves the design results from steel design output database tables and returns them as a 'Frame Name'/'Text Result' dictionary. 
    ''' Note that the summary table of all design codes is not included in this function.
    ''' </summary>
    ''' <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <param name="table">Table ID of the steel design output database Tables. 
    ''' The table names are input as the representative table numbers and are code-based. 
    ''' Please see the appendix at the bottom of the steel class.</param>
    ''' <param name="field">Field name with TEXT output data type in the specified steel design result database Tables. 
    ''' The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DetailedResultsText(ByVal itemName As String,
                                           ByVal itemType As eItemType,
                                           ByVal table As Integer,
                                           ByVal field As String) As Dictionary(Of String, String)
        Dim detailedTextResults As New Dictionary(Of String, String)
        Dim frameNames() As String = detailedTextResults.Keys.ToArray
        Dim textResults() As String = detailedTextResults.Values.ToArray
        Dim numberFrames As Integer

        GetDetailedResultsText(itemName, itemType, table, field, numberFrames, frameNames, textResults)

        For i = 0 To UBound(frameNames)
            detailedTextResults.Add(frameNames(i), textResults(i))
        Next

        Return detailedTextResults
    End Function


    ' ------
    ''' <summary>
    ''' This function retrieves the design results from steel design output database tables and returns them as a 'Frame Name'/'Text Result' dictionary. 
    ''' Note that the summary table of all design codes is not included in this function.
    ''' </summary>
    ''' <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <param name="table">Table ID of the steel design output database Tables. 
    ''' The table names are input as the representative table numbers and are code-based. 
    ''' Please see the appendix at the bottom of the steel class.</param>
    ''' <param name="field">Field name with Numerical Value output data type in the specified steel design result database Tables. 
    ''' The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DetailedResultsNumerical(ByVal itemName As String,
                                            ByVal itemType As eItemType,
                                            ByVal table As Integer,
                                            ByVal field As String) As Dictionary(Of String, Double)
        Dim detailedValueResults As New Dictionary(Of String, Double)
        Dim frameNames() As String = detailedValueResults.Keys.ToArray
        Dim numericalResults() As Double = detailedValueResults.Values.ToArray
        Dim numberFrames As Integer

        GetDetailedResultsNumerical(itemName, itemType, table, field, numberFrames, frameNames, numericalResults)

        For i = 0 To UBound(frameNames)
            detailedValueResults.Add(frameNames(i), numericalResults(i))
        Next

        Return detailedValueResults
    End Function


    ' ------
    ''' <summary>
    ''' Retrieves lateral displacement targets for steel design.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TargetDisplacements() As TargetDisplacements
        Dim currentTargetDisplacements As New TargetDisplacements()
        Dim numberItems As Integer
        Dim loadCase() As String = currentTargetDisplacements.LoadCases.ToArray
        Dim namePoint() As String = currentTargetDisplacements.PointNames.ToArray
        Dim diplacementTargets() As Double = currentTargetDisplacements.DisplacementTargets.ToArray
        Dim allSpecifedTargetsActive As Boolean

        GetTargetDisplacement(numberItems, loadCase, namePoint, diplacementTargets, allSpecifedTargetsActive)

        For i = 0 To numberItems - 1
            currentTargetDisplacements.Targets.Add(New TargetDisplacement With {.LoadCase = loadCase(i),
                                                                               .NamePoint = namePoint(i),
                                                                               .DisplacementTarget = diplacementTargets(i)})
        Next
        currentTargetDisplacements.AllSpecifedTargetsActive = allSpecifedTargetsActive

        Return currentTargetDisplacements
    End Function

    ''' <summary>
    ''' Sets lateral displacement targets for steel design.
    ''' </summary>
    ''' <param name="targetDisplacements">Object containing all values relevant to target displacements.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTargetDisplacement(ByVal targetDisplacements As TargetDisplacements) As Boolean
        With targetDisplacements
            Return SetTargetDisplacement(.Targets.Count, .LoadCases.ToArray, .PointNames.ToArray, .DisplacementTargets.ToArray, .AllSpecifedTargetsActive)
        End With
    End Function


    ' ------
    ''' <summary>
    ''' Retrieves time period targets for steel design.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTargetPeriod() As TargetPeriods
        Dim currentTargetPeriods As New TargetPeriods()
        Dim numberItems As Integer
        Dim modalCase As String
        Dim modeNumbers() As Integer = currentTargetPeriods.ModeNumbers.ToArray
        Dim periodTargets() As Double = currentTargetPeriods.PeriodTargets.ToArray
        Dim allSpecifedTargetsActive As Boolean

        GetTargetPeriod(numberItems, modalCase, modeNumbers, periodTargets, allSpecifedTargetsActive)

        For i = 0 To numberItems - 1
            currentTargetPeriods.Targets.Add(New TargetPeriod With {.ModalCase = modalCase,
                                                                    .ModeNumber = modeNumbers(i),
                                                                    .PeriodTarget = periodTargets(i)})
        Next
        currentTargetPeriods.AllSpecifedTargetsActive = allSpecifedTargetsActive

        Return currentTargetPeriods
    End Function

    ''' <summary>
    ''' Sets time period targets for steel design.
    ''' </summary>
    ''' <param name="targetPeriods">Object containing all values relevant to target periods.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetTargetPeriod(ByVal targetPeriods As TargetPeriods) As Boolean
        With targetPeriods
            Return SetTargetPeriod(.Targets.Count, .ModalCase, .ModeNumbers.ToArray, .PeriodTargets.ToArray, .AllSpecifedTargetsActive)
        End With
    End Function

 #End Region

#Region "Methods: Private"
    ' Interface
    ''' <summary>
    ''' Retrieves the design section for a specified frame object.
    ''' </summary>
    ''' <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function GetDesignSection(nameFrame As String) As String
        Dim nameSection As String = ""

        ret = SapModel.DesignSteel.GetDesignSection(nameFrame, nameSection)
        retCheck(ret, "SapModel.DesignSteel.GetDesignSection")

        Return nameSection
    End Function

    ' Local
    ''' <summary>
    ''' This function retrieves the design results from steel design output database tables. 
    ''' Note that the summary table of all design codes is not included in this function.
    ''' </summary>
    ''' <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <param name="table">Table ID of the steel design output database Tables. 
    ''' The table names are input as the representative table numbers and are code-based. 
    ''' Please see the appendix at the bottom of the steel class.</param>
    ''' <param name="field">Field name with TEXT output data type in the specified steel design result database Tables. 
    ''' The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
    ''' <param name="numberFrames">Number of frame objects for which results are obtained.</param>
    ''' <param name="frameNames">Frame object names for which results are obtained.</param>
    ''' <param name="textResults">Design results with TEXT output data type of the request field in the request table for the specified frame objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDetailedResultsText(ByVal itemName As String,
                                           ByVal itemType As eItemType,
                                           ByVal table As Integer,
                                           ByVal field As String,
                                           ByRef numberFrames As Integer,
                                           ByRef frameNames() As String,
                                           ByRef textResults() As String) As Boolean
        ret = SapModel.DesignSteel.GetDetailResultsText(itemName, itemType, table, field, numberFrames, frameNames, textResults)
        Return retCheck(ret, "SapModel.DesignSteel.GetDetailResultsText")
    End Function

    ''' <summary>
    ''' This function retrieves the design results from steel design output database tables. 
    ''' Note that the summary table of all design codes is not included in this function.
    ''' </summary>
    ''' <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
    ''' <param name="itemType">Selection type to use for applying the method.</param>
    ''' <param name="table">Table ID of the steel design output database Tables. 
    ''' The table names are input as the representative table numbers and are code-based. 
    ''' Please see the appendix at the bottom of the steel class.</param>
    ''' <param name="field">Field name with TEXT output data type in the specified steel design result database Tables. 
    ''' The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
    ''' <param name="numberFrames">Number of frame objects for which results are obtained.</param>
    ''' <param name="frameNames">Frame object names for which results are obtained.</param>
    ''' <param name="numericalResults">Design results with Numerical output data type of the request field in the request table for the specified frame objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDetailedResultsNumerical(ByVal itemName As String,
                                               ByVal itemType As eItemType,
                                               ByVal table As Integer,
                                               ByVal field As String,
                                               ByRef numberFrames As Integer,
                                               ByRef frameNames() As String,
                                               ByRef numericalResults() As Double) As Boolean
        ret = SapModel.DesignSteel.GetDetailResultsValue(itemName, itemType, table, field, numberFrames, frameNames, numericalResults)
        Return retCheck(ret, "SapModel.DesignSteel.GetDetailResultsValue")
    End Function

    ''' <summary>
    ''' Retrieves summary results for steel design.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="NumberItems"></param>
    ''' <param name="FrameName"></param>
    ''' <param name="Ratio"></param>
    ''' <param name="RatioType"></param>
    ''' <param name="Location"></param>
    ''' <param name="ComboName"></param>
    ''' <param name="ErrorSummary"></param>
    ''' <param name="WarningSummary"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSummaryResults(ByVal Name As String,
                                       ByRef NumberItems As Integer,
                                       ByRef FrameName() As String,
                                       ByRef Ratio() As Double,
                                       ByRef RatioType() As Integer,
                                       ByRef Location() As Double,
                                       ByRef ComboName() As String,
                                       ByRef ErrorSummary() As String,
                                       ByRef WarningSummary() As String,
                                       Optional ByVal ItemType As eItemType = eItemType.Object) As Boolean
        ret = SapModel.DesignSteel.GetSummaryResults(Name, NumberItems, FrameName, Ratio, RatioType, Location, ComboName, ErrorSummary, WarningSummary, ToCSi(ItemType))
        Return retCheck(ret, "SapModel.DesignSteel.GetSummaryResults")
    End Function



    ' Get/Set Methods
    ' ------
    ''' <summary>
    ''' Retrieves lateral displacement targets for steel design.
    ''' </summary>
    ''' <param name="numberItems">Number of lateral displacement targets specified.</param>
    ''' <param name="loadCase">Name of the static linear load case associated with each lateral displacement target.</param>
    ''' <param name="namePoint">Name of the point object associated to which the lateral displacement target applies.</param>
    ''' <param name="diplacementTargets">Lateral displacement targets. [L]</param>
    ''' <param name="allSpecifedTargetsActive">True: All specified lateral displacement targets are active. 
    ''' False: They are inactive.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTargetDisplacement(ByRef numberItems As Integer,
                                          ByRef loadCase() As String,
                                          ByRef namePoint() As String,
                                          ByRef diplacementTargets() As Double,
                                          ByRef allSpecifedTargetsActive As Boolean) As Boolean

        ret = SapModel.DesignSteel.GetTargetDispl(numberItems, loadCase, namePoint, diplacementTargets, allSpecifedTargetsActive)
        Return retCheck(ret, "SapModel.DesignSteel.GetTargetDispl")
    End Function

    ''' <summary>
    ''' Sets lateral displacement targets for steel design.
    ''' </summary>
    ''' <param name="numberItems">Number of targets specified.</param>
    ''' <param name="loadCase">Name of the static linear load case associated with each target.</param>
    ''' <param name="namePoint">Name of the point object associated with the target.</param>
    ''' <param name="diplacementTargets">Lateral displacement targets. [L]</param>
    ''' <param name="allSpecifedTargetsActive">True: All specified targets are active. 
    ''' False: They are inactive.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTargetDisplacement(ByVal numberItems As Integer,
                                          ByRef loadCase() As String,
                                          ByRef namePoint() As String,
                                          ByRef diplacementTargets() As Double,
                                          Optional ByVal allSpecifedTargetsActive As Boolean = True) As Boolean
        ret = SapModel.DesignSteel.SetTargetDispl(numberItems, loadCase, namePoint, diplacementTargets, allSpecifedTargetsActive)
        Return retCheck(ret, "SapModel.DesignSteel.SetTargetDispl")
    End Function


    ' ------
    ''' <summary>
    ''' Retrieves time period targets for steel design.
    ''' </summary>
    ''' <param name="numberItems">Number of targets specified.</param>
    ''' <param name="modalCase">Name of the modal load case for which the target applies.</param>
    ''' <param name="modeNumbers">Mode number associated with each target.</param>
    ''' <param name="periodTargets">Target periods. [s]</param>
    ''' <param name="allSpecifedTargetsActive">True: All specified targets are active. 
    ''' False: They are inactive.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTargetPeriod(ByRef numberItems As Integer,
                                     ByRef modalCase As String,
                                     ByRef modeNumbers() As Integer,
                                     ByRef periodTargets() As Double,
                                     ByRef allSpecifedTargetsActive As Boolean) As Boolean


        ret = SapModel.DesignSteel.GetTargetPeriod(numberItems, modalCase, modeNumbers, periodTargets, allSpecifedTargetsActive)
        Return retCheck(ret, "SapModel.DesignSteel.GetTargetPeriod")
    End Function

    ''' <summary>
    ''' Sets time period targets for steel design.
    ''' </summary>
    ''' <param name="numberItems">Number of targets specified.</param>
    ''' <param name="modalCase">Name of the modal load case for which the target applies.</param>
    ''' <param name="modeNumbers">Mode number associated with each target.</param>
    ''' <param name="periodTargets">Target periods. [s]</param>
    ''' <param name="allSpecifedTargetsActive">True: All specified targets are active. 
    ''' False: They are inactive.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTargetPeriod(ByVal numberItems As Integer,
                                     ByVal modalCase As String,
                                     ByVal modeNumbers() As Integer,
                                     ByVal periodTargets() As Double,
                                     Optional ByVal allSpecifedTargetsActive As Boolean = True) As Boolean
        ret = SapModel.DesignSteel.SetTargetPeriod(numberItems, modalCase, modeNumbers, periodTargets, allSpecifedTargetsActive)
        Return retCheck(ret, "SapModel.DesignSteel.SetTargetPeriod")
    End Function

#End Region

End Class
