Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Xml

Imports MPT.Enums.EnumLibrary
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter

Imports CSiTester.cPathSettings

''' <summary>
''' Class creating the controller for running end-to-end/functional tests on CSiTester.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETestController
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"
    Friend Const DIR_NAME_E2E_TESTING As String = "testing\Testing Set (1)"
    Friend Const FILE_NAME_E2E_TESTING As String = "e2eTests.xml"
#End Region

#Region "Fields"
    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties"
    ''' <summary>
    ''' ID identifying the controller class that contains a particular series of tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String

    ''' <summary>
    ''' List of classes containing data about the various test suites used in running &amp; validating the end-to-end tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property suites As List(Of cE2ETestSuite)
    ''' <summary>
    ''' List of paths to all of the testing suites used by the test controller. These correspond to the path in suiteIDs of the same index.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property suitePaths As List(Of String)
    ''' <summary>
    ''' List of the IDs of all of the testing suites used by the test controller. These correspond to the path in suitePaths of the same index.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property suiteIDs As List(Of String)

    ''' <summary>
    ''' List of classes containing data about the various test destinations used in running &amp; validating the end-to-end tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property destinations As List(Of cE2ETestDestination)
    ''' <summary>
    ''' List of paths to all of the testing destinations used by the test controller. These correspond to the path in destinationIDs of the same index.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property destinationPaths As List(Of String)
    ''' <summary>
    ''' List of the IDs of all of the testing destinations used by the test controller. These correspond to the path in destinationPaths of the same index.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property destinationIDs As List(Of String)

    ''' <summary>
    ''' List of test classes used in running &amp; validating the end-to-end tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testInstructions As List(Of cE2ETestInstructions)
    ''' <summary>
    ''' Path to the XML file read to populate some of the properties of the class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathXML As String

    Private _tests As ObservableCollection(Of cE2eTest)
    ''' <summary>
    ''' Collection of individual test objects that contains the selection status, name, description, and internal instructions for running each test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tests As ObservableCollection(Of cE2eTest)
        Set(ByVal value As ObservableCollection(Of cE2eTest))
            _tests = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tests"))
        End Set
        Get
            Return _tests
        End Get
    End Property

    Private _testsAggregates As ObservableCollection(Of cE2eTest)
    ''' <summary>
    ''' Collection of aggregated test objects that contains the selection status, name, description, and internal instructions for running each test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testsAggregates As ObservableCollection(Of cE2eTest)
        Set(ByVal value As ObservableCollection(Of cE2eTest))
            _testsAggregates = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("testsAggregates"))
        End Set
        Get
            Return _testsAggregates
        End Get
    End Property

    Private _testsSelected As ObservableCollection(Of cE2eTest)
    ''' <summary>
    ''' Combined collection of 'tests' and then 'testsAggregates' collections.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testsSelected As ObservableCollection(Of cE2eTest)
        Set(ByVal value As ObservableCollection(Of cE2eTest))
            _testsSelected = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("testsSelected"))
        End Set
        Get
            Return _testsSelected
        End Get
    End Property
#End Region


#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    ''' <summary>
    ''' Creates a new controller class that is automatically populated with data from the XML file specified.
    ''' </summary>
    ''' <param name="pathXMLRead">Path to the XML file by which the class will be constructed.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pathXMLRead As String)
        InitializeData()

        InitializeXMLData(pathXMLRead)

        InitializeSuites()
        InitializeDestinations()

        FinalizeData()

        InitializeTests()
    End Sub

    Private Sub InitializeData()
        suitePaths = New List(Of String)
        suiteIDs = New List(Of String)
        destinationPaths = New List(Of String)
        destinationIDs = New List(Of String)

        suites = New List(Of cE2ETestSuite)
        destinations = New List(Of cE2ETestDestination)
        testInstructions = New List(Of cE2ETestInstructions)
        tests = New ObservableCollection(Of cE2eTest)
        testsAggregates = New ObservableCollection(Of cE2eTest)
        testsSelected = New ObservableCollection(Of cE2eTest)
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
                ReadWriteTestsXML(True)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Initializes all of the suite objects that correspond with each suite XML path listed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeSuites()
        If suitePaths.Count > 0 Then
            For Each suitePath As String In suitePaths
                AddSuite(suitePath)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Initializes all of the destination objects that correspond with each destination XML path listed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDestinations()
        If destinationPaths.Count > 0 Then
            For Each destinationPath As String In destinationPaths
                Dim suiteID As String
                Dim destinationID As String

                AddDestination(destinationPath)

                destinationID = destinations(destinations.Count - 1).id
                suiteID = destinations(destinations.Count - 1).idSuite
                AddSuiteExamplesToDestination(suiteID, destinationID)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Computes final properties values based in the data that was loaded &amp; calculated from the XML files.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FinalizeData()
        SetTestValues()
    End Sub

    ''' <summary>
    ''' Creates the test objects that are used to run e2e tests in the program.
    ''' </summary>
    ''' <remarks></remarks>
    Sub InitializeTests()
        Try
            'Individual Test Functions
            Dim testID As String

            testID = "1"
            TestChangeProgramPath(testID)
            TestChangeSource(testID)
            TestChangeDestination(testID)
            TestDeselectExamplesRun(testID)
            TestDeselectExamplesCompare(testID)
            TestSelectExamplesRun(testID)
            TestSelectExamplesCompare(testID)
            TestClearDestination()
            TestRestoreDefaults()
            TestResetSession()
            TestRunCheck(testID)

            'Aggregated Test Functions
            testID = "1"
            TestFullBasicCSiTesterForm(testID)

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Populates the class with data from the XML.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteTestsXML(ByVal read As Boolean)
        Try
            'ReadWriteSettingsXmlNodes(read)
            ReadWriteTestsXmlList(read)
            ReadWriteTestsXmlObjects(read)

            If Not read Then
                _xmlReaderWriter.SaveXML(pathXML)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Test Initializations"
    Private Sub TestChangeProgramPath(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Change Program Path"
            .description = "Changes the path to the CSi program to be used in checking examples."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestChangeProgramPath), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestChangeSource(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Change Models Source Path"
            .description = "Changes the path to the models to be used by CSiTester."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestChangeSource), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestChangeDestination(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Change CSiTester Destination Path"
            .description = "Changes the tester destination directory path."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestChangeDestination), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestDeselectExamplesRun(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Deselect Examples Run"
            .description = "Deselects certain examples from being run."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestDeselectExamplesRun), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestDeselectExamplesCompare(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Deselect Examples Compare"
            .description = "Deselects certain examples from being compared."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestDeselectExamplesCompare), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestSelectExamplesRun(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Select Examples Run"
            .description = "Selects certain examples to be run."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestSelectExamplesRun), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestSelectExamplesCompare(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Select Examples Compare"
            .description = "Selects certain examples to be compared."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestSelectExamplesCompare), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestClearDestination()
        Dim test As New cE2eTest

        With test
            .name = "Clear Test Destination"
            .description = "Clears the destination directory."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestClearDestination))
        End With

        tests.Add(test)
    End Sub

    Private Sub TestRestoreDefaults()
        Dim test As New cE2eTest

        With test
            .name = "Restore CSiTester Default Settings"
            .description = "Resets the CSiTester settings to the defaults."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestRestoreDefaults))
        End With

        tests.Add(test)
    End Sub

    Private Sub TestResetSession()
        Dim test As New cE2eTest

        With test
            .name = "Reset CSiTester Session"
            .description = "Resets the current session of CSiTester, including clearing the destination directory."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestResetSession))
        End With

        tests.Add(test)
    End Sub

    Private Sub TestRunCheck(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "Check Examples"
            .description = "Checks examples in CSiTester by running the analysis for selected examples, and comparing selected examples. Test can include cancelling midway through."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestRunCheck), testID)
        End With

        tests.Add(test)
    End Sub

    Private Sub TestFullBasicCSiTesterForm(ByVal testID As String)
        Dim test As New cE2eTest

        With test
            .name = "CSiTester form - Full Basic Check"
            .description = "Runs through all of the basic tests of the CSiTester form."
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestChangeProgramPath), testID)
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestChangeSource), testID)
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestChangeDestination), testID)

            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestDeselectExamplesRun), testID)
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestDeselectExamplesCompare), testID)

            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestSelectExamplesRun), testID)
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestSelectExamplesCompare), testID)

            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestClearDestination))
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestRestoreDefaults))
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestResetSession))
            .AddComponentNew(CSiTester.CLASS_STRING, GetEnumDescription(CSiTester.eTestFunctions.TestRunCheck), testID)
        End With

        testsAggregates.Add(test)
    End Sub



    Friend Sub InstallationTestingInternal()

    End Sub

    Friend Sub InstallationTestingRelease()

    End Sub

    Friend Sub RunTesting()

    End Sub

    Friend Sub NavigationTesting()

    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Adds a suite object populated by the specified XML file to the list of suites in the class.
    ''' </summary>
    ''' <param name="xmlPath">XML path to the suite to reference to populate the cE2ETestSuite class.</param>
    ''' <remarks></remarks>
    Private Sub AddSuite(ByVal xmlPath As String)
        Dim mySuite As New cE2ETestSuite(xmlPath)

        suites.Add(mySuite)
    End Sub

    ''' <summary>
    ''' Adds a destination object populated by the specified XML file to the list of destinations in the class.
    ''' </summary>
    ''' <param name="xmlPath">XML path to the destination to reference to populate the cE2ETestDestination class.</param>
    ''' <remarks></remarks>
    Private Sub AddDestination(ByVal xmlPath As String)
        Dim myDestination As New cE2ETestDestination(xmlPath)

        destinations.Add(myDestination)
    End Sub

    ''' <summary>
    ''' Adds copies of the suite example objects to the specified destination and assigns values to properties specific to the destination.
    ''' </summary>
    ''' <param name="suiteID">ID of the suite that the destination is derived from.</param>
    ''' <param name="destinationID">ID of the destination to create the example objects for.</param>
    ''' <remarks></remarks>
    Private Sub AddSuiteExamplesToDestination(ByVal suiteID As String, ByVal destinationID As String)
        Dim clonedExamples As New ObservableCollection(Of cE2EExample)
        'Create a list of cloned examples from the suite
        For Each suite As cE2ETestSuite In suites
            If suite.id = suiteID Then
                For Each example As cE2EExample In suite.examples
                    clonedExamples.Add(CType(example.Clone, cE2EExample))
                Next
                Exit For
            End If
        Next

        'Attach the cloned list of examples to the appropriate destination
        For Each destination As cE2ETestDestination In destinations
            If destination.id = destinationID Then
                destination.examples = clonedExamples
                destination.InitializeExamples()
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Computes additional properties of the test set objects based on the test suites &amp; destinations associated with them.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTestValues()

        For Each test As cE2ETestInstructions In testInstructions
            With test
                Dim destinationExamples As New ObservableCollection(Of cE2EExample)

                'Begin assuming all examples are run
                .SetRanCompared()

                'Adjust for if the check is canceled
                .UpdateRanComparedFromCancel()

                'Get destination examples from related test id and pass into UpdateRanComparedFromExamples & UpdateFailedFromExamples
                For Each destination As cE2ETestDestination In destinations
                    If destination.id = .destinationIDChecked Then
                        destinationExamples = destination.examples
                    End If
                Next

                'Adjust for previously run examples that have not been included in the ran/compared lists, which are not affected by canceling the check
                .UpdateRanComparedFromExamples(destinationExamples)
                .UpdateFailedFromExamples(destinationExamples)
                .UpdateOverallResults(destinationExamples)
            End With
        Next
    End Sub
#End Region


#Region "XML Read/Write Master Functions"

    ''' <summary>
    ''' Reads from or writes to XML, with properties lists.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteTestsXmlList(ByVal read As Boolean)
        'Reads & writes open-ended lists in the XML
        Dim pathNode As String

        Try
            pathNode = "//n:testing_suite_paths"
            If read Then
                suitePaths = _xmlReaderWriter.ReadNodeListPath(pathNode, DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING)
            ElseIf Not read Then
                _xmlReaderWriter.WriteNodeListPath(pathNode, "path", suitePaths, DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING)
            End If
            If read Then
                _xmlReaderWriter.ReadNodeListText(pathNode, suiteIDs, "id")
            ElseIf Not read Then
                'WriteNodeListPath(pathNode, "path", suiteIDs)
            End If

            pathNode = "//n:testing_destination_paths"
            If read Then
                destinationPaths = _xmlReaderWriter.ReadNodeListPath(pathNode, DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING)
            ElseIf Not read Then
                _xmlReaderWriter.WriteNodeListPath(pathNode, "path", destinationPaths, DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING)
            End If
            If read Then
                _xmlReaderWriter.ReadNodeListText(pathNode, destinationIDs, "id")
            ElseIf Not read Then
                'WriteNodeListPath(pathNode, "path", destinationIDs)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Reads from or writes to suite XML file, with object properties. (e.g. examples)
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteTestsXmlObjects(ByVal read As Boolean)
        Try
            If read Then
                ReadTests()
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
    ''' Creates new cE2ETestInstructions objects for the class, read from the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadTests()
        Dim tagName As String = "test"
        Dim nodeList As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim parentNodes As New List(Of String)
        parentNodes.Add("controller")
        parentNodes.Add("tests")

        Try
            'Get list of results nodes to read
            nodeList = _xmlReaderWriter.GetResultsNodeList(tagName, nsmgr, parentNodes)

            If nodeList IsNot Nothing Then
                'Read the specified node within the node list to get the properties of each result
                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        testInstructions.Add(CreateTest(tagName, node, nsmgr))
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
    Private Function CreateTest(ByVal tagName As String, ByVal node As XmlNode, ByVal nsmgr As XmlNamespaceManager) As cE2ETestInstructions
        Dim myTest As New cE2ETestInstructions
        Dim myQueryStringPartial As String = ""

        Try
            'Create example object
            With myTest
                'Create Header Elements
                .id = _xmlReaderWriter.SelectSingleNode(node, "n:id", True, nsmgr, "")
                .title = _xmlReaderWriter.SelectSingleNode(node, "n:title", True, nsmgr, "")
                .description = _xmlReaderWriter.SelectSingleNode(node, "n:description", True, nsmgr, "")
                .cancelCheckTime = _xmlReaderWriter.SelectSingleNode(node, "n:milliseconds", True, nsmgr, "")
                .cancelCheckModelID = _xmlReaderWriter.SelectSingleNode(node, "n:model_id", True, nsmgr, "")

                'Create Lists
                Dim myRuns As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myRuns, "n:selected_set_to_run", True)
                .run = New ObservableCollection(Of String)(myRuns)

                Dim myComparisons As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myComparisons, "n:selected_set_to_compare", True)
                .compare = New ObservableCollection(Of String)(myComparisons)

                Dim myDeselectedRuns As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myDeselectedRuns, "n:deselected_set_to_not_run", True)
                .runDeselect = New ObservableCollection(Of String)(myDeselectedRuns)

                Dim myDeselectedComparisons As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myDeselectedComparisons, "n:deselected_set_to_not_compare", True)
                .compareDeselect = New ObservableCollection(Of String)(myDeselectedComparisons)

                Dim myPrograms As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myPrograms, "n:programs", True)
                .programNames = New ObservableCollection(Of String)(myPrograms)

                Dim mySuiteIDs As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", mySuiteIDs, "n:suites", True)
                .testSuiteIDs = New ObservableCollection(Of String)(mySuiteIDs)

                Dim myDestinationIDs As New List(Of String)
                _xmlReaderWriter.ReadXmlObjectTextSubTag(tagName, "n:id", .id, "", myDestinationIDs, "n:destinations", True)
                .testDestinationIDs = New ObservableCollection(Of String)(myDestinationIDs)

                .destinationIDChecked = _xmlReaderWriter.SelectSingleNode(node, "n:destination_id_checked", True, nsmgr, "")
                'Default to last destination ID if not specified
                If String.IsNullOrEmpty(.destinationIDChecked) Then .destinationIDChecked = .testDestinationIDs(.testDestinationIDs.Count - 1)
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Return myTest
    End Function

#End Region
End Class
