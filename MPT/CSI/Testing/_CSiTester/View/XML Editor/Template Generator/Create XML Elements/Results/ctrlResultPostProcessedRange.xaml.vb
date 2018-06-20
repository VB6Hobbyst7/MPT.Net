Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.ViewModel

Namespace View
    Public Class ctrlResultPostProcessedRange
        Implements INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
        Private _viewModel As New ViewModel.ResultPostProcessedRangeVM
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property viewModel As ViewModel.ResultPostProcessedRangeVM
            Set(ByVal value As ViewModel.ResultPostProcessedRangeVM)
                _viewModel = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("viewModel"))
            End Set
            Get
                Return _viewModel
            End Get
        End Property

#End Region

#Region "Initialize"
        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            SetDefaults()
        End Sub

        Private Sub SetDefaults()
            SetFromViewModelRangeType()
        End Sub

#End Region

#Region "Form Controls"
        Private Sub chkBxNone_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxNone.Checked
            spRange.Visibility = Windows.Visibility.Collapsed
            radBtnFullRange.IsChecked = False
            SetViewModelRangeType()
        End Sub

        Private Sub chkBxNone_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxNone.Unchecked
            spRange.Visibility = Windows.Visibility.Visible
            radBtnFullRange.IsChecked = True
            SetViewModelRangeType()
        End Sub

        Private Sub radBtnFullRange_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnFullRange.Checked
            spRangeLabel.Visibility = Windows.Visibility.Collapsed
            spRangeControls.Visibility = Windows.Visibility.Collapsed
            SetViewModelRangeType()
        End Sub

        Private Sub radBtnCustomRange_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnCustomRange.Checked
            spRangeLabel.Visibility = Windows.Visibility.Visible
            spRangeControls.Visibility = Windows.Visibility.Visible
            SetViewModelRangeType()
        End Sub
#End Region

#Region "Private"
        Private Sub SetFromViewModelRangeType()
            Select Case _viewModel.rangeType
                Case Global.CSiTester.ViewModel.ResultPostProcessedRangeVM.eRangeType.None
                    chkBxNone.IsChecked = True
                Case Global.CSiTester.ViewModel.ResultPostProcessedRangeVM.eRangeType.RangeAll
                    radBtnFullRange.IsChecked = True
                Case Global.CSiTester.ViewModel.ResultPostProcessedRangeVM.eRangeType.RangeCustom
                    radBtnCustomRange.IsChecked = True
            End Select
        End Sub

        Private Sub SetViewModelRangeType()
            If chkBxNone.IsChecked Then
                _viewModel.rangeType = Global.CSiTester.ViewModel.ResultPostProcessedRangeVM.eRangeType.None
            ElseIf radBtnFullRange.IsChecked Then
                _viewModel.rangeType = Global.CSiTester.ViewModel.ResultPostProcessedRangeVM.eRangeType.RangeAll
            ElseIf radBtnCustomRange.IsChecked Then
                _viewModel.rangeType = Global.CSiTester.ViewModel.ResultPostProcessedRangeVM.eRangeType.RangeCustom
            End If
        End Sub
#End Region

    End Class
End Namespace

