Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

''' <summary>
''' Class that contains the specified form, and its test function entries and specifications to be used.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETestComponent
#Region "Properties"
    ''' <summary>
    ''' Name of the form where the test functions to be used are located.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property formName As String

    ''' <summary>
    ''' List of objects as a sequence that each contain the test function to be called, the test ID instructions to use, and any sub-forms that should be called for testing.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property componentSubs As ObservableCollection(Of cE2ETestComponentSub)
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()

    End Sub

    Friend Sub New(ByVal pFormName As String, ByVal pFunctionName As String, Optional ByVal pTestID As String = "", Optional ByVal pComponent As cE2ETestComponent = Nothing)
        InitializeData()

        formName = pFormName
        AddSubComponent(pFunctionName, pTestID, pComponent)
    End Sub

    Private Sub InitializeData()
        componentSubs = New ObservableCollection(Of cE2ETestComponentSub)
    End Sub
#End Region

#Region "Methods"

    Friend Sub AddSubComponent(ByVal functionName As String, Optional ByVal testID As String = "", Optional ByVal component As cE2ETestComponent = Nothing)
        Dim tempComponentSub As New cE2ETestComponentSub(functionName, testID, component)

        componentSubs.Add(tempComponentSub)

    End Sub

#End Region
End Class
