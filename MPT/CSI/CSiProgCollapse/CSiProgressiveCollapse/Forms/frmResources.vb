Option Explicit On
Option Strict On

''' <summary>
''' Form containing links to the CSi website and documentation related to the program.
''' </summary>
''' <remarks></remarks>
Public Class frmResources

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Close()
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Dim myFrmAbout As New frmAbout

        myFrmAbout.ShowDialog()
    End Sub

    Private Sub btnCSiWeb_Click(sender As Object, e As EventArgs) Handles btnCSiWeb.Click
        Process.Start("http://www.csiamerica.com/")
    End Sub

    Private Sub btnDocumentation_Click(sender As Object, e As EventArgs) Handles btnDocumentation.Click
        Dim startupPath As String
        startupPath = Application.StartupPath
        Process.Start(startupPath & "\Resources\S-TN-PC-001.pdf")
    End Sub
End Class