USE BonVoyage_TravelAgency;

-- 1 
-- Добавление туров и отелей

INSERT INTO Tours (Title, Description, Duration, Price, Country, Route, StartDate, EndDate)
VALUES ('Balearic', 'Enjoy a luxurious stay at the Club Jumbo Palia Puerto del Sol with a newly renovated setup and breathtaking views.', 7, 481, 'Spain', 'Balearic Islands', '2024-09-15', '2024-09-22');

INSERT INTO Hotels (Name, Location, PricePerNight, StarRating, HasSwimmingPool, TourId)
VALUES ('Club Jumbo Palia Puerto del Sol', 'Balearic Islands, Spain', 69, 3, 1, (SELECT TourId FROM Tours WHERE Title='Balearic'));

-- фото для тура
INSERT INTO TourPhotos (TourId, PhotoUrl)
VALUES ((SELECT TourId FROM Tours WHERE Title='Balearic'), '/wwwroot/images/hotels/jumbo-palia-puerto_Spain.png');

-- фото для отеля
INSERT INTO HotelPhotos (HotelId, PhotoUrl)
VALUES ((SELECT HotelId FROM Hotels WHERE Name='Club Jumbo Palia Puerto del Sol'), '/wwwroot/images/hotels/jumbo-palia-puerto_Spain.png');

UPDATE Hotels
SET City = 'Balearic Islands', Country = 'Spain'
WHERE HotelId = 1; 

-- 2
INSERT INTO Tours (Title, Description, Duration, Price, Country, Route, StartDate, EndDate)
VALUES ('Greece', 'Stay at the Sun Beach Resort Complex with beachside views.', 7, 698, 'Greece', 'Ialyssos Beach, Rhodes Island', '2024-08-15', '2024-08-22');

INSERT INTO Hotels (Name, Location, PricePerNight, StarRating, HasSwimmingPool, City, Country, TourId)
VALUES ('Sun Beach Resort', 'Ialyssos Beach, Rhodes Island, Greece', 100.00, 4, 1, 'Rhodes Island', 'Greece', (SELECT TourId FROM Tours WHERE Title='Greece'));

INSERT INTO TourPhotos (TourId, PhotoUrl)
VALUES ((SELECT TourId FROM Tours WHERE Title='Greece'), '/wwwroot/images/tours/sun-beach-resort_Greece.png');

INSERT INTO HotelPhotos (HotelId, PhotoUrl)
VALUES ((SELECT HotelId FROM Hotels WHERE Name='Sun Beach Resort'), '/wwwroot/images/hotels/sun-beach-resort_Greece.png');

--3
USE BonVoyage_TravelAgency;
INSERT INTO Tours (Title, Description, Duration, Price, Country, Route, StartDate, EndDate)
VALUES ('Crete', 'Stay at the Hôtel Fragiskos with convivial atmosphere.', 7, 537, 'Greece', 'Crete', '2024-09-01', '2024-09-08');

INSERT INTO Hotels (Name, Location, PricePerNight, StarRating, HasSwimmingPool, City, Country, TourId)
VALUES ('Hôtel Fragiskos', 'Crete, Greece', 77.00, 3, 1, 'Crete', 'Greece', (SELECT TourId FROM Tours WHERE Title='Crete'));

INSERT INTO TourPhotos (TourId, PhotoUrl)
VALUES ((SELECT TourId FROM Tours WHERE Title='Crete'), 'wwwroot/images/tours/fragiskos_crete.png');

INSERT INTO HotelPhotos (HotelId, PhotoUrl)
VALUES ((SELECT HotelId FROM Hotels WHERE Name='Hôtel Fragiskos'), 'wwwroot/images/tours/fragiskos_crete.png');

--4
INSERT INTO Tours (Title, Description, Duration, Price, Country, Route, StartDate, EndDate)
VALUES ('Crete', 'Enjoy a luxurious stay at the Sitia Beach City Resort & Spa with beautiful views and excellent services.', 7, 340, 'Greece', 'Sitia, Crete', '2024-10-16', '2024-10-23');

INSERT INTO Hotels (Name, Location, PricePerNight, StarRating, HasSwimmingPool, City, Country, TourId)
VALUES ('Sitia Beach City Resort & Spa', '14 Karamanli Avenue, Sitia, Crete, Greece', 48.57, 5, 1, 'Sitia', 'Greece', 4);

INSERT INTO TourPhotos (TourId, PhotoUrl)
VALUES (4, '/wwwroot/images/tours/sitia_Crete.png');

INSERT INTO HotelPhotos (HotelId, PhotoUrl)
VALUES ((SELECT HotelId FROM Hotels WHERE Name='Sitia Beach City Resort & Spa'), '/wwwroot/images/hotels/sitia_Crete.png');

--5
INSERT INTO Tours (Title, Description, Duration, Price, Country, Route, StartDate, EndDate)
VALUES ('Egypt', 'Stay at the Elysees Hurghada (ex. Elysees Dream Beach) with excellent amenities and beautiful views.', 7, 42579, 'Egypt', 'Hurghada', '2024-11-14', '2024-11-21');

INSERT INTO Hotels (Name, Location, PricePerNight, StarRating, HasSwimmingPool, City, Country, TourId)
VALUES ('Elysees Hurghada', 'Sheraton Road, Hurghada, Egypt', 6070.86, 4, 1, 'Hurghada', 'Egypt', 5);

INSERT INTO TourPhotos (TourId, PhotoUrl)
VALUES (5, '/wwwroot/images/tours/hurghada_egypt.png');

INSERT INTO HotelPhotos (HotelId, PhotoUrl)
VALUES ((SELECT HotelId FROM Hotels WHERE Name='Elysees Hurghada'), '/wwwroot/images/hotels/hurghada_egypt.png');


UPDATE TourPhotos SET PhotoUrl = REPLACE(PhotoUrl, '/wwwroot', '');
UPDATE TourPhotos SET PhotoUrl = REPLACE(PhotoUrl, 'wwwroot', '');
