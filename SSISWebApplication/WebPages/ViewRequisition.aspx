<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRequisition.aspx.cs" Inherits="SSISWebApplication.WebPages.ViewRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>View Requisition</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="DepLab" runat="server" Text="Department Name:" Width="150px"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="DepartmentDropDownList" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2">
                    <asp:Label ID="StatusLab" runat="server" Text="Status:" Width="150px"></asp:Label>
                </div>
                <div class="col-md-7">
                    <asp:RadioButtonList ID="StatusRadioButtonList" runat="server" RepeatDirection="Horizontal" CellPadding="4" CellSpacing="2" Width="100%">
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-2"></div>
                <div class="col-md-3">
                    <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" CssClass="btn btn-primary navbar-btn" />
                </div>
            </div>
            <asp:GridView ID="RequisitionGridView" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="RequisitionGridView_SelectedIndexChanged"
                OnRowDataBound="RequisitionGridView_RowDataBound" OnRowCommand="RequisitionGridView_RowCommand"
                EmptyDataText="There is no record for your serach"
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="requisitionId" HeaderText="Requisition Id" HeaderStyle-Width="200px">
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="date" HeaderText="Date" HeaderStyle-Width="200px">
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="rejectreason" HeaderText="Reject Reason" Visible="false" HeaderStyle-Width="200px">
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:BoundField>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Details" ControlStyle-CssClass="btn btn-primary navbar-btn">
                        <ControlStyle Width="80px"></ControlStyle>
                    </asp:CommandField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="CancelBtn" runat="server" Text="Cancel" Width="80px"
                                CommandName="cancelRequisition" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
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
        <div class="panel-heading">
            <h5>Requisition Detail</h5>
        </div>
        <asp:GridView ID="DetailGridView" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="itemId" HeaderText="itemId" HeaderStyle-Width="200px">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="itemName" HeaderText="itemName" HeaderStyle-Width="200px">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="requestedQty" HeaderText="Requested Qty" HeaderStyle-Width="200px">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="deliveredQty" HeaderText="Delivered Qty" HeaderStyle-Width="200px">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundField>
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
