using System;
using System.Web.UI;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCompare_Click(object sender, EventArgs e)
    {
        string serverVal = txtServerInput.Text;
        string htmlVal = Request.Form["txtHtmlInput"];

        lblComparisonResult.Text = string.Format(
            "La PostBack -> Control ASP.NET: '{0}' (pastrat in ViewState) | Control HTML: '{1}' (pierdut din input dupa reincarcare).",
            serverVal, htmlVal);
    }
}