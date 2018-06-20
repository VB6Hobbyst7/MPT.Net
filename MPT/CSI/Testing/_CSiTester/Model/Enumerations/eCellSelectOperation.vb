Option Explicit On
Option Strict On

''' <summary>
''' Possible Cell Operations to do with selected row. 
''' Change checkbox status to add, remove, or make the current selection the only checkboxes to be added
''' </summary>
''' <remarks></remarks>
Public Enum eCellSelectOperation
    Add = 0
    Remove = 1
    ''' <summary>
    ''' Add operation, after all cells are cleared of add/remove status.
    ''' </summary>
    ''' <remarks></remarks>
    Replace = 2
End Enum