<%@ Page Title="Rapoarte Crystal Reports SP33" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Rapoarte.aspx.cs" Inherits="Rapoarte" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <div class="panel-custom">
            <h2>Generare Fluturas / Stat de Plata (Crystal Reports SP33)</h2>

            <!-- Exact controls required in Fig. 3 of Crystal-Reports-SP33 PDF -->
            <asp:GridView ID="GridView1" runat="server" CssClass="grid-view-custom"></asp:GridView>
            <br />

            <asp:Label ID="Label1" runat="server" Text="ID mai mare decat: "></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Text="0"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Cauta" OnClick="Button1_Click" CssClass="btn btn-primary btn-sm" />
            <br /><br />

            <asp:Label ID="lblReportStatus" runat="server" CssClass="status-msg" Text="Raportul este pregatit."></asp:Label>
            <hr />

            <!-- CrystalReportViewer Control -->
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
        </div>
    </div>
</asp:Content>