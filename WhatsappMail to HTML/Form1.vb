Public Class Form1
    Private Sub ApriButton_Click(sender As Object, e As EventArgs) Handles ApriButton.Click
        Dim dialog = Filedialog1.ShowDialog
        If dialog = DialogResult.OK Then
            Dim path = Filedialog1.FileName
            Dim name = Filedialog1.SafeFileName.Replace("Chat WhatsApp con ", "").Replace(".txt", "").Trim
            Dim c As New Conversation(name, path)
            MsgBox(c.Messages.Count)
        End If
    End Sub
End Class
