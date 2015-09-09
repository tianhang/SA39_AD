<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewDisbursement.aspx.cs" Inherits="SSISWebApplication.WebPages.ViewDisbursement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>View Disbursement</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <div class="col-md-3">
                    <asp:DropDownList ID="DepartmentDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <asp:RadioButton ID="PendingRadioButton" runat="server" GroupName="StatusGroup" Text="Outstanding" CssClass="radio" />
            <asp:RadioButton ID="CompletedRadioButton" runat="server" GroupName="StatusGroup" Text="Completed" CssClass="radio" />
            <div class="row row-padding">
                <div class="col-md-3">
                    <asp:Button ID="Generatebtn" runat="server" Text="Show" OnClick="Generatebtn_Click" CssClass="btn btn-primary navbar-bt" />
                </div>
            </div>
            <asp:Panel ID="DisbursementPanel" runat="server">
                <asp:GridView ID="DisbursementGridView" runat="server" Width="100%" OnRowCommand="DisbursementGridView_RowCommand" OnRowDataBound="DisbursementGridView_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="SelectDetails" HeaderText="Select" ControlStyle-CssClass="btn btn-primary navbar-bt" ShowHeader="True" Text="Details" />
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
            </asp:Panel>
        </div>
        <asp:Panel ID="DisbursementDetailsPanel" runat="server" CssClass="row-padding">
            <div id="PrintDiv2" class="row-padding">
                Department :
        <asp:Label ID="DepartmentLabel" runat="server" Text="Label"></asp:Label>
                <br />
                Collection Point :<asp:Label ID="CollectionPointLabel" runat="server" Text="Label"></asp:Label>
                <br />
                Representative :
        <asp:Label ID="RepresentativeLabel" runat="server" Text="Label"></asp:Label>
                <br />
                Date :
        <asp:Label ID="DateLabel" runat="server" Text="Label"></asp:Label>
                <br />
                Delivery Date :
        <asp:Label ID="DelDateLabel" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:GridView ID="DisbursementDetailsGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
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

            <asp:Button ID="Button2" runat="server" Text="Print" OnClientClick="doIt2()" CssClass="btn btn-default navbar-btn" />

        </asp:Panel>

        <asp:Panel ID="CompleteDeliveryPanel" runat="server">
            <div id="PrintDiv" class="row-padding">
                Department :
            <asp:Label ID="DepartmentLabel1" runat="server" Text="Label"></asp:Label>
                <br />
                Collection Point :
            <asp:Label ID="CollectionPointLabel1" runat="server" Text="Label"></asp:Label>
                <br />
                Representative :
            <asp:Label ID="RepresentativeLabel1" runat="server" Text="Label"></asp:Label>
                <br />
                Date :
            <asp:Label ID="DateLabel1" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:GridView ID="CompleteDetailsGridView" runat="server" AutoGenerateColumns="False" OnRowDataBound="CompleteDetailsGridView_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ItemId" HeaderText="ItemId"
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="ItemName" HeaderText="ItemName"
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="RequestedQty" HeaderText="RequestedQty"
                            InsertVisible="False" ReadOnly="True" />

                        <asp:TemplateField HeaderText="Delivered Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="DelQtyTextBox" runat="server" Text="" CssClass="form-control"></asp:TextBox>
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
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Reset" CssClass="btn btn-primary navbar-bt" />

            <asp:Button ID="SelectAllButton" runat="server" Text="Fullfill All" CssClass="btn btn-primary navbar-btn" OnClick="SelectAllButton_Click" />
            <br />
            <asp:Button ID="SubmitButton" runat="server" Text="Complete Delivery" CssClass="btn btn-primary navbar-btn" OnClick="SubmitButton_Click" />

            <asp:Button ID="PrintButton" runat="server" Text="Print" OnClientClick="doIt()" CssClass="btn btn-default navbar-btn" />

        </asp:Panel>
        <br />
        <%--<asp:Label ID="ErrorMessageLabel" runat="server" Text="Label"></asp:Label>--%>
        <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Error:</span>
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </div>

    </div>



    <script>

        function doIt() {
            var prtContent = document.getElementById("PrintDiv");
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function doIt2() {
            var prtContent = document.getElementById("PrintDiv2");
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

    </script>
</asp:Content>
