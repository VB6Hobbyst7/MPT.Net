Option Explicit On
Option Strict On

''' <summary>
''' Test results type to query for. 
''' Used for considering different strategies for filling in assumed run and compare times based on example output.
''' </summary>
''' <remarks></remarks>
Friend Enum eTestResults
    ModelID = 0
    ActualAnalysisRunTime = 1
    ActualDatabaseRetrievalTime = 2
    ActualTotalRunTime = 3
    Other = 4
End Enum