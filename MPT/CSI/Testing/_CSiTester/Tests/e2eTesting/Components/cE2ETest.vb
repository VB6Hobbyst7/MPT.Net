Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' Class that contains internal program instructions to running tests, such as calling a sequence of test functions in various forms, and assigning the XML-specified instructions.
''' </summary>
''' <remarks></remarks>
Public Class cE2eTest
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    ''' <summary>
    ''' Name of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
    ''' <summary>
    ''' Description of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String

    Private _selected As Boolean
    ''' <summary>
    ''' If true, the example has been selected to be run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property selected As Boolean
        Set(ByVal value As Boolean)
            If Not _selected = value Then
                _selected = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("selected"))
            End If
        End Set
        Get
            Return _selected
        End Get
    End Property

    ''' <summary>
    ''' Components of the test, with each entry contiaining items such as the name of the function, name of the form where the function is located, and which test instructions to use.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property components As ObservableCollection(Of cE2ETestComponent)
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()

    End Sub

    Private Sub InitializeData()
        components = New ObservableCollection(Of cE2ETestComponent)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Adds a new component entry to the highest level list of components in the cE2ETest class.
    ''' </summary>
    ''' <param name="formName">Name of the form where the function is located.</param>
    ''' <param name="functionName">Name of the function to be used in the test.</param>
    ''' <param name="testID">Test ID of the instructions to be used in the function.</param>
    ''' <param name="component">Component child to be added to the new component, which is composed of it's own form name, function name, test id, and component children.</param>
    ''' <remarks></remarks>
    Friend Sub AddComponentNew(ByVal formName As String, ByVal functionName As String, Optional ByVal testID As String = "", Optional ByVal component As cE2ETestComponent = Nothing)
        AddComponentAdditional(components, formName, functionName, testID, component)

    End Sub

    ''' <summary>
    ''' Adds a new component to a specified list of components. This can be used to add a component as a child of another component.
    ''' </summary>
    ''' <param name="componentsList">List of child components to which the new component is to be added.</param>
    ''' <param name="formName">Name of the form where the function is located.</param>
    ''' <param name="functionName">Name of the function to be used in the test.</param>
    ''' <param name="testID">Test ID of the instructions to be used in the function.</param>
    ''' <param name="childComponent">Component child to be added to the new component, which is composed of it's own form name, function name, test id, and component children.</param>
    ''' <remarks></remarks>
    Friend Sub AddComponentAdditional(ByVal componentsList As ObservableCollection(Of cE2ETestComponent), ByVal formName As String, ByVal functionName As String, Optional ByVal testID As String = "", Optional ByVal childComponent As cE2ETestComponent = Nothing)
        Dim tempComponent As New cE2ETestComponent(formName, functionName, testID, childComponent)

        componentsList.Add(tempComponent)

    End Sub

    ''' <summary>
    ''' Adds a new sub-component to the component provided.
    ''' </summary>
    ''' <param name="component">Components to which the sub-component is to be added.</param>
    ''' <param name="functionName">Name of the function to be used in the test.</param>
    ''' <param name="testID">Test ID of the instructions to be used in the function.</param>
    ''' <param name="childComponent">Component child to be added to the sub-component, which is composed of it's own form name, function name, test id, and component children.</param>
    ''' <remarks></remarks>
    Friend Sub AddComponentSub(ByVal component As cE2ETestComponent, ByVal functionName As String, Optional ByVal testID As String = "", Optional ByVal childComponent As cE2ETestComponent = Nothing)
        Dim tempComponentSub As New cE2ETestComponentSub(functionName, testID, childComponent)

        component.AddSubComponent(functionName, testID, component)

    End Sub
#End Region


End Class
