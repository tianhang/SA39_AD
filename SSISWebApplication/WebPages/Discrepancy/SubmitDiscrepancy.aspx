<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitDiscrepancy.aspx.cs" Inherits="SSISWebApplication.WebPages.Discrepancy.DiscrepancyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Submit Discrepancy</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:ValidationSummary ID="vs1" runat="server" ForeColor="Red" ValidationGroup="vg1" Width="100%" />
            <div class="row row-padding">
                <div class="col-md-2">
                    Item Name:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="DDListDescription" runat="server" OnSelectedIndexChanged="DDListDescription_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Category:
                </div>
                <div class="col-md-4">
                    <asp:Label ID="labCategory" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Quantity:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity can not be null." ValidationGroup="vg1" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RValidatorQty" runat="server" ControlToValidate="txtQuantity" ValidationGroup="vg1" ErrorMessage="Quantity should be a integer which is more than 0." ForeColor="Red" MaximumValue="999999999" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Supplier:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="DDListSupplier" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDListSupplier_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Price:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="labPrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="labPrice" ErrorMessage="Price can not be null." ValidationGroup="vg1" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Totle price:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="labTotalPrice" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="labTotalPrice" ErrorMessage="Total price can not be null." ValidationGroup="vg1" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    Reason:
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReason" ErrorMessage="Reason can not be null." ValidationGroup="vg1" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary navbar-btn" ValidationGroup="vg1" Text="Add" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
        <asp:GridView ID="gridviewDiscrepancy" runat="server"
            RowStyle-HorizontalAlign="NotSet" Width="100%" CellPadding="4" Height="24px"
            Style="margin-bottom: 77px" AutoGenerateColumns="False" OnRowDeleting="gridviewDiscrepancy_RowDeleting" OnRowCancelingEdit="gridviewDiscrepancy_RowCancelingEdit" OnRowEditing="gridviewDiscrepancy_RowEditing" OnRowUpdating="gridviewDiscrepancy_RowUpdating" ForeColor="#333333" GridLines="None">


            <AlternatingRowStyle BackColor="White" />


            <Columns>
                <asp:TemplateField HeaderText="Check">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="50" />
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBoxDiscrepancy" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxDiscrepancy" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DiscrepancyId" ReadOnly="true" HeaderText="Discrepancy Id">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemDescription" ReadOnly="true" HeaderText="Item Name">
                    <HeaderStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="CategoryName" ReadOnly="true" HeaderText="Category">
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Quantity">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Qunatity") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Qunatity") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:BoundField DataField="amount" ReadOnly="true" HeaderText="Totle prices">
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Reason">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("reason") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("reason") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="60px" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="Edit" CancelText="Cancel" ControlStyle-CssClass="btn btn-primary navbar-btn">
                    <ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>

                    <HeaderStyle Height="7px" Width="30px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" ControlStyle-CssClass="btn btn-primary navbar-btn">
                    <ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>

                    <HeaderStyle Height="7px" Width="30px" />
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
        &nbsp;

        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-default navbar-btn btn-lg" Text="Submit" OnClick="btnSubmit_Click" />
        <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Error:</span>
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </div>
    </div>
</asp:Content>
