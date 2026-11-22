<%@ Page Title="Adaugare Angajati" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdaugareAngajati.aspx.cs" Inherits="AdaugareAngajati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Adaugare angajat nou</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Completati campurile de mai jos. Valorile implicite la angajare (<code>SPOR_% = 0</code>, <code>PREMII_BRUTE = 0</code>, <code>RETINERI = 0</code>) sunt precompletate. Dupa salvare, salariul se calculeaza automat, rezultatul este afisat mai jos, iar formularul ramane deschis pentru adaugarea urmatorului angajat.
    </div>

    <table class="table table-bordered" style="max-width:650px;">
        <tr>
            <td><strong>Nume:</strong></td>
            <td>
                <asp:TextBox ID="txtNume" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNume" runat="server" ControlToValidate="txtNume" ErrorMessage="Introduceti numele!" ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revNume" runat="server" ControlToValidate="txtNume" ValidationExpression="^[a-zA-Z\-\s]{1,50}$" ErrorMessage="Doar litere (max 50 caractere)!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Prenume:</strong></td>
            <td>
                <asp:TextBox ID="txtPrenume" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPrenume" runat="server" ControlToValidate="txtPrenume" ErrorMessage="Introduceti prenumele!" ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revPrenume" runat="server" ControlToValidate="txtPrenume" ValidationExpression="^[a-zA-Z\-\s]{1,50}$" ErrorMessage="Doar litere (max 50 caractere)!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Functie:</strong></td>
            <td>
                <asp:TextBox ID="txtFunctie" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvFunctie" runat="server" ControlToValidate="txtFunctie" ErrorMessage="Introduceti functia!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Salar de baza (RON):</strong></td>
            <td>
                <asp:TextBox ID="txtSalarBaza" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvSalar" runat="server" ControlToValidate="txtSalarBaza" ErrorMessage="Introduceti salarul de baza!" ForeColor="Red" Display="Dynamic" />
                <asp:RangeValidator ID="rvSalar" runat="server" ControlToValidate="txtSalarBaza" Type="Integer" MinimumValue="1" MaximumValue="1000000" ErrorMessage="Introduceti un numar intreg intre 1 si 1.000.000!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Spor (%):</strong></td>
            <td>
                <asp:TextBox ID="txtSpor" runat="server" CssClass="form-control" Text="0" />
                <asp:RequiredFieldValidator ID="rfvSpor" runat="server" ControlToValidate="txtSpor" ErrorMessage="Introduceti sporul (0 implicit)!" ForeColor="Red" Display="Dynamic" />
                <asp:RangeValidator ID="rvSpor" runat="server" ControlToValidate="txtSpor" Type="Integer" MinimumValue="0" MaximumValue="100" ErrorMessage="Procentul trebuie sa fie intre 0 si 100!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Premii brute (RON):</strong></td>
            <td>
                <asp:TextBox ID="txtPremii" runat="server" CssClass="form-control" Text="0" />
                <asp:RangeValidator ID="rvPremii" runat="server" ControlToValidate="txtPremii" Type="Integer" MinimumValue="0" MaximumValue="1000000" ErrorMessage="Introduceti un numar intreg >= 0!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Retineri (RON):</strong></td>
            <td>
                <asp:TextBox ID="txtRetineri" runat="server" CssClass="form-control" Text="0" />
                <asp:RangeValidator ID="rvRetineri" runat="server" ControlToValidate="txtRetineri" Type="Integer" MinimumValue="0" MaximumValue="1000000" ErrorMessage="Introduceti un numar intreg >= 0!" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td><strong>Poza angajat (optional - BLOB):</strong></td>
            <td>
                <asp:FileUpload ID="fuPoza" runat="server" CssClass="form-control" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-right">
                <asp:Button ID="btnAdauga" runat="server" Text="Adauga angajat &amp; Calculeaza salariu" CssClass="btn btn-success" OnClick="btnAdauga_Click" />
            </td>
        </tr>
    </table>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <asp:Panel ID="pnlRezultat" runat="server" Visible="false">
        <h4>Ultimul angajat adaugat (salariu calculat automat):</h4>
        <div style="overflow-x:auto;">
            <asp:GridView ID="gvUltimul" runat="server" AutoGenerateColumns="True" CssClass="table table-bordered table-condensed" />
        </div>
        <asp:Image ID="imgPozaNoua" runat="server" Visible="false" Width="120px" style="border:1px solid #ccc; margin-top:8px;" />
    </asp:Panel>
</asp:Content>
