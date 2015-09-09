<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmergencyDisbursement.aspx.cs" Inherits="SSISWebApplication.WebPages.EmergencyDisbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Emergency Disbursement</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:ValidationSummary ID="vs1" runat="server" ValidationGroup="vg1" ForeColor="Red" BorderWidth="1px" />
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="Label14" runat="server" Text="Department:"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Catagory:"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vg1" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Please select one category." ForeColor="Red">*</asp:RequiredFieldValidator>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="vg1" runat="server" ControlToValidate="ddlItem" ErrorMessage="Please select one item." ForeColor="Red">*</asp:RequiredFieldValidator>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vg1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please enter quantity." ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtQuantity" ForeColor="Red" OnServerValidate="ServerValidation"></asp:CustomValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="vg1" CssClass="btn btn-primary navbar-btn" />
                </div>
            </div>
            <%--<asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>--%>
            <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Error:</span>
                <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
            </div>
        </div>
        <div class="panel-heading">
            <h4>
                <asp:Label ID="lblRequisitionItemList" runat="server" Font-Bold="True"></asp:Label></h4>
        </div>
        <asp:GridView ID="dgvRequisitionItemList" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDeleting="dgvRequisitionItemList_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ItemId" HeaderText="ItemId" ReadOnly="True" Visible="false" />
                <asp:BoundField DataField="ItemName" HeaderText="ItemName" ReadOnly="True" />
                <asp:BoundField DataField="RequestedQty" HeaderText="Item RequestedQty" ReadOnly="True" />
                <asp:BoundField DataField="DeliveredQty" HeaderText="Item DeliveredQty" ReadOnly="True" />
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
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-default navbar-btn btn-lg" OnClick="btnSubmit_Click" />
        <%--<asp:Label ID="lblNoti" runat="server" ForeColor="#0066CC"></asp:Label>--%>
    </div>
</asp:Content>
