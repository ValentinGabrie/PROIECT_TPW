using System;
using System.Data;
using System.Web.UI;

public partial class ServiciuWebPage : System.Web.UI.Page
{
    Serviciul_meu ws = new Serviciul_meu();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        lblDataSelectata.Text = "Data selectata pentru pontaj/raportare: " + Calendar1.SelectedDate.ToShortDateString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label2.Text = ws.Salut(TextBox1.Text.Trim());
    }

    protected void btnCautaWebService_Click(object sender, EventArgs e)
    {
        DataTable dt = ws.view_data();
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
}