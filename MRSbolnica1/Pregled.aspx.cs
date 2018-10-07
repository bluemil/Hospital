using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class Pregled : System.Web.UI.Page
{
    SqlConnection konekcija = new SqlConnection(WebConfigurationManager.ConnectionStrings["BolnicaKon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerisiSifru();//misli se na sifru pregleda
            PopuniListu();// ddl Lekar
            PopuniDijagnozu();
            PopuniTerapiju();
        }


    }
    public void GenerisiSifru()
    {
        string naredba = "select max(rbr_pregleda) from Pregled";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlDataReader citac;


        konekcija.Open();
        citac = komanda.ExecuteReader();
        citac.Read();
        string max_sif = citac[""].ToString();
        int nova_sif = int.Parse(max_sif) + 1;
        Label2.Text = nova_sif.ToString();
        citac.Close();
        konekcija.Close();
    }
    public void PopuniListu()
    {
        string naredba = "select * from Lekar";

        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlDataAdapter adapter = new SqlDataAdapter(komanda);

        DataTable dtLekar = new DataTable();

        konekcija.Open();
        adapter.Fill(dtLekar);

        konekcija.Close();

        foreach (DataRow red in dtLekar.Rows)
        {
            ListItem novi = new ListItem();
            novi.Text = red["ime_lekara"].ToString() + " " + red["prezime_lekara"];
            novi.Value = red["id_lekara"].ToString();
            DropDownList1.Items.Add(novi);
        }

            }
    public void PopuniDijagnozu()
    {
        string naredba2 = "Select sif_bolesti, naziv_bolesti from Dijagnoza";
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
        SqlDataReader citac2;
        konekcija.Open();
        citac2 = komanda2.ExecuteReader();
        Dictionary<int, string> dijagnoze = new Dictionary<int, string>(); //deklarisemo novi Dictionary<>, koji ce za kljuc sadrzati integer promenljivu, a za text string promenljivu

        while (citac2.Read())
        {
            dijagnoze.Add(Int32.Parse(citac2["sif_bolesti"].ToString()), citac2["naziv_bolesti"].ToString()); 
        }

        ListBoxDijagnoza.DataSource = dijagnoze;
        ListBoxDijagnoza.DataTextField = "Value"; //u listi prikazuje naziv bolesti
        ListBoxDijagnoza.DataValueField = "Key";
        this.DataBind();
        citac2.Close();
        konekcija.Close();
    }
    public void PopuniTerapiju()
    {
        string naredba2 = "Select sif_terapije, naziv_terapije from Terapija";
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);
        SqlDataReader citac2;
        konekcija.Open();
        citac2 = komanda2.ExecuteReader();
        Dictionary<int, string> terapije = new Dictionary<int, string>(); 

        while (citac2.Read())
        {
            terapije.Add(Int32.Parse(citac2["sif_terapije"].ToString()), citac2["naziv_terapije"].ToString()); 
        }

        ListBoxTerapija.DataSource = terapije;
        ListBoxTerapija.DataTextField = "Value"; 
        ListBoxTerapija.DataValueField = "Key";
        this.DataBind();
        citac2.Close();
        konekcija.Close();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBox1.Items.Clear();
        string naredba = "Select br_zdr_knjizice, ime_pacijenta, prezime_pacijenta from Pacijent where id_lekara='" + DropDownList1.SelectedItem.Value + "'";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        SqlDataAdapter adapter = new SqlDataAdapter(komanda);

        DataTable dtPacijent = new DataTable();
        konekcija.Open();
        adapter.Fill(dtPacijent);
        konekcija.Close();
        foreach (DataRow red in dtPacijent.Rows)
        {
            ListItem novi = new ListItem();
            novi.Text = red["ime_pacijenta"] + " " + red["prezime_pacijenta"];
            novi.Value = red["br_zdr_knjizice"].ToString();
            ListBox1.Items.Add(novi);
        }

    }

    protected void btnEvidentiraj_Click(object sender, EventArgs e)//transakcija- unos u tabele pregled i stavka kartona
    {
        ListBoxDijagnoza.SelectedIndex = -1;//da ne ostane zapamceno ono sto je selektovano
        ListBoxTerapija.SelectedIndex = -1;
        btnSamoStavka.Visible = true;
        string naredbaZK = "Select max(br_kartona) from Zdravstveni_karton where br_zdr_knjizice='" + ListBox1.SelectedItem.Value + "'";
        //maximalan zato sto pacijent moze imati vise kartona, pa da mu se upise u stavku novijeg kartona
        SqlCommand komandaZK = new SqlCommand(naredbaZK, konekcija);
        SqlDataReader citac;
        konekcija.Open();
        citac = komandaZK.ExecuteReader();
        citac.Read();
        int sifrakartona = int.Parse(citac[""].ToString());
        citac.Close();
        konekcija.Close();

        //prebrojava koliko ima redova u stavci, zato sto proveravamo da li je kojim slucajem stavka prazna
        string naredbaSK = "Select count(rbr_stavke)from Stavka_kartona where br_kartona=@br_kartona";
        SqlCommand komandaSK = new SqlCommand(naredbaSK, konekcija);
        komandaSK.Parameters.AddWithValue("@br_kartona", sifrakartona);
        SqlDataReader citac2;
        konekcija.Open();
        citac2 = komandaSK.ExecuteReader();
        citac2.Read();
        int brStavki = int.Parse(citac2 [""].ToString());
        citac2.Close();
        konekcija.Close();

        string maxi="Select max(rbr_stavke)from Stavka_kartona where br_kartona=@br_kartona";//uzima najvecu vrednost iz stavke
        SqlCommand komandamaxi = new SqlCommand(maxi , konekcija);
        komandamaxi.Parameters.AddWithValue("@br_kartona", sifrakartona);
        SqlDataReader citacmaxi;
        konekcija.Open();
        citacmaxi = komandamaxi.ExecuteReader();
        citacmaxi.Read();

        int najvecaVrednost;
        if (brStavki  == 0)//kaze ako u stavki nema redova, stavi da je najveca vrednost 1
        {
            najvecaVrednost = 1;
        }
        else
        {
            najvecaVrednost = int.Parse(citacmaxi[""].ToString())+1;
            
        }

        citacmaxi.Close();
        konekcija.Close();

        string naredba = "Insert into Pregled (rbr_pregleda, dat_i_vreme_pregleda, id_lekara, br_zdr_knjizice) values (@rbr_pregleda, @dat_i_vreme_pregleda, @id_lekara, @br_zdr_knjizice)";
        SqlCommand komanda = new SqlCommand(naredba, konekcija);
        komanda.Parameters.AddWithValue("@rbr_pregleda", Label2.Text);
        komanda.Parameters.AddWithValue("@dat_i_vreme_pregleda", Calendar1.SelectedDate.Date);
        komanda.Parameters.AddWithValue("@id_lekara", DropDownList1.SelectedItem.Value);
        komanda.Parameters.AddWithValue("@br_zdr_knjizice", ListBox1.SelectedItem.Value);


        string naredba2 = "Insert into Stavka_kartona (br_kartona, rbr_stavke, rbr_pregleda, sif_bolesti, sif_terapije) values (@br_kartona, @rbr_stavke, @rbr_pregleda, @sif_bolesti, @sif_terapije)";
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);

        komanda2.Parameters.AddWithValue("@br_kartona", sifrakartona);
        komanda2.Parameters.AddWithValue("@rbr_stavke", najvecaVrednost);
        komanda2.Parameters.AddWithValue("@rbr_pregleda", Label2.Text);
        
        if (ListBoxDijagnoza.SelectedValue !="")//ako je selektovano
        {
            komanda2.Parameters.AddWithValue("@sif_bolesti", ListBoxDijagnoza.SelectedItem.Value);
        }
        else 
        {
            komanda2.Parameters.AddWithValue("@sif_bolesti", DBNull.Value);
        }
       
        if (ListBoxTerapija.SelectedValue != "")
        {
            komanda2.Parameters.AddWithValue("@sif_terapije", ListBoxTerapija.SelectedItem.Value);
        }
        else
        {
            komanda2.Parameters.AddWithValue("@sif_terapije", DBNull.Value);
        }
        int dodato = 0;
        konekcija.Open();

        SqlTransaction tr = konekcija.BeginTransaction();
       
        komanda.Transaction = tr;
        komanda2.Transaction = tr;
        dodato = komanda.ExecuteNonQuery();
        komanda2.ExecuteNonQuery();
        tr.Commit(); //izvrsavanje transakcije
        konekcija.Close();
        if (dodato > 0)
        {
            btnEvidentiraj.Visible = false;//isti pregled moze biti na vise stavki
            btnNoviPregled.Visible = true;
            DropDownList1.Enabled = false;//ali samo se dijagnoze i terapije mogu menjati
            ListBox1.Enabled = false;
            Calendar1.Enabled = false;
        }

    }

        
    protected void btnNoviPregled_Click(object sender, EventArgs e)
    {
        GenerisiSifru();//sifru novog pregleda
        DropDownList1.Enabled = true;
        ListBox1.Items.Clear();
        ListBox1.Enabled = true;
        Calendar1.Enabled = true;
        btnEvidentiraj.Visible = true;
        btnNoviPregled.Visible = false;
        btnSamoStavka.Visible = false;
        ListBoxDijagnoza.SelectedIndex = -1;//da ne ostane zapamceno ono sto je selektovano
        ListBoxTerapija.SelectedIndex = -1;
        DropDownList1.SelectedIndex = -1;
        ListBox1.SelectedIndex = -1;
    }
    
    protected void btnSamoStavka_Click1(object sender, EventArgs e)
    {//isti kod kao i gore samo bez transakcije
        string naredbaZK = "Select max(br_kartona) from Zdravstveni_karton where br_zdr_knjizice='" + ListBox1.SelectedItem.Value + "'";
      
        SqlCommand komandaZK = new SqlCommand(naredbaZK, konekcija);
        SqlDataReader citac;
        konekcija.Open();
        citac = komandaZK.ExecuteReader();
        citac.Read();
        int sifrakartona = int.Parse(citac[""].ToString());
        citac.Close();
        konekcija.Close();

        string naredbaSK = "Select max (rbr_stavke)from Stavka_kartona where br_kartona=@br_kartona";//ne treba nam provera, sada definitivno ima bar jedna stavka
        SqlCommand komandaSK = new SqlCommand(naredbaSK, konekcija);
        komandaSK.Parameters.AddWithValue("@br_kartona", sifrakartona);
        SqlDataReader citac2;
        konekcija.Open();
        citac2 = komandaSK.ExecuteReader();
        citac2.Read();
       
        int max = int.Parse(citac2[""].ToString())+1;
       
        citac2.Close();
        konekcija.Close();

        string naredba2 = "Insert into Stavka_kartona (br_kartona, rbr_stavke, rbr_pregleda, sif_bolesti, sif_terapije) values (@br_kartona, @rbr_stavke, @rbr_pregleda, @sif_bolesti, @sif_terapije)";
        SqlCommand komanda2 = new SqlCommand(naredba2, konekcija);

        komanda2.Parameters.AddWithValue("@br_kartona", sifrakartona);
        komanda2.Parameters.AddWithValue("@rbr_stavke", max);
        komanda2.Parameters.AddWithValue("@rbr_pregleda", Label2.Text);
        if (ListBoxDijagnoza.SelectedValue != "")
        {
            komanda2.Parameters.AddWithValue("@sif_bolesti", ListBoxDijagnoza.SelectedItem.Value);
        }
        else
        {
            komanda2.Parameters.AddWithValue("@sif_bolesti", DBNull.Value);
        }
        if (ListBoxTerapija.SelectedValue != "")
        {
            komanda2.Parameters.AddWithValue("@sif_terapije", ListBoxTerapija.SelectedItem.Value);
        }
        else
        {
            komanda2.Parameters.AddWithValue("@sif_terapije", DBNull.Value);
        }
        int dodato = 0;
        konekcija.Open();
        dodato = komanda2.ExecuteNonQuery();
        konekcija.Close();
        ListBoxDijagnoza.SelectedIndex = -1;
        ListBoxTerapija.SelectedIndex = -1;
    }
    }


