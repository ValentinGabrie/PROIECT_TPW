<%@ Page Title="Modificare Procente (Protejat Parola)" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ModifProcente.aspx.cs" Inherits="ModifProcente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Modificare procente contributii &amp; impozit (MODIF_PROCENTE)</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Accesul la modificarea cotelor procentuale (<code>CAS</code>, <code>CASS</code>, <code>IMPOZIT</code>) este protejat prin parola criptata (SHA-256) stocata in tabela <code>PROCENTE</code> (parola implicita: <code>admin123</code>). Dupa salvare, salariile tuturor angajatilor sunt recalculate automat.
    </div>

    <asp:Panel ID="pnlAutentificare" runat="server" CssClass="panel panel-warning" style="max-width:500px;">
        <div class="panel-heading"><strong>Verificare parola acces tabela PROCENTE</strong></div>
        <div class="panel-body">
            <label>Parola curenta:</label>
            <asp:TextBox ID="txtParolaAcces" runat="server" TextMode="Password" CssClass="form-control" />
            <br />
            <asp:Button ID="btnVerificaParola" runat="server" Text="Deblocheaza editare procente" CssClass="btn btn-warning" OnClick="btnVerificaParola_Click" CausesValidation="false" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlEditare" runat="server" Visible="false" style="max-width:550px;">
        <table class="table table-bordered">
            <tr>
                <td><strong>CAS_% (Pensie - ex: 0.25):</strong></td>
                <td>
                    <asp:TextBox ID="txtCas" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvCas" runat="server" ControlToValidate="txtCas" ErrorMessage="Obligatoriu!" ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator ID="rvCas" runat="server" ControlToValidate="txtCas" Type="Double" MinimumValue="0" MaximumValue="1" ErrorMessage="Valoare intre 0 si 1!" ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td><strong>CASS_% (Sanatate - ex: 0.10):</strong></td>
                <td>
                    <asp:TextBox ID="txtCass" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvCass" runat="server" ControlToValidate="txtCass" ErrorMessage="Obligatoriu!" ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator ID="rvCass" runat="server" ControlToValidate="txtCass" Type="Double" MinimumValue="0" MaximumValue="1" ErrorMessage="Valoare intre 0 si 1!" ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td><strong>IMPOZIT_% (Impozit - ex: 0.10):</strong></td>
                <td>
                    <asp:TextBox ID="txtImpozit" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvImp" runat="server" ControlToValidate="txtImpozit" ErrorMessage="Obligatoriu!" ForeColor="Red" Display="Dynamic" />
                    <asp:RangeValidator ID="rvImp" runat="server" ControlToValidate="txtImpozit" Type="Double" MinimumValue="0" MaximumValue="1" ErrorMessage="Valoare intre 0 si 1!" ForeColor="Red" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td><strong>Parola noua (optional - lasati gol daca nu o schimbati):</strong></td>
                <td>
                    <asp:TextBox ID="txtParolaNoua" runat="server" TextMode="Password" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-right">
                    <asp:Button ID="btnSalveazaProcente" runat="server" Text="Salveaza procente &amp; Recalculeaza salariile" CssClass="btn btn-success" OnClick="btnSalveazaProcente_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
</asp:Content>
