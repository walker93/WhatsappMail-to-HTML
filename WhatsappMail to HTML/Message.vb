﻿Public Class Message
    Property Timestamp As Date 'Date and Time
    Property Sender As String
    Property Attachment As String 'Path to attachment
    Property Text As String
    Property ID As Integer
    Property isSent As Boolean
    Property Infomessage As Boolean

    Sub New(ID As Integer, Timestamp As Date, Sender As String, Text As String, isSent As Boolean, Optional Attachment As String = Nothing, Optional infomessage As Boolean = False)
        Me.Timestamp = Timestamp
        Me.Sender = Sender
        Me.Attachment = Attachment
        Me.ID = ID
        Me.Text = Text
        Me.isSent = isSent
        Me.InfoMessage = infomessage
    End Sub

    Public Function getHTML(center As Boolean)
        Dim builder As New Text.StringBuilder

        Dim res As String = MessageHTML

        res = res.Replace("SENDER_PLACEHOLDER", Sender)
        res = res.Replace("TEXT_PLACEHOLDER", Text.Replace("<", "&lt;").Replace(">", "&gt;")) '.Replace(vbLf, "<br>")
        res = res.Replace("TIMESTAMP_PLACEHOLDER", Timestamp.ToShortDateString & " " & Timestamp.ToShortTimeString)
        res = res.Replace("SENTCLASS_PLACEHOLDER", If(isSent, "sent", ""))
        res = res.Replace("CENTER_PLACEHOLDER", If(center Or Infomessage, "center", ""))
        res = res.Replace("ATTACHMENT_PLACEHOLDER", If(Not IsNothing(Attachment), attachmentHTML, ""))
        res = res.Replace("PATH_PLACEHOLDER", If(Not IsNothing(Attachment), "attachments\" + Attachment, ""))
        res = res.Replace("ATTACHMENT_END_PLACEHOLDER", If(Not IsNothing(Attachment), "</a>", ""))

        Return res
    End Function

    Public Overrides Function ToString() As String
        Return Text & If(IsNothing(Attachment), "NO ALL.", "SI ALL.")
    End Function
End Class
