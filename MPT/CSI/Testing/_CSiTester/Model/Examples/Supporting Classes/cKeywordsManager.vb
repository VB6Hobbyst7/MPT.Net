Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' Generates and stores a list of keywords to associate with a model control XML file. Includes generating keywords of various types preceded by the appropriate tags.
''' </summary>
''' <remarks></remarks>
Public Class cKeywordsManager
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Variables"
    Private _keywordEntry As New cKeyword
#End Region

#Region "Properties"
    '=== For various official keywords listed in the settings file
    ''' <summary>
    ''' Prefix to apply to all entries of the keyword group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property prefix As String
    ''' <summary>
    ''' Name of the keywords group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
    ''' <summary>
    ''' Description of the keyword group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String
    ''' <summary>
    ''' Collection of keywords contained within this group.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property keywords As ObservableCollection(Of cKeyword)

    'TODO: Consider removing or splitting into a different class?
    '=== For adding/removing keywords in the editor
    Private _keywordExisting As String
    Public Property keywordExisting As String
        Set(ByVal value As String)
            If Not _keywordAdd = value Then
                _keywordExisting = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordExisting"))
            End If
        End Set
        Get
            Return _keywordExisting
        End Get
    End Property

    Private _keywordAdd As String
    Public Property keywordAdd As String
        Set(ByVal value As String)
            If Not _keywordAdd = value Then
                _keywordAdd = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordAdd"))
            End If
        End Set
        Get
            Return _keywordAdd
        End Get
    End Property

    Private _keywordRemove As String
    Public Property keywordRemove As String
        Set(ByVal value As String)
            If Not _keywordRemove = value Then
                _keywordRemove = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordRemove"))
            End If
        End Set
        Get
            Return _keywordRemove
        End Get
    End Property

    Public Property keywordsExisting As ObservableCollection(Of String)
    Public Property keywordsAdd As ObservableCollection(Of String)
    Public Property keywordsRemove As ObservableCollection(Of String)
    Public Property keywordsOfficial As ObservableCollection(Of String)

#End Region

#Region "Initialization"
    Friend Sub New()
        keywords = New ObservableCollection(Of cKeyword)

        keywordsExisting = New ObservableCollection(Of String)
        keywordsAdd = New ObservableCollection(Of String)
        keywordsRemove = New ObservableCollection(Of String)
        keywordsOfficial = New ObservableCollection(Of String)

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cKeywordsManager

        With myClone
            .prefix = prefix
            .name = name
            .description = description

            For Each keyword As cKeyword In keywords
                .keywords.Add(CType(keyword.Clone, cKeyword))
            Next
        End With

        Return myClone
    End Function
#End Region

#Region "Methods"
    Private Sub AddEntry()
        _keywordEntry = New cKeyword
        keywords.Add(_keywordEntry)
    End Sub

#End Region

End Class
