Option Explicit On
Option Strict On

''' <summary>
''' Specifies whether the node create operation should make a child node to the specified node, 
''' or insert the node before or after the specified node.
''' </summary>
''' <remarks></remarks>
Public Enum eNodeCreate
    child
    insertBefore
    insertAfter
End Enum
