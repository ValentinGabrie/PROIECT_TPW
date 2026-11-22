using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;

public partial class ModifProcente : Page
{
    protected void btnVerificaParola_Click(object sender, EventArgs e)
    {
        string hashIntrodus = SecurityHelper.ComputeSha256Hash(txtParolaAcces.Text);

        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            conn.Open();
            string sql = "SELECT CAS_PROC, CASS_PROC, IMPOZIT_PROC, PAROLA_HASH FROM PROCENTE WHERE ID = 1";
            using (OracleCommand cmd = new OracleCommand(sql, conn))
            using (OracleDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    string hashDb = dr["PAROLA_HASH"].ToString();
                    if (string.Equals(hashIntrodus, hashDb, StringComparison.OrdinalIgnoreCase))
                    {
                        txtCas.Text = Convert.ToDecimal(dr["CAS_PROC"]).ToString("0.00", CultureInfo.InvariantCulture);
                        txtCass.Text = Convert.ToDecimal(dr["CASS_PROC"]).ToString("0.00", CultureInfo.InvariantCulture);
                        txtImpozit.Text = Convert.ToDecimal(dr["IMPOZIT_PROC"]).ToString("0.00", CultureInfo.InvariantCulture);

                        pnlAutentificare.Visible = false;
                        pnlEditare.Visible = true;
                        lblMesaj.ForeColor = System.Drawing.Color.Green;
                        lblMesaj.Text = "Acces permis. Puteti modifica procentele sau parola.";
                    }
                    else
                    {
                        lblMesaj.ForeColor = System.Drawing.Color.Red;
                        lblMesaj.Text = "Parola incorecta! Acces respins.";
                    }
                }
            }
        }
    }

    protected void btnSalveazaProcente_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        decimal cas = decimal.Parse(txtCas.Text.Trim(), CultureInfo.InvariantCulture);
        decimal cass = decimal.Parse(txtCass.Text.Trim(), CultureInfo.InvariantCulture);
        decimal impozit = decimal.Parse(txtImpozit.Text.Trim(), CultureInfo.InvariantCulture);
        string parolaNouaHash = string.IsNullOrWhiteSpace(txtParolaNoua.Text)
            ? null
            : SecurityHelper.ComputeSha256Hash(txtParolaNoua.Text);

        using (OracleConnection conn = new OracleConnection(SecurityHelper.ConnectionString))
        {
            try
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("SP_MODIFICA_PROCENTE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cas", OracleDbType.Decimal).Value = cas;
                    cmd.Parameters.Add("p_cass", OracleDbType.Decimal).Value = cass;
                    cmd.Parameters.Add("p_impozit", OracleDbType.Decimal).Value = impozit;
                    cmd.Parameters.Add("p_parola", OracleDbType.Varchar2).Value = (object)parolaNouaHash ?? DBNull.Value;
                    cmd.ExecuteNonQuery();
                }

                lblMesaj.ForeColor = System.Drawing.Color.Green;
                lblMesaj.Text = "Procentele au fost actualizate si salariile tuturor angajatilor au fost recalculate tranzactional!";
                txtParolaNoua.Text = "";
            }
            catch (Exception ex)
            {
                lblMesaj.ForeColor = System.Drawing.Color.Red;
                lblMesaj.Text = "Eroare la actualizarea procentelor: " + ex.Message;
            }
        }
    }
}
