Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.Xml

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.String.ConversionLibrary
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter

''' <summary>
''' Class containing properties and methods for a destination of a test suite used in validating the end-to-end tests involving checks.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETestDestination
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Field"
    Private _xmlReaderWriter As New cXmlReadWrite()

    Private _IDList As New List(Of String)
    Private _RanList As New List(Of String)
    Private _ComparedList As New List(Of String)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Path to the XML file used to record the destination data.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathXML As String

    ''' <summary>
    ''' List of the model ids of all of the examples that are expected to fail and appear under the 'failed' examples tab &amp; test sets.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property failedExamples As ObservableCollection(Of String)

    ''' <summary>
    ''' If true, the destination files have been run and there should be results files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property destinationRun As Boolean
    ''' <summary>
    ''' Original number of test sets of failed examples at the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testSetFailedNumOriginal As Integer
    ''' <summary>
    ''' Original number of failed tabs at the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tabsFailedNumOriginal As Integer
    ''' <summary>
    ''' Original number of failed examples expected in the failed examples test set &amp; tab.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesFailNumOriginal As Integer
    ''' <summary>
    ''' Maximum % difference expected among the compared examples at the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property maxPercDiff As String
    ''' <summary>
    ''' Overall result among the compared examples at the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property overallResult As String
#End Region

#Region "Properties XML"
    ''' <summary>
    ''' Path to the directory that contains the destination examples for the suite. Made private so that the property is retrieved with modifications from a function.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property pathDestination As String

    ''' <summary>
    ''' ID for the test destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String
    ''' <summary>
    ''' ID for the test suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property idSuite As String
    ''' <summary>
    ''' Title for the test destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
    ''' <summary>
    ''' Description for the test destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String
    ''' <summary>
    ''' General status of the destination directory, such as empty, initialized, ran, mismatched, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property status As String

    ''' <summary>
    ''' List of examples contained within the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examples As ObservableCollection(Of cE2EExample)
#End Region

#Region "Initialization"

    Friend Sub New()
        InitializeData()
    End Sub

    ''' <summary>
    ''' Creates a new class and populates it with data from the XML file that the path specifies.
    ''' </summary>
    ''' <param name="pathXMLRead">Path to the XML file by which the class will be constructed.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pathXMLRead As String)
        InitializeData()
        InitializeXMLData(pathXMLRead)
        SetDestinationRun()
    End Sub

    Private Sub InitializeData()
        examples = New ObservableCollection(Of cE2EExample)
        failedExamples = New ObservableCollection(Of String)
    End Sub

    ''' <summary>
    ''' Initializes XML reading methods of the class.
    ''' </summary>
    ''' <param name="pathXMLRead">Path to the xml file to read.</param>
    ''' <remarks></remarks>
    Private Sub InitializeXMLData(ByVal pathXMLRead As String)
        Try
            pathXML = pathXMLRead

            If _xmlReaderWriter.InitializeXML(pathXML) Then
                ReadWriteSuiteXML(True)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Populates the class with data from the XML.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteSuiteXML(ByVal read As Boolean)
        Try
            ReadWriteSuiteXmlNodes(read)
            ReadWriteSuiteXmlObjects(read)

            If Not read Then
                _xmlReaderWriter.SaveXML(pathXML)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determines other class properties based on the properties of the examples assigned to the class.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeExamples()
        SetFailedExamples()
        SetRanComparedExamples()
        UpdateOverallResults()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns the path of the destination associated with the destination object. Also handles unspecified &amp; new destination cases.
    ''' </summary>
    ''' <param name="numTempDirsMax">If creating a new temporary destination, more than one can exist if specified. 
    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDestinationPath(Optional ByVal numTempDirsMax As Integer = 1) As String
        Dim path As String = ""

        If String.IsNullOrEmpty(pathDestination) Then                'Unspecified. Use existing.
            path = myCsiTester.testerDestinationDir
        ElseIf pathDestination = "*" Then           'Create new destination folder.
            SetDestinationNew(numTempDirsMax)
        Else                                        'Use specified destination
            path = pathDestination
        End If

        Return path
    End Function

    ''' <summary>
    ''' Sets the boolean indicating whether or not examples have been run at the destination. Updates any other parameters affected by this parameter.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestinationRun() As Boolean
        If status = "Run" Then
            destinationRun = True
        Else
            destinationRun = False
        End If

        SetFailedSetsTabs()

        Return destinationRun
    End Function

    ''' <summary>
    ''' Creates a new empty destination directory and sets the destination path to it, and sets the path status to the appropriate designation.
    ''' </summary>
    ''' <param name="numTempDirsMax">If creating a new temporary destination, more than one can exist if specified. 
    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    ''' <remarks></remarks>
    Private Sub SetDestinationNew(ByVal numTempDirsMax As Integer)
        Dim newPath As String = GetPathDirectoryStub(e2eTester.controller.pathXML) & "\" & "DestinationTemp"

        myCsiTester.pathGlobal = CreateTempDirectory(newPath)
        status = "Empty"

        SetDestinationRun()
    End Sub

    ''' <summary>
    ''' Populates the failedExamples list with the model IDs of the examples marked to fail.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFailedExamples()
        For Each example As cE2EExample In examples
            If example.failed Then failedExamples.Add(example.id)
        Next
    End Sub

    ''' <summary>
    ''' Sets whether or not the example in the destination is expected to have been run and also perhaps compared.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRanComparedExamples()
        Dim i As Integer
        Try
            For Each exampleID As String In _IDList
                i = 0
                For Each example As cE2EExample In examples
                    If example.id = exampleID Then
                        example.expectedRan = ConvertYesTrueBoolean(_RanList(i))
                        example.expectedCompared = ConvertYesTrueBoolean(_ComparedList(i))
                        Exit For
                    End If
                    i += 1
                Next
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the expected number of failed tabs and test sets at the destination before the an e2e testis performed, based on whether examples have been run and compared, and if any of those compared are expected to fail.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFailedSetsTabs()
        examplesFailNumOriginal = 0

        If destinationRun Then
            For Each example As cE2EExample In examples
                If (example.expectedCompared AndAlso example.failed) Then
                    examplesFailNumOriginal += 1
                End If
            Next
        End If

        If examplesFailNumOriginal > 0 Then
            testSetFailedNumOriginal = 1
            tabsFailedNumOriginal = 1
        Else
            testSetFailedNumOriginal = 0
            tabsFailedNumOriginal = 0
        End If
    End Sub

    ''' <summary>
    ''' Returns the example contained within the destination class that matches the supplied model ID.
    ''' </summary>
    ''' <param name="modelID">ID associated with the model that is used to located the example object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetExampleByID(ByVal modelID As String) As cE2EExample
        Dim selectedExample As cE2EExample = Nothing

        For Each example As cE2EExample In examples
            If example.id = modelID Then
                selectedExample = example
                Exit For
            End If
        Next

        Return selectedExample
    End Function

    'Failure summaries
    ''' <summary>
    ''' Returns the number of examples in the destination that are expected to have failed in their runs.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExpectedRunsFailed() As Integer
        Dim runsFailed As Integer

        If Not StringsMatch(status, "Ran") Then Return 0

        For Each modelID As String In failedExamples
            For Each example As cE2EExample In examples
                With example
                    If .id = modelID AndAlso
                        (StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.timeOut)) OrElse
                        StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.outputFileMissing))) Then

                        runsFailed += 1
                    End If
                End With
            Next
        Next

        Return runsFailed
    End Function

    ''' <summary>
    ''' Returns the number of examples in the destination that are expected to have failed in their comparisons.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExpectedComparisonsFailed() As Integer
        Dim failedExample As Boolean
        Dim comparisonsFailed As Integer

        If Not StringsMatch(status, "Ran") Then Return 0

        For Each modelID As String In failedExamples
            For Each example As cE2EExample In examples
                With example
                    If .id = modelID Then
                        'Consider example passing by default if no expected status was recoreded
                        If String.IsNullOrEmpty(.expectedCompareStatus) Then Return comparisonsFailed

                        'Determine failure from recorded status
                        If (Not StringsMatch(.expectedCompareStatus, GetEnumDescription(eResultCompare.outputFileMissing))) Then                               'Output file was generated
                            If (Not StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.completedRun)) AndAlso
                                Not StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.manual))) Then   'Example failed to run properly
                                failedExample = True
                            ElseIf Not StringsMatch(.expectedCompareStatus, GetEnumDescription(eResultCompare.successCompared)) Then         'Example failed to check properly
                                failedExample = True
                            ElseIf Not IsNumeric(GetPrefix(.expectedPercentChange, "%")) Then                                                'Example had some other failure
                                failedExample = True
                            ElseIf Not CDbl(GetPrefix(.expectedPercentChange, "%")) = 0 Then                                                 '% difference exists
                                failedExample = True
                            End If
                        Else                                                                                                                'Example output files missing
                            failedExample = True
                        End If

                        If failedExample Then comparisonsFailed += 1
                    End If
                End With
            Next
        Next

        Return comparisonsFailed
    End Function

    ''' <summary>
    ''' Sets the max % difference of the expected overall result of the examples that have run in the destination. Returns this number. Returns -1000000 if there is an error.
    ''' </summary>
    ''' <param name="p_setProperty">True: Sets the property of the class to the value returned. False: Returns the value with no other action taken.</param>
    ''' <remarks></remarks>
    Private Function GetMaxPercDiff(ByVal p_setProperty As Boolean) As String
        Dim currentMaxPercDiff As Double = 0
        Dim maxPercDiffString As String = ""

        If Not StringsMatch(status, "Ran") Then Return "0%"

        Try
            For Each modelID As String In failedExamples
                For Each myExample As cE2EExample In examples
                    Try
                        With myExample
                            If .id = modelID Then
                                If StringExistInName(.expectedPercentChange, GetEnumDescription(eResultOverall.percDiffNotAvailable)) Then                   'No numerical results
                                    maxPercDiffString = GetEnumDescription(eResultOverall.percDiffNotAvailable)
                                ElseIf Math.Abs(CDbl(GetPrefix(.expectedPercentChange, "%"))) > Math.Abs(currentMaxPercDiff) Then
                                    currentMaxPercDiff = CDbl(GetPrefix(.expectedPercentChange, "%"))
                                End If
                            End If
                        End With
                    Catch ex As Exception
                        currentMaxPercDiff = -1000000
                    End Try
                Next
            Next

            maxPercDiffString = CStr(currentMaxPercDiff) & "%"

            If p_setProperty Then maxPercDiff = maxPercDiffString

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try



        Return maxPercDiffString
    End Function

    ''' <summary>
    ''' Sets the property of the expected overall result &amp; max % difference of examples that have run in the destination.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateOverallResults()
        Dim tempTestSet As New cExampleTestSet

        If Not StringsMatch(status, "Ran") Then
            overallResult = ""
        Else
            overallResult = tempTestSet.GetOverallResult(GetExpectedRunsFailed, GetExpectedComparisonsFailed, GetMaxPercDiff(True))
        End If
    End Sub
#End Region

#Region "XML Read/Write Master Functions"
    ''' <summary>
    ''' Reads from or writes to XML, with unique properties.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteSuiteXmlNodes(ByVal read As Boolean)
        Dim pathNode As String

        pathNode = "//n:summary/n:id"
        If read Then id = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(id, pathNode, "")

        pathNode = "//n:summary/n:suite_id"
        If read Then idSuite = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(idSuite, pathNode, "")

        pathNode = "//n:summary/n:title"
        If read Then title = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(title, pathNode, "")

        pathNode = "//n:summary/n:description"
        If read Then description = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(description, pathNode, "")

        pathNode = "//n:summary/n:path"
        If read Then pathDestination = GetPathDirectoryStub(pathXML) & "\" & _xmlReaderWriter.ReadNodeText(pathNode, "")
        'If Not read Then writeNodeText(pathDestination, pathNode, "")

        pathNode = "//n:summary/n:status"
        If read Then status = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(status, pathNode, "")
    End Sub

    ''' <summary>
    ''' Reads from or writes to suite XML file, with object properties. (e.g. examples)
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteSuiteXmlObjects(ByVal read As Boolean)
        Try
            If read Then
                ReadExamples()
            Else
                'Dim nodeLast As String = "//n:model/n:classification"

                'WriteUpdates(nodeLast)
                'WriteResults()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Compiles lists of the ids, ran, and compared statuses of the examples located at the destination.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadExamples()
        Dim tagName As String = "example"
        Dim nodeList As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim parentNodes As New List(Of String)

        parentNodes.Add("destination")
        parentNodes.Add("examples")

        Try
            'Get list of results nodes to read
            nodeList = _xmlReaderWriter.GetResultsNodeList(tagName, nsmgr, parentNodes)

            If nodeList IsNot Nothing Then
                'Read the specified node within the node list to get the properties of each result
                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        _IDList.Add(_xmlReaderWriter.SelectSingleNode(node, "n:id", True, nsmgr, ""))
                        _RanList.Add(_xmlReaderWriter.SelectSingleNode(node, "n:id", True, nsmgr, "ran"))
                        _ComparedList.Add(_xmlReaderWriter.SelectSingleNode(node, "n:id", True, nsmgr, "compared"))
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region
End Class
