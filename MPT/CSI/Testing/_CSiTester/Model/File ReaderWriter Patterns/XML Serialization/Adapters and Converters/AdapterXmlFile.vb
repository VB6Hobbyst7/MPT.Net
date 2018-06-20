Option Explicit On
Option Strict On

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Reporting
Imports MPT.XML.Serialization

' See: http://stackoverflow.com/questions/3187444/convert-xml-string-to-object#
' See: http://stackoverflow.com/questions/226599/deserializing-xml-to-objects-in-c-sharp
Friend MustInherit Class AdapterXmlFile
    Inherits XmlReaderWriter
    Shared Event SharedLog(exception As LoggerEventArgs)

    ''' <summary>
    ''' Returns a deserialized XML object from a file at the path provided.
    ''' </summary>
    ''' <param name="p_path">Path to an existing XML file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Deserialize(Of T)(ByVal p_path As String) As T
        Try
            If (String.IsNullOrEmpty(p_path) OrElse
                Not IO.File.Exists(p_path)) Then Return Nothing

            ' Get model data.
            Return GetObjectFromXML(Of T)(p_path)
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Serializes the XML object out to a file.
    ''' </summary>
    ''' <param name="p_fileXml">XML object to serialize.</param>
    ''' <param name="p_path">Path to the file to overwrite or create.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Serialize(Of T)(ByVal p_fileXml As T,
                                      ByVal p_path As String)
        Try
            If (p_fileXml Is Nothing OrElse
                String.IsNullOrEmpty(p_path)) Then Exit Sub

            ' Write model data back out.
            '   Use this to suppress wanted attribute assignments in the file
            WriteObjectToXMLNoNameSpace(p_path, p_fileXml)

            '   Basic: xsd:type attributes end up appearing on added objects
            'WriteObjectToTestXML(pathWrite, myModelControl)
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Gets the file path to read from.
    ''' </summary>
    ''' <param name="p_object"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetReadFilePath(ByVal p_object As cMCFile) As String
        If (p_object Is Nothing OrElse
            p_object.pathDestination Is Nothing) Then Return ""

        Dim filePath As String = ""
        If IO.File.Exists(p_object.pathDestination.path) Then
            Return p_object.pathDestination.path
        ElseIf IO.File.Exists(p_object.pathSource.path) Then
            Return p_object.pathSource.path
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Gets the file path to write to.
    ''' </summary>
    ''' <param name="p_object"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetWriteFilePath(ByVal p_object As cMCFile) As String
        If (p_object Is Nothing OrElse
            p_object.pathDestination Is Nothing) Then Return ""

        Return p_object.pathDestination.path
    End Function

    Protected Shared Sub OnSharedLogger(e As LoggerEventArgs)
        RaiseEvent SharedLog(e)
    End Sub
End Class
