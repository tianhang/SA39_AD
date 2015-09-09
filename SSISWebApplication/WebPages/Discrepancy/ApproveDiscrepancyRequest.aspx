<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveDiscrepancyRequest.aspx.cs" Inherits="SSISWebApplication.WebPages.Discrepancy.ApproveDiscrepancyRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Approve/Reject Discrepancy</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <asp:GridView ID="gridviewApproveDiscrepancy" Width="100%" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="gridviewApproveDiscrepancy_RowCancelingEdit" OnRowEditing="gridviewApproveDiscrepancy_RowEditing" OnRowUpdating="gridviewApproveDiscrepancy_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <%--                        <asp:CheckBoxField DataField="Status" HeaderText="Approve" />
                        <asp:CheckBoxField DataField="Status" HeaderText="Reject" />--%>

                        <asp:TemplateField HeaderText="Select">
                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBoxDiscrepancy" runat="server" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxDiscrepancy" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DiscrepancyId" HeaderText="Discrepancy Id" ReadOnly="True">
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemDescription" HeaderText="Item Name" ReadOnly="True">
                        </asp:BoundField>
                        <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="True">
                        </asp:BoundField>
                        <asp:BoundField DataField="Qunatity" HeaderText="Quantity" ReadOnly="True">
                        </asp:BoundField>
                        <asp:BoundField DataField="amount" HeaderText="Totle prices" ReadOnly="True">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Reason">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("reason") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("reason") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                        <asp:TemplateField HeaderText="Reject Reason">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("RejectReason") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("RejectReason") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Approve and Reject Reason">
                            <ItemTemplate>
                                <asp:TextBox ID="ApproveRejectReason" runat="server" Text='<%# Bind("RejectReason") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubmitDate" HeaderText="Submmited Date" ReadOnly="True" />
                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" ControlStyle-CssClass="btn btn-primary navbar-btn" >
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
            <div class="row row-padding">
                <asp:Button ID="btnSelect" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnSelect_Click" Text="Select all" />
                <asp:Button ID="btnDeselect" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnDeselect_Click" Text="Deselect all" />
                <asp:Button ID="btnReject" runat="server" CssClass="btn btn-default navbar-btn" OnClick="btnReject_Click" Text="Reject" />
                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-default navbar-btn" Text="Approve" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
