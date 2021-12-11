INSERT INTO Users (UserID, ToursID, Login, Password, AccessLevel) VALUES
(1, '{1, 4}', 'testt', '123', 1),
(2, '{}', 'testm', '123', 2),
(3, '{8}', 'Dealo', 'DealoProg1', 1),
(4, '{}', 'Koret', 'Koret97', 2),
(5, '{3, 4}','MilaUkn', 'SarkisMil', 1),
(6, '{}', 'GorVictor', 'Victor94', 2),
(7, '{6, 7}', 'test1', '123', 1),
(8, '{}', 'test2', '123', 2),
(9, '{9, 2}', 'test3', '123', 1),
(10, '{}', 'test4', '123', 2),
(11,'{3, 6}','Cairo','LYC22PMF7DW',1),
(12,'{7}','Kylynn','ESB83OOK0IB',1),
(13,'{6}','Herman','KRX78NWR7PU',1),
(14,'{}','Piper','JXH48GEV4IU',2),
(15,'{}','Jenna','KSI96IIQ8AO',2),
(16,'{}','Jasmine','MJV61MZY1SB',2),
(17,'{4, 2}','Olga','AOA46RIW4IT',1),
(18,'{6, 2}','Zephania','AFI68AEU8VK',1),
(19,'{7, 4, 8}','Ivory','ZYB55PNJ6ZJ',1),
(20,'{9}','Ava','TMU41BXZ7QF',1);

INSERT INTO Food (FoodID, Category, Menu, Bar, Cost) VALUES 
(1,'All inclusive','',True,4061),
(2,'Half board','Dietary',False,5350),
(3,'Continental breakfast','Children',True,8774),
(4,'American breakfast','',False,8299),
(5,'All inclusive','Children',True,5524),
(6,'Full board','Dietary',True,4018),
(7,'Half board','Vegeterian',True,1799),
(8,'Breakfast','',True,5965),
(9,'All inclusive','Children',True,3359),
(10,'Continental breakfast','Vegeterian',False,7787);


INSERT INTO Hotel (HotelID, Name, Type, Class, SwimPool, City, Cost) VALUES 
(1,'Venezia','Apartment',2,False,'Moscow',6757),
(2,'MOSS Boutique Hotel','BnB',2,False,'London',18746),
(3,'Democratia','Hotel',3,False,'Madrid',18322),
(4,'Scelerisque Scelerisque Limited','Camping',5,True,'Rome',19834),
(5,'Paradise','Vila',4,True,'London',19805),
(6,'Semper Auctor Mauris Ltd','Hostel',4,True,'Berlin',12523),
(7,'Pleaser','Apartment',4,False,'Madrid',8751),
(8,'Fringilla Porttitor Vulputate PC','Guest house',2,False,'Rome',9203),
(9,'Bristol','Camping',5,False,'Paris',13327),
(10,'South','Hotel',1,False,'Vena',12697);


INSERT INTO Transfer (TransferID, Type, CityFrom, CityTo, DepartureTime, Cost) VALUES
(1,'Bus','Moscow','2022-03-09 03:22:04',9838),
(2,'Plane','Berlin','2022-10-14 14:25:51',5138),
(3,'Train','Rome','2021-03-17 15:16:38',1658),
(4,'Bus','Vena','2022-06-02 12:08:54',2616),
(5,'Plane','Madrid','2021-07-24 16:05:25',1629),
(6,'Train','London','2022-08-30 05:04:29',14678),
(7,'Bus','Moscow','2021-08-30 17:06:53',2573),
(8,'Plane','London','2021-03-18 03:53:46',6770),
(9,'Train','Berlin','2021-02-17 04:09:37',2858),
(10,'Bus','Madrid','2021-10-14 17:31:13',11699);


INSERT INTO Tour (TourID, Food, Hotel, Transfer, Cost, DateBegin, DateEnd) VALUES
(1,8,9,5,18143,'2021-08-13','2022-05-26'),
(2,6,3,3,40434,'2021-10-31','2021-12-02'),
(3,7,4,10,41958,'2022-11-13','2022-12-07'),
(4,2,2,8,80498,'2022-11-23','2022-12-02'),
(5,6,10,2,92326,'2022-05-08','2022-11-27'),
(6,7,1,5,58299,'2021-03-02','2022-10-02'),
(7,10,6,7,26755,'2022-04-21','2022-12-16'),
(8,9,5,1,40505,'2022-04-02','2022-11-18'),
(9,6,2,8,55085,'2021-12-28','2022-07-28'),
(10,8,1,9,74056,'2022-03-19','2022-08-24');
