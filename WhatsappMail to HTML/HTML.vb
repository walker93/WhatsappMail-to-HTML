﻿Module HTML

    Public MessageCSS As String = ".message{
  box-shadow: 2px 2px 2px grey;
  background-color: #E6FFCC;
  border-radius: 20px;
  padding: 10px;
  width: 40%;
  font-family: sans-serif;
  margin-top: 10px;
}

.sent{
  align-self: flex-end;
  background-color: #CCF5FF;
}

.MessageText {
    white-space: pre-line;
}

.center{
  align-self: center;
}

.Timestamp{
  color: RGBA(100,100,100,70);
  font-size: 9pt;
}

.Sender {
  color: #0069CC;
  border-style: solid;
  border-width: 0px 0px 2px 0px;
  border-radius: 5px;
}

.convo {
  display: flex;
  flex-direction: column;
}

#frame {
  right: 0px;
  width: 80%;
  position: absolute;
}
"


    Public header As String = "<html>
    <head>
        <style>CSS_PLACEHOLDER</style>
    </head>
<body>
"

    Public MessageHTML As String = "<div class='message SENTCLASS_PLACEHOLDER CENTER_PLACEHOLDER'>
  <span class='Sender'>SENDER_PLACEHOLDER</span>
    ATTACHMENT_PLACEHOLDER
  <p class='MessageText'>TEXT_PLACEHOLDER</p>
    ATTACHMENT_END_PLACEHOLDER
  <span class='Timestamp'>TIMESTAMP_PLACEHOLDER</span>
</div>"

    Public attachmentHTML As String = "<a href='PATH_PLACEHOLDER'>"



    '------------------------------------------------------------

    Public indexHTML_first As String = "<html>
<head>
  <title>TITLE_PLACEHOLDER</title>
  <style>
  .container{
  height: 100%;
  display: flex;
  'align-items: stretch;
}
.nav{
  flex: 0.2 auto;
  width: 0%;
  order: 1;
  overflow-y: scroll;
}
.convo-link{
  padding: 10px 0px;
  border-bottom: solid 0px #ccc;
}

.convo-link:hover{
  background-color: #ccf2ff;
}

[name=convo]{
flex: 1;
order: 2;
}
  </style>
</head>
<body>
  <div class='container' width=100% height=100%>
    <nav class='nav'>
"

    Public indexHTML_last As String = "
</nav>
<iframe name='convo' src=''></iframe>
</div>
</body>
</html>
"

    Public IndexHTML_link As String = "<div class='convo-link'><a href='CONVO_INDEX_PLACEHOLDER' target='convo'>CONVO_NAME_PLACEHOLDER</a></div>"
End Module
