<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSupplier.aspx.cs" Inherits="SSISWebApplication.WebPages.ManageSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Manage Supplier</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:GridView ID="SupplierGridView" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                OnRowEditing=" SupplierGridView_RowEditing" OnRowUpdating="SupplierGridView_RowUpdating" OnRowCancelingEdit="SupplierGridView_CancelEdit"
                OnSelectedIndexChanged="SupplierGridView_SelectedIndexChanged" OnRowDeleting="SupplierGridView_RowDeleting"
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>                   
                    <asp:TemplateField HeaderText="SupplierId">
                        <ItemTemplate>
                            <asp:Label ID="LabelSupplierId" runat="server" Text='<%# Bind("supplierId") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxSupplierId" runat="server" ValidationGroup="AddNew" Width="100px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxSupplierId" Display="Dynamic" ErrorMessage="Please enter SupplierId" ForeColor="Red" ToolTip="Please enter SupplierId" ValidationGroup="AddNew">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SupplierName">
                        <ItemTemplate>
                            <asp:Label ID="LabelSupplierName" runat="server" Text='<%# Bind("name") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxSupplierName" runat="server" ValidationGroup="AddNew" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxSupplierName" Display="Dynamic" ErrorMessage="Please enter SupplierName" ForeColor="Red" ToolTip="Please enter SupplierName" ValidationGroup="AddNew">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ContactName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxEditContactName" runat="server" Text='<%# Bind("contactName") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelContactName" runat="server" Text='<%# Bind("contactName") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxContactName" runat="server" ValidationGroup="AddNew" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxContactName" Display="Dynamic" ErrorMessage="Please enter  Contact Name" ForeColor="Red" ToolTip="Please enter Contact Name" ValidationGroup="AddNew">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PhoneNo">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxEditPhoneNo" runat="server" Text='<%# Bind("phoneNo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelPhoneNo" runat="server" Text='<%# Bind("phoneNo") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxPhoneNo" runat="server" ValidationGroup="AddNew" Width="100px" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxPhoneNo" Display="Dynamic" ErrorMessage="Please enter 8 digit phone No " ForeColor="Red" ToolTip="Please enter 8 digit phone No " ValidationExpression="^[\d]{8}$" ValidationGroup="AddNew">*</asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FaxNo">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxEditFaxNo" runat="server" Text='<%# Bind("faxNo") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelFaxNo" runat="server" Text='<%# Bind("faxNo") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxFaxNo" runat="server" ValidationGroup="AddNew" Width="100px" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxFaxNo" Display="Dynamic" ErrorMessage="Please enter 7 digit Fax No " ForeColor="Red" ToolTip="Please enter 7 digit Fax No " ValidationExpression="^[\d]{7}$" ValidationGroup="AddNew">*</asp:RegularExpressionValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBoxEditAddress" runat="server" Text='<%# Bind("address") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelAddress" runat="server" Text='<%# Bind("address") %>' />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="TextBoxAddress" runat="server" ValidationGroup="AddNew" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxAddress" Display="Dynamic" ErrorMessage="Please enter Address" ForeColor="Red" ToolTip="Please enter Address" ValidationGroup="AddNew">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                     <asp:CommandField ShowSelectButton="True" SelectText="Price Detail" ControlStyle-CssClass="btn btn-primary navbar-btn" ButtonType="Button" >
<ControlStyle CssClass="btn btn-primary navbar-btn"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary navbar-btn" >
<ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>
                    </asp:CommandField>
                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-primary navbar-btn" >
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
        <asp:Label ID="Label2" runat="server"></asp:Label>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True" ValidationGroup="AddNew" />

        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True" />
        
    <asp:Button ID="AddNew" runat="server" Text="Add New" OnClick="AddNew_Click" ValidationGroup="AddNew" CssClass="btn btn-primary navbar-btn" />

        <asp:GridView ID="PriceGridView" runat="server" AutoGenerateColumns="False" OnRowEditing="PriceGridView_RowEditing"
        OnRowCancelingEdit="PriceGridView_RowCancelingEdit" OnRowUpdating="PriceGridView_RowUpdating"
        OnPageIndexChanging="PriceGridView_PageIndexChanging" AllowPaging="True" OnRowDeleting="PriceGridView_RowDeleting" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ItemID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="LabelItemID" runat="server" Text='<%# Bind("itemId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemStyle Width="300px" />
                <ItemTemplate>
                    <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("itemName") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditPrice" runat="server" Text='<%# Bind("price") %>' Width="100px" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxEditPrice" Display="Dynamic" ErrorMessage="Please enter integer or one-digit decimal" ForeColor="Red" ToolTip="Please enter integer or one-digit decimal" ValidationExpression="^(\d+\.\d{1})|(\d+)$">*</asp:RegularExpressionValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelPrice" runat="server" Text='<%# Bind("price") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary navbar-btn" >
<ControlStyle CssClass="btn btn-primary navbar-btn" ForeColor="White"></ControlStyle>
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-primary navbar-btn" >
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
</asp:Content>
