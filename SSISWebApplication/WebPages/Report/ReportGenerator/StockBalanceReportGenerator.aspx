<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockBalanceReportGenerator.aspx.cs" Inherits="SSISWebApplication.WebPages.Report.ReportGenerator.StockBalanceReportGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Stock Balance Report</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">            
            <div class="row row-padding">
                <div class="col-md-3">Category Name:</div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlCategoryName" class="form-control" OnSelectedIndexChanged="ddlCategoryName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-3">Item Name:</div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlItemName" class="form-control"></asp:DropDownList>
                </div>
            </div>            
            <div class="row row-padding">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <asp:Button runat="server" ID="butGenerate" class="btn btn-default navbar-btn" Text="Generate" OnClick="butGenerate_Click"></asp:Button>
                    <asp:Button runat="server" ID="butCancel" class="btn btn-default navbar-btn" Text="Cancel" OnClick="butCancel_Click"></asp:Button>
                </div>
            </div>
             <div class="alert alert-danger" role="alert" id="dvAlert" runat="server" visible="false">
                <span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>
                <span class="sr-only">Error:</span>
                <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
