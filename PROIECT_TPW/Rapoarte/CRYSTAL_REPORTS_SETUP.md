# Configurare Fisiere Binare Crystal Reports SP33 (.rpt)

Deoarece fisierele `.rpt` sunt binare si se creeaza in Visual Studio 2022 Designer (Crystal Reports SP33 64-bit), urmati pasii de mai jos dupa aplicarea patch-ului:

1. **Imprimanta A3:** Setati in Windows ca imprimanta implicita o imprimanta care suporta formatul **A3** (ex: *Microsoft Print to PDF* configurata cu A3 sau driver PDF A3) inainte de a deschide Designer-ul.
2. **Sursa de date:** In ambele rapoarte, alegeti `Database Expert -> Project Data -> ADO.NET DataSets -> DataSetSalarii -> ANGAJATI`.
3. **`Rapoarte/StatPlata.rpt` (Cerinta 4.1):**
   - Click dreapta in Designer -> `Design -> Page Setup` -> selectati **A3** si **Landscape** (orizontal).
   - **Report Header:** Titlu `STAT DE PLATA` + Special Field `Print Date`.
   - **Details:** Plasati pe un singur rand orizontal toate cele 14 coloane (`NR_CRT`, `NUME`, `PRENUME`, `FUNCTIE`, `SALAR_BAZA`, `SPOR_PROC`, `PREMII_BRUTE`, `TOTAL_BRUT`, `BRUT_IMPOZABIL`, `IMPOZIT`, `CAS`, `CASS`, `RETINERI`, `VIRAT_CARD`).
   - **Report Footer:** Inserati `Insert -> Summary -> Sum` pentru `TOTAL_BRUT` si `VIRAT_CARD` aliniate sub coloanele respective, cu eticheta `Total:`.
4. **`Rapoarte/Fluturasi.rpt` (Cerinta 4.2):**
   - **Details:** Plasati datele pe **doua coloane** pentru fiecare angajat:
     - Coloana stanga: `DATA` (Print Date), `NR_CRT`, `NUME`, `PRENUME`, `FUNCTIE`, `SALAR_BAZA`, `SPOR_PROC`, `PREMII_BRUTE`, `TOTAL_BRUT`, `BRUT_IMPOZABIL`.
     - Coloana dreapta: `IMPOZIT`, `CAS`, `CASS`, `RETINERI`, `VIRAT_CARD` (cu Bold).
   - Adaugati o linie de separare (`Insert -> Line`) sub banda `Details` pentru a delimita fluturasii fiecarui angajat.
5. **Imagini Header AJAX:** Creati folderul `Images/` in radacina proiectului si copiati 3 imagini JPEG numite `1.jpg`, `2.jpg`, `3.jpg`.
