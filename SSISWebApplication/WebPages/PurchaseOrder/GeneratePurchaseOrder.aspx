<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GeneratePurchaseOrder.aspx.cs" Inherits="SSISWebApplication.WebPages.PurchaseOrder.GeneratePurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Generate Purchase Order</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:ValidationSummary ID="vs1" runat="server" ForeColor="Red" ValidationGroup="vg1" Width="100%" />
            <div class="row row-padding">
                <div class="col-md-2">
                    Supplier:
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="SupplierDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary navbar-btn" OnClick="btnGenerate_Click" />
                </div>
            </div>

        </div>
        <asp:Label ID="MessageLabel" runat="server"></asp:Label>

        <div id="PrintDiv">
        <asp:Panel ID="newpanel" runat ="server">
        <table style="width:100%;" >
        <tr>
            <td style="width:50%">
                SupplierName:&nbsp;&nbsp; <asp:Label ID="supplierNameLbl" runat="server"></asp:Label></td>
            <td>
                
                Deliver to: Logic University Stationery Store</td>
        </tr>
        <tr>
            <td style="width:50%">
                Contact Name:&nbsp;&nbsp; <asp:Label ID="ContactName" runat="server" Text=""></asp:Label></td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="width:50%">
                Phone Number:&nbsp; <asp:Label ID="PhoneNo" runat="server" Text=""></asp:Label></td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="width:50%">
                Fax Number:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="FaxNo" runat="server" Text=""></asp:Label></td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td style="width:50%">
                Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Address" runat="server" Text=""></asp:Label></td>
            <td>
                
            </td>
        </tr>
    </table>
        </asp:Panel>
        
        <asp:GridView ID="SupplierGridView" runat="server" CssClass="table" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="itemName" HeaderText="Item Name" />
            <asp:BoundField DataField="supplierName" HeaderText="Supplier Name" />
            <asp:BoundField DataField="qtyToOrder" HeaderText="Quantity" />
            <asp:BoundField DataField="amount" HeaderText="Amount" />
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
        <div class="row row-padding">
                <div class="col-md-2">
                    Total Amount:
                </div>
                <div class="col-md-3">
                   <asp:Label ID="AmountLabel" runat="server"></asp:Label>
                </div>
            </div>
    </div>

    <asp:Panel ID="newPanel2" runat ="server">
    <table style="width:100%;">
        <tr>
            <td style="width:50%">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
                <td style="width:50%">
                    
                    Approved By:_____________________________<td>

                    Date:<asp:Label ID="Date" runat="server"></asp:Label>
                </td>
            </tr>
    </table>
        </asp:Panel>
    <br />

    </div>
    <asp:Button ID="PrintButton" runat="server" Text="Print" OnClientClick="doIt()" CssClass="btn btn-default navbar-btn" />

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
