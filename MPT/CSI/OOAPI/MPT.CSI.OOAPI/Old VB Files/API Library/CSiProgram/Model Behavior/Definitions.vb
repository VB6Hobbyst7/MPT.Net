Option Strict On
Option Explicit On

Imports CSiApiTester.cEnumerations

Public Class Definitions
#Region "Methods: Public"
    '= Properties
    '== Areas
    ' Get/Set
    ' -----
    ''' <summary>
    ''' Retrieves the method to determine the notional size of an area section for the creep and shrinkage calculations. 
    ''' This function is currently worked for shell type area section.
    ''' </summary>
    ''' <param name="nameShellSection">The name of an existing shell-type area section property.</param>
    ''' <param name="notionalSectionType">The type of notional size that is defined for the section.</param>
    ''' <param name="value">For notionalSectionType "Auto", the Value represents for the scale factor to the program-determined notional size. 
    ''' For notionalSectionType “User”, the Value represents for the user-defined notional size [L]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNotionalSize(ByVal nameShellSection As String,
                                    ByRef notionalSectionType As NotionalSectionType,
                                    ByRef value As Double) As Boolean
        Dim notionalSectionTypeStr As String = ""

        ret = SapModel.PropArea.GetNotionalSize(nameShellSection, notionalSectionTypeStr, value)


        notionalSectionType = ConvertStringToEnumByDescription(Of NotionalSectionType)(notionalSectionTypeStr)

        Return retCheck(ret, "SapModel.PropArea.GetNotionalSize")
    End Function

    ''' <summary>
    ''' Sets the method to determine the notional size of an area section for the creep and shrinkage calculations. 
    ''' This function is currently worked for shell type area section.
    ''' </summary>
    ''' <param name="nameShellSection">The name of an existing shell-type area section property.</param>
    ''' <param name="notionalSectionType">The type of notional size to be defined for the section.</param>
    ''' <param name="value">For notionalSectionType "Auto", the Value represents for the scale factor to the program-determined notional size. 
    ''' For notionalSectionType “User”, the Value represents for the user-defined notional size [L]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetNotionalSize(ByVal nameShellSection As String,
                                    ByVal notionalSectionType As NotionalSectionType,
                                    ByVal value As Double) As Boolean
        Dim notionalSectionTypeStr As String = GetEnumDescription(notionalSectionType)

        ret = SapModel.PropArea.GetNotionalSize(nameShellSection, notionalSectionTypeStr, value)

        Return retCheck(ret, "SapModel.PropArea.GetNotionalSize")
    End Function

#End Region
End Class
