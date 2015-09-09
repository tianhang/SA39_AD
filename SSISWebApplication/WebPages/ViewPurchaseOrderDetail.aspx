<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPurchaseOrderDetail.aspx.cs" Inherits="SSISWebApplication.WebPages.ViewPurchaseOrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>View Purchase Order Details/Delivery Receive</h4>
        </div>
        <div class="panel-body ">
            <div class="row row-padding">
                <div class="col-md-2">Purchase Order ID:</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtPurchaseOrderId" class="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">Supplier Name:</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtSupplierName" class="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">Order Date:</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtOrderDate" class="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">Status:</div>
                <div class="col-md-3">
                    <asp:RadioButtonList runat="server" ID="rdbStatus" CssClass="radio" Enabled="false">
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Error:</span>
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </div>
        <asp:GridView ID="gvPurchaseOrderList" runat="server" CssClass="table" AllowPaging="true" PageSize="10" Width="100%" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvPurchaseOrderList_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                <asp:BoundField DataField="qty" HeaderText="Quantity" />
                <asp:BoundField DataField="price" HeaderText="Price" />
                <asp:BoundField DataField="amount" HeaderText="Amount" />                
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle HorizontalAlign="Center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <a href="ViewPurchaseOrder.aspx" class="btn btn-default navbar-btn btn-lg" aria-label="Left Align">
            <span class="glyphicon glyphicon-circle-arrow-left" aria-hidden="true">Back</span>
        </a>
        <asp:Button ID="btnDeliveryReceive" runat="server" CssClass="btn btn-default navbar-btn btn-lg" Text="Confirm Delivery Received" OnClick="btnDeliveryReceive_Click"></asp:Button>
    </div>
</asp:Content>
