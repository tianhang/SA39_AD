<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionTrendReportGenerator.aspx.cs" Inherits="SSISWebApplication.WebPages.Report.ReportGenerator.RequisitionTrendReportGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Requisition Trend Report</h4>
        </div>
        <div class="panel-body row-padding " style="border: solid; border-color: darkgray; border-width: 1px;">
            <div class="row row-padding">
                <div class="col-md-3">Department Name:</div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlDepartment" class="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-3">Category Name:</div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlCategoryName" class="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row row-padding">
                <div class="col-md-3">Compare Months and Years :</div>
                <div class="col-md-3">
                    <a class="btn btn-primary navbar-btn btn-sm" data-toggle="modal"
                        data-target="#basicModal" aria-label="Left Align" id="btnPlus" runat="server">
                        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                    </a>
                    <asp:BulletedList ID="blCompareDateList" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" BulletImageUrl="~/Images/SystemLogo/Calendar.png" BulletStyle="CustomImage">
                    </asp:BulletedList>
                    <div class="modal fade" id="basicModal" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
                        <div class="modal-dialog modal-sm" style="width:300px;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">X</button>
                                    <h4 class="modal-title" id="myModalLabel">Compare Months and Years</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row row-padding">
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Width="120px">
                                                <asp:ListItem Text="January" Value="January" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="February" Value="February"></asp:ListItem>
                                                <asp:ListItem Text="March" Value="March"></asp:ListItem>
                                                <asp:ListItem Text="April" Value="April"></asp:ListItem>
                                                <asp:ListItem Text="May" Value="May"></asp:ListItem>
                                                <asp:ListItem Text="June" Value="June"></asp:ListItem>
                                                <asp:ListItem Text="July" Value="July"></asp:ListItem>
                                                <asp:ListItem Text="August" Value="August"></asp:ListItem>
                                                <asp:ListItem Text="September" Value="September"></asp:ListItem>
                                                <asp:ListItem Text="October" Value="October"></asp:ListItem>
                                                <asp:ListItem Text="November" Value="November"></asp:ListItem>
                                                <asp:ListItem Text="December" Value="December"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" Width="100px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button CssClass="btn btn-primary" ID="btnAdd" OnClick="btnAddDates_Click" runat="server" Text="Add"></asp:Button>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
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
