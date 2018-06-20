''' <summary>
''' Wraps the base class representation of a regTest xml file and provides an additional root node attribute of the schema location.
''' </summary>
''' <remarks></remarks>
<System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.csiamerica.com"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.csiamerica.com", ElementName:="regtest", IsNullable:=False)> _
Public Class xmlRegTest
    Inherits RegTest.regtest

    <System.Xml.Serialization.XmlAttribute("schemaLocation", [Namespace]:="http://www.w3.org/2001/XMLSchema-instance")>
    Public Property SchemaLocation As String
        Get
            Return "http://www.csiamerica.com http://www.csiamerica.com/sites/default/files/schemas/regtest_configuration.xsd"
        End Get
        Set(value As String)
            'Ignore... pureley needed for serialization.
        End Set
    End Property
End Class
