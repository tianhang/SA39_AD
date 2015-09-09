<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="SSISWebApplication.WebPages.Report.ReportViewer.ReportViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="panel panel-default">
        <div class="panel-heading">
            <h4><asp:Label ID="lblReportHeader" runat="server"></asp:Label></h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
             <div class="row row-padding">

                <CR:CrystalReportViewer ID="crvReportViewer" runat="server" AutoDataBind="true" />

            </div>
        </div>
    </div>
</asp:Content>
