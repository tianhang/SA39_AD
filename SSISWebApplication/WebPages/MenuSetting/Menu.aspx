<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="SSISWebApplication.WebPages.MenuSetting.Menu" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Menu Setting</h4>
        </div>
        <div class="panel-body row-padding ">
            <div class="row">
                <div class="col-md-4">
                    <asp:TreeView runat="server" ID="tvMenuSetting" BorderStyle="Inset" ShowLines="True" Width="100%" BorderColor="#CCCCCC" BorderWidth="1px" OnSelectedNodeChanged="tvMenuSetting_SelectedNodeChanged">
                        <HoverNodeStyle BackColor="#CCFFFF" BorderStyle="Outset" />
                        <SelectedNodeStyle BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TreeView>
                </div>
                <div class="col-md-1">
                    <div class="row">
                        <asp:Button ID="butAddParent" runat="server" Text="+ Parent" CssClass="btn btn-default navbar-btn" OnClick="butAddParent_Click"></asp:Button>
                    </div>
                    <div class="row">
                        <asp:Button ID="butAddChild" runat="server" Text="+ Child" CssClass="btn btn-default navbar-btn" OnClick="butAddChild_Click"></asp:Button>
                    </div>
                </div>
                <div class="col-md-7">
                    <asp:Label runat="server" ID="lblMenuID" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblParentID" Visible="false"></asp:Label>
                    <div class="row row-padding" id="dvParentMenu" runat="server">
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblParentMenu" AssociatedControlID="ddlParentMenu">Parent Menu :</asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlParentMenu" runat="server" ValidationGroup="vgMenu" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row row-padding">
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblMenuName" AssociatedControlID="txtMenuName">Menu Name :</asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtMenuName" runat="server" ValidationGroup="vgMenu" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row row-padding">
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblUrl" AssociatedControlID="txtUrl">Url :</asp:Label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtUrl" runat="server" ValidationGroup="vgMenu" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row row-padding">
                        <div class="col-md-3">
                            <asp:Label runat="server" ID="lblRole" AssociatedControlID="chkRoleList">Role :</asp:Label></div>
                        <div class="col-md-7">
                            <asp:CheckBoxList ID="chkRoleList" runat="server" RepeatLayout="UnorderedList"></asp:CheckBoxList></div>

                    </div>
                    <div class="row row-padding">
                        <div class="col-md-3"></div>
                        <div class="col-md-7">
                            <asp:Button ID="butSave" runat="server" ValidationGroup="vgMenu" Text="Add Parent" CssClass="btn btn-default navbar-btn btn-lg" OnClick="butSave_Click"></asp:Button>
                            <asp:Button ID="butDelete" runat="server" Text="Delete" CssClass="btn btn-default navbar-btn btn-lg" OnClick="butDelete_Click" OnClientClick="Confirm()"></asp:Button>
                            <asp:Button ID="butCancel" runat="server" ValidationGroup="vgMenu" Text="Cancel" CssClass="btn btn-default navbar-btn btn-lg" OnClick="butCancel_Click"></asp:Button>
                        </div>
                    </div>
                    <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
                        <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                        <span class="sr-only">Error:</span>
                        <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete menu?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
