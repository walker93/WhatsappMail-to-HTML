Module Regex

    Public Android_messagePattern As String = "^(([0-9]{2}[\/]?){2}[0-9]{4}), ([0-9]{2}:[0-9]{2}) - (([\S ]*): ){0,1}"
    '                                Group $1 = Date, Group $3 = Time, Group $5 = Sender

    Public iOS_messagePattern As String = "^(([0-9]{2}[\/]?){3}), ([0-9]{2}:[0-9]{2}:[0-9]{2}): (([^\r\n\t\f]*): ){0,1}"
    '                                Group $1 = Date, Group $3 = Time, Group $5 = Sender

    'TODO: Fix Sender if message text contains :

    Function getTimestamp(text As String, plat As Integer) As Date
        Dim reg As New Text.RegularExpressions.Regex("")
        If plat = Platforms.Android Then
            reg = New Text.RegularExpressions.Regex(Android_messagePattern)
        ElseIf plat = Platforms.iOS Then
            reg = New Text.RegularExpressions.Regex(iOS_messagePattern)
        End If
        Dim matches = reg.Matches(text)
        If matches.Count > 0 Then
            Dim day = matches(0).Groups(1).Value.Substring(0, 2)
            Dim month = matches(0).Groups(1).Value.Substring(3, 2)
            Dim year = matches(0).Groups(1).Value.Substring(6, 4)
            Dim Hours = matches(0).Groups(3).Value.Substring(0, 2)
            Dim minutes = matches(0).Groups(3).Value.Substring(3, 2)
            Dim seconds = If(plat = Platforms.iOS, matches(0).Groups(3).Value.Substring(6, 2), 0)
            Return New Date((Integer.Parse(year)), month, day, Hours, minutes, seconds)
        End If
        Return Nothing
    End Function

    Function getSender(text As String, plat As Integer) As String
        Dim reg As New Text.RegularExpressions.Regex("")
        If plat = Platforms.Android Then
            reg = New Text.RegularExpressions.Regex(Android_messagePattern)
        ElseIf plat = Platforms.iOS Then
            reg = New Text.RegularExpressions.Regex(iOS_messagePattern)
        End If
        Dim matches = reg.Matches(text)
        If matches.Count > 0 Then
            If matches(0).Groups.Count < 5 Then Return Nothing
            Dim sender = matches(0).Groups(5).Value.Split(":")(0)
            Return sender
        End If
        Return Nothing
    End Function

    Public Enum Platforms
        iOS = 0
        Android = 1
        unknown = -1
    End Enum

End Module
