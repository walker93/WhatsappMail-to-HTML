Public Class Conversation

    Property Name As String
    Property isGroup As Boolean
    Property ConvoFile As String
    Property Messages As New Dictionary(Of Integer, Message) 'ID, Message
    Property participants As New List(Of String)
    Property Platform As Integer = Platforms.Android

    Public Sub New(ConvoFile As String, plat As Integer, Optional Name As String = "Nessun Nome")
        If Name <> "Nessun Nome" Then Me.Name = Name
        Me.ConvoFile = ConvoFile
        Platform = plat
        PopulateMessages(Me.ConvoFile)
    End Sub

    Public Sub PopulateMessages(file As String)
        If Messages.Count <> 0 Then Messages.Clear()
        If Not IO.File.Exists(file) Then Throw New IO.FileNotFoundException
        Dim filetext = IO.File.ReadAllText(file)
        Dim lines() As String = filetext.Split(vbLf)
        Dim id As Integer = 0

        For Each line In lines
            id += 1
            Dim Timestamp = getTimestamp(line, Platform)
            Dim Sender = getSender(line, Platform)
            Dim SenderNoUnicode = Form1.removeUnicode(getSender(line, Platform))
            Dim systemmessage As Boolean = False

            If Timestamp.Equals(New Date) Then
                If line.StartsWith(If(Form1.selected_language = 0, "I messaggi inviati in quest", "Messages sent to this")) Then
                    systemmessage = True
                Else
                    If Messages.ContainsKey(id - 1) Then
                        Messages(id - 1).Text &= vbCrLf + line
                        id -= 1
                        Continue For
                    End If
                End If
            End If

            If Not participants.Contains(Sender) AndAlso Sender <> "" Then participants.Add(Sender)
            Dim text = ""
            Dim att As String = Nothing
            If Platform = Platforms.Android Then
                If line = "" Then Continue For
                text = line.Substring(18 + 2 + If(Sender = "", 0, Sender.Length + 2))
                'Gestione allegato
                Dim search = If(Form1.selected_language = 0, " (file allegato)", " (file attached)")
                If Sender.StartsWith("+") And text.StartsWith(": ") Then text = text.Replace(": ", "")
                If text.Contains(search) Then
                    'Dim fileinfo As New IO.FileInfo(file)

                    att = Form1.removeUnicode(text).Substring(0, text.IndexOf(search)).Trim
                End If
            ElseIf Platform = Platforms.iOS Then
                text = line.Substring(20 + If(Sender = "", 0, Sender.Length + 2))
                'Gestione allegato
                If text.Contains(" <‎allegato>") Then
                    'Dim fileinfo As New IO.FileInfo(file)
                    att = Form1.removeUnicode(text).Substring(0, text.IndexOf(" <allegato>")).Trim
                End If
            ElseIf Platform = Platforms.WP Then

                text = line.Substring(If(systemmessage, 0, 21) + If(Sender = "", 0, getSender(line, Platform).Length + 2))

                'no allegati
            End If

            Dim m As New Message(id, Timestamp, Sender, text, Name <> SenderNoUnicode, If(att, Nothing), systemmessage)
            Messages.Add(id, m)
        Next
        isGroup = participants.Count > 2
    End Sub

    Public Function ToHtml()
        Dim builder As New Text.StringBuilder("<div class='convo'>")
        builder.AppendLine()

        For Each m In Messages
            builder.AppendLine(m.Value.getHTML(isGroup))
        Next

        builder.AppendLine("</div>")
        Return builder.ToString
    End Function

    Public Function getConvoFolder()
        Dim fileinfo As New IO.FileInfo(ConvoFile)
        Return fileinfo.DirectoryName
    End Function


    Public Function sameSender(ByVal Sender As String) As Boolean
        If Sender = "" Then Return False
        Dim unicode As Text.Encoding = Text.Encoding.Unicode
        Dim senderbytes = unicode.GetBytes(Sender)
        Dim namebytes = unicode.GetBytes(Name)
        Dim list = senderbytes.ToList
        list.RemoveAt(0)
        list.RemoveAt(1)
        list.RemoveAt(2)
        senderbytes = list.ToArray
        If Name = unicode.GetString(senderbytes) Then Return True
        Return False
    End Function



End Class
