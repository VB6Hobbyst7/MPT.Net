Option Explicit On
Option Strict On

Imports System.Data

Imports MPT.FileSystem.PathLibrary

Imports CSiTester.cProgramControl

''' <summary>
''' Handles the specification of load pattern/case/combo types based on expected header names and the program type.
''' </summary>
''' <remarks></remarks>
Public Class cFieldProperties

#Region "Properties: Friend"
    Private _headerLoadCase As String
    ''' <summary>
    ''' Name of the column that displays the load case names.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property headerLoadCase As String
        Get
            Return _headerLoadCase
        End Get
    End Property

    Private _headerDesignLoadCombo As String
    ''' <summary>
    ''' Name of the column that displays the load combination names.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property headerDesignLoadCombo As String
        Get
            Return _headerDesignLoadCombo
        End Get
    End Property

    Private _headerLoadPattern As String
    ''' <summary>
    ''' Name of the column that displays the load pattern names.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property headerLoadPattern As String
        Get
            Return _headerLoadPattern
        End Get
    End Property


    Private _headerStepType As String
    ''' <summary>
    ''' Name of the column that displays the step types.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property headerStepType As String
        Get
            Return _headerStepType
        End Get
    End Property

    Private _stepTypeTime As String
    ''' <summary>
    ''' Name of the step type for by time.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property stepTypeTime As String
        Get
            Return _stepTypeTime
        End Get
    End Property

    Private _stepTypeLastStep As String
    ''' <summary>
    ''' Name of the step type for last step.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property stepTypeLastStep As String
        Get
            Return _stepTypeLastStep
        End Get
    End Property

    Private _program As eCSiProgram
    ''' <summary>
    ''' The program that the current properties are based on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property program As eCSiProgram
        Get
            Return _program
        End Get
    End Property
#End Region

#Region "Initialization"
    Sub New()
        SetTableHeaders(eCSiProgram.ETABS)
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets the table header names expected based on the program.
    ''' </summary>
    ''' <param name="p_program">Program to correspond the headers with.</param>
    ''' <remarks></remarks>
    Friend Sub SetTableHeaders(ByVal p_program As eCSiProgram)
        _program = p_program

        Select Case _program
            Case eCSiProgram.ETABS
                _headerLoadCase = "CaseCombo"
                _headerDesignLoadCombo = "PMMCombo"
                _headerLoadPattern = "LoadPat"
                _headerStepType = "StepType"
                _stepTypeTime = "Time"
                _stepTypeLastStep = "Last Step"
            Case eCSiProgram.SAP2000, eCSiProgram.CSiBridge
                _headerLoadCase = "OutputCase"
                _headerDesignLoadCombo = "Combo"
                _headerLoadPattern = "LoadPat"
                _headerStepType = "StepType"
                _stepTypeTime = "Time"
                _stepTypeLastStep = "Last Step"
            Case eCSiProgram.SAFE
                'TODO
        End Select
    End Sub

    'Result load type
    '''<summary>
    ''' Determines the load type for the specified query.
    ''' Only valid if a load type is referenced in the query.
    ''' </summary>
    ''' <param name="p_queryOrValue">The string containing the word that indicates the value sought.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetLoadType(ByVal p_queryOrValue As String) As eLoadType
        Dim loadTypeString As String

        If StringExistInName(p_queryOrValue, headerLoadCase) Then
            loadTypeString = ExtractHeaderValueFromQuery(p_queryOrValue, headerLoadCase)
            If Not String.IsNullOrEmpty(loadTypeString) Then
                Return eLoadType.loadCase
            Else
                Return eLoadType.none
            End If
        ElseIf StringExistInName(p_queryOrValue, headerDesignLoadCombo) Then
            Return eLoadType.loadCombination
        ElseIf StringExistInName(p_queryOrValue, headerLoadPattern) Then
            Return eLoadType.loadPattern
        Else
            Return eLoadType.none
        End If
    End Function
    ''' <summary>
    ''' Determines the load type for the specified row.
    ''' </summary>
    ''' <param name="p_row">Row data object to check for determining the load type.</param>
    ''' <param name="p_colIndex">Column index to set for marking the location of the load type column.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetLoadType(ByVal p_row As DataRow,
                                           ByRef p_colIndex As Integer) As eLoadType
        For Each column As DataColumn In p_row.Table.Columns
            If StringExistInName(column.ColumnName, headerLoadCase) Then
                Return eLoadType.loadCase
            ElseIf StringExistInName(column.ColumnName, headerDesignLoadCombo) Then
                Return eLoadType.loadCombination
            ElseIf StringExistInName(column.ColumnName, headerLoadPattern) Then
                Return eLoadType.loadPattern
            End If

            p_colIndex += 1
        Next

        Return eLoadType.none
    End Function

    ''' <summary>
    ''' Determines the load pattern name based on the query.
    ''' Only valid if a load pattern name is referenced in the query.
    ''' </summary>
    ''' <param name="p_queryOrValue">The string containing the word that indicates the value sought.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function LoadPatternName(ByVal p_queryOrValue As String) As String
        Dim loadPattern As String = ExtractHeaderValueFromQuery(p_queryOrValue, headerLoadPattern)

        Return loadPattern
    End Function
    ''' <summary>
    ''' Determines the load pattern name based on the provided data row.
    ''' </summary>
    ''' <param name="p_row">Row data object to check for determining the load pattern name.</param>
    ''' <param name="p_colIndex">Column index marking the location of the load type column.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function LoadPatternName(ByVal p_row As DataRow,
                                               ByVal p_colIndex As Integer) As String
        Dim loadPattern As String = p_row(p_colIndex).ToString

        Return loadPattern
    End Function

    ''' <summary>
    ''' Determines the load case name based on the query.
    ''' Only valid if a load pattern name is referenced in the query.
    ''' </summary>
    ''' <param name="p_queryOrValue">The string containing the word that indicates the value sought.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function LoadCaseName(ByVal p_queryOrValue As String) As String
        Dim loadCase As String = ExtractHeaderValueFromQuery(p_queryOrValue, headerLoadCase)

        Return loadCase
    End Function
    ''' <summary>
    ''' Determines the load case name based on the provided data row.
    ''' </summary>
    ''' <param name="p_row">Row data object to check for determining the load case name.</param>
    ''' <param name="p_colIndex">Column index marking the location of the load type column.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function LoadCaseName(ByVal p_row As DataRow,
                                            ByVal p_colIndex As Integer) As String
        Dim loadCase As String = p_row(p_colIndex).ToString

        Return loadCase
    End Function

    ''' <summary>
    ''' Determines the load combination name based on the query.
    ''' Only valid if a load combination is referenced in the query.
    ''' </summary>
    ''' <param name="p_queryOrValue">The string containing the word that indicates the value sought.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function LoadCombinationName(ByVal p_queryOrValue As String) As String
        Dim loadCombination As String = ExtractHeaderValueFromQuery(p_queryOrValue, headerDesignLoadCombo)
        If loadCombination.Length = 0 Then loadCombination = ExtractHeaderValueFromQuery(p_queryOrValue, headerLoadCase)

        Return loadCombination
    End Function
    ''' <summary>
    ''' Determines the load combination name based on the provided data row.
    ''' </summary>
    ''' <param name="p_row">Row data object to check for determining the load combination name.</param>
    ''' <param name="p_colIndex">Column index marking the location of the load type column.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function LoadCombinationName(ByVal p_row As DataRow,
                                                    ByVal p_colIndex As Integer) As String
        Dim loadCombination As String = p_row(p_colIndex).ToString

        Return loadCombination
    End Function

    'Result step type
    ''' <summary>
    ''' Determines the step type of the table output based on the provided query.
    ''' </summary>
    ''' <param name="p_queryOrValue">The string containing the word that indicates the value sought.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetStepType(ByVal p_queryOrValue As String) As eStepType
        Dim stepTypeString As String

        If StringExistInName(p_queryOrValue, headerStepType) Then
            stepTypeString = ExtractHeaderValueFromQuery(p_queryOrValue, headerStepType)
            If String.IsNullOrEmpty(stepTypeString) Then Return eStepType.none

            Select Case stepTypeString
                Case stepTypeTime
                    Return eStepType.stepByStep
                Case stepTypeLastStep
                    Return eStepType.lastStep
                Case Else
                    Return eStepType.envelope
            End Select
        Else
            Return eStepType.none
        End If
    End Function
    ''' <summary>
    ''' Determines the step type of the table output based on the provided data table.
    ''' </summary>
    ''' <param name="p_table">Data table checked to determine the current step type.</param>
    ''' <param name="p_multiStep">The default multiStep type to return if one is not found the table.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetStepType(ByVal p_multiStep As eStepType,
                                          ByVal p_table As DataTable) As eStepType
        Dim colIndex As Integer = 0

        For Each column As DataColumn In p_table.Columns
            If StringExistInName(column.ColumnName, headerStepType) Then
                Select Case p_table.Rows(0).Item(colIndex).ToString
                    Case stepTypeTime
                        Return eStepType.stepByStep
                    Case stepTypeLastStep
                        Return eStepType.lastStep
                    Case Else : Return eStepType.envelope
                End Select
            End If

            colIndex += 1
        Next

        Return p_multiStep
    End Function

    ''' <summary>
    ''' Determines the value that corresponds with the header specified in the query. 
    ''' Returns a blank string if none is found.
    ''' </summary>
    ''' <param name="p_header">Header name corresponding to the value to be returned. 
    ''' "" is returned if the header value is not found.</param>
    ''' <param name="p_queryOrValue">The string containing the word that indicates the value sought.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ExtractHeaderValueFromQuery(ByVal p_queryOrValue As String,
                                                ByVal p_header As String) As String
        Dim stepTypeString As String = GetSuffix(p_queryOrValue, p_header & "=")
        If String.Compare(stepTypeString, p_queryOrValue, True) = 0 Then Return ""

        If Left(stepTypeString, 1) = """" Then stepTypeString = Right(stepTypeString, stepTypeString.Length - 1)
        stepTypeString = GetPrefix(stepTypeString, """")

        Return stepTypeString
    End Function
#End Region

End Class
