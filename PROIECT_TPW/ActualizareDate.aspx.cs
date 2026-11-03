using System;
using System.Data;
using System.Data.OracleClient;
using System.Web.UI;

public partial class ActualizareDate : System.Web.UI.Page
{
    // Declarations and Connection Instantiation as specified in the Lab Guide
    OracleConnection conn = new OracleConnection("DATA SOURCE = localhost:1521/xe; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
    OracleCommand cmd;
    OracleDataAdapter da;
    DataSet ds;
    string str;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            IncarcaGrid();
            IncarcaDropDown();
        }
    }

    private void IncarcaGrid(string sqlQuery = "SELECT * FROM angajati ORDER BY id")
    {
        try
        {
            conn.Open();
            da = new OracleDataAdapter(sqlQuery, conn);
            ds = new DataSet();
            da.Fill(ds, "angajati");
            GridView1.DataSource = ds.Tables["angajati"].DefaultView;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMesaje.Text = "Eroare la incarcare date: " + ex.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    private void IncarcaDropDown()
    {
        try
        {
            conn.Open();
            da = new OracleDataAdapter("SELECT id, id || ' - ' || nume || ' ' || prenume AS afisare FROM angajati ORDER BY id", conn);
            ds = new DataSet();
            da.Fill(ds, "angajati");
            DropDownList1.DataSource = ds.Tables["angajati"];
            DropDownList1.DataTextField = "afisare";
            DropDownList1.DataValueField = "id";
            DropDownList1.DataBind();
        }
        catch (Exception ex)
        {
            lblMesaje.Text = "Eroare DropDownList: " + ex.Message;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearchId.Text = "";
        txtSearchNume.Text = "";
        IncarcaGrid();
        IncarcaDropDown();
        lblMesaje.Text = "Tabelul a fost reimprospatat.";
    }

    protected void btnCauta_Click(object sender, EventArgs e)
    {
        string conditie = " WHERE 1=1 ";
        if (!string.IsNullOrWhiteSpace(txtSearchId.Text))
        {
            conditie += " AND id = " + int.Parse(txtSearchId.Text.Trim());
        }
        if (!string.IsNullOrWhiteSpace(txtSearchNume.Text))
        {
            conditie += " AND UPPER(nume) LIKE UPPER('%" + txtSearchNume.Text.Trim() + "%')";
        }

        str = "SELECT * FROM angajati" + conditie + " ORDER BY id";
        IncarcaGrid(str);
        lblMesaje.Text = "Cautare finalizata.";
    }

    protected void btnAdaugare_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();
            str = string.Format(
                "INSERT INTO angajati (id, nume, prenume, functie, salar_baza, spor_procent, premii_brute, retineri) " +
                "VALUES ({0}, '{1}', '{2}', '{3}', {4}, {5}, {6}, {7})",
                txtId.Text.Trim(),
                txtNume.Text.Trim(),
                txtPrenume.Text.Trim(),
                txtFunctie.Text.Trim(),
                txtSalar.Text.Trim(),
                string.IsNullOrWhiteSpace(txtSpor.Text) ? "0" : txtSpor.Text.Trim(),
                string.IsNullOrWhiteSpace(txtPremii.Text) ? "0" : txtPremii.Text.Trim(),
                string.IsNullOrWhiteSpace(txtRetineri.Text) ? "0" : txtRetineri.Text.Trim()
            );

            cmd = new OracleCommand(str, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            lblMesaje.Text = "Adaugat: " + txtId.Text + " " + txtNume.Text + " " + txtSalar.Text;
            IncarcaGrid();
            IncarcaDropDown();
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            lblMesaje.Text = "Eroare la adaugare: " + ex.Message;
        }
    }

    protected void btnModificare_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();
            str = string.Format(
                "UPDATE angajati SET nume='{1}', prenume='{2}', functie='{3}', salar_baza={4}, spor_procent={5}, premii_brute={6}, retineri={7} WHERE id={0}",
                txtId.Text.Trim(),
                txtNume.Text.Trim(),
                txtPrenume.Text.Trim(),
                txtFunctie.Text.Trim(),
                txtSalar.Text.Trim(),
                string.IsNullOrWhiteSpace(txtSpor.Text) ? "0" : txtSpor.Text.Trim(),
                string.IsNullOrWhiteSpace(txtPremii.Text) ? "0" : txtPremii.Text.Trim(),
                string.IsNullOrWhiteSpace(txtRetineri.Text) ? "0" : txtRetineri.Text.Trim()
            );

            cmd = new OracleCommand(str, conn);
            int rows = cmd.ExecuteNonQuery();
            conn.Close();

            lblMesaje.Text = rows > 0 ? "Datele angajatului cu ID " + txtId.Text + " au fost actualizate!" : "ID inexistent.";
            IncarcaGrid();
            IncarcaDropDown();
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            lblMesaje.Text = "Eroare la modificare: " + ex.Message;
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            conn.Open();
            da = new OracleDataAdapter("SELECT * FROM angajati WHERE id = " + DropDownList1.SelectedValue, conn);
            ds = new DataSet();
            da.Fill(ds, "angajati");
            conn.Close();

            if (ds.Tables["angajati"].Rows.Count > 0)
            {
                DataRow r = ds.Tables["angajati"].Rows[0];
                txtId.Text = r["id"].ToString();
                txtNume.Text = r["nume"].ToString();
                txtPrenume.Text = r["prenume"].ToString();
                txtFunctie.Text = r["functie"].ToString();
                txtSalar.Text = r["salar_baza"].ToString();
                txtSpor.Text = r["spor_procent"].ToString();
                txtPremii.Text = r["premii_brute"].ToString();
                txtRetineri.Text = r["retineri"].ToString();
                lblMesaje.Text = "Selectat: " + r["id"] + " " + r["nume"] + " " + r["salar_baza"];
            }
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            lblMesaje.Text = "Eroare selectie: " + ex.Message;
        }
    }

    protected void btnStergere_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(DropDownList1.SelectedValue)) return;

        try
        {
            conn.Open();
            cmd = new OracleCommand("DELETE FROM angajati WHERE id = " + DropDownList1.SelectedValue, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            lblMesaje.Text = "Angajatul cu ID " + DropDownList1.SelectedValue + " a fost sters.";
            IncarcaGrid();
            IncarcaDropDown();
        }
        catch (Exception ex)
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            lblMesaje.Text = "Eroare la stergere: " + ex.Message;
        }
    }
}