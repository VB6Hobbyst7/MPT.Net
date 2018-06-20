Option Explicit On
Option Strict On

''' <summary>
''' Instructions to the XML Bulk Editor as to what action to take with the selected rows.
''' </summary>
''' <remarks></remarks>
Public Enum eXMLEditorAction
    None = 0
    Save = 1
    Convert = 2
    NodeAdd = 3
    KeywordsAddRemove = 4
    ObjectAdd = 5
    DirectoriesFlatten = 6
    DirectoriesDBGather = 7
    UpdateModelFiles = 8
    UpdateOutputSettingsFiles = 9
    NodeDelete = 10
    ActionToExistingFiles = 11
    ActionToNewFiles = 12
    RenameMCFilesAddSuffix = 13
    RenameOutputSettingsFilesRemoveImportTag = 14
    CreateNewModelSourceFromDestination = 15
End Enum