<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewPassword.aspx.cs" Inherits="FifaGame.NewPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 90px">
            New Password
            <asp:TextBox ID="newpass_id" runat="server"></asp:TextBox>
&nbsp;<br />
            Confirm New Password
            <asp:TextBox ID="cnewpass_id" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 29px" Text="Send" />
            <br />
        </div>
    </form>
</body>
</html>
