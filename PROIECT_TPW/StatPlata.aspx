<%@ Page Title="Stat de Plata (A3 Landscape)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StatPlata.aspx.cs" Inherits="StatPlata" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Stat de plata (Format A3 orizontal, cu totaluri)</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Raportul listeaza tabelat toti angajatii pe format <strong>A3 Landscape</strong> si afiseaza totalurile pe coloanele <code>TOTAL_BRUT</code> si <code>VIRAT_CARD</code>. Puteti salva raportul ca fisier PDF pentru luna curenta introducand luna si mentiunile dorite (bonus).
    </div>

    <div class="form-inline" style="margin-bottom:15px;">
        <label>Luna calcul:</label>
        <asp:DropDownList ID="ddlLuna" runat="server" CssClass="form-control">
            <asp:ListItem>Ianuarie</asp:ListItem>
            <asp:ListItem>Februarie</asp:ListItem>
            <asp:ListItem>Martie</asp:ListItem>
            <asp:ListItem>Aprilie</asp:ListItem>
            <asp:ListItem>Mai</asp:ListItem>
            <asp:ListItem>Iunie</asp:ListItem>
            <asp:ListItem>Iulie</asp:ListItem>
            <asp:ListItem>August</asp:ListItem>
            <asp:ListItem>Septembrie</asp:ListItem>
            <asp:ListItem>Octombrie</asp:ListItem>
            <asp:ListItem Selected="True">Noiembrie</asp:ListItem>
            <asp:ListItem>Decembrie</asp:ListItem>
        </asp:DropDownList>
        <label style="margin-left:10px;">Info fisier PDF:</label>
        <asp:TextBox ID="txtInfoPdf" runat="server" CssClass="form-control" Text="2026_Firma" />
        <asp:Button ID="btnExportPdf" runat="server" Text="Salveaza Stat Plata PDF" CssClass="btn btn-success" OnClick="btnExportPdf_Click" />
    </div>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
    <br />

    <div style="overflow-x:auto;">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            HasPrintButton="true" HasExportButton="true" ToolPanelView="None" />
    </div>
</asp:Content>
