Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cLibFolders

''' <summary>
''' Class containing a title and path to an image file associated with an example.
''' </summary>
''' <remarks></remarks>
Public Class cMCImage
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _title As String
    '=== To be recorded in the XML
    ''' <summary>
    ''' Title of the image.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
        Set(ByVal value As String)
            If Not _title = value Then
                _title = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("title"))
            End If
        End Set
        Get
            Return _title
        End Get
    End Property

    Private _path As String
    ''' <summary>
    ''' Path to the image file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property path As String
        Set(ByVal value As String)
            If Not _path = value Then
                _path = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("path"))
            End If
        End Set
        Get
            Return _path
        End Get
    End Property

    '=== For moving file
    Private _copyAction As cCopyAction
    ''' <summary>
    ''' Contains relevant properties and methods for adding an image to the appropriate location relative to the example model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property copyAction As cCopyAction
        Set(ByVal value As cCopyAction)
            _copyAction = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("copyAction"))
        End Set
        Get
            Return _copyAction
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        copyAction = New cCopyAction
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCImage

        With myClone
            .copyAction = CType(copyAction.Clone, cCopyAction)
            .path = path
            .title = title
        End With

        Return myClone
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns 'True' if the object provided does not perfectly match the existing object.
    ''' </summary>
    ''' <param name="p_image">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDifferent(ByVal p_image As cMCImage) As Boolean
        Dim isMatch As Boolean = True

        With p_image
            If Not .title = title Then isMatch = False
            If Not .path = path Then isMatch = False
            If Not .copyAction.IsDifferent(copyAction) Then isMatch = False
        End With

        Return Not isMatch
    End Function

    ''' <summary>
    ''' Copies the corresponding file to the correct locations relative to the model file, based on the directory structure chosen. Returns true if successful.
    ''' </summary>
    ''' <param name="p_xmlPath">Path to the Model Control XML file. Copy operations take place relative to this location.</param>
    ''' <remarks></remarks>
    Friend Function CopyFile(ByVal p_xmlPath As String) As Boolean
        If copyAction.copySourceToDestination Then myXMLEditor.CopyObjectFiles(p_xmlPath, copyAction)

        If FileExists(path) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

End Class
