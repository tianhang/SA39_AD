<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageCatalogue.aspx.cs" Inherits="SSISWebApplication.WebPages.Item.AddNewItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Manage Catalogue</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:ValidationSummary ID="vs1" runat="server" ForeColor="Red" ValidationGroup="vg1" Width="100%" />
            <div class="row row-padding">
                <div class="col-md-2">
                    Item ID:
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtItemId" runat="server" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemId" ValidationGroup="vg1" ErrorMessage="Item ID Required!" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Category Name:
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="DropDownListCategory" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    UOM:
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="DropDownListUom" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Reorder Level:
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRecordLevel" runat="server" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RangeValidator ID="RangeValidator1" MinimumValue="1" MaximumValue="99999" runat="server" ControlToValidate="txtRecordLevel"  ErrorMessage="Reorder level must be more than 0." ForeColor="Red" ValidationGroup="vg1">*</asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRecordLevel" ErrorMessage="Reorder Level Required!" ValidationGroup="vg1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Reorder Quantity:
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRecordQuantity" runat="server" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RangeValidator ID="RangeValidator2" runat="server" MinimumValue="1" MaximumValue="99999" ControlToValidate="txtRecordQuantity"  ErrorMessage="Reorder Qty must be more than 0." ForeColor="Red" ValidationGroup="vg1">*</asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRecordQuantity" ErrorMessage="Reorder Quantity Required!" ValidationGroup="vg1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
             <div class="row row-padding">
                <div class="col-md-2">
                    Stock Balance:
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RangeValidator ID="RangeValidator3" runat="server" MinimumValue="0" MaximumValue="99999" ControlToValidate="txtRecordQuantity"  ErrorMessage="Stock balance should be positive integer." ForeColor="Red" ValidationGroup="vg1" Type="Integer">*</asp:RangeValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBalance" ErrorMessage="Stock Balance Required!" ValidationGroup="vg1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Description:
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description Required!" ValidationGroup="vg1" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary navbar-btn" Text="Add" OnClick="btnAdd_Click" ValidationGroup="vg1" />
                </div>
            </div>
        </div>

        <asp:GridView ID="itemTable" runat="server" CssClass="table" Width="100%"
            RowStyle-HorizontalAlign="NotSet" AutoGenerateColumns="False" OnRowCancelingEdit="itemTable_RowCancelingEdit" OnRowDeleting="itemTable_RowDeleting" OnRowEditing="itemTable_RowEditing" OnRowUpdating="itemTable_RowUpdating" AllowPaging="True" OnPageIndexChanging="itemTable_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">

            <AlternatingRowStyle BackColor="White" />

            <Columns>
                <asp:BoundField HeaderText="Item Id" ReadOnly="True" DataField="ItemId" />
                <asp:BoundField DataField="UomName" HeaderText="Uom Name" ReadOnly="True" />
                <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ReadOnly="True" />
                <asp:BoundField HeaderText="Description" DataField="Description" />
                <asp:BoundField HeaderText="Reorder Level" DataField="ReorderLevel" />
                <asp:BoundField HeaderText="Reorder Quantity" DataField="ReorderQty" />
                <asp:BoundField HeaderText="Stock Balance" DataField="StockBalance" />
                <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary navbar-btn">
                    <ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-primary navbar-btn">
                    <ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>
                </asp:CommandField>
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
