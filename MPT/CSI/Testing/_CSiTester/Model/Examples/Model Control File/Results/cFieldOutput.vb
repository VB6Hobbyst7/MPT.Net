Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Enums
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger

Imports CSiTester.cMCResultIDs


''' <summary>
''' Class containing information relating to the result output, such as the field name for looking up the result, and the expected values of the result.
''' </summary>
''' <remarks></remarks>
Public Class cFieldOutput
    Inherits PropertyChanger
    Implements ICloneable
    Implements IComparable(Of cFieldOutput)

#Region "Properties"

    Private _id As Integer
    ''' <summary>
    ''' Number identifying the benchmark.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            If (Not value = _id AndAlso
                0 <= value AndAlso value <= MAX_BENCHMARK_ID) Then
                _id = value
                RaisePropertyChanged("ID")
            End If
        End Set
    End Property

    Private _name As String
    ''' <summary>
    ''' Name of the column/header of the field to reference.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
        Set(ByVal value As String)
            If Not _name = value Then
                _name = value
                RaisePropertyChanged("name")
            End If
        End Set
        Get
            Return _name
        End Get
    End Property

    Private _zeroTolerance As Double
    ''' <summary>
    ''' Limiting value for results that should be treated as zero. 
    ''' If absolute value of a result is smaller than or equal to this limit, then the result will be essentially rounded to zero.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property zeroTolerance As Double
        Set(ByVal value As Double)
            If Not _zeroTolerance = value Then
                _zeroTolerance = value
                RaisePropertyChanged("zeroTolerance")
            End If
        End Set
        Get
            Return _zeroTolerance
        End Get
    End Property

    Private _shiftCalc As Double
    ''' <summary>
    ''' Optional attribute to specify shift when calculating percent difference. 
    ''' This can be useful to obtain meaningful results for benchmarks that are noisy and near zeros.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property shiftCalc As Double
        Set(ByVal value As Double)
            If Not _shiftCalc = value Then
                _shiftCalc = value
                RaisePropertyChanged("shiftCalc")
            End If
        End Set
        Get
            Return _shiftCalc
        End Get
    End Property

    Private _valuePassingPercentDifferenceRange As Double
    ''' <summary>
    ''' Optional attribute to specifies a % for a +/- range within which a % difference in result comparisons is still considered passing for the model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valuePassingPercentDifferenceRange As Double
        Set(ByVal value As Double)
            If Not _valuePassingPercentDifferenceRange = value Then
                _valuePassingPercentDifferenceRange = value
                RaisePropertyChanged("valuePassingPercentDifferenceRange")
            End If
        End Set
        Get
            Return _valuePassingPercentDifferenceRange
        End Get
    End Property

    Private _valueTable As String
    ''' <summary>
    ''' Value of the table entry from which the benchmark is derived.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueTable As String
        Set(ByVal value As String)
            If Not _valueTable = value Then
                TrimQuotesSingle(value)
                _valueTable = value
                RaisePropertyChanged("valueTable")
            End If
        End Set
        Get
            Return _valueTable
        End Get
    End Property

    ' Recorded Benchmark Properties
    Private _valueBenchmark As String
    ''' <summary>
    ''' Value of the benchmark to compare the output against.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueBenchmark As String
        Set(ByVal value As String)
            If Not _valueBenchmark = value Then
                TrimQuotesSingle(value)
                _valueBenchmark = value

                If value = "0" Then zeroTolerance = 1
                RaisePropertyChanged("valueBenchmark")
            End If
        End Set
        Get
            Return _valueBenchmark
        End Get
    End Property

    Private _roundBenchmark As String
    ''' <summary>
    ''' Number of significant digits to which the calculated and benchmark values should be rounded before calculating percent difference. 
    ''' This is an optional attribute. If not supplied, the number of significant digits is taken to be the same as there are significant digits in the benchmark value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property roundBenchmark As String
        Set(ByVal value As String)
            If Not _roundBenchmark = value Then
                _roundBenchmark = value
                RaisePropertyChanged("roundBenchmark")
            End If
        End Set
        Get
            Return _roundBenchmark
        End Get
    End Property

    Private _overrideBenchmark As Boolean
    ''' <summary>
    ''' If true, benchmark has been changed from the table value manually by the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property overrideBenchmark As Boolean
        Set(ByVal value As Boolean)
            If Not _overrideBenchmark = value Then
                _overrideBenchmark = value
                RaisePropertyChanged("overrideBenchmark")
            End If
        End Set
        Get
            Return _overrideBenchmark
        End Get
    End Property

    Private _isCorrect As eYesNoUnknown
    ''' <summary>
    ''' True: Benchmark value is known to be correct, usually matching a theoretical value. False, the value is known to be incorrect. If unknown, value is blank.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isCorrect As eYesNoUnknown
        Set(ByVal value As eYesNoUnknown)
            If Not _isCorrect = value Then
                _isCorrect = value
                RaisePropertyChanged("isCorrect")
            End If
        End Set
        Get
            Return _isCorrect
        End Get
    End Property

    ' Alternative Benchmark Properties
    Private _valueTheoretical As String
    ''' <summary>
    '''Value of the theoretical answer to compare the output against. This is from either a hand calculation or a published independent reference.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueTheoretical As String
        Set(ByVal value As String)
            If Not _valueTheoretical = value Then
                _valueTheoretical = value
                RaisePropertyChanged("valueTheoretical")
            End If
        End Set
        Get
            Return _valueTheoretical
        End Get
    End Property

    Private _roundTheoretical As String
    ''' <summary>
    ''' Number of significant digits to which the theoretical values should be rounded before calculating percent difference. 
    ''' This is an optional attribute. If not supplied, the number of significant digits is taken to be the same as there are significant digits in the theoretical value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property roundTheoretical As String
        Set(ByVal value As String)
            If Not _roundTheoretical = value Then
                _roundTheoretical = value
                RaisePropertyChanged("roundTheoretical")
            End If
        End Set
        Get
            Return _roundTheoretical
        End Get
    End Property


    Private _valueLastBest As String
    ''' <summary>
    ''' Value of the last best benchmark achieved to compare the output against.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueLastBest As String
        Set(ByVal value As String)
            If Not _valueLastBest = value Then
                _valueLastBest = value
                RaisePropertyChanged("valueLastBest")
            End If
        End Set
        Get
            Return _valueLastBest
        End Get
    End Property

    Private _roundLastBest As String
    ''' <summary>
    ''' Number of significant digits to which the last best values should be rounded before calculating percent difference. 
    ''' This is an optional attribute. If not supplied, the number of significant digits is taken to be the same as there are significant digits in the theoretical value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property roundLastBest As String
        Set(ByVal value As String)
            If Not _roundLastBest = value Then
                _roundLastBest = value
                RaisePropertyChanged("roundLastBest")
            End If
        End Set
        Get
            Return _roundLastBest
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        isCorrect = eYesNoUnknown.unknown
    End Sub

    Friend Overridable Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As cFieldOutput = Create()

        With myClone
            .ID = ID
            .isCorrect = isCorrect
            .name = name
            .zeroTolerance = zeroTolerance
            .shiftCalc = shiftCalc
            .valuePassingPercentDifferenceRange = valuePassingPercentDifferenceRange
            .overrideBenchmark = overrideBenchmark
            .valueTable = valueTable
            .valueBenchmark = valueBenchmark
            .valueLastBest = valueLastBest
            .valueTheoretical = valueTheoretical
            .roundBenchmark = roundBenchmark
            .roundTheoretical = roundTheoretical
            .roundLastBest = roundLastBest
        End With

        Return myClone
    End Function

    Protected Overridable Function Create() As cFieldOutput
        Return New cFieldOutput()
    End Function
#End Region

#Region "Methods: Override/Overload/Implement"
    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cFieldOutput) Then Return False
        Dim isMatch As Boolean = True
        Dim comparedObject As cFieldOutput = TryCast(p_object, cFieldOutput)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            ' Note: Do not compare IDs
            If Not .isCorrect = isCorrect Then Return False
            If Not .name = name Then Return False
            If Not .overrideBenchmark = overrideBenchmark Then Return False
            If Not .valueTable = valueTable Then Return False
            If Not .shiftCalc = shiftCalc Then Return False
            If Not .zeroTolerance = zeroTolerance Then Return False
            If Not .valuePassingPercentDifferenceRange = valuePassingPercentDifferenceRange Then Return False
            If Not .valueBenchmark = valueBenchmark Then Return False
            If Not .valueTheoretical = valueTheoretical Then Return False
            If Not .valueLastBest = valueLastBest Then Return False
            If Not .roundBenchmark = roundBenchmark Then Return False
            If Not .roundTheoretical = roundTheoretical Then Return False
            If Not .roundLastBest = roundLastBest Then Return False
        End With

        Return True
    End Function

    Public Function CompareTo(other As cFieldOutput) As Integer Implements IComparable(Of cFieldOutput).CompareTo
        If ID.CompareTo(other.ID) <> 0 Then
            Return ID.CompareTo(other.ID)
        Else
            Return 0
        End If
    End Function

    Public Overrides Function ToString() As String
        Return (MyBase.ToString() & ": ID " & ID & " - " & name)
    End Function
#End Region

End Class
