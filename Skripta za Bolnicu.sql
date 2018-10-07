






create table Dijagnoza
(
	sif_bolesti int primary key,
	naziv_bolesti varchar(30) not null,
	grupa_bolesti varchar(20) not null
)
go


create table Terapija
(
	sif_terapije int primary key,
	naziv_terapije varchar(50) not null
)
go



create table Lekar
(
	id_lekara smallint primary key,
	ime_lekara varchar(20) not null,
	prezime_lekara varchar(30) not null,
	str_sprema_lekara char (3) check (str_sprema_lekara in ('vss','spc','doc')) not null,
	zvanje_lekara varchar(20) not null,
	stecene_dipl varchar(50) not null
)
go



create table Lekar_specijalista
(
	id_lekara smallint primary key references Lekar (id_lekara),
	zavrsena_spec varchar(20) not null,
	mesto_spec varchar(20) not null,
	dat_zavr_spec datetime not null
)
go

create table Lekar_opste_prakse
(
	id_lekara smallint primary key references Lekar (id_lekara)
)
go


create table Pacijent
(
	br_zdr_knjizice varchar(10) primary key,
	ime_pacijenta varchar(20) not null,
	prezime_pacijenta varchar(30) not null,
	adresa_pacijenta varchar(50) not null,
	tel_pacijenta varchar(20) null,
	id_lekara smallint references Lekar (id_lekara) null
)
go


create table Pregled
(
	rbr_pregleda int primary key,
	dat_i_vreme_pregleda datetime not null,
	id_lekara smallint references Lekar (id_lekara) not null,
	br_zdr_knjizice varchar(10) references Pacijent(br_zdr_knjizice) not null
)
go


create table Zdravstveni_karton
(
	br_kartona smallint primary key,
	dat_otvaranja_kartona datetime not null,
	br_zdr_knjizice varchar(10) references Pacijent(br_zdr_knjizice) not null
)
go


create table Stavka_kartona
(
	br_kartona smallint references Zdravstveni_karton (br_kartona),
	rbr_stavke int,
	rbr_pregleda int references Pregled (rbr_pregleda) not null,
	sif_bolesti int references Dijagnoza (sif_bolesti) null,
	sif_terapije int references Terapija (sif_terapije) null,
	primary key (br_kartona, rbr_stavke)
)
go















