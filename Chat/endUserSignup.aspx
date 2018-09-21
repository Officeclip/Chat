<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="endUserSignup.aspx.cs" Inherits="OfficeClip.LiveChat.Chat.endUserSignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <style>
        body {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            font-style: normal;
            font-weight: 400;
            color: #000;
        }

        #container {
            display: table;
        }

        .row {
            display: table-row;
        }

        .cell {
            display: table-cell;
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery-1.6.4.min.js"></script>
    <script type="text/javascript" src="Scripts/js.cookie.js"></script>
    <script type="text/javascript">
        btnClick = function () {
            var name = $("#inpName").val();
            var email = $("#inpEmail").val();
            Cookies.set("name", name, { expires: 1 });
            Cookies.set("email", email, { expires: 1 });
            return false;
        };

        isCookiePresent = function () {
                var cookiePresent = Cookies.get("name");
                return (
                    typeof (cookiePresent) !== 'undefined' &&
                    cookiePresent !== null);
            };

        $(document).ready(function () {
            if (isCookiePresent()) {
                var name = Cookies.get("name");
                var email = Cookies.get("email");
                $("#inpName").val(name);
                $("#inpEmail").val(email);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img id="imgLogo" runat="server" />
        </div>
        <div style="margin-top: 30px; width: 300px">
            <div style="vertical-align: top">
                <img id="Img1" runat="server" style="width: 24px; height: 24px">
                <asp:Label ID="topMessage" runat="server"
                    Style="font-size: larger" />
            </div>
            <div id="container">
                <div class="row">
                    <div class="cell" style="padding-top: 20px">
                        Name:
                    </div>
                    <div class="cell">
                        <input id="inpName" type="text" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="cell" style="padding-top: 20px">
                        Email:
                    </div>
                    <div class="cell">
                        <input id="inpEmail" type="text" runat="server" />
                    </div>
                </div>
                <div style="padding-top: 20px"></div>
                <div class="row">
                    <div class="cell">
                        Message:
                    </div>
                    <div class="cell" style="vertical-align: top">
                        <textarea id="txtMessage" runat="server"
                            rows="5" cols="25" />
                    </div>
                </div>
                <div style="padding-top: 20px"></div>
                <div class="row">
                    <div class="cell" style="margin-left: 20px">
                        <button id="btnSend" runat="server" type="submit"
                            onserverclick="btnSendClick" >
                            Send</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
