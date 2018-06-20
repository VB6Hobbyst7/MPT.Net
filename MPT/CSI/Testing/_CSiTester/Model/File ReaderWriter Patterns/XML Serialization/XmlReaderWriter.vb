Option Explicit On
Option Strict On

Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml

''' <summary>
''' Reads and writes XML files.
''' </summary>
''' <remarks></remarks>
Public Class XmlReaderWriter
    ''' <summary>
    ''' Returns the desired class type read from an XML file at the provided path.
    ''' Note: XML comments will cause this to throw an exception.
    ''' </summary>
    ''' <typeparam name="T">Class object type to return.</typeparam>
    ''' <param name="p_path">Path to an existing XMl file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetObjectFromXML(Of T)(ByVal p_path As String) As T
        Try
            Dim serializer = New XmlSerializer(GetType(T))
            Using sr As StreamReader = New StreamReader(p_path)
                Return CType(serializer.Deserialize(sr), T)
            End Using
        Catch ex As InvalidOperationException
            Return GetObjectFromXMLWithComments(Of T)(p_path)
        End Try
    End Function

    ''' <summary>
    ''' Returns the desired class type read from an XML file at the provided path.
    ''' Handles XML files with comments by stripping out the comment objects.
    ''' Comments will be lost in any written files using this method.
    ''' </summary>
    ''' <typeparam name="T">Class object type to return.</typeparam>
    ''' <param name="p_path">Path to an existing XMl file.</param>
    ''' <returns></returns>
    ''' <remarks>From: http://stackoverflow.com/questions/4919280/troubles-wtih-comments-in-xmlserialzier</remarks>
    Public Shared Function GetObjectFromXMLWithComments(Of T)(ByVal p_path As String) As T
        'Load Document
        Dim xmlDoc As XmlDocument = New XmlDocument()
        xmlDoc.Load(p_path)

        ' Remove all comments
        Dim commentNodes As XmlNodeList = xmlDoc.SelectNodes("//comment()")
        For Each commentNode As XmlNode In commentNodes
            commentNode.ParentNode.RemoveChild(commentNode)
        Next

        ' Store to memory stream and rewind
        Using ms As New MemoryStream()
            xmlDoc.Save(ms)
            ms.Seek(0, SeekOrigin.Begin)

            ' Deserialize using clean XML
            Dim serializer = New XmlSerializer(GetType(T))
            Using reader As XmlReader = XmlReader.Create(ms)
                Return CType(serializer.Deserialize(reader), T)
            End Using
        End Using
    End Function

    ''' <summary>
    ''' Serializes the provided object to an XML file at the specified path.
    ''' Note: With this method, in some elements an XSI namespace attribute is written.
    ''' </summary>
    ''' <typeparam name="T">Class object type to write.</typeparam>
    ''' <param name="p_path">Path to write to.</param>
    ''' <param name="p_model">Object to write out.</param>
    ''' <remarks></remarks>
    Public Shared Sub WriteObjectToXML(Of T)(ByVal p_path As String,
                                                 ByVal p_model As T)
        Dim serializer = New XmlSerializer(GetType(T))
        serializer.Serialize(New StreamWriter(p_path), p_model)
    End Sub

    ''' <summary>
    ''' Serializes the provided object to an XML file at the specified path.
    ''' This method suppresses some unwanted XSI namespace attributes from being written in the file.
    ''' </summary>
    ''' <typeparam name="T">Class object type to write.</typeparam>
    ''' <param name="p_path">Path to write to.</param>
    ''' <param name="p_model">Object to write out.</param>
    ''' <remarks></remarks>
    Public Shared Sub WriteObjectToXMLNoNameSpace(Of T)(ByVal p_path As String,
                                                            ByVal p_model As T)

        Dim xmlTextWriter As New NonXsiTextWriter(New StreamWriter(p_path))
        xmlTextWriter.Formatting = Formatting.Indented

        Dim serializer = New XmlSerializer(GetType(T))
        serializer.Serialize(xmlTextWriter, p_model)
    End Sub
End Class
