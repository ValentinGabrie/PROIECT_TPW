using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Oracle.ManagedDataAccess.Client;

public partial class StatPlata : Page
{
    private ReportDocument _raport;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Nu reincarca raportul la fiecare tick de 2 secunde al ceasului AJAX din Site.master
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null && sm.IsInAsyncPostBack)
        {
            return;
        }

        IncarcaRaport();
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        if (_raport != null)
        {
            _raport.Close();
            _raport.Dispose();
        }
    }

    private DataSet ObtineDateStatPlata()
    {
        DataSet ds = new DataSet();
        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            string sql = @"SELECT NR_CRT, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR_PROC, PREMII_BRUTE,
                                  TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD
                           FROM ANGAJATI
                           ORDER BY NR_CRT";
            OracleDataAdapter da = new OracleDataAdapter(sql, conn);
            da.Fill(ds, "ANGAJATI");
        }
        return ds;
    }

    private void IncarcaRaport()
    {
        try
        {
            string reportPath = Server.MapPath("~/Rapoarte/StatPlata.rpt");
            if (!File.Exists(reportPath))
            {
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Fisierul raport nu a fost gasit la calea: " + reportPath;
                return;
            }

            DataSet ds = ObtineDateStatPlata();
            if (ds.Tables["ANGAJATI"].Rows.Count == 0)
            {
                lblMesaj.ForeColor = System.Drawing.Color.DarkOrange;
                lblMesaj.Text = "Tabela ANGAJATI este goala. Rulati SQL/script_salarizare.sql sau adaugati angajati.";
            }

            _raport = new ReportDocument();
            _raport.Load(reportPath);
            _raport.SetDataSource(ds.Tables["ANGAJATI"]);

            // Protejat in try/catch pentru imprimante implicite blocate pe A4
            try
            {
                _raport.PrintOptions.PaperSize = PaperSize.PaperA3;
                _raport.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            }
            catch
            {
                // Foloseste formatul A3 disociat salvat direct in StatPlata.rpt
            }

            CrystalReportViewer1.ReportSource = _raport;
        }
        catch (Exception ex)
        {
            _raport = null;
            lblMesaj.ForeColor = System.Drawing.Color.Red;
            lblMesaj.Text = "Eroare incarcare raport: " + ex.Message;
        }
    }

    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        if (_raport == null)
        {
            lblMesaj.ForeColor = System.Drawing.Color.Red;
            if (string.IsNullOrEmpty(lblMesaj.Text))
            {
                lblMesaj.Text = "Raportul nu este incarcat; exportul PDF nu poate rula.";
            }
            return;
        }

        try
        {
            string infoClean = Regex.Replace(txtInfoPdf.Text.Trim(), @"[^a-zA-Z0-9_\-]", "_");
            string fileName = string.Format("StatPlata_{0}_{1}.pdf", ddlLuna.SelectedValue, infoClean);
            string exportDir = Server.MapPath("~/Exports/");
            if (!Directory.Exists(exportDir))
            {
                Directory.CreateDirectory(exportDir);
            }

            string fullPath = Path.Combine(exportDir, fileName);
            DiskFileDestinationOptions fisier = new DiskFileDestinationOptions { DiskFileName = fullPath };
            _raport.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            _raport.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            _raport.ExportOptions.DestinationOptions = fisier;
            _raport.Export();

            lblMesaj.ForeColor = System.Drawing.Color.Green;
            lblMesaj.Text = "PDF generat cu succes: <a href='Exports/" + fileName + "' target='_blank'>" + fileName + "</a><br/>Cale pe disc: " + fullPath;
        }
        catch (Exception ex)
        {
            lblMesaj.ForeColor = System.Drawing.Color.Red;
            lblMesaj.Text = "Eroare export PDF: " + ex.Message;
        }
    }
}