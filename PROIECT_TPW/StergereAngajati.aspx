<%@ Page Title="Stergere Angajati" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StergereAngajati.aspx.cs" Inherits="StergereAngajati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Stergere angajati</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Cautati angajatul dupa Nume si/sau Prenume. Daca se gasesc mai multi angajati cu acelasi nume, selectati inregistrarea dorita din lista si confirmati stergerea.
    </div>

    <div class="form-inline" style="margin-bottom:15px;">
        <label>Nume:</label>
        <asp:TextBox ID="txtNume" runat="server" CssClass="form-control" placeholder="Nume..." />
        <label style="margin-left:10px;">Prenume:</label>
        <asp:TextBox ID="txtPrenume" runat="server" CssClass="form-control" placeholder="Prenume..." />
        <asp:Button ID="btnCauta" runat="server" Text="Cauta angajat" CssClass="btn btn-primary" OnClick="btnCauta_Click" />
    </div>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <asp:GridView ID="gvRezultate" runat="server" AutoGenerateColumns="False" DataKeyNames="NR_CRT"
        CssClass="table table-bordered table-hover"
        OnSelectedIndexChanged="gvRezultate_SelectedIndexChanged">
        <SelectedRowStyle BackColor="#ffe6e6" Font-Bold="true" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="Selecteaza" />
            <asp:BoundField DataField="NR_CRT" HeaderText="NR.CRT" />
            <asp:BoundField DataField="NUME" HeaderText="NUME" />
            <asp:BoundField DataField="PRENUME" HeaderText="PRENUME" />
            <asp:BoundField DataField="FUNCTIE" HeaderText="FUNCTIE" />
            <asp:BoundField DataField="SALAR_BAZA" HeaderText="SALAR_BAZA" />
            <asp:BoundField DataField="VIRAT_CARD" HeaderText="VIRAT_CARD" />
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlConfirmare" runat="server" Visible="false" CssClass="alert alert-danger">
        <asp:Label ID="lblConfirmare" runat="server" Font-Bold="true"></asp:Label>
        <br /><br />
        <asp:Button ID="btnConfirmaStergere" runat="server" Text="Confirm stergerea definitiva" CssClass="btn btn-danger"
            OnClientClick="return confirm('Sigur doriti stergerea acestui angajat din baza de date?');"
            OnClick="btnConfirmaStergere_Click" />
        <asp:Button ID="btnAnuleaza" runat="server" Text="Anuleaza" CssClass="btn btn-default" OnClick="btnAnuleaza_Click" />
    </asp:Panel>
</asp:Content>
