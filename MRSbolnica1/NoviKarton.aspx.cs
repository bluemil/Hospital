using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class NoviKarton : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerisiSifru();
            lblbrzdrknj.Text = Session["brknjizice"].ToString();//u labeli se ispisuje vrednost definisane sesije s prethodne strane
        }
    }
    public void GenerisiSifru()
    {
        string naredba = "select max(br_kartona) from Zdravstveni_karton";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlDataReader citac;


        konekcija.Open();
        citac = komanda.ExecuteReader();
        citac.Read();
        string max_sif = citac[""].ToString();
        int nova_sif = (int.Parse(max_sif)) + 1;
        lblsifkart.Text = nova_sif.ToString();
        citac.Close();
        konekcija.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)//evidentiranje u bazu
    {
        string naredba = "Insert into Zdravstveni_karton (br_kartona, dat_otvaranja_kartona, br_zdr_knjizice) values (@br_kartona, @dat_otvaranja_kartona, @br_zdr_knjizice)";
        SqlCommand ubaci = new SqlCommand(naredba, konekcija);
        ubaci.Parameters.AddWithValue("@br_kartona", lblsifkart.Text);
        ubaci.Parameters.AddWithValue("@dat_otvaranja_kartona", Calendar1.SelectedDate.Date);
        ubaci.Parameters.AddWithValue("@br_zdr_knjizice", lblbrzdrknj.Text);
        int dodato = 0;
        konekcija.Open();
        dodato = ubaci.ExecuteNonQuery();
        konekcija.Close();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PregledKartona.aspx");
    }
}
