Option Explicit On
Option Strict On

Imports System.Data
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.Units

''' <summary>
''' Contains data relevant to the table files exported from CSi products.
''' </summary>
''' <remarks></remarks>
Public Class cProgramControl
    Inherits PropertyChanger
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Enumerations"
    ''' <summary>
    ''' Force units possible for export from a CSI product.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eForceUnit
        <Description("None")> none = 0
        <Description("lb")> lb
        <Description("kip")> kip
        <Description("N")> N
        <Description("kN")> kN
        <Description("kgf")> kgf
        <Description("tonf")> tonf
    End Enum

    ''' <summary>
    ''' Length units possible for export from a CSI product.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eLengthUnit
        <Description("None")> none = 0
        <Description("in")> inches
        <Description("ft")> feet
        <Description("micron")> micron
        <Description("mm")> millimeter
        <Description("cm")> centimeter
        <Description("m")> meter
    End Enum

    ''' <summary>
    ''' Temperature units possible for export from a CSI product.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eTemperatureUnit
        <Description("None")> none = 0
        <Description("C")> Celcius
        <Description("F")> Farenheit
    End Enum

    ''' <summary>
    ''' Step types possible for export from a CSI product.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eStepType
        <Description("")> none
        <Description("Envelope")> envelope
        <Description("Step-by-Step")> stepByStep
        <Description("Last Step")> lastStep
    End Enum

    ''' <summary>
    ''' Load types possible for export from a CSI product.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eLoadType
        <Description("")> none
        <Description("Load Pattern")> loadPattern
        <Description("Load Case")> loadCase
        <Description("Load Combination")> loadCombination
    End Enum
#End Region

#Region "Fields:"
    ''' <summary>
    ''' String listing of units from Program Control.
    ''' </summary>
    ''' <remarks></remarks>
    Private _unitsString As String

    ''' <summary>
    ''' Model control object associated with the program control object.
    ''' </summary>
    ''' <remarks></remarks>
    Private _mcModel As cMCModel
#End Region

#Region "Properties: Friend"
    Private _programName As String
    ''' <summary>
    ''' Name of the program that exported the table file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property programName As String
        Get
            Return _programName
        End Get
    End Property

    Private _version As String
    ''' <summary>
    ''' Version of the program that exported the table file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property version As String
        Get
            Return _version
        End Get
    End Property

    Private _level As String
    ''' <summary>
    ''' Level of the program that exported the table file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property level As String
        Get
            Return _level
        End Get
    End Property

    Private _baseUnits As New ObservableCollection(Of cUnitsController)
    ''' <summary>
    ''' Database units that the table file was exported in.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property baseUnits As ObservableCollection(Of cUnitsController)
        Get
            Return New ObservableCollection(Of cUnitsController)(_baseUnits)
        End Get
    End Property

    Private _unitsLength As String
    ''' <summary>
    ''' Name of the length units used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsLength As String 'eLengthUnit
        Get
            Return _unitsLength
        End Get
    End Property

    Private _unitsForce As String
    ''' <summary>
    ''' Name of the force units used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsForce As String 'eForceUnit
        Get
            Return _unitsForce
        End Get
    End Property

    Private _unitsTemperature As String
    ''' <summary>
    ''' Name of the temperature units used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsTemperature As String 'eTemperatureUnit
        Get
            Return _unitsTemperature
        End Get
    End Property

    Private _unitsRotation As String
    ''' <summary>
    ''' Name of the rotation units used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsRotation As String
        Get
            Return _unitsRotation
        End Get
    End Property

    Private _unitsTime As String
    ''' <summary>
    ''' Name of the time units used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsTime As String
        Get
            Return _unitsTime
        End Get
    End Property

    Private _codeSteel As String
    ''' <summary>
    ''' Steel design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeSteel As String
        Get
            Return _codeSteel
        End Get
    End Property

    Private _codeConcrete As String
    ''' <summary>
    ''' Concrete design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeConcrete As String
        Get
            Return _codeConcrete
        End Get
    End Property

    Private _codeCompositeBeam As String
    ''' <summary>
    ''' Composite beam design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeCompositeBeam As String
        Get
            Return _codeCompositeBeam
        End Get
    End Property

    Private _codeCompositeColumn As String
    ''' <summary>
    ''' Composite column design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeCompositeColumn As String
        Get
            Return _codeCompositeColumn
        End Get
    End Property

    Private _codeWall As String
    ''' <summary>
    ''' Shear wall design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeWall As String
        Get
            Return _codeWall
        End Get
    End Property

    Private _codeAluminum As String
    ''' <summary>
    ''' Aluminum design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeAluminum As String
        Get
            Return _codeAluminum
        End Get
    End Property

    Private _codeColdSteel As String
    ''' <summary>
    ''' Cold-formed steel design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeColdSteel As String
        Get
            Return _codeColdSteel
        End Get
    End Property

    Private _codeConnection As String
    ''' <summary>
    ''' Connection design code set in the model file from which the table file was exported.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property codeConnection As String
        Get
            Return _codeConnection
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    Friend Sub New(ByVal p_mcModel As cMCModel,
                   ByVal p_pathDataSource As String)
        FillData(p_pathDataSource)
        _mcModel = p_mcModel
        _mcModel.programControl = Me
    End Sub
    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cProgramControl

        With myClone
            ._programName = programName
            ._version = version
            ._level = level

            For Each unitCtlr As cUnitsController In baseUnits
                ._baseUnits.Add(CType(unitCtlr.Clone, cUnitsController))
            Next
            ._unitsLength = unitsLength
            ._unitsForce = unitsForce
            ._unitsTemperature = unitsTemperature
            ._unitsRotation = unitsRotation
            ._unitsTime = unitsTime

            ._codeSteel = codeSteel
            ._codeConcrete = codeConcrete
            ._codeCompositeBeam = codeCompositeBeam
            ._codeCompositeColumn = codeCompositeColumn
            ._codeWall = codeWall
            ._codeAluminum = codeAluminum
            ._codeColdSteel = codeColdSteel
            ._codeConnection = codeConnection
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cProgramControl) Then Return False
        Dim isMatch As Boolean
        Dim comparedObject As cProgramControl = TryCast(p_object, cProgramControl)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .programName = programName Then Return False
            If Not .version = version Then Return False
            If Not .level = level Then Return False

            For Each unitCtlrOuter As cUnitsController In .baseUnits
                isMatch = False
                For Each unitCtlr As cUnitsController In baseUnits
                    If Not unitCtlrOuter.Equals(unitCtlr) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            If Not .unitsLength = unitsLength Then Return False
            If Not .unitsForce = unitsForce Then Return False
            If Not .unitsTemperature = unitsTemperature Then Return False
            If Not .unitsRotation = unitsRotation Then Return False
            If Not .unitsTime = unitsTime Then Return False

            If Not .codeSteel = codeSteel Then Return False
            If Not .codeConcrete = codeConcrete Then Return False
            If Not .codeCompositeBeam = codeCompositeBeam Then Return False
            If Not .codeCompositeColumn = codeCompositeColumn Then Return False
            If Not .codeWall = codeWall Then Return False
            If Not .codeAluminum = codeAluminum Then Return False
            If Not .codeColdSteel = codeColdSteel Then Return False
            If Not .codeConnection = codeConnection Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Populates the data of the class from the specified table file.
    ''' </summary>
    ''' <param name="p_dataSource">Table file exported from a CSi product to use.</param>
    ''' <remarks></remarks>
    Friend Sub FillData(ByVal p_dataSource As String)
        If Not IO.File.Exists(p_dataSource) Then Exit Sub

        Dim dtController As New cDataTableController
        Dim pcTable As DataTable = dtController.DataTableFromFile(p_dataSource, "[Program Control]")
        If pcTable Is Nothing Then Exit Sub

        'Get program information
        Dim programNameCurrent As String = GetProgramControlStrings(pcTable, "ProgramName")

        '   Only set the information if it is of a matching program
        If _mcModel IsNot Nothing Then
            If _mcModel.targetProgram.Contains(ConvertStringToEnumByDescription(Of eCSiProgram)(programNameCurrent)) Then
                _programName = programNameCurrent
            Else
                Exit Sub
            End If
        Else
            _programName = programNameCurrent
        End If

        _version = GetProgramControlStrings(pcTable, "Version")
        _level = GetProgramControlStrings(pcTable, "Level", "ProgLevel")

        'Get units information
        SetUnits(pcTable)

        'Get design codes
        _codeSteel = GetProgramControlStrings(pcTable, "SteelCode")
        _codeConcrete = GetProgramControlStrings(pcTable, "ConcCode")
        _codeCompositeBeam = GetProgramControlStrings(pcTable, "CompBmCode")
        _codeCompositeColumn = GetProgramControlStrings(pcTable, "CompColCode")
        _codeWall = GetProgramControlStrings(pcTable, "WallCode")
        _codeAluminum = GetProgramControlStrings(pcTable, "AlumCode")
        _codeColdSteel = GetProgramControlStrings(pcTable, "ColdCode")
        _codeConnection = GetProgramControlStrings(pcTable, "ConnCode")
    End Sub

    ''' <summary>
    ''' Generates a new program control object, populated by the specified datasource, and attaches itself to the specified model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to associate with the program control object.</param>
    ''' <param name="p_pathDataSource">Path to the data source from which the program control data is to be read.</param>
    ''' <param name="p_overrideIfNotEmpty">True: The existing program control object of the model control file will be replaced even if it is currently populated. 
    ''' False: Operation will only be performed if the program control object of the model control file is currently empty.</param>
    ''' <remarks></remarks>
    Friend Sub SetProgramControlData(ByRef p_mcModel As cMCModel,
                                     ByVal p_pathDataSource As String,
                                     Optional ByVal p_overrideIfNotEmpty As Boolean = False)
        If ShouldSetProgramControlData(p_mcModel, p_pathDataSource, p_overrideIfNotEmpty) Then
            Dim pc As New cProgramControl
            pc.FillData(p_pathDataSource)

            If Not String.IsNullOrEmpty(pc.programName) Then p_mcModel.programControl = pc
        End If
    End Sub
#End Region


#Region "Methods: Private"
    ''' <summary>
    ''' Returns the string value of the specified column. 
    ''' If the column does not exist, an empty string is returned.
    ''' A second chance name can be tried in case the name is different.
    ''' </summary>
    ''' <param name="p_table">Table object.</param>
    ''' <param name="p_columnName">Name of the column.</param>
    ''' <param name="p_columnNameSecondary">Alternate name possibility for the column</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProgramControlStrings(ByVal p_table As DataTable,
                                              ByVal p_columnName As String,
                                              Optional ByVal p_columnNameSecondary As String = "") As String
        Try
            Dim dtCol As DataColumn = p_table.Columns(p_columnName)

            If dtCol IsNot Nothing Then
                Return p_table.Rows(0).Item(p_columnName).ToString
            ElseIf (dtCol Is Nothing AndAlso Not String.IsNullOrEmpty(p_columnNameSecondary)) Then
                dtCol = p_table.Columns(p_columnNameSecondary)
                If dtCol IsNot Nothing Then Return p_table.Rows(0).Item(p_columnNameSecondary).ToString
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Assigns the various units properties from the specified table and column.
    ''' </summary>
    ''' <param name="p_table">Table object.</param>
    ''' <remarks></remarks>
    Private Sub SetUnits(ByVal p_table As DataTable)
        Try
            Dim unitsList As New List(Of String)
            Dim timeUnit As New cUnitTime
            Dim rotationUnit As New cUnitRotation

            'Get list of units exported from the program
            _unitsString = GetProgramControlStrings(p_table, "CurrUnits")
            unitsList = ParseStringToList(_unitsString, ",")

            'Assign the string properties
            If unitsList.Count > 0 Then
                _unitsForce = cProgramControlUnitsAdapter.ReconcileForceFromProgram(unitsList(0))
            End If
            If unitsList.Count > 1 Then
                _unitsLength = cProgramControlUnitsAdapter.ReconcileLengthFromProgram(unitsList(1))
            End If
            If unitsList.Count > 2 Then _unitsTemperature = unitsList(2)

            '= Add additional units by default
            _unitsTime = GetEnumDescription(timeUnit.unitDefault)
            _unitsRotation = GetEnumDescription(rotationUnit.unitDefault)

            'Add additional units to list
            unitsList.Add(unitsTime)
            unitsList.Add(unitsRotation)

            'Create list of units objects
            _baseUnits.Clear()
            For Each unitName As String In unitsList
                Dim unitCtrl As New cUnitsController

                unitCtrl.ParseStringToUnits(unitName)
                _baseUnits.Add(unitCtrl)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns True if the program control object should be set.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to associate with the program control object.</param>
    ''' <param name="p_pathDataSource">Path to the data source from which the program control data is to be read.</param>
    ''' <param name="p_overrideIfNotEmpty">True: The existing program control object of the model control file will be replaced even if it is currently populated. 
    ''' False: Operation will only be performed if the program control object of the model control file is currently empty.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShouldSetProgramControlData(ByVal p_mcModel As cMCModel,
                                                 ByVal p_pathDataSource As String,
                                                 Optional ByVal p_overrideIfNotEmpty As Boolean = False) As Boolean
        If (IO.File.Exists(p_pathDataSource) AndAlso
             (p_overrideIfNotEmpty OrElse
              String.IsNullOrEmpty(p_mcModel.programControl.programName))) Then

            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
