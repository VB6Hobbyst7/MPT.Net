Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

''' <summary>
''' Module that contains variables that are accessible across the entire program. Dimensioned as either Public or Friend
''' </summary>
''' <remarks></remarks>
Module mVariablesFriend

#If COMPILE_RELEASE Then

#ElseIf COMPILE_INTERNAL Then

#End If

#Region "Classes"
    Friend csiLogger As cCSiTesterLogger
    Friend testerSettings As cSettings
    Friend myRegTest As cRegTest
    Friend myCsiTester As cCsiTester
    Public myXMLEditor As cXMLEditor
    Friend e2eTester As cE2ETester
#End Region

#Region "Forms"
    Friend windowXMLEditorBulk As frmXMLEditorBulk
    Public myStatusForm As frmStatus
    Public myE2EForm As frmTestingE2E
    Friend windowXMLTemplateGenerator As frmXMLTemplateGenerator
    Friend csiMessageBox As MessageBoxLong
    'Temporary?
    Friend windowCSiTesterTests As CSiTester
    Friend windowXMLTemplateGeneratorUnique As frmXMLTemplateGeneratorUnique
    Friend windowExampleEditor As frmExampleEditor
    Friend windowSuiteOperations As frmSuiteOperations
    Friend windowXMLObjectResults As frmXMLObjectResults
#End Region

#Region "Collections, Lists"
    Public examplesTestSetList As ObservableCollection(Of cExampleTestSet)
#End Region

#Region "Properties & Variables"
    ''' <summary>
    ''' If the directory containing the *.ini file exists and allows creating/writing the file, this is True. Else, False.
    ''' </summary>
    ''' <remarks></remarks>
    Friend iniAccessible As Boolean
    ''' <summary>
    ''' If true, then the tester is starting up from a location different than when it had last written to the settings files.
    ''' </summary>
    ''' <remarks></remarks>
    Friend testerLocationChanged As Boolean
    ''' <summary>
    ''' If true, then an end-to-end/functional test is running. This will affect some automated behaviors in the program.
    ''' </summary>
    ''' <remarks></remarks>
    Friend e2eTestingRunning As Boolean
#End Region

End Module
