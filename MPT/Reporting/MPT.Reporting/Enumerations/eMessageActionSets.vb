Option Explicit On
Option Strict On

''' <summary>
''' Sets of actions for common prompts. To be used for custom forms in the program.
''' </summary>
''' <remarks></remarks>
Public Enum eMessageActionSets
    None = 0
    OkCancel
    OkOnly
    YesNo
    AbortRetryIgnore
    RetryCancel
    YesNoCancel
End Enum