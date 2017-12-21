CREATE SCHEMA hs;

CREATE TABLE hs.Species (
	SpeciesID int IDENTITY(1,1) PRIMARY KEY,
	SpeciesName varchar(50) NOT NULL	
);

CREATE TABLE hs.Rooms (
	RoomID int IDENTITY(1,1) PRIMARY KEY,
	RoomNumber int,
	UNIQUE(RoomNumber)
);

CREATE TABLE hs.Animals (
	AnimalID int IDENTITY(1,1) PRIMARY KEY,
	Name varchar(50) NOT NULL,
	SpeciesID int,
	FOREIGN KEY (SpeciesID) REFERENCES hs.Species(SpeciesID),
	RoomID int,
	FOREIGN KEY (RoomID) REFERENCES hs.Rooms(RoomID),
	IsAdopted bit,
	HasShots bit,
	Price float,
	FoodPerWeek int
);

CREATE TABLE hs.Cities (
	CityID int IDENTITY(1,1) PRIMARY KEY,
	CityName varchar(50) NOT NULL,
	UNIQUE(CityName)
);

CREATE TABLE hs.States (
	StateID int IDENTITY(1,1) PRIMARY KEY,
	StateName varchar(2) NOT NULL,
	UNIQUE(StateName)
);

CREATE TABLE hs.Zip_Codes (
	ZipCodeID int IDENTITY(1,1) PRIMARY KEY,
	Number varchar(5) NOT NULL,
	UNIQUE(Number)
);

CREATE TABLE hs.Addresses (
	AddressID int IDENTITY(1,1) PRIMARY KEY,
	Street1 varchar(100) NOT NULL,
	CityID int,
	FOREIGN KEY (AddressID) REFERENCES hs.Cities(CityID),
	StateID int,
	FOREIGN KEY (StateID) REFERENCES hs.States(StateID),
	ZipCodeID int,
	FOREIGN KEY (ZipCodeID) REFERENCES hs.Zip_Codes(ZipCodeID)
);

CREATE TABLE hs.Adopters (
	AdopterID int IDENTITY(1,1) PRIMARY KEY,
	AdopterName varchar(50) NOT NULL,
	AdopterEmail varchar(50) NOT NULL,
	AddressID int,
	FOREIGN KEY (AddressID) REFERENCES hs.Addresses(AddressID),
	HasPaid bit
);

INSERT INTO hs.Species VALUES ('cat');

INSERT INTO hs.Species VALUES ('dog');

INSERT INTO hs.Species VALUES ('bird');

INSERT INTO hs.Species VALUES ('rabbit');

INSERT INTO hs.Species VALUES ('ferret');