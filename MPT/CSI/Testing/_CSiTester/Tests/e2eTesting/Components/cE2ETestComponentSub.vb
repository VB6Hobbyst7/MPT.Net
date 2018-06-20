Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

''' <summary>
''' Class that contains the test function entry and specifications for a given form.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETestComponentSub
#Region "Properties"
    ''' <summary>
    ''' Name of the function within the form to use for part of the overall test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property functionName As String
    ''' <summary>
    ''' ID of the test instructions to use in the function.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testID As String
    ''' <summary>
    ''' Components of the sub-component, with each entry contiaining items such as the name of the function, name of the form where the function is located, and which test instructions to use. 
    ''' Sub-components are used for calling test functions that are located within forms that are accessed from other forms.
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

    Friend Sub New(ByVal pFunctionName As String, Optional ByVal pTestID As String = "", Optional ByVal pComponent As cE2ETestComponent = Nothing)
        InitializeData()

        'Set Properties
        functionName = pFunctionName
        testID = pTestID
        components.Add(pComponent)
    End Sub

    Private Sub InitializeData()
        components = New ObservableCollection(Of cE2ETestComponent)
    End Sub
#End Region

#Region "Methods"

#End Region
End Class
