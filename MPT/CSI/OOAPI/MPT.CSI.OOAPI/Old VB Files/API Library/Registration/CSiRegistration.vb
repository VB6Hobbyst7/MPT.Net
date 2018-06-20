Option Strict On
Option Explicit On

Imports System.IO
Imports System.Reflection

''' <summary>
''' Handles the registration of CSi program *.dll files.
''' </summary>
''' <remarks></remarks>
Public Class CSiRegistration

#Region "Fields"
    Private _batch As BatchFile
    Private _programPathIsValid As Boolean
#End Region

#Region "Properties"
    Private _ProgramPath As String
    ''' <summary>
    ''' Path to the CSi program to register.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property ProgramPath As String
        Get
            Return _ProgramPath
        End Get
    End Property

    Private _ApiDllFileName As String
    ''' <summary>
    ''' Name of the API *.dll file corresponding with the program to be used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property ApiDllFileName As String
        Get
            Return _ApiDllFileName
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="apiDllFileName">Name of the API *.dll file corresponding with the program to be used.</param>
    ''' <param name="programPath">Path to the CSi program to register.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal apiDllFileName As String,
                   ByVal programPath As String)
        _ProgramPath = programPath
        _programPathIsValid = File.Exists(programPath)
        _ApiDllFileName = apiDllFileName
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Registers the specified CSi program if it is currently unregistered. Returns 'True' if successful.
    ''' </summary>
    ''' <param name="registerIfFileNotExists">If no *.dll file currently exists, then any existing matching program will be unregistered and the specified program will be registered.</param>
    ''' <remarks></remarks>
    Friend Function Register(Optional ByVal registerIfFileNotExists As Boolean = True) As Boolean
        Try
            If Not File.Exists(_ProgramPath) Then Throw New IOException("Program path refers to a file that does not exist.")

            Dim referencePath As String = _ProgramPath & Path.PathSeparator & _ApiDllFileName

            If (Not File.Exists(referencePath) AndAlso registerIfFileNotExists) Then
                Dim pathBatch As String = Assembly.GetExecutingAssembly().Location & Path.PathSeparator & "Register.bat"

                _batch = New BatchFile(pathBatch) With {.CloseAfterRun = True,
                                                        .ConsoleIsVisible = True,
                                                        .DeleteExistingBatch = True,
                                                        .WaitForExit = True}

                If WriteRegisterBatchFile() Then _batch.Run()

                If File.Exists(referencePath) Then Return True
            End If
        Catch ex As Exception
            ' No action needed here
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Writes a batch file registers the CSi program after unregistering any currently registered CSi program of the same name.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WriteRegisterBatchFile() As Boolean
        If _batch Is Nothing Then Return False

        Dim programName As String = Path.GetFileName(_ProgramPath)

        Dim commandLine As String = "cd " & """" & _ProgramPath & """" & vbNewLine
        commandLine &= """" & "UnRegister" & programName & """" & vbNewLine
        commandLine &= """" & "Register" & programName & """" & vbNewLine

        _batch.Append(commandLine)

        Return True
    End Function

#End Region

  
End Class
