Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Imports CSiTester.cMCModelID
Imports CSiTester.cMCModel

''' <summary>
''' Class which stores a collection of model control objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCModels
    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Enumerations"
    ''' <summary>
    ''' Specifies the sorting order of the model control objects in the collection.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eSort
        <Description("Composite ID")> ID
        <Description("Model Name")> ModelName
        <Description("Example Name")> ExampleName
    End Enum
#End Region

#Region "Fields"
    ''' <summary>
    ''' Shared name of the most recently considered example for a multi-model example. e.g. "Example 10a", this would be "Example 10".
    ''' </summary>
    ''' <remarks></remarks>
    Private _latestExampleName As String
#End Region

#Region "Properties"
    ''' <summary>
    ''' Setting as to whether or not all models are part of one example, multimodel examples should be generated automatically based on file name, 
    ''' or all examples are to be treated as full and separate examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property multiModelMethod As eMultiModelIDNumbering

    Private _totalReservedIDs As New cObsColUniqueString
    ''' <summary>
    ''' Readonly list of all IDs that will be skipped in numbering collection IDs. 
    ''' Models that contain such an ID will also be skipped in setting IDs.
    ''' This list includes existing/added IDs if the class is set to preserve existing IDs.
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property totalReservedIDs As List(Of Decimal)
        Get
            Return ConvertTotalReservedIDs()
        End Get
    End Property
    ''' <summary>
    ''' ReadOnly list of all example IDs that will be skipped in numbering collection IDs. 
    ''' Models that contain such an ID will also be skipped in setting IDs.
    ''' This list includes existing/added IDs if the class is set to preserve existing IDs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property totalReservedExampleIDs As List(Of Integer)
        Get
            Return ConvertTotalReservedIDsToExampleIDs()
        End Get
    End Property

    Private _propertiesSync As New cExamplePropsSync
    ''' <summary>
    ''' Returns a copy of the object that contains syncing information for various model control properties that are to be shared across the entire example.
    ''' </summary>
    ''' <param name="p_exampleID">Example ID to get the object by.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property propertiesSync(ByVal p_exampleID As Integer) As cMCPropsSync
        Get
            For Each compositeID As String In IDs
                If myCInt(GetPrefix(compositeID, ".")) = p_exampleID Then
                    Return _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True)
                End If
            Next
            Return Nothing
        End Get
    End Property

    Private _skippedIDsList As New cObsColUniqueString
    ''' <summary>
    ''' ID numbers to be skipped when auto-generating unique model ID numbers associated with the examples.
    ''' Provided from an external source.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property skippedIDsList As cObsColUniqueString
        Get
            Return _skippedIDsList
        End Get
    End Property

    Private _overWriteExistingIDs As Boolean = False
    ''' <summary>
    ''' False: If list model IDs are set, currently set model IDs will be skipped.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property overWriteExistingIDs As Boolean
        Get
            Return _overWriteExistingIDs
        End Get
        Set(value As Boolean)
            If Not _overWriteExistingIDs = value Then
                _overWriteExistingIDs = value
                SetTotalSkippedIDs()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Starting ID to use in auto-generating unique model ID numbers associated with the examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property startingExampleID As String = "0"

    ''' <summary>
    ''' Minimum model ID in the collection.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property minID As String
        Get
            Return MinCompositeID()
        End Get
    End Property

    ''' <summary>
    ''' Maximum model ID in the collection.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property maxID As String
        Get
            Return MaxCompositeID()
        End Get
    End Property

    ''' <summary>
    ''' All unique composite IDs in the collection.
    ''' </summary>
    ''' <remarks></remarks>
    Private _IDs As New cObsColUniqueString
    ''' <summary>
    ''' List of all composite IDs in the collection, sorted (May be in different order than actual list).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property IDs As List(Of String)
        Get
            _IDs.ToList.Sort()
            Return _IDs.ToList
        End Get
    End Property

    ''' <summary>
    ''' List of all example IDs in the collection.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property IDsExample As List(Of Integer)
        Get
            Return GetExampleIDs()
        End Get
    End Property
#End Region

#Region "Properties - List"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cMCModel
        Get
            Return GetItem(p_index)
        End Get
    End Property
    ''' <summary>
    ''' Gets the element at the model ID.
    ''' </summary>
    ''' <param name="p_id"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_id As Decimal) As cMCModel
        Get
            Return GetItem(p_id)
        End Get
    End Property
    ''' <summary>
    ''' Gets the element of the specified composite ID.
    ''' </summary>
    ''' <param name="p_id"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_id As String) As cMCModel
        Get
            Return GetItem(p_id)
        End Get
    End Property
    ''' <summary>
    ''' Gets the element of the path.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <param name="p_isSource">True: The path used to determine a match is the source path. 
    ''' False: The path used to determine a match is the destination path.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_path As cPath,
                                                    Optional ByVal p_isSource As Boolean = False) As cMCModel
        Get
            Return GetItem(p_path, p_isSource)
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCModels

        With myClone
            For Each item As cMCModel In InnerList
                .Add(item)
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
        If Not (TypeOf p_object Is cMCModels) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCModels = TryCast(p_object, cMCModels)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each itemOuter As cMCModel In comparedObject
            isMatch = False
            For Each itemInner As cMCModel In InnerList
                If itemOuter.Equals(itemInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
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
    Friend Overloads Sub Add(ByVal p_items As ICollection(Of cMCModel))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCModel In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new model control item to the list if it is unique by model id.
    ''' </summary>
    ''' <param name="p_object"></param>
    ''' <param name="p_incrementID">True: The model ID will automatically be assigned based on the last model of the collection.</param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_object As cMCModel,
                                  Optional p_incrementID As Boolean = False) As Boolean
        Try
            If p_object Is Nothing Then Return False

            'If update entry is unique, add it
            If MCModelUnique(p_object) Then
                If p_incrementID Then SetNextID(p_object)

                AddID(p_object.ID.idComposite)
                InnerList.Add(p_object)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))

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

            Dim item As cMCModel = GetItem(p_index)
            RemoveID(item.ID.idComposite)
            InnerList.RemoveAt(p_index)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the item that has the specified model id.
    ''' </summary>
    ''' <param name="p_modelID">Model ID that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_modelID As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCModel)

            If String.IsNullOrWhiteSpace(p_modelID) Then Throw New ArgumentException("p_modelID is not specified.")
            If p_modelID = "0" Then Throw New ArgumentException("p_modelID cannot be 0 for removal. Models without model IDs use this, so it is not a unique identifier.")

            For Each item As cMCModel In InnerList
                If Not item.ID.idComposite = p_modelID Then
                    tempList.Add(item)
                Else
                    itemsRemoved = True
                    RemoveID(item.ID.idComposite)
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCModel In tempList
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
    ''' Removes the item that has the specified path.
    ''' </summary>
    ''' <param name="p_path">Path that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_path As cPath)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCModel)

            If p_path Is Nothing Then Throw New ArgumentException("The path provided is 'Nothing'.")
            If String.IsNullOrWhiteSpace(p_path.path) Then Throw New ArgumentException("The path object provided has no path set.")

            For Each item As cMCModel In InnerList
                If Not item.mcFile.pathDestination.path = p_path.path Then
                    tempList.Add(item)
                Else
                    itemsRemoved = True
                    RemoveID(item.ID.idComposite)
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCModel In tempList
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
    ''' Replaces the matching model control object with the one provided. 
    ''' A match is determined by model ID.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Replace(ByVal p_item As cMCModel) As Boolean
        Try
            If p_item Is Nothing Then Return False
            Dim index As Integer = 0

            For Each item As cMCModel In InnerList
                If item.ID.idComposite = p_item.ID.idComposite Then
                    Exit For
                End If
                index += 1
            Next
            If index < InnerList.Count Then
                InnerList(index) = p_item
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function
    ''' <summary>
    ''' Replaces the matching model control object with the one provided. 
    ''' A match is determined by model path.
    ''' </summary>
    ''' <param name="p_pathIsSource">True: The path used to match the item is the source path. 
    ''' False: The path used is the destination path.</param>
    ''' <remarks></remarks>
    Friend Overloads Function Replace(ByVal p_item As cMCModel,
                                      ByVal p_pathIsSource As Boolean) As Boolean
        Try
            If p_item Is Nothing Then Return False
            Dim index As Integer = 0

            Sort(eSort.ID)

            If p_pathIsSource Then
                For Each item As cMCModel In InnerList
                    If StringsMatch(item.modelFile.pathSource.path, p_item.modelFile.pathSource.path) Then
                        Exit For
                    End If
                    index += 1
                Next
            Else
                For Each item As cMCModel In InnerList
                    If StringsMatch(item.modelFile.pathDestination.path, p_item.modelFile.pathDestination.path) Then
                        Exit For
                    End If
                    index += 1
                Next
            End If

            If index < InnerList.Count Then
                ReplaceID(DirectCast(InnerList(index), cMCModel).ID.idComposite, p_item.ID.idComposite)
                InnerList(index) = p_item
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                Return True
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
    Friend Overloads Sub Replace(ByVal p_items As ICollection(Of cMCModel))
        InnerList.Clear()
        _totalReservedIDs.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Sorts the objects by the specified criteria.
    ''' </summary>
    ''' <param name="p_sort">Enumeration of which criteria to sort by.</param>
    ''' <remarks></remarks>
    Friend Sub Sort(ByVal p_sort As eSort)
        Select Case p_sort
            Case eSort.ExampleName
                SortByExampleName()
            Case eSort.ID
                SortByID()
            Case eSort.ModelName
                SortByModelName()
        End Select
    End Sub

    ''' <summary>
    ''' Returns the index of the model control object based on the composite [example].[model] ID.
    ''' </summary>
    ''' <param name="p_IDComposite"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IndexOf(ByVal p_IDComposite As Decimal) As Integer
        Dim index As Integer = 0

        For Each item As cMCModel In InnerList
            If item.ID.idCompositeDecimal = p_IDComposite Then Return index
            index += 1
        Next
        Return -1
    End Function

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cMCModel)
        Return TryCast(ToCollection(), ObservableCollection(Of cMCModel))
    End Function
    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cMCModel)
        Return TryCast(ToCollection(), List(Of cMCModel))
    End Function
    ''' <summary>
    ''' Returns the collection object as any collection type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToCollection() As ICollection(Of cMCModel)
        Return ToCollection(Of cMCModel)()
    End Function

    ' TODO: Consider making a common base class that contains the following:
    ''' <summary>
    ''' Returns the collection object as any collection type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function ToCollection(Of T)() As ICollection(Of T)
        Dim templist As New Collection(Of T)

        For Each item As T In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns a list of model control objects that belong to the same example.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to base the grouping on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetExampleModels(ByVal p_mcModel As cMCModel) As List(Of cMCModel)
        Return GetExampleModels(p_mcModel.ID.idExample)
    End Function
    ''' <summary>
    ''' Returns a list of model control objects that belong to the same example.
    ''' </summary>
    ''' <param name="p_exampleID">Example ID to base the grouping on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetExampleModels(ByVal p_exampleID As Integer) As List(Of cMCModel)
        Dim exampleModels As New List(Of cMCModel)

        For Each model As cMCModel In InnerList
            If model.ID.idExample = p_exampleID Then
                exampleModels.Add(model)
            End If
        Next

        Return exampleModels
    End Function
#End Region

#Region "Methods: Friend - Query"
    ''' <summary>
    ''' Checks all paths in the list of model control objects and returns the longest path that is shared among all of the model control files.
    ''' Returns nothing if no shared parent directory can be found.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function CommonParentDirectory() As String
        'Determine the shared parent directory of all filtered path objects
        Dim tempList As New List(Of String)
        Dim sharedPath As Boolean = True
        Dim currentCharacter As String = ""
        Dim maxCount As Integer
        Dim maxPath As String = ""

        For Each mcModel As cMCModel In InnerList
            tempList.Add(mcModel.mcFile.pathDestination.directory)
        Next
        If tempList.Count > 0 Then
            'Get longest path
            For Each pathDir As String In tempList
                If maxCount < Len(pathDir) Then
                    maxCount = Len(pathDir)
                    maxPath = pathDir
                End If

            Next

            For i = 1 To maxCount
                currentCharacter = Mid(maxPath, i, 1)
                For Each pathDir As String In tempList
                    If Not currentCharacter = Mid(pathDir, i, 1) Then
                        sharedPath = False
                        Exit For
                    End If
                Next
                If Not sharedPath Then Return Left(maxPath, i - 1)
            Next
        End If
        Return ""
    End Function

    ''' <summary>
    ''' Returns a collection of all models that have the specified status, paired with the file path.
    ''' </summary>
    ''' <param name="p_exampleStatus">Example status trigger that causes an example to be included (Complete, Edited) or skipped (Incomplete, Edit). 
    ''' Regardless of completion, examples will always be included if this is "Unspecified".</param>
    ''' <param name="p_checkIfFileExists">True: The list returned will be dependent on the existence of a corresponding Model Control File. 
    ''' False: The list will contain any model object of the specified status.</param>
    ''' <param name="p_getNonExistingModels">Only active if p_checkIfFileExists=True. 
    ''' True: The list will only contain model objects that have no existing corresponding file.
    ''' False: The list will only contain models that currently have existing Model Control files.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ModelsByStatus(ByVal p_exampleStatus As eExampleStatus,
                                       Optional p_checkIfFileExists As Boolean = False,
                                       Optional p_getNonExistingModels As Boolean = False) As cMCModels
        Dim addModel As Boolean = False
        Dim models As New cMCModels

        For Each mcModel As cMCModel In InnerList
            addModel = False

            If (mcModel.statusExample = GetEnumDescription(p_exampleStatus) OrElse
                p_exampleStatus = eExampleStatus.Unspecified) Then

                If p_checkIfFileExists Then
                    If (Not p_getNonExistingModels AndAlso
                        IO.File.Exists(mcModel.mcFile.pathDestination.path)) Then

                        addModel = True
                    ElseIf (p_getNonExistingModels AndAlso
                            Not IO.File.Exists(mcModel.mcFile.pathDestination.path)) Then

                        addModel = True
                    End If
                Else
                    addModel = True
                End If
            End If

            If addModel Then models.Add(mcModel)
        Next

        Return models
    End Function

    ''' <summary>
    ''' For a given example status, returns a collection of models that contain Excel results.
    ''' </summary>
    ''' <param name="p_exampleStatus">Example status trigger that causes an example to be included (Complete, Edited) or skipped (Incomplete, Edit). 
    ''' Regardless of completion, examples will always be included if this is "Unspecified".</param>
    ''' <param name="p_ignoreBulkUpdate">True: All examples of matching status with Excel results will be returned.
    ''' False: Values will only be returned if all matching examples are set to be updated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ModelsWithExcelResultsByStatus(ByVal p_exampleStatus As eExampleStatus,
                                                   Optional ByVal p_ignoreBulkUpdate As Boolean = False) As cMCModels
        Dim models As New cMCModels

        If (p_ignoreBulkUpdate OrElse
            AllModelsWithExcelResultsToBeUpdated(p_exampleStatus)) Then

            'Gather list of examples to save
            For Each mcModel As cMCModel In InnerList
                If (ExampleStatusValid(p_exampleStatus, mcModel) AndAlso
                    mcModel.resultsExcel.Count > 0) Then

                    models.Add(mcModel)
                End If
            Next
        End If

        Return models
    End Function


    'TODO: See about merging the next two?
    ''' <summary>
    ''' True: No examples in the collection are incomplete of the minimum requirements.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AllExamplesComplete() As Boolean
        Dim exampleStatusIncomplete As String = GetEnumDescription(eExampleStatus.Incomplete)
        Dim isComplete As Boolean = True

        For Each mcModel As cMCModel In InnerList
            If Not mcModel.statusExample = exampleStatusIncomplete Then
                isComplete = False
                Exit For
            End If
        Next

        Return isComplete
    End Function
    ''' <summary>
    ''' True: For all model control objects, the minimum required information has been specified for creating example XML control files.
    ''' Updates the status based on the current state.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AllRequiredDataFilled() As Boolean
        Dim isRequiredDataFilled As Boolean = True

        For Each mcModel As cMCModel In InnerList
            If Not mcModel.RequiredDataFilled() Then
                isRequiredDataFilled = False
                Exit For
            End If
        Next

        Return isRequiredDataFilled
    End Function


    ''' <summary>
    ''' Determines if any examples of the specified status have Excel results and if all Excel results are set to be updated.
    ''' If so, the individual example switches are turned off with the assumption that updates will be done in bulk.
    ''' If any Excel results are not set to be updated, this will not occur to avoid updating results that are not set to be updated.
    ''' </summary>
    ''' <param name="p_exampleStatus">Example status trigger that causes an example to be included (Complete, Edited) or skipped (Incomplete, Edit). 
    ''' Regardless of completion, examples will always be included if this is "Unspecified".</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AllModelsWithExcelResultsToBeUpdated(ByVal p_exampleStatus As eExampleStatus) As Boolean
        Dim modelsWithExcelResultsExist As Boolean = False
        Dim allToBeUpdated As Boolean = True

        For Each mcModel As cMCModel In InnerList
            If (ExampleStatusValid(p_exampleStatus, mcModel) AndAlso
                mcModel.resultsExcel.Count > 0) Then

                modelsWithExcelResultsExist = True
                If Not mcModel.updateResultsFromExcel Then allToBeUpdated = False
            End If
        Next

        If (modelsWithExcelResultsExist AndAlso allToBeUpdated) Then
            SuppressExcelResultsUpdate(p_exampleStatus)
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Methods: Friend - Action"
    ''' <summary>
    ''' Sets up the list of model ids to skip.
    ''' </summary>
    ''' <param name="p_skippedModels">Manually specified IDs to skip in the list.</param>
    ''' <remarks></remarks>
    Friend Sub SetSkippedModelIDsList(Optional ByVal p_skippedModels As cObsColUniqueString = Nothing)
        If p_skippedModels Is Nothing Then p_skippedModels = New cObsColUniqueString

        _skippedIDsList = p_skippedModels
        SetTotalSkippedIDs()
    End Sub

    ''' <summary>
    ''' Sets all of the model IDs in the model control objects in the collection.
    ''' </summary>
    ''' <param name="p_sort">Specifies the sorting order of the model control objects in the collection.</param>
    ''' <remarks></remarks>
    Friend Sub SetModelIDs(ByVal p_sort As eSort)
        Try
            Sort(p_sort)

            Dim isFirstModel As Boolean = True
            Dim latestModelID As Decimal = CDec(startingExampleID)

            _latestExampleName = ""
            _IDs.Clear()
            SetTotalSkippedIDs()

            For Each currentMCModel As cMCModel In InnerList
                SetID(currentMCModel, latestModelID, _latestExampleName, isFirstModel)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' For all items, finalizes preparation of the example and saves data to the Model Control file.
    ''' Other relevant files, such as attachments, images, Excel files, and outputSettings files are saved to the appropriate locations.
    ''' Returns an error message if any major action failed.
    ''' </summary>
    ''' <param name="p_throwExceptions">True: Exceptions will be thrown and must be handled elsewhere. False: Likely exceptions will be handled within the method.</param>
    ''' <param name="p_suppressMessages">True: No result messages will appear.</param>
    ''' <remarks></remarks>
    Friend Function SaveExamplesToFiles(Optional p_throwExceptions As Boolean = False,
                                        Optional p_suppressMessages As Boolean = False) As Boolean

        Dim statusSaved As String = ""
        Dim statusFailed As String = ""

        For Each mcModel As cMCModel In InnerList
            Try
                mcModel.SaveExampleToFiles(p_throwExceptions:=True, p_suppressMessages:=True)
                statusSaved &= mcModel.mcFile.pathDestination.path & Environment.NewLine & Environment.NewLine
            Catch ex As ArgumentException
                ' No action. Incomplete files are expected and skipped
            Catch ex As IOException
                If p_throwExceptions Then
                    Throw New IOException(ex.Message)
                Else
                    statusFailed &= ex.Message & Environment.NewLine & Environment.NewLine
                End If
            End Try
        Next

        If Not String.IsNullOrEmpty(statusFailed) Then statusFailed = "The following errors were encountered: " & Environment.NewLine & Environment.NewLine & statusFailed & Environment.NewLine & "=====================" & Environment.NewLine
        If Not String.IsNullOrEmpty(statusSaved) Then statusSaved = "The following examples were saved: " & Environment.NewLine & Environment.NewLine & statusSaved
        Dim statusMessage As String = statusFailed & statusSaved

        If Not p_suppressMessages Then
            Dim status = New frmLongListPrompt(eMessageActionSets.OkOnly, "Files Saved", "", "", statusMessage, MessageBoxImage.None)
            status.Show()
        End If

        If String.IsNullOrEmpty(statusFailed) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' For all examples in the collection, updates the Model Control file of the Excel results and then populates the corresponding classes with those results.
    ''' </summary>
    ''' <param name="p_exampleStatus">Example status trigger that causes an example to be included (Complete, Edited) or skipped (Incomplete, Edit). 
    ''' Regardless of completion, examples will always be included if this is "Unspecified".</param>
    ''' <remarks></remarks>
    Friend Sub UpdateResultsFromExcelFilesToMCFiles(ByVal p_exampleStatus As eExampleStatus)
        Dim directories As New List(Of String)
        Dim modelsToBeUpdated As New cMCModels

        If AllModelsWithExcelResultsToBeUpdated(p_exampleStatus) Then
            modelsToBeUpdated = ModelsWithExcelResultsByStatus(p_exampleStatus, p_ignoreBulkUpdate:=True)
            directories.Add(CommonParentDirectory())
        Else
            For Each mcModel As cMCModel In InnerList
                If mcModel.updateResultsFromExcel Then
                    directories.Add(mcModel.mcFile.pathDestination.directory)
                    modelsToBeUpdated.Add(mcModel)
                End If
            Next
        End If

        UpdateResultsFromExcelFilesToMCFiles(directories, modelsToBeUpdated)

        'Update list
        For Each mcModel As cMCModel In modelsToBeUpdated
            Replace(mcModel)
        Next
    End Sub

    ''' <summary>
    ''' Automatically sets the syncing status of various properties of the model control objects in the collection.
    ''' This is determined automatically by finding the two criteria true: 
    '''   1. The example is multi-model
    '''   2. For a given property, it is identical across all model control objects within the example.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AutoSetExamplePropertiesSync()
        Dim exampleIDs As List(Of Integer) = IDsExample
        For Each exampleID As Integer In exampleIDs
            Dim models As List(Of cMCModel) = GetExampleModels(exampleID)
            If (models.Count = 0 OrElse models(0).ID.multiModelType = eMultiModelType.singleModel) Then
                Continue For
            ElseIf Not _propertiesSync.Contains(exampleID) Then
                _propertiesSync.Add(exampleID, New cMCPropsSync)
            ElseIf models.Count > 1 Then
                AutoSetCommon(_propertiesSync(exampleID), models)
                AutoSetBenchmarkReference(_propertiesSync(exampleID), models)
                AutoSetExcelResult(_propertiesSync(exampleID), models)
                AutoSetListObjects(_propertiesSync(exampleID), models)
            End If
        Next
    End Sub

    ' Query of Contents
    Friend Function ContainsAttachment(ByVal p_item As cFileAttachment,
                                       Optional ByVal p_exampleID As Integer = 0) As Boolean
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            If model.attachments.Contains(p_item, New cMCAttachmentComparer) Then Return True
        Next
        Return False
    End Function
    Friend Function ContainsImage(ByVal p_item As cFileAttachment,
                                  Optional ByVal p_exampleID As Integer = 0) As Boolean
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            If model.images.Contains(p_item, New cMCAttachmentComparer) Then Return True
        Next
        Return False
    End Function
    Friend Function ContainsLink(ByVal p_item As cMCLink,
                                 Optional ByVal p_exampleID As Integer = 0) As Boolean
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            If model.links.Contains(p_item, New cMCLinkComparer) Then Return True
        Next
        Return False
    End Function
    Friend Function ContainsIncident(ByVal p_item As Integer,
                                     Optional ByVal p_exampleID As Integer = 0) As Boolean
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            If model.incidents.Contains(p_item) Then Return True
        Next
        Return False
    End Function
    Friend Function ContainsTicket(ByVal p_item As Integer,
                                   Optional ByVal p_exampleID As Integer = 0) As Boolean
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            If model.tickets.Contains(p_item) Then Return True
        Next
        Return False
    End Function
    Friend Function ContainsUpdate(ByVal p_item As cMCUpdate,
                                   Optional ByVal p_exampleID As Integer = 0) As Boolean
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            If model.updates.Contains(p_item, New cMCUpdateComparer) Then Return True
        Next
        Return False
    End Function

    ' Add/Replace Items in a Model Control Object or Example Group
    Friend Sub AddReplaceAttachment(ByVal p_item As cFileAttachment,
                                    Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            If Not innerModel.attachments.Replace(p_item) Then innerModel.attachments.Add(p_item)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).attachments(p_item.title) = True
    End Sub
    Friend Sub AddReplaceImage(ByVal p_item As cFileAttachment,
                                Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            If Not innerModel.images.Replace(p_item) Then innerModel.images.Add(p_item)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).images(p_item.title) = True
    End Sub
    Friend Sub AddReplaceLink(ByVal p_item As cMCLink,
                            Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            If Not innerModel.links.Replace(p_item) Then innerModel.links.Add(p_item)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).links(p_item.title) = True
    End Sub
    Friend Sub AddIncident(ByVal p_item As Integer,
                            Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            innerModel.incidents.Add(p_item)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).incidents(p_item.ToString) = True
    End Sub
    Friend Sub AddTicket(ByVal p_item As Integer,
                        Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            innerModel.tickets.Add(p_item)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).tickets(p_item.ToString) = True
    End Sub
    Friend Sub AddReplaceUpdate(ByVal p_item As cMCUpdate,
                                Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            If Not innerModel.updates.Replace(p_item) Then innerModel.updates.Add(p_item)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).updates(p_item.id.ToString) = True
    End Sub
    Friend Sub AddReplaceBenchmark(ByVal p_item As cMCBenchmarkRef,
                                   Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            innerModel.program = DirectCast(p_item.Clone, cMCBenchmarkRef)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).benchmarkReferences = True
    End Sub
    Friend Sub AddReplaceExcelResult(ByVal p_item As cMCResultsExcel,
                                     Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            innerModel.AddResultExcel(DirectCast(p_item.Clone, cMCResultsExcel).file, p_replaceExisting:=True)
            innerModel.changedSinceSave = True
        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).excelResults = True
    End Sub
    ''' <summary>
    ''' Adds or replaces values for miscellaneous properties that are often common across model control objects that are part of the same example.
    ''' </summary>
    ''' <param name="p_title">Title of the example.</param>
    ''' <param name="p_description">Description of the example.</param>
    ''' <param name="p_exampleID">Example ID.</param>
    ''' <remarks></remarks>
    Friend Sub AddReplaceCommonProperties(ByVal p_title As String,
                                          ByVal p_description As String,
                                          Optional ByVal p_exampleID As Integer = 0)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_exampleID)
        For Each model As cMCModel In models
            Dim innerModel As cMCModel = DirectCast(InnerList(IndexOf(model.ID.idCompositeDecimal)), cMCModel)
            With innerModel
                If Not .title = p_title Then
                    .title = p_title
                    innerModel.changedSinceSave = True
                End If
                If Not .description = p_description Then
                    .description = p_description
                    innerModel.changedSinceSave = True
                End If
            End With

        Next

        ' Update properties syncing settings
        _propertiesSync(p_exampleID, p_createNewEntryIfNotExist:=True).common = True
    End Sub

    ''' <summary>
    ''' Changes the example ID for all models that share the old ID.
    ''' </summary>
    ''' <param name="p_oldID">Old example ID that is to be changed.</param>
    ''' <param name="p_newID">New example ID that is to be assigned.</param>
    ''' <remarks></remarks>
    Friend Sub ChangeExampleID(ByVal p_oldID As Integer,
                               ByVal p_newID As Integer)
        Dim models As List(Of cMCModel) = GetExampleModelsOrInnerList(p_oldID)
        For Each model As cMCModel In models
            Dim index As Integer = IndexOf(model.ID.idCompositeDecimal)

            Dim innerModel As cMCModel = DirectCast(InnerList(index), cMCModel)
            Dim oldIDComposite As String = innerModel.ID.idComposite

            ' Update model
            innerModel.ID.idExample = p_newID
            innerModel.changedSinceSave = True

            ' Update IDs
            ReplaceID(oldIDComposite, innerModel.ID.idComposite)
        Next

        UpdatePropertiesSyncIDs(p_oldID, p_newID)
    End Sub
#End Region

#Region "Methods: Private - List"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cMCModel
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cMCModel)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the model control specified by model ID.
    ''' </summary>
    ''' <param name="p_modelID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_modelID As String) As cMCModel
        Try
            If String.IsNullOrWhiteSpace(p_modelID) Then Throw New ArgumentException("{0} is not specified.", p_modelID)
            If p_modelID = "0" Then Throw New ArgumentException("{0} cannot be 0.", p_modelID)
            If Not IsNumeric(myCDbl(p_modelID)) Then Throw New ArgumentException("{0} must be a number corresponding to a model ID.", p_modelID)

            'Try a string match
            For Each item As cMCModel In InnerList
                If item.ID.idComposite = p_modelID Then
                    Return item
                End If
            Next

            'Try converting the string to a decimal
            Dim modelIDDecimal As Decimal = CType(p_modelID, Decimal)
            Return GetItem(modelIDDecimal)

        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the model control specified by model ID.
    ''' </summary>
    ''' <param name="p_modelID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_modelID As Decimal) As cMCModel
        Try
            If p_modelID = 0 Then Throw New ArgumentException("{0} cannot be 0.", p_modelID.ToString)

            'Try converting the string to a decimal
            Dim modelIDDecimal As Decimal = CType(p_modelID, Decimal)
            For Each item As cMCModel In InnerList
                If item.ID.idCompositeDecimal = modelIDDecimal Then
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

    ''' <summary>
    ''' Returns the model control specified by the path object.
    ''' A match is made by the path to the associated model file.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <param name="p_isSource">True: The path used to determine a match is the source path. 
    ''' False: The path used to determine a match is the destination path.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_path As cPath,
                                       Optional ByVal p_isSource As Boolean = False) As cMCModel
        Try
            If p_path Is Nothing Then Throw New ArgumentException("The path provided is 'Nothing'.")
            If String.IsNullOrWhiteSpace(p_path.path) Then Throw New ArgumentException("The path object provided has no path set.")

            If p_isSource Then
                For Each item As cMCModel In InnerList
                    If StringsMatch(item.modelFile.pathSource.path, p_path.path) Then
                        Return item
                    End If
                Next
            Else
                For Each item As cMCModel In InnerList
                    If StringsMatch(item.modelFile.pathDestination.path, p_path.path) Then
                        Return item
                    End If
                Next
            End If

        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Replaces the composite ID from all relevant lists in the object.
    ''' </summary>
    ''' <param name="p_oldIDComposite"></param>
    ''' <param name="p_newIDComposite"></param>
    ''' <remarks></remarks>
    Private Sub ReplaceID(ByVal p_oldIDComposite As String,
                      ByVal p_newIDComposite As String)
        RemoveID(p_oldIDComposite)
        AddID(p_newIDComposite)
    End Sub

    ''' <summary>
    ''' Adds the composite ID to all relevant lists in the object.
    ''' </summary>
    ''' <param name="p_idComposite"></param>
    ''' <remarks></remarks>
    Private Sub AddID(ByVal p_idComposite As String)
        _IDs.Add(p_idComposite)
        _IDs.Sort()
        If Not overWriteExistingIDs Then
            _totalReservedIDs.Add(p_idComposite)
            _totalReservedIDs.Sort()
        End If
    End Sub

    ''' <summary>
    ''' Removes the composite ID from all relevant lists in the object.
    ''' </summary>
    ''' <param name="p_idComposite"></param>
    ''' <remarks></remarks>
    Private Sub RemoveID(ByVal p_idComposite As String)
        _IDs.Remove(p_idComposite)
        If Not overWriteExistingIDs Then _totalReservedIDs.Remove(p_idComposite)
    End Sub

    ''' <summary>
    ''' Updates any example syncing properties related to the model ID.
    ''' </summary>
    ''' <param name="p_oldID">Old example ID.</param>
    ''' <param name="p_newID">New example ID.</param>
    ''' <remarks></remarks>
    Private Sub UpdatePropertiesSyncIDs(ByVal p_oldID As Integer,
                                    ByVal p_newID As Integer)
        If _propertiesSync.Contains(p_oldID) Then
            _propertiesSync.Add(p_newID, _propertiesSync(p_oldID))
            _propertiesSync.Remove(p_oldID)
            _propertiesSync(p_newID).idExample = True
        Else
            _propertiesSync(p_newID, p_createNewEntryIfNotExist:=True).idExample = True
        End If
    End Sub

    ''' <summary>
    ''' Compiles a list of all example IDs from the composite IDs list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExampleIDs() As List(Of Integer)
        Dim exampleIDs As New List(Of Integer)
        For Each ID As String In IDs
            Dim exampleID As Integer = myCInt(GetPrefix(ID, "."))
            If Not exampleIDs.Contains(exampleID) Then exampleIDs.Add(exampleID)
        Next
        exampleIDs.Sort()

        Return exampleIDs
    End Function

    ''' <summary>
    ''' Determines if the provided object is unique to the list of the current list.
    ''' </summary>
    ''' <param name="p_mcModel">Object to check for uniqueness.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MCModelUnique(ByVal p_mcModel As cMCModel) As Boolean
        Dim mcModelIDs As New List(Of String)

        For Each mcModel As cMCModel In InnerList
            mcModelIDs.Add(mcModel.ID.idComposite)
        Next

        'If update entry is unique, add it
        If Not ExistsInListString(p_mcModel.ID.idComposite, mcModelIDs) Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub SortByModelName()
        InnerList.Sort(New cMCModelNameComparer)
    End Sub
    Private Sub SortByExampleName()
        InnerList.Sort(New cMCExampleNameComparer)
    End Sub
    Private Sub SortByID()
        InnerList.Sort(New cMCIDComparer)
    End Sub

    ''' <summary>
    ''' Returns a list of models, either grouped by example ID, or if none is provided, then the InnerList.
    ''' </summary>
    ''' <param name="p_exampleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExampleModelsOrInnerList(ByVal p_exampleID As Integer) As List(Of cMCModel)
        If p_exampleID = 0 Then
            Return ToList()
        Else
            Return GetExampleModels(p_exampleID)
        End If
    End Function
#End Region

#Region "Methods: Private"
    ' Auto Set Example Properties Sync
    Private Sub AutoSetCommon(ByRef p_propertySync As cMCPropsSync,
                              ByVal p_models As List(Of cMCModel))
        If Not p_models.Count > 2 Then Exit Sub
        Dim isMatching As Boolean = True
        For i = 1 To p_models.Count - 1
            'TODO: Consider lumping these into a specialized class that handles all comparing & updating?
            isMatching = (p_models(i - 1).title.Equals(p_models(i).title))
            If Not isMatching Then Exit For
            isMatching = (p_models(i - 1).description.Equals(p_models(i).description))
            If Not isMatching Then Exit For
        Next

        p_propertySync.common = isMatching
    End Sub
    Private Sub AutoSetBenchmarkReference(ByRef p_propertySync As cMCPropsSync,
                                          ByVal p_models As List(Of cMCModel))
        If Not p_models.Count > 2 Then Exit Sub
        Dim isMatching As Boolean = True
        For i = 1 To p_models.Count - 1
            isMatching = (p_models(i - 1).program.Equals(p_models(i).program))
            If Not isMatching Then Exit For
        Next

        p_propertySync.benchmarkReferences = isMatching
    End Sub
    Private Sub AutoSetExcelResult(ByRef p_propertySync As cMCPropsSync,
                                    ByVal p_models As List(Of cMCModel))
        If Not p_models.Count > 2 Then Exit Sub
        Dim isMatching As Boolean = True
        For i = 1 To p_models.Count - 1
            isMatching = (p_models(i - 1).resultsExcel.Equals(p_models(i).resultsExcel))
            If Not isMatching Then Exit For
        Next

        p_propertySync.excelResults = isMatching
    End Sub
    Private Sub AutoSetListObjects(ByRef p_propertySync As cMCPropsSync,
                                   ByVal p_models As List(Of cMCModel))
        If Not p_models.Count > 2 Then Exit Sub
        For i = 1 To p_models.Count - 1
            AutoSetAttachments(p_propertySync, p_models(0).attachments, p_models(i).attachments)
            AutoSetAttachments(p_propertySync, p_models(0).images, p_models(i).images, p_isImage:=True)
            AutoSetLinks(p_propertySync, p_models(0).links, p_models(i).links)
            AutoSetIncidentsTickets(p_propertySync, p_models(0).incidents, p_models(i).incidents)
            AutoSetIncidentsTickets(p_propertySync, p_models(0).tickets, p_models(i).tickets, p_isTicket:=True)
            AutoSetUpdates(p_propertySync, p_models(0).updates, p_models(i).updates)
        Next
    End Sub

    Private Sub AutoSetAttachments(ByRef p_propertySync As cMCPropsSync,
                                   ByVal p_itemsBase As cMCAttachments,
                                   ByVal p_itemsCompare As cMCAttachments,
                                   Optional ByVal p_isImage As Boolean = False)
        Dim comparer As New cMCAttachmentComparer
        For Each item As cFileAttachment In p_itemsCompare
            If p_itemsBase.Contains(item, comparer) Then
                If Not p_isImage Then
                    p_propertySync.attachments.Add(item.title, True)
                Else
                    p_propertySync.images.Add(item.title, True)
                End If
            End If
        Next
    End Sub
    Private Sub AutoSetLinks(ByRef p_propertySync As cMCPropsSync,
                             ByVal p_itemsBase As cMCLinks,
                             ByVal p_itemsCompare As cMCLinks)
        Dim comparer As New cMCLinkComparer
        For Each item As cMCLink In p_itemsCompare
            If p_itemsBase.Contains(item, comparer) Then
                p_propertySync.links.Add(item.title, True)
            End If
        Next
    End Sub
    Private Sub AutoSetIncidentsTickets(ByRef p_propertySync As cMCPropsSync,
                                         ByVal p_itemsBase As cMCIncidentsTickets,
                                         ByVal p_itemsCompare As cMCIncidentsTickets,
                                         Optional ByVal p_isTicket As Boolean = False)
        For Each item As Integer In p_itemsCompare
            If p_itemsBase.Contains(item) Then
                If Not p_isTicket Then
                    p_propertySync.incidents.Add(item.ToString, True)
                Else
                    p_propertySync.tickets.Add(item.ToString, True)
                End If
            End If
        Next
    End Sub
    Private Sub AutoSetUpdates(ByRef p_propertySync As cMCPropsSync,
                               ByVal p_itemsBase As cMCUpdates,
                               ByVal p_itemsCompare As cMCUpdates)
        Dim comparer As New cMCUpdateComparer
        For Each item As cMCUpdate In p_itemsCompare
            If p_itemsBase.Contains(item, comparer) Then
                p_propertySync.updates.Add(item.id.ToString, True)
            End If
        Next
    End Sub

#End Region

#Region "Methods: Private - Excel Results"
    ''' <summary>
    ''' For all examples of a specified status, sets the model control boolean property to 'False' for updating results from Excel.
    ''' </summary>
    ''' <param name="p_exampleStatus">Example status trigger that causes an example to be included (Complete, Edited) or skipped (Incomplete, Edit). 
    ''' Regardless of completion, examples will always be included if this is "Unspecified".</param>
    ''' <remarks></remarks>
    Private Sub SuppressExcelResultsUpdate(ByVal p_exampleStatus As eExampleStatus)
        For Each mcModel As cMCModel In InnerList
            If ExampleStatusValid(p_exampleStatus, mcModel) Then
                mcModel.updateResultsFromExcel = False  'Suppresses individual updates
            End If
        Next
    End Sub

    ''' <summary>
    ''' True: Status of model control object matches that provided.
    ''' </summary>
    ''' <param name="p_exampleStatus">Example status trigger that causes an example to be included (Complete, Edited) or skipped (Incomplete, Edit). 
    ''' Regardless of completion, examples will always be included if this is "Unspecified".</param>
    ''' <param name="p_mcModel">Model Control object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExampleStatusValid(ByVal p_exampleStatus As eExampleStatus,
                                        ByVal p_mcModel As cMCModel) As Boolean
        If (p_mcModel.statusExample = GetEnumDescription(p_exampleStatus) OrElse
            p_exampleStatus = eExampleStatus.Unspecified) Then

            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' For all examples within the parent directories specified, if the example has Excel results noted for a saved Model Control file, update this file to contain the results from the Excel file.
    ''' RegTest will save these under an "internal use" branch of the Model Control file.
    ''' </summary>
    ''' <param name="p_mcXMLPaths">List of directories within which the operation is carried out.</param>
    ''' <remarks></remarks>
    Private Sub AddResultsFromExcelFilesToMCFiles(ByVal p_mcXMLPaths As List(Of String))
        Dim mcXMLPathsExisting As New List(Of String)

        If p_mcXMLPaths Is Nothing Then Exit Sub

        For Each path As String In p_mcXMLPaths
            If IO.Directory.Exists(path) Then mcXMLPathsExisting.Add(path)
        Next
        If mcXMLPathsExisting.Count = 0 Then Exit Sub

        myCsiTester.RunRegTest(eRegTestAction.ResultsUpdateFromExcel, testerSettings.exampleUpdateFile.fileNameWithExtension, , mcXMLPathsExisting)
    End Sub

    ''' <summary>
    ''' For all examples within the parent directories specified, updates the Model Control file of the Excel results and then populates the class with those results.
    ''' </summary>
    ''' <param name="p_mcXMLPaths">List of directories within which the operation is carried out.</param>
    ''' <param name="p_mcModels">Model Control objects to update based on the updated files.</param>
    ''' <remarks></remarks>
    Private Sub UpdateResultsFromExcelFilesToMCFiles(ByVal p_mcXMLPaths As List(Of String),
                                                           ByRef p_mcModels As cMCModels)
        AddResultsFromExcelFilesToMCFiles(p_mcXMLPaths)

        For Each mcModel As cMCModel In p_mcModels
            mcModel.AddExcelResultsFromMCFile()
        Next
    End Sub
#End Region

#Region "Methods: Private - Model IDs"

    ''' <summary>
    ''' Returns the maximum ID in the list of composite IDs.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MaxCompositeID() As String
        If _IDs.Count = 0 Then
            Return "0"
        Else
            Dim sortedModelIDs As New List(Of String)(_IDs.ToList)
            sortedModelIDs.Sort()

            Return sortedModelIDs(_IDs.Count - 1)
        End If
    End Function

    ''' <summary>
    ''' Returns the minimum ID in the list of composite IDs.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MinCompositeID() As String
        If _IDs.Count = 0 Then
            Return "0"
        Else
            Dim sortedModelIDs As New List(Of String)(_IDs.ToList)
            sortedModelIDs.Sort()

            Return sortedModelIDs(0)
        End If
    End Function

    ''' <summary>
    ''' Sets the model ID of the provided model control object to be the next one to occurr in the collection.
    ''' </summary>
    ''' <param name="p_nextModel">Model control object to assign the next model ID to.</param>
    ''' <remarks></remarks>
    Private Sub SetNextID(ByRef p_nextModel As cMCModel)
        Try
            Sort(eSort.ID)

            Dim isFirstModel As Boolean = (InnerList.Count = 0)
            Dim lastModel As cMCModel = GetLastModel()
            Dim lastlID As Decimal = lastModel.ID.idCompositeDecimal
            _latestExampleName = lastModel.ID.exampleName

            SetID(p_nextModel, lastlID, _latestExampleName, isFirstModel)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns the last model in the list of models.
    ''' If the list is empty, an empty model class is generated.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLastModel() As cMCModel
        If InnerList.Count = 0 Then
            Return New cMCModel
        Else
            Return CType(InnerList(InnerList.Count - 1), cMCModel)
        End If
    End Function


    ''' <summary>
    ''' Sets the model ID of the current model control object based on the prior ID information given.
    ''' </summary>
    ''' <param name="p_currentModel">Current model to assign the next ID to.</param>
    ''' <param name="p_lastCompositeID">The last/latest composite ID in the collection. i.e. [Example ID].[Model ID]</param>
    ''' <param name="p_lastExampleName">The last/latest multi-model example name in the collection.</param>
    ''' <param name="p_firstModel">True: The model is the first of a set for a single example.
    ''' Only active if 'multiModelMethod' is 'All'.</param>
    ''' <remarks></remarks>
    Private Sub SetID(ByRef p_currentModel As cMCModel,
                      ByRef p_lastCompositeID As Decimal,
                      ByRef p_lastExampleName As String,
                      Optional ByRef p_firstModel As Boolean = True)
        Try
            Dim currentModelName As String = p_currentModel.modelFile.pathDestination.fileName
            Dim currentMultiModelType As eMultiModelType = MultiModelType(currentModelName, p_lastExampleName, p_firstModel)

            With p_currentModel.ID
                .multiModelType = currentMultiModelType
                .SetExampleName(p_lastExampleName)
                .UpdateSkippedIDs(_totalReservedIDs)
            End With

            p_lastCompositeID = SyncIDWithStartingExampleID(p_lastCompositeID)
            Dim currentCompositeID As Decimal = p_currentModel.ID.IncrementCompositeIDs(p_lastCompositeID)

            ' Validate the current ID
            If (Not _totalReservedIDs.ToList.Contains(CStr(currentCompositeID))) Then
                p_currentModel.ID.idCompositeDecimal = currentCompositeID

                'Add ID to updated list
                If Not _totalReservedIDs.Contains(p_currentModel.ID.idComposite) Then _totalReservedIDs.Add(p_currentModel.ID.idComposite)
                If Not _IDs.Contains(p_currentModel.ID.idComposite) Then _IDs.Add(p_currentModel.ID.idComposite)
            End If

            ' Update for checking continuing example on next iteration
            p_lastExampleName = p_currentModel.ID.exampleName
            p_lastCompositeID = currentCompositeID
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns the composite ID, adjusted for any ID restrictions.
    ''' </summary>
    ''' <param name="p_compositeID">Composite ID to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SyncIDWithStartingExampleID(ByVal p_compositeID As Decimal) As Decimal
        If p_compositeID < myCDbl(startingExampleID) - 1 Then
            Return CDec(startingExampleID) - 1
        Else
            Return p_compositeID
        End If
    End Function

    ''' <summary>
    ''' Determines the multi-model type.
    ''' </summary>
    ''' <param name="p_currentModelName">Name of the model to consider in classification.</param>
    ''' <param name="p_firstModel">True: The model is the first of a set for a single example.
    ''' Only active if 'multiModelMethod' is 'All'.</param>
    ''' <param name="p_lastExampleName">Last example name updated in the models list.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MultiModelType(ByVal p_currentModelName As String,
                                    ByVal p_lastExampleName As String,
                                    Optional ByRef p_firstModel As Boolean = False) As eMultiModelType
        Select Case multiModelMethod
            Case eMultiModelIDNumbering.All
                Return MultiModelTypeSingleGroup(p_firstModel)
            Case eMultiModelIDNumbering.Auto
                Return MultiModelTypeByFileName(p_currentModelName, p_lastExampleName)
            Case Else
                Return eMultiModelType.singleModel
        End Select
    End Function

    ''' <summary>
    ''' Determines which enumeration type the example/model is, based on the assumption that all models are part of one group.
    ''' </summary>
    ''' <param name="p_firstModel">True: This is the first model being considered out of a single series.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MultiModelTypeSingleGroup(ByRef p_firstModel As Boolean) As eMultiModelType
        If p_firstModel Then
            p_firstModel = False
            Return eMultiModelType.starting
        Else
            Return eMultiModelType.continuing
        End If
    End Function

    ''' <summary>
    ''' Sets the total list of model IDs to skip based on the overwrite setting.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTotalSkippedIDs()
        _totalReservedIDs = skippedIDsList
        If Not _overWriteExistingIDs Then
            AddCurrentIDsToSkippedIDs()
        End If
        _totalReservedIDs.Sort()
    End Sub

    ''' <summary>
    ''' Converts the list of all IDs to be reserved from a list of strings to a list of decimals.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertTotalReservedIDs() As List(Of Decimal)
        Dim idsSkipped As New List(Of Decimal)
        For Each id As String In _totalReservedIDs
            idsSkipped.Add(myCDec(id))
        Next

        Return idsSkipped
    End Function

    ''' <summary>
    ''' Returns a list of all reserved example IDs based on the IDs in the total list of reserved IDs.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertTotalReservedIDsToExampleIDs() As List(Of Integer)
        Dim idsSkipped As New List(Of Integer)
        For Each id As String In _totalReservedIDs
            Dim exampleID As Integer = CInt(GetPrefix(id, "."))
            If Not idsSkipped.Contains(exampleID) Then idsSkipped.Add(exampleID)
        Next

        Return idsSkipped
    End Function

    ''' <summary>
    ''' Adds additional model IDs to skip based on IDs listed in existing XML files.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddCurrentIDsToSkippedIDs()
        For Each mcModel As cMCModel In InnerList
            If mcModel.ID.idCompositeDecimal > 0 Then
                _totalReservedIDs.Add(mcModel.ID.idComposite)
            End If
        Next
        _totalReservedIDs.Sort()
    End Sub
#End Region

End Class
