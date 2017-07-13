Module Regex

    Public messagePattern As String = "^(([0-9]{2}[\/]?){3}), ([0-9]{2}:[0-9]{2}) - ([\S]*): "
    '                                Group $1 = Date, Group $3 = Time, Group $4 = Sender

    Function getTimestamp(text As String) As Date
        Dim reg As New Text.RegularExpressions.Regex(messagePattern)
        Dim matches = reg.Matches(text)
        If matches.Count > 0 Then
            Dim day = matches(0).Groups(1).Value.Substring(0, 2)
            Dim month = matches(0).Groups(1).Value.Substring(3, 2)
            Dim year = matches(0).Groups(1).Value.Substring(6, 2)
            Dim Hours = matches(0).Groups(3).Value.Substring(0, 2)
            Dim minutes = matches(0).Groups(3).Value.Substring(3, 2)
            Return New Date((Integer.Parse(year) + 2000), month, day, Hours, minutes, 0)
        End If
        Return Nothing
    End Function

    Function getSender(text As String) As String
        Dim reg As New Text.RegularExpressions.Regex(messagePattern)
        Dim matches = reg.Matches(text)
        If matches.Count > 0 Then
            Dim sender = matches(0).Groups(4).Value
            Return sender
        End If
        Return Nothing
    End Function

End Module
