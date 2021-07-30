<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="流水帳紀錄.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="plcLogin" runat="server">
        Account: <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
        <br />
        Password:<asp:TextBox ID="txtPWD" TextMode="Password" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="登入" OnClick="Button1_Click" />
            <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        </asp:PlaceHolder>
    </form>
</body>
</html>
