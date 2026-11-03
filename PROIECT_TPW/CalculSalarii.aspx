<%@ Page Title="Apel Procedura Stocata" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CalculSalarii.aspx.cs" Inherits="CalculSalarii" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <div class="panel-custom">
            <h2>Modificare Salariu prin Procedura Stocata Oracle (<code>procedura1</code>)</h2>
            <p>Apeleaza procedura stocata <code>procedura1(p_nume, p_salar)</code> din baza de date Oracle si recalculeaza automat darile la stat (CAS 25%, CASS 10%, Impozit 10%) si restul de plata (Virat Card).</p>

            <div class="form-inline" style="margin-top: 15px;">
                <asp:Label ID="Label1" runat="server" Text="Nume Angajat: " AssociatedControlID="txtProcNume"></asp:Label>
                <asp:TextBox ID="txtProcNume" runat="server" CssClass="form-control" placeholder="Ex: Popescu"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="Noul Salar de Baza: " AssociatedControlID="txtProcSalar"></asp:Label>
                <asp:TextBox ID="txtProcSalar" runat="server" CssClass="form-control" placeholder="Ex: 9500"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Button ID="btnExecutaProc" runat="server" Text="Executa Procedura Stocata" CssClass="btn btn-primary" OnClick="btnExecutaProc_Click" />
            </div>

            <br />
            <asp:Label ID="lblProcStatus" runat="server" CssClass="status-msg" Text="Introduceti numele si noul salariu."></asp:Label>

            <hr />
            <h4>Stat de Plata Recalculat:</h4>
            <asp:GridView ID="GridViewProc" runat="server" CssClass="grid-view-custom"></asp:GridView>
        </div>
    </div>
</asp:Content>