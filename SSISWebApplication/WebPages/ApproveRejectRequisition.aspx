<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveRejectRequisition.aspx.cs" Inherits="SSISWebApplication.WebPages.ApproveRejectRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Approve/Reject Requisition</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:GridView ID="dgvRequisitionList" runat="server" Width="100%" CssClass="table" AutoGenerateColumns="False" OnRowCommand="dgvRequisitionList_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="requisitionId" HeaderText="Requisition ID" />
                    <asp:BoundField DataField="userId" HeaderText="Requestor Name" ReadOnly="True" />
                    <asp:BoundField DataField="date" HeaderText="Requisition Date" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Reject Reason">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRejectReason" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RequisitionDetails">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnShowDetails" Text="ShowDetails" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' CommandName="ShowDetails" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary navbar-btn" OnClick="btnApproveReject_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-primary navbar-btn" OnClick="btnApproveReject_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
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
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="50%">
            <AlternatingRowStyle BackColor="White" />
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

    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text=""></asp:Label>
    <asp:Label ID="lblRequisitionDetails" runat="server" Font-Bold="True"></asp:Label>
</asp:Content>
