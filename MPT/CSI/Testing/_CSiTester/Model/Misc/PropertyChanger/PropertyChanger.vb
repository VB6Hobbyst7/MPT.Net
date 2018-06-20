Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Linq.Expressions
Imports System.Collections.Generic

''' <summary>
''' Handles property changing and changed events using INotify interfaces.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class PropertyChanger
    Implements INotifyPropertyChanged
    Implements INotifyPropertyChanging

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event PropertyChanging As PropertyChangingEventHandler Implements INotifyPropertyChanging.PropertyChanging

#Region "Methods: Protected - Property Changing"
    Protected Sub RaisePropertyChanging(sender As Object, e As PropertyChangingEventArgs)
        RaiseEvent PropertyChanging(Me, e)
    End Sub
    Protected Sub RaisePropertyChanging(p_propertyName As String)
        If Not String.IsNullOrEmpty(p_propertyName) Then
            RaisePropertyChanging(Me, New PropertyChangingEventArgs(p_propertyName))
        End If
    End Sub
    ''' <summary>
    ''' Raises the property changing event using the actual property to determine the property name.
    ''' Call this by: NameOfProp(Function() Me.{property})
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="p_propertyName">Function() Me.{property}</param>
    ''' <remarks></remarks>
    Protected Sub RaisePropertyChanging(Of T)(p_propertyName As Expression(Of Func(Of T)))
        Dim currentPropertyName As String = p_propertyName.ToString.Split("."c).Last()

        If Not String.IsNullOrEmpty(currentPropertyName) Then
            RaisePropertyChanging(Me, New PropertyChangingEventArgs(currentPropertyName))
        End If
    End Sub
#End Region

#Region "Methods: Protected - Property Changed"
    Protected Sub RaisePropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub
    Protected Sub RaisePropertyChanged(p_propertyName As String)
        If Not String.IsNullOrEmpty(p_propertyName) Then
            RaisePropertyChanged(Me, New PropertyChangedEventArgs(p_propertyName))
        End If
    End Sub
    ''' <summary>
    ''' Raises the property changed event using the actual property to determine the property name.
    ''' Call this by: NameOfProp(Function() Me.{property})
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="p_propertyName">Function() Me.{property}</param>
    ''' <remarks></remarks>
    Protected Sub RaisePropertyChanged(Of T)(p_propertyName As Expression(Of Func(Of T)))
        Dim currentPropertyName As String = p_propertyName.ToString.Split("."c).Last()

        If Not String.IsNullOrEmpty(currentPropertyName) Then
            RaisePropertyChanged(Me, New PropertyChangedEventArgs(currentPropertyName))
        End If
    End Sub


    Protected Overridable Function [Set](Of T)(ByVal propertyName As String,
                                               ByRef field As T,
                                               ByVal value As T) As Boolean
        If EqualityComparer(Of T).[Default].Equals(field, value) Then Return False

        field = value
        RaisePropertyChanged(propertyName)
        Return True
    End Function
    Protected Overridable Function [Set](Of T)(ByVal propertyExpression As Expression(Of Func(Of T)),
                                               ByRef field As T,
                                               ByVal value As T) As Boolean
        If EqualityComparer(Of T).[Default].Equals(field, value) Then Return False

        field = value
        RaisePropertyChanged(propertyExpression)
        Return True
    End Function
    Protected Overridable Function [Set](Of T)(ByVal field As Field(Of T),
                                               ByVal value As T) As Boolean
        If (field Is Nothing OrElse
           EqualityComparer(Of T).[Default].Equals(field, value)) Then Return False

        field.value = value
        RaisePropertyChanged(field.propertyName)
        Return True
    End Function

#End Region

#Region "Methods: Shared Friend"
    ''' <summary>
    ''' Returns the name of the property supplied.
    ''' If there are a chain of properties, only the last one is returned.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="p_propertyName">Function() {object}.{property}</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function NameOfProp(Of T)(p_propertyName As Expression(Of Func(Of T))) As String
        Dim exp As MemberExpression = TryCast(p_propertyName.Body, MemberExpression)

        If exp IsNot Nothing Then
            Return exp.Member.Name
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Class - Protected"
    ''' <summary>
    ''' Backing field that allows storing of the property name as a string.
    ''' This is done to avoid repeatedly calling the lambda expression for determining the name.
    ''' Using this method allows for refactoring support of property names, but avoids most of the time penalty of using the lambda expressions.
    ''' Performance hit seems to not really accrue until used over 10^5 times, and then it is comparable to the better straight lambda cases.
    ''' See implementation examples following (commented out so they are not active).
    ''' Obtained from: http:\\blog.amusedia.com/2013/06/inotifypropertychanged-implementation.html  
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <remarks></remarks>
    Protected Class Field(Of T)
        ''' <summary>
        ''' Stores the property name of as a string.
        ''' </summary>
        ''' <remarks></remarks>
        Public propertyName As String
        ''' <summary>
        ''' Value of the property.
        ''' </summary>
        ''' <remarks></remarks>
        Public value As T

        ''' <summary>
        ''' Initializes the field object by sending it the property name to store.
        ''' </summary>
        ''' <param name="p_propertyName">Property name as a string.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal p_propertyName As String)
            Me.propertyName = p_propertyName
        End Sub
        ''' <summary>
        ''' Initializes the field object by sending it the property to store as a name. This is determined within the class..
        ''' </summary>
        ''' <param name="p_propertyName">Property name as a lambda expression.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal p_propertyName As Expression(Of Func(Of T)))
            Me.propertyName = NameOfProp(p_propertyName)
        End Sub

        ''' <summary>
        ''' Implicitly converts the value.
        ''' </summary>
        ''' <param name="t"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Widening Operator CType(t As Field(Of T)) As T
            Return t.value
        End Operator
    End Class

    '' The string name of the property is set during initialization of the property.
    '' This property is set as ReadOnly so that the string name is only set once, during initialization of the Field object.
    'Private ReadOnly _testMCModel As New Field(Of cMCModel)(Function() Me.mcModel)
    'Public Property mcModel() As cMCModel
    '    Get
    '        Return _testMCModel
    '    End Get
    '    Set(ByVal value As cMCModel)
    '        [Set](_testMCModel, value)
    '    End Set
    'End Property

    'Sub TestChangeProperty()
    '    Dim newTestModel As New cMCModel

    '    'The only change in setting private values directly is that they must have the additional ".value"
    '    _testMCModel.value = newTestModel

    'End Sub
#End Region

End Class
