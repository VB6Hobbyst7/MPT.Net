Option Explicit On
Option Strict On

Public Class frmXMLObjectCreate

    Private Sub btnCreateIncident_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateIncident.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Incident)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnCreateTicket_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateTicket.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Ticket)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnCreateLink_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateLink.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Link)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnCreateAttachment_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateAttachment.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Attachment)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnCreateImage_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateImage.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Image)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnCreateUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateUpdate.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Update)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnCreateExcelResult_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateExcelResult.Click
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.ExcelResult)

        windowXMLNodeCreateObject.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
