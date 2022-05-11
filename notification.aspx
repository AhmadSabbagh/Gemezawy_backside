<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="notification.aspx.cs" Inherits="FifaGame.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <p>
            Send Notification To All Users</p>
        <p>
            <asp:TextBox ID="All_User_Text_Noti" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" Text="Send" OnClick="Button1_Click"/>
       

        <br />
        <br />
        <br />
          <p>
            Send Notification To Fifa Users</p>
        <p>
            <asp:TextBox ID="Fifa_User_Text_Not" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </p>
        <asp:Button ID="Button2" runat="server" Text="Send" OnClick="Button2_Click"/>
       

        <br />
        <br />
        <br />

        <p>
            Send Notification To Custom_Competition Users , Enter The Competition_Id :<asp:TextBox ID="Comp_IdTX" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="Custom_Competition_Text_Noti" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </p>
        <asp:Button ID="Button3" runat="server" Text="Send" OnClick="Button3_Click"/>
       

        <br />
        <br />
        <br />

        <p>
            Send Notification To Custom Round,Enter The Round_Id :<asp:TextBox ID="Round_IdTX" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="Custom_Round_Noti" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </p>
        <asp:Button ID="Button4" runat="server" Text="Send" OnClick="Button4_Click"/>
       

        <br />
        <br />
        <br />

         <p>
            Send Notification To Custom User , Enter The User_Id :<asp:TextBox ID="User_id_TX" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:TextBox ID="Custom_User_noti" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </p>
        <asp:Button ID="Button5" runat="server" Text="Send" OnClick="Button5_Click"/>
       

        <br />
        <br />
        <br />


    </form>
</asp:Content>
