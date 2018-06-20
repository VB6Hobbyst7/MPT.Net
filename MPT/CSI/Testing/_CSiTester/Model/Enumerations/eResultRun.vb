Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Possible status returned for the run status of a result.
''' </summary>
''' <remarks></remarks>
Friend Enum eResultRun
    ''' <summary>
    ''' The analysis/'bridge objects' were successfully updated within the expected time limit.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Completed")> completed = 1

    <Description("Run")> completedRun = 2
    ''' <summary>
    ''' The operation was not completed within the expected time limit.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Time Out")> timeOut = 3
    ''' <summary>
    ''' Designates that the model was run manually and RegTest was only used to retrieve the results from the automatically saved tabular file using the --update-test-results-xml-file command line parameter
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Unknown (Manual Run)")> manual = 4

    <Description("Not Run")> notRun = 5

    <Description("Running")> running = 6

    ''' <summary>
    ''' RegTest label of the example currently running.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Currently Running")> runningCurrently = 7
    ''' <summary>
    ''' RegTest label of an example that is set to be run.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("To Be Run")> toBeRun = 8

    <Description("Not Run Yet")> notRunYet = 9

    <Description("Output File Missing")> outputFileMissing = 10
End Enum