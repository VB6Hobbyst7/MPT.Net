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

Public Class DesignConcrete
    Inherits DesignFrame

#Region "Initialization"
    Public Sub New()
        _codeType = CodeType.ConcreteFrame
    End Sub
#End Region

#Region "Methods: Interface"
    Public Overrides Function DeleteResults() As Boolean
        ret = SapModel.DesignConcrete.DeleteResults
        Return retCheck(ret, "SapModel.DesignConcrete.DeleteResults")
    End Function

    Public Overrides Function StartDesign() As Boolean
        ret = SapModel.DesignConcrete.StartDesign
        Return retCheck(ret, "SapModel.DesignConcrete.StartDesign")
    End Function


    Public Overrides Function ResetOverwrites() As Boolean
        ret = SapModel.DesignConcrete.ResetOverwrites
        Return retCheck(ret, "SapModel.DesignConcrete.ResetOverwrites")
    End Function

    Public Overrides Function VerifyPassed(ByRef framesFailed As FramesNotPassed) As Boolean
        Dim frameNames() As String = framesFailed.NamesNotPassed.ToArray
        With framesFailed
            ret = SapModel.DesignConcrete.VerifyPassed(.NumberNotPassed, .NumberFailed, .NumberNotChecked, frameNames)
        End With
        framesFailed.NamesNotPassed = frameNames.ToList()
        retCheck(ret, "SapModel.DesignConcrete.VerifyPassed")

        Return (framesFailed.NumberNotPassed = 0)
    End Function

    Public Overrides Function VerifySections(ByRef namesDifferentSections As List(Of String)) As Boolean
        Dim numberItems As Integer
        Dim nameSections() As String = namesDifferentSections.ToArray

        ret = SapModel.DesignConcrete.VerifySections(numberItems, nameSections)

        namesDifferentSections = nameSections.ToList
        retCheck(ret, "SapModel.DesignConcrete.VerifySections")

        Return (namesDifferentSections.Count = 0)
    End Function

    ' Set Methods
    Public Overrides Function SetComboStrength(nameLoadCombination As String,
                                  selectLoadCombination As Boolean) As Boolean
        ret = SapModel.DesignConcrete.SetComboStrength(nameLoadCombination, selectLoadCombination)
        Return retCheck(ret, "SapModel.DesignConcrete.SetComboStrength")
    End Function

#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Retrieves summary results for concrete design of beams.
    ''' Torsion results are not included for all codes.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SummaryResultsBeam() As DesignResultsConcreteBeam
        Dim designResults As New DesignResultsConcreteBeam
        With designResults
            Dim numberItems As Integer = .ItemResults.Count
            Dim frameNames() As String = .FrameNames.ToArray
            Dim errorSummaries() As String = .ErrorSummaries.ToArray
            Dim warningSummaries() As String = .WarningSummaries.ToArray
            Dim locations() As Double = .Locations.ToArray
            Dim VmajorCombos() As String = .VMajorCombos.ToArray
            Dim VmajorAreas() As Double = .AvMajor.ToArray
            Dim topCombos() As String = .TopLongCombos.ToArray
            Dim topAreas() As Double = .TopLongAreas.ToArray
            Dim botCombos() As String = .BottomLongCombos.ToArray
            Dim botAreas() As Double = .BottomLongAreas.ToArray
            Dim TLCombos() As String = .TorsionLongCombos.ToArray
            Dim TLAreas() As Double = .TorsionLongAreas.ToArray
            Dim TTCombos() As String = .TorsionTransverseCombos.ToArray
            Dim TTAreas() As Double = .TorsionTransverseAreas.ToArray

            GetSummaryResultsBeam(.ItemName, numberItems,
                                  frameNames, locations,
                                  topCombos, topAreas,
                                  botCombos, botAreas,
                                  VmajorCombos, VmajorAreas,
                                  TLCombos, TLAreas,
                                  TTCombos, TTAreas,
                                  errorSummaries, warningSummaries,
                                  .ItemType)

            For index = 0 To UBound(frameNames)
                designResults.ItemResults.Add(New DesignResultConcreteBeam() With {.FrameName = frameNames(index),
                                                                                   .ErrorSummary = errorSummaries(index),
                                                                                   .WarningSummary = warningSummaries(index),
                                                                                   .Location = locations(index),
                                                                                   .AvMajor = VmajorAreas(index),
                                                                                   .VmajorCombo = VmajorCombos(index),
                                                                                   .BottomLongArea = botAreas(index),
                                                                                   .BottomLongCombo = botCombos(index),
                                                                                   .TopLongArea = topAreas(index),
                                                                                   .TopLongCombo = topCombos(index),
                                                                                   .TorsionLongArea = TLAreas(index),
                                                                                   .TorsionLongCombo = TLCombos(index),
                                                                                   .TorsionTransverseArea = TTAreas(index),
                                                                                   .TorsionTransverseCombo = TTCombos(index)
                                                                                  }
                                                                              )
            Next
        End With

        Return designResults
    End Function

    ''' <summary>
    ''' Retrieves summary results for concrete design of columns.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SummaryResultsColumn() As DesignResultsConcreteColumn
        Dim designResults As New DesignResultsConcreteColumn
        With designResults
            Dim numberItems As Integer = .ItemResults.Count
            Dim frameNames() As String = .FrameNames.ToArray
            Dim errorSummaries() As String = .ErrorSummaries.ToArray
            Dim warningSummaries() As String = .WarningSummaries.ToArray
            Dim locations() As Double = .Locations.ToArray
            Dim designOptions() As Integer = .DesignOptions.ToArray
            Dim VmajorCombos() As String = .VMajorCombos.ToArray
            Dim VmajorAreas() As Double = .AvMajor.ToArray
            Dim VminorCombos() As String = .VMinorCombos.ToArray
            Dim VminorAreas() As Double = .AvMinor.ToArray
            Dim PMMCombos() As String = .PMMCombos.ToArray
            Dim PMMAreas() As Double = .PMMAreas.ToArray
            Dim PMMRatios() As Double = .PMMRatios.ToArray
            

            GetSummaryResultsColumn(.ItemName, numberItems,
                                      frameNames, designOptions, locations,
                                      PMMCombos, PMMAreas, PMMRatios,
                                      VmajorCombos, VmajorAreas,
                                      VminorCombos, VminorAreas,
                                      errorSummaries, warningSummaries,
                                      .ItemType)

            For index = 0 To UBound(frameNames)
                designResults.ItemResults.Add(New DesignResultConcreteColumn() With {.FrameName = frameNames(index),
                                                                                     .ErrorSummary = errorSummaries(index),
                                                                                     .WarningSummary = warningSummaries(index),
                                                                                     .Location = locations(index),
                                                                                     .AvMajor = VmajorAreas(index),
                                                                                     .VmajorCombo = VmajorCombos(index),
                                                                                     .AvMinor = VminorAreas(index),
                                                                                     .VminorCombo = VminorCombos(index),
                                                                                     .DesignOption = CType(designOptions(index), DesignOption),
                                                                                     .PMMArea = PMMAreas(index),
                                                                                     .PMMCombo = PMMCombos(index),
                                                                                     .PMMRatio = PMMRatios(index)
                                                                                      }
                                                                                  )
            Next
        End With

        Return designResults
    End Function

    ''' <summary>
    ''' Retrieves summary results for concrete design of joints.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SummaryResultsJoint() As DesignResultsConcreteJoint
        Dim designResults As New DesignResultsConcreteJoint
        With designResults
            Dim numberItems As Integer = .ItemResults.Count
            Dim frameNames() As String = .FrameNames.ToArray
            Dim errorSummaries() As String = .ErrorSummaries.ToArray
            Dim warningSummaries() As String = .WarningSummaries.ToArray
            Dim CJSRatioMajorCombos() As String = .ControllingJointShearRatiosMajorCombo.ToArray
            Dim CJSRatioMajor() As Double = .JointShearRatiosMajor.ToArray
            Dim CJSRatioMinorCombos() As String = .ControllingJointShearRatiosMinorCombo.ToArray
            Dim CJSRatioMinor() As Double = .JointShearRatiosMinor.ToArray
            Dim CBCCRatioMajorCombos() As String = .ControllingBeamColumnCapacityRatioMajorCombos.ToArray
            Dim BCCRatiosMajor() As Double = .BeamColumnCapacityRatiosMajor.ToArray
            Dim CBCCRatioMinorCombos() As String = .ControllingBeamColumnCapacityRatioMinorCombos.ToArray
            Dim BCCRatiosMinor() As Double = .BeamColumnCapacityRatiosMinor.ToArray

            GetSummaryResultsJoint(.ItemName, numberItems,
                                    frameNames,
                                    CJSRatioMajorCombos, CJSRatioMajor,
                                    CJSRatioMinorCombos, CJSRatioMinor,
                                    CBCCRatioMajorCombos, BCCRatiosMajor,
                                    CBCCRatioMinorCombos, BCCRatiosMinor,
                                    errorSummaries, warningSummaries,
                                    .ItemType)

            For index = 0 To UBound(frameNames)
                designResults.ItemResults.Add(New DesignResultConcreteJoint() With {.FrameName = frameNames(index),
                                                                                    .ErrorSummary = errorSummaries(index),
                                                                                    .WarningSummary = warningSummaries(index),
                                                                                    .ControllingJointShearRatioMajorCombo = CJSRatioMajorCombos(index),
                                                                                    .ControllingJointShearRatioMinorCombo = CJSRatioMinorCombos(index),
                                                                                    .ControllingBeamColumnCapacityRatioMajorCombo = CBCCRatioMajorCombos(index),
                                                                                    .BeamColumnCapacityRatioMajor = BCCRatiosMajor(index),
                                                                                    .ControllingBeamColumnCapacityRatioMinorCombo = CBCCRatioMinorCombos(index),
                                                                                    .BeamColumnCapacityRatioMinor = BCCRatiosMinor(index)
                                                                                     }
                                                                                  )
            Next
        End With

        Return designResults
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Retrieves summary results for concrete design of beams.
    ''' Torsion results are not included for all codes.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="NumberItems"></param>
    ''' <param name="FrameName"></param>
    ''' <param name="Location"></param>
    ''' <param name="TopCombo"></param>
    ''' <param name="TopArea"></param>
    ''' <param name="BotCombo"></param>
    ''' <param name="BotArea"></param>
    ''' <param name="VmajorCombo"></param>
    ''' <param name="VmajorArea"></param>
    ''' <param name="TLCombo"></param>
    ''' <param name="TLArea"></param>
    ''' <param name="TTCombo"></param>
    ''' <param name="TTArea"></param>
    ''' <param name="ErrorSummary"></param>
    ''' <param name="WarningSummary"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSummaryResultsBeam(ByVal Name As String,
                                           ByRef NumberItems As Integer,
                                           ByRef FrameName() As String,
                                           ByRef Location() As Double,
                                           ByRef TopCombo() As String,
                                           ByRef TopArea() As Double,
                                           ByRef BotCombo() As String,
                                           ByRef BotArea() As Double,
                                           ByRef VmajorCombo() As String,
                                           ByRef VmajorArea() As Double,
                                           ByRef TLCombo() As String,
                                           ByRef TLArea() As Double,
                                           ByRef TTCombo() As String,
                                           ByRef TTArea() As Double,
                                           ByRef ErrorSummary() As String,
                                           ByRef WarningSummary() As String,
                                           Optional ByVal ItemType As eItemType = eItemType.Object) As Boolean
        ret = SapModel.DesignConcrete.GetSummaryResultsBeam(Name, NumberItems, FrameName, Location, TopCombo, TopArea, BotCombo, BotArea, VmajorCombo, VmajorArea, TLCombo, TLArea, TTCombo, TTArea, ErrorSummary, WarningSummary, ToCSi(ItemType))
        Return retCheck(ret, "SapModel.DesignConcrete.GetSummaryResultsBeam")
    End Function

    ''' <summary>
    ''' Retrieves summary results for concrete design of columns.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="NumberItems"></param>
    ''' <param name="FrameName"></param>
    ''' <param name="MyOption"></param>
    ''' <param name="Location"></param>
    ''' <param name="PMMCombo"></param>
    ''' <param name="PMMArea"></param>
    ''' <param name="PMMRatio"></param>
    ''' <param name="VmajorCombo"></param>
    ''' <param name="AVmajor"></param>
    ''' <param name="VminorCombo"></param>
    ''' <param name="AVminor"></param>
    ''' <param name="ErrorSummary"></param>
    ''' <param name="WarningSummary"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSummaryResultsColumn(ByVal Name As String,
                                             ByRef NumberItems As Integer,
                                             ByRef FrameName() As String,
                                             ByRef MyOption() As Integer,
                                             ByRef Location() As Double,
                                             ByRef PMMCombo() As String,
                                             ByRef PMMArea() As Double,
                                             ByRef PMMRatio() As Double,
                                             ByRef VmajorCombo() As String,
                                             ByRef AVmajor() As Double,
                                             ByRef VminorCombo() As String,
                                             ByRef AVminor() As Double,
                                             ByRef ErrorSummary() As String,
                                             ByRef WarningSummary() As String,
                                             Optional ByVal ItemType As eItemType = eItemType.Object) As Boolean
        ret = SapModel.DesignConcrete.GetSummaryResultsColumn(Name, NumberItems, FrameName, MyOption, Location, PMMCombo, PMMArea, PMMRatio, VmajorCombo, AVmajor, VminorCombo, AVminor, ErrorSummary, WarningSummary, ToCSi(ItemType))
        Return retCheck(ret, "SapModel.DesignConcrete.GetSummaryResultsBeam")
    End Function

    ''' <summary>
    ''' Retrieves summary results for concrete design of joints.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="NumberItems"></param>
    ''' <param name="FrameName"></param>
    ''' <param name="LCJSRatioMajor"></param>
    ''' <param name="JSRatioMajor"></param>
    ''' <param name="LCJSRatioMinor"></param>
    ''' <param name="JSRatioMinor"></param>
    ''' <param name="LCBCCRatioMajor"></param>
    ''' <param name="BCCRatioMajor"></param>
    ''' <param name="LCBCCRatioMinor"></param>
    ''' <param name="BCCRatioMinor"></param>
    ''' <param name="ErrorSummary"></param>
    ''' <param name="WarningSummary"></param>
    ''' <param name="ItemType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSummaryResultsJoint(ByVal Name As String,
                                            ByRef NumberItems As Integer,
                                            ByRef FrameName() As String,
                                            ByRef LCJSRatioMajor() As String,
                                            ByRef JSRatioMajor() As Double,
                                            ByRef LCJSRatioMinor() As String,
                                            ByRef JSRatioMinor() As Double,
                                            ByRef LCBCCRatioMajor() As String,
                                            ByRef BCCRatioMajor() As Double,
                                            ByRef LCBCCRatioMinor() As String,
                                            ByRef BCCRatioMinor() As Double,
                                            ByRef ErrorSummary() As String,
                                            ByRef WarningSummary() As String,
                                            Optional ByVal ItemType As eItemType = eItemType.Object) As Boolean
        ret = SapModel.DesignConcrete.GetSummaryResultsJoint(Name, NumberItems, FrameName, LCJSRatioMajor, JSRatioMajor, LCJSRatioMinor, JSRatioMinor, LCBCCRatioMajor, BCCRatioMajor, LCBCCRatioMinor, BCCRatioMinor, ErrorSummary, WarningSummary, ToCSi(ItemType))
        Return retCheck(ret, "SapModel.DesignConcrete.GetSummaryResultsBeam")
    End Function
#End Region

End Class
