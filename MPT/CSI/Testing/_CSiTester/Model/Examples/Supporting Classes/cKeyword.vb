Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger

''' <summary>
''' Class that contains a keyword type, and additional description information. As a property of cClassifcation and cKeywords.
''' </summary>
''' <remarks></remarks>
Public Class cKeyword
    Inherits PropertyChanger
    Implements ICloneable

#Region "Properties"
    Private _name As String
    ''' <summary>
    ''' Value of the keyword.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
        Set(ByVal value As String)
            [Set](Function() Me.name, _name, value)
        End Set
        Get
            Return _name
        End Get
    End Property

    ''' <summary>
    ''' The complete keyword, with the tag prefix and value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property nameWithTag As String
        Get
            Return tag & name
        End Get
    End Property

    Private _tag As String
    ''' <summary>
    ''' Tag prefix of the keyword.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property tag As String
        Get
            Return _tag
        End Get
    End Property

    Private _maxTagOccurrence As Integer = 1
    ''' <summary>
    ''' Maximum number of times that this tag can be used in a given list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property maxTagOccurrence As Integer
        Get
            Return _maxTagOccurrence
        End Get
    End Property


    Private _description As String
    ''' <summary>
    ''' Description of the keyword value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property description As String
        Get
            Return _description
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Initializes the keyword.
    ''' </summary>
    ''' <param name="p_name">Sets the value of the keyword. 
    ''' If a tag exists in the name (indicated by ':'), this will be separated and set in the 'tag' property.</param>
    ''' <param name="p_tag">Sets the ReadOnly tag property. Overwrites any tag auto-assignment from the name provided.</param>
    ''' <param name="p_description">Sets the description of the keyword.</param>
    ''' <param name="p_maxTagOccurrence">Sets the limit of how many times the tag can appear in a single list of keywords.</param>
    ''' <remarks></remarks>
    Sub New(Optional p_name As String = "",
            Optional p_tag As String = "",
            Optional p_description As String = "",
            Optional p_maxTagOccurrence As Integer = 1)

        If Not String.IsNullOrWhiteSpace(p_name) Then
            Dim trimmedValue = Trim(GetSuffix(p_name, ":"))
            name = trimmedValue

            If String.IsNullOrEmpty(p_tag) Then
                Dim tagValue = GetPrefix(p_name, ":")
                If Not StringsMatch(tagValue, p_name) Then
                    _tag = tagValue & ": "
                End If
            End If
        End If
        If Not String.IsNullOrWhiteSpace(p_tag) Then _tag = p_tag
        If Not String.IsNullOrWhiteSpace(p_description) Then _description = p_description
        If p_maxTagOccurrence >= 1 Then
            _maxTagOccurrence = p_maxTagOccurrence
        End If
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cKeyword

        With myClone
            .name = name
            ._description = _description
            ._tag = _tag
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
        If Not (TypeOf p_object Is cKeyword) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cKeyword = TryCast(p_object, cKeyword)

        'Check for any differences
        If comparedObject Is Nothing Then Return False

        With comparedObject
            If Not .name = name Then Return False
            If Not ._description = _description Then Return False
            If Not ._tag = _tag Then Return False
        End With

        Return True
    End Function
#End Region

End Class
