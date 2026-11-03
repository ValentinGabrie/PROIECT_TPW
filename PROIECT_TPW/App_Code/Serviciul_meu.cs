using System;
using System.Data;
using System.Data.OracleClient;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Serviciul_meu : System.Web.Services.WebService
{
    public Serviciul_meu()
    {
    }

    [WebMethod]
    public string Salut(string nume)
    {
        return "Bine ati venit in sistemul de salarizare, " + nume + "!";
    }

    [WebMethod]
    public DataTable view_data()
    {
        OracleConnection conn = new OracleConnection("DATA SOURCE = localhost:1521/xe; PASSWORD = student; PERSIST SECURITY INFO = True; USER ID = STUDENT");
        OracleDataAdapter dr = new OracleDataAdapter("SELECT * FROM angajati ORDER BY id", conn);
        DataSet ds = new DataSet();
        ds.Tables.Add("angajati");
        dr.Fill(ds, "angajati");
        DataTable tabela = ds.Tables[0];
        return tabela;
    }
}