Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cMCModel
Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment
Imports CSiTester.cPathOutputSettings
Imports CSiTester.cFileAttachment

''' <summary>
''' Class which stores a collection of attachment objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCAttachments
    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Enumerations"
    ''' <summary>
    ''' Type that the attachments list is set to be.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eAttachmentType
        enumError = 0
        <Description("Not Specified")> none
        <Description("Images")> images
        <Description("Attachments")> attachments
        <Description("Supporting Files")> supportingFiles
    End Enum
#End Region

#Region "Variables"
    Protected _mcModel As cMCModel
#End Region

#Region "Properties - List"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cFileAttachment
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified file name.
    ''' </summary>
    ''' <param name="p_fileName"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_fileName As String) As cFileAttachment
        Get
            Return GetItem(p_fileName)
        End Get
    End Property
#End Region

#Region "Properties"
    Protected _directoryDestination As String
    ''' <summary>
    ''' Path to the files folder.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property directoryDestination As String
        Get
            Return _directoryDestination
        End Get
    End Property

    Protected _attachmentType As eAttachmentType = eAttachmentType.none
    ''' <summary>
    ''' Type of attachment that the contents are considered to be classified as.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property attachmentType As eAttachmentType
        Set(value As eAttachmentType)
            _attachmentType = value
            ChangeDirectory()
        End Set
        Get
            Return _attachmentType
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Initializes a new collection that is set to reference the provided model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to reference.</param>
    ''' <param name="p_attachmentType">Attachment type to impose on the collection.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_mcModel As cMCModel,
                   Optional ByVal p_attachmentType As eAttachmentType = eAttachmentType.none)
        _attachmentType = p_attachmentType
        Bind(p_mcModel)
    End Sub

    Friend Overloads Function Clone() As Object Implements System.ICloneable.Clone
        Return Clone(Nothing)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_bindTo">If specified, the model control reference will be switched to the one provided. 
    ''' Otherwise, the original reference is kept.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overridable Overloads Function Clone(Optional ByVal p_bindTo As cMCModel = Nothing) As Object
        Dim myClone As New cMCAttachments

        With myClone
            ' Passing reference instead of cloning to maintain reference. DO NOT make a copy!
            If p_bindTo Is Nothing Then
                ' Maintain current reference
                ._mcModel = _mcModel
            Else
                ' Shift reference to newly specified model control object.
                ._mcModel = p_bindTo
            End If

            For Each item As cFileAttachment In InnerList
                .Add(CType(item.Clone(p_bindTo:=p_bindTo), cFileAttachment))
            Next

            ._directoryDestination = _directoryDestination
            ._attachmentType = _attachmentType
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
        If Not (TypeOf p_object Is cMCAttachments) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCAttachments = TryCast(p_object, cMCAttachments)

        'Check for any differences
        If comparedObject Is Nothing Then Return False

        With comparedObject
            If Not ._directoryDestination = _directoryDestination Then Return False
            If Not ._attachmentType = _attachmentType Then Return False
        End With

        For Each itemOuter As cFileAttachment In comparedObject
            isMatch = False
            For Each itemInner As cFileAttachment In InnerList
                If Not itemOuter.Equals(itemInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Friend - Collection"
    ''' <summary>
    ''' Adds every element of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As List(Of cFileAttachment))
        If p_items Is Nothing Then Exit Sub

        For Each item As cFileAttachment In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of cFileAttachment))
        If p_items Is Nothing Then Exit Sub

        For Each item As cFileAttachment In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new attachment item to the list if it is unique in both the file name and title.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_item As cFileAttachment) As Boolean
        If p_item Is Nothing Then Return False

        Try
            Dim itemUnique As Boolean = True

            ' Ensure that the item is unique to the list
            For Each item As cFileAttachment In InnerList
                If (StringsMatch(item.PathAttachment.fileNameWithExtension, p_item.PathAttachment.fileNameWithExtension) OrElse
                    StringsMatch(item.title, p_item.title)) Then

                    itemUnique = False
                    Exit For
                End If
            Next

            If itemUnique Then
                InnerList.Add(p_item)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))

                'Set binding to added item (not before adding, as that will change the object outside!)
                BindAddedAttachment()

                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Removes the item at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_index As Integer)
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            InnerList.RemoveAt(p_index)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the item that has the specified file name.
    ''' </summary>
    ''' <param name="p_fileName">File name that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_fileName As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCFile)

            If String.IsNullOrWhiteSpace(p_fileName) Then Throw New ArgumentException("Parameter {0} is not specified.", p_fileName)

            For Each attachment As cMCFile In InnerList
                If Not StringsMatch(attachment.pathDestination.fileNameWithExtension, p_fileName) Then
                    tempList.Add(attachment)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCFile In tempList
                    InnerList.Add(item)
                Next

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Replaces the matching attachment object with the one provided. 
    ''' A match is determined by either title or the file name.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overridable Overloads Function Replace(ByVal p_item As cFileAttachment) As Boolean
        If p_item Is Nothing Then Return False
        Dim index As Integer = 0

        Try
            If InnerList.Count = 0 Then
                InnerList.Add(p_item)
                BindAddedAttachment()
                Return True
            Else
                Dim attachmentComparer As New cMCAttachmentComparer
                For Each item As cFileAttachment In InnerList
                    If attachmentComparer.Compare(p_item, item) = 0 Then Exit For
                    index += 1
                Next
                If index < InnerList.Count Then
                    InnerList(index) = p_item
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                    BindAddedAttachment(index)
                    Return True
                End If
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As List(Of cFileAttachment))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of cFileAttachment))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns 'True' if the collection contains the provided object, based on the comparer provided.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Contains(ByVal p_item As cFileAttachment,
                             ByVal p_comparer As IComparer) As Boolean
        Try
            For Each item As cFileAttachment In InnerList
                If p_comparer.Compare(item, p_item) = 0 Then
                    Return True
                End If
            Next
        Catch ex As Exception
            ' If an incompatible comparer is passed in, it should also return false.
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cFileAttachment)
        Dim templist As New ObservableCollection(Of cFileAttachment)

        For Each item As cFileAttachment In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cFileAttachment)
        Dim templist As New List(Of cFileAttachment)

        For Each item As cFileAttachment In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function


#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Associates the entire list of attachments with a particular model control file.
    ''' </summary>
    ''' <param name="p_bindTo">Model control file object to bind to the attachments.</param>
    ''' <remarks></remarks>
    Friend Sub Bind(ByVal p_bindTo As cMCModel)
        _mcModel = p_bindTo
        For Each attachment As cFileAttachment In InnerList
            attachment.Bind(p_bindTo)
        Next
    End Sub

    ''' <summary>
    ''' Sets the directory path for the attachment files.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object whose state affects the model file directory. 
    ''' Attachment type also determines the directory.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetAttachmentsDestinationDirectories(ByVal p_mcModel As cMCModel)
        If p_mcModel.folderStructure = eFolderStructure.Flattened Then
            SetDirectoryFlattened(p_mcModel.mcFile.pathDestination.directory)
            NormalizeAttachmentsPaths(p_mcModel.folderStructure)
        Else
            SetDirectoryDatabase(p_mcModel.mcFile.pathDestination.directory)
            NormalizeAttachmentsPaths(p_mcModel.folderStructure)
        End If
    End Sub

    ''' <summary>
    ''' Sets the directory path for the attachment files based on the current attachment type and a flattened directory type.
    ''' </summary>
    ''' <param name="p_mcDirectory">Path to the model control directory.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetDirectoryFlattened(ByVal p_mcDirectory As String)
        _directoryDestination = p_mcDirectory
    End Sub
#End Region

#Region "Methods: Private - Collection"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cFileAttachment
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cFileAttachment)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by file name.
    ''' </summary>
    ''' <param name="p_fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_fileName As String) As cFileAttachment
        Try
            If String.IsNullOrEmpty(p_fileName) Then Throw New ArgumentException("Parameter {0} is not specified.", p_fileName)

            For Each item As cFileAttachment In InnerList
                If StringsMatch(item.PathAttachment.fileNameWithExtension, p_fileName) Then
                    Return item
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Binds the shared model control object to the attachedment object.
    ''' Set binding to added item after adding (not before adding, as that will change the object outside!).
    ''' </summary>
    ''' <param name="p_index">Index in the collection corresponding to the object.
    ''' If not specified, it is assumed to be the last one in the collection, which is the appropriate case when adding an object.</param>
    ''' <remarks></remarks>
    Private Sub BindAddedAttachment(Optional ByVal p_index As Integer = -1)
        ' Set index to last in the collection if a valid one is not specified.
        If (p_index = -1 OrElse p_index > InnerList.Count - 1) Then p_index = InnerList.Count - 1


        Dim newItem As cFileAttachment = DirectCast(InnerList.Item(p_index), cFileAttachment)
        newItem.Bind(_mcModel)
    End Sub

    ''' <summary>
    ''' Sets the directory path for the attachment files based on the current attachment type and a database directory type.
    ''' </summary>
    ''' <param name="p_mcDirectory">Path to the model control directory.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub SetDirectoryDatabase(ByVal p_mcDirectory As String)
        Select Case attachmentType
            Case eAttachmentType.attachments
                _directoryDestination = p_mcDirectory & "\" & DIR_NAME_ATTACHMENTS_DEFAULT
            Case eAttachmentType.images
                _directoryDestination = p_mcDirectory & "\" & DIR_NAME_FIGURES_DEFAULT
            Case eAttachmentType.supportingFiles
                _directoryDestination = p_mcDirectory & "\" & DIR_NAME_MODELS_DEFAULT
        End Select
    End Sub

    ''' <summary>
    ''' Sets all attachment path destinations to reflect the same destination directory for the appropriate attachment type.
    ''' </summary>
    ''' <param name="p_folderStructure">Structure of the folders where the attachments will be copied.</param>
    ''' <remarks></remarks>
    Protected Overridable Sub NormalizeAttachmentsPaths(ByVal p_folderStructure As eFolderStructure)
        If String.IsNullOrEmpty(_directoryDestination) Then Exit Sub

        For Each attachment As cFileAttachment In InnerList
            If (attachmentType = eAttachmentType.attachments AndAlso
                p_folderStructure = eFolderStructure.Flattened AndAlso
                IsOutputSettingsAttachment(attachment)) Then

                With attachment.PathAttachment
                    .SetProperties(_directoryDestination & "\" & DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT & "\" & .fileNameWithExtension)
                End With
            Else
                With attachment.PathAttachment
                    .SetProperties(_directoryDestination & "\" & .fileNameWithExtension)
                End With
            End If
        Next
    End Sub

    ''' <summary>
    ''' Updates the directory path for the attachment type set.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub ChangeDirectory()
        Dim mcDirectory As String = _directoryDestination
        Dim lastDirectory As String = GetSuffix(mcDirectory, "\")
        Dim filterString As String = ""

        If _mcModel.folderStructure = eFolderStructure.Database Then
            Select Case lastDirectory
                Case DIR_NAME_ATTACHMENTS_DEFAULT
                    filterString = DIR_NAME_ATTACHMENTS_DEFAULT
                Case DIR_NAME_FIGURES_DEFAULT
                    filterString = DIR_NAME_FIGURES_DEFAULT
                Case DIR_NAME_MODELS_DEFAULT
                    filterString = DIR_NAME_MODELS_DEFAULT
                Case Else
                    filterString = ""
            End Select

            If Not String.IsNullOrEmpty(filterString) Then
                mcDirectory = FilterStringFromName(mcDirectory, "\" & filterString, p_retainPrefix:=True, p_retainSuffix:=False, p_endDirectory:=True)
                SetDirectoryDatabase(mcDirectory)
            End If
        ElseIf _mcModel.folderStructure = eFolderStructure.Flattened Then
            'No action is taken
        End If

    End Sub

    ''' <summary>
    ''' Returns True if the attachment object is an outputSettings attachment.
    ''' </summary>
    ''' <param name="p_attachment">Attachment object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsOutputSettingsAttachment(ByVal p_attachment As cFileAttachment) As Boolean
        If StringExistInName(p_attachment.title, TAG_ATTACHMENT_TABLE_SET_FILE) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
