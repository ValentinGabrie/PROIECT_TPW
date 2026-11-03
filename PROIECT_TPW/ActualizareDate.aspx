<%@ Page Title="Actualizare si Cautare Angajati" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ActualizareDate.aspx.cs" Inherits="ActualizareDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <!-- AJAX UpdatePanel for Asynchronous Partial PostBacks -->
        <asp:UpdatePanel ID="UpdatePanelCrud" runat="server">
            <ContentTemplate>
                <h2>Gestiune Date Salariale (ADO.NET + AJAX)</h2>

                <!-- Yellow/Green Search Bar matching Lab Guide -->
                <div class="search-highlight">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 120px;"><strong>ID:</strong></td>
                            <td><asp:TextBox ID="txtSearchId" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><strong>NUME:</strong></td>
                            <td><asp:TextBox ID="txtSearchNume" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="padding-top: 8px;">
                                <asp:Button ID="btnRefresh" runat="server" Text="REFRESH" OnClick="btnRefresh_Click" />
                            </td>
                            <td style="padding-top: 8px;">
                                <asp:Button ID="btnCauta" runat="server" Text="Cauta" OnClick="btnCauta_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

                <!-- Unconfigured GridView bound strictly via C# Code-Behind -->
                <asp:GridView ID="GridView1" runat="server" CssClass="grid-view-custom"></asp:GridView>

                <div style="margin-top: 15px; margin-bottom: 15px;">
                    <strong>Mesaje:</strong><br />
                    <asp:Label ID="lblMesaje" runat="server" CssClass="status-msg" Text="Gata pentru operatii."></asp:Label>
                </div>

                <hr />

                <div class="row">
                    <!-- Add / Update Form -->
                    <div class="col-md-6">
                        <div class="panel-custom">
                            <h4>Adaugare / Modificare Angajat</h4>
                            <table class="table">
                                <tr>
                                    <td>ID:</td>
                                    <td><asp:TextBox ID="txtId" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>NUME:</td>
                                    <td><asp:TextBox ID="txtNume" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>PRENUME:</td>
                                    <td><asp:TextBox ID="txtPrenume" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>FUNCTIE:</td>
                                    <td><asp:TextBox ID="txtFunctie" runat="server" CssClass="form-control" Text="Inginer"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>SALAR BAZA:</td>
                                    <td><asp:TextBox ID="txtSalar" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>SPOR (%):</td>
                                    <td><asp:TextBox ID="txtSpor" runat="server" CssClass="form-control" Text="0"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>PREMII BRUTE:</td>
                                    <td><asp:TextBox ID="txtPremii" runat="server" CssClass="form-control" Text="0"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>RETINERI:</td>
                                    <td><asp:TextBox ID="txtRetineri" runat="server" CssClass="form-control" Text="0"></asp:TextBox></td>
                                </tr>
                            </table>
                            <asp:Button ID="btnAdaugare" runat="server" Text="Adaugare" CssClass="btn btn-success" OnClick="btnAdaugare_Click" />
                            &nbsp;
                            <asp:Button ID="btnModificare" runat="server" Text="Modificare (dupa ID)" CssClass="btn btn-warning" OnClick="btnModificare_Click" />
                        </div>
                    </div>

                    <!-- DropDownList Selection & Delete -->
                    <div class="col-md-6">
                        <div class="panel-custom">
                            <h4>Selectie Rapida / Stergere Angajat (DropDownList)</h4>
                            <p>Selectati ID-ul angajatului din lista legata la baza de date:</p>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"
                                CssClass="form-control" Width="200px"
                                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:Button ID="btnStergere" runat="server" Text="Sterge Angajatul Selectat"
                                CssClass="btn btn-danger" OnClick="btnStergere_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>