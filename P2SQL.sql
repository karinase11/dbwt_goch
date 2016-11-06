-- data definition language
-- sql stub
-- wird erweitert am 31.10.

-- Tabellen löschen zu Beginn des Skripts
-- beachten Sie die Lösch-Reihenfolge


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Produkt]') AND type in (N'U')) 
DROP TABLE [Produkt]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BE-Benutzer]') AND type in (N'U')) 
DROP TABLE [BE-Benutzer]


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Rechte]') AND type in (N'U')) 
DROP TABLE [Rechte]


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Bewertung]') AND type in (N'U')) 
DROP TABLE [Bewertung]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[FE-Benutzer]') AND type in (N'U')) 
DROP TABLE [FE-Benutzer]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[FH-Angehörige]') AND type in (N'U')) 
DROP TABLE [FH-Angehörige]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GAST]') AND type in (N'U')) 
DROP TABLE [GAST]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Student]') AND type in (N'U')) 
DROP TABLE [Student]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Mitarbeiter]') AND type in (N'U')) 
DROP TABLE [Mitarbeiter]


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Preis]') AND type in (N'U')) 
DROP TABLE [Preis]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Zutat]') AND type in (N'U')) 
DROP TABLE [Zutat]


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Zahlung]') AND type in (N'U')) 
DROP TABLE [Zahlung]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Bild]') AND type in (N'U')) 
DROP TABLE [Bild]


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Kategorie]') AND type in (N'U')) 
DROP TABLE [Kategorie]

-- Tabellen definieren

CREATE TABLE [BE-Nutzer]
(
ID INT IDENTITY(1,1) PRIMARY KEY not null,
[E-mail] VARCHAR(200) NOT NULL,
[Letzter Login] timestamp not null,
[Admin] bit not null,

[Algo] varchar(6) check( Algo in('sha1','sha256')) not null,
[Hash] char(24) not null,
[SALT] char(32) not null,
Stretch int not null,

)

CREATE TABLE [Rechte]
(
ID INT IDENTITY(1,1) PRIMARY KEY not null,
[Tabelle] varchar(100) check(Tabelle in ('Kategorie','Produkt','Bild','Preis','Zutat'))  not null,
)

CREATE TABLE [Bewertung]
(
ID INT IDENTITY(1,1) PRIMARY KEY not null,
[Bemerkung] varchar(140) not null,
[Note] tinyint check (Note in(1,2,3,4,5)) not null,
[Sichtungen] int not null default 0,
[BE-Nutzer_ID] int references [BE-Nutzer](ID) not null,
)

CREATE TABLE [FE-Nutzer]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Aktiv bit not null,
[Letzter Login] timestamp not null,
[BE-Nutzer_ID] int foreign key references [BE-Nutzer](ID), -- BE benutzer legt FE nutzer an


[Algo] varchar(6) check( Algo in('sha1','sha256')) not null,
[Hash] char(24) not null,
[SALT] char(32) not null,
Stretch int not null,
)

CREATE TABLE[FH-Angehörige]
(
[FE-Nutzer_ID] int references [FE-Nutzer](ID) ON DELETE CASCADE PRIMARY KEY not null,
[Name] varchar(100) not null,
[Fachbereich] int not null,
[E-Mail] varchar(150) UNIQUE not null,
)

CREATE TABLE [Gast]
(
[FE-Nutzer_ID] int references [FE-Nutzer](ID) ON DELETE CASCADE PRIMARY KEY not null,
[Name] varchar(100) not null,
von DATETIME not null,
bis DATETIME not null,
)

CREATE TABLE [Mitarbeiter]
(
[FH-Angehörige_ID] int references [FH-Angehörige]([FE-Nutzer_ID]) ON DELETE CASCADE PRIMARY KEY not null,
[Büro] varchar(10),
[MA-Nummer] int UNIQUE check([MA-Nummer] >=10000 and [MA-Nummer] <= 9999999) not null,
)

CREATE TABLE [Student]
(
[FH-Angehörige_ID] int references [FH-Angehörige]([FE-Nutzer_ID]) ON DELETE CASCADE PRIMARY KEY not null,
[Studiengang] varchar(100),
[MA-Nummer] int UNIQUE check([MA-Nummer] >=10000 and [MA-Nummer] <= 9999999) not null,
)

CREATE TABLE [Bild]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[Unter-schrift] varchar(100) not null,
[Binar-daten] varbinary(max) not null, 
[Alt-Text] varchar(250) not null,
Title varchar(100) not null,
)

CREATE TABLE [Kategorie]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[Bild_ID] int references Bild(ID),
Bezeichnung varchar(100) not null,
)
-- Produkte
-- ID, Kategorie_ID-FK, Name, Beschreibung, von timestamp, bis timestamp

CREATE TABLE [Produkt] ( 
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(50) not null,
	Beschreibung VARCHAR(50) not null,
	von DATETIME,
	bis DATETIME,
	[Bild_ID] int references Bild(ID) not null,
	[Kategorie_ID] int references Kategorie(ID),

) 


CREATE TABLE [Preis]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[Rolle] varchar(15) check(Rolle in ('Gast','Mitarbeiter','Student'))  not null,
Preis money not null,
[Produkt_ID] int references Produkt(ID) not null,
)

CREATE TABLE [Zutat]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
Bezeichnung varchar(100) not null,
)

CREATE TABLE [Zahlung]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[Status] int NOT NULL,
Zeitstempel timestamp not null,
AutServer varchar(30) not null,
Betrag money not null
)



-- N:M RELATIONEN

CREATE TABLE[schreibtBewertung]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[FH-Angehörige_ID] int references [FH-Angehörige]([FE-Nutzer_ID]) not null,
[Produkt_ID] int references Produkt(ID) not null,
[Bewertung_ID] int references Bewertung(ID) not null,
)

CREATE TABLE[KauftProdukt]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[FE-Nutzer_ID] int references [FE-Nutzer](ID) not null,
[Produkt_ID] int references Produkt(ID) not null,
[Zahlung_ID] int references Zahlung(ID) not null,
)


CREATE TABLE [hatZutat]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[Zutat_ID] int references Zutat(ID) not null,
[Produkt_ID] int references Produkt(ID) not null,
)

CREATE TABLE [BE-Nutzer-Rechte]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[BE-Nutzer_ID] int references [BE-Nutzer](ID) not null,
[Rechte_ID] int references Rechte(ID) not null,
)

CREATE TABLE [BefreundetmitFE]
(
[Befreundet_ID] int references [FE-Nutzer](ID) not null,
[Freunde_ID] int references [FE-Nutzer](ID) not null,
)


CREATE TABLE [Oberkategorie]
(
ID INT IDENTITY(1,1) PRIMARY KEY,
[OberKategorie1_ID] int references [Kategorie](ID) not null,
[OberKategorie2_ID] int references [Kategorie](ID) not null,
)


-- Tabellen ändern (fügen Sie hier CHECK und andere Constraints, falls Sie sie nicht bei der Definition verwenden können)

-- ALTER TABLE [beispiel] ADD CONSTRAINT [constrname] CHECK (1=1)