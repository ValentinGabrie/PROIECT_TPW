using System;
using System.Data;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

public partial class StergereAngajati : Page
{
    private int SelectedNrCrt
    {
        get { return ViewState["SelectedNrCrt"] != null ? (int)ViewState["SelectedNrCrt"] : 0; }
        set { ViewState["SelectedNrCrt"] = value; }
    }

    protected void btnCauta_Click(object sender, EventArgs e)
    {
        pnlConfirmare.Visible = false;
        SelectedNrCrt = 0;
        gvRezultate.SelectedIndex = -1;
        CautaAngajati();
    }

    private void CautaAngajati()
    {
        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            string sql = "SELECT NR_CRT, NUME, PRENUME, FUNCTIE, SALAR_BAZA, VIRAT_CARD FROM ANGAJATI WHERE 1=1";
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.Connection = conn;
                if (!string.IsNullOrWhiteSpace(txtNume.Text))
                {
                    sql += " AND UPPER(NUME) LIKE :pNume";
                    cmd.Parameters.Add("pNume", OracleDbType.Varchar2).Value = "%" + txtNume.Text.Trim().ToUpper() + "%";
                }
                if (!string.IsNullOrWhiteSpace(txtPrenume.Text))
                {
                    sql += " AND UPPER(PRENUME) LIKE :pPrenume";
                    cmd.Parameters.Add("pPrenume", OracleDbType.Varchar2).Value = "%" + txtPrenume.Text.Trim().ToUpper() + "%";
                }
                sql += " ORDER BY NR_CRT";
                cmd.CommandText = sql;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvRezultate.DataSource = dt;
                gvRezultate.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMesaj.ForeColor = System.Drawing.Color.DarkRed;
                    lblMesaj.Text = "Nu a fost gasit niciun angajat cu numele/prenumele specificat!";
                }
                else if (dt.Rows.Count == 1)
                {
                    lblMesaj.ForeColor = System.Drawing.Color.Blue;
                    lblMesaj.Text = "A fost gasit 1 angajat. Selectati-l pentru confirmarea stergerii.";
                }
                else
                {
                    lblMesaj.ForeColor = System.Drawing.Color.Blue;
                    lblMesaj.Text = "Au fost gasiti " + dt.Rows.Count + " angajati. Va rugam sa selectati din tabel angajatul pe care doriti sa il stergeti.";
                }
            }
        }
    }

    protected void gvRezultate_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedNrCrt = Convert.ToInt32(gvRezultate.SelectedDataKey.Value);
        string nume = gvRezultate.SelectedRow.Cells[2].Text;
        string prenume = gvRezultate.SelectedRow.Cells[3].Text;

        lblConfirmare.Text = "Sunteti pe cale sa stergeti angajatul #" + SelectedNrCrt + " (" + nume + " " + prenume + ").";
        pnlConfirmare.Visible = true;
    }

    protected void btnConfirmaStergere_Click(object sender, EventArgs e)
    {
        if (SelectedNrCrt <= 0) return;

        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            conn.Open();
            OracleTransaction tx = conn.BeginTransaction();
            try
            {
                using (OracleCommand cmd = new OracleCommand("DELETE FROM ANGAJATI WHERE NR_CRT = :pNrCrt", conn))
                {
                    cmd.Transaction = tx;
                    cmd.Parameters.Add("pNrCrt", OracleDbType.Int32).Value = SelectedNrCrt;
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new Exception("Angajatul selectat nu mai exista.");
                }

                tx.Commit();
                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Angajatul #" + SelectedNrCrt + " a fost sters cu succes!";
                pnlConfirmare.Visible = false;
                SelectedNrCrt = 0;
                gvRezultate.SelectedIndex = -1;
                CautaAngajati();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Eroare la stergere (ROLLBACK): " + ex.Message;
            }
        }
    }

    protected void btnAnuleaza_Click(object sender, EventArgs e)
    {
        pnlConfirmare.Visible = false;
        gvRezultate.SelectedIndex = -1;
        SelectedNrCrt = 0;
    }
}
