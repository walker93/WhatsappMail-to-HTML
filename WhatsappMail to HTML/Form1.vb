Imports System.IO
Imports System.Text

Public Class Form1

    Dim chats As New List(Of Conversation)
    Dim selected_platform As Integer = Platforms.Android
    Public selected_language As Integer

    Public Sub AddFiles(path As String)
        Dim file = New System.IO.FileInfo(path)
        If isSupportedFile(file) Then
            Dim p = file.FullName
            Dim name = file.Name.Replace("Chat WhatsApp con ", "").Replace("WhatsApp Chat with ", "").Replace(".txt", "").Trim
            name = removeUnicode(name)
            Dim c As Conversation
            If selected_platform = Platforms.Android Then c = New Conversation(p, selected_platform, name)
            If selected_platform = Platforms.WP Then c = New Conversation(p, selected_platform, name)
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
        Return file.Extension.Equals(".txt", StringComparison.CurrentCultureIgnoreCase) And (file.Name.StartsWith(If(selected_language = 0, "Chat WhatsApp", "WhatsApp Chat")) OrElse file.Name = ("_chat.txt"))
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

    Private Sub ApriToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApriToolStripMenuItem.Click, ApriCartellaIOSToolStripMenuItem.Click, ToolStripMenuItem1.Click
        Dim dialog = FolderBrowserDialog1.ShowDialog
        selected_platform = CType(sender, ToolStripMenuItem).Tag
        selected_language = If(ToolStripComboBox1.Text = "Italiano", 0, 1)
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
        Dim convolinks As New List(Of String)
        Dim dialog = FolderBrowserDialog1.ShowDialog
        If dialog = DialogResult.OK Then
            Dim savepath = FolderBrowserDialog1.SelectedPath
            For Each chat As Conversation In chats
                ToolStripProgressBar1.Value = 100 * chats.IndexOf(chat) \ chats.Count
                ToolStripStatusLabel1.Text = "Generazione rapporto in corso... (" & (100 * chats.IndexOf(chat) \ chats.Count).ToString & "%)"
                HTML.AppendLine(headerHTML)
                HTML.AppendLine(chat.ToHtml)
                HTML.AppendLine("</body></html>")
                Dim relativePath As String = "\chats\(" & chats.IndexOf(chat).ToString & ") " & chat.Name.TrimEnd
                Dim convopath = savepath & relativePath
                IO.Directory.CreateDirectory(convopath)
                IO.Directory.CreateDirectory(convopath + "\attachments\")

                convolinks.Add(IndexHTML_link.Replace("CONVO_INDEX_PLACEHOLDER", "." & relativePath & "\index.html").Replace("CONVO_NAME_PLACEHOLDER", chat.Name))

                For Each m In chat.Messages
                    Dim att = m.Value.Attachment
                    If Not IsNothing(att) AndAlso att <> "" Then
                        Try
                            Dim copysource = Path.Combine(chat.getConvoFolder, att)
                            Dim copydest = Path.Combine(convopath & "\attachments\", att)
                            IO.File.Copy(copysource, copydest, False)
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

            Dim indexHTML As New Text.StringBuilder(indexHTML_first.Replace("TITLE_PLACEHOLDER", "Rapporto chat whatsapp"))
            For Each link In convolinks
                indexHTML.AppendLine(link)
            Next
            indexHTML.AppendLine(indexHTML_last)
            IO.File.WriteAllText(savepath & "\" & "index.html", indexHTML.ToString)
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
            chat.PopulateMessages(chat.ConvoFile)
            Dim col() = {chat.Name, String.Join(", ", chat.participants), chat.Messages.Count.ToString, chat.ConvoFile}

            Dim item As New ListViewItem(col)
            ListView1.Items.Add(item)
            Application.DoEvents()
        Next
    End Sub


    Public Function removeUnicode(text As String) As String
        If text = String.Empty Then Return ""
        Dim InputString = text
        Dim asAscii = System.Text.Encoding.ASCII.GetString(
             System.Text.Encoding.Convert(
                 System.Text.Encoding.UTF8,
                 System.Text.Encoding.GetEncoding(
                    System.Text.Encoding.ASCII.EncodingName,
                    New System.Text.EncoderReplacementFallback(String.Empty),
                    New System.Text.DecoderExceptionFallback()),
                 System.Text.Encoding.UTF8.GetBytes(text)))
        Return asAscii
    End Function
End Class
