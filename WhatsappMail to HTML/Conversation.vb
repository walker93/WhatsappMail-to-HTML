﻿Public Class Conversation

    Property Name As String
    Property isGroup As Boolean
    Property ConvoFile As String
    Property Messages As New Dictionary(Of Integer, Message) 'ID, Message
    Property participants As New List(Of String)


    Public Sub New(Name As String, ConvoFile As String)
        Me.Name = Name
        Me.ConvoFile = ConvoFile

        PopulateMessages(Me.ConvoFile)
    End Sub

    Private Sub PopulateMessages(file As String)
        If Not IO.File.Exists(file) Then Throw New IO.FileNotFoundException
        Dim filetext = IO.File.ReadAllText(file)
        Dim lines() As String = filetext.Split(vbLf)
        Dim id As Integer = 0
        For Each line In lines
            id += 1
            Dim Timestamp = getTimestamp(line)
            Dim Sender = getSender(line)
            If Timestamp.Equals(New Date) Then
                Messages(id - 1).Text &= vbCrLf + line
                id -= 1
                Continue For
            End If
            Dim text = line.Substring(18 + If(Sender = "", 0, Sender.Length + 2))
            Dim att As String = Nothing
            If Not participants.Contains(Sender) AndAlso Sender <> "" Then participants.Add(Sender)
            'Gestione allegato
            If text.Contains(" (file allegato)") Then
                Dim fileinfo As New IO.FileInfo(file)
                att = fileinfo.DirectoryName + "\" + text.Substring(0, text.IndexOf(" (file allegato)"))
            End If
            Dim m As New Message(id, Timestamp, Sender, text, Name <> Sender, If(att, Nothing))
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

End Class
