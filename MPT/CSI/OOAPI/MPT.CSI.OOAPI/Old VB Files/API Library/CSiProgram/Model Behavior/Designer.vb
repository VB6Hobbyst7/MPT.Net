Option Strict On
Option Explicit On

Imports CSiApiTester.cEnumerations

Public Class Designer

#Region "Properties"
    Private _CodeTypesUsed As New List(Of IDesignCode)
    Public ReadOnly Property CodeTypesUsed As List(Of IDesignCode)
        Get
            Return _CodeTypesUsed
        End Get
    End Property
#End Region

#Region "Methods: Public"

    ''' <summary>
    ''' Run all applicable forms of design for the structure.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function RunDesign(Optional ByVal codeTypes As List(Of IDesignCode) = Nothing) As Boolean
        If codeTypes Is Nothing Then codeTypes = _CodeTypesUsed
        Dim success As Boolean = True

        'Run all applicable design types
        For Each codeType As IDesignCode In codeTypes
            If Not codeType.StartDesign() AndAlso success Then success = False
        Next

        Return success
    End Function

    Public Function DeleteResults() As Boolean
        If codeTypes Is Nothing Then codeTypes = _CodeTypesUsed
        Dim success As Boolean = True

        'Run all applicable design types
        For Each codeType As IDesignCode In codeTypes
            If Not codeType.DeleteResults() AndAlso success Then success = False
        Next

        Return success
    End Function
#End Region

#Region "Methods: Private"

#End Region

End Class
