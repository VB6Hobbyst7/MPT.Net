Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Classification of the XML node object found in model xml control files.
''' </summary>
''' <remarks></remarks>
Public Enum eXMLObjectType
    <Description("Incident")> Incident
    <Description("Ticket")> Ticket
    <Description("Link")> Link
    <Description("Attachment")> Attachment
    <Description("Image")> Image
    <Description("Update")> Update
    <Description("Excel Result")> ExcelResult
    <Description("Supporting File")> SupportingFile
    <Description("Documentation")> Documentation
    <Description("Output Settings")> OutputSettings
End Enum