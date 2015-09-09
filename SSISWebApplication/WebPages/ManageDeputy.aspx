<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDeputy.aspx.cs" Inherits="SSISWebApplication.WebPages.ManageDeputy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h4>Manage Deputy</h4>
        </div>
        <div class="panel-body row-padding " id="PrintDV" style="border: solid; border-color: darkgray; border-width: 1px;">
            <asp:Panel ID="Panel1" runat="server">
                <div class="row row-padding">
                    <div class="col-md-2">
                        UID:
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="UIDLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row row-padding">
                    <div class="col-md-2">
                        Name:
                    
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="NameLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row row-padding">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="StartDateLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row row-padding">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="EndDateLabel" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="row row-padding">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="UpdpateButton" runat="server" Text="Extend" OnClick="UpdpateButton_Click" CssClass="btn btn-primary navbar-btn" />
                        <asp:Button ID="RemoveButton" runat="server" Text="Remove" OnClick="RemoveButton_Click" CssClass="btn btn-primary navbar-btn" />
                    </div>
                </div>





                <br />

                <br />

                <br />

                <br />

                <br />

            </asp:Panel>
        </div>
        <asp:GridView ID="EmployeeGridView" runat="server" Width="100%" AutoGenerateColumns="False" OnRowCommand="EmployeeGridView_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="EmployeeGridView_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="UserId" HeaderText="ID"
                    InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="UserName" HeaderText="Name"
                    InsertVisible="False" ReadOnly="True" />
                <asp:ButtonField ButtonType="Button" CommandName="Select" ControlStyle-CssClass="btn btn-primary navbar-btn" HeaderText="Assign" ShowHeader="True" Text="Assign">
                    <ControlStyle CssClass="btn btn-primary navbar-btn"></ControlStyle>
                </asp:ButtonField>
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
        <asp:Panel ID="Panel2" runat="server">
            <div class="row row-padding">
                <div class="col-md-1">
                    <asp:Label ID="StLabel" runat="server" Text="Start Date : "> </asp:Label>
                </div>
                <div class="col-md-3">                    
           <asp:Calendar ID="StartCalendar" runat="server" OnDayRender="Calendar_DayRender"></asp:Calendar>
                </div>
                <div class="col-md-1">
            End Date :
                </div>
                <div class="col-md-3">
                    <asp:Calendar ID="EndCalendar" runat="server" OnDayRender="Calendar_DayRender" OnSelectionChanged="EndCalendar_SelectionChanged"></asp:Calendar>
                </div>
            </div>
        

            <asp:Button ID="SubmitButton" runat="server" Text="Save" OnClick="SubmitButton_Click" CssClass="btn btn-default navbar-btn btn-lg" />
        </asp:Panel>

        <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Text="End Date cannot be earlier than start date or the previous end date."></asp:Label>
    </div>





</asp:Content>
