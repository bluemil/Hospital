using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class SpisakLekara : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string naredba = "Select * from Lekar";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        DataTable lekar = new DataTable();
        konekcija.Open();
        adapter.Fill(lekar);
        konekcija.Close();
        GridView1.DataSource = lekar;
        this.DataBind();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Lekar.aspx");
    }
}
