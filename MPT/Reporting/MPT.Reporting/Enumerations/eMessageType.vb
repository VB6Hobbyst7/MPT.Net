Option Explicit On
Option Strict On

''' <summary>
''' Type of message.
''' </summary>
''' <remarks></remarks>
Public Enum eMessageType
    None = 0
    Asterisk
    [Error]
    Exclamation
    Hand
    Information
    Question
    [Stop]
    Warning
End Enum