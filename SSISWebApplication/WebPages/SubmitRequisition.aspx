<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitRequisition.aspx.cs" Inherits="SSISWebApplication.WebPages.SubmitRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Submit Requisition</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="vg1" ForeColor="Red" BorderWidth="1px" />
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="Label13" runat="server" Text="Requisition Date:"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblRequisitionDate" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Catagory:"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>

                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vg1" runat="server" ForeColor="Red" ControlToValidate="ddlCategory" ErrorMessage="Please select one category.">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="Label7" runat="server" Text="Item:"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="vg1" runat="server" ForeColor="Red" ControlToValidate="ddlItem" ErrorMessage="Please select one item.">*</asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Quantity:"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vg1" ForeColor="Red" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please enter quantity.">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ValidationGroup="vg1" ControlToValidate="txtQuantity" ErrorMessage="Please enter the valid quantity." MaximumValue="50000" MinimumValue="1" Type="Integer" ForeColor="Red">*</asp:RangeValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary navbar-btn" ValidationGroup="vg1" Text="Add" OnClick="btnAdd_Click" />
                    <%--<asp:Label ID="lblmsg" runat="server"></asp:Label>--%>
                </div>
            </div>
        </div>
        <div class="panel-heading">
            <h4>
                <asp:Label ID="lblRequisitionItemList" runat="server" Font-Bold="True"></asp:Label></h4>
        </div>
        <asp:GridView ID="dgvRequisitionItemList" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDeleting="dgvRequisitionItemList_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="categoryName" HeaderText="Category" ReadOnly="True" />
                <asp:BoundField DataField="description" HeaderText="Item Name" ReadOnly="True" />
                <asp:BoundField DataField="uomName" HeaderText="UOM" ReadOnly="True" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" ReadOnly="True" />
                <%--                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Bind("quantity") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                <asp:CommandField ShowDeleteButton="True" DeleteText="Remove" ControlStyle-CssClass="btn btn-primary navbar-btn">
                    <ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>
                </asp:CommandField>
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
        &nbsp;
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-default navbar-btn btn-lg" OnClick="btnSubmit_Click" />
        <%--<asp:Label ID="lblNoti" runat="server"></asp:Label>--%>
        <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Error:</span>
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </div>
    </div>
</asp:Content>
