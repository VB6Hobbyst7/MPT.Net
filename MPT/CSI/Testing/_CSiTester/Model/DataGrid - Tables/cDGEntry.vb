Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' Class for storing the basic keyword information in the keywords form, for generating add/remove lists of keywords. 
''' Basic keyword unit.
''' </summary>
''' <remarks></remarks>
Public Class cDGEntry

    Public Property columnHeader As String

    Public Property rowEntry As String

    Public Property selectedEntry As String
End Class
