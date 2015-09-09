<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="SSISWebApplication.WebPages.TestPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
    <asp:Button ID="Button3" runat="server" Text="Notify Reorders" OnClick="Button3_Click"/>
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Get Back Authority" />
    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Button" />
    <asp:Button ID="Button6" runat="server" Text="Button" OnClick="Button6_Click" />
    <br />
    <br />
    <br />

    <script>

        function doIt()
        {
            var prtContent = document.getElementById("PrintDiv");
            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

    </script>
    <div id ="PrintDiv">
    <input id="Button7" type="button" value="button" onclick="doIt()"/><br />
        </div>
</asp:Content>
