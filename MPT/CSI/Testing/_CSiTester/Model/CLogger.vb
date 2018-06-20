Imports System.Xml
Imports System.Diagnostics


#Region "Enumerations"

''' <summary>
''' Enumeration for log item severity
''' </summary>
''' <remarks></remarks>
Public Enum LoggerSeverity

    Debug
    SeriousError
    Info
    NotSet
    Warning

End Enum

''' <summary>
''' Enumeration for log item level 1 category
''' </summary>
''' <remarks></remarks>
Public Enum LoggerCategory

    ControlFile
    CopyFiles
    Database
    DatabaseConnection
    ExcelResult
    Initialization
    MissingDirectory
    MissingFile
    NotSet
    PostProcessedResult
    Range
    ReadingXMLFile
    RegularResult
    RunningTest
    Selection
    Validation
    XMLValidation

End Enum




#End Region


''' <summary>
''' <para>
''' Class to provide mechanism for processing error, warning and notice messages
''' and writting them to an XML log file.
''' </para>
''' <para>
''' To log items to the log file, call the logger as follows (to clear previous values,
''' set new values and write the log report to the log file).
''' 
''' Logger.Clear()
''' Logger.SetMessage("Logger has been intialized.")
''' Logger.SetCategory(LoggerCategory.Initialization)
''' Logger.SetSeverity(LoggerSeverity.Notice)
''' Logger.SetFunctioName("")
''' Logger.SetFunctionLogItemID(0)
''' Logger.WriteLogItemToFile()
''' 
''' </para>
''' 
''' </summary>
''' <remarks></remarks>
Public Class CLogger

    ' ====== Properties ======

#Region "Properties"

    Private id As Integer ' ID is the log item
    Private dateAndTime As String ' string representing the date and time of the log
    Private category As LoggerCategory
    Private severity As LoggerSeverity
    Private message As String ' log message
    Private functionName As String ' name of the function that generated the error message
    Private functionLogItemID As Double ' number of the log within a function
    Private modelID As Double ' model id (only for log items related to a particular model)
    Private logFilePath As String ' absolute path to the log file
    Private logFileStylesheetPath As String ' absolute path to the log file stylesheet


#End Region


    ' ====== Methods ======

#Region "Initialization"

    ''' <summary>
    ''' Initilizes the object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize()

    End Sub


    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()

        logFileStylesheetPath = ""

    End Sub


#End Region


#Region "Setters"


    ''' <summary>
    ''' Sets message id
    ''' </summary>
    ''' <param name="p_id"></param>
    ''' <remarks></remarks>
    Public Sub SetID(ByVal p_id As Double)

        Me.id = p_id

    End Sub


    ''' <summary>
    ''' Sets level category
    ''' </summary>
    ''' <param name="p_category"></param>
    ''' <remarks></remarks>
    Public Sub SetCategory(ByVal p_category As LoggerCategory)

        category = p_category

    End Sub


    ''' <summary>
    ''' Sets log item severity.
    ''' </summary>
    ''' <param name="p_severity"></param>
    ''' <remarks></remarks>
    Public Sub SetSeverity(ByVal p_severity As LoggerSeverity)

        severity = p_severity

    End Sub



    ''' <summary>
    ''' Sets path to the log file.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Public Sub SetLogFilePath(ByVal p_path As String)

        logFilePath = p_path

    End Sub

    ''' <summary>
    ''' Set path to the stylesheet file
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Public Sub SetLogFileStylesheetPath(ByVal p_path As String)

        logFileStylesheetPath = p_path

    End Sub


    ''' <summary>
    ''' Sets message.
    ''' </summary>
    ''' <param name="p_message">Message to be logged.</param>
    ''' <remarks></remarks>
    Public Sub SetMessage(ByVal p_message As String, Optional ByVal p_logger As CLogger = Nothing)

        message = p_message

        If (Not IsNothing(p_logger)) Then

            functionName = p_logger.functionName
            severity = p_logger.severity
            category = p_logger.category
            functionLogItemID = p_logger.functionLogItemID
            modelID = p_logger.modelID

        End If

    End Sub


    ''' <summary>
    ''' Sets the name of the function in which the log item has been recorded.
    ''' </summary>
    ''' <param name="p_functionName"></param>
    Public Sub SetFunctionName(ByVal p_functionName As String)

        functionName = p_functionName

    End Sub


    ''' <summary>
    ''' Sets log item ID within a function.
    ''' </summary>
    ''' <param name="p_id"></param>
    Public Sub SetFunctionLogItemID(ByVal p_id As Double)

        functionLogItemID = p_id

    End Sub


    ''' <summary>
    ''' Sets model ID.
    ''' </summary>
    ''' <param name="p_id"></param>
    ''' <remarks></remarks>
    Public Sub SetModelID(ByVal p_id As Double)

        modelID = p_id

    End Sub

#End Region


#Region "Writing to a File"


    ''' <summary>
    ''' Writes the current log item to XML log file.
    ''' </summary>
    ''' <returns>0 for success, nonzero for failure</returns>
    Public Function WriteLogItemToFile(Optional ByVal p_writeToConsole As Boolean = False,
                                       Optional ByVal p_supressNewLine As Boolean = False)

        Dim xmlDoc As New XmlDocument ' XmlDocument storing information about about all log items
        Dim xmlDocSingleItem As New XmlDocument ' XmlDocument storing information about single log item

        Dim xmlNode As XmlNode

        Dim idString As String
        Dim categoryString As String
        Dim severityString As String
        Dim functionErrorNumberString As String

        'Dim transformMessage As String

        id = id + 1

        If id = 0 Then
            idString = ""
        Else
            idString = id.ToString()
        End If

        Select Case category

            Case LoggerCategory.ControlFile

                categoryString = "control file"

            Case LoggerCategory.CopyFiles

                categoryString = "copy files"

            Case LoggerCategory.Database

                categoryString = "database"

            Case LoggerCategory.DatabaseConnection

                categoryString = "database connection"

            Case LoggerCategory.ExcelResult

                categoryString = "excel result"

            Case LoggerCategory.Initialization

                categoryString = "initialization"

            Case LoggerCategory.MissingDirectory

                categoryString = "missing directory"

            Case LoggerCategory.MissingFile

                categoryString = "missing file"

            Case LoggerCategory.NotSet

                categoryString = "not set"

            Case LoggerCategory.PostProcessedResult

                categoryString = "post processed result"

            Case LoggerCategory.Range

                categoryString = "range"

            Case LoggerCategory.ReadingXMLFile

                categoryString = "reading xml file"

            Case LoggerCategory.RegularResult

                categoryString = "regular result"

            Case LoggerCategory.RunningTest

                categoryString = "running test"

            Case LoggerCategory.Selection

                categoryString = "selection"

            Case LoggerCategory.Validation

                categoryString = "validation"

            Case LoggerCategory.XMLValidation

                categoryString = "xml validation"

            Case Else

                categoryString = "uncategorized"

        End Select


        Select Case severity

            Case LoggerSeverity.Debug

                severityString = "debug"

            Case LoggerSeverity.SeriousError

                severityString = "error"

            Case LoggerSeverity.Info

                severityString = "info"

            Case LoggerSeverity.Warning

                severityString = "warning"


            Case Else

                severityString = ""

        End Select

        If functionLogItemID = 0 Then
            functionErrorNumberString = ""
        Else
            functionErrorNumberString = functionLogItemID.ToString()
        End If


        'functionName = 

        xmlDoc.Load(logFilePath)

        xmlDocSingleItem.LoadXml("<item>" &
                                 "<id>" & idString & "</id>" &
                                 "<date_and_time>" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "</date_and_time>" &
                                 "<category>" & categoryString & "</category>" &
                                 "<severity>" & severityString & "</severity>" &
                                 "<message>" & message & "</message>" &
                                 "<function>" &
                                 "<name>" & functionName & "</name>" &
                                 "<log_item_id>" & functionErrorNumberString & "</log_item_id>" &
                                 "</function>" &
                                 "<model_id>" & modelID & "</model_id>" &
                                 "</item>")

        xmlNode = xmlDoc.ImportNode(xmlDocSingleItem.DocumentElement, True)
        xmlDoc.SelectSingleNode("//log/items").AppendChild(xmlNode)



        If (p_writeToConsole Or severity = LoggerSeverity.SeriousError) Then

            ' write to console if requested by the function parameter or for serious errors

            If (p_supressNewLine = True) Then

                Console.Write(" " & message)

            Else

                Console.Write(vbCrLf & vbCrLf & "[" & id & "] " & message)

            End If

        End If

        ' save changes to the log file
        xmlDoc.Save(logFilePath)

        ' convert to html file (only if the path to the stylesheet file has been set (after full initialization in CRegTest.Initialize)
        'TO DO:
        'TransformXML(logFilePath, logFileStylesheetPath, logFilePath.Replace(".xml", ".html"), transformMessage)

        'if the severity is error, also update status in the log file
        If (severity = LoggerSeverity.SeriousError) Then

            Me.WriteOverallTestStatusToLogFile("terminated", message)

        End If

        Return 0

    End Function

    ''' <summary>
    ''' Function to update the overall status in the XML log file.
    ''' </summary>
    ''' 
    ''' <param name="p_status">
    ''' Status, one of the following values:
    ''' </param>
    ''' 
    ''' <param name="p_message">
    ''' Message explaining the status in detail.
    ''' </param>
    ''' 
    ''' <returns>
    ''' Zero for success, nonzero for failure
    ''' </returns>
    Public Function WriteOverallTestStatusToLogFile(ByVal p_status As String,
                                                    Optional ByVal p_message As String = "") As Integer

        Dim xmlDoc As New XmlDocument ' XmlDocument storing information about about all log items
        'Dim transformMessage As String

        ' load log file
        xmlDoc.Load(logFilePath)

        xmlDoc.SelectSingleNode("//log/status/value").InnerText = p_status
        xmlDoc.SelectSingleNode("//log/status/message").InnerText = p_message

        ' save changes to the log file
        xmlDoc.Save(logFilePath)

        ' convert to html file (only if the path to the stylesheet file has been set (after full initialization in CRegTest.Initialize)
        'TO DO
        'TransformXML(logFilePath, logFileStylesheetPath, logFilePath.Replace(".xml", ".html"), transformMessage)

        Return 0

    End Function

#End Region


#Region "Miscellaneous"

    ''' <summary>
    ''' Clears content of all internal properties.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Clear()

        dateAndTime = ""
        category = LoggerCategory.NotSet
        severity = LoggerSeverity.NotSet
        message = ""
        functionName = ""
        functionLogItemID = 0
        modelID = 0

    End Sub

#End Region


End Class
