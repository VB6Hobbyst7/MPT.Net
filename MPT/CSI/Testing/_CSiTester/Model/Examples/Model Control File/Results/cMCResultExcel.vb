Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cLibFolders

''' <summary>
''' A class with directions to an Excel file that stores or calculates results using output from the analysis program. 
''' Regtest is able to interact with Excel to extract these results from new runs.
''' </summary>
''' <remarks></remarks>
Public Class cMCResultExcel
    Inherits cMCFile
    Implements ICloneable
    Implements INotifyPropertyChanged

#Region "Properties"

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Initializes the object with the path to an existing file.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_path As String)
        MyBase.New(p_path)
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myBaseClone As cMCFile = CType(MyBase.Clone, cMCFile)
        Dim myClone As cMCResultExcel = CType(myBaseClone, cMCResultExcel)

        With myClone
            .path = path
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided does not perfectly match the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function IsDifferent(ByVal p_object As cMCResultExcel) As Boolean
        With p_object
            If MyBase.IsDifferent(p_object) Then Return True
            If .path.IsDifferent(path) Then Return True
        End With

        Return False
    End Function
#End Region

#Region "Methods: Friend"

#End Region

End Class
