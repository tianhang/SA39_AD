<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenerateStationaryRetrievalForm.aspx.cs" Inherits="SSISWebApplication.WebPages.GenerateStationaryRetrievalForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div id ="PrintDiv">
        <div class="panel-heading">
            <h4>Stationary Retrieval Form</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:GridView ID="RetrievalFormGridView" runat="server" Width="100%" AutoGenerateColumns="False" OnRowEditing="RetrievalFormGridView_RowEditing" OnRowCancelingEdit="RetrievalFormGridView_RowCancelingEdit" OnRowUpdating="RetrievalFormGridView_RowEditing_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                        InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="Description" HeaderText="Description"
                        InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="BinNumber" HeaderText="Bin #"
                        InsertVisible="False" ReadOnly="True" />
                    <asp:BoundField DataField="RetrievalQty" HeaderText="Retrieval Quantity"
                        InsertVisible="False" ReadOnly="True" />
                    <asp:TemplateField HeaderText="Stock Balance">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("StockBalance") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="StockBalance" runat="server" Text='<%# Bind("StockBalance") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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

            </div>

        <asp:Button ID="PrintButton" runat="server" Text="Print" OnClientClick="doIt()" CssClass="btn btn-default navbar-btn" />

        <asp:Label ID="MsgLabel" runat="server" Text="There are no remaining requisitions."></asp:Label>
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

    </script>
</asp:Content>
