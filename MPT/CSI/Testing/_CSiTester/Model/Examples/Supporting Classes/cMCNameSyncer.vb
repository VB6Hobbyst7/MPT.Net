Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.PropertyChanger

''' <summary>
''' Manages the syncing status of the various names in an example. 
''' Prevents infinite reference loops. 
''' If a new assignment creates one, the older assignment will be set to the 'Custom' eNameSync type.
''' </summary>
''' <remarks></remarks>
Public Class cMCNameSyncer
    Inherits PropertyChanger
    Implements ICloneable

#Region "Enumerations"
    ''' <summary>
    ''' The type of syncing to be enforced between different file names.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eNameSync
        enumError = 0
        ''' <summary>
        ''' Name will be synced to the model file name.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Model File Name")> ModelFileName
        ''' <summary>
        ''' Name will be synced to the model control ID number.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Model Control ID")> ModelControlID
        ''' <summary>
        ''' Name will be synced to the model control secondary ID, which is a name.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Model Control Secondary ID")> ModelControlSecondaryID
        ''' <summary>
        ''' Indicates to perform an action to rename the file, appending the "_MC" extension at the end. Temporary sync that will be reset to 'Custom' after action is taken. 
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Add Extension")> AddExtension
        ''' <summary>
        ''' Name will not be altered by the program, and left as-is.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Custom")> Custom
    End Enum
#End Region

#Region "Properties"
    Private _modelFileNameSynced As eNameSync = eNameSync.Custom
    ''' <summary>
    ''' States in what way the model file name is synced with other properties or files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property modelFileNameSynced() As eNameSync
        Get
            Return _modelFileNameSynced
        End Get
        Set(ByVal value As eNameSync)
            [Set](NameOfProp(Function() Me.modelFileNameSynced), _modelFileNameSynced, value)
            If (_mcSecondaryIDSynced = eNameSync.ModelFileName AndAlso
                    value = eNameSync.ModelControlSecondaryID) Then
                _mcSecondaryIDSynced = eNameSync.Custom
            End If
        End Set
    End Property

    Private _mcFileNameSynced As eNameSync = eNameSync.ModelControlSecondaryID
    ''' <summary>
    ''' States in what way the model control XML file name is synced with other properties or files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property mcFileNameSynced() As eNameSync
        Get
            Return _mcFileNameSynced
        End Get
        Set(ByVal value As eNameSync)
            [Set](NameOfProp(Function() Me.mcFileNameSynced), _mcFileNameSynced, value)
        End Set
    End Property

    Private _mcSecondaryIDSynced As eNameSync = eNameSync.ModelFileName
    ''' <summary>
    ''' States in what way the model control secondary id is synced with other properties or files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property mcSecondaryIDSynced() As eNameSync
        Get
            Return _mcSecondaryIDSynced
        End Get
        Set(ByVal value As eNameSync)
            If [Set](NameOfProp(Function() Me.mcSecondaryIDSynced), _mcSecondaryIDSynced, value) Then
                If (_modelFileNameSynced = eNameSync.ModelControlSecondaryID AndAlso
                    value = eNameSync.ModelFileName) Then
                    _modelFileNameSynced = eNameSync.Custom
                End If
            End If
        End Set
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim myClone As New cMCNameSyncer

        With myClone
            .mcFileNameSynced = mcFileNameSynced
            .mcSecondaryIDSynced = .mcSecondaryIDSynced
            .modelFileNameSynced = modelFileNameSynced
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
        If Not (TypeOf p_object Is cMCNameSyncer) Then Return False
        Dim comparedObject As cMCNameSyncer = TryCast(p_object, cMCNameSyncer)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .mcFileNameSynced = mcFileNameSynced Then Return False
            If Not .mcSecondaryIDSynced = .mcSecondaryIDSynced Then Return False
            If Not .modelFileNameSynced = modelFileNameSynced Then Return False
        End With

        Return True
    End Function
#End Region


End Class
