Imports System.IO

Public Class Form1
    Private Sub ApriButton_Click(sender As Object, e As EventArgs) Handles ApriButton.Click
        Dim dialog = FolderBrowserDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            'Dim path = Filedialog1.FileName
            'Dim name = Filedialog1.SafeFileName.Replace("Chat WhatsApp con ", "").Replace(".txt", "").Trim
            'Dim c As New Conversation(name, path)
            AddDirectory(FolderBrowserDialog1.SelectedPath)

            'Dim HTML = header.Replace("CSS_PLACEHOLDER", MessageCSS) & c.ToHtml & "</body></html>"
            'IO.File.WriteAllText("C:\Users\Workstation 2\Desktop\convo.html", HTML)
        End If
    End Sub

    Public Sub AddFiles(path As String)
        Dim file = New System.IO.FileInfo(path)
        If isSupportedFile(file) Then
            Dim p = file.FullName
            Dim name = file.Name.Replace("Chat WhatsApp con ", "").Replace(".txt", "").Trim
            Dim c As New Conversation(name, p)
            Dim col() = {c.Name, String.Join(", ", c.participants), p}
            Dim item As New ListViewItem(col)
            ListView1.Items.Add(item)
            Application.DoEvents()
        Else
            Console.WriteLine("Not supported: " + file.FullName)
        End If
    End Sub

    Public Function isSupportedFile(file As FileInfo) As Boolean
        Return file.Extension.Equals(".txt", StringComparison.CurrentCultureIgnoreCase) And file.Name.StartsWith("Chat WhatsApp")
    End Function

    Public Sub AddFiles(paths As String())
        For Each p In paths
            AddFiles(p)
        Next
    End Sub
    Public Sub AddDirectory(path As String, Optional recursive As Boolean = True)
        If recursive Then
            Dim directories = System.IO.Directory.GetDirectories(path)
            For Each dire In directories
                AddDirectory(dire, recursive)
            Next
        End If
        Dim files = System.IO.Directory.GetFiles(path, "Chat WhatsApp *.txt")
        AddFiles(files)
    End Sub

End Class
