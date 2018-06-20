Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Reporting

' See: http://stackoverflow.com/questions/3187444/convert-xml-string-to-object#
' See: http://stackoverflow.com/questions/226599/deserializing-xml-to-objects-in-c-sharp
''' <summary>
''' Reads and writes the outputSettings XML file to/from the program's classes.
''' </summary>
''' <remarks></remarks>
Friend Class AdapterOutputSettings
    Inherits AdapterXmlFile

    ''' <summary>
    ''' Fills the provided object with data from the correspodning XML file.
    ''' </summary>
    ''' <param name="p_outputSettings">Object to fill.</param>
    ''' <param name="p_outputSettingsXml">Deserialized XML file to use to fill the object.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Fill(ByRef p_outputSettings As cFileOutputSettings,
                           ByVal p_outputSettingsXml As xmlOutputSettings)
        Try
            If (p_outputSettings Is Nothing OrElse
                p_outputSettingsXml Is Nothing) Then Exit Sub

            'Transfer data to object
            With p_outputSettingsXml
                p_outputSettings.tableFileName = .filename
                p_outputSettings.saveOS = .save
                '.saveSpecified
                '.SchemaLocation
                p_outputSettings.v9Units = .v9units

                With .tableSet
                    p_outputSettings.tableSetName = .name
                    p_outputSettings.tables = ConverterEnumsTables.ConvertFromFile(.tables)

                    With .options
                        p_outputSettings.combineCaseStepFields = .combineCaseStepFields

                        ' Currently these are limited to 1 item in the list according to the xml schema
                        p_outputSettings.groups.Add(.groups.group)
                        p_outputSettings.loadCases.Add(.loadCases.loadCase)
                        p_outputSettings.loadCombinations.Add(.loadCombinations.loadCombination)
                        p_outputSettings.loadPatterns.Add(.loadPatterns.loadPattern)

                        p_outputSettings.multiStep = ConverterEnumMultiStep.ConvertFromFile(.multiStep)
                        p_outputSettings.showAllFields = .showAllFields
                        p_outputSettings.showOnlyIfUsedInModel = .showOnlyIfUsedInModel
                        p_outputSettings.showsSelectionOnly = .showSelectionOnly
                    End With

                    With .options.units
                        p_outputSettings.forceUnit = ConverterEnumUnits.ConvertFromFile(.forceUnit)
                        p_outputSettings.lengthUnit = ConverterEnumUnits.ConvertFromFile(.lengthUnit)
                        p_outputSettings.temperatureUnit = ConverterEnumUnits.ConvertFromFile(.temperatureUnit)
                    End With
                End With
            End With
        Catch ex As Exception
            OnSharedLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Writes the provided object out to an XML file.
    ''' </summary>
    ''' <param name="p_outputSettings">Object to write out.</param>
    ''' <param name="p_outputSettingsXml">Serialized file originally read in. 
    ''' This will maintain data that was not read into program classes.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Write(ByVal p_outputSettings As cFileOutputSettings,
                            ByRef p_outputSettingsXml As xmlOutputSettings)
        Try
            If p_outputSettings Is Nothing Then Exit Sub

            Dim filePath As String = GetWriteFilePath(p_outputSettings)
            If String.IsNullOrEmpty(filePath) Then Exit Sub

            Dim myOutputSettings As xmlOutputSettings = If(p_outputSettingsXml, New xmlOutputSettings())

            'Transfer data to model data class
            With myOutputSettings
                .filename = p_outputSettings.tableFileName
                .save = p_outputSettings.saveOS
                '.saveSpecified
                '.SchemaLocation
                .v9units = p_outputSettings.v9Units

                With .tableSet
                    .name = p_outputSettings.tableSetName
                    .tables = ConverterEnumsTables.ConvertToFile(p_outputSettings.tables)

                    With .options
                        .combineCaseStepFields = p_outputSettings.combineCaseStepFields

                        ' Currently these are limited to 1 item in the list according to the xml schema
                        .groups = New tabularOutputTableSetOptionsGroups With {.group = p_outputSettings.groups(0)}
                        .loadCases = New tabularOutputTableSetOptionsLoadCases With {.loadCase = p_outputSettings.loadCases(0)}
                        .loadCombinations = New tabularOutputTableSetOptionsLoadCombinations With {.loadCombination = p_outputSettings.loadCombinations(0)}
                        .loadPatterns = New tabularOutputTableSetOptionsLoadPatterns With {.loadPattern = p_outputSettings.loadPatterns(0)}

                        .multiStep = ConverterEnumMultiStep.ConvertToFile(p_outputSettings.multiStep)
                        .showAllFields = p_outputSettings.showAllFields
                        .showOnlyIfUsedInModel = p_outputSettings.showOnlyIfUsedInModel
                        .showSelectionOnly = p_outputSettings.showsSelectionOnly
                    End With

                    With .options.units
                        .forceUnit = ConverterEnumUnits.ConvertToFile(p_outputSettings.forceUnit)
                        .lengthUnit = ConverterEnumUnits.ConvertToFile(p_outputSettings.lengthUnit)
                        .temperatureUnit = ConverterEnumUnits.ConvertToFile(p_outputSettings.temperatureUnit)
                    End With
                End With
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
        Dim fileName As String = "outputSettings"
        Dim pathRead As String = directory & "\" & fileName & ".xml"
        Dim pathWrite As String = directory & "\" & fileName & "_Change.xml"

        ' Get model data.
        Dim myOutputSettings As xmlOutputSettings = GetObjectFromXML(Of xmlOutputSettings)(pathRead)

        ' Change model data for a test.
        myOutputSettings.filename = "NonExistingFile.Void"
        myOutputSettings.tableSet.options.units.forceUnit = tabularOutputTableSetOptionsUnitsForceUnit.Item

        myOutputSettings.save = True

        ' Write model data back out.
        '   Use this to suppress wanted attribute assignments in the file
        WriteObjectToXMLNoNameSpace(pathWrite, myOutputSettings)

        '   Basic: xsd:type attributes end up appearing on added objects
        'WriteObjectToTestXML(pathWrite, myOutputSettings)
    End Sub
End Class
