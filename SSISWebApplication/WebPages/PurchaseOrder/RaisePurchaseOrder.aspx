<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaisePurchaseOrder.aspx.cs" Inherits="SSISWebApplication.WebPages.PurchaseOrder.RaisePurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Raise Purchase Order</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <div class="col-md-2">
                    Category:
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="form-control"
                        onselectedindexchanged="CategoryDropDownList_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <asp:GridView ID="SelectGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
            OnRowCommand="SelectGridView_RowCommand" OnRowDataBound="SupplierGridView_RowDataBound"
            style="width: 100%;" AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanged="DetailBtn_Click"
            OnPageIndexChanging="SelectGridView_PageIndexChanging">
            <alternatingrowstyle backcolor="White" />
            <columns>
                        <asp:BoundField DataField="description" HeaderText="Description">
                        </asp:BoundField>
                        <asp:BoundField DataField="reorderLevel" HeaderText="Reorder Level">
                        </asp:BoundField>
                        <asp:BoundField DataField="reorderQty" HeaderText="Reorder Qty">
                        </asp:BoundField>
                        <asp:BoundField DataField="stockBalance" HeaderText="Stock Balance">
                        </asp:BoundField>
                        <asp:BoundField DataField="uomName" HeaderText="UOM">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="100px" ></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Suplier">
                            <ItemTemplate>
                                <asp:DropDownList ID="SupplierDropDownList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="SupplierDropDownList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price" >
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text="" Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" >
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" runat="server" Width="50px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" Text="Add" CssClass="btn btn-primary navbar-btn"
                                    CommandName="AddToTable"
                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True"  SelectText="Detail" ButtonType="Button" ControlStyle-CssClass="btn btn-primary navbar-btn"/>
                    </columns>
            <footerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
            <headerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
            <pagerstyle backcolor="#2461BF" forecolor="White" horizontalalign="Center" />
            <rowstyle backcolor="#EFF3FB" />
            <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
            <sortedascendingcellstyle backcolor="#F5F7FB" />
            <sortedascendingheaderstyle backcolor="#6D95E1" />
            <sorteddescendingcellstyle backcolor="#E9EBEF" />
            <sorteddescendingheaderstyle backcolor="#4870BE" />
        </asp:GridView>
            </div>
        </div>
         <%--<asp:Label ID="Statement" runat="server" ForeColor="Red" Text="Label"></asp:Label>--%>
         <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
            <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
            <span class="sr-only">Error:</span>
            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
        </div>
        <asp:GridView ID="UpdateGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" style="width: 50%" AutoGenerateColumns="False"
            onrowdeleting="UpdateGridview_RowDeleting">
            <alternatingrowstyle backcolor="White" />
            <columns>
                <asp:BoundField HeaderText="Item Description" DataField="itemName"/>
                <asp:BoundField HeaderText="Qty" DataField="qtyToOrder"/>
                <asp:BoundField HeaderText="Supplier" DataField="supplierName"/>
                <asp:BoundField HeaderText="Price" DataField="price"/>
                <asp:BoundField HeaderText="Amount" DataField="amount"/>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button"/>
            </columns>
            <footerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
            <headerstyle backcolor="#507CD1" font-bold="True" forecolor="White" />
            <pagerstyle backcolor="#2461BF" forecolor="White" horizontalalign="Center" />
            <rowstyle backcolor="#EFF3FB" />
            <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
            <sortedascendingcellstyle backcolor="#F5F7FB" />
            <sortedascendingheaderstyle backcolor="#6D95E1" />
            <sorteddescendingcellstyle backcolor="#E9EBEF" />
            <sorteddescendingheaderstyle backcolor="#4870BE" />
        </asp:GridView>
        <asp:Button ID="SubmitBtn" runat="server" onclick="SubmitBtn_Click"
            Text="Submit" CssClass="btn btn-default navbar-btn" />
    </div>
    <script lang="javascript" type="text/javascript">

        function openNewWin(url) {

            var x = window.open(url, 'mynewwin', 'width=600,height=600,toolbar=1, directories=no,status=no,menubar=no,scrollbars=yes,resizable=no');

            x.focus();

        }

    </script>    
</asp:Content>
