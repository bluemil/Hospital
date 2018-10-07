using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections;
using System.Data;

public partial class Pacijent : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Popuni();
            
        }
        
        txtBrZdrKnj.ReadOnly = true;
        txtIdLekara.ReadOnly = true;

    }
    public void Popuni()//deklarisanje metode kojom popunjavamo ddl imenima pacijenata
    {
        DropDownList1.Items.Clear();
        SqlCommand komanda = new SqlCommand();
        komanda.Connection = konekcija;
        komanda.CommandText = "Select br_zdr_knjizice, ime_pacijenta, prezime_pacijenta from Pacijent";
        SqlDataReader citac;
        konekcija.Open();
        citac = komanda.ExecuteReader();
        while (citac.Read())
        {
            ListItem novi = new ListItem();
            novi.Text = citac["ime_pacijenta"] + " " + citac["prezime_pacijenta"];
            novi.Value = citac["br_zdr_knjizice"].ToString();
            DropDownList1.Items.Add(novi);
        }
        citac.Close();
        konekcija.Close();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)//ispis u txtboxove podatke o selektovanom iz ddl
    {
        RequiredFieldValidator1.Enabled = true;
        RequiredFieldValidator2.Enabled = true;
        RequiredFieldValidator3.Enabled = true;
        RequiredFieldValidator4.Enabled = true;
        RequiredFieldValidator5.Enabled = true;
        RegularExpressionValidator1.Enabled = true;
    
        string naredba = "Select * from Pacijent where br_zdr_knjizice='" + DropDownList1.SelectedItem .Value  + "'";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
                  
        SqlDataReader citac;
        konekcija.Open();
        citac = komanda.ExecuteReader();
        
        citac.Read();
             
        txtBrZdrKnj.Text  = citac["br_zdr_knjizice"].ToString();
        txtAdresa.Text = citac["adresa_pacijenta"].ToString();
        txtIdLekara.Text = citac["id_lekara"].ToString();
        txtIme.Text = citac["ime_pacijenta"].ToString();
        txtPrezime.Text = citac["prezime_pacijenta"].ToString();
        txtTelefon.Text = citac["tel_pacijenta"].ToString();
        citac.Close();

        konekcija.Close();
    }
    protected void btnNovi_Click(object sender, EventArgs e)
    {
        txtBrZdrKnj.ReadOnly = false;
        txtIdLekara.Visible = false;//ne treba nam, imamo listu
        btnDodaj.Visible = true;
        txtTelefon.Text = "";
        txtPrezime.Text = "";
        txtIme.Text = "";
        txtBrZdrKnj.Text = "";
        txtAdresa.Text = "";
        Label6.Visible = false;//id lekara
       
        
        DropDownList1.Visible = false;
        RequiredFieldValidator1.Enabled = true;
        RequiredFieldValidator2.Enabled = true;
        RequiredFieldValidator3.Enabled = true;
        RequiredFieldValidator4.Enabled = true;
        RequiredFieldValidator5.Enabled = true;
        RegularExpressionValidator1.Enabled = true;
      
       
    }
    public void Popuni2()
    {
        RadioButtonList1.Visible = true;
        string naredba2 = "Select id_lekara, ime_lekara, prezime_lekara from Lekar";
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
        SqlDataReader citac2;
        konekcija.Open();
        citac2 = komanda2.ExecuteReader();
        Dictionary<int, string> lekari = new Dictionary<int, string>(); //deklarisemo novi Dictionary<>, koji ce za kljuc sadrzati integer promenljivu, a za text string promenljivu

        while (citac2.Read())
        {
            lekari.Add(Int32.Parse(citac2["id_lekara"].ToString()), citac2["ime_lekara"].ToString()+" " +citac2 ["prezime_lekara"].ToString ()); //dodajemo stavku na listu
        }

        RadioButtonList1.DataSource = lekari;
        RadioButtonList1.DataTextField =  "Value"; //u listi prikazuje ime i prezime lekara
        RadioButtonList1.DataValueField = "Key";//nosi vrednost kljuca odn. id_lekara
        this.DataBind();
        citac2.Close();
        konekcija.Close();

    }
    protected void btnDodaj_Click(object sender, EventArgs e)
    {
        string naredbaS = "select max(br_kartona) from Zdravstveni_karton";//ovo radim jer hocu da mi se automatski otvori i zk za svakog novododatog pacijenta
        SqlCommand komandaS = new SqlCommand(naredbaS, konekcija);
        SqlDataReader citac;
        konekcija.Open();
        citac = komandaS.ExecuteReader();
        citac.Read();
        int sifra = int.Parse(citac[""].ToString()) + 1;
       
        citac.Close();
        konekcija.Close();
        
            string naredba = "Insert into Pacijent (br_zdr_knjizice, ime_pacijenta, prezime_pacijenta, adresa_pacijenta, tel_pacijenta, id_lekara)values (@br_zdr_knjizice, @ime_pacijenta, @prezime_pacijenta, @adresa_pacijenta, @tel_pacijenta, @id_lekara)";
            SqlCommand komanda = new SqlCommand(naredba, konekcija);
            komanda.Parameters.AddWithValue("@br_zdr_knjizice", txtBrZdrKnj.Text);
            komanda.Parameters.AddWithValue("@ime_pacijenta", txtIme.Text);
            komanda.Parameters.AddWithValue("@prezime_pacijenta", txtPrezime.Text);
            komanda.Parameters.AddWithValue("@adresa_pacijenta", txtAdresa.Text);
            komanda.Parameters.AddWithValue("@tel_pacijenta", txtTelefon.Text);
           
        if(RadioButtonList1 .SelectedValue !="")
            {
                komanda.Parameters.AddWithValue("@id_lekara", RadioButtonList1.SelectedItem.Value);
            }
            else 
            {
                komanda.Parameters.AddWithValue("@id_lekara", DBNull .Value );
            }
            string naredba2 = "Insert into Zdravstveni_karton values (@br_kartona, @dat_otvaranja_kartona, @br_zdr_knjizice)";
            SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
            komanda2.Parameters.AddWithValue("@br_kartona", sifra);
            komanda2.Parameters.AddWithValue("@dat_otvaranja_kartona", DateTime.Now);
            komanda2.Parameters.AddWithValue("br_zdr_knjizice", txtBrZdrKnj.Text);
            
            int dodato = 0;
           
                konekcija.Open();
                SqlTransaction tr = konekcija.BeginTransaction();
                komanda.Transaction = tr;
                komanda2.Transaction = tr;

                dodato = komanda.ExecuteNonQuery();
                komanda2.ExecuteNonQuery();
                tr.Commit();
                Label7.Text = dodato.ToString() + " dodat.";
           
            
            
            konekcija.Close();
            
            if (dodato > 0)
            {
                Popuni();
            }

        }
   
    
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            Popuni2();
        }
        else if (CheckBox1.Checked == false)
        {
            RadioButtonList1.Visible = false;

        }
    }
    protected void btnIzmeni_Click(object sender, EventArgs e)
    {
        string izmeni = "Update Pacijent set  ime_pacijenta=@ime_pacijenta, prezime_pacijenta=@prezime_pacijenta, adresa_pacijenta=@adresa_pacijenta, tel_pacijenta=@tel_pacijenta, id_lekara=@id_lekara where br_zdr_knjizice=@br_zdr_knjizice";
        SqlCommand komanda = new SqlCommand(izmeni, konekcija);
        komanda.Parameters.AddWithValue("@br_zdr_knjizice", txtBrZdrKnj.Text);
        komanda.Parameters.AddWithValue("@ime_pacijenta", txtIme.Text);
        komanda.Parameters.AddWithValue("@prezime_pacijenta", txtPrezime.Text);
        komanda.Parameters.AddWithValue("@adresa_pacijenta", txtAdresa.Text);
        komanda.Parameters.AddWithValue("@tel_pacijenta", txtTelefon.Text);

        if (RadioButtonList1.SelectedValue != "")//ako hoce da promeni ili doda lekara
        {
            komanda.Parameters.AddWithValue("@id_lekara", RadioButtonList1.SelectedItem.Value);
        }
        else if (txtIdLekara.Text != "")//ovo je slucaj kada pacijent ima lekara i nece da ga menja
        {
            komanda.Parameters.AddWithValue("@id_lekara", txtIdLekara.Text);
        }
        else //slucaj kada pacijent nema lekara i nece da ga doda
        {
            komanda.Parameters.AddWithValue("@id_lekara", DBNull.Value);
        }
        int izmenjeno = 0;
        konekcija.Open();
        izmenjeno = komanda.ExecuteNonQuery();
        Label7.Text = izmenjeno.ToString() + " izmenjen.";
        konekcija.Close();
        if (izmenjeno > 0)
        {

            Popuni();
        }
       
    }
    protected void btnObrisi_Click(object sender, EventArgs e)//bice transakcija nad 4 tabele
    {//obrisi iz stavke koja se odnosi na zk (a povezana je sa njim preko br kartona) u kojem je br_zdr_knjiz jednak br_knjizice onog pacijenta kojeg hocemo da obrisemo(txtbox)
        string obrisi1 = "Delete from Stavka_kartona  where EXISTS(SELECT br_zdr_knjizice from Zdravstveni_karton where Stavka_kartona.br_kartona=Zdravstveni_karton.br_kartona and br_zdr_knjizice=@br_zdr_knjizice)";
        SqlCommand komanda1 = new SqlCommand(obrisi1, konekcija);
        komanda1.Parameters.AddWithValue("@br_zdr_knjizice", txtBrZdrKnj.Text);

        string obrisi2 = "Delete from Zdravstveni_karton  where br_zdr_knjizice=@br_zdr_knjizice";
        SqlCommand komanda2 = new SqlCommand(obrisi2, konekcija);
        komanda2.Parameters.AddWithValue("@br_zdr_knjizice", txtBrZdrKnj.Text);

        string obrisi3 = "Delete from Pregled where br_zdr_knjizice=@br_zdr_knjizice";
        SqlCommand komanda3 = new SqlCommand(obrisi3, konekcija);
        komanda3.Parameters.AddWithValue("@br_zdr_knjizice", txtBrZdrKnj.Text);

        string obrisi4 = "Delete from Pacijent where br_zdr_knjizice=@br_zdr_knjizice";
        SqlCommand komanda4 = new SqlCommand(obrisi4, konekcija);
        komanda4.Parameters.AddWithValue("@br_zdr_knjizice", txtBrZdrKnj.Text);
       
        int obrisano = 0;
        konekcija.Open();
        SqlTransaction tr = konekcija.BeginTransaction();
        komanda1.Transaction = tr;
        komanda2.Transaction = tr;
        komanda3.Transaction = tr;
        komanda4.Transaction = tr;

        komanda1.ExecuteNonQuery();
        komanda2.ExecuteNonQuery();
        komanda3.ExecuteNonQuery();
        obrisano=komanda4.ExecuteNonQuery();

        tr.Commit();
        Label7.Text = obrisano.ToString() + " obrisan.";
        konekcija.Close();
        if (obrisano > 0)
        {
            txtAdresa.Text = "";
            txtBrZdrKnj.Text = "";
            txtIdLekara.Text = "";
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtTelefon.Text = "";
            Popuni();
        }
       
    }

    protected void btnNoviKarton_Click(object sender, EventArgs e)
    {
        Session["brknjizice"] = txtBrZdrKnj.Text;//u sesiju stavljamo vrednost iz txtboxa
        Response.Redirect("NoviKarton.aspx");
    }
}

        

    
   