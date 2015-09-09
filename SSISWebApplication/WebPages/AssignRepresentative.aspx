<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignRepresentative.aspx.cs" Inherits="SSISWebApplication.WebPages.AssignRepresentative" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Assign Representative</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
        </div>
        <asp:GridView ID="EmployeeGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="EmployeeGridView_RowCommand" OnRowDataBound="EmployeeGridView_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table" Width="100%" OnPageIndexChanging="EmployeeGridView_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="ID"
                    InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="UserName" HeaderText="Name"
                    InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="RoleName" HeaderText="Role"
                    InsertVisible="False" ReadOnly="True" />
                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Assign" ControlStyle-CssClass="btn btn-primary navbar-btn" ShowHeader="True" Text="Assign" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</asp:Content>
