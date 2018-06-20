' Note: The base class generated has a namespace conflict with the regTest-related classes.
' When a new model control base class is brought in, it should be placed in the 'ModelControl' namespace.

''' <summary>
''' Wraps the base class representation of a model control xml file and provides an additional root node attribute of the schema location.
''' </summary>
''' <remarks></remarks>
<System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.csiberkeley.com"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://www.csiberkeley.com", ElementName:="model", IsNullable:=False)> _
Public Class xmlModelControl
    Inherits ModelControl.model

    <System.Xml.Serialization.XmlAttribute("schemaLocation", [Namespace]:="http://www.w3.org/2001/XMLSchema-instance")>
    Public Property SchemaLocation As String
        Get
            Return "http://www.csiberkeley.com http://www.csiamerica.com/sites/default/files/schemas/model_database_model_V0_4.xsd"
        End Get
        Set(value As String)
            'Ignore... pureley needed for serialization.
        End Set
    End Property
End Class
