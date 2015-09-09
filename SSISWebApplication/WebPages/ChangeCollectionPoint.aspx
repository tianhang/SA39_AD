<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeCollectionPoint.aspx.cs" Inherits="SSISWebApplication.WebPages.ChangeCollectionPoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Change Collection Point</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:DropDownList ID="DepartmentDropDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DepartmentDropDownList_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <asp:GridView ID="CollectionPointGridView" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="CollectionPointGridView_RowDataBound" OnRowCommand="CollectionPointGridView_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="CollectionPointId" HeaderText="ID"
                    InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Address" HeaderText="Address"
                    InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="Time" HeaderText="Time Slot"
                    InsertVisible="False" ReadOnly="True" />
                <asp:ButtonField ButtonType="Button" CommandName="Select" ControlStyle-CssClass="btn btn-primary navbar-btn" HeaderText="Choose" ShowHeader="True" Text="Select">
                    <ControlStyle CssClass="btn btn-primary navbar-btn"></ControlStyle>
                </asp:ButtonField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
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
