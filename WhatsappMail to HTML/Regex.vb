Module Regex

    Public Android_messagePattern As String = "^(([0-9]{2}[\/]?){2}[0-9]{2}), ([0-9]{1,2}:[0-9]{2}) (?>AM|PM)? ?- (([\S ]*): ){0,1}"
    '                                Group $1 = Date, Group $3 = Time, Group $5 = Sender
    '                                           01/01/18, 00:00 - Sender: 
    Public iOS_messagePattern As String = "^(([0-9]{2}[\/]?){3}), ([0-9]{2}:[0-9]{2}:[0-9]{2}): (([^\r\n\t\f]*): ){0,1}"
    '                                Group $1 = Date, Group $3 = Time, Group $5 = Sender
    '                                           01/01/18, 00:00:00: Sender: 

    Public WP_messagePattern As String = "^(([0-9]{2}[\/]?){2}[0-9]{4}) ([0-9]{2}:[0-9]{2}:[0-9]{2}): (([^\r\n\t\f]*): ){0,1}"
    '                                Group $1 = Date, Group $3 = Time, Group $5 = Sender
    '                                           01/01/2018 00:00:00: Sender: 

    'TODO: Fix Sender if message text contains :

    Function getTimestamp(text As String, plat As Integer) As Date
        Dim reg As New Text.RegularExpressions.Regex("")
        Dim yearLenght = 2
        If plat = Platforms.Android Then
            reg = New Text.RegularExpressions.Regex(Android_messagePattern)
        ElseIf plat = Platforms.iOS Then
            reg = New Text.RegularExpressions.Regex(iOS_messagePattern)
        ElseIf plat = Platforms.WP Then
            reg = New Text.RegularExpressions.Regex(WP_messagePattern)
            yearLenght = 4
        End If
        Dim matches = reg.Matches(text)
        If matches.Count > 0 Then
            Dim day = Integer.Parse(matches(0).Groups(1).Value.Substring(0, 2))
            Dim month = Integer.Parse(matches(0).Groups(1).Value.Substring(3, 2))
            Dim year = Integer.Parse(matches(0).Groups(1).Value.Substring(6, yearLenght)) + 2000
            Dim time_text = matches(0).Groups(3).Value
            Dim Hours = Integer.Parse(time_text.Substring(0, time_text.IndexOf(":")))
            Dim minutes = Integer.Parse(time_text.Substring(1 + time_text.IndexOf(":"), 2))
            Dim seconds = Integer.Parse(If(plat <> Platforms.Android, time_text.Substring(6, 2), 0))
            If text.Contains("PM") Then Hours += 12 : Hours = Hours Mod 24

            Return New Date(year, month, day, Hours, minutes, seconds)
        End If
        Return Nothing
    End Function

    Function getSender(text As String, plat As Integer) As String
        Dim reg As New Text.RegularExpressions.Regex("")
        If plat = Platforms.Android Then
            reg = New Text.RegularExpressions.Regex(Android_messagePattern)
        ElseIf plat = Platforms.iOS Then
            reg = New Text.RegularExpressions.Regex(iOS_messagePattern)
        ElseIf plat = Platforms.WP Then
            reg = New Text.RegularExpressions.Regex(WP_messagePattern)
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
        WP = 2
        unknown = -1
    End Enum

End Module
