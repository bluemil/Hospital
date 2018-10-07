using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Collections;

public partial class Lekar : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerisiSifru();//poziv metode
            List<string> SSLekara = new List<string>();
            SSLekara.Add("vss");
            SSLekara.Add("doc");
            DropDownList1.DataSource = SSLekara;
            this.DataBind();
            
        }
        
    }
    public void GenerisiSifru()//selektuje najvecu sifru iz tabele lekar i uvecava je za 1
    {
        string naredba = "Select max(id_lekara)as max from Lekar";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlDataReader citac;
        konekcija.Open();
        citac = komanda.ExecuteReader();
        citac.Read();
        int sifra = int.Parse(citac["max"].ToString()) + 1;
        lblSifraLekara.Text = sifra.ToString();//vrednost nove sifre ispisujemo u labelu
        citac.Close();
        konekcija.Close();
    }
   
    
    protected void btnNovi_Click(object sender, EventArgs e)
    {
        GenerisiSifru();//generise se sifra novog lekara
        
        txtDiplome.Text = "";
        txtGodZavrsSpec.Text = "";
        txtIme.Text = "";
        txtMestoSpec.Text = "";
        txtPrezime.Text = "";
       
        txtZavrsSpec.Text  = "";
        btnDodaj.Visible = true;//zato sto je bio false nakon prethodnog dodavanja
        btnNovi.Visible = false;
       
       
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)//Lekar opste prakse
    {
        RadioButton2.Checked = false;//onemogucavamo cekiranje drugog radiobuttona
        Panel2.Visible = true;//panel lekara opste prakse
        txtZvanje.Text = "opsta praksa";
        txtZvanje.ReadOnly = true;
        Panel3.Visible = false;//jer nam ne trebaju podaci o specijalisti
        btnDodaj.Visible = true;
        Label4.Visible = true ;//zato sto se ne vidi kada kliknemo na radiobutton2, pa da ponovo bude vidljiv
        DropDownList1.Visible = true ;

    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)//Specijalista
    {
        RadioButton1.Checked = false;
        Panel2.Visible = true;
        txtSS.ReadOnly = true;
        txtSS.Text = "spc";
        txtZvanje.Text = "specijalista";
        txtZvanje.ReadOnly = true;
        Panel3.Visible = true;
        btnDodaj.Visible = true;
        Label4.Visible = false;//strucna sprema
        DropDownList1.Visible = false;
    }
    protected void btnDodaj_Click(object sender, EventArgs e)//transakcija nad tabelama Lekar i L.o.p./Specijalista
    {
        string naredba = "Insert into Lekar values (@id_lekara, @ime_lekara, @prezime_lekara, @str_sprema_lekara, @zvanje_lekara, @stecene_dipl)";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        komanda.Parameters.AddWithValue("@id_lekara", int.Parse (lblSifraLekara .Text ));//definisemo vrednosti parametara koji ce biti prosledjeni komandi
        komanda.Parameters.AddWithValue("@ime_lekara", txtIme.Text);
        komanda.Parameters.AddWithValue("@prezime_lekara", txtPrezime.Text);
        if (RadioButton1.Checked == true)//l.o.p.
        {
            komanda.Parameters.AddWithValue("@str_sprema_lekara", DropDownList1 .SelectedItem .Value );
        }
        else if (RadioButton2.Checked == true)//specijalista
        {
            komanda.Parameters.AddWithValue("@str_sprema_lekara", txtSS .Text );
        }
        komanda.Parameters.AddWithValue("@zvanje_lekara", txtZvanje.Text);
        komanda.Parameters.AddWithValue("@stecene_dipl", txtDiplome .Text );

        if (RadioButton1.Checked == true)
        {
            string naredba2 = "Insert into Lekar_opste_prakse values(@id_lekara)";
            SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
            komanda2.Parameters.AddWithValue("@id_lekara", lblSifraLekara .Text );

            int dodato = 0;
            konekcija.Open();
            SqlTransaction tr = konekcija.BeginTransaction();
            komanda.Transaction = tr;//objekat za transakciju se dodeljuje i jednom i drugom objektu klase SqlCommand
            komanda2.Transaction = tr;
            dodato = komanda.ExecuteNonQuery();
            komanda2.ExecuteNonQuery();
            tr.Commit();//izvrsavanje transakcije nad obe tabele pozivom metode Commit();
            Label10.Text = dodato.ToString() + " dodato.";
            konekcija.Close();
           
        }
        else if (RadioButton2.Checked == true)
        {
            string naredba2 = "Insert into Lekar_specijalista values (@id_lekara, @zavrsena_spec, @mesto_spec, @dat_zavr_spec)";
            SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
            komanda2.Parameters.AddWithValue("@id_lekara", lblSifraLekara .Text );
            komanda2.Parameters.AddWithValue("@zavrsena_spec", txtZavrsSpec.Text);
            komanda2.Parameters.AddWithValue("@mesto_spec", txtMestoSpec.Text);
            komanda2.Parameters.AddWithValue("@dat_zavr_spec", txtGodZavrsSpec.Text);
            int dodato = 0;
            konekcija.Open();
            SqlTransaction tr = konekcija.BeginTransaction();
            komanda.Transaction = tr;
            komanda2.Transaction = tr;
            dodato = komanda.ExecuteNonQuery();
            komanda2.ExecuteNonQuery();
            tr.Commit();
            Label10.Text = dodato.ToString() + " dodato.";
            konekcija.Close();
          
        }
        btnNovi.Visible = true ;
        btnDodaj.Visible = false;
        
    }


   
    
  
}
