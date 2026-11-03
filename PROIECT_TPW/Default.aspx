<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <div class="panel-custom">
            <h2>Sistem Informatic pentru Gestiunea Salariilor Angajatilor</h2>
            <p>Aplicatie Web dezvoltata in Visual Studio 2022 folosind ASP.NET Web Forms, ADO.NET, AJAX, CSS, C#, baza de date Oracle 21c XE si Crystal Reports SP33.</p>
        </div>

        <div class="row">
            <!-- Comparison: ASP.NET Server Control vs HTML Control -->
            <div class="col-md-6">
                <div class="panel-custom">
                    <h4>1. Demonstratie: Control Server ASP.NET vs. Control HTML</h4>
                    <p><strong>Control Server ASP.NET (pastreaza ViewState si executa eveniment C# pe server):</strong></p>
                    <asp:TextBox ID="txtServerInput" runat="server" CssClass="form-control" placeholder="Introduceti text (Server Control)..."></asp:TextBox>
                    <br />
                    <p><strong>Control HTML Standard (fara ViewState automat la PostBack):</strong></p>
                    <input type="text" id="txtHtmlInput" name="txtHtmlInput" class="form-control" placeholder="Introduceti text (HTML standard)..." />
                    <br />
                    <asp:Button ID="btnCompare" runat="server" Text="Trimite pe Server (PostBack)" CssClass="btn btn-primary" OnClick="btnCompare_Click" />
                    <br /><br />
                    <asp:Label ID="lblComparisonResult" runat="server" CssClass="status-msg" Text="Rezultatul comparatiei va aparea aici."></asp:Label>
                </div>
            </div>

            <!-- Read-Only Display using SqlDataSource (Design Mode equivalent) -->
            <div class="col-md-6">
                <div class="panel-custom">
                    <h4>2. Afisare Rapida (SqlDataSource - Oracle Client)</h4>
                    <p>Vizualizare read-only conectata direct prin providerul Oracle:</p>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="DATA SOURCE=localhost:1521/xe;PASSWORD=student;PERSIST SECURITY INFO=True;USER ID=STUDENT"
                        ProviderName="System.Data.OracleClient"
                        SelectCommand="SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, VIRAT_CARD FROM ANGAJATI ORDER BY ID">
                    </asp:SqlDataSource>

                    <asp:GridView ID="GridViewReadOnly" runat="server" DataSourceID="SqlDataSource1"
                        CssClass="grid-view-custom" AutoGenerateColumns="True" EmptyDataText="Nu exista angajati in baza de date.">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>