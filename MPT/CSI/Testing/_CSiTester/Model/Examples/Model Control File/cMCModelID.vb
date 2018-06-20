Option Explicit On
Option Strict On

Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

''' <summary>
''' Handles storing and altering the example and model IDs.
''' </summary>
''' <remarks></remarks>
Public Class cMCModelID
    Inherits PropertyChanger
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Enumerations"
    ''' <summary>
    ''' Setting how to include multi-model cases when generating examples.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eMultiModelIDNumbering
        ''' <summary>
        ''' Every model control is assumed to be part of a unique example group..
        ''' </summary>
        ''' <remarks></remarks>
        <Description("None")> None = 0
        ''' <summary>
        ''' Every model control is assumed to be part of the same example group..
        ''' </summary>
        ''' <remarks></remarks>
        <Description("All")> All
        ''' <summary>
        ''' Each model control is checked to see if it is part of a new or existing example group.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Auto")> Auto
    End Enum

    ''' <summary>
    ''' Classifies the example as to the group type and position in the group by naming convention.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eMultiModelType
        ''' <summary>
        ''' Model is not part of a group of models by naming convention.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Single Model")> singleModel
        ''' <summary>
        ''' Model is the first model listed by name for the group.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Starting Model")> starting
        ''' <summary>
        ''' Model is part of a group, but it is not the first one listed by name for the group.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Continuing Model")> continuing
    End Enum
#End Region

#Region "Constants"
    Friend Const MAX_EXAMPLE_ID As Integer = 9999
    Friend Const MAX_MODEL_ID As Integer = 99

    Friend Const STARTING_LETTER As String = "a"
    Friend Const STARTING_NUMBER_ZERO As String = "0"
    Friend Const STARTING_NUMBER_ONE As String = "1"
    Friend Const INDICATOR_DASH As String = "-"
    Friend Const INDICATOR_DECIMAL As String = "."
#End Region

#Region "Variables"
    Private _mcModel As cMCModel = Nothing

#End Region

#Region "Properties"
    ''' <summary>
    ''' A copy of the currently referenced model control object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property mcModel As cMCModel
        Get
            Return CType(_mcModel.Clone, cMCModel)
        End Get
    End Property

    Private _multiModelType As eMultiModelType = eMultiModelType.singleModel
    ''' <summary>
    ''' Indicates the model group type of the model id.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property multiModelType As eMultiModelType
        Get
            Return _multiModelType
        End Get
        Set(value As eMultiModelType)
            If Not _multiModelType = value Then
                _multiModelType = value
                RaisePropertyChanged(Function() Me.multiModelType)

                ' Reset model ID if the example is reverted to a single model type from another type
                If _multiModelType = eMultiModelType.singleModel Then
                    _idModel = -1
                    RaisePropertyChanged(Function() Me.idModel)

                    UpdateCompositeID()
                End If
            End If
        End Set
    End Property

    Private _currentExampleName As String
    ''' <summary>
    ''' Shared name of a current example for a multi-model example. e.g. "Example 10a", this would be "Example 10".
    ''' </summary>
    ''' <remarks></remarks>
    Friend Property exampleName As String
        Get
            Return _currentExampleName
        End Get
        Set(value As String)
            If Not _currentExampleName = value Then
                _currentExampleName = value
                RaisePropertyChanged(Function() Me.exampleName)
            End If
        End Set
    End Property

    Private _skippedCompositeIDDecimal As New List(Of Decimal)
    Private _skippedCompositeIDs As New cObsColUniqueString
    ''' <summary>
    ''' ID numbers to be skipped when auto-generating unique model ID numbers associated with the examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property skippedCompositeIDs As cObsColUniqueString
        Get
            Return _skippedCompositeIDs
        End Get
    End Property

    Private _idComposite As String = "0"
    ''' <summary>
    ''' The combined [exampleID].[modelID] of the model control object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property idComposite As String
        Get
            Return _idComposite
        End Get
        Set(value As String)
            If (IsNumeric(value) AndAlso
                Not _idComposite = value) Then
                _idComposite = value
                RaisePropertyChanged(Function() Me.idComposite)

                UpdateComponentIDs()
            End If
        End Set
    End Property

    ''' <summary>
    ''' The combined [exampleID].[modelID] of the model control object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property idCompositeDecimal As Decimal
        Get
            Return ConvertIDComponentsToComposite()
        End Get
        Set(value As Decimal)
            UpdateComponentIDs(value)
        End Set
    End Property

    ''' <summary>
    ''' ID correspodning to the example, in a padded string format used in labels, such as folder names.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property idExampleLabel As String
        Get
            Return "id" & CStr(idExample).PadLeft(CStr(MAX_EXAMPLE_ID).Length, CChar("0"))
        End Get
    End Property

    Private _idExample As Integer = 0
    ''' <summary>
    ''' ID corresponding to the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property idExample As Integer
        Get
            Return _idExample
        End Get
        Set(value As Integer)
            If (Not _idExample = value AndAlso
                value >= 0) Then

                _idExample = value
                RaisePropertyChanged(Function() Me.idExample)

                UpdateCompositeID()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Initial value indicates that the property hasn't been explicitly set.
    ''' </summary>
    ''' <remarks></remarks>
    Private _idModel As Integer = -1
    ''' <summary>
    ''' ID corresponding to the model file in the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property idModel As Integer
        Get
            If _idModel < 0 Then
                Return 0
            Else
                Return _idModel
            End If
        End Get
        Set(value As Integer)
            If (Not _idModel = value AndAlso
                0 <= value AndAlso value <= MAX_MODEL_ID) Then

                _idModel = value
                RaisePropertyChanged(Function() Me.idModel)

                UpdateCompositeID()
                SetMultiModelTypeByModelID()
            End If
        End Set
    End Property
#End Region

#Region "Initialization"

    Friend Sub New(Optional ByVal p_bindTo As cMCModel = Nothing)
        If p_bindTo IsNot Nothing Then Bind(p_bindTo)
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
        Dim myClone As New cMCModelID

        With myClone
            ' Passing reference instead of cloning to maintain reference. DO NOT make a copy!
            If p_bindTo Is Nothing Then
                ' Maintain current reference
                ._mcModel = _mcModel
            Else
                ' Shift reference to newly specified model control object.
                ._mcModel = p_bindTo
            End If

            ._idComposite = _idComposite
            ._currentExampleName = exampleName
            ._idExample = _idExample
            ._idModel = _idModel

            ._multiModelType = multiModelType
            ._skippedCompositeIDs = skippedCompositeIDs
            ._skippedCompositeIDDecimal = _skippedCompositeIDDecimal
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
        If Not (TypeOf p_object Is cMCModelID) Then Return False
        Dim comparedObject As cMCModelID = TryCast(p_object, cMCModelID)
        Dim isMatching As Boolean = False

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not ._idComposite = idComposite Then Return False
            If Not ._currentExampleName = exampleName Then Return False
            If Not ._multiModelType = multiModelType Then Return False

            For Each itemOuter As String In skippedCompositeIDs
                isMatching = False
                For Each itemInner As String In .skippedCompositeIDs
                    If itemOuter = itemInner Then
                        isMatching = True
                        Exit For
                    End If
                Next
                If Not isMatching Then Return False
            Next
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend Shared"
    ''' <summary>
    ''' If 'True', then the model of the specified name is the first model in a group.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the model file.</param>
    ''' <param name="p_isAdjectival">True: ID is part of a multi-model example distinguished solely by a non-numeric description (e.g. 4-001-comp, 4-001-incomp, etc.).</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function IsStartingMultiModel(ByVal p_modelFileName As String,
                                                Optional ByVal p_isAdjectival As Boolean = True) As Boolean
        If String.IsNullOrWhiteSpace(p_modelFileName) Then Return False

        p_modelFileName = FileNameWithoutExtension(p_modelFileName)

        Dim precedingCharacter As String = ""
        Dim currentCharacter As String = ""
        Dim p_isStartingNumberZero As Boolean = True
        For currentCharacterIndex = 1 To Len(p_modelFileName)
            precedingCharacter = currentCharacter
            currentCharacter = Mid(p_modelFileName, currentCharacterIndex, 1)

            If IsStartingMultiModel(precedingCharacter, currentCharacter, p_isStartingNumberZero) Then
                Return True
            End If
        Next

        precedingCharacter = ""
        currentCharacter = ""
        Dim nextCharacter = ""
        If (p_isAdjectival AndAlso
            Len(p_modelFileName) > 2) Then
            ' Filename is searched backwards in order to find the pattern of the last occurrence of [number]-[letter].
            For currentCharacterIndex = Len(p_modelFileName) To 2 Step -1
                precedingCharacter = currentCharacter
                currentCharacter = Mid(p_modelFileName, currentCharacterIndex, 1)
                nextCharacter = Mid(p_modelFileName, currentCharacterIndex - 1, 1)

                If IsStartingMultiModelAdjectival(precedingCharacter, currentCharacter, nextCharacter) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    ''' <summary>
    ''' Determines if the current model ID is a continuing ID based on the group names.
    ''' </summary>
    ''' <param name="p_priorID">Model ID object associated with the most recent model control object in the ID sequence.</param>
    ''' <param name="p_currentID">The model ID object associated with the current model control object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function IsContinuingExampleName(ByVal p_priorID As cMCModelID,
                                                   ByVal p_currentID As cMCModelID) As Boolean
        If p_priorID.exampleName = p_currentID.exampleName Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Determines which enumeration type the example/model is, based on the composite ID.
    ''' </summary>
    ''' <param name="p_exampleCompositeID">Example.Model ID to base the classification on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function MultiModelTypeByID(ByVal p_exampleCompositeID As String) As eMultiModelType
        Dim subID As String = GetSuffix(p_exampleCompositeID, ".")
        If subID = p_exampleCompositeID Then
            Return eMultiModelType.singleModel
        ElseIf myCDbl(subID) = 0 Then
            Return eMultiModelType.starting
        Else
            Return eMultiModelType.continuing
        End If
    End Function
    ''' <summary>
    ''' Determines which enumeration type the example/model is, based on next model file name and current example name. 
    ''' </summary>
    ''' <param name="p_nextModelFileName">Model file name to base the type classification on.</param>
    ''' <param name="p_currentExampleName">Name of the current example group to base classification on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function MultiModelTypeByFileName(ByVal p_nextModelFileName As String,
                                                    ByVal p_currentExampleName As String) As eMultiModelType
        If IsMultiModelOfCurrentGroup(p_nextModelFileName, p_currentExampleName) Then
            Return eMultiModelType.continuing
        ElseIf IsStartingMultiModel(p_nextModelFileName) Then
            Return eMultiModelType.starting
        Else
            Return eMultiModelType.singleModel
        End If
    End Function

    ''' <summary>
    ''' Determines the example name of a set of model files. 
    ''' If the filename is not indicative of starting a new example, then the full filename is used.
    ''' </summary>
    ''' <param name="p_fileNameFirstExampleModel">File name of the model that is the first one of an example.</param>
    ''' <param name="p_isAdjectival">True: ID is part of a multi-model example distinguished solely by a non-numeric description (e.g. 4-001-comp, 4-001-incomp, etc.).</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ExampleNameByFileName(ByVal p_fileNameFirstExampleModel As String,
                                                 Optional ByVal p_isAdjectival As Boolean = True) As String
        p_fileNameFirstExampleModel = FileNameWithoutExtension(p_fileNameFirstExampleModel)

        Dim precedingCharacter As String = ""
        Dim currentCharacter As String = ""
        Dim isStartingNumberZero As Boolean = True
        Dim setExampleName As Boolean = False
        Dim exampleNameMaxIndex As Integer = 0
        For currentCharacterIndex = 1 To Len(p_fileNameFirstExampleModel)
            precedingCharacter = currentCharacter
            currentCharacter = Mid(p_fileNameFirstExampleModel, currentCharacterIndex, 1)

            If IsStartingMultiModel(precedingCharacter, currentCharacter, isStartingNumberZero) Then
                setExampleName = True
                exampleNameMaxIndex = currentCharacterIndex
                Exit For
            End If
        Next

        ' If example name is not yet set for multi-model, but is possibly adjectival, do another check.
        precedingCharacter = ""
        currentCharacter = ""
        Dim nextCharacter = ""
        If (p_isAdjectival AndAlso
            Not setExampleName AndAlso
            Len(p_fileNameFirstExampleModel) > 2) Then
            ' Filename is searched backwards in order to find the pattern of the last occurrence of [number]-[letter].
            For currentCharacterIndex = Len(p_fileNameFirstExampleModel) To 2 Step -1
                precedingCharacter = currentCharacter
                currentCharacter = Mid(p_fileNameFirstExampleModel, currentCharacterIndex, 1)
                nextCharacter = Mid(p_fileNameFirstExampleModel, currentCharacterIndex - 1, 1)

                If (IsStartingMultiModelAdjectival(precedingCharacter, currentCharacter, nextCharacter) AndAlso
                    currentCharacterIndex > exampleNameMaxIndex) Then
                    setExampleName = True
                    exampleNameMaxIndex = currentCharacterIndex
                    Exit For
                End If
            Next
        End If

        Dim currentExampleName As String = ""
        If setExampleName Then
            currentExampleName = ExampleNameAssumed(p_fileNameFirstExampleModel, exampleNameMaxIndex)
        End If

        Return currentExampleName
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets a reference to the provided model control object. 
    ''' One effect is the current multi-model group name is permanently linked with this object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend Sub Bind(ByVal p_mcModel As cMCModel)
        _mcModel = p_mcModel

        SetMultiModelTypeByFileName(_mcModel.modelFile.pathDestination.fileNameWithExtension, p_overrideIDClassification:=True)
    End Sub

    ''' <summary>
    ''' Increments the model ID count for the current model in a series of models being created.
    ''' </summary>
    ''' <param name="p_currentCompositeID">Full ID of the current model.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IncrementCompositeIDs(ByVal p_currentCompositeID As Decimal) As Decimal
        Select Case multiModelType
            Case eMultiModelType.starting
                Return IncrementCompositeID(p_currentCompositeID, p_isNewExample:=True)
            Case eMultiModelType.continuing
                Return IncrementCompositeID(p_currentCompositeID, p_isNewExample:=False)
            Case eMultiModelType.singleModel
                Return IncrementExampleID(p_currentCompositeID)
            Case Else : Return CDec(p_currentCompositeID)
        End Select
    End Function

    ''' <summary>
    ''' Sets the example name based on the state of the object and the provided example name.
    ''' </summary>
    ''' <param name="p_continuingExampleName">Only used if the model type is of a continuing example. 
    ''' This is the name of the example group that the ID is continuing. </param>
    ''' <remarks></remarks>
    Friend Sub SetExampleName(Optional ByVal p_continuingExampleName As String = "")
        Dim modelFileName As String = ""
        If _mcModel IsNot Nothing Then modelFileName = _mcModel.modelFile.pathDestination.fileName

        Select Case multiModelType
            Case eMultiModelType.starting
                exampleName = ExampleNameByFileName(modelFileName)
            Case eMultiModelType.continuing
                exampleName = p_continuingExampleName
            Case eMultiModelType.singleModel
                exampleName = modelFileName
        End Select
    End Sub

    ''' <summary>
    ''' Sets and sorts the string and decimal lists of composite IDs to skip.
    ''' </summary>
    ''' <param name="p_skippedList">List of IDs to skip. 
    ''' To indicate model IDs can be appended to reserved example IDs, preserve the 0 padding. 
    ''' e.g. 1.00 indicates that 1.00 is reserved, but 1.01 is not. However, 1 indicates that all 1.[modelIDs] are taken.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateSkippedIDs(ByVal p_skippedList As cObsColUniqueString)
        If p_skippedList IsNot Nothing Then
            _skippedCompositeIDs = p_skippedList
            _skippedCompositeIDs.Sort()

            _skippedCompositeIDDecimal.Clear()
            For Each compositeID As String In _skippedCompositeIDs
                _skippedCompositeIDDecimal.Add(myCDec(compositeID))
            Next
            _skippedCompositeIDDecimal.Sort()
        End If
    End Sub
#End Region

#Region "Methods: Private"
    ' Converting
    ''' <summary>
    ''' Returns the composite ID based on the current component IDs.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertIDComponentsToComposite() As Decimal
        Return ConvertIDComponentsToComposite(idExample, idModel)
    End Function
    ''' <summary>
    ''' Returns the composite ID based on the provided component IDs.
    ''' </summary>
    ''' <param name="p_exampleID"></param>
    ''' <param name="p_modelID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertIDComponentsToComposite(ByVal p_exampleID As Integer,
                                                    ByVal p_modelID As Integer) As Decimal
        Return CDec(p_exampleID + p_modelID / (Math.Max(1, 10 ^ (CStr(MAX_MODEL_ID).Length))))
    End Function

    ''' <summary>
    ''' Returns the example ID portion of the composite ID as an integer.
    ''' </summary>
    ''' <param name="p_compositeID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertCompositeToExampleID(ByVal p_compositeID As Decimal) As Integer
        Return CInt(Math.Floor(p_compositeID))
    End Function

    ''' <summary>
    ''' Returns the model ID portion of the composite ID as an integer.
    ''' </summary>
    ''' <param name="p_compositeID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertCompositeToModelID(ByVal p_compositeID As Decimal) As Integer
        Dim exampleID As Integer = ConvertCompositeToExampleID(p_compositeID)
        If ((p_compositeID - exampleID) = 0 AndAlso
            _multiModelType = eMultiModelType.singleModel) Then
            Return -1
        Else
            Return CInt((p_compositeID - exampleID) * (10 ^ (CStr(MAX_MODEL_ID).Length)))
        End If
    End Function

    ' Cascading Updates
    ''' <summary>
    ''' Updates the composite ID based on the current component example and model IDs.
    ''' Padded zeros are only added to a model ID of 0 if the example is a multimodel example.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCompositeID()
        Dim exampleID As String = CStr(idExample)
        Dim modelID As String

        If (_idModel < 0 OrElse multiModelType = eMultiModelType.singleModel) Then
            modelID = ""
        Else
            modelID = "." & CStr(idModel).PadLeft(CStr(MAX_MODEL_ID).Length, CChar("0"))
        End If

        Dim compositeID As String = exampleID & modelID

        If Not _idComposite = compositeID Then
            _idComposite = compositeID
            RaisePropertyChanged(Function() Me.idComposite)
        End If
    End Sub

    ''' <summary>
    ''' Updates the example ID and model ID components based on the current composite ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateComponentIDs()
        If Not IsNumeric(idComposite) Then Exit Sub

        Dim exampleID As Integer = myCInt(GetPrefix(idComposite, "."))
        If Not _idExample = exampleID Then
            _idExample = exampleID
            RaisePropertyChanged(Function() Me.idExample)
        End If

        If Not StringExistInName(idComposite, ".") Then Exit Sub

        Dim modelID As Integer = myCInt(GetSuffix(idComposite, "."))
        If Not _idModel = modelID Then
            _idModel = modelID
            RaisePropertyChanged(Function() Me.idModel)

            SetMultiModelTypeByModelID()
        End If
    End Sub
    ''' <summary>
    ''' Updates the example ID and model ID components based on the provided composite ID.
    ''' </summary>
    ''' <param name="p_compositeID">Composite ID to break down into integer components.</param>
    ''' <remarks></remarks>
    Private Sub UpdateComponentIDs(ByVal p_compositeID As Decimal)
        idExample = ConvertCompositeToExampleID(p_compositeID)
        idModel = ConvertCompositeToModelID(p_compositeID)
    End Sub

    ' Classification
    ''' <summary>
    ''' Determines if the current model is a member of the last recorded model group.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the model file.</param>
    ''' <param name="p_currentMultiModelGroupName">Name of the current example group.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsMultiModelOfCurrentGroup(ByVal p_modelFileName As String,
                                                       ByVal p_currentMultiModelGroupName As String) As Boolean
        Return StringExistInName(p_modelFileName, p_currentMultiModelGroupName)
    End Function

    ' Incrementing
    ''' <summary>
    ''' Generates the next model ID to be used. 
    ''' Also increments the example ID if starting a new example.
    ''' </summary>
    ''' <param name="p_currentCompositeID">Current ID to be incremented.</param>
    ''' <param name="p_isNewExample">True: Model is the first model of a new example.</param>
    ''' <remarks></remarks>
    Private Function IncrementCompositeID(ByVal p_currentCompositeID As Decimal,
                                          ByVal p_isNewExample As Boolean) As Decimal
        Dim modelID As Integer = 0
        Dim exampleID As Integer

        'Check if this is the first Sub ID in the count for a group
        If p_isNewExample Then
            exampleID = IncrementExampleID(p_currentCompositeID)
        Else
            exampleID = ConvertCompositeToExampleID(p_currentCompositeID)
            Try
                modelID = IncrementModelID(p_currentCompositeID, p_suppressIDException:=True)
            Catch ex As ModelIDException
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Exclamation),
                                            ex.Message & Environment.NewLine & Environment.NewLine &
                                            "Should the Example ID be incremented instead?",
                                            "Reserved Example ID")
                    Case eMessageActions.Yes
                        modelID = 0
                        exampleID = IncrementExampleID(p_currentCompositeID)
                    Case eMessageActions.No
                        ' No action taken
                End Select
            End Try
        End If

        'Append model group ID to new Sub ID
        Return ConvertIDComponentsToComposite(exampleID, modelID)
    End Function

    ''' <summary>
    ''' Generates the next example ID to be used, accounting for composite IDs set to be skipped. 
    ''' Only returns the example ID portion of the composite ID.
    ''' </summary>
    ''' <param name="p_currentCompositeID">Current ID to be incremented.</param>
    ''' <remarks></remarks>
    Private Function IncrementExampleID(ByVal p_currentCompositeID As Decimal) As Integer
        Dim nextExampleID As Integer = ConvertCompositeToExampleID(p_currentCompositeID) + 1

        'Check if model ID is specified to be skipped & if so, repeat incrementing (compared list assumed to be sorted)
        For Each skippedCompositeID As Decimal In _skippedCompositeIDDecimal
            Dim skippedExampleID As Integer = ConvertCompositeToExampleID(skippedCompositeID)
            If skippedExampleID = nextExampleID Then nextExampleID += 1
        Next

        Return nextExampleID
    End Function

    ''' <summary>
    ''' Generates the next model ID to be used, accounting for composite IDs set to be skipped.
    ''' Only returns the model ID portion of the composite ID.
    ''' </summary>
    ''' <param name="p_currentCompositeID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IncrementModelID(ByVal p_currentCompositeID As Decimal,
                                      Optional ByVal p_suppressIDException As Boolean = False) As Integer
        Try
            Dim currentExampleID As Integer = ConvertCompositeToExampleID(p_currentCompositeID)
            Dim nextModelID As Integer = ConvertCompositeToModelID(p_currentCompositeID) + 1
            Dim exampleIDMatchingAndSingular As Boolean = False

            'Check if model ID is specified to be skipped & if so, repeat incrementing (compared list assumed to be sorted)
            For i = 0 To _skippedCompositeIDDecimal.Count - 1
                exampleIDMatchingAndSingular = False

                Dim skippedCompositeID As Decimal = _skippedCompositeIDDecimal(i)
                Dim skippedExampleID As Integer = ConvertCompositeToExampleID(skippedCompositeID)
                Dim skippedModelID As Integer = ConvertCompositeToModelID(skippedCompositeID)

                ' Consider case of the example ID existing in the skipped list, but only for certain benchmarks
                ' A singular example ID indicates that all associated model IDs should be skipped. 
                ' Otherwise, the benchmark ID can be incremented based off of the skipped model IDs.
                If skippedExampleID = currentExampleID Then
                    If skippedModelID = 0 Then
                        ' Possible first example in a multi-model
                        ' Check the corresponding string list, as a singular model is represented as 1, while a multimodel is written as 1.[padded zeros]
                        Dim modelIDPadded As String = ""
                        If GetSuffix(_skippedCompositeIDs(i), ".") = modelIDPadded.PadLeft(CStr(MAX_MODEL_ID).Length, CChar("0")) Then
                            exampleIDMatchingAndSingular = False
                        ElseIf (_skippedCompositeIDDecimal.Count > (i + 1)) Then
                            ' Check to see if the example ID occurs again
                            Dim nextSkippedExampleID As Integer = ConvertCompositeToExampleID(_skippedCompositeIDDecimal(i + 1))
                            exampleIDMatchingAndSingular = (Not nextSkippedExampleID = currentExampleID)
                        End If
                    End If
                End If

                If (Not exampleIDMatchingAndSingular AndAlso
                    skippedExampleID = currentExampleID AndAlso
                    skippedModelID = nextModelID) Then

                    nextModelID += 1
                ElseIf (exampleIDMatchingAndSingular AndAlso
                        skippedModelID = nextModelID) Then
                    ' Model ID should not be incremented.
                    Throw New ModelIDException("The model ID corresponds to an example ID set that is set to be skipped: " & p_currentCompositeID & Environment.NewLine &
                                               "The model ID will not be incremented.")
                End If
            Next

            Return nextModelID
        Catch ex As ModelIDException
            If Not p_suppressIDException Then
                RaiseEvent Log(New LoggerEventArgs(ex))
            Else
                Throw New ModelIDException(ex.Message)
            End If
        End Try
        Return -1
    End Function

    ' Update class
    ''' <summary>
    ''' Sets the multi-model type based on the composite ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMultiModelTypeByCompositeID()
        _multiModelType = MultiModelTypeByID(idComposite)
        RaisePropertyChanged(Function() Me.multiModelType)
    End Sub

    ''' <summary>
    ''' Sets which enumeration type the example/model is, based on model ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMultiModelTypeByModelID()
        If _idModel < 0 Then
            _multiModelType = eMultiModelType.singleModel
        ElseIf _idModel = 0 Then
            _multiModelType = eMultiModelType.starting
        Else
            _multiModelType = eMultiModelType.continuing
        End If

        RaisePropertyChanged(Function() Me.multiModelType)
    End Sub

    ''' <summary>
    ''' Sets the multi-model type based on the provided filename and other parameters.
    ''' </summary>
    ''' <param name="p_modelFileName">Model file name to base the type classification on.</param>
    ''' <param name="p_priorMCModelID">Model ID object of the model prior to the current model ID. 
    ''' If not provided, or if the ID number is greater than the current model, then the 'continuing' type will not be considered.</param>
    ''' <param name="p_overrideIDClassification">True: Value will be set by name even if already set by current ID. False: Value will not be changed.</param>
    ''' <remarks></remarks>
    Private Sub SetMultiModelTypeByFileName(ByVal p_modelFileName As String,
                                           Optional ByVal p_overrideIDClassification As Boolean = False,
                                           Optional ByVal p_priorMCModelID As cMCModelID = Nothing)
        'Do not change the current type if an ID has already been set
        If (Not p_overrideIDClassification AndAlso
            Not idCompositeDecimal = 0) Then Exit Sub

        Dim lastExampleName As String
        If p_priorMCModelID IsNot Nothing Then
            lastExampleName = p_priorMCModelID.exampleName
        Else
            lastExampleName = exampleName
        End If

        Dim tempModelType As eMultiModelType = MultiModelTypeByFileName(p_modelFileName, lastExampleName)
        If Not _multiModelType = tempModelType Then
            _multiModelType = tempModelType
            RaisePropertyChanged(Function() Me.multiModelType)
        End If
    End Sub
#End Region

#Region "Methods: Private Shared"
    ''' <summary>
    ''' Determines an assumed group name of a set of model files based on the file name and a specified character index.
    ''' The name returned is dependent on the index specified and there might be several results, with the correct result coming from the highest index number if checking the name left-to-right.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the model file to use to formulate an assumed group name.</param>
    ''' <param name="p_currentCharacterIndex">Character index to use to formulate an assumed group name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ExampleNameAssumed(ByVal p_modelFileName As String,
                                               ByVal p_currentCharacterIndex As Integer) As String
        Dim numCharacters As Integer
        'Check to preserve 'Example 1' from 'Example 1a'
        If IsNumeric(Right(Left(p_modelFileName, p_currentCharacterIndex - 1), 1)) Then
            numCharacters = p_currentCharacterIndex - 1
        Else
            numCharacters = p_currentCharacterIndex - 2
        End If

        If (0 <= numCharacters AndAlso numCharacters <= p_modelFileName.Length) Then
            Return Trim(Left(p_modelFileName, numCharacters))
        Else
            Return p_modelFileName
        End If
    End Function

    ''' <summary>
    ''' True: Model is determined to be the first model of a group of models by comparing two neighboring text characters from the file name.
    ''' </summary>
    ''' <param name="p_precedingCharacter">Preceding text character to check in relation to the current character.</param>
    ''' <param name="p_currentCharacter">Current text characted being checked.</param>
    ''' <param name="p_isStartingNumberZero">In cases where a model id starts with 0 (e.g. 5.0 = True), this flag is used to avoid double counting the first model of an example (e.g. 5.0 vs. 5.1).</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsStartingMultiModel(ByVal p_precedingCharacter As String,
                                                 ByVal p_currentCharacter As String,
                                                 ByRef p_isStartingNumberZero As Boolean) As Boolean
        Dim possibleMultiModelExample As Boolean = False

        If p_currentCharacter = STARTING_LETTER Then
            If (IsNumeric(p_precedingCharacter) OrElse
                p_precedingCharacter = INDICATOR_DASH OrElse
                p_precedingCharacter = INDICATOR_DECIMAL) Then

                'e.g. Example 5a, Example 5-a, Example 5.a
                possibleMultiModelExample = True
            End If
        ElseIf p_precedingCharacter = INDICATOR_DECIMAL Then
            If p_currentCharacter = STARTING_NUMBER_ZERO Then
                'e.g. Example 5.0
                possibleMultiModelExample = True
                p_isStartingNumberZero = True
            ElseIf p_currentCharacter = STARTING_NUMBER_ONE Then
                'e.g. Example 5.1
                If Not p_isStartingNumberZero Then possibleMultiModelExample = True
                p_isStartingNumberZero = False
            End If
        End If

        Return possibleMultiModelExample
    End Function

    ''' <summary>
    ''' True: Model is determined to be the first model of a group of models by comparing two neighboring text characters from the file name.
    ''' Pattern checked is in the form of '[name][number]-[adjectival]', or more concisely '[number]-[letter]', reading right-to-left.
    ''' </summary>
    ''' <param name="p_precedingCharacter">Preceding text character to check in relation to the current character.</param>
    ''' <param name="p_currentCharacter">Current text characted being checked.</param>
    ''' <param name="p_nextCharacter">Next text character being checked in relation to the current character.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsStartingMultiModelAdjectival(ByVal p_precedingCharacter As String,
                                                           ByVal p_currentCharacter As String,
                                                           ByVal p_nextCharacter As String) As Boolean
        Dim possibleMultiModelExample As Boolean = False

        If (p_currentCharacter = INDICATOR_DASH AndAlso
            IsNumeric(p_nextCharacter) AndAlso
            Not IsNumeric(p_precedingCharacter)) Then

            'e.g. Example 4-001-comp, Example 4-001-incomp, ...
            possibleMultiModelExample = True
        End If

        Return possibleMultiModelExample
    End Function
#End Region

End Class
