CREATE TABLE IF NOT EXISTS Food
(
	FoodID INT NOT NULL PRIMARY KEY,
	Category VARCHAR(30) NOT NULL,
	Menu VARCHAR(30) NULL,
	Bar BOOL NULL,
	Cost INT NOT NULL
);

CREATE TABLE IF NOT EXISTS Tour
(
	TourID INT NOT NULL PRIMARY KEY,
	Food INT NOT NULL,
	Hotel INT NOT NULL,
	Transfer INT NOT NULL,
	Cost INT NOT NULL,
	DateBegin DATE NOT NULL,
	DateEnd DATE NOT NULL
);

CREATE TABLE IF NOT EXISTS Hotel
(
	HotelID INT NOT NULL PRIMARY KEY,
	Name VARCHAR(50) NOT NULL,
	Type VARCHAR(40) NULL,
	Class INT NULL,
	SwimPool BOOL NULL,
	City VARCHAR(30) NULL,
	Cost INT NOT NULL
);

CREATE TABLE IF NOT EXISTS Users
(
	UserID INT NOT NULL PRIMARY KEY,
	ToursID INT[],
	Login VARCHAR(30),
	Password VARCHAR(30),
	AccessLevel INT NULL
);

CREATE TABLE IF NOT EXISTS Transfer
(
	TransferID INT NOT NULL PRIMARY KEY,
	Type VARCHAR(30) NOT NULL,
	CityFrom VARCHAR(30) NULL,
	DepartureTime TIMESTAMP NULL,
	Cost INT NULL
);