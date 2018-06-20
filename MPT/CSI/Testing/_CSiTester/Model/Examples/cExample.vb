Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Xml

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary
Imports MPT.Time.TimeLibrary
Imports MPT.XML
Imports MPT.XML.ReaderWriter

Imports CSiTester.cExampleTestSet
Imports CSiTester.cMCModel
Imports CSiTester.cFileAttachment
Imports CSiTester.cPathAttachment
Imports CSiTester.cKeywordTags


''' <summary>
''' Basic class that represents a given example. 
''' While mostly populated with properties from the model XML and model output XML, there are unique values also calculated here, as well as methods directly related to the tracking and organizing of the examples in the GUI.
''' Contains collections of cExampelItem classes.
''' Parent regions pertain to a unique use of the class to serve as a single parent class to multpiple child example classes. This class has a combination of duplicate prorperties, agglomerated properties, and some new properties.
''' </summary>
''' <remarks></remarks>
Public Class cExample
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Friend Const CLASS_STRING As String = "cExample"

#Region "Constants: Private"
    ' Update Results
    Private Const _PROMPT_UPDATE_BM_MISMATCH_PROGRAM_NEW As String = "The currently selected analysis program is: "
    Private Const _PROMPT_UPDATE_BM_MISMATCH_PROGRAM_OLD As String = "The current program used for the results benchmark is: "
    Private Const _PROMPT_UPDATE_BM_MISMATCH_PROGRAM_ACTION As String = "Do you want to override the program name and version associated with the benchmark values?" _
                                    & vbNewLine & vbNewLine & "Selecting 'No' will update the values without updating the benchmark attribution."
    Private Const _TITLE_UPDATE_BM_MISMATCH_PROGRAM As String = "Program Mismatch"

    ' Multi Model
    Private Const _PROMPT_GENERATE_PARENT_CLASS As String = "Generating a Parent Class!"

#End Region

#Region "Constants"
    ' Times Summary - If record of prior run times exists, 'Assumed' values will be overwritten
    ' = Time Run
    Friend Const TIME_RUN_ASSUMED_DEFAULT As String = "-"
    Private Const _TIME_RUN_ACTUAL_DEFAULT As String = "-"
    Private Const _TIME_RUN_ACTUAL_FAIL_RESULT As String = "N/A"

    ' = Time Compared
    Friend Const TIME_COMPARE_ASSUMED_DEFAULT As String = "-"
    Private Const _TIME_COMPARE_ACTUAL_DEFAULT As String = "-"
    Private Const _TIME_COMPARE_ACTUAL_FAIL_RESULT As String = "N/A"

    ' = Time Checked
    Friend Const TIME_CHECK_ASSUMED_DEFAULT As String = "-"
    Private Const _TIME_CHECK_ACTUAL_DEFAULT As String = "-"
    Private Const _TIME_CHECK_ACTUAL_FAIL_RESULT As String = "N/A"

    ' Results
    Friend Const RESULT_DEFAULT As String = "-"
    Friend Const INDEPENDENT_VALUE_DEFAULT As String = "-"
    Friend Const LAST_BEST_VALUE_DEFAULT As String = "-"

    Friend Const RESULT_NONE As String = "No Result"
    Friend Const RESULT_NOT_COMPARED As String = "Not Compared"
    Friend Const RESULT_NOT_AVAILABLE As String = "N/A"

    ' Update Results
    Friend Const PROGRAM_VERSION_DEFAULT As String = "-"
    Private Const _PROGRAM_BUILD_DEFAULT As String = "-"
    Private Const _RUN_DATETIME_DEFAULT As String = "-"

    ' Keywords (Possible redundancies in MC. TODO: See below for future integration of classes?)
    Private Const _CLASS_REGION_DEFAULT As String = "Unspecified"

    Friend Const TYPE_DEFAULT As String = "Unspecified"
    Friend Const TYPE_ANALYSIS As String = "Analysis"
    Friend Const TYPE_DESIGN As String = "Design"
    Friend Const TYPE_ANALYSIS_DESIGN As String = "Analysis & Design"
#End Region

#Region "Fields"
    ''' <summary>
    ''' Tabluar row comparison.
    ''' </summary>
    ''' <remarks></remarks>
    Private exampleItem As cExampleItem

    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties: Class"
    '=== Paths - IMPORTANT: All paths are absolute paths to the SOURCE version of the example. Conversion is necessary to reference the destination version of the example and related values.
    ''' <summary>
    ''' Path to the model control XML file.
    ''' Makes non-indexed references possible.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathXmlMC As String
    ''' <summary>
    ''' Path to the outputSettings.xml file in the Source directory if outputSettingsUsed = True. Always at the model level.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathXmlOutputSettingsSrc As String
    ''' <summary>
    ''' Path to the outputSettings.xml file in the Destination directory if outputSettingsUsed = True. Always at the model level.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathXmlOutputSettingsDest As String
    ''' <summary>
    ''' Path to the results XML file generated by regTest for this particular example. Not the composite results file of all examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathXmlModelResults As String
    ''' <summary>
    ''' Path to the model file used for this example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathModelFile As String
    ''' <summary>
    ''' If a model file is imported, this is the name of the model file run in the analysis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileNameModelImported As String

    '=== Initialization Properties
    ''' <summary>
    ''' For initial syncing with the list of examples in CSiTester class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleIndex As Integer
    ''' <summary>
    ''' Index of example within a given test set.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testSetIndex As Integer
    ''' <summary>
    ''' Index of example within the 'latest.xml' file. This file is queried for getting the estimated times.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testResultLatestIndex As Integer

    ''' <summary>
    ''' If example is a multi-part example, this is set to true.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isMultiModel As Boolean
    ''' <summary>
    ''' If example is being generated as a parent example to the multi-part examples, this is set to true.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isParentModel As Boolean

    '=== Title
    ''' <summary>
    ''' Classification of the object type for analyses, such as frame, shell, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleClass As String
    ''' <summary>
    ''' The country or region for which a design code applies. Otherwise, left empty.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleRegion As String
    ''' <summary>
    ''' If an example has both a class and a region property, they are combined here as [Class] / [Region].
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property classRegion As String
    ''' <summary>
    ''' Either Analysis, Design, 'Analysis &amp; Design', or 'Not Specified'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleType As String

    '=== Instructions
    Private _runExample As Boolean
    ''' <summary>
    ''' True: Example will be run in regTest.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property runExample As Boolean
        Set(ByVal value As Boolean)
            'If Parent of a multi-model example, cascade selection through all models
            UpdateChildrenSelection(value)

            If Not _runExample = value Then
                _runExample = value

                'Update time estimates
                If value Then
                    myCsiTester.estimatedTotalRunTimeNum += ConvertTimesNumberMinute(timeRunAssumed)
                Else
                    myCsiTester.estimatedTotalRunTimeNum -= ConvertTimesNumberMinute(timeRunAssumed)
                End If
                myCsiTester.UpdateEstimatedTimes()

                'Update datagrid
                If Not myCsiTester.checkboxClick Then RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("runExample"))
            End If
        End Set
        Get
            Return _runExample
        End Get
    End Property

    Private _ranExample As Boolean
    ''' <summary>
    ''' True: Example has been run. Results will automatically be read back into the GUI upon load if this is saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ranExample As Boolean
        Set(ByVal value As Boolean)
            'If Parent of a multi-model example, cascade selection through all models
            UpdateChildrenSelection(value)

            If Not _ranExample = value Then
                _ranExample = value

                'Update datagrid
                'If Not myCsiTester.checkboxClick Then RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("compareExample"))
            End If
        End Set
        Get
            Return _ranExample
        End Get
    End Property

    Private _compareExample As Boolean
    ''' <summary>
    ''' True: Example results generated from regTest will be read and added to the GUI display.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property compareExample As Boolean
        Set(ByVal value As Boolean)
            'If Parent of a multi-model example, cascade selection through all models
            UpdateChildrenSelection(value)

            If Not _compareExample = value Then
                _compareExample = value

                If value Then
                    myCsiTester.estimatedTotalCompareTimeNum += ConvertTimesNumberMinute(timeCompareAssumed)
                Else
                    myCsiTester.estimatedTotalCompareTimeNum -= ConvertTimesNumberMinute(timeCompareAssumed)
                End If
                myCsiTester.UpdateEstimatedTimes()

                'Update datagrid
                If Not myCsiTester.checkboxClick Then RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("compareExample"))
            End If
        End Set
        Get
            Return _compareExample
        End Get
    End Property

    Private _comparedExample As Boolean
    ''' <summary>
    ''' True: Example has been compared. Results will automatically be read back into the GUI upon load if this is saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property comparedExample As Boolean
        Set(ByVal value As Boolean)
            'If Parent of a multi-model example, cascade selection through all models
            UpdateChildrenSelection(value)

            If Not _comparedExample = value Then
                _comparedExample = value

                'Update datagrid
                'If Not myCsiTester.checkboxClick Then RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("compareExample"))
            End If
        End Set
        Get
            Return _comparedExample
        End Get
    End Property

    ''' <summary>
    ''' Specifies whether or not the outputSettings.xml file is being used with the model file. This overwrites any parameters in the existing model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputSettingsUsed As Boolean

    ''' <summary>
    ''' Expected name of the exported tables file from the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputFileName As String
    ''' <summary>
    ''' Expected file extension of the exported tables file from the example, as either saved in the model or imposed by the '_OutputSettings.xml' file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputFileExtension As String

#End Region

#Region "Properties: Model Control XML File"
    ''' <summary>
    ''' Class that mirrors the model control XML file. XML-related properties of the example class are gathered from here.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myMCModel As cMCModel

    ''' <summary>
    ''' Program for which the model is to be used. There can be more than one program, so this is taken to be the first one specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property targetProgram As eCSiProgram

    '=== Title Elements
    ''' <summary>
    ''' Overall classification of the example, such as "Published Verification", "Regression", or "Internal Verification".
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property classificationLevel1 As String
    ''' <summary>
    ''' Secondary classification of the example. Options available are dependent upon the Level 1 classification. 
    ''' For "Published Verification", this is the section of the verification suite, such as 'Analysis', or 'Composite Beam', etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property classificationLevel2 As String

    Private _numberCodeExample As String
    ''' <summary>
    ''' Code name (if applicable) and number for the example. Usually this is the file name of the model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numberCodeExample As String
        Set(ByVal value As String)
            If Not _numberCodeExample = value Then
                _numberCodeExample = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("numberCodeExample"))
            End If
        End Set
        Get
            Return _numberCodeExample
        End Get
    End Property
    Private _titleExample As String
    ''' <summary>
    ''' Title name of the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property titleExample As String
        Set(ByVal value As String)
            If Not _titleExample = value Then
                _titleExample = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("titleExample"))
            End If
        End Set
        Get
            Return _titleExample
        End Get
    End Property

    Private _benchmarkLastVersion As String
    ''' <summary>
    ''' The last version of the program in which the benchmark had been updated and confirmed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property benchmarkLastVersion As String
        Set(ByVal value As String)
            If Not _benchmarkLastVersion = value Then
                _benchmarkLastVersion = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("benchmarkLastVersion"))
            End If
        End Set
        Get
            Return _benchmarkLastVersion
        End Get
    End Property

    '=== Link Elements
    ''' <summary>
    ''' Path to the documentation PDF file associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property linkDocumentation As String
    ''' <summary>
    ''' Path to the attachments folder associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property linkAttachments As String
    ''' <summary>
    ''' Path to the Excel file that is associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property linkExcel As String
    ''' <summary>
    ''' List of links associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property linksLinks As ObservableCollection(Of cMCLink)

    '=== Time Elements
    Private _timeRunAssumed As String
    ''' <summary>
    ''' Time estimated to run the example. This is first gathered from the model control XML, and then updated from the latest.xml results file, if available.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeRunAssumed As String
        Set(ByVal value As String)
            If Not _timeRunAssumed = value Then
                _timeRunAssumed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeRunAssumed"))
            End If
        End Set
        Get
            Return _timeRunAssumed
        End Get
    End Property
    Private _timeRunActual As String
    ''' <summary>
    ''' Actual time that it took to run the example in the analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeRunActual As String
        Set(ByVal value As String)
            If Not _timeRunActual = value Then
                _timeRunActual = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeRunActual"))
            End If
        End Set
        Get
            Return _timeRunActual
        End Get
    End Property

    Private _timeCompareAssumed As String
    ''' <summary>
    ''' Time estimated to compare the example. This is first gathered from the model control XML, and then updated from the latest.xml results file, if available.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeCompareAssumed As String
        Set(ByVal value As String)
            If Not _timeCompareAssumed = value Then
                _timeCompareAssumed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeCompareAssumed"))
            End If
        End Set
        Get
            Return _timeCompareAssumed
        End Get
    End Property
    Private _timeCompareActual As String
    ''' <summary>
    ''' Actual time that it took to compare the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeCompareActual As String
        Set(ByVal value As String)
            If Not _timeCompareActual = value Then
                _timeCompareActual = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeCompareActual"))
            End If
        End Set
        Get
            Return _timeCompareActual
        End Get
    End Property

    Private _timeCheckAssumed As String
    ''' <summary>
    ''' Time estimated to run and compare the example. Summed result of the assumed run and compare time components.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>    
    Public Property timeCheckAssumed As String
        Set(ByVal value As String)
            If Not _timeCheckAssumed = value Then
                _timeCheckAssumed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeCheckAssumed"))
            End If
        End Set
        Get
            Return _timeCheckAssumed
        End Get
    End Property

    Private _timeCheckActual As String
    ''' <summary>
    ''' Actual time that it took to run and compare the example. Summed result of the actual run and compare time components.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property timeCheckActual As String
        Set(ByVal value As String)
            If Not _timeCheckActual = value Then
                _timeCheckActual = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeCheckActual"))
            End If
        End Set
        Get
            Return _timeCheckActual
        End Get
    End Property

    '=== Comparison Elements
    ''' <summary>
    ''' Number of results compared straight from results produced by the analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numResults As Integer
    ''' <summary>
    ''' Number of results compared using an Excel file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numResultsExcel As Integer
    ''' <summary>
    ''' Number of results within the example that underwent post-processing and calculation beyond the results obtained from the analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numResultsPostProcessed As Integer
    ''' <summary>
    ''' Total number of results compared for the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numResultsTotal As Integer

    Private _itemList As ObservableCollection(Of cExampleItem)
    ''' <summary>
    ''' Collection of tabular row comparisons within the example. These are what appear in the example details datagrid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property itemList As ObservableCollection(Of cExampleItem)
        Set(ByVal value As ObservableCollection(Of cExampleItem))
            _itemList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("itemList"))
        End Set
        Get
            Return _itemList
        End Get
    End Property

    '=== Misc Elements
    ''' <summary>
    ''' List of keywords associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property keywordsList As ObservableCollection(Of String)

    '=== Results   
    Private _runStatus As String
    ''' <summary>
    ''' Status of the run, such as 'Completed'.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property runStatus As String
        Set(ByVal value As String)
            If Not _runStatus = value Then
                _runStatus = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("runStatus"))
            End If
        End Set
        Get
            Return _runStatus
        End Get
    End Property

    Private _cellClassRun As String
    ''' <summary>
    ''' Indicator of status for color formatting of the run status cell in datagrid views.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property cellClassRun As String
        Set(ByVal value As String)
            If Not _cellClassRun = value Then
                _cellClassRun = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("cellClassRun"))
            End If
        End Set
        Get
            Return _cellClassRun
        End Get
    End Property

    Private _compareStatus As String
    ''' <summary>
    ''' Status of the comparison, such as 'Completed', or 'no file exists, etc.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property compareStatus As String
        Set(ByVal value As String)
            If Not _compareStatus = value Then
                _compareStatus = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("compareStatus"))
            End If
        End Set
        Get
            Return _compareStatus
        End Get
    End Property

    Private _cellClassCompare As String
    ''' <summary>
    ''' Indicator of status for color formatting of the compare status cell in datagrid views.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property cellClassCompare As String
        Set(ByVal value As String)
            If Not _cellClassCompare = value Then
                _cellClassCompare = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("cellClassCompare"))
            End If
        End Set
        Get
            Return _cellClassCompare
        End Get
    End Property

    Private _percentDifferenceMax As String
    ''' <summary>
    ''' Maximum precent difference between the rounded result and benchmark result for the example.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property percentDifferenceMax As String
        Set(ByVal value As String)
            If Not _percentDifferenceMax = value Then
                _percentDifferenceMax = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("percentDifferenceMax"))
            End If
        End Set
        Get
            Return _percentDifferenceMax
        End Get
    End Property

    Private _overallResult As String
    ''' <summary>
    ''' Overall result of the example, which includes max % difference, and the run status (if not successful) or compare status (if not successful, but run was successful).
    ''' </summary>
    ''' <remarks></remarks>
    Public Property overallResult As String
        Set(ByVal value As String)
            If Not _overallResult = value Then
                _overallResult = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("overallResult"))
            End If
        End Set
        Get
            Return _overallResult
        End Get
    End Property

    Private _cellClassMaxResult As String
    ''' <summary>
    ''' Indicator of status for color formatting of the overall result status cell in datagrid views or text styles.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property cellClassMaxResult As String
        Set(ByVal value As String)
            If Not _cellClassMaxResult = value Then
                _cellClassMaxResult = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("cellClassMaxResult"))
            End If
        End Set
        Get
            Return _cellClassMaxResult
        End Get
    End Property

    '=== Misc Information
    Private _programVersion As String
    ''' <summary>
    ''' Version of the program used to run the example.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property programVersion As String
        Set(ByVal value As String)
            If Not _programVersion = value Then
                _programVersion = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programVersion"))
            End If
        End Set
        Get
            Return _programVersion
        End Get
    End Property

    Private _programBuild As String
    ''' <summary>
    ''' Build of the program used to run the example.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property programBuild As String
        Set(ByVal value As String)
            If Not _programBuild = value Then
                _programBuild = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programBuild"))
            End If
        End Set
        Get
            Return _programBuild
        End Get
    End Property

    Private _runDateTime As String
    ''' <summary>
    ''' Date and time of the analysis run.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property runDateTime As String
        Set(ByVal value As String)
            If Not _runDateTime = value Then
                _runDateTime = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("runDateTime"))
            End If
        End Set
        Get
            Return _runDateTime
        End Get
    End Property

    Private _modelID As String
    ''' <summary>
    ''' Unique ID used to track and reference the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property modelID As String
        Set(ByVal value As String)
            If Not _modelID = value Then
                _modelID = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("modelID"))
            End If
        End Set
        Get
            Return _modelID
        End Get
    End Property
#End Region

#Region "Properties: Parent Class"
    ''' <summary>
    ''' Test set that the match was found in. Used to attach model to test set after formulation of a multi-model example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testSetNumber As Integer
    ''' <summary>
    ''' List of the model ids of the models that are children to the parent of a multi-model example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subModelIDs As List(Of String)
    ''' <summary>
    ''' List of the model paths of the models that are children to the parent of a multi-model example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subModelPaths As List(Of String)
#End Region

#Region "Initialization"
    '=== Dummy Class Initialization
    ''' <summary>
    ''' Generates Example class populated with dummy data
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
        'Set Independent Data
        SetDummyValues()

        SetDefaults()
        SetTotalCheckTimes()

        'Generate Output Entries
        InitializeExampleItemsDummy()
    End Sub
    ''' <summary>
    ''' Populates class with dummy data if the class has not been referred to an existing XML
    ''' </summary>
    ''' <remarks>Only used if instantiating an empty cExampleItem class</remarks>
    Private Sub SetDummyValues()
        '==Example Summary Dummy Data
        'Title Elements
        numberCodeExample = "AISC ASD-89 Ex001"
        titleExample = "Wide Flange Member Under Bending"

        benchmarkLastVersion = "v16.1.0"

        'Link Elements
        linkDocumentation = "..\Manuals\Verification\Design\SteelFrame\AISC ASD-89 Example001.pdf"
        linkAttachments = "\Attachments"
        linkExcel = "\Attachments\AISC ASD-89 Ex001.xls"

        'Time Elements
        timeRunAssumed = "00:00:15"
        timeCompareAssumed = "0:00:07"

        'Example Summary Element
        classRegion = "New Zealand"
    End Sub
    ''' <summary>
    ''' Initializes all cExampleItem classes with dummy values and adds them to the cExample Class. 
    ''' </summary>
    ''' <remarks>Only used if instantiating an empty cExampleItem class</remarks>
    Private Sub InitializeExampleItemsDummy()
        itemList = New ObservableCollection(Of cExampleItem)       'Collection of example items created

        Dim i As Long
        For i = 0 To 50
            exampleItem = New cExampleItem

            itemList.Add(exampleItem)   'Collection of example items populated
        Next

    End Sub

    '=== Normal Class Initialization
    ''' <summary>
    ''' Generates Example class, populated by data stored in external XML files
    ''' </summary>
    ''' <param name="newExamplePath">Path to XML referenced for class data</param>
    ''' <param name="newExampleIndex">Index of example in the csiTester class collection.</param>
    ''' <remarks>Perhaps adjust this to directly take in the </remarks>
    Friend Sub New(ByVal newExamplePath As String, Optional ByVal newExampleIndex As Integer = 0, _
                   Optional ByVal isParent As Boolean = False, Optional ByVal modelBaseID As String = "", _
                   Optional ByVal modelSubIDs As List(Of String) = Nothing, Optional ByVal modelXMLPaths As List(Of String) = Nothing)
        Try
            'Initialization Properties
            pathXmlMC = newExamplePath
            exampleIndex = newExampleIndex

            isParentModel = False
            isMultiModel = False

            If isParent Then
                isMultiModel = True
                isParentModel = True
                modelID = modelBaseID
                subModelIDs = modelSubIDs
                subModelPaths = modelXMLPaths
            End If

            SetDefaults()

            If Not isParent Then
                InitializeExampleData()
            Else
                InitializeParentData()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Initializes all data used by a typical, standalone example.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeExampleData()
        Try
            myMCModel = New cMCModel(pathXmlMC)

            ''=== XML Operations ===
            ReadWriteExampleXML()  'Populate Data
            InitializeExampleItems()                'Generate Output Entries
            ''=== End XML Operations ===
            SetFileNameModelImported()


            'Check MultiModel Status
            CheckIfMultiModel()                     'Makes adjustments if example is part of a multi-model example set (e.g. 15a, 15b, etc.)

            'Set Independent Data
            SetTotalCheckTimes()

            'Sync with other XML files
            SetRunStatus()
            SetCompareStatus()
            SetRanStatus()
            SetComparedStatus()
            myCsiTester.UpdateEstimatedTimes()

            'Check state of using outputSettings.xml file
            CheckOutputSettingsFileActive()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Initializes data used by a parent example for an example group.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeParentData()
        RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_GENERATE_PARENT_CLASS))
        CreateParentClassProperties()
    End Sub

    ''' <summary>
    ''' Initializes all cExampleItem classes and adds them to the cExample Class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeExampleItems()
        If Not (numResults > 0 OrElse numResultsPostProcessed > 0 OrElse numResultsExcel > 0) Then Exit Sub
        If _xmlReaderWriter.InitializeXML(pathXmlMC) Then
            Dim i As Integer
            itemList = New ObservableCollection(Of cExampleItem)       'Collection of example items created

            'Gather raw results
            If numResults > 0 Then
                For i = 0 To numResults - 1
                    exampleItem = New cExampleItem(True, eResultType.regular, i)
                    exampleItem.result = myMCModel.results.resultsRegular(i)
                    itemList.Add(exampleItem)   'Collection of example items populated
                Next
            End If

            'Gather post-processed results
            If numResultsPostProcessed > 0 Then
                For i = 0 To numResultsPostProcessed - 1
                    exampleItem = New cExampleItem(True, eResultType.postProcessed, i)
                    'exampleItem.resultPostProcessed = myMCModel.results.resultsPostProcessed(i)
                    itemList.Add(exampleItem)   'Collection of example items populated
                Next
            End If

            ' Gather Excel Results
            If numResultsExcel > 0 Then
                For i = 0 To numResultsExcel - 1
                    exampleItem = New cExampleItem(True, eResultType.excelCalculated, i)
                    exampleItem.result = myMCModel.results.resultsExcel(i)
                    itemList.Add(exampleItem)   'Collection of example items populated
                Next
            End If
            _xmlReaderWriter.CloseXML()
        End If
    End Sub

    ''' <summary>
    ''' If the model file is imported, this procedure generates the expected model file name that is used in the analysis.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFileNameModelImported()
        If myMCModel.importedModel Then
            If testerSettings.programName = eCSiProgram.ETABS Then
                fileNameModelImported = GetPathFileName(pathModelFile, True) & testerSettings.versionImportTag
            Else
                fileNameModelImported = GetPathFileName(pathModelFile, True)
            End If
        Else
            fileNameModelImported = ""
        End If
    End Sub

    ''' <summary>
    ''' Checks if the example being populated is part of a set that is composed of multiple models
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckIfMultiModel()
        If StringExistInName(modelID, ".") Then     'Model is part of a multi-model set
            isMultiModel = True
        End If
    End Sub

    ''' <summary>
    ''' Checks RegTest to see if the model is set to run and sets class property to match.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRunStatus()
        Try
            runExample = False

            If testerSettings.examplesRunSaved.Count > 0 Then
                For Each myModelID As String In testerSettings.examplesRunSaved
                    If modelID = myModelID Then
                        runExample = True
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        'Update estimated times
        myCsiTester.estimatedTotalRunTimeNum += ConvertTimesNumberMinute(timeRunAssumed)
    End Sub
    ''' <summary>
    ''' Checks settings file to see if the model is set to be compared and sets class property to match.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCompareStatus()
        Try
            compareExample = False

            If testerSettings.examplesCompareSaved.Count > 0 Then

                For Each myModelID As String In testerSettings.examplesCompareSaved
                    If modelID = myModelID Then
                        compareExample = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        'Update estimated times
        myCsiTester.estimatedTotalCompareTimeNum += ConvertTimesNumberMinute(timeCompareAssumed)
    End Sub

    ''' <summary>
    ''' Checks settings file to see if the model has had its run results shown in a prior session and sets class property to match.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRanStatus()
        Try
            ranExample = False

            If testerSettings.examplesRanSaved.Count > 0 Then
                For Each myModelID As String In testerSettings.examplesRanSaved
                    If modelID = myModelID Then
                        ranExample = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub


    ''' <summary>
    ''' Checks settings file to see if the model has had its results compared in a prior session and sets class property to match.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComparedStatus()
        Try
            comparedExample = False

            If testerSettings.examplesComparedSaved.Count > 0 Then
                For Each myModelID As String In testerSettings.examplesComparedSaved
                    If modelID = myModelID Then
                        comparedExample = True
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets default starting values for example
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaults()

        'Times
        'If record of prior run times exists, 'Assumed' values will be overwritten
        timeRunAssumed = TIME_RUN_ASSUMED_DEFAULT
        timeCompareAssumed = TIME_COMPARE_ASSUMED_DEFAULT
        timeCheckAssumed = TIME_CHECK_ASSUMED_DEFAULT
        timeRunActual = _TIME_RUN_ACTUAL_DEFAULT
        timeCompareActual = _TIME_COMPARE_ACTUAL_DEFAULT
        timeCheckActual = _TIME_CHECK_ACTUAL_DEFAULT


        '==Example Summary Defaults
        'Results
        runStatus = GetEnumDescription(eResultRun.notRun)
        compareStatus = GetEnumDescription(eResultCompare.notCompared)
        percentDifferenceMax = GetEnumDescription(eResultOverall.notChecked)
        overallResult = GetEnumDescription(eResultOverall.notChecked)

        'Misc Information
        programVersion = PROGRAM_VERSION_DEFAULT
        programBuild = _PROGRAM_BUILD_DEFAULT
        runDateTime = _RUN_DATETIME_DEFAULT
    End Sub
#End Region

#Region "Methods: Reading, Writing & Updating"
    ''' <summary>
    ''' Populates summary with data from example XML file
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleXML() 'ByVal examplePath As String) ', ByVal read As Boolean)  ''' <param name="examplePath">Path to the XML file</param> ''' <param name="read">Specify whether to read values from XML or write values to XML</param>
        Try
            ReadWriteExampleXmlNode() 'read)
            ReadWriteExampleXmlListKeywords() 'read)
            GetClassRegion()
            GetExampleType()

            'ReadWriteExampleXmlObject(read)
            If String.IsNullOrEmpty(numberCodeExample) Then numberCodeExample = "id " & modelID
            'If Not read Then SaveXML(examplePath)

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the path to the results XML file, based on the provided directory path for the regTest results.
    ''' </summary>
    ''' <param name="dirPathResults">Path to the results files created by regTest.</param>
    ''' <remarks></remarks>
    Friend Sub SetResultsXMLPath(ByVal dirPathResults As String)
        pathXmlModelResults = dirPathResults & "\model_" & modelID & ".xml"
    End Sub

    ''' <summary>
    ''' Reads the XML of test results, if they exist.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadResultsXML()
        If ReadResultsRunXML() Then ReadResultsCompareXML()
    End Sub

    ''' <summary>
    ''' Reads the XML of the test run results, if they exist. If not, returns false.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadResultsRunXML() As Boolean
        Dim pathNode As String
        ReadResultsRunXML = False

        'Run Result
        pathNode = "//n:status/n:running_analysis"
        runStatus = _xmlReaderWriter.ReadNodeText(pathNode)
        cellClassRun = _xmlReaderWriter.ReadNodeText(pathNode, "cell_class")

        'Check Run Status for further instructions
        If String.IsNullOrEmpty(runStatus) Then                                                              'Results XML has no values
            runStatus = GetEnumDescription(eResultRun.notRunYet)
            compareStatus = GetEnumDescription(eResultCompare.notRunYet)
            myCsiTester.exampleNotComparedIDs.Add(modelID)
            Exit Function
        Else                                                                                'XML results values
            Select Case runStatus.ToUpper
                Case GetEnumDescription(eResultRun.completed).ToUpper, GetEnumDescription(eResultRun.manual).ToUpper
                    runStatus = GetEnumDescription(eResultRun.completedRun)     'Completed

                Case GetEnumDescription(eResultRun.timeOut).ToUpper
                    runStatus = GetEnumDescription(eResultRun.timeOut)          'Time Out
                    Exit Function

                Case GetEnumDescription(eResultRun.running).ToUpper, GetEnumDescription(eResultRun.runningCurrently).ToUpper
                    runStatus = GetEnumDescription(eResultRun.running)          'Running

                Case GetEnumDescription(eResultRun.toBeRun).ToUpper
                    runStatus = GetEnumDescription(eResultRun.notRunYet)        'Not Run Yet
            End Select
        End If

        'Actual Time
        pathNode = "//n:actual_runtime/n:running_analysis/n:minutes"
        timeRunActual = ConvertTimesStringMinutes(CDbl(_xmlReaderWriter.ReadNodeText(pathNode)))
        If String.IsNullOrEmpty(timeRunActual) Then
            If runStatus = GetEnumDescription(eResultRun.notRun) Then
                timeRunActual = _TIME_RUN_ACTUAL_DEFAULT
            Else
                timeRunActual = _TIME_RUN_ACTUAL_FAIL_RESULT
            End If
        End If

        pathNode = "//n:actual_runtime/n:total/n:minutes"
        timeCheckActual = ConvertTimesStringMinutes(CDbl(_xmlReaderWriter.ReadNodeText(pathNode)))
        If String.IsNullOrEmpty(timeCheckActual) Then
            If (runStatus = GetEnumDescription(eResultRun.notRun) Or compareStatus = GetEnumDescription(eResultCompare.notCompared)) Then
                timeCheckActual = _TIME_CHECK_ACTUAL_DEFAULT
            Else
                timeCheckActual = _TIME_CHECK_ACTUAL_FAIL_RESULT
            End If
        End If

        'Example Summary
        pathNode = "//n:analyzed_in_version"
        programVersion = _xmlReaderWriter.ReadNodeText(pathNode) 'regTest.program_version '

        pathNode = "//n:analyzed_in_build"
        programBuild = _xmlReaderWriter.ReadNodeText(pathNode) 'regTest.program_build '

        pathNode = "//n:start_date"
        runDateTime = _xmlReaderWriter.ReadNodeText(pathNode)

        ReadResultsRunXML = True
    End Function

    ''' <summary>
    ''' Reads XML of test compare results, if they exist
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadResultsCompareXML()
        Dim pathNode As String

        'Compare Result
        pathNode = "//n:status/n:retrieving_database_results"
        compareStatus = _xmlReaderWriter.ReadNodeText(pathNode)
        cellClassCompare = _xmlReaderWriter.ReadNodeText(pathNode, "cell_class")

        'Check compare status for further instructions
        If StringsMatch(compareStatus, GetEnumDescription(eResultCompare.success)) Then
            compareStatus = GetEnumDescription(eResultCompare.successCompared)
        ElseIf Not StringsMatch(compareStatus, GetEnumDescription(eResultCompare.successCompared)) Then
            If StringExistInName(compareStatus, GetEnumDescription(eResultCompare.comparing)) Then
                compareStatus = GetEnumDescription(eResultCompare.comparing)
            ElseIf StringExistInName(compareStatus, GetEnumDescription(eResultCompare.dbReadFailure)) Then
                compareStatus = GetEnumDescription(eResultCompare.dbReadFailure)
            ElseIf StringExistInName(compareStatus, GetEnumDescription(eResultCompare.noDBFile)) Then
                compareStatus = GetEnumDescription(eResultCompare.noDBFile)
            End If
            Exit Sub
        End If

        pathNode = "//n:max_percent_difference/from_benchmark"
        percentDifferenceMax = _xmlReaderWriter.ReadNodeText(pathNode)
        cellClassMaxResult = _xmlReaderWriter.ReadNodeText(pathNode, "cell_class")

        pathNode = "//n:actual_runtime/n:retrieving_database_results/n:minutes"
        timeCompareActual = ConvertTimesStringMinutes(CDbl(_xmlReaderWriter.ReadNodeText(pathNode)))
        If String.IsNullOrEmpty(timeCompareActual) Then
            If compareStatus = GetEnumDescription(eResultCompare.notCompared) Then
                timeCompareActual = _TIME_COMPARE_ACTUAL_DEFAULT
            Else
                timeCompareActual = _TIME_CHECK_ACTUAL_FAIL_RESULT
            End If
        End If
    End Sub

    ''' <summary>
    ''' Updates remaining fields after example has been run and compared
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateRunAndCompare()
        Try
            Dim i As Integer = 0

            'Update Summary & Title/Time Results
            ReadResultsXML()

            'Adjust if run not completed
            If (Not runStatus = GetEnumDescription(eResultRun.completedRun) AndAlso Not runStatus = GetEnumDescription(eResultRun.manual)) Then
                Exit Sub
            End If

            'Continue filling example results if check completed
            If IsNumeric(percentDifferenceMax) Then
                percentDifferenceMax = percentDifferenceMax & "%"
            Else
                percentDifferenceMax = GetEnumDescription(eResultOverall.percDiffNotAvailable)
            End If

            'Update Example Item Results
            For Each myExampleItem As cExampleItem In itemList
                myExampleItem.UpdateResults(myExampleItem.resultType, i)
                i = i + 1
            Next

            SetTotalCheckTimes()
            ranExample = True
            comparedExample = True
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Updates the run results after an example has been run. No comparison of results is done.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateRun()
        Try
            Dim i As Integer = 0

            'Update Run Results
            ReadResultsRunXML()

            'Adjust if run & comparisons not completed
            If (runStatus = GetEnumDescription(eResultRun.completedRun) OrElse runStatus = GetEnumDescription(eResultRun.manual)) Then
                SetTotalCheckTimes()
                ranExample = True
                Exit Sub
            Else
                Exit Sub
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Resets example result values to default values, and (if specified) deletes any analysis results if they exist.
    ''' </summary>
    ''' <param name="deleteAnalysisFiles">If True, analysis files associated with the example will be deleted.</param>
    ''' <remarks></remarks>
    Friend Sub ResetRun(ByVal deleteAnalysisFiles As Boolean)
        ResetCompare()
        ranExample = False
        runStatus = GetEnumDescription(eResultRun.notRun)

        'Delete analysis results if they exist
        If deleteAnalysisFiles Then myCsiTester.DeleteAnalysisFilesNextExample(Me, True) ', True)
    End Sub

    ''' <summary>
    ''' Resets example compare values to default values.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ResetCompare()
        'Times
        timeRunActual = _TIME_RUN_ACTUAL_DEFAULT
        timeCompareActual = _TIME_COMPARE_ACTUAL_DEFAULT
        timeCheckActual = _TIME_CHECK_ACTUAL_DEFAULT


        '==Example Summary Defaults
        'Results
        comparedExample = False
        compareStatus = GetEnumDescription(eResultCompare.notCompared)
        percentDifferenceMax = GetEnumDescription(eResultOverall.notChecked)

        'Misc Information
        programVersion = PROGRAM_VERSION_DEFAULT
        programBuild = _PROGRAM_BUILD_DEFAULT
        runDateTime = _RUN_DATETIME_DEFAULT
    End Sub

    ''' <summary>
    ''' Updates the assumed run time in the example class properties, and saves the new value to the associated xml file.
    ''' </summary>
    ''' <param name="timeNew"></param>
    ''' <remarks></remarks>
    Friend Sub UpdateTimes(ByVal timeNew As String)
        Dim pathNode As String = "//n:model/n:run_time/n:minutes"

        timeRunAssumed = timeNew
        myMCModel.runTime = ConvertTimesNumberMinute(timeNew)

        If _xmlReaderWriter.InitializeXML(pathXmlMC) Then
            If _xmlReaderWriter.NodeExists(pathNode) Then
                _xmlReaderWriter.WriteNodeText(CStr(myMCModel.runTime), pathNode)
            Else
                _xmlReaderWriter.CreateNodeByPath("//n:model/n:date", "run_time", "", eXMLElementType.Header, eNodeCreate.insertAfter)
                _xmlReaderWriter.CreateNodeByPath("//n:model/n:date/n:run_time", "minutes", CStr(myMCModel.runTime), eXMLElementType.Node, eNodeCreate.child)
            End If

            _xmlReaderWriter.SaveXML(pathXmlMC)
            _xmlReaderWriter.CloseXML()
        End If
    End Sub

    ''' <summary>
    ''' Updates the benchmark values in the model control XML and updates the benchmark version reference.
    ''' </summary>
    ''' <param name="matchResult">If true, benchmarks will be saved to match the rounded results of the examples results. 
    ''' If false, the benchmarks will be saved with whatever values are stored in the class.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateBenchmarks(ByVal matchResult As Boolean)
        If Not (numResults > 0 OrElse numResultsPostProcessed > 0 OrElse numResultsExcel > 0) Then Exit Sub

        'Benchmark attribution validation
        Dim updateBM As Boolean = True
        With myMCModel.program
            If Not .programName = myRegTest.program_name Then
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNoCancel, eMessageType.Question),
                                            _PROMPT_UPDATE_BM_MISMATCH_PROGRAM_NEW & myRegTest.program_name & Environment.NewLine &
                                            _PROMPT_UPDATE_BM_MISMATCH_PROGRAM_OLD & .programName & Environment.NewLine & Environment.NewLine &
                                            _PROMPT_UPDATE_BM_MISMATCH_PROGRAM_ACTION,
                                            _TITLE_UPDATE_BM_MISMATCH_PROGRAM)
                    Case eMessageActions.Yes
                        .programName = myRegTest.program_name
                        .programVersion = myRegTest.program_version
                    Case eMessageActions.No
                    Case eMessageActions.Cancel : updateBM = False
                End Select
            Else
                .programName = myRegTest.program_name
                .programVersion = myRegTest.program_version
            End If
        End With

        If (updateBM AndAlso
            _xmlReaderWriter.InitializeXML(pathXmlMC)) Then
            Dim i As Integer
            Dim iStart As Integer = 0

            'Update raw benchmarks to results
            If numResults > 0 Then
                For i = 0 To numResults - 1
                    UpdateMCModelBenchmark(i)
                    'itemList(i).UpdateBenchmarks(eResultType.regular, i, matchResult)
                Next
                iStart += numResults
            End If

            'Update post-processed benchmarks to results
            If numResultsPostProcessed > 0 Then
                For i = 0 To numResultsPostProcessed - 1
                    UpdateMCModelBenchmark(iStart + i)
                    ' itemList(iStart + i).UpdateBenchmarks(eResultType.postProcessed, i, matchResult)
                Next
                iStart += numResultsPostProcessed
            End If

            ' Update Excel benchmarks to results
            If numResultsExcel > 0 Then
                For i = 0 To numResultsExcel - 1
                    UpdateMCModelBenchmark(iStart + i)
                Next
            End If

            'Update benchmark program & version attribute
            myMCModel.SaveFile(pathXmlMC)
            _xmlReaderWriter.CloseXML()
        End If

    End Sub

    Private Sub UpdateMCModelBenchmark(ByVal p_i As Integer)
        'Create a result update object for this update
        If Not itemList(p_i).benchmarkValue = itemList(p_i).checkResultRounded Then
            Dim resultUpdate As New cMCResultUpdate
            Dim update As cMCUpdate = myMCModel.updates(myMCModel.updates.Count - 1)

            With resultUpdate
                .id = update.id
                .comment = update.comment
            End With

            myMCModel.results(p_i).updates.Add(resultUpdate)
        End If

        'Update other aspects of the MC result object
        With itemList(p_i)
            If (Not String.IsNullOrEmpty(.checkResultRounded) AndAlso
                Not .checkResultRounded = RESULT_DEFAULT AndAlso
                Not .checkResultRounded = RESULT_NONE AndAlso
                Not .checkResultRounded = RESULT_NOT_COMPARED AndAlso
                Not .checkResultRounded = RESULT_NOT_AVAILABLE) Then

                'Account for zero number results, where the rounded result might be very small, e.g. BM = 0, Result = 1E-15
                If (IsNumeric(.benchmarkValue) AndAlso
                    myCDbl(.benchmarkValue) = 0 AndAlso
                    .percentDifferenceBenchmark = "0%") Then

                    myMCModel.results(p_i).benchmark.valueBenchmark = "0"
                Else
                    myMCModel.results(p_i).benchmark.valueBenchmark = itemList(p_i).checkResultRounded
                End If
            End If
        End With
    End Sub

    '''<summary>
    ''' Updates the other values in the model control XML, such as user-changed independent values, or output parameter descriptions.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateOthers() 'ByVal matchResult As Boolean)
        If Not (numResults > 0 OrElse numResultsPostProcessed > 0 OrElse numResultsExcel > 0) Then Exit Sub

        Dim updateOthers As Boolean = True
        If (updateOthers AndAlso
            _xmlReaderWriter.InitializeXML(pathXmlMC)) Then
            Dim i As Integer
            Dim iStart As Integer = 0

            'Update raw benchmarks to results
            If numResults > 0 Then
                For i = 0 To numResults - 1
                    itemList(i).UpdateOthers(eResultType.regular, i)
                Next
                iStart = numResults
            End If

            'Update post-processed benchmarks to results
            If numResultsPostProcessed > 0 Then
                For i = 0 To numResultsPostProcessed - 1
                    itemList(iStart + i).UpdateOthers(eResultType.postProcessed, i)
                Next
            End If
            _xmlReaderWriter.SaveXML(pathXmlMC)
            _xmlReaderWriter.CloseXML()

            ' Update Excel Results
            ' TODO!

            'Update benchmark program & version attribute
            myMCModel.SaveFile(pathXmlMC)
        End If
    End Sub

    ''' <summary>
    ''' Returns the overall result of an example based on either the example's property, or the equivalent supplied override. Sets an optional boolean status accordingly, and sets the example's overallResult property as specified.
    ''' </summary>
    ''' <param name="p_failedExample">If the example has failed a check, this is true. The function will change this value based on the state of the example.</param>
    ''' <param name="p_setExampleProperty">If true (default), the example property will be updated. If false, this function only returns a hypothetical result.</param>
    ''' <param name="p_expctRunStatus">The expected run status. Defaults to the example's run status.</param>
    ''' <param name="p_expctCompareStatus">The expected compare status. Defaults to the example's compare status.</param>
    ''' <param name="p_expctPercentDifferenceMax">The expected maximum percent difference. Defaults to the example's maximum percent difference.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetOverallResult(Optional ByRef p_failedExample As Boolean = False,
                                     Optional ByVal p_setExampleProperty As Boolean = True,
                                     Optional ByVal p_expctRunStatus As String = "",
                                     Optional ByVal p_expctCompareStatus As String = "",
                                     Optional ByVal p_expctPercentDifferenceMax As String = "") As String
        Dim tempOverallResult As String = ""
        Dim tempRunStatus As String
        Dim tempCompareStatus As String
        Dim tempPercentDifferenceMax As String

        If String.IsNullOrEmpty(p_expctRunStatus) Then
            tempRunStatus = runStatus
        Else
            tempRunStatus = p_expctRunStatus
        End If
        If String.IsNullOrEmpty(p_expctCompareStatus) Then
            tempCompareStatus = compareStatus
        Else
            tempCompareStatus = p_expctCompareStatus
        End If
        If String.IsNullOrEmpty(p_expctPercentDifferenceMax) Then
            tempPercentDifferenceMax = percentDifferenceMax
        Else
            tempPercentDifferenceMax = p_expctPercentDifferenceMax
        End If

        If (StringsMatch(tempRunStatus, GetEnumDescription(eResultRun.notRun)) AndAlso
            StringsMatch(tempCompareStatus, GetEnumDescription(eResultCompare.notCompared))) Then  'Example has not been attempted to run or compare
            If p_setExampleProperty Then overallResult = tempPercentDifferenceMax
        Else        'Example has been attempted to be run and/or compared
            If Not StringsMatch(tempCompareStatus, GetEnumDescription(eResultCompare.notRunYet)) Then
                If Not StringsMatch(tempCompareStatus, GetEnumDescription(eResultCompare.outputFileMissing)) Then                               'Output file was generated
                    If (Not StringsMatch(tempRunStatus, GetEnumDescription(eResultRun.completedRun)) AndAlso
                        Not StringsMatch(tempRunStatus, GetEnumDescription(eResultRun.manual))) Then   'Example failed to run properly
                        p_failedExample = True
                        tempOverallResult = tempRunStatus
                    ElseIf Not StringsMatch(tempCompareStatus, GetEnumDescription(eResultCompare.successCompared)) Then         'Example failed to check properly
                        p_failedExample = True
                        tempOverallResult = tempCompareStatus
                    ElseIf Not IsNumeric(GetPrefix(tempPercentDifferenceMax, "%")) Then                                                'Example had some other failure
                        p_failedExample = True
                        tempOverallResult = GetEnumDescription(eResultOverall.overallResultError)
                    ElseIf Not CDbl(GetPrefix(tempPercentDifferenceMax, "%")) = 0 Then                                                 '% difference exists
                        p_failedExample = True
                        tempOverallResult = tempPercentDifferenceMax
                    End If
                Else                                                                                                                'Example output files missing
                    p_failedExample = True
                    tempOverallResult = tempCompareStatus
                End If
            End If
            If p_setExampleProperty Then overallResult = tempOverallResult
        End If

        Return tempOverallResult
    End Function

#End Region

#Region "Methods: Parent Class"
    ''' <summary>
    ''' Creates a variation of the example class that serves as a parent entry to the multiple models that share the same example
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateParentClassProperties()
        Try
            Dim modelCount As Integer = 0
            Dim subModelFound As Boolean = False

            'TODO: Updated properties need to be communicated, such as the following: Instructions, Results, Misc Information

            itemList = New ObservableCollection(Of cExampleItem)

            'Search existing classes to find those that correspond to the parent class base ID
            For Each myMultiModelID As String In subModelIDs                                'For each sub ID of the Parent
                testSetNumber = 0
                For Each myExampleTestSet As cExampleTestSet In examplesTestSetList         'Check each example in each test set
                    If Not (StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples))) Then
                        For Each myExample As cExample In myExampleTestSet.examplesList
                            If myMultiModelID = myExample.modelID Then                          'If model IDs match, populate properties
                                If modelCount = 0 Then                                          'If Model  is first in the list
                                    'Title Elements
                                    classificationLevel1 = myExample.classificationLevel1
                                    classificationLevel2 = myExample.classificationLevel2
                                    titleExample = myExample.titleExample
                                    benchmarkLastVersion = myExample.benchmarkLastVersion

                                    'Link Elements
                                    linkDocumentation = myExample.linkDocumentation
                                    linkAttachments = myExample.linkAttachments
                                    linkExcel = myExample.linkExcel '
                                    keywordsList = myExample.keywordsList
                                    GetMultiModelNumberCodeExample()

                                    'Example Summary Properties
                                    'Title
                                    exampleClass = myExample.exampleClass
                                    exampleRegion = myExample.exampleRegion
                                    classRegion = myExample.classRegion
                                    exampleType = myExample.exampleType

                                    'Add Accumulating Properties, such as time, and result entries
                                    AddParentAccumulatedProperties(myExample)
                                ElseIf modelCount = subModelIDs.Count - 1 Then                  'If model is last model in the list
                                    'Populate basic properties based on count
                                    testSetIndex = myExampleTestSet.examplesList.Count

                                    'Add Accumulating Properties, such as time, and result entries
                                    AddParentAccumulatedProperties(myExample)
                                Else
                                    'Add Accumulating Properties, such as time, and result entries
                                    AddParentAccumulatedProperties(myExample)
                                End If

                                modelCount += 1
                                subModelFound = True
                                Exit For
                            End If
                        Next
                    End If

                    If subModelFound Then Exit For
                    testSetNumber += 1
                Next
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Adds properties to the parent class that accumulate from the original example classes.
    ''' </summary>
    ''' <param name="myExample">Example currently being included within the parent example.</param>
    ''' <remarks></remarks>
    Private Sub AddParentAccumulatedProperties(ByVal myExample As cExample)
        Try
            Dim tempTime As Double

            'Time Elements
            tempTime = ConvertTimesNumberMinute(timeRunAssumed) + ConvertTimesNumberMinute(myExample.timeRunAssumed)
            timeRunAssumed = ConvertTimesStringMinutes(tempTime)

            tempTime = ConvertTimesNumberMinute(timeCompareAssumed) + ConvertTimesNumberMinute(myExample.timeCompareAssumed)
            timeCompareAssumed = ConvertTimesStringMinutes(tempTime)

            tempTime = ConvertTimesNumberMinute(timeCheckAssumed) + ConvertTimesNumberMinute(myExample.timeCheckAssumed)
            timeCheckAssumed = ConvertTimesStringMinutes(tempTime)

            'Comparison Elements
            numResults += myExample.numResults
            numResultsPostProcessed += myExample.numResultsPostProcessed
            numResultsTotal += myExample.numResultsTotal

            'Comparison Items
            For Each myExampleItem As cExampleItem In myExample.itemList
                Dim tempExampleItem As New cExampleItem
                tempExampleItem = myExampleItem
                tempExampleItem.subExample = FilterStringFromName(myExample.numberCodeExample, numberCodeExample, False, True)
                itemList.Add(tempExampleItem)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    Private Sub UpdateChildrenSelection(ByVal value As Boolean)

    End Sub

    'TODO
    Private Sub UpdateParent()
        Try
            Dim modelCount As Integer = 0
            Dim subModelFound As Boolean = False

            'Search existing classes to find those that correspond to the parent class base ID
            For Each myMultiModelID As String In subModelIDs                                'For each sub ID of the Parent
                testSetNumber = 0
                For Each myExampleTestSet As cExampleTestSet In examplesTestSetList         'Check each example in each test set
                    If Not (StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples))) Then
                        For Each myExample As cExample In myExampleTestSet.examplesList
                            If myMultiModelID = myExample.modelID Then                          'If model IDs match, populate properties
                                If modelCount = 0 Then                                          'If Model  is first in the list
                                    'Title Elements
                                    classificationLevel1 = myExample.classificationLevel1
                                    classificationLevel2 = myExample.classificationLevel2
                                    titleExample = myExample.titleExample
                                    benchmarkLastVersion = myExample.benchmarkLastVersion

                                    'Link Elements
                                    linkDocumentation = myExample.linkDocumentation
                                    linkAttachments = myExample.linkAttachments
                                    linkExcel = myExample.linkExcel '
                                    keywordsList = myExample.keywordsList
                                    GetMultiModelNumberCodeExample()

                                    'Example Summary Properties
                                    'Title
                                    exampleClass = myExample.exampleClass
                                    exampleRegion = myExample.exampleRegion
                                    classRegion = myExample.classRegion
                                    exampleType = myExample.exampleType

                                    'Add Accumulating Properties, such as time, and result entries
                                    UpdateParentAccumulatedProperties(myExample)
                                ElseIf modelCount = subModelIDs.Count - 1 Then                  'If model is last model in the list
                                    'Populate basic properties based on count
                                    testSetIndex = myExampleTestSet.examplesList.Count

                                    'Add Accumulating Properties, such as time, and result entries
                                    UpdateParentAccumulatedProperties(myExample)
                                Else
                                    'Add Accumulating Properties, such as time, and result entries
                                    UpdateParentAccumulatedProperties(myExample)
                                End If

                                modelCount += 1
                                subModelFound = True
                                Exit For
                            End If
                        Next
                    End If

                    If subModelFound Then Exit For
                    testSetNumber += 1
                Next
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    'TODO
    Private Sub UpdateParentAccumulatedProperties(ByVal myExample As cExample)
        Dim tempTime As Double

        'Time Elements

        tempTime = ConvertTimesNumberMinute(timeRunActual) + ConvertTimesNumberMinute(myExample.timeRunActual)
        timeRunActual = ConvertTimesStringMinutes(tempTime)

        tempTime = ConvertTimesNumberMinute(timeCompareActual) + ConvertTimesNumberMinute(myExample.timeCompareActual)
        timeCompareActual = ConvertTimesStringMinutes(tempTime)

        tempTime = ConvertTimesNumberMinute(timeCheckActual) + ConvertTimesNumberMinute(myExample.timeCheckActual)
        timeCheckActual = ConvertTimesStringMinutes(tempTime)

    End Sub

#End Region

#Region "Methods: Time"
    ''' <summary>
    ''' Generates Check Time property for assumed and actual times. Corrects time components if they are blank
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTotalCheckTimes()

        'Check for empty times and set to default
        If String.IsNullOrWhiteSpace(timeRunAssumed) Then timeRunAssumed = TIME_RUN_ASSUMED_DEFAULT
        If String.IsNullOrWhiteSpace(timeCompareAssumed) Then timeCompareAssumed = TIME_COMPARE_ASSUMED_DEFAULT
        'Set total time
        If Not timeRunAssumed = TIME_RUN_ASSUMED_DEFAULT Or Not timeCompareAssumed = TIME_COMPARE_ASSUMED_DEFAULT Then
            timeCheckAssumed = TotalTimes(timeRunAssumed, timeCompareAssumed)
        End If

        'Check for empty times and set to default
        If String.IsNullOrWhiteSpace(timeRunActual) Then timeRunActual = _TIME_RUN_ACTUAL_DEFAULT
        If String.IsNullOrWhiteSpace(timeCompareActual) Then timeCompareActual = _TIME_COMPARE_ACTUAL_DEFAULT
        'Set total time
        If Not timeRunActual = _TIME_RUN_ACTUAL_DEFAULT Or Not timeCompareActual = _TIME_COMPARE_ACTUAL_DEFAULT Then
            timeCheckActual = TotalTimes(timeRunActual, timeCompareActual)
        End If
    End Sub

    ''' <summary>
    ''' Combines Run Times and Compare Times together into a total Check Time property
    ''' </summary>
    ''' <param name="timeRun">Time it took to open model, run analysis, export tables, and close the model</param>
    ''' <param name="timeCompare">Time it took to open the Access file, query the data, fill it into the XML file, and compare results</param>
    ''' <returns>Time it took to run a given example, from first opening the model to completing comparison of results</returns>
    ''' <remarks></remarks>
    Private Function TotalTimes(ByVal timeRun As String, ByVal timeCompare As String) As String
        Dim timeRunNum As Double
        Dim timeCompareNum As Double
        Dim timeTotalNum As Double

        'Convert time components to numbers for addition
        timeRunNum = ConvertTimesNumberMinute(timeRun)
        timeCompareNum = ConvertTimesNumberMinute(timeCompare)

        'Add numerical timeRun & timeCompare together for total time in seconds
        timeTotalNum = timeRunNum + timeCompareNum

        'Convert back to hh:mm:ss format
        TotalTimes = ConvertTimesStringMinutes(timeTotalNum)
    End Function

#End Region

#Region "Methods: XML Read/Write Master Functions"
    ''' <summary>
    ''' Reads from or writes to [example].XML, with unique properties.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleXmlNode() '
        Try
            With myMCModel
                'Program & Model File
                targetProgram = .targetProgram.primary
                pathModelFile = .modelFile.pathDestination.path

                'Model ID for RegTest
                modelID = .ID.idComposite

                'Title Elements
                With .classification
                    classificationLevel1 = .level1
                    classificationLevel2 = .level2
                End With
                numberCodeExample = .secondaryID
                titleExample = .title
                benchmarkLastVersion = .program.programVersion

                'Linked Elements
                'Attachments - General
                'Assumes model files in database structure
                linkAttachments = GetPathDirectoryStub(pathXmlMC) & "\" & DIR_NAME_ATTACHMENTS_DEFAULT

                'Attachments - Documentation
                ReadExampleDocumentation()

                'Excel Results
                If (.resultsExcel IsNot Nothing AndAlso
                    .resultsExcel.Count = 1) Then linkExcel = .resultsExcel.filePath

                'Estimated Times
                Try
                    timeRunAssumed = ConvertTimesStringMinutes(.runTime)
                Catch ex As Exception
                    timeRunAssumed = TIME_RUN_ASSUMED_DEFAULT
                End Try

                'Example Summary Element
                'Get number of Example Items
                If _xmlReaderWriter.InitializeXML(pathXmlMC) Then
                    Dim pathNode As String = "//n:postprocessed_results"
                    If _xmlReaderWriter.NodeExists(pathNode) Then
                        numResultsPostProcessed = _xmlReaderWriter.CountChildNodes(pathNode)
                    Else
                        numResultsPostProcessed = 0
                    End If
                    _xmlReaderWriter.CloseXML()
                End If

                numResults = .results.resultsRegular.Count
                numResultsExcel = .results.resultsExcel.Count
                'numResultsPostProcessed = .results.resultsPostProcessed.Count
                numResultsTotal = numResults + numResultsPostProcessed + numResultsExcel
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Assembles the path to the documentation file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadExampleDocumentation() 'ByVal pathNode As String)   '''' <param name="pathNode">Path to the lowest level unique parent node under which the documenation information is stored.</param>
        Dim documentationPathSegment As String = ""
        Dim documentationPathEnd As String = ""

        'Determine Documentation Path Segment & Assemble filepath based on release status

        '       If myCsiTester.releaseStatus = eCSiTesterLevel.Published Then

        'Determine Documentation Path End
        'documentationPathEnd = ListObjectByKey(pathNode, "Published Documentation", "path")

        For Each attachment As cFileAttachment In myMCModel.attachments
            If StringExistInName(attachment.title, TAG_ATTACHMENT_DOCUMENTATION_PUBLISHED) Then documentationPathEnd = attachment.PathAttachment.path
        Next

        If Not String.IsNullOrEmpty(documentationPathEnd) Then
            If classificationLevel2 = "Design Verification Suite - Steel Frame" Then
                documentationPathSegment = testerSettings.documentsPathStubDesignSteelFrame
            ElseIf classificationLevel2 = "Design Verification Suite - Concrete Frame" Then
                documentationPathSegment = testerSettings.documentsPathStubDesignConcreteFrame
            ElseIf classificationLevel2 = "Design Verification Suite - Shear Wall" Then
                documentationPathSegment = testerSettings.documentsPathStubDesignShearWall
            ElseIf classificationLevel2 = "Design Verification Suite - Composite Beam" Then
                documentationPathSegment = testerSettings.documentsPathStubDesignCompositeBeam
            ElseIf classificationLevel2 = "Design Verification Suite - Composite Column" Then
                documentationPathSegment = testerSettings.documentsPathStubDesignCompositeColumn
            ElseIf classificationLevel2 = "Design Verification Suite - Slab" Then
                documentationPathSegment = testerSettings.documentsPathStubDesignSlab
            Else
                documentationPathSegment = testerSettings.documentsPathStubAnalysis
            End If

            linkDocumentation = FilterStringFromName(myRegTest.program_file.path, GetSuffix(myRegTest.program_file.path, "\"), True, False) & documentationPathSegment & "\" & documentationPathEnd
            'linkDocumentation = pathStartup() & "\" & documentationPathSegment & "\" & documentationPathEnd
        End If

        'Else
        ''Determine Documentation Path End
        'documentationPathEnd = ListObjectByKey(pathNode, "//n:title", "Unpublished Documentation", "//n:path")

        ''Assumes model files in database structure
        'linkDocumentation = linkAttachments & "\" & documentationPathEnd
        'End If

    End Sub

    ''' <summary>
    ''' Reads from or writes to [example].XML, with properties lists.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleXmlListKeywords() 'ByVal read As Boolean)   '''' <param name="read">Specify whether to read values from XML or write values to XML</param>
        keywordsList = New ObservableCollection(Of String)
        linksLinks = New ObservableCollection(Of cMCLink)

        keywordsList = myMCModel.keywords.NamesToObservableCollection
        linksLinks = myMCModel.links.ToObservableCollection
    End Sub

    ''' <summary>
    ''' Determines whether an example can be grouped in a class or region, depending on keyword specification. This is used for a table header and possible future organization.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetClassRegion()
        For Each keywordItem As String In keywordsList
            If Left(keywordItem, Len(TAG_KEYWORD_CODE_REGION)) = TAG_KEYWORD_CODE_REGION Then
                exampleRegion = Right(keywordItem, Len(keywordItem) - Len(TAG_KEYWORD_CODE_REGION))
                TrimWhiteSpace(exampleRegion)
                classRegion = exampleRegion
            ElseIf Left(keywordItem, Len(TAG_KEYWORD_ANALYSIS_CLASS)) = TAG_KEYWORD_ANALYSIS_CLASS Then
                exampleClass = Right(keywordItem, Len(keywordItem) - Len(TAG_KEYWORD_ANALYSIS_CLASS))
                TrimWhiteSpace(exampleClass)
                If classRegion IsNot Nothing Then
                    If Not classRegion = exampleClass Then
                        classRegion = classRegion & " \ " & exampleClass 'Accounts for Compound class/region
                    End If
                Else
                    classRegion = exampleClass
                End If
            End If
        Next

        If String.IsNullOrEmpty(classRegion) Then classRegion = _CLASS_REGION_DEFAULT
    End Sub

    ''' <summary>
    ''' Sets example as either Analysis, Design, 'Analysis and Design', or 'Not Specified'. This is used for title headers and possible future organization
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetExampleType()
        Dim typeAnalysis As Boolean = False
        Dim typeDesign As Boolean = False

        For Each keywordItem As String In keywordsList
            If Left(keywordItem, Len(TAG_KEYWORD_EXAMPLE_TYPE)) = TAG_KEYWORD_EXAMPLE_TYPE Then
                keywordItem = Right(keywordItem, Len(keywordItem) - Len(TAG_KEYWORD_EXAMPLE_TYPE))
                TrimWhiteSpace(keywordItem)

                If keywordItem = TYPE_ANALYSIS Then typeAnalysis = True
                If keywordItem = TYPE_DESIGN Then typeDesign = True
            End If
        Next

        If typeAnalysis And typeDesign Then
            exampleType = TYPE_ANALYSIS_DESIGN
        ElseIf typeAnalysis Then
            exampleType = TYPE_ANALYSIS
        ElseIf typeDesign Then
            exampleType = TYPE_DESIGN
        Else
            exampleType = TYPE_DEFAULT
        End If
    End Sub

    ''' <summary>
    ''' Determines the basic example title of a Multi-model set based on a specially tagged keyword
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMultiModelNumberCodeExample()
        For Each keywordItem As String In keywordsList
            If Left(keywordItem, Len(TAG_KEYWORD_MULTI_MODEL)) = TAG_KEYWORD_MULTI_MODEL Then
                numberCodeExample = Right(keywordItem, Len(keywordItem) - Len(TAG_KEYWORD_MULTI_MODEL))
                TrimWhiteSpace(numberCodeExample)
            End If
        Next
    End Sub

    ''' <summary>
    ''' INCOMPLETE: Reads from or writes to [example].XML, with properties objects, which may contain lists.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteExampleXmlObject(ByVal read As Boolean)
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
#End Region

#Region "Methods: OutputSettings File & Exported Table Sets"
    ''' <summary>
    ''' Checks if the outputSettings.xml file exists at the model location and is properly named. 
    ''' If so, class properties record the path and mark the file as active, and the function returns true.
    ''' Else, the function returns false.
    ''' </summary>
    ''' <param name="checkModelSource">If True, then the model source path is checked. If False, the model destination is checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckOutputSettingsFileActive(Optional ByVal checkModelSource As Boolean = False) As Boolean
        Dim pathOutputSettingsSrc As String
        Dim pathOutputSettingsAltSrc As String
        Dim pathOutputSettingsDest As String
        Dim pathOutputSettingsAltDest As String

        'Check Destination
        pathOutputSettingsDest = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(pathModelFile, True)) & "\" & GetPathFileName(pathModelFile, True) & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
        pathOutputSettingsAltDest = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(pathModelFile, True)) & "\" & GetPathFileName(pathModelFile, True) & testerSettings.outputSettingsVersionSession & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML

        If IO.File.Exists(pathOutputSettingsDest) Then
            pathXmlOutputSettingsDest = pathOutputSettingsDest
            If Not checkModelSource Then
                outputSettingsUsed = True
                CheckOutputSettingsFileActive = True
            End If
        ElseIf IO.File.Exists(pathOutputSettingsAltDest) Then
            pathXmlOutputSettingsDest = pathOutputSettingsAltDest
            If Not checkModelSource Then
                outputSettingsUsed = True
                CheckOutputSettingsFileActive = True
            End If
        Else
            pathXmlOutputSettingsDest = ""
        End If

        'Check Source
        pathOutputSettingsSrc = GetPathDirectoryStub(pathModelFile) & "\" & GetPathFileName(pathModelFile, True) & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
        pathOutputSettingsAltSrc = GetPathDirectoryStub(pathModelFile) & "\" & GetPathFileName(pathModelFile, True) & testerSettings.outputSettingsVersionSession & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML

        If IO.File.Exists(pathOutputSettingsSrc) Then
            pathXmlOutputSettingsSrc = pathOutputSettingsSrc
            If checkModelSource Then
                'outputSettingsUsed = True
                'CheckOutputSettingsFileActive = True
            End If
        ElseIf IO.File.Exists(pathOutputSettingsAltSrc) Then
            pathXmlOutputSettingsSrc = pathOutputSettingsAltSrc
            If checkModelSource Then
                'outputSettingsUsed = True
                'CheckOutputSettingsFileActive = True
            End If
        Else
            pathXmlOutputSettingsSrc = ""
            'outputSettingsUsed = False
            'CheckOutputSettingsFileActive = False
        End If

        outputSettingsUsed = False
        CheckOutputSettingsFileActive = False

        Return CheckOutputSettingsFileActive
    End Function

    ''' <summary>
    ''' Returns the name, with file extension, of the exported tables file. 
    ''' Also assigns these components to the corresponding example class' properties.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetExportedTableNameWExtension() As String
        Dim fileNameExportedTableSets As String = ""
        Dim pathExportedTableSets As String
        Dim fileExtensionTemp As String = ""
        Dim fileExtensions As New List(Of String)

        fileNameExportedTableSets = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(pathModelFile, True)) & "\" & outputFileName & "." & outputFileExtension

        If Not IO.File.Exists(fileNameExportedTableSets) Then
            outputFileExtension = ""

            If outputSettingsUsed Then                                                  'Check outputSettings.xml
                If IO.File.Exists(pathXmlOutputSettingsDest) Then
                    _xmlReaderWriter.GetSingleXMLNodeValue(pathXmlOutputSettingsDest, "//n:filename", fileNameExportedTableSets)
                    outputFileExtension = GetSuffix(fileNameExportedTableSets, ".")

                    'If Not String.IsNullOrEmpty(testerSettings.tableExportFileExtensionAll) Then                 'Override with global constraint
                    '    outputFileExtension = testerSettings.tableExportFileExtensionAll
                    'End If
                End If
            Else                                                                        'Check MC XML
                fileNameExportedTableSets = myMCModel.dataSource.pathDestination.fileNameWithExtension
                If Not String.IsNullOrEmpty(fileNameExportedTableSets) Then
                    outputFileExtension = GetSuffix(fileNameExportedTableSets, ".")
                End If
            End If

            'If the file querying methods fail, do more general search
            If String.IsNullOrEmpty(fileNameExportedTableSets) Then                            'Table Set is probably the file name, with either an *.mdb or *.xml extension
                fileExtensions = myMCModel.dataSource.PathExportedTable.AllowedExportedTableFileTypes

                'Determine expected exported table file name. If filetype is different than expected, add it to the list of filetypes to check.
                With myMCModel
                    If String.IsNullOrEmpty(.dataSource.pathDestination.fileNameWithExtension) Then                      'Assume database filename is the same as the model file name
                        pathExportedTableSets = .modelFile.pathDestination.directory & "\" & .modelFile.pathDestination.fileName & "." & fileExtensions(0)
                    Else                                                'Use the provided database filename
                        pathExportedTableSets = .dataSource.pathDestination.path

                        'If specified extension is not included in the provided file extensions list, add it
                        fileExtensions = AddIfNew(fileExtensions, .dataSource.pathDestination.fileExtension, p_placeFirst:=True).ToList
                    End If
                End With

                'If the recorded exported file name is not valid, go a more general search
                If Not IO.File.Exists(pathExportedTableSets) Then
                    'Check the same filename with the remaining file extensions to see if it exists
                    For Each fileExtension As String In fileExtensions
                        If Not fileExtension = fileExtensions(0) Then
                            pathExportedTableSets = FilterStringFromName(pathExportedTableSets, fileExtensionTemp, True, False) & fileExtension
                            If IO.File.Exists(pathExportedTableSets) Then Exit For
                        End If
                        fileExtensionTemp = fileExtension
                    Next
                    If IO.File.Exists(pathExportedTableSets) Then
                        outputFileName = GetPathFileName(pathExportedTableSets, True)
                        outputFileExtension = GetSuffix(outputFileName, ".")
                        fileNameExportedTableSets = outputFileName & "." & outputFileExtension
                    End If
                End If
            End If
        End If

        Return fileNameExportedTableSets
    End Function

    ''' <summary>
    ''' Copies outputSettings.xml file from the attachments folder to the model file level.
    ''' Adjust MC XML file as necessary to keep exported table names in sync.
    ''' </summary>
    ''' <param name="p_tableExportFileExtensionAll">If all examples are set to be run with a uniform file extension type for the exported table files, it is imposed by assigning the global file extension to this property. 
    ''' For this to be used, outputSettingsUsedAll = True.
    ''' If blank, no overwrite will be performed.</param>
    ''' <param name="p_updateAttachments">If true, when outputSettings file is modified with new file extension value, both the activated file and the attachments file will be modified. If false, only the activated file will be modified.</param>
    ''' <param name="p_applyAtSource">If True, the action will be done at the model source location. If False (default), the action will be done at the model destination location.</param>
    ''' <remarks></remarks>
    Friend Sub ActivateOutputSettingsXMLFile(ByVal p_tableExportFileExtensionAll As String,
                                             ByVal p_updateAttachments As Boolean,
                                             Optional ByVal p_applyAtSource As Boolean = False)
        Dim pathNodeDBFileNameOS As String = "//n:filename"
        Dim pathNodeDBFileNameMC As String = "//n:database_file_name"
        Dim pathSource As String
        Dim pathDestination As String
        Dim myPathXmlMC As String
        Dim exportedFileName As String
        Dim exportedFileNameInMCXml As String

        With myMCModel
            For Each attachment As cFileAttachment In .attachments
                If StringExistInName(attachment.title, TAG_ATTACHMENT_TABLE_SET_FILE) Then
                    If p_applyAtSource Then
                        pathSource = myMCModel.mcFile.pathDestination.directory & "\" & attachment.PathAttachment.path
                        pathDestination = .modelFile.pathDestination.directory & "\" & GetPathFileName(pathSource)
                    Else
                        pathSource = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(myMCModel.mcFile.pathDestination.path, True)) & "\" & attachment.PathAttachment.path
                        pathDestination = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(.modelFile.pathDestination.path, True)) & "\" & GetPathFileName(pathSource)
                    End If

                    CopyFile(pathSource, pathDestination, True)

                    'Enforce global overwrite of exported table file type
                    If Not String.IsNullOrEmpty(p_tableExportFileExtensionAll) Then
                        exportedFileName = ""
                        _xmlReaderWriter.GetSingleXMLNodeValue(pathDestination, pathNodeDBFileNameOS, exportedFileName)
                        exportedFileName = FilterStringFromName(exportedFileName, GetSuffix(exportedFileName, "."), True, False) & p_tableExportFileExtensionAll
                        _xmlReaderWriter.WriteSingleXMLNodeValue(pathDestination, pathNodeDBFileNameOS, exportedFileName)

                        'Update original file, if specified
                        If p_updateAttachments Then _xmlReaderWriter.WriteSingleXMLNodeValue(pathSource, pathNodeDBFileNameOS, exportedFileName)

                        'MC XML should be modified to changes in outputSettings extension if it is not blank
                        exportedFileNameInMCXml = ""
                        If p_applyAtSource Then
                            myPathXmlMC = pathXmlMC
                        Else
                            myPathXmlMC = myCsiTester.ConvertPathModelSourceToDestination(pathXmlMC)
                        End If
                        _xmlReaderWriter.GetSingleXMLNodeValue(myPathXmlMC, pathNodeDBFileNameMC, exportedFileNameInMCXml)
                        If Not String.IsNullOrEmpty(exportedFileNameInMCXml) Then _xmlReaderWriter.WriteSingleXMLNodeValue(myPathXmlMC, pathNodeDBFileNameMC, exportedFileName)
                    End If
                    Exit For
                End If
            Next
        End With
    End Sub

    ''' <summary>
    ''' Deletes outputSettings.xml file at the model level, unless the file is listed as a supporting file in the MC attachments.
    ''' Adjust MC XML file as necessary to keep exported table names in sync.
    ''' </summary>
    ''' <param name="applyAtSource">If True, the action will be done at the model source location. If False (default), the action will be done at the model destination location.</param>
    ''' <remarks></remarks>
    Friend Sub DeactivateOutPutSettingsXMLFile(Optional ByVal applyAtSource As Boolean = False)
        Dim pathNode As String = "//n:database_file_name"
        Dim pathSource As String
        Dim pathDestination As String
        Dim myPathXmlMC As String = ""
        Dim exportedFileNameInMCXml As String
        Dim deleteOSFile As Boolean

        deleteOSFile = True
        With myMCModel
            pathDestination = ""
            For Each attachment As cFileAttachment In .attachments
                If StringExistInName(attachment.title, TAG_ATTACHMENT_TABLE_SET_FILE) Then
                    If applyAtSource Then
                        pathSource = .mcFile.pathDestination.directory & "\" & attachment.PathAttachment.path
                        pathDestination = .modelFile.pathDestination.directory & "\" & GetPathFileName(pathSource)
                    Else
                        pathSource = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(.mcFile.pathDestination.directory, True)) & "\" & attachment.PathAttachment.path
                        pathDestination = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(.modelFile.pathDestination.path, True)) & "\" & GetPathFileName(pathSource)
                    End If
                End If

                'Check if file should not be deleted, such as for v9.7.4 model file imports
                If (StringExistInName(attachment.title, TAG_ATTACHMENT_SUPPORTING_FILE) AndAlso
                    StringExistInName(attachment.title, cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML)) Then deleteOSFile = False
            Next
            If (deleteOSFile AndAlso
                Not String.IsNullOrEmpty(pathDestination) AndAlso
                IO.File.Exists(pathDestination)) Then

                DeleteFile(pathDestination, False)
                deleteOSFile = True

                'MC XML should be modified to changes in outputSettings extension if it is not blank
                exportedFileNameInMCXml = ""
                If applyAtSource Then
                    myPathXmlMC = pathXmlMC
                Else
                    myPathXmlMC = myCsiTester.ConvertPathModelSourceToDestination(pathXmlMC)
                End If

                _xmlReaderWriter.GetSingleXMLNodeValue(myPathXmlMC, pathNode, exportedFileNameInMCXml)
                If Not String.IsNullOrEmpty(exportedFileNameInMCXml) Then                                                'Get value recorded in source MC XML file and write it to the destination file
                    _xmlReaderWriter.GetSingleXMLNodeValue(myPathXmlMC, pathNode, exportedFileNameInMCXml)
                    _xmlReaderWriter.WriteSingleXMLNodeValue(myPathXmlMC, pathNode, exportedFileNameInMCXml)
                End If
            End If
        End With
    End Sub
#End Region

#Region "Test Components"
    ''' <summary>
    ''' Validates that items related to the running of the example have been properly reset.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="chkRunStatus">Check if example is set to be run.</param>
    ''' <param name="chkRanStatus">Check if example is marked as having been run.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResetRun(ByVal className As String, Optional ByVal chkRunStatus As Boolean = False, Optional ByVal chkRanStatus As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If chkRunStatus Then
                .expectation = "Run status set to not run"
                .resultActual = CStr(runExample)
                .resultActualCall = classIdentifier & "runExample"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass
            End If
            If chkRanStatus Then
                .expectation = "Example is marked as having not been run"
                .resultActual = CStr(ranExample)
                .resultActualCall = classIdentifier & "ranExample"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example ran result status is as expected"
                .resultActual = runStatus
                .resultActualCall = classIdentifier & "runStatus"
                .resultExpected = GetEnumDescription(eResultRun.notRun)
                If Not .RunSubTest() Then Return subTestPass
            End If
        End With

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that items related to the comparing of the example have been properly reset.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="resultIDsList">List of the results by index used to check specific results within the example. If not supplied, all results are checked.</param>
    ''' <param name="chkCompareStatus">Check if example is set to be compared.</param>
    ''' <param name="chkComparedStatus">Check if example is marked as having been compared.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResetCompare(ByVal className As String, Optional ByVal resultIDsList As List(Of Integer) = Nothing, Optional ByVal chkCompareStatus As Boolean = False, Optional ByVal chkComparedStatus As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        With e2eTester
            If chkCompareStatus Then
                .expectation = "Compare status set to not compare"
                .resultActual = CStr(compareExample)
                .resultActualCall = classIdentifier & "compareStatus"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass
            End If
            If chkComparedStatus Then
                'Results
                .expectation = "Example marked as having not been compared"
                .resultActual = CStr(comparedExample)
                .resultActualCall = classIdentifier & "comparedExample"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example compared result status is as expected"
                .resultActual = compareStatus
                .resultActualCall = classIdentifier & "compareStatus"
                .resultExpected = GetEnumDescription(eResultCompare.notCompared)
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example has the default % difference"
                .resultActual = CStr(percentDifferenceMax)
                .resultActualCall = classIdentifier & "percentDifferenceMax"
                .resultExpected = GetEnumDescription(eResultOverall.notChecked)
                If Not .RunSubTest() Then Return subTestPass

                'Time
                .expectation = "Example has the default actual time to run"
                .resultActual = CStr(timeRunActual)
                .resultActualCall = classIdentifier & "timeRunActual "
                .resultExpected = _TIME_RUN_ACTUAL_DEFAULT
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example has the default actual time to compare"
                .resultActual = CStr(timeCompareActual)
                .resultActualCall = classIdentifier & "timeCompareActual "
                .resultExpected = _TIME_COMPARE_ACTUAL_DEFAULT
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example has the default actual time to check"
                .resultActual = CStr(timeCheckActual)
                .resultActualCall = classIdentifier & "timeCheckActual "
                .resultExpected = _TIME_CHECK_ACTUAL_DEFAULT
                If Not .RunSubTest() Then Return subTestPass

                'Misc Information
                .expectation = "Example has the default version"
                .resultActual = CStr(programVersion)
                .resultActualCall = classIdentifier & "programVersion"
                .resultExpected = PROGRAM_VERSION_DEFAULT
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example has the default build"
                .resultActual = CStr(programBuild)
                .resultActualCall = classIdentifier & "programBuild"
                .resultExpected = _PROGRAM_BUILD_DEFAULT
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Example has the default date & time of run"
                .resultActual = CStr(runDateTime)
                .resultActualCall = classIdentifier & "runDateTime"
                .resultExpected = _RUN_DATETIME_DEFAULT
                If Not .RunSubTest() Then Return subTestPass

                'Result Items
                If resultIDsList Is Nothing Then
                    i = 0
                    For Each resultItem As cExampleItem In itemList
                        If Not resultItem.VldtResultReset(classIdentifier & "itemList[{" & cExampleItem.CLASS_STRING & "} " & i & "]", True) Then Return subTestPass
                        i += 1
                    Next
                Else
                    For Each resultID As Integer In resultIDsList
                        i = 0
                        For Each resultItem As cExampleItem In itemList
                            If i = resultID Then
                                If Not resultItem.VldtResultReset(classIdentifier & "itemList[{" & cExampleItem.CLASS_STRING & "} " & i & "]", True) Then Return subTestPass
                            End If
                            i += 1
                        Next
                    Next
                End If
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the example is selected (or not) to be run.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="selectRun">If true, example is expected to be selected to run.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExampleSelectedRun(ByVal className As String, Optional ByVal selectRun As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If selectRun Then
                .expectation = "Example is selected to be run"
                .resultExpected = "True"
            Else
                .expectation = "Example is not selected to be run"
                .resultExpected = "False"
            End If
            .resultActual = CStr(runExample)
            .resultActualCall = classIdentifier & "runExample"
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the example is selected (or not) to be compared.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="selectCompare">If true, example is expected to be selected to compared.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExampleSelectedCompare(ByVal className As String, Optional ByVal selectCompare As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        Try
            With e2eTester
                If selectCompare Then
                    .expectation = "Example is selected to be compared"
                    .resultExpected = "True"
                Else
                    .expectation = "Example is not selected to be compared"
                    .resultExpected = "False"
                End If
                .resultActual = CStr(compareExample)
                .resultActualCall = classIdentifier & "compareExample"
                If Not .RunSubTest() Then Return subTestPass
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the example has been run, as well as other associated properties.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="expctdRan">If true, the example is assumed to have been run.</param>
    ''' <param name="expctdRunReset">If true, it is expected that the example was set to run even though it was not run in the check.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExampleRan(ByVal className As String, Optional ByVal expctdRan As Boolean = False, Optional ByVal expctdRunReset As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If expctdRan Then                                   'Lookup expected example results from test
                If Not VldtExampleRanTrue(classIdentifier) Then Return subTestPass
            Else                                                'Expected example results are default
                If expctdRunReset Then
                    If Not VldtResetRun(classIdentifier, , True) Then Return subTestPass
                Else
                    If Not VldtExampleRanFalse(classIdentifier) Then Return subTestPass
                End If
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates properties assuming that the example has been run.
    ''' </summary>
    ''' <param name="classIdentifier">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtExampleRanTrue(ByVal classIdentifier As String) As Boolean
        Dim subTestPass As Boolean

        With e2eTester
            Dim example As cE2EExample
            Dim destination As cE2ETestDestination

            .expectation = "Example is marked as having been run"
            .resultExpected = "True"
            .resultActual = CStr(ranExample)
            .resultActualCall = classIdentifier & "ranExample"
            If Not .RunSubTest() Then Return subTestPass

            '   Set up example expectation objects
            destination = e2eTester.GetDestinationByTestID(e2eTester.currentTestID)
            If destination Is Nothing Then Return subTestPass
            example = destination.GetExampleByID(modelID)
            If example Is Nothing Then Return subTestPass

            '   Run Status
            .expectation = "Example ran result status is as expected"
            .resultExpected = example.expectedRunStatus
            .resultActual = runStatus
            .resultActualCall = classIdentifier & "runStatus"
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates properties assuming that the example has not been run.
    ''' </summary>
    ''' <param name="classIdentifier">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtExampleRanFalse(ByVal classIdentifier As String) As Boolean
        Dim subTestPass As Boolean

        With e2eTester
            .expectation = "Example is marked as having not been run"
            .resultExpected = "False"
            .resultActual = CStr(ranExample)
            .resultActualCall = classIdentifier & "ranExample"
            If Not .RunSubTest() Then Return subTestPass

            '   Run Status
            .expectation = "Example ran result status is as expected"
            If compareStatus = GetEnumDescription(eResultCompare.notRunYet) Then
                .resultExpected = GetEnumDescription(eResultRun.notRunYet)
            ElseIf compareStatus = GetEnumDescription(eResultCompare.outputFileMissing) Then
                .resultExpected = GetEnumDescription(eResultRun.outputFileMissing)
            Else
                .resultExpected = GetEnumDescription(eResultRun.notRun)
            End If
            .resultActual = runStatus
            .resultActualCall = classIdentifier & "runStatus"
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the example has been compared.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="expctdCompared">If true, the example is assumed to have been compared.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExampleCompared(ByVal className As String, Optional ByVal expctdCompared As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If expctdCompared Then                              'Lookup expected example results from test
                If Not VldtExampleComparedTrue(classIdentifier) Then Return subTestPass
            Else                                                'Expected example results are default
                If Not VldtResetCompare(className, , , True) Then Return subTestPass
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates properties assuming that the example has been compared.
    ''' </summary>
    ''' <param name="classIdentifier">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtExampleComparedTrue(ByVal classIdentifier As String) As Boolean
        Dim subTestPass As Boolean

        With e2eTester
            Dim example As cE2EExample
            Dim destination As cE2ETestDestination

            .expectation = "Example is marked as having been compared"
            .resultExpected = "True"
            .resultActual = CStr(comparedExample)
            .resultActualCall = classIdentifier & "comparedExample"
            If Not .RunSubTest() Then Return subTestPass

            '   Set up example expectation objects
            destination = e2eTester.GetDestinationByTestID(e2eTester.currentTestID)
            If destination Is Nothing Then Return subTestPass
            example = destination.GetExampleByID(modelID)
            If example Is Nothing Then Return subTestPass

            '   Compare Status
            .expectation = "Example compared result status is as expected"
            .resultExpected = example.expectedRunStatus
            .resultActual = compareStatus
            .resultActualCall = classIdentifier & "compareStatus"
            If Not .RunSubTest() Then Return subTestPass

            '   % Change Status
            .expectation = "Example max % change result status is as expected"
            .resultExpected = example.expectedPercentChange
            .resultActual = percentDifferenceMax
            .resultActualCall = classIdentifier & "percentDifferenceMax"
            If Not .RunSubTest() Then Return subTestPass

            '   Overall Result
            .expectation = "Overall result is as expected"
            .resultExpected = example.expectedOverallResult
            .resultActual = overallResult
            .resultActualCall = classIdentifier & "overallResult"
            If Not .RunSubTest() Then Return subTestPass

            '   Version 
            .expectation = "Program version is as expected"
            .resultExpected = e2eTester.GetVersion(myRegTest.program_name)
            .resultActual = programVersion
            .resultActualCall = classIdentifier & "programVersion"
            If Not .RunSubTest() Then Return subTestPass

            '   Build 
            .expectation = "Program build is as expected"
            .resultExpected = e2eTester.GetBuild(myRegTest.program_name)
            .resultActual = programBuild
            .resultActualCall = classIdentifier & "programBuild"
            If Not .RunSubTest() Then Return subTestPass

            '   Actual Time 
            .expectation = "Actual time is as expected"
            .resultExpected = example.expectedTimeCheck
            .resultActual = timeCheckActual
            .resultActualCall = classIdentifier & "timeCheckActual"
            If Not .RunSubTest() Then Return subTestPass

            '   For all specified results
            For Each result As cE2EExampleResult In example.results
                If Not String.IsNullOrEmpty(result.id) Then
                    Dim classNameList As String = classIdentifier & "itemList[{" & cExampleItem.CLASS_STRING & "} " & result.id & "]"
                    If Not itemList(myCInt(result.id)).VldtResultFilled(classNameList, result) Then Return subTestPass
                End If
            Next
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the example has only been listed once in a failed test set.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="uniqueExample">If true, the example has only been listed as failed once. If false, the example has been listed as having failed more than once.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtFailedExampleListedOnce(ByVal className As String, Optional ByVal uniqueExample As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            .expectation = "Example is marked as having been failed once"
            .resultExpected = "True"
            .resultActual = CStr(uniqueExample)
            .resultActualCall = classIdentifier & "VldtFailedExampleListedOnce(uniqueExample)"
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

#End Region

End Class
