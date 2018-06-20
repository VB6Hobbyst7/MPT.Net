Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.Xml

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter

''' <summary>
''' Class containing properties and methods for the test suite used in validating the end-to-end tests involving checks.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETestSuite
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Fields"
    ''' <summary>
    ''' List of paths to the model control XML files associated with the suite.
    ''' </summary>
    ''' <remarks></remarks>
    Private _mcXMLPathList As List(Of String)
    ''' <summary>
    ''' List of the model ids associated with each entry in the _mcXMLPathList list,
    ''' </summary>
    ''' <remarks></remarks>
    Private _mcXMLIDList As List(Of String)

    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties"
    ''' <summary>
    ''' Path to the XML file used to record the suite data.
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
    ''' The list of unique level 2 classifications contained within the suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property classificationLevel2List As ObservableCollection(Of String)

    ''' <summary>
    ''' Maximum % difference expected among the examples in the suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property maxPercDiff As String
    ''' <summary>
    ''' Overall result among the examples in the suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property overallResult As String
#End Region

#Region "Properties XML"
    ''' <summary>
    ''' Path to the directory that contains the source examples for the suite. Made private so that the property is retrieved with modifications from a function.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property pathSuite As String

    ''' <summary>
    ''' ID for the test suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String
    ''' <summary>
    ''' Title for the test suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
    ''' <summary>
    ''' Description for the test suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String
    ''' <summary>
    ''' List of examples contained within the suite.
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
        SetFailedExamples()
        UpdateOverallResults()

        FinalizeExamplesFromMCXML()
        SetClassificationLevel2List()
    End Sub

    Private Sub InitializeData()
        _mcXMLPathList = New List(Of String)
        _mcXMLIDList = New List(Of String)

        examples = New ObservableCollection(Of cE2EExample)
        failedExamples = New ObservableCollection(Of String)
        classificationLevel2List = New ObservableCollection(Of String)
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
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns the path of the suite associated with the suite object. Also handles unspecified &amp; new suite cases.
    ''' </summary>
    ''' <param name="numTempDirsMax">If creating a new temporary destination, more than one can exist if specified. 
    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSuitePath(Optional ByVal numTempDirsMax As Integer = 1) As String
        Dim path As String = ""

        If String.IsNullOrEmpty(pathSuite) Then                'Unspecified. Use existing.
            path = myRegTest.models_database_directory.path
        ElseIf pathSuite = "*" Then           'Create new destination folder.
            SetSourceNew(numTempDirsMax)
        Else                                        'Use specified destination
            path = pathSuite
        End If

        Return path
    End Function

    ''' <summary>
    ''' Creates a new empty source directory and sets the source path to it.
    ''' </summary>
    ''' <param name="numTempDirsMax">If creating a new temporary source, more than one can exist if specified. 
    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    ''' <remarks></remarks>
    Private Sub SetSourceNew(ByVal numTempDirsMax As Integer)
        Dim newPath As String = GetPathDirectoryStub(e2eTester.controller.pathXML) & "\" & "SourceTemp"

        myCsiTester.pathGlobal = CreateTempDirectory(newPath)
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
    ''' Determines the unique list of level 2 classifications contained within the suite.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetClassificationLevel2List()
        Dim tempUniqueList As New List(Of String)
        Dim tempList As New List(Of String)

        'Create list of classification level 2 designations, then create a list of unique entries
        For Each example As cE2EExample In examples
            tempList.Add(example.classificationLevel2)
        Next
        tempUniqueList = CreateUniqueListString(tempList, tempUniqueList).ToList

        classificationLevel2List = New ObservableCollection(Of String)(tempUniqueList)
    End Sub

    ''' <summary>
    ''' Populates final properties of the test examples from their corresponding model control XML files.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FinalizeExamplesFromMCXML()
        Dim i As Integer

        'Check folder suite and compile a list of paths to valid XML files
        If IO.Directory.Exists(pathSuite) Then
            _mcXMLPathList = cMCGenerator.ListModelControlFilesInFolders(pathSuite)
        Else
            Exit Sub
        End If

        'Get corresponding list of example IDs
        For Each path As String In _mcXMLPathList
            Dim tempID As String = ""
            _xmlReaderWriter.GetSingleXMLNodeValue(path, "//n:model/n:id", tempID)
            _mcXMLIDList.Add(tempID)
        Next

        For Each example As cE2EExample In examples
            i = 0
            For Each exampleID As String In _mcXMLIDList
                If exampleID = example.id Then
                    example.FinalizeExampleFromMCXML(_mcXMLPathList(i))
                    Exit For
                End If
                i += 1
            Next
        Next
    End Sub

    'Failure summaries
    ''' <summary>
    ''' Returns the number of examples in the suite that are expected to fail in their runs.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExpectedRunsFailed() As Integer
        Dim runsFailed As Integer

        For Each example As cE2EExample In examples
            With example
                If (StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.timeOut)) OrElse
                    StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.outputFileMissing))) Then
                    runsFailed += 1
                End If
            End With
        Next

        Return runsFailed
    End Function

    ''' <summary>
    ''' Returns the number of examples in the suite that are expected to fail in their comparisons.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExpectedComparisonsFailed() As Integer
        Dim failedExample As Boolean
        Dim comparisonsFailed As Integer

        For Each example As cE2EExample In examples
            With example
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
            End With
        Next

        Return comparisonsFailed
    End Function

    ''' <summary>
    ''' Sets the max % difference of the expected overall result of the examples in the suite. Returns this number. Returns -1000000 if there is an error.
    ''' </summary>
    ''' <param name="setProperty">If ture, sets the property of the class to the value returned. Else, the function returns the value with no other action taken.</param>
    ''' <remarks></remarks>
    Private Function GetMaxPercDiff(ByVal setProperty As Boolean) As String
        Dim currentMaxPercDiff As Double = 0
        Dim maxPercDiffString As String = ""

        Try
            For Each myExample As cE2EExample In examples
                Try
                    With myExample
                        If StringExistInName(.expectedPercentChange, GetEnumDescription(eResultOverall.percDiffNotAvailable)) Then                   'No numerical results
                            maxPercDiffString = GetEnumDescription(eResultOverall.percDiffNotAvailable)
                        ElseIf String.IsNullOrEmpty(.expectedPercentChange) Then
                            currentMaxPercDiff = 0
                        ElseIf Math.Abs(CDbl(GetPrefix(.expectedPercentChange, "%"))) > Math.Abs(currentMaxPercDiff) Then
                            currentMaxPercDiff = CDbl(GetPrefix(.expectedPercentChange, "%"))
                        End If
                    End With
                Catch ex As Exception
                    currentMaxPercDiff = -1000000
                End Try
            Next

            If String.IsNullOrEmpty(maxPercDiffString) Then maxPercDiffString = CStr(currentMaxPercDiff) & "%"

            If setProperty Then maxPercDiff = maxPercDiffString

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try



        Return maxPercDiffString
    End Function

    ''' <summary>
    ''' Sets the property of the expected overall result &amp; max % difference of the suite.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateOverallResults()
        Dim tempTestSet As New cExampleTestSet

        overallResult = tempTestSet.GetOverallResult(GetExpectedRunsFailed, GetExpectedComparisonsFailed, GetMaxPercDiff(True))
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

        pathNode = "//n:summary/n:title"
        If read Then title = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(title, pathNode, "")

        pathNode = "//n:summary/n:description"
        If read Then description = _xmlReaderWriter.ReadNodeText(pathNode, "")
        If Not read Then _xmlReaderWriter.WriteNodeText(description, pathNode, "")

        pathNode = "//n:summary/n:path"
        If read Then pathSuite = GetPathDirectoryStub(pathXML) & "\" & _xmlReaderWriter.ReadNodeText(pathNode, "")
        'If Not read Then writeNodeText(pathSuite, pathNode, "")
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
    ''' Creates new cE2EExample objects for the class, read from the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadExamples()
        Dim tagName As String = "example"
        Dim nodeList As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim parentNodes As New List(Of String)
        parentNodes.Add("suite")
        parentNodes.Add("examples")

        Try
            'Get list of results nodes to read
            nodeList = _xmlReaderWriter.GetResultsNodeList(tagName, nsmgr, parentNodes)

            If nodeList IsNot Nothing Then
                'Read the specified node within the node list to get the properties of each result
                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        examples.Add(CreateExample(tagName, node, nsmgr))
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a cE2EExample object to be added to the list of examples in the cE2ETestSuite class, one for each 'example' element included under 'examples' within the suite XML file.
    ''' </summary>
    ''' <param name="tagName">Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="node">xmlNode object that corresponds to the result entry.</param>
    ''' <param name="nsmgr">namespace manager object that corresponds to the result entry.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateExample(ByVal tagName As String, ByVal node As XmlNode, ByVal nsmgr As XmlNamespaceManager) As cE2EExample
        Dim myExample As New cE2EExample
        Dim myResults As New List(Of cE2EExampleResult)

        Try
            'Create example object
            With myExample
                'Create Header Elements
                .id = _xmlReaderWriter.SelectSingleNode(node, "n:id", True, nsmgr, "")
                .idSecondary = _xmlReaderWriter.SelectSingleNode(node, "n:id_secondary", True, nsmgr, "")
                .description = _xmlReaderWriter.SelectSingleNode(node, "n:description", True, nsmgr, "")
                .expectedRunStatus = _xmlReaderWriter.SelectSingleNode(node, "n:run", True, nsmgr, "")
                .expectedCompareStatus = _xmlReaderWriter.SelectSingleNode(node, "n:compare", True, nsmgr, "")
                .expectedPercentChange = _xmlReaderWriter.SelectSingleNode(node, "n:percent_change_overall", True, nsmgr, "")
                .SetExpectedOverallResult()

                'Create List of Types
                Dim myTypes As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myTypes, "n:types", True)
                .types = New ObservableCollection(Of String)(myTypes)

                'Create list of results
                myResults = CreateResults(tagName, .id)

                'Add example result object
                For Each result As cE2EExampleResult In myResults
                    .results.Add(result)
                Next
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Return myExample
    End Function

    ''' <summary>
    ''' Creates a collection of cE2EExampleResult objects, populated with values from the suite XML file.
    ''' </summary>
    ''' <param name="tagName">Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="myExampleID">Example id used to distinguish tags from one example from another.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateResults(ByVal tagName As String, ByVal myExampleID As String) As List(Of cE2EExampleResult)
        Dim myResults As New List(Of cE2EExampleResult)
        Dim myResultsID As New List(Of String)
        Dim myResultsRaw As New List(Of String)
        Dim myResultsRounded As New List(Of String)
        Dim myResultsPerDiff As New List(Of String)

        Try
            'Get lists from lookup field sub-objects
            _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", myExampleID, "", myResultsID, "n:results_expected", True, "n:result_id")
            _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", myExampleID, "", myResultsRaw, "n:results_expected", True, "n:result_raw")
            _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", myExampleID, "", myResultsRounded, "n:results_expected", True, "n:result_rounded")
            _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", myExampleID, "", myResultsPerDiff, "n:results_expected", True, "n:result_percent_change")

            'Add lookup field sub-objects
            For j = 0 To myResultsID.Count - 1
                Dim myResult As New cE2EExampleResult
                myResult.id = myResultsID(j)
                myResult.raw = myResultsRaw(j)
                myResult.rounded = myResultsRounded(j)
                myResult.percentChange = myResultsPerDiff(j)
                myResults.Add(myResult)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return myResults
    End Function

#End Region

End Class
