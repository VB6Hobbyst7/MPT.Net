Option Strict On
Option Explicit On

Imports MPT.FileSystem.PathLibrary

Imports CSiTester.cFileAttachment

''' <summary>
''' A class with directions to an Excel file that stores or calculates results using output from the analysis program. 
''' Regtest is able to interact with Excel to extract these results from new runs.
''' </summary>
''' <remarks></remarks>
Public Class cPathExcelResultFile
    Inherits cPathAttachment

#Region "Constants"
    ''' <summary>
    ''' Only macro-enabled Excel files will work with RegTest using them for post-processing results.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FILE_EXTENSION_EXCEL_RESULT As String = "xlsm"

    ''' <summary>
    ''' Directory type that the Excel result file is considered to be. Used to hold the directoryType and field constant.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const DIRECTORY_TYPE As eAttachmentDirectoryType = eAttachmentDirectoryType.supportingFile
#End Region

#Region "Properties"
    ''' <summary>
    ''' Directory type associated with the attachment file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Overrides Property directoryType As eAttachmentDirectoryType
        Get
            Return DIRECTORY_TYPE
        End Get
        Set(ByVal value As eAttachmentDirectoryType)
            ' No value is set.
        End Set
    End Property

#End Region

#Region "Initialization"
    ''' <summary>
    ''' Depending on parameters given, sets all properties, including the reference and initializes based on an existing file. 
    ''' Priority of properties is given to the file path if it is provided.
    ''' </summary>
    ''' <param name="p_bindTo">Model control file to reference.</param>
    ''' <param name="p_path">Path to be used.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_path As String = "")
        _pathType = ePathType.FileWithExtension
        If IsValidExcelFile(p_path) Then
            MyBase.SetProperties(p_path, p_bindTo)
        ElseIf (p_bindTo IsNot Nothing AndAlso
        IsValidExcelFile(p_bindTo.resultsExcel.filePath)) Then
            MyBase.SetProperties(p_bindTo.resultsExcel.filePath, p_bindTo)
        Else
            Exit Sub
        End If

        If p_bindTo IsNot Nothing Then MyBase.SetMCModel(p_bindTo)

        _directoryType = DIRECTORY_TYPE

        MyBase.InitializeCustomHandlers()
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cPathExcelResultFile = DirectCast(MyBase.Clone, cPathExcelResultFile)

        With myClone
            ' No properties specific to this class need to be cloned
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathExcelResultFile()
    End Function
#End Region


#Region "Methods: Friend"


    ''' <summary>
    ''' Determines whether the file in the path provided is a valid Excel results file to be used in CSiTester for result post-processing.
    ''' </summary>
    ''' <param name="p_path">File path to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsValidExcelFile(ByVal p_path As String) As Boolean
        Dim fileExtension As String = GetSuffix(p_path, ".")

        If StringsMatch(fileExtension, FILE_EXTENSION_EXCEL_RESULT) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
