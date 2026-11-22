using System;
using System.Data;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

public partial class AdaugareAngajati : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReseteazaFormular();
        }
    }

    private void ReseteazaFormular()
    {
        txtNume.Text = "";
        txtPrenume.Text = "";
        txtFunctie.Text = "";
        txtSalarBaza.Text = "";
        txtSpor.Text = "0";
        txtPremii.Text = "0";
        txtRetineri.Text = "0";
        txtNume.Focus();
    }

    protected void btnAdauga_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        int nrCrtNou = 0;
        byte[] pozaBytes = fuPoza.HasFile ? fuPoza.FileBytes : null;

        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            conn.Open();
            OracleTransaction tx = conn.BeginTransaction();
            try
            {
                string sql = @"INSERT INTO ANGAJATI (NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR_PROC, PREMII_BRUTE, RETINERI, POZA)
                               VALUES (:pNume, :pPrenume, :pFunctie, :pSalar, :pSpor, :pPremii, :pRetineri, :pPoza)
                               RETURNING NR_CRT INTO :pOutNrCrt";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Transaction = tx;
                    cmd.Parameters.Add("pNume", OracleDbType.Varchar2).Value = txtNume.Text.Trim();
                    cmd.Parameters.Add("pPrenume", OracleDbType.Varchar2).Value = txtPrenume.Text.Trim();
                    cmd.Parameters.Add("pFunctie", OracleDbType.Varchar2).Value = txtFunctie.Text.Trim();
                    cmd.Parameters.Add("pSalar", OracleDbType.Int32).Value = int.Parse(txtSalarBaza.Text.Trim());
                    cmd.Parameters.Add("pSpor", OracleDbType.Int32).Value = int.Parse(txtSpor.Text.Trim());
                    cmd.Parameters.Add("pPremii", OracleDbType.Int32).Value = int.Parse(txtPremii.Text.Trim());
                    cmd.Parameters.Add("pRetineri", OracleDbType.Int32).Value = int.Parse(txtRetineri.Text.Trim());

                    OracleParameter pPoza = cmd.Parameters.Add("pPoza", OracleDbType.Blob);
                    pPoza.Value = (object)pozaBytes ?? DBNull.Value;

                    OracleParameter pOut = new OracleParameter("pOutNrCrt", OracleDbType.Int32);
                    pOut.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pOut);

                    cmd.ExecuteNonQuery();
                    nrCrtNou = Convert.ToInt32(pOut.Value.ToString());
                }

                tx.Commit();
                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Angajatul #" + nrCrtNou + " a fost adaugat si salariul a fost calculat! Puteti adauga un nou angajat.";

                AfiseazaUltimulAngajat(conn, nrCrtNou, pozaBytes);
                ReseteazaFormular();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Eroare la adaugare (ROLLBACK): " + ex.Message;
            }
        }
    }

    private void AfiseazaUltimulAngajat(OracleConnection conn, int nrCrt, byte[] pozaBytes)
    {
        string sql = @"SELECT NR_CRT, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR_PROC, PREMII_BRUTE,
                              TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD
                       FROM ANGAJATI WHERE NR_CRT = :pNrCrt";
        using (OracleCommand cmd = new OracleCommand(sql, conn))
        {
            cmd.Parameters.Add("pNrCrt", OracleDbType.Int32).Value = nrCrt;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvUltimul.DataSource = dt;
            gvUltimul.DataBind();
            pnlRezultat.Visible = dt.Rows.Count > 0;
        }

        if (pozaBytes != null && pozaBytes.Length > 0)
        {
            imgPozaNoua.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(pozaBytes);
            imgPozaNoua.Visible = true;
        }
        else
        {
            imgPozaNoua.Visible = false;
        }
    }
}
