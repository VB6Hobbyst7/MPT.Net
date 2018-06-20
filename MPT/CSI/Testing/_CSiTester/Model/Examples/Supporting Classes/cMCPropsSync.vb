Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Contains state of whether or not a given property is to be synced for all model control objects in a group.
''' </summary>
''' <remarks></remarks>
Friend Class cMCPropsSync
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    ''' <summary>
    ''' Some general properties, such as title, description, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property common As Boolean = True
    Friend Property idExample As Boolean = True
    Friend Property benchmarkReferences As Boolean = True
    Friend Property excelResults As Boolean = True

    ' Not all objects in the collections need to be in sync.
    ' These lists track which specific objects are to be kept in sync.
    Friend Property attachments As New Dictionary(Of String, Boolean)
    Friend Property images As New Dictionary(Of String, Boolean)
    Friend Property links As New Dictionary(Of String, Boolean)
    Friend Property incidents As New Dictionary(Of String, Boolean)
    Friend Property tickets As New Dictionary(Of String, Boolean)
    Friend Property updates As New Dictionary(Of String, Boolean)
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
#End Region

#Region "Overrides/Implements"
    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCPropsSync

        With myClone
            .common = common
            .idExample = idExample
            .benchmarkReferences = benchmarkReferences
            .excelResults = excelResults

            .attachments = CopyDictionary(attachments)
            .images = CopyDictionary(images)
            .links = CopyDictionary(links)
            .incidents = CopyDictionary(incidents)
            .tickets = CopyDictionary(tickets)
            .updates = CopyDictionary(updates)
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
        If Not (TypeOf p_object Is cMCPropsSync) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCPropsSync = TryCast(p_object, cMCPropsSync)

        'Check for any differences
        If comparedObject Is Nothing Then Return False

        With comparedObject
            If Not .common = common Then Return False
            If Not .idExample = idExample Then Return False
            If Not .benchmarkReferences = benchmarkReferences Then Return False
            If Not .excelResults = excelResults Then Return False

            If Not DictionaryEquals(.attachments, attachments) Then Return False
            If Not DictionaryEquals(.images, images) Then Return False
            If Not DictionaryEquals(.links, links) Then Return False
            If Not DictionaryEquals(.incidents, incidents) Then Return False
            If Not DictionaryEquals(.tickets, tickets) Then Return False
            If Not DictionaryEquals(.updates, updates) Then Return False
        End With


        Return True
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns a new dictionary that is a value copy of the provided dictionary.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <typeparam name="U"></typeparam>
    ''' <param name="p_dictionary"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyDictionary(Of T, U)(ByVal p_dictionary As Dictionary(Of T, U)) As Dictionary(Of T, U)
        Dim dictionaryClone As New Dictionary(Of T, U)
        For Each key As T In p_dictionary.Keys
            dictionaryClone.Add(key, p_dictionary(key))
        Next

        Return dictionaryClone
    End Function

    ''' <summary>
    ''' Determines if the two provided dictionaries are equal.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <typeparam name="U"></typeparam>
    ''' <param name="p_dictionaryOuter"></param>
    ''' <param name="p_dictionaryInner"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DictionaryEquals(Of T, U)(ByVal p_dictionaryOuter As Dictionary(Of T, U),
                                               ByVal p_dictionaryInner As Dictionary(Of T, U)) As Boolean
        Dim isMatch As Boolean = False

        For Each keyOuter As T In p_dictionaryOuter.Keys
            isMatch = False
            If p_dictionaryInner.ContainsKey(keyOuter) AndAlso
                p_dictionaryInner(keyOuter).Equals(p_dictionaryOuter(keyOuter)) Then

                isMatch = True
            Else
                Exit For
            End If
        Next

        Return isMatch
    End Function
#End Region
End Class
