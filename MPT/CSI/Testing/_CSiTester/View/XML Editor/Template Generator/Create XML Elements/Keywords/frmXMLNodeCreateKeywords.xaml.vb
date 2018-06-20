Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting

Public Class frmXMLNodeCreateKeywords
    Implements INotifyPropertyChanged
    Implements IMessengerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Variables"
    Dim nodesChanged As Boolean
    Dim editExisting As Boolean
#End Region

#Region "Properties"
    Public Property keywordAction As eXMLEditorAction

    Public Property keywordsList As ObservableCollection(Of cKeywordsManager)

    Public Property keywordsExistingList As ObservableCollection(Of cDGEntry)
    Public Property keywordsAddList As ObservableCollection(Of cDGEntry)
    Public Property keywordsRemoveList As ObservableCollection(Of cDGEntry)

    Private _keywordsCreateEmpty As Boolean
    Public Property keywordsCreateEmpty As Boolean
        Set(ByVal value As Boolean)
            If Not _keywordsCreateEmpty = value Then
                _keywordsCreateEmpty = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordsCreateEmpty"))
            End If
        End Set
        Get
            Return _keywordsCreateEmpty
        End Get
    End Property

    Private _keywordsNumberEmpty As Integer
    Public Property keywordsNumberEmpty As Integer
        Set(ByVal value As Integer)
            If Not _keywordsNumberEmpty = value Then
                _keywordsNumberEmpty = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordsNumberEmpty"))
            End If
        End Set
        Get
            Return _keywordsNumberEmpty
        End Get
    End Property

    Public Property keywordsSave As ObservableCollection(Of String)

#End Region

#Region "Initialization"

    Friend Sub New(Optional ByVal myKeywordAction As eXMLEditorAction = eXMLEditorAction.None)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If Not myKeywordAction = eXMLEditorAction.None Then
            keywordAction = myKeywordAction
        End If

        InitializeData()

        InitializeControls()

        InitializeDGClassesAdd()
        InitializeDGClassesRemove()
    End Sub

    ''' <summary>
    ''' Initializes the keywords form for editing the keywords list for a specific selected example.
    ''' </summary>
    ''' <param name="myKeywordsList"></param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef myKeywordsList As ObservableCollection(Of String))
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData()

        InitializeControls()

        keywordAction = Nothing
        editExisting = True
        keywordsSave = myKeywordsList
        keywordsList(0).keywordsExisting = myKeywordsList

        InitializeDGClassesExisting()
        InitializeDGClassesAdd()
        InitializeDGClassesRemove()
    End Sub


    Private Sub InitializeData()
        keywordsList = New ObservableCollection(Of cKeywordsManager)
        keywordsList.Add(New cKeywordsManager)

        If Not keywordAction = eXMLEditorAction.ActionToNewFiles Then
            If myXMLEditor.addKeywordList IsNot Nothing Then myXMLEditor.addKeywordList.Clear()
        End If

        nodesChanged = False
        editExisting = False
    End Sub

    Private Sub InitializeControls()
        btnCreateEmptyRows.IsEnabled = False

        txtBx_keywordsNum.IsEnabled = False
    End Sub

    ''' <summary>
    ''' Initializes the datagrid for displaying keywords that exist in the examples selected.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeDGClassesExisting()
        keywordsExistingList = New ObservableCollection(Of cDGEntry)

        For Each myEntry As String In keywordsList(0).keywordsExisting
            Dim mydgEntry As New cDGEntry
            mydgEntry.columnHeader = "Existing Keywords"
            mydgEntry.rowEntry = myEntry

            keywordsExistingList.Add(mydgEntry)
        Next

        dg_keywordsExisting.ItemsSource = keywordsExistingList
    End Sub

    ''' <summary>
    ''' Initializes the datagrid for displaying keywords specified to be added to the examples selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDGClassesAdd()
        Dim tempKeywordAddList As New List(Of String)

        If myXMLEditor.addKeywordList Is Nothing Then
            tempKeywordAddList = keywordsList(0).keywordsAdd.ToList
        ElseIf myXMLEditor.addKeywordList.Count > 0 Then
            If String.IsNullOrEmpty(myXMLEditor.addKeywordList(0)) Then
                tempKeywordAddList = keywordsList(0).keywordsAdd.ToList
            Else
                tempKeywordAddList = myXMLEditor.addKeywordList
            End If
        Else                                                    'Use existing keywords list in cXMLEditor
            tempKeywordAddList = myXMLEditor.addKeywordList
        End If

        keywordsAddList = New ObservableCollection(Of cDGEntry)

        For Each myEntry As String In tempKeywordAddList
            Dim mydgEntry As New cDGEntry
            mydgEntry.columnHeader = "Add Keywords"
            mydgEntry.rowEntry = myEntry

            keywordsAddList.Add(mydgEntry)
        Next

        dg_keywordsAdd.ItemsSource = keywordsAddList

    End Sub

    ''' <summary>
    ''' Initializes the datagrid for displaying keywords that are to be removed from the examples selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDGClassesRemove()
        keywordsRemoveList = New ObservableCollection(Of cDGEntry)

        For Each myEntry As String In keywordsList(0).keywordsRemove
            Dim mydgEntry As New cDGEntry
            mydgEntry.columnHeader = "Remove Keywords"
            mydgEntry.rowEntry = myEntry

            keywordsRemoveList.Add(mydgEntry)
        Next

        dg_keywordsRemove.ItemsSource = keywordsRemoveList

    End Sub

#End Region

#Region "Form Controls"
    Private Sub btnCreateNodes_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNodes.Click
        'Compile lists and record them in the xml editor class
        WriteDGClassesAdd()
        WriteDGClassesRemove()

        If editExisting Then
            AddKeywordsToExisting()
            RemoveKeywordsFromExisting()

            keywordsList(0).keywordsExisting = New ObservableCollection(Of String)(TrimListOfEmptyItems(keywordsList(0).keywordsExisting))
            keywordsSave = keywordsList(0).keywordsExisting
        End If

        If keywordAction = eXMLEditorAction.ActionToExistingFiles Then
            'Apply operation through editor class
            If myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.KeywordsAddRemove) Then
                nodesChanged = True
            End If

            'Check if XML files were changed. If so, the editor is refreshed to show the changes.
            If nodesChanged Then
                windowXMLEditorBulk.ChangeXMLSource()
            End If
        End If

        Me.Close()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub CheckBox_Checked(sender As Object, e As RoutedEventArgs)
        txtBx_keywordsNum.IsEnabled = True
    End Sub
    Private Sub CheckBox_Unchecked(sender As Object, e As RoutedEventArgs)
        txtBx_keywordsNum.IsEnabled = False
        btnCreateEmptyRows.IsEnabled = False
    End Sub

    ''' <summary>
    ''' Enforces only integer entries, and enables a button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBx_keywordsNum_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBx_keywordsNum.TextChanged
        If Not IsNumeric(txtBx_keywordsNum.Text) Or StringExistInName(txtBx_keywordsNum.Text, ".") Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), "Invalid character. Enter whole numbers only."))
            txtBx_keywordsNum.Text = CStr(keywordsNumberEmpty)
            Exit Sub
        Else
            Try
                btnCreateEmptyRows.IsEnabled = True
            Catch ex As Exception

            End Try
        End If
    End Sub

    ''' <summary>
    ''' Creates empty datagrid keyword rows
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCreateEmptyRows_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateEmptyRows.Click
        For i = 1 To keywordsNumberEmpty
            Dim mydgEntry As New cDGEntry
            mydgEntry.columnHeader = "Add Keywords"
            mydgEntry.rowEntry = ""

            keywordsAddList.Add(mydgEntry)
        Next i
    End Sub

#End Region

#Region "Methods"

    '=== Dynamically sets the maximum height of the datagrid so that scrollbars appear if not all rows are visible
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the window is made too small to display all rows
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        dg_keywordsExisting.MaxHeight = rowDG.ActualHeight - dg_keywordsExisting.Margin.Bottom - gridMain.Margin.Bottom
        dg_keywordsAdd.MaxHeight = rowDG.ActualHeight - dg_keywordsAdd.Margin.Bottom - gridMain.Margin.Bottom
        dg_keywordsRemove.MaxHeight = rowDG.ActualHeight - dg_keywordsRemove.Margin.Bottom - gridMain.Margin.Bottom


        'dg_keywordsExisting.MinWidth = mainCol.ActualWidth - dg_keywordsExisting.Margin.Bottom - gridMain.Margin.Left - gridMain.Margin.Right
        'dg_keywordsAdd.MinWidth = mainCol.ActualWidth - dg_keywordsAdd.Margin.Bottom - gridMain.Margin.Bottom
        'dg_keywordsRemove.MinWidth = mainCol.ActualWidth - dg_keywordsRemove.Margin.Bottom - gridMain.Margin.Bottom
    End Sub


    ''' <summary>
    ''' Writes a list of the keywords to be added to the selected examples, and assigns the list to an Editor class property.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WriteDGClassesAdd()
        keywordsList(0).keywordsAdd.Clear()

        For Each myEntry As cDGEntry In keywordsAddList
            keywordsList(0).keywordsAdd.Add(myEntry.rowEntry)
        Next

        myXMLEditor.addKeywordList = keywordsList(0).keywordsAdd.ToList
    End Sub

    ''' <summary>
    ''' Writes a list of the keywords to be removed from the selected examples, and assigns the list to an Editor class property.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WriteDGClassesRemove()
        keywordsList(0).keywordsRemove.Clear()

        For Each myEntry As cDGEntry In keywordsRemoveList
            keywordsList(0).keywordsRemove.Add(myEntry.selectedEntry)
        Next

        myXMLEditor.removeKeywordList = keywordsList(0).keywordsRemove.ToList
    End Sub

    ''' <summary>
    ''' Adds all keywords in the 'add' column to the list of existing keywords if they do not currently exist.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddKeywordsToExisting()
        Dim uniqueKeyword As Boolean

        For Each myEntry As cDGEntry In keywordsAddList
            uniqueKeyword = True

            'Check if keyword to remove exists in the list
            For Each keyword As String In keywordsList(0).keywordsExisting
                If myEntry.rowEntry = keyword Then uniqueKeyword = False
            Next

            If uniqueKeyword Then keywordsList(0).keywordsExisting.Add(myEntry.rowEntry)
        Next
    End Sub

    ''' <summary>
    ''' Creates a new list of existing keywords composed of the existing keywords, minus any specified to be removed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveKeywordsFromExisting()
        Dim keywordsTemp As New ObservableCollection(Of String)
        Dim uniqueKeyword As Boolean

        'Check if keyword to remove exists in the list
        For Each keyword As String In keywordsList(0).keywordsExisting
            uniqueKeyword = True

            If keywordsRemoveList.Count > 0 Then
                For Each myEntry As cDGEntry In keywordsRemoveList
                    If myEntry.selectedEntry = keyword Then uniqueKeyword = False
                Next
                If uniqueKeyword Then keywordsTemp.Add(keyword)
            Else
                Exit Sub
            End If
        Next

        keywordsList(0).keywordsExisting.Clear()
        keywordsList(0).keywordsExisting = keywordsTemp
    End Sub

#End Region

End Class