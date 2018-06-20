Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.Reporting
Imports MPT.XML.ReaderWriter

''' <summary>
''' Class containing properties and methods of an example used in end-to-end testing.
''' </summary>
''' <remarks></remarks>
Public Class cE2EExample
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Fields"
    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties"
    ''' <summary>
    ''' Path to the model control XML for the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathMCxml As String

    ''' <summary>
    ''' Model ID for the example. Same as in model control XML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String
    ''' <summary>
    ''' Secondary ID for the example. Same as in model control XML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property idSecondary As String
    ''' <summary>
    ''' Description of the general type, purpose and setup of the model for the testing purposes of the suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String

    ''' <summary>
    ''' True of the example, if compared, should be classified as failing and therefore appearing in the failed test sets.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property failed As Boolean
    ''' <summary>
    ''' List of keywords for the types of behaviors the example has.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property types As ObservableCollection(Of String)

    ''' <summary>
    ''' The expected run status of the example, if run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedRunStatus As String
    ''' <summary>
    ''' The expected compare status of the example, if compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedCompareStatus As String
    ''' <summary>
    ''' The expected percent difference status of the example, if compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedPercentChange As String
    ''' <summary>
    ''' The expected overall result of the example, if compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedOverallResult As String

    ''' <summary>
    ''' Whether or not the example in its destination is expected to initially have been run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedRan As Boolean
    ''' <summary>
    ''' Whether or not the example in its destination is expected to initially have been compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedCompared As Boolean

    ''' <summary>
    ''' List of results with specific items to compare for validation. Usually only the first and/or last result of the set in an example is checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property results As List(Of cE2EExampleResult)
#End Region

#Region "Properties: MC XML"
    ''' <summary>
    ''' Secondary classification of the example. Options available are dependent upon the Level 1 classification. These can be represented by similar examples grouped on the same tab.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property classificationLevel2 As String
    ''' <summary>
    ''' The expected time to check the example, as recorded in the MC XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectedTimeCheck As String
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        types = New ObservableCollection(Of String)

        results = New List(Of cE2EExampleResult)
    End Sub

    Friend Function Clone() As Object Implements ICloneable.Clone
        Dim myClone As New cE2EExample

        With myClone
            .pathMCxml = pathMCxml
            .id = id
            .idSecondary = idSecondary
            .description = description
            .failed = failed

            .types = New ObservableCollection(Of String)
            For Each type As String In types
                .types.Add(type)
            Next

            .expectedRunStatus = expectedRunStatus
            .expectedCompareStatus = expectedCompareStatus
            .expectedPercentChange = expectedPercentChange
            .expectedOverallResult = expectedOverallResult
            .expectedTimeCheck = expectedTimeCheck

            .expectedRan = expectedRan
            .expectedCompared = expectedCompared

            .results = New List(Of cE2EExampleResult)
            For Each result As cE2EExampleResult In results
                .results.Add(CType(result.Clone, cE2EExampleResult))
            Next
        End With
        Return myClone

    End Function

    ''' <summary>
    ''' Initializes XML reading methods of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeXMLData()
        Try
            If (Not String.IsNullOrEmpty(pathMCxml) AndAlso
                _xmlReaderWriter.InitializeXML(pathMCxml)) Then
                ReadExampleXML()
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Populates the class with data from the XML.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadExampleXML()
        Try
            ReadExampleXmlNodes()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Populates remaining data of the class with information from the corresponding model control XML file.
    ''' </summary>
    ''' <param name="mcXMLPath">Path to the model control XML.</param>
    ''' <remarks></remarks>
    Friend Sub FinalizeExampleFromMCXML(ByVal mcXMLPath As String)
        pathMCxml = mcXMLPath
        InitializeXMLData()
    End Sub

    ''' <summary>
    ''' Sets the property of the expected overall result of the example based on the ran, compared &amp; % difference result statuses.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetExpectedOverallResult()
        Dim tempExample As New cExample

        expectedOverallResult = tempExample.GetOverallResult(, False, expectedRunStatus, expectedCompareStatus, expectedPercentChange)
    End Sub
#End Region

#Region "XML Read/Write Master Functions"
    ''' <summary>
    ''' Reads from or writes to XML, with unique properties.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadExampleXmlNodes()
        Dim pathNode As String

        pathNode = "//n:classification/n:value/n:level_2"
        classificationLevel2 = _xmlReaderWriter.ReadNodeText(pathNode, "")


        pathNode = "//n:run_time/n:minutes"
        expectedTimeCheck = _xmlReaderWriter.ReadNodeText(pathNode, "")

    End Sub

#End Region

End Class
