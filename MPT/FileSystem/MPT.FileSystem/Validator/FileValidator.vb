Option Strict On
Option Explicit On

Namespace Validator
Public MustInherit Class FileValidator
        Implements IValidateFile

        Protected ReadOnly _excludeFile As Boolean
        ''' <summary>
        ''' True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ExcludeFile As Boolean Implements IValidateFile.ExcludeFile
            Get
                Return _excludeFile
            End Get
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="excludeFile">True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal excludeFile As Boolean = False)
            _excludeFile = excludeFile
        End Sub

        Public MustOverride Function IsValidFile(path As String) As Boolean Implements IValidateFile.IsValidFile
            
        ''' <summary>
        ''' Returns the correct boolean result of the following combinations:
        ''' 1. isValid and Not excluded = True 
        ''' 2. Not isValid and Not excluded = False 
        ''' 3. isValid and excluded = False 
        ''' 4. Not isValid and excluded = True
        ''' </summary>
        ''' <param name="isValid"></param>
        ''' <returns></returns>
        Protected Function Result(ByVal isValid As Boolean) As Boolean
            Return ((isValid AndAlso Not _excludeFile) OrElse
                    (Not isValid AndAlso _excludeFile))
        End Function
    End Class

End Namespace

