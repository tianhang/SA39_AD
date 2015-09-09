<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SSISWebApplication._Default" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron center">
        <div class="container text-center">
            <asp:Image runat="server" ImageUrl="~/Images/SystemLogo/system_logo.png" CssClass="system-logo" />
        </div>
        <div class="container text-center">
            <CR:CrystalReportViewer ID="crvRequisition" runat="server" AutoDataBind="true" DisplayStatusbar="False" DisplayToolbar="False" EnableToolTips="False" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasDrilldownTabs="False" HasDrillUpButton="False" HasExportButton="False" HasGotoPageButton="False" HasPageNavigationButtons="False" HasPrintButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" HasZoomFactorList="False" ToolPanelView="None"/>
        </div>
    </div>

</asp:Content>
