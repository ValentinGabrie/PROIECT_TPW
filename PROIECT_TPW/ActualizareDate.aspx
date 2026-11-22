<%@ Page Title="Actualizare Date Angajati" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ActualizareDate.aspx.cs" Inherits="ActualizareDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Actualizare date angajati</h3>
    <div class="help-box">
        <strong>Indicatii de operare:</strong> Puteti cauta un angajat dupa Nume sau Prenume (optimizat pentru peste 1000 angajati) sau puteti parcurge toate inregistrarile folosind paginarea. Apasati <em>Edit</em> pentru a modifica campurile editabile sau utilizati modul <em>Acordare rapida premii/retineri</em> pentru a actualiza mai multi angajati succesiv intr-o singura tranzactie. Coloanele pe fundal galben sunt calculate automat si nu pot fi editate direct.
    </div>

    <div class="form-inline" style="margin-bottom:15px;">
        <label>Cautare Nume/Prenume:</label>
        <asp:TextBox ID="txtCauta" runat="server" CssClass="form-control" placeholder="Introduceti nume sau prenume..."></asp:TextBox>
        <asp:Button ID="btnCauta" runat="server" Text="Cauta" CssClass="btn btn-primary" OnClick="btnCauta_Click" CausesValidation="false" />
        <asp:Button ID="btnToti" runat="server" Text="Afiseaza toti" CssClass="btn btn-default" OnClick="btnToti_Click" CausesValidation="false" />
        &nbsp;|&nbsp;
        <asp:Button ID="btnModMultiplu" runat="server" Text="Mod editare multipla (Premii / Retineri)" CssClass="btn btn-warning" OnClick="btnModMultiplu_Click" CausesValidation="false" />
        <asp:Button ID="btnSalveazaMultiplu" runat="server" Text="Salveaza toate modificarile (Tranzactie)" CssClass="btn btn-success" Visible="false" OnClick="btnSalveazaMultiplu_Click" CausesValidation="false" />
    </div>

    <asp:Label ID="lblMesaj" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <div style="overflow-x:auto;">
        <asp:GridView ID="gvAngajati" runat="server" AutoGenerateColumns="False" DataKeyNames="NR_CRT"
            CssClass="table table-bordered table-condensed table-hover"
            AllowPaging="True" PageSize="15"
            OnPageIndexChanging="gvAngajati_PageIndexChanging"
            OnRowEditing="gvAngajati_RowEditing"
            OnRowCancelingEdit="gvAngajati_RowCancelingEdit"
            OnRowUpdating="gvAngajati_RowUpdating">
            <Columns>
                <asp:CommandField ShowEditButton="True" EditText="Edit" UpdateText="Salveaza" CancelText="Anuleaza" />
                <asp:BoundField DataField="NR_CRT" HeaderText="NR.CRT" ReadOnly="True" />

                <asp:TemplateField HeaderText="NUME">
                    <ItemTemplate><%# Eval("NUME") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtNume" runat="server" Text='<%# Bind("NUME") %>' Width="100px" />
                        <asp:RequiredFieldValidator ID="rfvNume" runat="server" ControlToValidate="txtNume" ErrorMessage="*" ForeColor="Red" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revNume" runat="server" ControlToValidate="txtNume" ValidationExpression="^[a-zA-Z\-\s]{1,50}$" ErrorMessage="Litere!" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PRENUME">
                    <ItemTemplate><%# Eval("PRENUME") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPrenume" runat="server" Text='<%# Bind("PRENUME") %>' Width="100px" />
                        <asp:RequiredFieldValidator ID="rfvPrenume" runat="server" ControlToValidate="txtPrenume" ErrorMessage="*" ForeColor="Red" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revPrenume" runat="server" ControlToValidate="txtPrenume" ValidationExpression="^[a-zA-Z\-\s]{1,50}$" ErrorMessage="Litere!" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="FUNCTIE">
                    <ItemTemplate><%# Eval("FUNCTIE") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFunctie" runat="server" Text='<%# Bind("FUNCTIE") %>' Width="100px" />
                        <asp:RequiredFieldValidator ID="rfvFunctie" runat="server" ControlToValidate="txtFunctie" ErrorMessage="*" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SALAR_BAZA">
                    <ItemTemplate><%# Eval("SALAR_BAZA") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSalar" runat="server" Text='<%# Bind("SALAR_BAZA") %>' Width="75px" />
                        <asp:RequiredFieldValidator ID="rfvSalar" runat="server" ControlToValidate="txtSalar" ErrorMessage="*" ForeColor="Red" Display="Dynamic" />
                        <asp:RangeValidator ID="rvSalar" runat="server" ControlToValidate="txtSalar" Type="Integer" MinimumValue="0" MaximumValue="1000000" ErrorMessage=">=0!" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="SPOR_%">
                    <ItemTemplate><%# Eval("SPOR_PROC") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSpor" runat="server" Text='<%# Bind("SPOR_PROC") %>' Width="55px" />
                        <asp:RangeValidator ID="rvSpor" runat="server" ControlToValidate="txtSpor" Type="Integer" MinimumValue="0" MaximumValue="100" ErrorMessage="0-100!" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PREMII_BRUTE">
                    <ItemTemplate>
                        <asp:Label ID="lblPremii" runat="server" Text='<%# Eval("PREMII_BRUTE") %>' Visible='<%# !EsteModMultiplu %>' />
                        <asp:TextBox ID="txtPremiiBatch" runat="server" Text='<%# Eval("PREMII_BRUTE") %>' Width="70px" Visible='<%# EsteModMultiplu %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPremii" runat="server" Text='<%# Bind("PREMII_BRUTE") %>' Width="70px" />
                        <asp:RangeValidator ID="rvPremii" runat="server" ControlToValidate="txtPremii" Type="Integer" MinimumValue="0" MaximumValue="1000000" ErrorMessage=">=0!" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="TOTAL_BRUT" HeaderText="TOTAL_BRUT" ReadOnly="True" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="BRUT_IMPOZABIL" HeaderText="BRUT_IMPOZABIL" ReadOnly="True" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="IMPOZIT" HeaderText="IMPOZIT" ReadOnly="True" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="CAS" HeaderText="CAS" ReadOnly="True" ItemStyle-CssClass="calc-col" />
                <asp:BoundField DataField="CASS" HeaderText="CASS" ReadOnly="True" ItemStyle-CssClass="calc-col" />

                <asp:TemplateField HeaderText="RETINERI">
                    <ItemTemplate>
                        <asp:Label ID="lblRetineri" runat="server" Text='<%# Eval("RETINERI") %>' Visible='<%# !EsteModMultiplu %>' />
                        <asp:TextBox ID="txtRetineriBatch" runat="server" Text='<%# Eval("RETINERI") %>' Width="70px" Visible='<%# EsteModMultiplu %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRetineri" runat="server" Text='<%# Bind("RETINERI") %>' Width="70px" />
                        <asp:RangeValidator ID="rvRetineri" runat="server" ControlToValidate="txtRetineri" Type="Integer" MinimumValue="0" MaximumValue="1000000" ErrorMessage=">=0!" ForeColor="Red" Display="Dynamic" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="VIRAT_CARD" HeaderText="VIRAT_CARD" ReadOnly="True" ItemStyle-CssClass="calc-col" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
