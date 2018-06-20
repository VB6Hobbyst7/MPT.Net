Option Strict On
Option Explicit On

''' <summary>
''' This is a class of 6 boolean values, indicating if the specified model global degree of freedom is active.
''' </summary>
''' <remarks></remarks>
Public Class DegreesOfFreedom
#Region "Properties"
    ''' <summary>
    ''' True: Global degree of translational freedom is active in the X-direction.
    ''' False: Global degree of translational freedom is restrained in the X-direction.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UX As Boolean = False
    ''' <summary>
    ''' True: Global degree of translational freedom is active in the Y-direction.
    ''' False: Global degree of translational freedom is restrained in the Y-direction.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UY As Boolean = False
    ''' <summary>
    ''' True: Global degree of translational freedom is active in the Z-direction.
    ''' False: Global degree of translational freedom is restrained in the Z-direction.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UZ As Boolean = False
    ''' <summary>
    ''' True: Global degree of rotational freedom is active about the X-axis.
    ''' False: Global degree of rotational freedom is restrained about the X-axis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RX As Boolean = False
    ''' <summary>
    ''' True: Global degree of rotational freedom is active about the Y-axis.
    ''' False: Global degree of rotational freedom is restrained about the Y-axis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RY As Boolean = False
    ''' <summary>
    ''' True: Global degree of rotational freedom is active about the Z-axis.
    ''' False: Global degree of rotational freedom is restrained about the Z-axis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RZ As Boolean = False
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns the degrees of freedom as an array of 6 boolean values in the order of Ux, Uy, Uz, Rx, Ry, Rz.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToArray() As Boolean()
        Dim DOFArray(5) As Boolean
        DOFArray(0) = UX
        DOFArray(1) = UY
        DOFArray(2) = UZ
        DOFArray(3) = RX
        DOFArray(4) = RY
        DOFArray(5) = RZ

        Return DOFArray
    End Function
    ''' <summary>
    ''' Populates the degrees of freedom from an array of 6 boolean values in the order of Ux, Uy, Uz, Rx, Ry, Rz. 
    ''' </summary>
    ''' <param name="degreesOfFreedom"></param>
    ''' <remarks></remarks>
    Public Sub FillFromArray(ByVal degreesOfFreedom() As Boolean)
        Dim maxIndex As Integer = UBound(degreesOfFreedom)
        If maxIndex <> 5 Then Exit Sub

        UX = degreesOfFreedom(0)
        UY = degreesOfFreedom(1)
        UZ = degreesOfFreedom(2)
        RX = degreesOfFreedom(3)
        RY = degreesOfFreedom(4)
        RZ = degreesOfFreedom(5)
    End Sub
#End Region
End Class
