Option Strict On
Option Explicit On


Public Class Analyzer
#Region "Properties"

#End Region


#Region "Initialization"

#End Region

#Region "Methods: Public"
    ' Basic Analysis Methods
    ''' <summary>
    ''' Creates the analysis model. 
    ''' If the analysis model is already created and current, nothing is done.
    ''' It is not necessary to call this function before running an analysis. 
    ''' The analysis model is automatically created, if necessary, when the model is run.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateAnalysisModel() As Boolean
        ret = SapModel.Analyze.CreateAnalysisModel
        Return retCheck(ret, " SapModel.Analyze.CreateAnalysisModel")
    End Function

    ''' <summary>
    ''' Runs the analysis. 
    ''' The analysis model is automatically created as part of this function.
    ''' </summary>
    ''' <param name="filePath">Path to the model file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function RunAnalysis(ByVal filePath As String) As Boolean
        If Not IO.File.Exists(filePath) Then Return False

        'run model (this will create the analysis model)
        ret = SapModel.Analyze.RunAnalysis
        Return retCheck(ret, " SapModel.Analyze.RunAnalysis")
    End Function

    ''' <summary>
    ''' Deletes results for the specified load cases.
    ''' </summary>
    ''' <param name="nameLoadCases">List of all load cases for which the results are to be deleted.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteResults(ByVal nameLoadCases As List(Of String)) As Boolean
        Dim success As Boolean = True

        For Each name As String In nameLoadCases
            If Not DeleteResults(name) AndAlso success Then success = False
        Next

        Return success
    End Function
    ''' <summary>
    ''' Deletes results for the specified load case.
    ''' </summary>
    ''' <param name="nameLoadCase">The name of an existing load case that is to have its results deleted.
    ''' This item is ignored when the All item is True.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteResults(ByVal nameLoadCase As String) As Boolean
        Return DeleteResults(nameLoadCase)
    End Function
    ''' <summary>
    ''' Deletes results for all load cases.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteResults() As Boolean
        Return DeleteResults("", deleteAll:=True)
    End Function

    ' Other Methods
    ''' <summary>
    ''' Modifies the undeformed geometry based on displacements obtained from a specified load case.
    ''' </summary>
    ''' <param name="caseName">The name of the static load case from which displacements are obtained.</param>
    ''' <param name="scaleFactor">The scale factor applied to the displacements.</param>
    ''' <param name="stageOfStageConstruction">This item applies only when the specified load case is a staged construction load case. 
    ''' It is the stage number from which the displacements are obtained. 
    ''' Specifying a -1 for this item means to use the last run stage.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyUnDeformedGeometry(ByVal caseName As String,
                                             ByVal scaleFactor As Double,
                                             Optional ByVal stageOfStageConstruction As Integer = -1) As Boolean
        Return ModifyUnDeformedGeometry(caseName, scaleFactor, stageOfStageConstruction, resetToOriginal:=False)
    End Function

    ''' <summary>
    ''' Resets the undeformed geometry such that the original undeformed geometry data is reinstated.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ResetUndeformedGeometry() As Boolean
        Return ModifyUnDeformedGeometry("", 1, , resetToOriginal:=True)
    End Function


    'Get/Set Methods
    ''' <summary>
    ''' Retrieves the model global degrees of freedom.
    ''' </summary>
    ''' <param name="degreesOfFreedom">Boolean indications of which degrees of freedom are active.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetActiveDOF(ByRef degreesOfFreedom As DegreesOfFreedom) As Boolean
        Dim DOFArray() As Boolean = degreesOfFreedom.ToArray
        ret = SapModel.Analyze.GetActiveDOF(DOFArray)

        degreesOfFreedom.FillFromArray(DOFArray)

        Return retCheck(ret, " SapModel.Analyze.GetActiveDOF")
    End Function

    ''' <summary>
    ''' Sets the model global degrees of freedom.
    ''' </summary>
    ''' <param name="degreesOfFreedom">Boolean indications of which degrees of freedom are active.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetActiveDOF(ByVal degreesOfFreedom As DegreesOfFreedom) As Boolean
        ret = SapModel.Analyze.SetActiveDOF(degreesOfFreedom.ToArray)
        Return retCheck(ret, " SapModel.Analyze.SetActiveDOF")
    End Function


    ' -----
    ''' <summary>
    ''' Retrieves the status for all load cases.
    ''' </summary>
    ''' <param name="caseNameAndStatus">List of 'Load Case'/'Analysis Status' pairs.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCaseStatus(ByRef caseNameAndStatus As Dictionary(Of String, AnalysisStatus)) As Boolean
        Dim numberOfCases As Integer = caseNameAndStatus.Count
        Dim caseNames() As String = caseNameAndStatus.Keys.ToArray
        Dim caseStatus() As Integer = caseNameAndStatus.Values.ToArray

        If GetCaseStatus(numberOfCases, caseNames, caseStatus) Then
            For i = 0 To UBound(caseNames)
                caseNameAndStatus.Add(caseNames(i), CType(caseStatus(i), AnalysisStatus))
            Next

            Return True
        Else
            Return False
        End If
    End Function


    ' -----
    ''' <summary>
    ''' Retrieves the run flags for all analysis cases.
    ''' </summary>
    ''' <param name="caseNamesAndFlags">List of 'Load Case'/'Analysis Flag' pairs.
    ''' The analysis flag indicates if the corresponding load case is set to be run.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRunCaseFlag(ByRef caseNamesAndFlags As Dictionary(Of String, Boolean)) As Boolean
        Dim numberOfCases As Integer = caseNamesAndFlags.Count
        Dim caseNames() As String = caseNamesAndFlags.Keys.ToArray
        Dim caseStatuses() As Boolean = caseNamesAndFlags.Values.ToArray

        If GetRunCaseFlag(numberOfCases, caseNames, caseStatuses) Then
            For i = 0 To UBound(caseNames)
                caseNamesAndFlags.Add(caseNames(i), caseStatuses(i))
            Next

            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' Sets the run flags for the specified load cases.
    ''' </summary>
    ''' <param name="caseNamesAndFlags">List of 'Load Case'/'Analysis Flag' pairs.
    ''' The analysis flag indicates if the corresponding load case is set to be run.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetRunCaseFlag(ByVal caseNamesAndFlags As Dictionary(Of String, Boolean)) As Boolean
        Dim success As Boolean = True

        For Each caseName As String In caseNamesAndFlags.Keys
            If Not SetRunCaseFlag(caseName, caseNamesAndFlags(caseName)) AndAlso success Then success = False
        Next

        Return success
    End Function
    ''' <summary>
    ''' Sets the run flag for the specified load case.
    ''' </summary>
    ''' <param name="caseName">The name of an existing load case that is to have its flag set.</param>
    ''' <param name="setToRun">True: The specified load case is to be run.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetRunCaseFlag(ByVal caseName As String,
                                   ByVal setToRun As Boolean) As Boolean
        Return SetRunCaseFlag(caseName, setToRun)
    End Function
    ''' <summary>
    ''' Sets the run flag for all of the existing load cases.
    ''' </summary>
    ''' <param name="setToRun">True: The specified load case is to be run.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetRunCaseFlag(ByVal setToRun As Boolean) As Boolean
        Return SetRunCaseFlag("", setToRun, applyToAll:=True)
    End Function

    ' -----
    ''' <summary>
    ''' Retrieves the model solver options.
    ''' </summary>
    ''' <param name="solverType"></param>
    ''' <param name="solverProcessType"></param>
    ''' <param name="force32BitSolver">True: Rhe analysis is always run using 32-bit, even on 64-bit computers.</param>
    ''' <param name="stiffCase">The name of the load case used when outputting the mass and stiffness matrices to text files. 
    ''' If this item is blank, no matrices are output.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSolverOptions(ByRef solverType As SolverType,
                                    ByRef solverProcessType As SolverProcessType,
                                    ByRef force32BitSolver As Boolean,
                                    Optional ByRef stiffCase As String = "") As Boolean
        Dim solverTypeInt As Integer
        Dim solverProcessTypeInt As Integer

        ret = SapModel.Analyze.GetSolverOption_1(solverTypeInt, solverProcessTypeInt, force32BitSolver, stiffCase)

        solverType = CType(solverTypeInt, SolverType)
        solverProcessType = CType(solverProcessTypeInt, SolverProcessType)

        Return retCheck(ret, " SapModel.Analyze.GetSolverOption_1")
    End Function

    ''' <summary>
    ''' Sets the model solver options.
    ''' </summary>
    ''' <param name="solverType"></param>
    ''' <param name="solverProcessType"></param>
    ''' <param name="force32BitSolver">True: The analysis is always run using 32-bit, even on 64-bit computers.</param>
    ''' <param name="stiffCase">The name of the load case used when outputting the mass and stiffness matrices to text files. 
    ''' If this item is blank, no matrices are output.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetSolverOptions(ByVal solverType As SolverType,
                                    ByVal solverProcessType As SolverProcessType,
                                    ByVal force32BitSolver As Boolean,
                                    Optional ByVal stiffCase As String = "") As Boolean
        ret = SapModel.Analyze.SetSolverOption_1(solverType, solverProcessType, force32BitSolver, stiffCase)

        Return retCheck(ret, " SapModel.Analyze.SetSolverOption_1")
    End Function


#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Deletes results for the specified load case.
    ''' </summary>
    ''' <param name="nameLoadCase">The name of an existing load case that is to have its results deleted.
    ''' This item is ignored when the All item is True.</param>
    ''' <param name="deleteAll">True: Results are deleted for all load cases, and the Name item is ignored.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteResults(ByVal nameLoadCase As String,
                                   Optional ByVal deleteAll As Boolean = False) As Boolean
        ret = SapModel.Analyze.DeleteResults(nameLoadCase, deleteAll)
        Return retCheck(ret, " SapModel.Analyze.DeleteResults")
    End Function

    ''' <summary>
    ''' Modifies the undeformed geometry based on displacements obtained from a specified load case.
    ''' </summary>
    ''' <param name="caseName">The name of the static load case from which displacements are obtained.</param>
    ''' <param name="scaleFactor">The scale factor applied to the displacements.</param>
    ''' <param name="stageOfStageConstruction">This item applies only when the specified load case is a staged construction load case. 
    ''' It is the stage number from which the displacements are obtained. 
    ''' Specifying a -1 for this item means to use the last run stage.</param>
    ''' <param name="resetToOriginal">True: All other input items in this function are ignored and the original undeformed geometry data is reinstated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModifyUnDeformedGeometry(ByVal caseName As String,
                                              ByVal scaleFactor As Double,
                                              Optional ByVal stageOfStageConstruction As Integer = -1,
                                              Optional ByVal resetToOriginal As Boolean = False) As Boolean
        ret = SapModel.Analyze.ModifyUndeformedGeometry(caseName, scaleFactor, stageOfStageConstruction, resetToOriginal)
        Return retCheck(ret, " SapModel.Analyze.ModifyUnDeformedGeometry")
    End Function

    ' -----
    ''' <summary>
    ''' Retrieves the status for all load cases.
    ''' </summary>
    ''' <param name="numberOfCases">The number of load cases for which the status is reported.</param>
    ''' <param name="caseName">This is an array that includes the name of each analysis case for which the status is reported.</param>
    ''' <param name="caseStatus"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCaseStatus(ByRef numberOfCases As Integer,
                                  ByRef caseName() As String,
                                  ByRef caseStatus() As Integer) As Boolean
        ret = SapModel.Analyze.GetCaseStatus(numberOfCases, caseName, caseStatus)
        Return retCheck(ret, " SapModel.Analyze.GetCaseStatus")
    End Function

    ' -----
    ''' <summary>
    ''' Retrieves the run flags for all analysis cases.
    ''' </summary>
    ''' <param name="numberOfCases">The number of load cases for which the run flag is reported.</param>
    ''' <param name="caseName">This is an array that includes the name of each analysis case for which the run flag is reported.</param>
    ''' <param name="caseSetToRun">This is an array of boolean values indicating if the specified load case is to be run.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRunCaseFlag(ByRef numberOfCases As Integer,
                                    ByRef caseName() As String,
                                    ByRef caseSetToRun() As Boolean) As Boolean
        ret = SapModel.Analyze.GetRunCaseFlag(numberOfCases, caseName, caseSetToRun)
        Return retCheck(ret, " SapModel.Analyze.GetRunCaseFlag")
    End Function
    ''' <summary>
    ''' Sets the run flag for all of the existing load cases.
    ''' </summary>
    ''' <param name="setToRun">True: The specified load case is to be run.</param>
    ''' <param name="caseName">The name of an existing load case that is to have its flag set.</param>
    ''' <param name="applyToAll">True: The run flag is set as specified by the Run item for all load cases, and the Name item is ignored.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetRunCaseFlag(ByVal caseName As String,
                                   ByVal setToRun As Boolean,
                                   Optional ByVal applyToAll As Boolean = False) As Boolean
        ret = SapModel.Analyze.SetRunCaseFlag(caseName, setToRun, applyToAll)
        Return retCheck(ret, " SapModel.Analyze.SetRunCaseFlag")
    End Function
#End Region

End Class
