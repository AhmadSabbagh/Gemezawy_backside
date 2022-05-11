<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="pubg_competition_run.aspx.cs" Inherits="FifaGame.pubg_competition_run" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form id="form1" runat="server">
    
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false"
              BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnRowCommand="GridView1_RowCommand"
             
              
              DataKeyNames="competition_id">
                  <Columns>

                    <asp:BoundField DataField="competition_id" HeaderText ="competition_id" />
                    <asp:BoundField DataField="competition_name" HeaderText ="competition_name" />
                    <asp:BoundField DataField="competition_date" HeaderText ="competition_date" />
                       <asp:BoundField DataField="competition_price" HeaderText ="competition_price" />
                      <asp:BoundField DataField="competition_status" HeaderText ="competition_status" />
                       <asp:TemplateField ShowHeader="False">
                         <ItemTemplate>
                <asp:Button ID="runBut" runat="server" CausesValidation="false" CommandName="SendMail"
                        Text="Run The Competition" CommandArgument='<%# Eval("competition_id") %>' />
                       </ItemTemplate>
                     </asp:TemplateField>
                  </Columns>
        </asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Room's Name : "></asp:Label>
            <asp:TextBox ID="roomnameTXT" runat="server"></asp:TextBox>
            &nbsp;<br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Room's pass"></asp:Label>
        &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="roompassTXT" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelRun" runat="server" Text="No Cometition is runing "></asp:Label>
        <br />
    </form>
</asp:Content>
