<%@ Page Title="Calcul Salarii (Read-Only &amp; Procedura Stocata)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CalculSalarii.aspx.cs" Inherits="CalculSalarii" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Calcul salarii &amp; Vizualizare generala (Read-Only)</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Salariile se calculeaza automat la fiecare adaugare/actualizare prin triggerul Oracle <code>TRG_ANGAJATI_CALC</code>. Puteti apasa butonul de recalculare pentru a rula explicit procedura stocata tranzactionala <code>SP_RECALCULEAZA_SALARII</code> pentru toti angajatii.
    </div>

    <div class="form-inline" style="margin-bottom:15px;">
        <asp:Button ID="btnRecalculeaza" runat="server" Text="Executa Recalculare Globala (SP_RECALCULEAZA_SALARII)" CssClass="btn btn-primary" OnClick="btnRecalculeaza_Click" />
        &nbsp;|&nbsp;
        <label>Cauta angajat:</label>
        <asp:TextBox ID="txtFiltru" runat="server" CssClass="form-control" placeholder="Nume sau prenume..." />
        <asp:Button ID="btnFiltreaza" runat="server" Text="Filtreaza" CssClass="btn btn-default" OnClick="btnFiltreaza_Click" />
    </div>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <div style="overflow-x:auto;">
        <asp:GridView ID="gvCalcul" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="15"
            CssClass="table table-bordered table-striped table-condensed"
            OnPageIndexChanging="gvCalcul_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="NR_CRT" HeaderText="NR.CRT" />
                <asp:BoundField DataField="NUME" HeaderText="NUME" />
                <asp:BoundField DataField="PRENUME" HeaderText="PRENUME" />
                <asp:BoundField DataField="FUNCTIE" HeaderText="FUNCTIE" />
                <asp:BoundField DataField="SALAR_BAZA" HeaderText="SALAR_BAZA" />
                <asp:BoundField DataField="SPOR_PROC" HeaderText="SPOR_%" />
                <asp:BoundField DataField="PREMII_BRUTE" HeaderText="PREMII_BRUTE" />
                <asp:BoundField DataField="TOTAL_BRUT" HeaderText="TOTAL_BRUT" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="BRUT_IMPOZABIL" HeaderText="BRUT_IMPOZABIL" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="IMPOZIT" HeaderText="IMPOZIT" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="CAS" HeaderText="CAS" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="CASS" HeaderText="CASS" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="RETINERI" HeaderText="RETINERI" />
                <asp:BoundField DataField="VIRAT_CARD" HeaderText="VIRAT_CARD" ItemStyle-CssClass="calc-col" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
