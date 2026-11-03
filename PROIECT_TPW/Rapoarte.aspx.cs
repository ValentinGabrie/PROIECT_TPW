using System;
using System.Data;
using System.Data.OracleClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;

public partial class Rapoarte : System.Web.UI.Page
{
    OracleConnection conn = new OracleConnection("DATA SOURCE = localhost:1521/xe; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
    OracleDataAdapter da;
    DataSet ds;
    string str;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GenereazaRaport(0);
        }
        else if (Session["ReportDoc"] != null)
        {
            CrystalReportViewer1.ReportSource = (ReportDocument)Session["ReportDoc"];
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int idMinim = 0;
        int.TryParse(TextBox1.Text.Trim(), out idMinim);
        GenereazaRaport(idMinim);
    }

    private void GenereazaRaport(int idMin)
    {
        try
        {
            conn.Open();
            str = "SELECT * FROM angajati WHERE id > " + idMin + " ORDER BY id";
            da = new OracleDataAdapter(str, conn);
            ds = new DataSet();
            da.Fill(ds, "angajati");
            conn.Close();

            // 1. Bind GridView via C# code (NOT Design Wizard!)
            GridView1.DataSource = ds.Tables["angajati"].DefaultView;
            GridView1.DataBind();

            // 2. Load Crystal Report and push the filtered ADO.NET DataTable into it
            ReportDocument rpt = new ReportDocument();
            string reportPath = Server.MapPath("~/CrystalReport1.rpt");
            if (System.IO.File.Exists(reportPath))
            {
                rpt.Load(reportPath);
                rpt.SetDataSource(ds.Tables["angajati"]);
                CrystalReportViewer1.ReportSource = rpt;
                Session["ReportDoc"] = rpt;
                lblReportStatus.Text = "Raport Crystal Reports generat pentru ID > " + idMin;
            }
            else
            {
                lblReportStatus.Text = "Tabelul a fost filtrat (ID > " + idMin + "). Adaugati fisierul 'CrystalReport1.rpt' in radacina proiectului pentru afisarea raportului.";
            }
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            lblReportStatus.Text = "Eroare la generare raport: " + ex.Message;
        }
    }
}