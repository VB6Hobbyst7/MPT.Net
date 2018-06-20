Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Possible status returned for the compared status of a result.
''' </summary>
''' <remarks></remarks>
Friend Enum eResultCompare
    ''' <summary>
    ''' All results specified in model XML file were successfully retrieve from the database file.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Success")> success = 1

    <Description("Compared")> successCompared = 2
    ''' <summary>
    ''' The database file does not exist, or RegTest was unable to establish connection to the database file.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("No DB File")> noDBFile = 3
    ''' <summary>
    ''' RegTest was able to connect to the database file, but was unable to retrieve all the results specified in the model XML.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("DB Read Failure")> dbReadFailure = 4

    <Description("Not Compared")> notCompared = 5

    <Description("Comparing")> comparing = 6

    <Description("Model Needs To Be Run")> notRunYet = 7
    ''' <summary>
    ''' Program is unable to find the exported table files from the analysis programs.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Output File Missing")> outputFileMissing = 8
End Enum