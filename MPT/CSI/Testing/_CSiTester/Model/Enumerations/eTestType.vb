Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' The various test types that regTest can run.
''' </summary>
''' <remarks></remarks>
Public Enum eTestType
    <Description("")> myError = 0
    <Description("")> none
    ''' <summary>
    ''' The model runs without any changes of the model prior to running it. 
    ''' This applies to the vast majority of the models.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Run As Is")> runAsIs
    ''' <summary>
    ''' Same as 'Run As Is', but also will run 9 different combinations of analysis parameters while saving the test results into separate subdirectories in the output directory.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Run As Is PSB")> runAsIsDiffAnalyParams
    ''' <summary>
    ''' This test applies only to CSiBridge and serves to verify whether all bridge objects can be successfully updated. 
    ''' If the program gets stuck while updating the bridge objects, it will time out and the model will be subjected to further scrutiny.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Update Bridge")> updateBridge
    ''' <summary>
    ''' This test applies only to CSiBridge and serves to verify whether the benchmarks remain the same when the model is run after updating bridge objects.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Update Bridge and Run")> updateBridgeAndRun
End Enum