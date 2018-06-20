Option Strict On
Option Explicit On

''' <summary>
'''  Implements a design interface for all auto load types.
''' </summary>
Public Interface IAutoLoad
#Region "Properties"
    ''' <summary>
    ''' Name of an existing load pattern of the auto load type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameLoadPattern As String
    ''' <summary>
    ''' Name of the code used for the auto parameters. 
    ''' Blank means that no auto load is specified for the named load pattern.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NameCode As String
#End Region

#Region "Methods"

#End Region
End Interface
