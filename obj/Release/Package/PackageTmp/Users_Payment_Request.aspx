﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Users_Payment_Request.aspx.cs" Inherits="FifaGame.Users_Payment_Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <form id="form1" runat="server">

          <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false"
              BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
              AutoGenerateDeleteButton="True"
              AutoGenerateEditButton="True" 
              OnRowEditing="GridView1_RowEditing"
              OnRowCancelingEdit="GridView1_RowCancelingEdit"
              OnRowDeleting="GridView1_RowDeleting"
              OnRowUpdating="GridView1_RowUpdating"
              
              DataKeyNames="request_id">
                  <Columns>
                      
                    <asp:BoundField DataField="request_id" HeaderText ="Request_id" />
                       <asp:BoundField DataField="user_id" HeaderText ="User ID" />
                    <asp:BoundField DataField="type" HeaderText ="Payment Method" />
                    <asp:BoundField DataField="payment_info" HeaderText ="Payment information" />
                       <asp:BoundField DataField="date" HeaderText ="Payment Date" />
                       <asp:BoundField DataField="status" HeaderText ="Payment Status" />
                   
                  </Columns>
        </asp:GridView>
  
          <br />
      </form>

</asp:Content>
