Option Explicit On
Option Strict On

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger

Public Class cKeywordTags
    Inherits PropertyChanger

#Region "Constants"
    '=== Keywords Tags
    'TODO: Make these reference an enum with description, with ': "' appended at this location
    'Unique Keywords (1 allowed)
    Friend Const TAG_KEYWORD_EXAMPLE_TYPE As String = "Type: "
    Friend Const TAG_KEYWORD_ANALYSIS_CLASS As String = "Analysis Class: "
    Friend Const TAG_KEYWORD_CODE_REGION As String = "Region: "
    Friend Const TAG_KEYWORD_DESIGN_TYPE As String = "Type Design: "
    Friend Const TAG_KEYWORD_DESIGN_CLASS As String = "Design Class: "
    Friend Const TAG_KEYWORD_MULTI_MODEL As String = "MultiModel: "
    Friend Const TAG_KEYWORD_IMPORTED As String = "Import: "

    'TODO: Make these reference an enum with description, with ': "' appended at this location
    'Multi-Keyword (multiple allowed)
    Friend Const TAG_KEYWORD_ANALYSIS_TYPE As String = "Type Analysis: "
    Friend Const TAG_KEYWORD_ELEMENT_TYPE As String = "Type Element: "
    Friend Const TAG_KEYWORD_WARNING_TYPE As String = "Type Warning: "
    Friend Const TAG_KEYWORD_WARNING As String = "Warning: "
#End Region

#Region "Properties"
    Private _exampleType As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_EXAMPLE_TYPE, p_maxTagOccurrence:=2)
    Public ReadOnly Property exampleType As cKeyword
        Get
            Return _exampleType
        End Get
    End Property

    Private _analysisClass As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_ANALYSIS_CLASS)
    Public ReadOnly Property analysisClass As cKeyword
        Get
            Return _analysisClass
        End Get
    End Property

    Private _codeRegion As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_CODE_REGION)
    Public ReadOnly Property codeRegion As cKeyword
        Get
            Return _codeRegion
        End Get
    End Property

    Private _designType As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_DESIGN_TYPE)
    Public ReadOnly Property designType As cKeyword
        Get
            Return _designType
        End Get
    End Property

    Private _designClass As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_DESIGN_CLASS)
    Public ReadOnly Property designClass As cKeyword
        Get
            Return _designClass
        End Get
    End Property

    Private _multiModel As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_MULTI_MODEL)
    Public ReadOnly Property multiModel As cKeyword
        Get
            Return _multiModel
        End Get
    End Property

    Private _importedModel As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_IMPORTED)
    Public ReadOnly Property importedModel As cKeyword
        Get
            Return _importedModel
        End Get
    End Property

    Private _warning As cKeyword = New cKeyword(p_tag:=TAG_KEYWORD_WARNING)
    Public ReadOnly Property warning As cKeyword
        Get
            Return _warning
        End Get
    End Property
#End Region

#Region "Method"
    ''' <summary>
    ''' Returns the value of the keyword without a prefix tag, if one exists.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to trim.</param>
    ''' <param name="p_tagKeyword">Keyword object containing the tag to check.</param>
    ''' <param name="p_returnBlankIfNoMatch">True: If no match is found, no value is returned. 
    ''' False: The original value is returned if no match is found.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function ValueWithoutMatchingTag(ByVal p_keyword As String,
                                                    ByVal p_tagKeyword As cKeyword,
                                                    Optional ByVal p_returnBlankIfNoMatch As Boolean = False) As String
        Dim tag As String = p_tagKeyword.tag
        Return ValueWithoutMatchingTag(p_keyword, tag, p_returnBlankIfNoMatch)
    End Function
    ''' <summary>
    ''' Returns the value of the keyword without a prefix tag, if one exists.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to trim.</param>
    ''' <param name="p_tagKeyword">Tag to match.</param>
    ''' <param name="p_returnBlankIfNoMatch">True: If no match is found, no value is returned. 
    ''' False: The original value is returned if no match is found.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function ValueWithoutMatchingTag(ByVal p_keyword As String,
                                                            ByVal p_tagKeyword As String,
                                                            Optional ByVal p_returnBlankIfNoMatch As Boolean = False) As String
        If IsTagType(p_keyword, p_tagKeyword) Then
            Return ValueWithoutTag(p_keyword, p_tagKeyword)
        ElseIf p_returnBlankIfNoMatch Then
            Return ""
        Else
            Return p_keyword
        End If
    End Function

    ''' <summary>
    ''' Determines if the keyword is of a matching tag type.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to check.</param>
    ''' <param name="p_tagKeyword">Keyword object containing the tag to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function IsTagType(ByVal p_keyword As String,
                                     ByVal p_tagKeyword As cKeyword) As Boolean
        Dim tag As String = p_tagKeyword.tag
        If String.IsNullOrEmpty(tag) Then Return False

        Return IsTagType(p_keyword, tag)
    End Function
    ''' <summary>
    ''' Determines if the keyword is of a matching tag type.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to check.</param>
    ''' <param name="p_tagKeyword">Tag to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function IsTagType(ByVal p_keyword As String,
                                               ByVal p_tagKeyword As String) As Boolean
        If String.IsNullOrEmpty(p_tagKeyword) Then Return False

        If StringsMatch(Left(p_keyword, Len(p_tagKeyword)), p_tagKeyword) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns the keyword without the tag prefix.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to trim.</param>
    ''' <param name="p_tagKeyword">Keyword object containing the tag prefix to remove.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function ValueWithoutTag(ByVal p_keyword As String,
                                            ByVal p_tagKeyword As cKeyword) As String
        Dim tag As String = p_tagKeyword.tag
        If String.IsNullOrEmpty(tag) Then Return p_keyword

        Return ValueWithoutTag(p_keyword, tag)
    End Function
    ''' <summary>
    ''' Returns the keyword without the tag prefix.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to trim.</param>
    ''' <param name="p_tagKeyword">Tag prefix to remove.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function ValueWithoutTag(ByVal p_keyword As String,
                                                     ByVal p_tagKeyword As String) As String
        If String.IsNullOrEmpty(p_tagKeyword) Then Return p_keyword

        p_keyword = Right(p_keyword, Len(p_keyword) - Len(p_tagKeyword))
        TrimWhiteSpace(p_keyword)

        Return p_keyword
    End Function

#End Region

End Class

