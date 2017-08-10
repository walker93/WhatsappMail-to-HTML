Imports System.IO

Public Class Form1

    Dim chats As New List(Of Conversation)
    Dim selected_platform As Integer = Platforms.Android

    Public Sub AddFiles(path As String)
        Dim file = New System.IO.FileInfo(path)
        If isSupportedFile(file) Then
            Dim p = file.FullName
            Dim name = file.Name.Replace("Chat WhatsApp con ", "").Replace(".txt", "").Trim
            Dim c As Conversation
            If selected_platform = Platforms.Android Then c = New Conversation(p, selected_platform, name)
            If selected_platform = Platforms.iOS Then c = New Conversation(p, selected_platform)
            Dim col() = {c.Name, String.Join(", ", c.participants), c.Messages.Count.ToString, p}
                chats.Add(c)
                Dim item As New ListViewItem(col)
                ListView1.Items.Add(item)
                Application.DoEvents()
            Else
                Console.WriteLine("Not supported:  " + file.FullName)
        End If
    End Sub

    Public Function isSupportedFile(file As FileInfo) As Boolean
        Return file.Extension.Equals(".txt", StringComparison.CurrentCultureIgnoreCase) And (file.Name.StartsWith("Chat WhatsApp") OrElse file.Name = ("_chat.txt"))
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
        Dim files = System.IO.Directory.GetFiles(path, "*.txt")
        AddFiles(files)
    End Sub

    Private Sub ApriToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApriToolStripMenuItem.Click, ApriCartellaIOSToolStripMenuItem.Click
        Dim dialog = FolderBrowserDialog1.ShowDialog
        selected_platform = CType(sender, ToolStripMenuItem).Tag
        If dialog = DialogResult.OK Then
            AddDirectory(FolderBrowserDialog1.SelectedPath)
            If selected_platform = Platforms.iOS Then checknames()
        End If
    End Sub

    Private Sub GeneraRapportoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GeneraRapportoToolStripMenuItem.Click
        If chats.Count = 0 Then
            MsgBox("Caricare almeno una chat.", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Nessuna conversazione caricata")
            Exit Sub
        End If
        Dim headerHTML As String = header.Replace("CSS_PLACEHOLDER", MessageCSS)
        Dim HTML As New Text.StringBuilder()
        Dim dialog = FolderBrowserDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            Dim savepath = FolderBrowserDialog1.SelectedPath
            For Each chat As Conversation In chats
                ToolStripProgressBar1.Value = 100 * chats.IndexOf(chat) \ chats.Count
                ToolStripStatusLabel1.Text = "Generazione rapporto in corso... (" & (100 * chats.IndexOf(chat) \ chats.Count).ToString & "%)"
                HTML.AppendLine(headerHTML)
                HTML.AppendLine(chat.ToHtml)
                HTML.AppendLine("</body></html>")
                Dim convopath = savepath & "\chats\(" & chats.IndexOf(chat).ToString & ") " & chat.Name
                IO.Directory.CreateDirectory(convopath)
                IO.Directory.CreateDirectory(convopath + "\attachments\")
                For Each m In chat.Messages
                    Dim att = m.Value.Attachment
                    If Not IsNothing(att) AndAlso att <> "" Then
                        Try
                            IO.File.Copy(chat.getConvoFolder & "\" & att, convopath + "\attachments\" + att, False)
                        Catch ex As FileNotFoundException
                            Console.WriteLine(ex.FileName & " non trovato!")
                        Catch Exist As IOException
                            Console.WriteLine(Exist.Message)
                        End Try
                    End If
                Next
                IO.File.WriteAllText(convopath & "\index.html", HTML.ToString)
                HTML.Clear()
                Application.DoEvents()
            Next
        End If
        ToolStripProgressBar1.Value = 100
        ToolStripStatusLabel1.Text = "Generazione rapporto completata"
    End Sub

    Sub checknames()
        Dim participants As New List(Of String)
        Dim owner As String = ""
        Dim maxCount = 0
        For Each chat In chats
            participants.AddRange(chat.participants)
        Next
        For Each p In participants
            Dim c = participants.Where(Function(x) x = p).Count
            If c > maxCount Then
                maxCount = c
                owner = p
            End If
        Next
        Dim i = 0
        ListView1.Items.Clear()

        For Each chat In chats
            If chat.isGroup Then
                i += 1
                chat.Name = "Gruppo " + i.ToString
            Else
                Dim temp As New List(Of String)(chat.participants)
                temp.Remove(owner)
                chat.Name = If(temp.Count > 0, temp.First, "Uknown")
            End If
            Dim col() = {chat.Name, String.Join(", ", chat.participants), chat.Messages.Count.ToString, chat.ConvoFile}

            Dim item As New ListViewItem(col)
            ListView1.Items.Add(item)
            Application.DoEvents()
        Next
    End Sub

End Class
