Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Time.TimeLibrary

Public Class frmTimeFactor
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Variables"
    Private _cleanUpForm As Boolean
#End Region

#Region "Properties"
    Private _timeFactor As Double
    ''' <summary>
    ''' Factor that is multiplied by the actual run time to get an assumed run time.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeFactor As Double
        Set(ByVal value As Double)
            If Not _timeFactor = value Then
                _timeFactor = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("timeFactor"))
            End If
        End Set
        Get
            Return _timeFactor
        End Get
    End Property

    Private _myRunTimeActual As Double
    ''' <summary>
    ''' Actual run time of the example, according to the model results XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myRunTimeActual As Double
        Set(ByVal value As Double)
            If Not _myRunTimeActual = value Then
                _myRunTimeActual = value
                myRunTimeActualString = ConvertTimesStringMinutes(value)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myRunTimeActual"))
            End If
        End Set
        Get
            Return _myRunTimeActual
        End Get
    End Property
    Private _myRunTimeActualString As String
    ''' <summary>
    ''' Actual run time of the example, according to the model results XML file. In time/string format.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myRunTimeActualString As String
        Set(ByVal value As String)
            If Not _myRunTimeActualString = value Then
                _myRunTimeActualString = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myRunTimeActualString"))
            End If
        End Set
        Get
            Return _myRunTimeActualString
        End Get
    End Property

    Private _myRunTimeAssumed As Double
    ''' <summary>
    ''' Assumed run time after multiplying the actual run time by the specified factor.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myRunTimeAssumed As Double
        Set(ByVal value As Double)
            If Not _myRunTimeAssumed = value Then
                _myRunTimeAssumed = value
                myRunTimeAssumedString = ConvertTimesStringMinutes(value)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("runTimeAssumed"))
            End If
        End Set
        Get
            Return _myRunTimeAssumed
        End Get
    End Property
    Private _myRunTimeAssumedString As String
    ''' <summary>
    ''' Assumed run time after multiplying the actual run time by the specified factor. In time/string format.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myRunTimeAssumedString As String
        Set(ByVal value As String)
            If Not _myRunTimeAssumedString = value Then
                _myRunTimeAssumedString = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myRunTimeAssumedString"))
            End If
        End Set
        Get
            Return _myRunTimeAssumedString
        End Get
    End Property

    Public Property formCanceled As Boolean
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeControls()

        _cleanUpForm = True
    End Sub

    ''' <summary>
    ''' Main initialization function.
    ''' </summary>
    ''' <param name="runTimeActual">Actual run time to be used as the basis for a new time estimate.</param>
    ''' <param name="timeFactorDefault">Default time factor to load in the form to apply for the time estimate. Can be respecified in the form.</param>
    ''' <param name="bulkEditing">If True, the preview fields will be hidden as they only apply on a case-by-case basis.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal runTimeActual As Double, Optional ByVal timeFactorDefault As Double = 1, Optional ByVal bulkEditing As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(runTimeActual, timeFactorDefault)
        InitializeControls(bulkEditing)
    End Sub

    Private Sub InitializeData(ByVal runTimeActual As Double, Optional ByVal timeFactorDefault As Double = 1)
        myRunTimeActual = runTimeActual
        timeFactor = timeFactorDefault
        myRunTimeAssumed = myRunTimeActual * timeFactor

        _cleanUpForm = True
    End Sub

    ''' <summary>
    ''' Initializes controls in the form.
    ''' </summary>
    ''' <param name="bulkEditing">If True, the preview fields will be hidden as they only apply on a case-by-case basis.</param>
    ''' <remarks></remarks>
    Private Sub InitializeControls(Optional ByVal bulkEditing As Boolean = False)
        txtBxTimeOld.IsEnabled = False
        txtBxTimeNew.IsEnabled = False

        If bulkEditing Then
            dgRowPreviewActual.Height = New GridLength(0, GridUnitType.Pixel)
            dgRowPreviewAssumed.Height = New GridLength(0, GridUnitType.Pixel)
        End If
    End Sub
#End Region

#Region "Form Controls"
    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs) Handles btnOK.Click
        formCanceled = False
        _cleanUpForm = False
        Me.Close()
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        formCanceled = True
        _cleanUpForm = False
        Me.Close()
    End Sub

    ''' <summary>
    ''' Updates the assumed run time property.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBxNumber_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxNumber.LostFocus
        myRunTimeAssumed = myRunTimeActual * timeFactor
    End Sub
#End Region

#Region "Form Behavior"

    ''' <summary>
    ''' All actions to be done whenever the form is closed occur here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Window_Closing(sender As Object, e As CancelEventArgs)
        If _cleanUpForm Then formCanceled = True
    End Sub
#End Region

End Class
