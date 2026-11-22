<%@ Page Title="Fluturasi Salarii" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fluturasi.aspx.cs" Inherits="Fluturasi" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Fluturasi de salariu (Format 2 coloane)</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Implicit se genereaza raportul global cu fluturasii tuturor angajatilor. Introduceti numele sau prenumele unui angajat si apasati <em>Filtreaza fluturas</em> pentru a afisa si tipari doar fluturasul angajatului cautat.
    </div>

    <div class="form-inline" style="margin-bottom:15px;">
        <label>Cauta angajat (Nume/Prenume):</label>
        <asp:TextBox ID="txtCautaFluturas" runat="server" CssClass="form-control" placeholder="Ex: Popescu" />
        <asp:Button ID="btnFiltreaza" runat="server" Text="Filtreaza fluturas" CssClass="btn btn-primary" OnClick="btnFiltreaza_Click" />
        <asp:Button ID="btnToti" runat="server" Text="Raport global (toti angajatii)" CssClass="btn btn-default" OnClick="btnToti_Click" />
    </div>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
    <br />

    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        HasPrintButton="true" HasExportButton="true" ToolPanelView="None" />
</asp:Content>
