Option Explicit On
Option Strict On

''' <summary>
''' Whether to run an example, compare an example's results, or both.
''' </summary>
''' <remarks></remarks>
Public Enum eCheckType
    None = 1
    Run = 2
    Compare = 3
    RunAndCompare = 4
    ''' <summary>
    ''' Examples are run, without being compared, in some cases.
    ''' </summary>
    ''' <remarks></remarks>
    RunAndCompareNoSync = 5
End Enum