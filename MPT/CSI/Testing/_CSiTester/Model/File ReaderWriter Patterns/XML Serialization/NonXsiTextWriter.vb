Option Explicit On
Option Strict On

Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml.Schema

''' <summary>
''' Affects the writing of XML files by suppressing certain attributes based on various filters..
''' </summary>
''' <remarks>Based on: http://weblogs.asp.net/cazzu/62141 </remarks>
Public Class NonXsiTextWriter
    Inherits XmlTextWriter

    Public Sub New(ByVal w As TextWriter)
        MyBase.New(w)
    End Sub

    Public Sub New(ByVal w As Stream, ByVal encoding As Encoding)
        MyBase.New(w, encoding)
    End Sub

    Public Sub New(ByVal filename As String, ByVal encoding As Encoding)
        MyBase.New(filename, encoding)
    End Sub

    Dim _skip As Boolean = False

    Public Overrides Sub WriteStartAttribute(ByVal prefix As String, ByVal localName As String, ByVal ns As String)
        If ((ns = XmlSchema.InstanceNamespace AndAlso localName = "type") OrElse
           (prefix = "xmlns" AndAlso Not localName = "xsi")) Then

            _skip = True
            Return
        End If
        MyBase.WriteStartAttribute(prefix, localName, ns)
    End Sub

    Public Overrides Sub WriteString(ByVal text As String)
        If _skip Then Return
        MyBase.WriteString(text)
    End Sub

    Public Overrides Sub WriteEndAttribute()
        If _skip Then
            _skip = False
            Return
        End If
        MyBase.WriteEndAttribute()
    End Sub
End Class

<XmlInclude(GetType(ProvisionTextField))>
<XmlInclude(GetType(ProvisionCDataField))>
Public MustInherit Class ProvisionDataField

    <XmlAttribute("datatype")>
    Public Property DataType As String

    <XmlElement("name")>
    Public Property Name As String

End Class

Public Class ProvisionCDataField
    Inherits ProvisionDataField

    <XmlIgnore()>
    Public Property ValueContent As String

    <XmlElement("value")>
    Public Property Value() As XmlCDataSection
        Get
            Dim doc As New XmlDocument
            Return doc.CreateCDataSection(ValueContent)
        End Get
        Set(ByVal value As XmlCDataSection)
            ValueContent = value.Value
        End Set
    End Property
End Class

Public Class ProvisionTextField
    Inherits ProvisionDataField

    <XmlElement("value")>
    Public Property Value As String
End Class
