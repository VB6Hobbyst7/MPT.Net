Option Explicit On
Option Strict On

Public MustInherit Class TemplateModelFrame
    Inherits TemplateModelBeam

#Region "Properties"
    ''' <summary>
    ''' The number of stories in the frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberStories As Integer
        Get
            Return _baseSegment.Number
        End Get
        Set(value As Integer)
            _baseSegment.Number = value
        End Set
    End Property
    ''' <summary>
    ''' The height of each story. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StoryHeight As Double
        Get
            Return _baseSegment.Length
        End Get
        Set(value As Double)
            _baseSegment.Length = value
        End Set
    End Property

    ''' <summary>
    ''' The frame section property used for all columns in the frame. 
    ''' This must either be Default or the name of a defined frame section property.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Column As String = _default
#End Region

#Region "Initializaiton"
    Public Sub New(ByVal numberStories As Integer,
                   ByVal storyHeight As Double)
        With _baseSegment
            .Number = numberStories
            .Length = storyHeight
        End With
    End Sub
#End Region
End Class
