<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveRejectPurchaseOrder.aspx.cs" Inherits="SSISWebApplication.WebPages.ApproveRejectPurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Approve / Reject Purchase Order</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <div class="col-md-2">Supplier Name:</div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlSupplierName" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2"></div>
                <div class="col-md-3">
                    <asp:RadioButtonList runat="server" ID="rdbStatus" CssClass="radio" Visible="False">
                        <asp:ListItem>Pending</asp:ListItem>

                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2"></div>
                <div class="col-md-3">
                    <asp:Button runat="server" ID="butSearch" CssClass="btn btn-default navbar-btn" Text="Search" OnClick="butSearch_Click"></asp:Button>
                    <asp:Button runat="server" ID="butCancel" CssClass="btn btn-default navbar-btn" Text="Cancel" OnClick="butCancel_Click"></asp:Button>
                </div>
            </div>
            <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Error:</span>
                <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
            </div>
        </div>
        <asp:GridView ID="gvReorderItemList" runat="server" CssClass="table" Width="100%" PageSize="10" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvReorderList_RowCommand" AllowPaging="True" OnPageIndexChanging="gvReorderList_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="reorderitemid" HeaderText="Reorder ID"></asp:BoundField>
                <asp:BoundField DataField="itemName" HeaderText="Item Name"></asp:BoundField>
                <asp:BoundField DataField="suppliername" HeaderText="Supplier Name"></asp:BoundField>
                <asp:BoundField DataField="QtyToOrder" HeaderText="Quantity"></asp:BoundField>
                <asp:BoundField DataField="price" HeaderText="Price"></asp:BoundField>
                <asp:BoundField DataField="amount" HeaderText="Amount"></asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="Status"></asp:BoundField>
                <asp:TemplateField HeaderText="Reject Reason">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRejectReason" runat="server" Text='<%# Bind("rejectreason") %>' CssClass='<%# "form-control "+Eval("Enable") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lblApprove" runat="server" CssClass='<%# "btn btn-success navbar-btn btn-xs "+Eval("Enable") %>' CommandName="Approved" CommandArgument='<%# Bind("ReorderItemID") %>' CausesValidation="False">
                            <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>&nbsp;Approve</asp:LinkButton>
                        <asp:LinkButton ID="lblReject" runat="server" CssClass='<%# "btn btn-danger navbar-btn btn-xs "+Eval("Enable") %>' OnClientClick="ShowPopup" CommandName="Rejected" CommandArgument='<%# Bind("ReorderItemID") %>' CausesValidation="False">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Reject</asp:LinkButton>
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
