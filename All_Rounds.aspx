<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="All_Rounds.aspx.cs" Inherits="FifaGame.All_Rounds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <form id="form1" runat="server">

          <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false"
              BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
              AutoGenerateDeleteButton="True"
              AutoGenerateEditButton="True" 
              OnRowEditing="GridView1_RowEditing"
              OnRowCancelingEdit ="GridView1_RowCancelingEdit"
              OnRowDeleting="GridView1_RowDeleting"
              OnRowUpdating="GridView1_RowUpdating"
              
              DataKeyNames="round_id">
                  <Columns>
                          <asp:BoundField DataField="round_id" HeaderText ="Round_Id" />
                    <asp:BoundField DataField="user_id1" HeaderText ="First_Player" />
                    <asp:BoundField DataField="user_id2" HeaderText ="Second_Player" />
                       <asp:BoundField DataField="compitition_id" HeaderText ="Competition_id" />
                       <asp:BoundField DataField="date" HeaderText ="Date" />
                       <asp:BoundField DataField="winner_id" HeaderText ="Winner_id" />
                       <asp:BoundField DataField="loser_id" HeaderText ="Loser_id" />
                  </Columns>
        </asp:GridView>
  
          <br />
      </form>
</asp:Content>
