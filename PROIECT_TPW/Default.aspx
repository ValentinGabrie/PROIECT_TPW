<%@ Page Title="Home - Ghid Utilizare" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>ZONA AFISARE - Ghid global de operare a aplicatiei de salarizare</h3>
    <div class="help-box">
        <p><strong>Bine ati venit!</strong> Aceasta aplicatie gestioneaza calculul salariilor angajatilor si generarea rapoartelor tiparibile (Stat de Plata A3 si Fluturasi). Utilizati meniul superior sau bara de scurtaturi rapide pentru a naviga.</p>
    </div>
    <h4>1. Optiunile disponibile din meniu</h4>
    <ul>
        <li><strong>Home:</strong> Afiseaza acest ghid global de utilizare.</li>
        <li><strong>Introducere date &rarr; Actualizare date:</strong> Cautare, editare individuala si actualizare in masa a campurilor editabile. Coloanele calculate sunt read-only si se recalculeaza automat.</li>
        <li><strong>Introducere date &rarr; Adaugare:</strong> Adauga angajati noi cu validari, incarcare optionala poza si calcul automat.</li>
        <li><strong>Introducere date &rarr; Stergere:</strong> Cautare, selectare si stergere confirmata a unui angajat.</li>
        <li><strong>Introducere date &rarr; Calcul salarii:</strong> Afisare read-only si recalculare prin procedura Oracle.</li>
        <li><strong>Rapoarte:</strong> Genereaza Statul de Plata A3 landscape si fluturasi pe doua coloane.</li>
        <li><strong>Procente:</strong> Modificare protejata prin parola a cotelor CAS, CASS si IMPOZIT.</li>
    </ul>
    <h4>2. Formule de calcul utilizate (rotunjire la intreg)</h4>
    <ul>
        <li><code>TOTAL_BRUT = SALAR_BAZA * (1 + SPOR_% / 100) + PREMII_BRUTE</code></li>
        <li><code>CAS = TOTAL_BRUT * CAS_%</code></li>
        <li><code>CASS = TOTAL_BRUT * CASS_%</code></li>
        <li><code>BRUT_IMPOZABIL = TOTAL_BRUT - CAS - CASS</code></li>
        <li><code>IMPOZIT = BRUT_IMPOZABIL * IMPOZIT_%</code></li>
        <li><code>VIRAT_CARD = TOTAL_BRUT - IMPOZIT - CAS - CASS - RETINERI</code></li>
    </ul>
</asp:Content>