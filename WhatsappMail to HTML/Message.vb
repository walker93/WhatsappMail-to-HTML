Public Class Message
    Property Timestamp As Date 'Date and Time
    Property Sender As String
    Property Attachment As String 'Path to attachment
    Property Text As String
    Property ID As Integer

    Sub New(ID As Integer, Timestamp As Date, Sender As String, Text As String, Optional Attachment As String = Nothing)
        Me.Timestamp = Timestamp
        Me.Sender = Sender
        Me.Attachment = Attachment
        Me.ID = ID
        Me.Text = Text
    End Sub
End Class
