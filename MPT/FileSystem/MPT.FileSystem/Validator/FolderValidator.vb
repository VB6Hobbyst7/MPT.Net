Option Strict On
Option Explicit On

Namespace Validator
Public MustInherit Class FolderValidator
        Implements IValidateFolder

        Protected ReadOnly _excludeFolder As Boolean
        ''' <summary>
        ''' True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ExcludeFolder As Boolean Implements IValidateFolder.ExcludeFolder
            Get
                Return _excludeFolder
            End Get
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="excludeFolder">True: Matching criteria will exclude the folder.
        ''' False: Matching criteria will include the folder.</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal excludeFolder As Boolean = False)
            _excludeFolder = excludeFolder
        End Sub

        Public MustOverride Function IsValidFolder(path As String) As Boolean Implements IValidateFolder.IsValidFolder

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
            Return ((isValid AndAlso Not _excludeFolder) OrElse
                    (Not isValid AndAlso _excludeFolder))
        End Function
End Class

End Namespace