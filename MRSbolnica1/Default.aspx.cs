using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Web.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //List<string> ZvanjeLekara = new List<string>();
            ArrayList ZvanjeLekara = new ArrayList();//kreiramo listu da bismo popunili CheckBoxList
            ZvanjeLekara.Add("Lekar opste prakse");
            ZvanjeLekara.Add("Lekar specijalista");
            CheckBoxList2.DataSource = ZvanjeLekara;
            this.DataBind();
        }
    }
    protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox1.Items.Clear();
        if (CheckBoxList2.Items[0].Selected == true)//ako je selektovan 1. CheckBox
        {
            string naredba = "SELECT id_lekara, ime_lekara, prezime_lekara from Lekar where zvanje_lekara='opsta praksa' ";
            SqlCommand komanda = new SqlCommand(naredba, konekcija);
            SqlDataReader citac;
            konekcija.Open();
            citac = komanda.ExecuteReader();
            while (citac.Read())
            {
                ListItem novi = new ListItem();
                novi.Text = citac["ime_lekara"] + " " + citac["prezime_lekara"];
                novi.Value = citac["id_lekara"].ToString();
                ListBox1.Items.Add(novi);//punimo ListBox1 podacima definisanim u naredbi

            }
            citac.Close();
            konekcija.Close();
        }
        if (CheckBoxList2.Items[1].Selected == true)//ako je cekiran drugi CheckBox
        {
            string naredba2 = "SELECT id_lekara, ime_lekara, prezime_lekara from Lekar where zvanje_lekara='specijalista'";
            SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
            SqlDataReader citac;
            konekcija.Open();
            citac = komanda2.ExecuteReader();
            while (citac.Read())
            {
                ListItem novi = new ListItem();
                novi.Text = citac["ime_lekara"] + " " + citac["prezime_lekara"];
                novi.Value = citac["id_lekara"].ToString();
                ListBox1.Items.Add(novi);
            }
            citac.Close();
            konekcija.Close();

        }
    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox2.Items.Clear();
        string sqlnaredba = "SELECT id_lekara, ime_lekara, prezime_lekara FROM Lekar where id_lekara='" + ListBox1.SelectedItem.Value + "'";
        SqlCommand komanda = new SqlCommand(sqlnaredba, konekcija);

        SqlDataAdapter adapter = new SqlDataAdapter(komanda);
        System.Data.DataSet dsLekar = new System.Data.DataSet();
       
        konekcija.Open();
        adapter.Fill(dsLekar, "Lekar");
        
        komanda.CommandText = "SELECT id_lekara, br_zdr_knjizice, ime_pacijenta, prezime_pacijenta FROM Pacijent";
        adapter.Fill(dsLekar, "Pacijent");
        
        System.Data.DataRelation Lekar_Pacijent = new System.Data.DataRelation("Lekar_Pacijent", dsLekar.Tables["Lekar"].Columns["id_lekara"], dsLekar.Tables["Pacijent"].Columns["id_lekara"], false);
        
        dsLekar.Relations.Add(Lekar_Pacijent);
                
        foreach (DataRow rs in dsLekar.Tables["Lekar"].Rows)
        {
            
            foreach (DataRow rsu in rs.GetChildRows("Lekar_Pacijent"))
            {
                ListItem Pacijenti = new ListItem();
                Pacijenti.Text = rsu["ime_pacijenta"].ToString() + " " + rsu["prezime_pacijenta"].ToString();
                Pacijenti.Value = rsu["br_zdr_knjizice"].ToString();
                ListBox2.Items.Add(Pacijenti);//popunice se pacijentima samo selektovanog lekara kao sto je i receno u naredbi
            }
        }

        konekcija.Close();
        
           
    }
   
protected void  ListBox2_SelectedIndexChanged(object sender, EventArgs e)
{
    string naredba = "Select * from Pacijent where br_zdr_knjizice='" + ListBox2.SelectedItem.Value + "'";

    SqlCommand komanda = new SqlCommand(naredba, konekcija);
    SqlDataAdapter adapter = new SqlDataAdapter(komanda);
    DataTable dtpacijent = new DataTable();
    konekcija.Open();
    adapter.Fill(dtpacijent);
    konekcija.Close();
    DetailsView1.DataSource = dtpacijent;
    this.DataBind();
    Label2.Visible = true;
    DetailsView1.Visible = true;//postaje vidljiv tek nakon klika na pacijenta
}
protected void LinkButton1_Click(object sender, EventArgs e)
{
    Response.Redirect("PregledKartona.aspx");
}
protected void LinkButton2_Click(object sender, EventArgs e)
{
    Response.Redirect("SpisakLekara.aspx");
}
}

    
  




           
