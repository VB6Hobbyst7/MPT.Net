Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Xml

Imports MPT.String.ConversionLibrary
Imports MPT.XML.ReaderWriter

Imports CSiTester.cExample
Imports CSiTester.cMCModel

''' <summary>
''' Each class represents one row of output for a given example. 
''' This class is mostly a collection of properties, such as a theoretical value, benchmark value, latest result, time to run, SQL query, etc. for a given value that is part of a collection of values constituting an example.
''' This class has its own methods for updating post-run results.
''' </summary>
''' <remarks></remarks>
Public Class cExampleItem
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Friend Const CLASS_STRING As String = "cExampleItem"

#Region "Fields"
    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties: Class"
    'Temp: Remove XML-open/close functions once integrated with cMCModel result class
    Public Property xmlPath As String

    '=== Misc
    Private _valueChanged As Boolean
    ''' <summary>
    ''' Used to determine if a result value has been changed when editing the example results directly in the detailed view.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueChanged As Boolean
        Set(ByVal value As Boolean)
            If Not _valueChanged = value Then
                _valueChanged = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("valueChanged"))
            End If
        End Set
        Get
            Return _valueChanged
        End Get
    End Property

    Private _editMode As eExampleEditMode
    ''' <summary>
    ''' Used to determine if a result value has been set to be editable when editing the example results directly in the detailed view.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property editMode As eExampleEditMode
        Set(ByVal value As eExampleEditMode)
            If Not _editMode = value Then
                _editMode = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("editMode"))
            End If
        End Set
        Get
            Return _editMode
        End Get
    End Property


    ''' <summary>
    ''' Result type, such as regular or post-processed, which is used for instructions on how to gather results based on how regTest handles these various types.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultType As eResultType
    ''' <summary>
    ''' Sub example name designation, such as 'a' for Example 10a.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subExample As String

    '=== Calculated Properties
    Private _percentDifferenceIndependent As String
    ''' <summary>
    ''' % difference between the analysis value and the recorded independent/theoretical value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property percentDifferenceIndependent As String
        Set(ByVal value As String)
            If Not _percentDifferenceIndependent = value Then
                _percentDifferenceIndependent = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("percentDifferenceIndependent"))
            End If
        End Set
        Get
            Return _percentDifferenceIndependent
        End Get
    End Property

    Private _percentDifferenceBenchmark As String
    ''' <summary>
    ''' % difference between the analysis value and the established benchmark value from a prior analysis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property percentDifferenceBenchmark As String
        Set(ByVal value As String)
            If Not _percentDifferenceBenchmark = value Then
                _percentDifferenceBenchmark = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("percentDifferenceBenchmark"))
            End If
        End Set
        Get
            Return _percentDifferenceBenchmark
        End Get
    End Property

    Private _cellClassBenchResult As String
    ''' <summary>
    ''' Color designation corresponding to the 'percentDifferenceBenchmark' value. Used for styles.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cellClassBenchResult As String
        Set(ByVal value As String)
            If Not _cellClassBenchResult = value Then
                _cellClassBenchResult = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("cellClassBenchResult"))
            End If
        End Set
        Get
            Return _cellClassBenchResult
        End Get
    End Property

    '=== cMC-based properties
    Private _result As cMCResultBasic
    ''' <summary>
    ''' Standard result item that is queried from the tables and compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property result As cMCResultBasic
        Set(ByVal value As cMCResultBasic)
            _result = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("result"))
        End Set
        Get
            Return _result
        End Get
    End Property

    Private _resultPostProcessed As cMCResult
    ''' <summary>
    ''' A compared result item that requires some calculation or other post-processing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultPostProcessed As cMCResult
        Set(ByVal value As cMCResult)
            _resultPostProcessed = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("resultPostProcessed"))
        End Set
        Get
            Return _resultPostProcessed
        End Get
    End Property

    Private _resultExcel As cMCFile
    ''' <summary>
    ''' Result that is compared via post-processing in Excel.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultExcel As cMCFile
        Set(ByVal value As cMCFile)
            _resultExcel = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("resultExcel"))
        End Set
        Get
            Return _resultExcel
        End Get
    End Property
#End Region

#Region "Properties: XML File"
    '=== Title Elements
    Private _outputParameter As String
    ''' <summary>
    ''' Name of the output parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputParameter As String
        Set(ByVal value As String)
            If Not _outputParameter = value Then
                _outputParameter = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("outputParameter"))
            End If
        End Set
        Get
            Return _outputParameter
        End Get
    End Property
    Private _tableQuery As String
    ''' <summary>
    ''' Query used for looking up the analysis values for the output parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableQuery As String
        Set(ByVal value As String)
            If Not _tableQuery = value Then
                _tableQuery = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tableQuery"))
            End If
        End Set
        Get
            Return _tableQuery
        End Get
    End Property
    '=== Query Parameters
    Private _outputField As String
    ''' <summary>
    ''' Name of the header for the value desired to be used for analysis results comparison.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputField As String
        Set(ByVal value As String)
            If Not _outputField = value Then
                _outputField = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("outputField"))
            End If
        End Set
        Get
            Return _outputField
        End Get
    End Property

    Private _tableName As String
    ''' <summary>
    ''' Name of the table queried.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableName As String
        Set(ByVal value As String)
            If Not _tableName = value Then
                _tableName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tableName"))
            End If
        End Set
        Get
            Return _tableName
        End Get
    End Property

    Private _tableFieldNames As ObservableCollection(Of String)
    ''' <summary>
    ''' Collection of the names of various headers/columns in the table to be referenced for looking up the 'outputField' value. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableFieldNames As ObservableCollection(Of String) 'For multiple query critera
        Set(ByVal value As ObservableCollection(Of String))
            _tableFieldNames = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tableFieldNames"))
        End Set
        Get
            Return _tableFieldNames
        End Get
    End Property

    Private _tableFieldValues As ObservableCollection(Of String)
    ''' <summary>
    ''' A collection of values to loop up, corresponding to the 'tableFieldNames' columns. A unique set will yield a particular entry/row, whereby the 'outputField' value can be found.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableFieldValues As ObservableCollection(Of String) 'For multiple query critera
        Set(ByVal value As ObservableCollection(Of String))
            _tableFieldValues = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tableFieldValues"))
        End Set
        Get
            Return _tableFieldValues
        End Get
    End Property

    '=== Initial Results
    Private _independentValue As String
    ''' <summary>
    ''' Value established by hand calculations or independent verified sources outside of the analysis, such as examples in manuals and books.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property independentValue As String
        Set(ByVal value As String)
            If Not _independentValue = value Then
                _independentValue = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("independentValue"))
            End If
        End Set
        Get
            Return _independentValue
        End Get
    End Property

    Private _benchmarkValue As String
    ''' <summary>
    ''' Value established from a prior analysis, to be used as a reference for changes in the program. Need not be correct, if used as a regression test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property benchmarkValue As String
        Set(ByVal value As String)
            If Not _benchmarkValue = value Then
                _benchmarkValue = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("benchmarkValue"))
            End If
        End Set
        Get
            Return _benchmarkValue
        End Get
    End Property

    '=== Run Results
    Private _checkResultRaw As String
    ''' <summary>
    ''' Analysis result, as queried from the tables.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkResultRaw As String
        Set(ByVal value As String)
            If Not _checkResultRaw = value Then
                _checkResultRaw = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("checkResultRaw"))
            End If
        End Set
        Get
            Return _checkResultRaw
        End Get
    End Property

    Private _checkResultRounded As String
    ''' <summary>
    ''' Analysis result, after being rounded to the same level of accuracy as the benchmark value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkResultRounded As String
        Set(ByVal value As String)
            If Not _checkResultRounded = value Then
                _checkResultRounded = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("checkResultRounded"))
            End If
        End Set
        Get
            Return _checkResultRounded
        End Get
    End Property
#End Region

#Region "Initialization"

    ''' <summary>
    ''' Generates ExampleItem class populated with dummy data
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
        SetDummyValues()
        SetDefaults()
    End Sub

    ''' <summary>
    ''' Generates Example class, populated by data stored in external XML files
    ''' </summary>
    ''' <param name="read">Specifies that the function should assign class properties from reading an XML</param>
    ''' <param name="myResultType">Specifies whether entry is dircectly from database (regular), calculated within RegTest (post-processed), or calculated in Excel</param>
    ''' <param name="nodeIndex">Index number of the result node in the XML file</param>
    ''' <param name="myXMLPath">Optional: If specified, the XMl file will be opened for the particular item. If not specified, it is assumed that the XML file is already open.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal read As Boolean, ByVal myResultType As eResultType, ByVal nodeIndex As Integer, Optional ByVal myXMLPath As String = "")
        xmlPath = myXMLPath
        resultType = myResultType
        ReadWriteExampleXML(read, myResultType, nodeIndex)  'TODO: Merge this with the cMCResult and related classes for reading/writing. The classes mostly have writing capability, while this class mostly has reading capability
        SetDefaults()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Populates summary with data from example XML file
    ''' </summary>
    ''' <param name="read">Specifies that the function should assign class properties from reading an XML</param>
    ''' <param name="myResultType">Specifies whether entry is dircectly from database (regular), calculated within RegTest (post-processed), or calculated in Excel</param>
    ''' <param name="nodeIndex">Index number of the result node in the XML file</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleXML(ByVal read As Boolean, ByVal myResultType As eResultType, Optional ByVal nodeIndex As Integer = 0)
        'Temp: Remove XML-open/close functions once integrated with cMCModel result class
        If Not String.IsNullOrEmpty(xmlPath) Then
            Dim xmlReaderWriter As New cXmlReadWrite()
            If xmlReaderWriter.InitializeXML(xmlPath) Then
                'TODO: finish this. 
                ReadWriteExampleItemXmlNode(read, myResultType, nodeIndex)
                'ReadWriteExampleItemXmlList(read)
                'ReadWriteExamplItemeXmlObject(read)

                xmlReaderWriter.CloseXML()
            End If
        Else
            ReadWriteExampleItemXmlNode(read, myResultType, nodeIndex)
        End If

        'Determine % Difference
        'Calculated Method, as there is no XML result available before analysis

        percentDifferenceIndependent = PercentDifference(benchmarkValue, independentValue)

    End Sub

    ''' <summary>
    ''' Populates class with dummy data if the class has not been referred to an existing XML
    ''' </summary>
    ''' <remarks>Only used if instantiating an empty class</remarks>
    Private Sub SetDummyValues()
        outputParameter = "Compactness"
        tableQuery = "SELECT Period FROM [Modal Participating Mass Ratios] WHERE Mode LIKE '1'"

        independentValue = "8"
        benchmarkValue = "14.3"

        percentDifferenceIndependent = PercentDifference(benchmarkValue, independentValue)
    End Sub

    ''' <summary>
    ''' Sets default starting values for example.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaults()
        checkResultRaw = RESULT_DEFAULT
        checkResultRounded = RESULT_DEFAULT
        percentDifferenceBenchmark = RESULT_NOT_COMPARED
    End Sub

    Private Sub ResetResults()
        checkResultRaw = RESULT_DEFAULT
        checkResultRounded = RESULT_DEFAULT
        percentDifferenceBenchmark = RESULT_NOT_COMPARED

        'TODO: Finish!
    End Sub

    ''' <summary>
    ''' Updates remaining fields after example has been run and compared.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateResults(ByVal myResultType As eResultType, Optional ByVal nodeIndex As Integer = 0)
        ReadResultItemXmlNode(myResultType, nodeIndex)
        ' ReadResultsXML(myResultType, nodeIndex)

        'Determine % Difference
        'Direct from XML
        'percentDifferenceIndependent   'Currently only calculated between theoretical and current results and not between theoretical and benchmark

        If String.IsNullOrEmpty(percentDifferenceBenchmark) Then
            percentDifferenceBenchmark = RESULT_NOT_AVAILABLE
        Else
            percentDifferenceBenchmark = percentDifferenceBenchmark & "%"
        End If

        'Calculated Method
        'If String.IsNullOrEmpty(checkResultRounded) Then
        '    percentDifferenceBenchmark = "N/A"
        'Else
        '    percentDifferenceBenchmark = PercentDifference(checkResultRounded, benchmarkValue)
        'End If
    End Sub

    ''' <summary>
    ''' Updates the benchmark value of the example item in the model control XML to the rounded value of the example item result.
    ''' </summary>
    ''' <param name="p_resultType">Specifies whether entry is dircectly from database (regular), calculated within RegTest (post-processed), or calculated in Excel.</param>
    ''' <param name="p_nodeIndex">Index number of the result node in the XML file.</param>
    ''' <param name="p_matchResult">If true, benchmarks will be saved to match the rounded results. If false, the benchmarks will be saved with whatever values are stored in the class.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateBenchmarks(ByVal p_resultType As eResultType,
                                ByVal p_nodeIndex As Integer,
                                ByVal p_matchResult As Boolean)
        Dim xmlCSi As New cXMLCSi()
        If Not String.IsNullOrEmpty(xmlPath) Then
            If _xmlReaderWriter.InitializeXML(xmlPath) Then
                ReadWriteExampleItemXmlNode(False, p_resultType, p_nodeIndex, p_matchResult)
                If benchmarkValue = "0" Then
                    'Automatically add the zero_tolerance = 1 element if it does not already exist
                    xmlCSi.HandleZeroTolerance(p_nodeIndex, resultType)
                End If
                _xmlReaderWriter.CloseXML()
            End If
        Else
            ReadWriteExampleItemXmlNode(False, p_resultType, p_nodeIndex, p_matchResult)
            If benchmarkValue = "0" Then
                'Automatically add the zero_tolerance = 1 element if it does not already exist
                xmlCSi.HandleZeroTolerance(p_nodeIndex, resultType)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Updates the benchmark value of the example item in the model control XML to the rounded value of the example item result.
    ''' </summary>
    ''' <param name="myResultType">Specifies whether entry is dircectly from database (regular), calculated within RegTest (post-processed), or calculated in Excel</param>
    ''' <param name="nodeIndex">Index number of the result node in the XML file</param>
    ''' <remarks></remarks>
    Friend Sub UpdateOthers(ByVal myResultType As eResultType, ByVal nodeIndex As Integer)
        percentDifferenceIndependent = PercentDifference(benchmarkValue, independentValue)

        If Not String.IsNullOrEmpty(xmlPath) Then
            If _xmlReaderWriter.InitializeXML(xmlPath) Then
                ReadWriteExampleItemXmlNode(False, myResultType, nodeIndex, False)
                _xmlReaderWriter.CloseXML()
            End If
        Else
            ReadWriteExampleItemXmlNode(False, myResultType, nodeIndex, False)
        End If
    End Sub

    'Not Used VVVV
    ''' <summary>
    ''' Reads XML of test results, if they exist
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadResultsXML(ByVal myResultType As eResultType, Optional ByVal nodeIndex As Integer = 0)
        'percentDifferenceBenchmark = PercentDifference(checkResultRounded, benchmarkValue)

        'ReadResultItemXmlNode(myResultType, nodeIndex)
    End Sub
    '^^^^^

    '==== Supporting Methods
    ''' <summary>
    ''' Calculates percent difference, as a number converted to a string. Handles numbers, string comparisons, zero values, missing values
    ''' </summary>
    ''' <param name="NewResult">New result, as numerator</param>
    ''' <param name="OldResult">Baseline result, as denominator</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function PercentDifference(ByVal NewResult As String, ByVal OldResult As String) As String
        Dim PercentDifferenceNum As Double
        Dim NewResultNum As Double
        Dim OldResultNum As Double

        'Check if New Result is Blank and assign accordingly
        If String.IsNullOrEmpty(NewResult) Then Return RESULT_NONE
        If OldResult = RESULT_DEFAULT Then Return RESULT_DEFAULT

        If (IsNumeric(NewResult) AndAlso IsNumeric(OldResult)) Then   'Value is a numeric comparison
            'Convert string numbers to Long
            NewResultNum = CDbl(NewResult)
            OldResultNum = CDbl(OldResult)

            'Check if number is near 0
            If NewResultNum < 10 ^ -8 Then NewResultNum = 0
            If OldResultNum < 10 ^ -8 Then OldResultNum = 0

            If NewResultNum = 0 And OldResultNum = 0 Then   'Both numbers match
                PercentDifferenceNum = 0
            ElseIf OldResultNum = 0 Then    'Adjust for 0 in denominator
                PercentDifferenceNum = 1
            Else    'Standard % Difference calculation
                PercentDifferenceNum = (NewResultNum - OldResultNum) / OldResultNum
            End If
        Else    'Value is a string comparison
            If NewResult = OldResult Then
                PercentDifferenceNum = 0
            Else
                PercentDifferenceNum = 1
            End If
        End If

        'Set result to string and adjust for % difference specified in results display
        PercentDifference = CStr(PercentDifferenceNum * 100)
        PercentDifference = PercentDifferenceFormat(PercentDifference) & "%"

    End Function

    ''' <summary>
    ''' Shortens or lengthens the percent difference number to match that specified in RegTest
    ''' </summary>
    ''' <param name="percentDifference">The caculated percent difference in results</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function PercentDifferenceFormat(ByRef percentDifference As String) As String
        Dim percentDifferenceNum As Double
        Dim indexOfDecimalPoint As Integer
        Dim numberOfDecimals As Integer
        Dim numberOfDecimalsSpecified As Integer
        Dim differenceOfDecimals As Integer
        Dim i As Integer

        '===== Adjust number if it has more decimal places than necessary
        'Determine specified number of decimals, and round number to this
        numberOfDecimalsSpecified = myCInt(myRegTest.percent_difference_decimal_digits)
        percentDifferenceNum = System.Math.Round(CDbl(percentDifference), numberOfDecimalsSpecified)
        percentDifference = CStr(percentDifferenceNum)


        '===== Adjust member if it has fewer decimal places than necessary
        'Determine decimal location, or adjust if absent (i.e. an integer)
        indexOfDecimalPoint = percentDifference.IndexOf(".")
        If indexOfDecimalPoint < 0 Then                         'Correct if integer, which has no decimal
            indexOfDecimalPoint = Len(percentDifference)        'Adjust index. No need to subract for length vs. array as an integer is missing the decimal so is off by 1 in the opposite sense
            If percentDifference = "0" Then
                percentDifference = "0." & percentDifference    'Adjust for no whole number or decimal point
            Else
                percentDifference = percentDifference & "."     'Adjust for no decimal point
            End If
        End If

        'Determine number of decimals in percentDifference
        numberOfDecimals = percentDifference.Substring(indexOfDecimalPoint + 1).Length

        'Determine difference from that specified in RegTest
        differenceOfDecimals = numberOfDecimals - numberOfDecimalsSpecified

        'Adjust output for any difference in number of decimals less than specified
        If differenceOfDecimals < 0 Then
            For i = 1 To System.Math.Abs(differenceOfDecimals)
                percentDifference = percentDifference & "0"
            Next i
        End If

        PercentDifferenceFormat = percentDifference
    End Function

#End Region

#Region "Methods: XML Read/Write Master Functions"

    ''' <summary>
    ''' Reads from or writes to [example].XML, with unique properties.
    ''' </summary>
    ''' <param name="p_read">Specify whether to read values from XML or write values to XML.</param>
    ''' <param name="p_resultType">Specifies whether entry is directly from database (regular), calculated within RegTest (post-processed), or calculated in Excel.</param>
    ''' <param name="p_nodeIndex">Index number of the result node in the XML file.</param>
    ''' <param name="p_updateBenchmarks">Optional: If true while read = false, the benchmark values in the model control XML file will be updated to the current rounded results values.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleItemXmlNode(ByVal p_read As Boolean,
                                            ByVal p_resultType As eResultType,
                                            Optional ByVal p_nodeIndex As Integer = 0,
                                            Optional ByVal p_updateBenchmarks As Boolean = False)
        'TODO: Merge this with the cMCResult and related classes for reading/writing. The classes mostly have writing capability, while this class mostly has reading capability
        Dim pathNodeParent As String = ""
        Dim pathNode As String
        Dim pathNodeAttrib As String = ""
        Dim i As Integer = p_nodeIndex + 1    'Result node index

        'MySQL query parameters
        Dim j As Integer                       'tableFieldsNumber counter
        Dim tableFieldsNumber As Integer = 0
        Dim tableQueryStub As String = ""

        '======
        'TODO: Right now, only taking first variable. Code later for extracting multiple variables
        '======
        Select Case p_resultType
            Case eResultType.regular : pathNodeParent = "results/n:"
            Case eResultType.postProcessed : pathNodeParent = "postprocessed_results/n:"
            Case eResultType.excelCalculated : pathNodeParent = "regtest_internal_use/n:excel_results/n:"
        End Select

        'Title Elements
        pathNode = "//n:" & pathNodeParent & "result[" & i & "]/n:comment"
        If p_read Then
            outputParameter = _xmlReaderWriter.ReadNodeText(pathNode)
            If String.IsNullOrEmpty(outputParameter) Then
                pathNode = "//n:" & pathNodeParent & "result[" & i & "]/n:name"
                outputParameter = _xmlReaderWriter.ReadNodeText(pathNode)
            End If
        Else
            _xmlReaderWriter.WriteNodeText(outputParameter, pathNode)
        End If

        'Query Elements
        pathNode = "//n:" & pathNodeParent & "result[" & i & "]/n:table_name"
        If p_read Then tableName = _xmlReaderWriter.ReadNodeText(pathNode)
        If Not p_read Then _xmlReaderWriter.WriteNodeText(tableName, pathNode)

        'Query Node Path & Lookup Fields
        Select Case p_resultType
            Case eResultType.regular
                pathNode = "//n:" & pathNodeParent & "result[" & i & "]/n:output_field/n:name"
                tableFieldsNumber = _xmlReaderWriter.CountChildNodes("//n:result[" & i & "]/n:lookup_fields")
            Case eResultType.postProcessed
                'Query Node Path & Lookup Fields
                pathNode = "//n:" & pathNodeParent & "result[" & i & "]/n:formula/n:variables/n:variable[1]/n:output_field/n:name"
                tableFieldsNumber = _xmlReaderWriter.CountChildNodes("//n:postprocessed_results/n:result[" & i & "]/n:formula/n:variables/n:variable[1]/n:lookup_fields")
        End Select

        If Not p_resultType = eResultType.excelCalculated Then
            If p_read Then
                outputField = _xmlReaderWriter.ReadNodeText(pathNode)
                tableFieldNames = New ObservableCollection(Of String)
                tableFieldValues = New ObservableCollection(Of String)
            Else
                _xmlReaderWriter.WriteNodeText(outputField, pathNode)
            End If

            For j = 1 To tableFieldsNumber
                Select Case p_resultType
                    Case eResultType.regular
                        'Table Field Name
                        pathNode = "//n:results/n:result[" & i & "]/n:lookup_fields/n:field[" & j & "]/n:name"
                        If p_read Then tableFieldNames.Add(_xmlReaderWriter.ReadNodeText(pathNode))
                        If Not p_read Then _xmlReaderWriter.WriteNodeText(tableFieldNames(j - 1), pathNode)
                        'Table Field Value
                        pathNode = "//n:results/n:result[" & i & "]/n:lookup_fields/n:field[" & j & "]/n:value"
                        If p_read Then tableFieldValues.Add(_xmlReaderWriter.ReadNodeText(pathNode))
                        If Not p_read Then _xmlReaderWriter.WriteNodeText(tableFieldValues(j - 1), pathNode)
                    Case eResultType.postProcessed
                        'Table Field Name
                        pathNode = "//n:postprocessed_results/n:result[" & i & "]/n:formula/n:variables/n:variable[1]/n:lookup_fields/n:field[" & j & "]/n:name"
                        If p_read Then tableFieldNames.Add(_xmlReaderWriter.ReadNodeText(pathNode))
                        If Not p_read Then _xmlReaderWriter.WriteNodeText(tableFieldNames(j - 1), pathNode)
                        'Table Field Value
                        pathNode = "//n:postprocessed_results/n:result[" & i & "]/n:formula/n:variables/n:variable[1]/n:lookup_fields/n:field[" & j & "]/n:value"
                        If p_read Then tableFieldValues.Add(_xmlReaderWriter.ReadNodeText(pathNode))
                        If Not p_read Then _xmlReaderWriter.WriteNodeText(tableFieldValues(j - 1), pathNode)
                End Select

                'Create table query stub
                If tableFieldsNumber > 1 And j > 1 Then tableQueryStub = tableQueryStub & " AND" 'For multiple queries
                tableQueryStub = tableQueryStub & " WHERE " & tableFieldNames(j - 1) & " LIKE '" & tableFieldValues(j - 1) & "'"
            Next j

            'Assemble Table Query
            '======
            'TODO: Right now, only taking first variable/Query. Code later for reporting multiple queries & the calculations
            '======
            tableQuery = "SELECT '" & outputField & "' FROM [" & tableName & "]" & tableQueryStub
        Else
            tableQuery = "Excel Calculated Result"
        End If


        'Initial Results
        Select Case p_resultType
            Case eResultType.regular
                'Theoretical/Independent Value
                pathNode = "//n:results/n:result[" & i & "]/n:output_field/n:value/n:theoretical"
                If p_read Then
                    independentValue = _xmlReaderWriter.ReadNodeText(pathNode)
                Else
                    If Not independentValue = INDEPENDENT_VALUE_DEFAULT Then _xmlReaderWriter.WriteNodeText(independentValue, pathNode)
                End If
                'Benchmark Value
                pathNode = "//n:results/n:result[" & i & "]/n:output_field/n:value/n:benchmark"
                If p_read Then
                    benchmarkValue = _xmlReaderWriter.ReadNodeText(pathNode)
                Else
                    If p_updateBenchmarks Then
                        If (Not String.IsNullOrEmpty(checkResultRounded) AndAlso
                            Not checkResultRounded = RESULT_DEFAULT AndAlso
                            Not checkResultRounded = RESULT_NONE AndAlso
                            Not checkResultRounded = RESULT_NOT_COMPARED AndAlso
                            Not checkResultRounded = RESULT_NOT_AVAILABLE) Then

                            'Account for zero number results, where the rounded result might be very small, e.g. BM = 0, Result = 1E-15
                            If (IsNumeric(benchmarkValue) AndAlso
                                myCDbl(benchmarkValue) = 0 AndAlso
                                percentDifferenceBenchmark = "0%") Then

                                _xmlReaderWriter.WriteNodeText("0", pathNode)
                            Else
                                _xmlReaderWriter.WriteNodeText(checkResultRounded, pathNode)
                            End If
                        End If
                    Else
                        _xmlReaderWriter.WriteNodeText(benchmarkValue, pathNode)
                    End If
                End If
            Case eResultType.excelCalculated
                'Theoretical/Independent Value
                pathNode = "//n:regtest_internal_use/n:excel_results/n:result[" & i & "]/n:value/n:theoretical"
                If p_read Then
                    independentValue = _xmlReaderWriter.ReadNodeText(pathNode)
                Else
                    If Not independentValue = INDEPENDENT_VALUE_DEFAULT Then _xmlReaderWriter.WriteNodeText(independentValue, pathNode)
                End If
                'Benchmark Value
                pathNode = "//n:regtest_internal_use/n:excel_results/n:result[" & i & "]/n:value/n:benchmark"
                If p_read Then
                    benchmarkValue = _xmlReaderWriter.ReadNodeText(pathNode)
                Else
                    If p_updateBenchmarks Then
                        If (Not String.IsNullOrEmpty(checkResultRounded) AndAlso
                            Not checkResultRounded = RESULT_DEFAULT AndAlso
                            Not checkResultRounded = RESULT_NONE AndAlso
                            Not checkResultRounded = RESULT_NOT_COMPARED AndAlso
                            Not checkResultRounded = RESULT_NOT_AVAILABLE) Then

                            'Account for zero number results, where the rounded result might be very small, e.g. BM = 0, Result = 1E-15
                            If (IsNumeric(benchmarkValue) AndAlso
                                myCDbl(benchmarkValue) = 0 AndAlso
                                percentDifferenceBenchmark = "0%") Then

                                _xmlReaderWriter.WriteNodeText("0", pathNode)
                            Else
                                _xmlReaderWriter.WriteNodeText(checkResultRounded, pathNode)
                            End If
                        End If
                    Else
                        _xmlReaderWriter.WriteNodeText(benchmarkValue, pathNode)
                    End If
                End If
            Case eResultType.postProcessed
                'Theoretical/Independent Value
                pathNode = "//n:postprocessed_results/n:result[" & i & "]/n:formula/n:result/n:theoretical"
                If p_read Then
                    independentValue = _xmlReaderWriter.ReadNodeText(pathNode)
                Else
                    If Not independentValue = INDEPENDENT_VALUE_DEFAULT Then _xmlReaderWriter.WriteNodeText(independentValue, pathNode)
                End If
                pathNode = "//n:postprocessed_results/n:result[" & i & "]/n:formula/n:result/n:benchmark"
                'Benchmark Value
                If p_read Then
                    benchmarkValue = _xmlReaderWriter.ReadNodeText(pathNode)
                Else
                    If p_updateBenchmarks Then
                        If (Not String.IsNullOrEmpty(checkResultRounded) AndAlso
                            Not checkResultRounded = RESULT_DEFAULT AndAlso
                            Not checkResultRounded = RESULT_NONE AndAlso
                            Not checkResultRounded = RESULT_NOT_COMPARED AndAlso
                            Not checkResultRounded = RESULT_NOT_AVAILABLE) Then
                            'Account for zero number results, where the rounded result might be very small, e.g. BM = 0, Result = 1E-15
                            If (IsNumeric(benchmarkValue) AndAlso CDbl(benchmarkValue) = 0 AndAlso percentDifferenceBenchmark = "0%") Then
                                _xmlReaderWriter.WriteNodeText("0", pathNode)
                            Else
                                _xmlReaderWriter.WriteNodeText(checkResultRounded, pathNode)
                            End If
                        End If
                    Else
                        _xmlReaderWriter.WriteNodeText(benchmarkValue, pathNode)
                    End If
                End If
        End Select

        If String.IsNullOrEmpty(independentValue) Then independentValue = INDEPENDENT_VALUE_DEFAULT
    End Sub

    ''' <summary>
    ''' INCOMPLETE: Reads from or writes to [example].XML, with properties lists.
    ''' </summary>
    ''' <param name="read"></param>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleItemXmlList(ByVal read As Boolean)
        'Reads & writes open-ended lists in the XML
        'Dim pathNode As String
        'Dim nameListNode As String
        'Dim myList As New List(Of String)
        'Dim propValue As String()

        'keywordsList = New ObservableCollection(Of String)
        'pathNode = "//n:keywords"
        'If read Then
        '    readNodeListText(pathNode, keywordsList)
        'ElseIf Not read Then
        '    nameListNode = "keyword"
        '    propValue = keywordsList.ToArray
        '    writeNodeListText(propValue, pathNode, nameListNode)
        'End If
    End Sub

    ''' <summary>
    ''' INCOMPLETE: Reads from or writes to [example].XML, with properties objects, which may contain lists.
    ''' </summary>
    ''' <param name="read"></param>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleItemXmlObject(ByVal read As Boolean)
        'Reads & writes formatting properties of 
        'This sub assumes that the number of 'contour' formatting groups & criteria is fixed, so changes can only be made to existing criteria.
        'Could expand capability to allow user to insert additional 'contour' groups & criteria

        Dim xmlRoot As XmlElement = _xmlReaderWriter.xmlRoot

        Dim namedCell As String
        Dim namedCellStub As String
        Dim pathNode As String

        Dim m As Integer
        Dim i As Integer
        Dim j As Integer
        Dim n As Integer

        Dim cellText As String


        For n = 0 To 0
            pathNode = "//regtest/reporting/model_results_table_cells_color_coding"
            Select Case n
                Case 0
                    pathNode = pathNode & "/absolute_percent_difference_from_benchmark_or_last_best_value_if_available"
                    namedCellStub = "benchmark_contour_"
                Case Else
                    pathNode = ""
                    namedCellStub = ""
            End Select

            'Lookup node or attribute within XML file
            Dim myXMLNode As XmlNode = xmlRoot.SelectSingleNode(pathNode)

            If read = True Then 'Read XML to Excel
                'Place values in sheet
                For j = 0 To myXMLNode.ChildNodes.Count - 1
                    namedCell = namedCellStub & j + 1
                    myXMLNode = xmlRoot.SelectSingleNode(pathNode).ChildNodes(j)
                    i = 1
                    For m = 0 To myXMLNode.ChildNodes.Count - 1
                        'Update -                       Range(namedCell).Offset(i, 0) = xmlNode.ChildNodes(m).Value
                        i = i + 1
                    Next m
                Next j
            Else    'Write new values to XML
                For j = 0 To myXMLNode.ChildNodes.Count - 1
                    namedCell = namedCellStub & j + 1
                    myXMLNode = xmlRoot.SelectSingleNode(pathNode).ChildNodes(j)
                    i = 1
                    For m = 0 To myXMLNode.ChildNodes.Count - 1
                        'Update -                         cellText = Range(namedCell).Offset(i, 0) 'Gather values from sheet
                        cellText = ""
                        myXMLNode.ChildNodes(m).Value = cellText    'Set values in XML
                        i = i + 1
                    Next m
                Next j
            End If
        Next n

    End Sub

    ''' <summary>
    ''' Gathers example item results from the output XML file
    ''' </summary>
    ''' <param name="myResultType">Result type of either regular, post-processed, or Excel calculated</param>
    ''' <param name="nodeIndex">Which result value is gathered, by node index</param>
    ''' <remarks></remarks>
    Private Sub ReadResultItemXmlNode(ByVal myResultType As eResultType, Optional ByVal nodeIndex As Integer = 0)
        Dim pathNode As String
        Dim pathNodeAttrib As String = ""
        Dim i As Integer = nodeIndex + 1    'Result node index

        'Result
        pathNode = "//n:result[" & i & "]/n:calculated_value/n:value"
        checkResultRaw = _xmlReaderWriter.ReadNodeText(pathNode)
        If String.IsNullOrEmpty(checkResultRaw) Then checkResultRaw = "N/A"

        'Rounded Result
        pathNode = "//n:result[" & i & "]/n:benchmark_value/n:rounded_calculated_value"
        checkResultRounded = _xmlReaderWriter.ReadNodeText(pathNode)
        If String.IsNullOrEmpty(checkResultRounded) Then checkResultRounded = "N/A"

        '% Different of Benchmark from Theoretical
        pathNode = "//n:result[" & i & "]/n:benchmark_value/n:percent_difference_from_benchmark"
        percentDifferenceBenchmark = _xmlReaderWriter.ReadNodeText(pathNode)

        '% Different from Benchmark
        pathNode = "//n:result[" & i & "]/n:benchmark_value/n:percent_difference_from_benchmark"
        percentDifferenceBenchmark = _xmlReaderWriter.ReadNodeText(pathNode)
        cellClassBenchResult = _xmlReaderWriter.ReadNodeText(pathNode, "cell_class")

    End Sub

#End Region

#Region "Test Components"

    ''' <summary>
    ''' Validates that the result and related items are reset.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="chkResultReset">Check if the result has been reset.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResultReset(ByVal className As String, Optional ByVal chkResultReset As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If chkResultReset Then
                .expectation = "% Difference resulting from a run have been cleared"
                .resultActual = percentDifferenceBenchmark
                .resultActualCall = classIdentifier & "percentDifferenceBenchmark"
                .resultExpected = RESULT_NOT_COMPARED
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Result read from a run has been cleared"
                .resultActual = checkResultRaw
                .resultActualCall = classIdentifier & "checkResultRaw"
                .resultExpected = ""
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Result read and rounded from a run has been cleared"
                .resultActual = checkResultRounded
                .resultActualCall = classIdentifier & "checkResultRounded"
                .resultExpected = ""
                If Not .RunSubTest() Then Return subTestPass

                'TODO: Check that the cMCResult class & subclasses has been cleared of the appropriate properties
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function



    ''' <summary>
    ''' Validates that the result and related items are set to the appropriate values.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResultFilled(ByVal className As String, ByVal expctResult As cE2EExampleResult) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If Not String.IsNullOrEmpty(expctResult.percentChange) Then
                .expectation = "% Difference is as expected"
                .resultActual = percentDifferenceBenchmark
                .resultActualCall = classIdentifier & "percentDifferenceBenchmark"
                .resultExpected = expctResult.percentChange
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not String.IsNullOrEmpty(expctResult.raw) Then
                .expectation = "Result read from a run is as expected"
                .resultActual = checkResultRaw
                .resultActualCall = classIdentifier & "checkResultRaw"
                .resultExpected = expctResult.raw
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not String.IsNullOrEmpty(expctResult.rounded) Then
                .expectation = "Result read and rounded from a run is as expected"
                .resultActual = checkResultRounded
                .resultActualCall = classIdentifier & "checkResultRounded"
                .resultExpected = expctResult.rounded
                If Not .RunSubTest() Then Return subTestPass
            End If

            'TODO: Check that the cMCResult class & subclasses has been set to the appropriate properties
        End With

        subTestPass = True

        Return subTestPass
    End Function

#End Region
End Class
