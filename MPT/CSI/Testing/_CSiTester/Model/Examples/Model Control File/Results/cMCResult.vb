Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Data

Imports MPT.String.ConversionLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cProgramControl
Imports CSiTester.cMCQuery
Imports CSiTester.cMCResultIDs

''' <summary>
''' A class containing results that are checked for the example to determine if results have changed, and how closely they match independent values. 
''' It also contains the information necessary to look up the results.
''' </summary>
''' <remarks></remarks>
Public Class cMCResult
    Inherits cMCResultBasic
    Implements ICloneable

#Region "Properties"
    Private _tableFieldProperties As New cFieldProperties
    ''' <summary>
    ''' Contains information about the table fields, such as which headers indicate a column of load cases.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property tableFieldProperties As cFieldProperties
        Get
            Return _tableFieldProperties
        End Get
    End Property
#End Region

#Region "Properties: XML File"
    ''' <summary>
    ''' Name of the table queried to look up the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property tableName As String
        Set(ByVal value As String)
            If Not _tableName = value Then
                _tableName = value
                RaisePropertyChanged("tableName")
            End If
        End Set
        Get
            Return _tableName
        End Get
    End Property

    ''' <summary>
    ''' Field names and values used to look up the unique output cell to check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property query As cMCQuery
        Set(ByVal value As cMCQuery)
            _query = value
            RaisePropertyChanged("query")
        End Set
        Get
            Return _query
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        _resultType = cMCModel.eResultType.regular
    End Sub

    Protected Overrides Function Create() As cMCResultBasic
        Return New cMCResult()
    End Function
#End Region

#Region "Methods: Override/Overload/Implement"
    Friend Overrides Function Clone() As Object
        Dim myClone As cMCResult = DirectCast(MyBase.Clone, cMCResult)

        Try
            With myClone
                .query = DirectCast(query.Clone, cMCQuery)

                .tableName = tableName
            End With
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCResult) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCResult = TryCast(p_object, cMCResult)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not .idTemp = idTemp Then Return False

            If Not .query.Equals(query) Then Return False

            If Not .tableName = tableName Then Return False
        End With

        Return True
    End Function

    ''' <summary>
    ''' Sorts first by table name, then query ID, then benchmark ID.
    ''' </summary>
    ''' <param name="other"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function CompareTo(other As cMCResultBasic) As Integer
        If tableName.CompareTo(other.tableName) <> 0 Then
            Return tableName.CompareTo(other.tableName)
        ElseIf MyBase.CompareTo(other) <> 0 Then
            Return MyBase.CompareTo(other)
        Else
            Return 0
        End If
    End Function

    Public Overrides Function ToString() As String
        Return (MyBase.ToString() & ": ID " & id & " - [" & tableName & "]")
    End Function

#End Region

#Region "Methods: Friend"

    'Query Methods
    ''' <summary>
    ''' Returns the current classification of the query based on a provided DataTable.
    ''' If a proper DataTable is submitted (e.g. matching table name), then the relevant object properties will also be updated.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to apply the query to.</param>
    ''' <param name="p_updateResult">If 'True', then the corresponding result properties will be updated based on the result.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetQueryType(ByVal p_dataTable As DataTable,
                                 Optional ByVal p_updateResult As Boolean = False) As eQueryType
        Me.SetIsComplete()
        Return query.GetSetQueryType(p_dataTable, _tableName, p_updateResult)
    End Function

    'Table Header-Related
    ''' <summary>
    ''' Sets the properties related to table output to reflect the specified program.
    ''' </summary>
    ''' <param name="p_program">Program that table output is based on.</param>
    ''' <remarks></remarks>
    Friend Sub SetTableHeaders(ByVal p_program As eCSiProgram)
        tableFieldProperties.SetTableHeaders(p_program)
    End Sub
    ''' <summary>
    ''' Determines the step type of the table output associated with the result.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetStepType() As eStepType
        Return tableFieldProperties.GetStepType(query.asString)
    End Function
    ''' <summary>
    ''' Determines the load type of the result.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetLoadType() As eLoadType
        Return tableFieldProperties.GetLoadType(query.asString)
    End Function
    ''' <summary>
    ''' Determines the load pattern name associated with the result..
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadPatternName() As String
        Return tableFieldProperties.LoadPatternName(query.asString)
    End Function
    ''' <summary>
    ''' Determines the load case name associated with the result.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadCaseName() As String
        Return tableFieldProperties.LoadCaseName(query.asString)
    End Function
    ''' <summary>
    ''' Determines the load combination name associated with the result.
    ''' If none is found, the load case name is returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadCombinationName() As String
        Return tableFieldProperties.LoadCombinationName(query.asString)
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Updates the string representation of the complete ID for the result based on the component query &amp; benchmark IDs.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub UpdateResultIDFromIDComponents()
        _id = CStr(query.ID) & "." & CStr(benchmark.ID).PadLeft(CStr(MAX_BENCHMARK_ID).Length, CChar("0"))
        SetResultNewSynced()
    End Sub
#End Region

End Class
