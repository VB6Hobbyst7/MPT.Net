Option Strict On
Option Explicit On

Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting

Friend Class cMCIDGenerator
    Inherits PropertyChanger
    Implements IMessengerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Prompts"
    Private Const _TITLE_ENTER_INTEGER As String = "Invalid Type"
    Private Const _PROMPT_ENTER_INTEGER As String = "Please provide an integer value."

    Private Const _TITLE_GREATER_THAN_ZERO As String = "Invalid Value"
    Private Const _PROMPT_GREATER_THAN_ZERO As String = "Please enter a value greater than zero."
#End Region

#Region "Properties"
    Private _startingModelID As String = "1"
    ''' <summary>
    ''' ID to be used for the first example generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property startingModelID As String
        Set(ByVal value As String)
            If (Not _startingModelID = value AndAlso
                ValidateStartingModelID(value)) Then
                _startingModelID = value
                RaisePropertyChanged("startingModelID")
            End If
        End Set
        Get
            Return _startingModelID
        End Get
    End Property

    ''' <summary>
    ''' IDs skipped during the generation of a series of examples.
    ''' This includes invalid IDs, reserved IDs, and IDs recently generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property skippedModelIDsList As New cObsColUniqueString

    ''' <summary>
    ''' IDs that are considered invalid for any model control object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property invalidModelIDs As New List(Of Decimal)

    ''' <summary>
    ''' IDs that are already being used or reserved for other model control files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property reservedModelIDs As New List(Of Decimal)
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Populates the list of reserved IDs.
    ''' </summary>
    ''' <param name="p_skippedModelIDs">List of model IDs to skip, separated by delimeters.</param>
    ''' <remarks></remarks>
    Friend Sub SetReservedIDs(ByVal p_skippedModelIDs As String)
        Dim skippedList As IList(Of String) = skippedModelIDsList.ToList()
        ParseListString(skippedList, p_skippedModelIDs, False)
        skippedModelIDsList.Replace(skippedList)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Validates that the starting model ID is an integer.
    ''' </summary>
    ''' <param name="p_startingID">Starting ID to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateStartingModelID(ByVal p_startingID As String) As Boolean
        'Check if any non-numeric entries exist in field
        Dim valueNumeric As Boolean = True
        For i = 1 To Len(p_startingID)
            If Not IsNumeric(Mid(p_startingID, i, 1)) Then
                valueNumeric = False
                Exit For
            End If
        Next

        ' Validate that the value is numeric and an integer
        If Not (String.IsNullOrWhiteSpace(p_startingID) OrElse
            (valueNumeric AndAlso Not StringExistInName(p_startingID, "."))) Then

            RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_ENTER_INTEGER,
                                                        _TITLE_ENTER_INTEGER))
            Return False
        End If

        ' Validate that the value is greater than 0
        Dim startingID As Integer = CInt(p_startingID)
        If startingID < 1 Then
            RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_GREATER_THAN_ZERO,
                                                        _TITLE_GREATER_THAN_ZERO))
            Return False
        End If

        Return True
    End Function
#End Region

End Class
