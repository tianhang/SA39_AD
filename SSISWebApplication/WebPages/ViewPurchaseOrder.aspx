<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPurchaseOrder.aspx.cs" Inherits="SSISWebApplication.WebPages.ViewPurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>View Purchase Order/Delivery Receive</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <div class="col-md-2">Supplier Name:</div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlSupplierName" class="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">Status:</div>
                <div class="col-md-3">
                    <asp:RadioButtonList runat="server" ID="rdbStatus" CssClass="radio">
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2"></div>
                <div class="col-md-3">
                    <asp:Button runat="server" ID="butSearch" class="btn btn-default navbar-btn" Text="Search" OnClick="butSearch_Click"></asp:Button>
                    <asp:Button runat="server" ID="butCancel" class="btn btn-default navbar-btn" Text="Cancel" OnClick="butCancel_Click"></asp:Button>
                </div>
            </div>
        </div>
        <asp:GridView ID="gvPurchaseOrderList" runat="server" CssClass="table" Width="100%" PageSize="10" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvPurchaseOrderList_RowCommand" AllowPaging="True" OnPageIndexChanging="gvPurchaseOrderList_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="purchaseorderid" HeaderText="Purchase Order ID" />
                <asp:BoundField DataField="suppliername" HeaderText="Supplier Name" />
                <asp:BoundField DataField="orderdate" HeaderText="Order Date" />
                <asp:BoundField DataField="deliverydate" HeaderText="Delivery Date" />
                <asp:BoundField DataField="status" HeaderText="Status" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbReceive" runat="server" CssClass='<%# Eval("Status_Bool") %>' CommandName="Receive" CommandArgument='<%# Bind("PurchaseOrderID") %>' CausesValidation="False">Receive</asp:LinkButton>
                        <asp:LinkButton ID="lbDetail" runat="server" CssClass="btn btn-primary navbar-btn" CommandName="Detail" CommandArgument='<%# Bind("PurchaseOrderID") %>' CausesValidation="False">Detail</asp:LinkButton>
                        <span class="badge">
                            <asp:Label runat="server" ID="lblPoDetailCount" Text='<%# Bind("PoDetailCount") %>'></asp:Label></span>
                    </ItemTemplate>
                </asp:TemplateField>
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
    </div>
</asp:Content>
