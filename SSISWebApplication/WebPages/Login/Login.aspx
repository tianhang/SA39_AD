<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSISWebApplication.WebPages.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body class="login-page-bg">
    <form id="form1" runat="server">
        <div class="jumbotron login-placeholder container body-content">
            <div class="row login-header">
                <asp:Image runat="server" ImageUrl="~/Images/SystemLogo/feature-banner.png" CssClass="logo" /></div>            
            <div class="row row-padding">
                <div class="col-md-4">
                    <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail">Email :</asp:Label></div>
                <div class="col-md-5">
                    <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="vgLogin" CssClass="form-control"></asp:TextBox>                    
                </div>
                <div class="col-md-2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ValidationGroup="vgLogin" ControlToValidate="txtEmail" ErrorMessage="Email Require!" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="vgLogin" ErrorMessage="Invalid Email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red">*</asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-4">
                    <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword">Password :</asp:Label></div>
                <div class="col-md-5">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="vgLogin" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvPassword" ValidationGroup="vgLogin" ControlToValidate="txtPassword" ErrorMessage="Password Require!" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row row-padding">
                <div class="col-md-4"></div>
                <div class="col-md-5">
                    <asp:Button ID="butLogin" runat="server" ValidationGroup="vgLogin" Text="Login" CssClass="btn btn-primary navbar-btn btn-lg" OnClick="butLogin_Click"></asp:Button>
                    <asp:Button ID="butCanel" runat="server"  Text="Cancel" CssClass="btn btn-default navbar-btn btn-lg" OnClick="butCanel_Click" ></asp:Button>
                </div>
            </div>
            <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Error:</span>
                <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
            </div>
            <asp:ValidationSummary ID="vsLogin" ValidationGroup="vgLogin" runat="server" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
