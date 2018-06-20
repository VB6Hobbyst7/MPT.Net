Option Explicit On
Imports System.Xml
Module friendVariables
    '====Available shortcut keys (ctrl-.)
    'e - selectSelectedCells
    'j
    'm
    'q
    't

    Friend regTest As cRegTest

    Friend NoMatchArrayPath() As Object
    Friend NoMatchArrayPathRun() As Object

    Friend path As String
    Friend SName As String

    Friend row As Integer
    Friend col As Integer
    Friend exAddress As String                      'Excel cell address, in Letter+Number format

    Friend objXMLelementParent As XmlElement
    Friend objXMLelementChild As XmlElement

    Friend pathSuite As String
    Friend xmlPathName As String





End Module
