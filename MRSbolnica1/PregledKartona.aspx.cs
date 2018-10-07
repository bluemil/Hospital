using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class PregledKartona : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Popuni();//popunjavanje obe liste
            
        }
    }
    public void Popuni()
    {
        string naredba = "Select br_kartona from Zdravstveni_karton";//popunjavanje ddl siframa kartona
       
        SqlCommand komanda=new SqlCommand (naredba , konekcija );
       
        SqlDataReader citac;
        
        konekcija .Open ();
        citac =komanda .ExecuteReader();
       
        while (citac.Read())
        {
            DropDownList1 .Items .Add (citac ["br_kartona"].ToString ());
        }
        citac .Close ();
        konekcija.Close();


        string naredba2 = "Select br_zdr_knjizice, ime_pacijenta, prezime_pacijenta from Pacijent";//popunjavanje ddlPacijenti
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
        SqlDataReader citac2;
        konekcija.Open();
        citac2 = komanda2.ExecuteReader();
        while (citac2.Read())
        {
            ListItem lista = new ListItem();
            lista.Text = citac2["ime_pacijenta"] + " " + citac2["prezime_pacijenta"];
            lista.Value = citac2["br_zdr_knjizice"].ToString();
            ddlPacijent.Items.Add(lista);
        }
        citac2.Close();
        konekcija .Close ();
    }
   
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        string naredba1 = "Select * from Zdravstveni_karton where br_kartona=@br_kartona";
        SqlCommand komanda1 = new SqlCommand(naredba1, konekcija);
        komanda1.Parameters.AddWithValue("@br_kartona", DropDownList1.SelectedItem.Value);
        SqlDataAdapter adapter1 = new SqlDataAdapter(komanda1);
        DataSet ds = new DataSet();

        string naredba2 = "select * from Stavka_kartona where br_kartona=@br_kartona";
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
        komanda2.Parameters.AddWithValue("@br_kartona", DropDownList1.SelectedItem.Value);
        SqlDataAdapter adapter2 = new SqlDataAdapter(komanda2);
        konekcija.Open();

        adapter1.Fill(ds, "ZK");
        adapter2.Fill(ds, "Stavka");
        konekcija.Close();

        DetailsView1.DataSource = ds.Tables ["ZK"];
        DetailsView1.DataBind();
        GridView1.DataSource = ds.Tables["Stavka"];
        GridView1.DataBind();


    }

    protected void ddlPacijent_SelectedIndexChanged(object sender, EventArgs e)
    {//br knjizice vuce iz ddl
        string naredba = "select * from Zdravstveni_karton where br_zdr_knjizice=@br_zdr_knjizice";
        string naredba2 = "select  rbr_stavke, rbr_pregleda, sif_bolesti, sif_terapije from Stavka_kartona join Zdravstveni_karton on Stavka_kartona.br_kartona=Zdravstveni_karton.br_kartona where br_zdr_knjizice=@br_zdr_knjizice";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
        komanda.Parameters.AddWithValue("@br_zdr_knjizice", ddlPacijent .SelectedItem .Value );
        komanda2.Parameters.AddWithValue("@br_zdr_knjizice", ddlPacijent.SelectedItem.Value);
        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        SqlDataAdapter adapter2 = new SqlDataAdapter(komanda2);
        DataTable dtZKarton = new DataTable();
        DataTable dtStavkaKartona = new DataTable();
        adapter.Fill(dtZKarton);
        adapter2.Fill(dtStavkaKartona);
        DetailsView1.DataSource = dtZKarton;
        DetailsView1.DataBind();
        GridView1.DataSource = dtStavkaKartona;
        GridView1.DataBind();
    }
}
