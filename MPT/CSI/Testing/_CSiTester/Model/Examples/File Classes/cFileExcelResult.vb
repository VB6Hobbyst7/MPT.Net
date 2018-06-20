Option Explicit On
Option Strict On

Imports MPT.Reporting

Public Class cFileExcelResult
    Inherits cMCFile

#Region "Initialization"
    Friend Sub New(Optional ByVal p_pathFile As String = "",
                   Optional ByVal p_bindTo As cMCModel = Nothing)
        MyBase.New()
        InitializeFile(p_bindTo, p_pathFile)
    End Sub

    Protected Overrides Sub InitializeFile(Optional ByVal p_bindTo As cMCModel = Nothing,
                                            Optional ByVal p_pathFile As String = "")
        _pathDestination = New cPathExcelResultFile(p_bindTo, p_pathFile)
        CompleteInitialization()
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cFileExcelResult = DirectCast(MyBase.Clone, cFileExcelResult)

        Try
            With myClone
                ._pathDestination = CType(PathExcelResult().Clone, cPathExcelResultFile)
            End With
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try

        Return myClone
    End Function
    Protected Overrides Function Create() As cMCFile
        Return New cFileExcelResult()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cFileExcelResult) Then Return False

        Dim comparedObject As cFileExcelResult = TryCast(p_object, cFileExcelResult)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False

            Dim pathCast As cPathExcelResultFile = PathExcelResult()
            Dim pathCastCompare As cPathExcelResultFile = .PathExcelResult()
            If Not pathCastCompare.Equals(pathCast) Then Return False
        End With

        Return True
    End Function

#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Binds the object to the state of the supplied model control object.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub Bind(ByVal p_bindTo As cMCModel)
        PathExcelResult.SetMCModel(p_bindTo)

        _fileManager.SetDestinationPath(pathDestination)
    End Sub

    ''' <summary>
    ''' Returns the path object in the desired downcast class. 
    ''' If the path object was not upcast from this class, 'Nothing' is returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function PathExcelResult() As cPathExcelResultFile
        Try
            Return DirectCast(_pathDestination, cPathExcelResultFile)
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
            Return Nothing
        End Try
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Determines if the path object provided is of a matching type.
    ''' </summary>
    ''' <param name="p_path">Path object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function isMatchingPathType(ByVal p_path As cPath) As Boolean
        If TypeOf p_path Is cPathExcelResultFile Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
