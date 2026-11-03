using System;
using System.Data;
using System.Data.OracleClient;
using System.Web.UI;

public partial class CalculSalarii : System.Web.UI.Page
{
    OracleConnection conn = new OracleConnection("DATA SOURCE = localhost:1521/xe; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
    OracleCommand cmd;
    OracleDataAdapter da;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AfiseazaTabel();
        }
    }

    private void AfiseazaTabel()
    {
        try
        {
            conn.Open();
            da = new OracleDataAdapter("SELECT id, nume, prenume, salar_baza, total_brut, cas, cass, impozit, retineri, virat_card FROM angajati ORDER BY id", conn);
            ds = new DataSet();
            da.Fill(ds, "angajati");
            GridViewProc.DataSource = ds.Tables["angajati"].DefaultView;
            GridViewProc.DataBind();
        }
        catch (Exception ex)
        {
            lblProcStatus.Text = "Eroare: " + ex.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    protected void btnExecutaProc_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();
            cmd = new OracleCommand("procedura1", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_nume", OracleType.VarChar).Value = txtProcNume.Text.Trim();
            cmd.Parameters.Add("p_salar", OracleType.Number).Value = Convert.ToDecimal(txtProcSalar.Text.Trim());

            cmd.ExecuteNonQuery();
            conn.Close();

            lblProcStatus.Text = "Procedura stocata 'procedura1' a fost executata cu succes pentru angajatul: " + txtProcNume.Text;
            AfiseazaTabel();
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            lblProcStatus.Text = "Eroare la apelul procedurii stocate: " + ex.Message;
        }
    }
}