Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Possible status returned for the overall status of a checked result.
''' </summary>
''' <remarks></remarks>
Friend Enum eResultOverall
    ''' <summary>
    ''' All individual operations completed successfully.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Checked")> success = 1
    ''' <summary>
    ''' One or more individual operations failed.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Error")> errorResult = 2
    ''' <summary>
    ''' One or more individual operations have "unknown (manual run)" status.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Unknown (Manual Run)")> manual = 3

    <Description("Not Checked")> notChecked = 4
    ''' <summary>
    ''' Percent difference not available.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("N/A")> percDiffNotAvailable = 5

    <Description("Comparison Error")> overallResultError = 6
End Enum