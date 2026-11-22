using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

public partial class CalculSalarii : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IncarcaDate();
        }
    }

    private void IncarcaDate()
    {
        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            string sql = @"SELECT NR_CRT, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR_PROC, PREMII_BRUTE,
                                  TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD
                           FROM ANGAJATI";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                if (!string.IsNullOrWhiteSpace(txtFiltru.Text))
                {
                    sql += " WHERE UPPER(NUME) LIKE :pF OR UPPER(PRENUME) LIKE :pF";
                    cmd.Parameters.Add("pF", OracleDbType.Varchar2).Value = "%" + txtFiltru.Text.Trim().ToUpper() + "%";
                }
                sql += " ORDER BY NR_CRT";
                cmd.CommandText = sql;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvCalcul.DataSource = dt;
                gvCalcul.DataBind();
            }
        }
    }

    protected void btnRecalculeaza_Click(object sender, EventArgs e)
    {
        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            try
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("SP_RECALCULEAZA_SALARII", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Calculul salariilor pentru toti angajatii s-a incheiat cu succes (tranzactie finalizata)!";
                IncarcaDate();
            }
            catch (Exception ex)
            {
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Eroare la recalculare: " + ex.Message;
            }
        }
    }

    protected void btnFiltreaza_Click(object sender, EventArgs e)
    {
        gvCalcul.PageIndex = 0;
        IncarcaDate();
    }

    protected void gvCalcul_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCalcul.PageIndex = e.NewPageIndex;
        IncarcaDate();
    }
}
