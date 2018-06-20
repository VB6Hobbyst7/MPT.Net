Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

' See: http://stackoverflow.com/questions/3187444/convert-xml-string-to-object#
' See: http://stackoverflow.com/questions/226599/deserializing-xml-to-objects-in-c-sharp
''' <summary>
''' Reads and writes the model control XML file to/from the program's classes.
''' </summary>
''' <remarks></remarks>
Friend Class AdapterModelControl
    Inherits AdapterXmlFile

    ''' <summary>
    ''' Fills the provided object with data from the correspodning XML file.
    ''' Returns the deserialized file as a class.
    ''' </summary>
    ''' <param name="p_modelControl">Object to fill.</param>
    ''' <param name="p_modelControlXml">Deserialized XML file to use to fill the object.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Fill(ByRef p_modelControl As cMCModel,
                           ByVal p_modelControlXml As xmlModelControl)
        Try
            If (p_modelControl Is Nothing OrElse
                p_modelControlXml Is Nothing) Then Exit Sub

            With p_modelControl
                .ClearResults()
                .ClearUpdates()
            End With

            'Transfer data to object
            With p_modelControlXml
                p_modelControl.attachments = ConverterAttachments.ConvertFromFile(.attachments, p_modelControl)

                With .author
                    p_modelControl.author.company = .company
                    p_modelControl.author.name = .name
                End With

                With .classification.value
                    p_modelControl.classification.level1 = ConverterClassificationLevel1.ConvertFromFile(.level_1, testerSettings.exampleClassificationLvl1)
                    p_modelControl.classification.level2 = .level_2
                End With

                p_modelControl.commandLine = .command_line
                p_modelControl.comments = .comments

                With .date
                    p_modelControl.exampleDate.numDay = myCInt(.day)
                    p_modelControl.exampleDate.numMonth = myCInt(.month)
                    p_modelControl.exampleDate.numYear = myCInt(.year)
                End With

                p_modelControl.dataSource.pathSource.SetProperties(.database_file_name)
                p_modelControl.description = .description
                p_modelControl.statusDocumentation = ConverterDocumentationStatus.ConvertFromFile(.documentation_status, testerSettings.documentationStatusTypes)
                '.documentation_statusSpecified

                With .excel_results
                    p_modelControl.AddResultExcel(New cFileExcelResult(.excel_file.path, p_modelControl), p_replaceExisting:=True)
                End With

                p_modelControl.ID.idCompositeDecimal = .id
                p_modelControl.secondaryID = .id_secondary
                p_modelControl.images = ConverterImages.ConvertFromFile(.images, p_modelControl)
                p_modelControl.incidents = ConverterIncidents.ConvertFromFile(.incidents)
                p_modelControl.isBug = ConvertYesTrueBoolean(.is_bug.ToString())
                '.is_bugSpecified
                p_modelControl.isPublic = ConvertYesTrueBoolean(.is_public.ToString())

                p_modelControl.keywords = ConverterKeywords.ConvertFromFile(.keywords)

                p_modelControl.links = ConverterLinks.ConvertFromFile(.links)

                p_modelControl.mcFile.pathDestination.SetProperties(.path.Value)
                '.path.type
                '.path.typeSpecified

                ConverterResultsPostProcessed.ConvertFromFile(.postprocessed_results, p_modelControl)

                With .program
                    p_modelControl.program.programName = ConverterProgramNames.ConvertFromFile(.name)
                    p_modelControl.program.programVersion = .version
                    p_modelControl.program.programVersionLastBest = .version_for_last_best_value
                End With

                With .regtest_internal_use
                    ConverterResultsExcel.ConvertFromFile(.excel_results, p_modelControl)
                End With

                ConverterResults.ConvertFromFile(.results, p_modelControl)
                p_modelControl.runTime = .run_time.minutes

                '.SchemaLocation
                p_modelControl.statusExample = ConverterExampleStatus.ConvertFromFile(.status, testerSettings.statusTypes)
                '.statusSpecified

                p_modelControl.targetProgram = ConverterProgramNames.ConvertFromFile(.target_program)
                p_modelControl.tests = ConverterTests.ConvertFromFile(.tests)
                p_modelControl.tickets = ConverterTickets.ConvertFromFile(.tickets)
                p_modelControl.title = .title
                ConverterUpdates.ConvertFromFile(.updates, p_modelControl)
            End With

        Catch ex As Exception
            OnSharedLogger(New LoggerEventArgs(ex))
        End Try
    End Sub


    ''' <summary>
    ''' Writes the provided object out to an XML file.
    ''' </summary>
    ''' <param name="p_modelControl">Object to write out.</param>
    ''' <param name="p_modelControlXml">Serialized file originally read in. 
    ''' This will maintain data that was not read into program classes.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Write(ByVal p_modelControl As cMCModel,
                            ByRef p_modelControlXml As xmlModelControl)
        Try
            If p_modelControl Is Nothing Then Exit Sub

            Dim filePath As String = GetWriteFilePath(p_modelControl.mcFile)
            If String.IsNullOrEmpty(filePath) Then Exit Sub

            Dim myModelControl As xmlModelControl = If(p_modelControlXml, New xmlModelControl())

            'Transfer data to model data class
            With myModelControl
                .attachments = ConverterAttachments.ConvertToFile(p_modelControl.attachments)

                With .author
                    .company = p_modelControl.author.company
                    .name = p_modelControl.author.name
                End With

                With .classification.value
                    .level_1 = ConverterClassificationLevel1.ConvertToFile(p_modelControl.classification.level1)
                    .level_2 = p_modelControl.classification.level2
                End With

                .command_line = p_modelControl.commandLine
                .comments = p_modelControl.comments

                With .date
                    .day = CStr(p_modelControl.exampleDate.numDay)
                    .month = CStr(p_modelControl.exampleDate.numMonth)
                    .year = CStr(p_modelControl.exampleDate.numYear)
                End With

                If IsWriteableTableFileName(p_modelControl) Then
                    .database_file_name = p_modelControl.dataSource.pathDestination.fileNameWithExtension
                Else
                    .database_file_name = ""
                End If

                .description = p_modelControl.description
                .documentation_status = ConverterDocumentationStatus.ConvertToFile(p_modelControl.statusDocumentation)
                '.documentation_statusSpecified

                .excel_results.excel_file.path = p_modelControl.resultsExcel.filePath

                .id = p_modelControl.ID.idCompositeDecimal
                .id_secondary = p_modelControl.secondaryID
                .images = ConverterImages.ConvertToFile(p_modelControl.images)
                .incidents = ConverterIncidents.ConvertToFile(p_modelControl.incidents)
                .is_bug = ConvertTrueYesNoUnknownString(p_modelControl.isBug).ToLower
                '.is_bugSpecified
                .is_public = ConvertTrueYesNoUnknownString(p_modelControl.isPublic).ToLower

                .keywords = ConverterKeywords.ConvertToFile(p_modelControl.keywords)

                .links = ConverterLinks.ConvertToFile(p_modelControl.links)

                .path.Value = p_modelControl.mcFile.pathDestination.path
                '.path.type
                '.path.typeSpecified

                .postprocessed_results = ConverterResultsPostProcessed.ConvertToFile(p_modelControl.results)

                With .program
                    .name = ConverterProgramNames.ConvertToFile(p_modelControl.program.programName)
                    .version = p_modelControl.program.programVersion
                    .version_for_last_best_value = p_modelControl.program.programVersionLastBest
                End With

                With .regtest_internal_use
                    .excel_results = ConverterResultsExcel.ConvertToFile(p_modelControl.results)
                End With

                .results = ConverterResults.ConvertToFile(p_modelControl.results)
                .run_time.minutes = myCDec(CStr(p_modelControl.runTime))

                '.SchemaLocation
                .status = ConverterExampleStatus.ConvertToFile(p_modelControl.statusExample)
                '.statusSpecified

                .target_program = ConverterProgramNames.ConvertToFile(p_modelControl.targetProgram)
                .tests = ConverterTests.ConvertToFile(p_modelControl.tests)
                .tickets = ConverterTickets.ConvertToFile(p_modelControl.tickets)
                .title = p_modelControl.title
                .updates = ConverterUpdates.ConvertToFile(p_modelControl.updates)
            End With
        Catch ex As Exception
            OnSharedLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Basic test method.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub Test()
        Dim directory As String = System.AppDomain.CurrentDomain.BaseDirectory
        Dim fileName As String = "0160_MC"
        Dim pathRead As String = directory & "\" & fileName & ".xml"
        Dim pathWrite As String = directory & "\" & fileName & "_Change.xml"

        ' Get model data.
        Dim myModelControl As xmlModelControl = GetObjectFromXML(Of xmlModelControl)(pathRead)

        ' Change model data for a test.
        myModelControl.id = 1

        'myModelControl.tests(0) = testsTest.updatebridge

        'Adding Object lists: Do as for arrays. Assigning to an existing index overwrites the value.
        Dim attachment As New attachmentsAttachment With {.title = "New Title", .path = "New Path"}
        ReDim Preserve myModelControl.attachments(myModelControl.attachments.Length + 1)
        myModelControl.attachments(myModelControl.attachments.Length - 1) = attachment

        Dim attachment2 As New attachmentsAttachment With {.title = "New Title2", .path = "New Path2"}
        ReDim Preserve myModelControl.attachments(myModelControl.attachments.Length + 1)
        myModelControl.attachments(myModelControl.attachments.Length - 1) = attachment2

        ' Write model data back out.
        '   Use this to suppress wanted attribute assignments in the file
        WriteObjectToXMLNoNameSpace(pathWrite, myModelControl)

        '   Basic: xsd:type attributes end up appearing on added objects
        'WriteObjectToTestXML(pathWrite, myModelControl)

    End Sub

#Region "Methods - Private"
    ''' <summary>
    ''' True: The table file name exists and is not a default.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsWriteableTableFileName(ByVal p_mcModel As cMCModel) As Boolean
        Return (Not String.IsNullOrEmpty(p_mcModel.dataSource.pathDestination.fileNameWithExtension) AndAlso
                 Not p_mcModel.dataSource.PathExportedTable.isPathDefault)
    End Function
#End Region
End Class
