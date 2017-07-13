Public Class Form1
    Private Sub ApriButton_Click(sender As Object, e As EventArgs) Handles ApriButton.Click
        Dim dialog = Filedialog1.ShowDialog
        If dialog = DialogResult.OK Then
            Dim path = Filedialog1.FileName
            Dim name = Filedialog1.SafeFileName.Replace("Chat WhatsApp con ", "").Replace(".txt", "").Trim
            Dim c As New Conversation(name, path)


            Dim HTML = header.Replace("CSS_PLACEHOLDER", MessageCSS) & c.ToHtml & "</body></html>"
            IO.File.WriteAllText("C:\Users\Workstation 2\Desktop\convo.html", HTML)
        End If
    End Sub
End Class
