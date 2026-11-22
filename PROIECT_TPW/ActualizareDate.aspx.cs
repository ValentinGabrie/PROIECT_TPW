using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

public partial class ActualizareDate : Page
{
    protected bool EsteModMultiplu
    {
        get { return ViewState["EsteModMultiplu"] != null && (bool)ViewState["EsteModMultiplu"]; }
        set { ViewState["EsteModMultiplu"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IncarcaAngajati();
        }
    }

    private void IncarcaAngajati()
    {
        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            string sql = @"SELECT NR_CRT, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR_PROC,
                                  PREMII_BRUTE, TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT,
                                  CAS, CASS, RETINERI, VIRAT_CARD
                           FROM ANGAJATI";

            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                if (!string.IsNullOrWhiteSpace(txtCauta.Text))
                {
                    sql += " WHERE UPPER(NUME) LIKE :pCauta OR UPPER(PRENUME) LIKE :pCauta";
                    cmd.Parameters.Add("pCauta", OracleDbType.Varchar2).Value = "%" + txtCauta.Text.Trim().ToUpper() + "%";
                }
                sql += " ORDER BY NR_CRT";
                cmd.CommandText = sql;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvAngajati.DataSource = dt;
                gvAngajati.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMesaj.ForeColor = System.Drawing.Color.DarkRed;
                    lblMesaj.Text = "Nu a fost gasit niciun angajat conform criteriului introdus.";
                }
            }
        }
    }

    protected void btnCauta_Click(object sender, EventArgs e)
    {
        gvAngajati.PageIndex = 0;
        lblMesaj.Text = "";
        IncarcaAngajati();
    }

    protected void btnToti_Click(object sender, EventArgs e)
    {
        txtCauta.Text = "";
        gvAngajati.PageIndex = 0;
        lblMesaj.Text = "";
        IncarcaAngajati();
    }

    protected void gvAngajati_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAngajati.PageIndex = e.NewPageIndex;
        IncarcaAngajati();
    }

    protected void gvAngajati_RowEditing(object sender, GridViewEditEventArgs e)
    {
        EsteModMultiplu = false;
        btnSalveazaMultiplu.Visible = false;
        gvAngajati.EditIndex = e.NewEditIndex;
        IncarcaAngajati();
    }

    protected void gvAngajati_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAngajati.EditIndex = -1;
        IncarcaAngajati();
    }

    protected void gvAngajati_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (!Page.IsValid) return;

        int nrCrt = Convert.ToInt32(gvAngajati.DataKeys[e.RowIndex].Value);
        GridViewRow row = gvAngajati.Rows[e.RowIndex];

        string nume = ((TextBox)row.FindControl("txtNume")).Text.Trim();
        string prenume = ((TextBox)row.FindControl("txtPrenume")).Text.Trim();
        string functie = ((TextBox)row.FindControl("txtFunctie")).Text.Trim();
        int salar = int.Parse(((TextBox)row.FindControl("txtSalar")).Text.Trim());
        int spor = int.Parse(((TextBox)row.FindControl("txtSpor")).Text.Trim());
        int premii = int.Parse(((TextBox)row.FindControl("txtPremii")).Text.Trim());
        int retineri = int.Parse(((TextBox)row.FindControl("txtRetineri")).Text.Trim());

        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            conn.Open();
            OracleTransaction tx = conn.BeginTransaction();
            try
            {
                string sql = @"UPDATE ANGAJATI
                               SET NUME = :pNume, PRENUME = :pPrenume, FUNCTIE = :pFunctie,
                                   SALAR_BAZA = :pSalar, SPOR_PROC = :pSpor,
                                   PREMII_BRUTE = :pPremii, RETINERI = :pRetineri
                               WHERE NR_CRT = :pNrCrt";
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    cmd.Transaction = tx;
                    cmd.Parameters.Add("pNume", OracleDbType.Varchar2).Value = nume;
                    cmd.Parameters.Add("pPrenume", OracleDbType.Varchar2).Value = prenume;
                    cmd.Parameters.Add("pFunctie", OracleDbType.Varchar2).Value = functie;
                    cmd.Parameters.Add("pSalar", OracleDbType.Int32).Value = salar;
                    cmd.Parameters.Add("pSpor", OracleDbType.Int32).Value = spor;
                    cmd.Parameters.Add("pPremii", OracleDbType.Int32).Value = premii;
                    cmd.Parameters.Add("pRetineri", OracleDbType.Int32).Value = retineri;
                    cmd.Parameters.Add("pNrCrt", OracleDbType.Int32).Value = nrCrt;
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Datele angajatului #" + nrCrt + " au fost actualizate si salariul a fost recalculat automat!";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Eroare actualizare: " + ex.Message;
            }
        }

        gvAngajati.EditIndex = -1;
        IncarcaAngajati();
    }

    protected void btnModMultiplu_Click(object sender, EventArgs e)
    {
        gvAngajati.EditIndex = -1;
        EsteModMultiplu = !EsteModMultiplu;
        btnSalveazaMultiplu.Visible = EsteModMultiplu;
        btnModMultiplu.Text = EsteModMultiplu ? "Iesire mod editare multipla" : "Mod editare multipla (Premii / Retineri)";
        IncarcaAngajati();
    }

    protected void btnSalveazaMultiplu_Click(object sender, EventArgs e)
    {
        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            conn.Open();
            OracleTransaction tx = conn.BeginTransaction();
            try
            {
                foreach (GridViewRow row in gvAngajati.Rows)
                {
                    int nrCrt = Convert.ToInt32(gvAngajati.DataKeys[row.RowIndex].Value);
                    int premii = int.Parse(((TextBox)row.FindControl("txtPremiiBatch")).Text.Trim());
                    int retineri = int.Parse(((TextBox)row.FindControl("txtRetineriBatch")).Text.Trim());

                    if (premii < 0 || retineri < 0)
                        throw new ArgumentException("Valorile pentru premii si retineri trebuie sa fie >= 0.");

                    string sql = "UPDATE ANGAJATI SET PREMII_BRUTE = :pPremii, RETINERI = :pRetineri WHERE NR_CRT = :pNrCrt";
                    using (OracleCommand cmd = new OracleCommand(sql, conn))
                    {
                        cmd.Transaction = tx;
                        cmd.Parameters.Add("pPremii", OracleDbType.Int32).Value = premii;
                        cmd.Parameters.Add("pRetineri", OracleDbType.Int32).Value = retineri;
                        cmd.Parameters.Add("pNrCrt", OracleDbType.Int32).Value = nrCrt;
                        cmd.ExecuteNonQuery();
                    }
                }

                tx.Commit();
                EsteModMultiplu = false;
                btnSalveazaMultiplu.Visible = false;
                btnModMultiplu.Text = "Mod editare multipla (Premii / Retineri)";
                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Toate modificarile multiple au fost salvate atomic (tranzactional) si salariile recalculate!";
            }
            catch (Exception ex)
            {
                tx.Rollback();
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Tranzactie anulata (ROLLBACK): " + ex.Message;
            }
        }

        IncarcaAngajati();
    }
}
