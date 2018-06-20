Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Reporting

''' <summary>
''' Contains a list of Example IDs that are associated with objects that contain syncing instructions.
''' </summary>
''' <remarks></remarks>
Friend Class cExamplePropsSync
    Inherits System.Collections.DictionaryBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties - List"
    ''' <summary>
    ''' Gets the element at the specified key.
    ''' </summary>
    ''' <param name="p_key">The Example ID.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_key As Integer) As cMCPropsSync
        Get
            Return GetItem(p_key)
        End Get
    End Property
    ''' <summary>
    ''' Gets the element at the specified key.
    ''' If the key does not yet exist, the provided key may be inserted with a new blank value created.
    ''' </summary>
    ''' <param name="p_key">Example ID.</param>
    ''' <param name="p_createNewEntryIfNotExist">True: If the key is not found, it is added and a new blank value is created.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_key As Integer,
                                                    ByVal p_createNewEntryIfNotExist As Boolean) As cMCPropsSync
        Get
            If (p_createNewEntryIfNotExist AndAlso Not Contains(p_key)) Then Add(p_key, New cMCPropsSync)
            Return GetItem(p_key)
        End Get
    End Property
    Public ReadOnly Property Keys() As ICollection
        Get
            Return Dictionary.Keys
        End Get
    End Property

    Public ReadOnly Property Values() As ICollection
        Get
            Return Dictionary.Values
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
#End Region

#Region "Overrides/Implements"
    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cExamplePropsSync

        With myClone
            For Each item As Integer In Dictionary.Keys
                .Add(item, DirectCast(DirectCast(Dictionary(item), cMCPropsSync).Clone, cMCPropsSync))
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cExamplePropsSync) Then Return False

        Dim itemsOuter As cExamplePropsSync = TryCast(p_object, cExamplePropsSync)
        If itemsOuter Is Nothing Then Return False

        'Check for any differences
        For Each keyOuter As Integer In itemsOuter.Keys
            If Not (Contains(keyOuter) AndAlso
                    itemsOuter(keyOuter).Equals(GetItem(keyOuter))) Then
                Return False
            End If
        Next

        Return True
    End Function
#End Region

#Region "Methods: Friend - List"
    ''' <summary>
    ''' Adds every element of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As IDictionary(Of Integer, cMCPropsSync))
        If p_items Is Nothing Then Exit Sub

        For Each key As Integer In p_items.Keys
            Add(key, p_items(key))
        Next
    End Sub
    ''' <summary>
    ''' Adds an item to the list if it is unique.
    ''' </summary>
    ''' <param name="p_key">The Example ID.</param>
    ''' <param name="p_value"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_key As Integer,
                                  ByVal p_value As cMCPropsSync) As Boolean
        Try
            If p_value Is Nothing Then Return False

            'If key is unique, add it
            If Not Contains(p_key) Then
                Dictionary.Add(p_key, p_value)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))

                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Removes the item at the specified key.
    ''' </summary>
    ''' <param name="p_key">The Example ID.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_key As Integer)
        Try
            If p_key < 0 Then Throw New ArgumentException("Key {1} cannot be a negative number.", p_key.ToString)
            If Not Contains(p_key) Then Throw New ArgumentException("Dictionary does not contain the key: {1} ", p_key.ToString)

            Dictionary.Remove(p_key)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' True: The dictionary contains the provided Example ID.
    ''' </summary>
    ''' <param name="p_key">The Example ID.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Contains(ByVal p_key As Integer) As Boolean
        Return Dictionary.Contains(p_key)
    End Function

    ''' <summary>
    ''' Returns a copy of the dictionary list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToDictionary() As IDictionary(Of Decimal, cMCPropsSync)
        Dim templist As New Dictionary(Of Decimal, cMCPropsSync)

        For Each key As Integer In Dictionary.Keys
            templist.Add(key, DirectCast(Dictionary(key), cMCPropsSync))
        Next

        Return templist
    End Function
#End Region

#Region "Methods: Private - List"
    ''' <summary>
    ''' Returns the item specified by key.
    ''' </summary>
    ''' <param name="p_key">The Example ID.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_key As Integer) As cMCPropsSync
        Try
            If p_key < 0 Then Throw New ArgumentException("Key {1} cannot be a negative number.", p_key.ToString)
            If Not Contains(p_key) Then Throw New ArgumentException("Dictionary does not contain the key: {1} ", p_key.ToString)

            Return CType(Dictionary(p_key), cMCPropsSync)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function
#End Region
End Class
