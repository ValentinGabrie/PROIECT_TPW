<%@ Page Title="Acces Date prin Serviciu Web" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ServiciuWebPage.aspx.cs" Inherits="ServiciuWebPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main-container">
        <div class="panel-custom">
            <h2>Acces Baza de Date Oracle folosind un Serviciu Web (<code>Serviciul_meu.asmx</code>)</h2>

            <asp:Label ID="lblDataSelectata" runat="server" Text="Selectati o data din calendar:"></asp:Label>
            <br /><br />
            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
            <br />

            <asp:TextBox ID="TextBox1" runat="server" placeholder="Introduceti numele..."></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br /><br />

            <asp:GridView ID="GridView1" runat="server" CssClass="grid-view-custom"></asp:GridView>
            <br />
            <asp:Button ID="btnCautaWebService" runat="server" Text="Cauta (Apel view_data din Serviciu Web)" CssClass="btn btn-info" OnClick="btnCautaWebService_Click" />
        </div>
    </div>
</asp:Content>