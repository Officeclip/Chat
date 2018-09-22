<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent.aspx.cs" Inherits="OfficeClip.LiveChat.Chat.Agent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .container{
            display: flex;
        }
        .column {
            flex: 1;
        }
        .column-1 {
            order: 1;
            border-right: solid 1px gray;
        }
         .column-2 {
            order: 2
        }
        .column-3 {
            order: 3
        }
   </style>
</head>
<body>
    <div class="container">
        <div class="column column-1">
            
        </div>
        <div class="column column-2">

        </div>
        <div class="column column-3">

        </div>
    </div>
</body>
</html>
