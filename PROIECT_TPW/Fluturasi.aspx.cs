using System;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using Oracle.ManagedDataAccess.Client;

public partial class Fluturasi : Page
{
    private ReportDocument _raport;

    protected void Page_Load(object sender, EventArgs e)
    {
        IncarcaFluturasi(txtCautaFluturas.Text.Trim());
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        if (_raport != null)
        {
            _raport.Close();
            _raport.Dispose();
        }
    }

    private void IncarcaFluturasi(string filtru)
    {
        try
        {
            DataSet ds = new DataSet();
            using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
            {
                string sql = @"SELECT NR_CRT, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR_PROC, PREMII_BRUTE,
                                      TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD
                               FROM ANGAJATI";
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    if (!string.IsNullOrWhiteSpace(filtru))
                    {
                        sql += " WHERE UPPER(NUME) LIKE :pF OR UPPER(PRENUME) LIKE :pF";
                        cmd.Parameters.Add("pF", OracleDbType.Varchar2).Value = "%" + filtru.ToUpper() + "%";
                    }
                    sql += " ORDER BY NR_CRT";
                    cmd.CommandText = sql;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds, "ANGAJATI");
                }
            }

            if (ds.Tables["ANGAJATI"].Rows.Count == 0)
            {
                lblMesaj.ForeColor = System.Drawing.Color.DarkRed;
                lblMesaj.Text = "Nu a fost gasit niciun angajat pentru criteriul introdus.";
                CrystalReportViewer1.ReportSource = null;
                return;
            }

            lblMesaj.Text = "";
            _raport = new ReportDocument();
            _raport.Load(Server.MapPath("~/Rapoarte/Fluturasi.rpt"));
            _raport.SetDataSource(ds.Tables["ANGAJATI"]);
            CrystalReportViewer1.ReportSource = _raport;
        }
        catch (Exception ex)
        {
            lblMesaj.ForeColor = System.Drawing.Color.Red;
            lblMesaj.Text = "Eroare incarcare raport Fluturasi.rpt: " + ex.Message;
        }
    }

    protected void btnFiltreaza_Click(object sender, EventArgs e)
    {
        IncarcaFluturasi(txtCautaFluturas.Text.Trim());
    }

    protected void btnToti_Click(object sender, EventArgs e)
    {
        txtCautaFluturas.Text = "";
        IncarcaFluturasi("");
    }
}
