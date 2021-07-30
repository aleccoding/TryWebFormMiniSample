<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="流水帳紀錄.SystemAdmin.UserInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan ="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a><br />
                    <a href="AccountingList.aspx"> 流水帳管理 </a>
                </td>
                <td>
                    <!--主要內容-->
                    <table>
                        <tr>
                            <th>Account</th>
                                <td><asp:Literal ID="liAcc" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>Name</th>
                            <td><asp:Literal ID="liName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td> <asp:Literal ID="liemail" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                    <asp:Button ID="Button2" runat="server" Text="登出" OnClick="Button1_Click" />
                </td>
            </tr >
        </table>
    </form>
</body>
</html>
