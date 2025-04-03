DELETE FROM Administrators;
SET IDENTITY_INSERT Administrators ON
INSERT INTO Administrators (Id, Name, PasswordHash, Email) VALUES
(1, 'MyNameIsAdmin', 'imthebest123', 'admin1@example.com'),
(2, 'admin2', 'securepass2', 'admin2@example.com'),
(3, 'admin3', 'securepass3', 'admin3@example.com'),
(4, 'admin4', 'securepass4', 'admin4@example.com'),
(5, 'admin5', 'securepass5', 'admin5@example.com');
SET IDENTITY_INSERT Administrators Off

DELETE FROM Locations;
SET IDENTITY_INSERT Locations ON
INSERT INTO Locations (Id, Name, Address, Capacity) VALUES
(1, 'Cineplex Downtown', '123 Main Street, Cityville', 250),
(2, 'Grand Cinema', '45 Elm Avenue, Movietown', 300),
(3, 'Starlight Theaters', '678 Oak Drive, Filmcity', 200),
(4, 'Metro Movie House', '90 Maple Street, Cinemaville', 350),
(5, 'Galaxy Cinemas', '555 Star Blvd, Galaxy Town', 400);
SET IDENTITY_INSERT Locations OFF

DELETE FROM Vendors;
SET IDENTITY_INSERT Vendors ON
INSERT INTO Vendors (Id, Name, PasswordHash, Email) VALUES
(1, 'TicketMaster Pro', 'vendorpass1', 'contact@ticketmasterpro.com'),
(2, 'CinemaDeals', 'vendorpass2', 'info@cinemadeals.com'),
(3, 'MoviePass Express', 'vendorpass3', 'support@moviepassexpress.com'),
(4, 'BigScreen Tickets', 'vendorpass4', 'sales@bigscreen.com'),
(5, 'CineWorld Tickets', 'vendorpass5', 'help@cineworldtickets.com');
SET IDENTITY_INSERT Vendors OFF

DELETE FROM LocationVendor;
INSERT INTO LocationVendor (LocationsId, VendorsId) VALUES
(1, 1), -- TicketMaster Pro sells only for Cineplex Downtown
(2, 2), -- CinemaDeals sells only for Grand Cinema
(3, 3), -- MoviePass Express sells for Starlight Theaters
(4, 4), -- BigScreen Tickets sells only for Metro Movie House
(5, 5), -- CineWorld Tickets sells for Galaxy Cinemas
(1, 5), -- CineWorld Tickets also sells for Cineplex Downtown
(2, 5), -- CineWorld Tickets also sells for Grand Cinema
(3, 4); -- MoviePass Express sells for Metro Movie House

DELETE FROM Movies;
SET IDENTITY_INSERT Movies ON
INSERT INTO Movies (Id, Title, Description, LengthInSeconds, ImdbId) VALUES
(1, 'Inception', 'A thief who enters the dreams of others to steal secrets.', 8880, 1375666),
(2, 'The Dark Knight', 'Batman faces the Joker in Gotham City.', 9120, 468569),
(3, 'Interstellar', 'A team of explorers travels through a wormhole in space.', 10140, 816692),
(4, 'The Matrix', 'A hacker learns the truth about reality and joins a rebellion.', 8160, 133093),
(5, 'Fight Club', 'An office worker forms an underground fight club.', 8340, 137523),
(6, 'The Shawshank Redemption', 'A banker is imprisoned for a crime he did not commit.', 8520, 111161),
(7, 'Pulp Fiction', 'Interwoven stories of crime, violence, and redemption.', 9240, 110912),
(8, 'Forrest Gump', 'A simple man witnesses historic events in America.', 8520, 109830),
(9, 'Gladiator', 'A betrayed Roman general seeks revenge.', 9300, 172495),
(10, 'Titanic', 'A romance set on the ill-fated Titanic ship.', 11700, 120338),
(11, 'The Godfather', 'A mafia family’s patriarch passes control to his son.', 10500, 68646),
(12, 'The Lord of the Rings: The Fellowship of the Ring', 'A hobbit begins his journey to destroy a powerful ring.', 10620, 120737),
(13, 'The Avengers', 'Superheroes unite to save the world from Loki.', 8580, 848228),
(14, 'Jurassic Park', 'Dinosaurs are brought back to life on an island.', 7620, 107290);
SET IDENTITY_INSERT Movies OFF

DELETE FROM Screenings;
SET IDENTITY_INSERT Screenings ON
INSERT INTO Screenings (id, OfMovieId, InLocationId, Time, SeatsTaken) VALUES
-- Inception
(1, 1, 1, '2025-04-05 18:00:00', 120),
(2, 1, 2, '2025-04-06 20:30:00', 95),
(3, 1, 1, '2025-04-07 15:00:00', 80),
(4, 1, 3, '2025-04-08 21:45:00', 150),

-- The Dark Knight
(5, 2, 3, '2025-04-07 19:00:00', 200),
(6, 2, 4, '2025-04-08 21:00:00', 175),
(7, 2, 3, '2025-04-09 17:30:00', 90),
(8, 2, 5, '2025-04-10 20:15:00', 220),

-- Interstellar
(9, 3, 5, '2025-04-09 17:45:00', 140),
(10, 3, 1, '2025-04-10 19:30:00', 160),
(11, 3, 2, '2025-04-11 18:30:00', 95),
(12, 3, 4, '2025-04-12 21:15:00', 180),

-- The Matrix
(13, 4, 2, '2025-04-11 20:00:00', 135),
(14, 4, 3, '2025-04-12 22:00:00', 145),
(15, 4, 2, '2025-04-13 16:00:00', 85),
(16, 4, 5, '2025-04-14 20:45:00', 175),

-- Fight Club
(17, 5, 4, '2025-04-13 18:15:00', 110),
(18, 5, 5, '2025-04-14 20:45:00', 130),
(19, 5, 4, '2025-04-15 19:00:00', 140),
(20, 5, 2, '2025-04-16 22:30:00', 100),

-- The Shawshank Redemption
(21, 6, 1, '2025-04-15 19:30:00', 170),
(22, 6, 2, '2025-04-16 21:15:00', 165),
(23, 6, 3, '2025-04-17 18:00:00', 90),
(24, 6, 1, '2025-04-18 20:45:00', 190),

-- Pulp Fiction
(25, 7, 3, '2025-04-17 17:00:00', 145),
(26, 7, 4, '2025-04-18 19:45:00', 125),
(27, 7, 3, '2025-04-19 20:15:00', 80),
(28, 7, 5, '2025-04-20 21:30:00', 175),

-- Forrest Gump
(29, 8, 5, '2025-04-19 18:30:00', 155),
(30, 8, 1, '2025-04-20 20:15:00', 160),
(31, 8, 2, '2025-04-21 19:45:00', 140),
(32, 8, 3, '2025-04-22 22:00:00', 120),

-- Gladiator
(33, 9, 2, '2025-04-21 19:00:00', 175),
(34, 9, 3, '2025-04-22 21:30:00', 155),
(35, 9, 5, '2025-04-23 18:45:00', 130),
(36, 9, 1, '2025-04-24 20:30:00', 195),

-- Titanic
(37, 10, 4, '2025-04-23 17:45:00', 200),
(38, 10, 5, '2025-04-24 20:00:00', 215),
(39, 10, 3, '2025-04-25 18:15:00', 145),
(40, 10, 2, '2025-04-26 22:30:00', 185),

-- The Godfather
(41, 11, 1, '2025-04-25 19:30:00', 190),
(42, 11, 2, '2025-04-26 22:00:00', 200),
(43, 11, 4, '2025-04-27 17:45:00', 210),
(44, 11, 5, '2025-04-28 20:30:00', 220),

-- The Lord of the Rings: The Fellowship of the Ring
(45, 12, 3, '2025-04-27 18:15:00', 195),
(46, 12, 4, '2025-04-28 21:00:00', 210),
(47, 12, 1, '2025-04-29 17:00:00', 170),
(48, 12, 2, '2025-04-30 20:45:00', 200),

-- The Avengers
(49, 13, 5, '2025-04-29 19:45:00', 215),
(50, 13, 1, '2025-04-30 22:30:00', 230),
(51, 13, 2, '2025-05-01 18:00:00', 205),
(52, 13, 3, '2025-05-02 21:15:00', 180),

-- Jurassic Park
(53, 14, 2, '2025-05-01 17:00:00', 190),
(54, 14, 3, '2025-05-02 19:30:00', 175),
(55, 14, 4, '2025-05-03 18:15:00', 200),
(56, 14, 5, '2025-05-04 20:45:00', 225);
SET IDENTITY_INSERT Screenings OFF

DELETE FROM People;
SET IDENTITY_INSERT People ON
INSERT INTO People (Id, Name, BirthDate, Nationality) VALUES
(1, 'Leonardo DiCaprio', '1974-11-11', 'American'),
(2, 'Joseph Gordon-Levitt', '1981-03-17', 'American'),
(3, 'Ellen Page', '1987-02-21', 'Canadian'),
(4, 'Christopher Nolan', '1970-07-30', 'British'),
(5, 'Christian Bale', '1974-01-30', 'British'),
(6, 'Heath Ledger', '1979-04-04', 'Australian'),
(7, 'Aaron Eckhart', '1968-03-12', 'American'),
(8, 'Matthew McConaughey', '1969-11-04', 'American'),
(9, 'Anne Hathaway', '1982-11-12', 'American'),
(10, 'Jessica Chastain', '1977-03-24', 'American'),
(11, 'Keanu Reeves', '1964-09-02', 'Canadian'),
(12, 'Laurence Fishburne', '1961-07-30', 'American'),
(13, 'Carrie-Anne Moss', '1967-08-21', 'Canadian'),
(14, 'Edward Norton', '1969-08-18', 'American'),
(15, 'Brad Pitt', '1963-12-18', 'American'),
(16, 'Helena Bonham Carter', '1966-05-26', 'British'),
(17, 'Tim Robbins', '1958-10-16', 'American'),
(18, 'Morgan Freeman', '1937-06-01', 'American'),
(19, 'Samuel L. Jackson', '1948-12-21', 'American'),
(20, 'Uma Thurman', '1970-04-29', 'American'),
(21, 'John Travolta', '1954-02-18', 'American'),
(22, 'Tom Hanks', '1956-07-09', 'American'),
(23, 'Robin Wright', '1966-04-08', 'American'),
(24, 'Russell Crowe', '1964-04-07', 'New Zealander'),
(25, 'Joaquin Phoenix', '1974-10-28', 'American'),
(26, 'Leonardo DiCaprio', '1974-11-11', 'American'),
(27, 'Kate Winslet', '1975-10-05', 'British'),
(28, 'Marlon Brando', '1924-04-03', 'American'),
(29, 'Al Pacino', '1940-04-25', 'American'),
(30, 'James Caan', '1940-03-26', 'American'),
(31, 'Elijah Wood', '1981-01-28', 'American'),
(32, 'Ian McKellen', '1939-05-25', 'British'),
(33, 'Viggo Mortensen', '1958-10-20', 'American'),
(34, 'Robert Downey Jr.', '1965-04-04', 'American'),
(35, 'Chris Evans', '1981-06-13', 'American'),
(36, 'Scarlett Johansson', '1984-11-22', 'American'),
(37, 'Sam Neill', '1947-09-14', 'New Zealander'),
(38, 'Laura Dern', '1967-02-10', 'American'),
(39, 'Jeff Goldblum', '1952-10-22', 'American');
SET IDENTITY_INSERT People OFF

DELETE FROM Credits;
INSERT INTO Credits (OfMovieId, WhoIsId, Role) VALUES
(1, 1, 'Actor'), -- Inception: Leonardo DiCaprio
(1, 2, 'Actor'), -- Inception: Joseph Gordon-Levitt
(1, 3, 'Actor'), -- Inception: Ellen Page
(1, 4, 'Director'), -- Inception: Christopher Nolan
(1, 4, 'Writer'), -- Inception: Christopher Nolan
(2, 5, 'Actor'), -- The Dark Knight: Christian Bale
(2, 6, 'Actor'), -- The Dark Knight: Heath Ledger
(2, 7, 'Actor'), -- The Dark Knight: Aaron Eckhart
(2, 4, 'Director'), -- The Dark Knight: Christopher Nolan
(2, 4, 'Writer'), -- The Dark Knight: Christopher Nolan
(3, 8, 'Actor'), -- Interstellar: Matthew McConaughey
(3, 9, 'Actor'), -- Interstellar: Anne Hathaway
(3, 10, 'Actor'), -- Interstellar: Jessica Chastain
(3, 4, 'Director'), -- Interstellar: Christopher Nolan
(3, 4, 'Writer'), -- Interstellar: Christopher Nolan
(4, 11, 'Actor'), -- The Matrix: Keanu Reeves
(4, 12, 'Actor'), -- The Matrix: Laurence Fishburne
(4, 13, 'Actor'), -- The Matrix: Carrie-Anne Moss
(4, 11, 'Director'), -- The Matrix: Keanu Reeves
(4, 11, 'Writer'), -- The Matrix: Keanu Reeves
(5, 14, 'Actor'), -- Fight Club: Edward Norton
(5, 15, 'Actor'), -- Fight Club: Brad Pitt
(5, 16, 'Actor'), -- Fight Club: Helena Bonham Carter
(5, 14, 'Director'), -- Fight Club: Edward Norton
(5, 14, 'Writer'), -- Fight Club: Edward Norton
(6, 17, 'Actor'), -- The Shawshank Redemption: Tim Robbins
(6, 18, 'Actor'), -- The Shawshank Redemption: Morgan Freeman
(6, 17, 'Director'), -- The Shawshank Redemption: Tim Robbins
(6, 17, 'Writer'), -- The Shawshank Redemption: Tim Robbins
(7, 19, 'Actor'), -- Pulp Fiction: Samuel L. Jackson
(7, 20, 'Actor'), -- Pulp Fiction: Uma Thurman
(7, 21, 'Actor'), -- Pulp Fiction: John Travolta
(7, 19, 'Director'), -- Pulp Fiction: Samuel L. Jackson
(7, 19, 'Writer'), -- Pulp Fiction: Samuel L. Jackson
(8, 22, 'Actor'), -- Forrest Gump: Tom Hanks
(8, 23, 'Actor'), -- Forrest Gump: Robin Wright
(8, 22, 'Director'), -- Forrest Gump: Tom Hanks
(8, 22, 'Writer'), -- Forrest Gump: Tom Hanks
(9, 24, 'Actor'), -- Gladiator: Russell Crowe
(9, 25, 'Actor'), -- Gladiator: Joaquin Phoenix
(9, 24, 'Director'), -- Gladiator: Russell Crowe
(9, 24, 'Writer'), -- Gladiator: Russell Crowe
(10, 26, 'Actor'), -- Titanic: Leonardo DiCaprio
(10, 27, 'Actor'), -- Titanic: Kate Winslet
(10, 26, 'Director'), -- Titanic: Leonardo DiCaprio
(10, 26, 'Writer'), -- Titanic: Leonardo DiCaprio
(11, 28, 'Actor'), -- The Godfather: Marlon Brando
(11, 29, 'Actor'), -- The Godfather: Al Pacino
(11, 30, 'Actor'), -- The Godfather: James Caan
(11, 28, 'Director'), -- The Godfather: Marlon Brando
(11, 28, 'Writer'), -- The Godfather: Marlon Brando
(12, 31, 'Actor'), -- The Lord of the Rings: The Fellowship of the Ring: Elijah Wood
(12, 32, 'Actor'), -- The Lord of the Rings: The Fellowship of the Ring: Ian McKellen
(12, 33, 'Actor'), -- The Lord of the Rings: The Fellowship of the Ring: Viggo Mortensen
(12, 31, 'Director'), -- The Lord of the Rings: The Fellowship of the Ring: Elijah Wood
(12, 31, 'Writer'), -- The Lord of the Rings: The Fellowship of the Ring: Elijah Wood
(13, 34, 'Actor'), -- The Avengers: Robert Downey Jr.
(13, 35, 'Actor'), -- The Avengers: Chris Evans
(13, 36, 'Actor'), -- The Avengers: Scarlett Johansson
(13, 34, 'Director'), -- The Avengers: Robert Downey Jr.
(13, 34, 'Writer'), -- The Avengers: Robert Downey Jr.
(14, 37, 'Actor'), -- Jurassic Park: Sam Neill
(14, 38, 'Actor'), -- Jurassic Park: Laura Dern
(14, 39, 'Actor'), -- Jurassic Park: Jeff Goldblum
(14, 37, 'Director'), -- Jurassic Park: Sam Neill
(14, 37, 'Writer'); -- Jurassic Park: Sam Neill

DELETE FROM Users;
SET IDENTITY_INSERT Users ON
INSERT INTO Users (Id, Username, PasswordHash) VALUES
(1, 'AliceS', 'password123'),
(2, 'BobJ', 'securepass'),
(3, 'CharlieW', 'mysecret'),
(4, 'DavidB', 'strongpass'),
(5, 'EmilyD', 'pass1234'),
(6, 'FrankM', 'mypassword'),
(7, 'GraceW', 'verysecure'),
(8, 'HenryM', 'password99'),
(9, 'IvyT', 'topsecret'),
(10, 'JackA', 'passphrase'),
(11, 'KatieT', 'superpass'),
(12, 'LiamJ', '12345pass'),
(13, 'MiaW', 'my1pass'),
(14, 'NoahH', 'pass1word'),
(15, 'OliviaM', 'thebestpass');
SET IDENTITY_INSERT Users OFF

DELETE FROM CustomerData;
SET IDENTITY_INSERT CustomerData ON
INSERT INTO CustomerData (Id, OfUserId, Email, Phone) VALUES
(1, 1, 'alice.smith@email.com', '555-1001'),
(2, 2, 'bob.johnson@email.com', '555-1002'),
(3, 3, 'charlie.w@email.com', '555-1003'),
(4, 4, 'david.b@email.com', '555-1004'),
(5, 5, 'emily.d@email.com', '555-1005'),
(6, 6, 'frank.m@email.com', '555-1006'),
(7, 7, 'grace.w@email.com', '555-1007'),
(8, 8, 'henry.m@email.com', '555-1008'),
(9, 9, 'ivy.t@email.com', '555-1009'),
(10, 10, 'jack.a@email.com', '555-1010'),
(11, 11, 'katie.t@email.com', '555-1011'),
(12, 12, 'liam.j@email.com', '555-1012'),
(13, 13, 'mia.w@email.com', '555-1013'),
(14, 14, 'noah.h@email.com', '555-1014'),
(15, 15, 'olivia.m@email.com', '555-1015'),
(16, NULL, 'jane.doe@anon.com', '555-2001'),
(17, NULL, 'john.smith123@tempmail.net', '555-2002'),
(18, NULL, 'sarah.jones@mailinator.com', '555-2003'),
(19, NULL, 'mike.brown89@yopmail.com', '555-2004'),
(20, NULL, 'emily.green_test@guerrillamail.com', '555-2005'),
(21, NULL, 'david.white.unreg@example.net', '555-2006'),
(22, NULL, 'jessica.black.guest@fake.email', '555-2007'),
(23, NULL, 'kevin.lee.noaccount@null.email', '555-2008'),
(24, NULL, 'ashley.young.temp@discardmail.com', '555-2009'),
(25, NULL, 'brian.hill.anonuser@inbox.com', '555-2010'),
(26, NULL, 'megan.king.notreal@nonexistent.com', '555-2011'),
(27, NULL, 'justin.wright.unverified@void.email', '555-2012'),
(28, NULL, 'rachel.lopez.shadow@ghost.mail', '555-2013'),
(29, NULL, 'adam.scott.unknown@mystery.box', '555-2014'),
(30, NULL, 'lauren.turner.invisible@nowhere.net', '555-2015'),
(31, NULL, 'brandon.phillips.secretid@hidden.com', '555-2016'),
(32, NULL, 'nicole.mitchell.anonymous@private.net', '555-2017'),
(33, NULL, 'samuel.roberts.cloak@masked.email', '555-2018'),
(34, NULL, 'stephanie.carter.alias@pseudonym.com', '555-2019'),
(35, NULL, 'ryan.collins.incognito@undetected.net', '555-2020'),
(36, NULL, 'heather.bailey.unnamed@nameless.email', '555-2021'),
(37, NULL, 'joshua.reed.xuser@crossedout.com', '555-2022'),
(38, NULL, 'amanda.howard.deleted@wiped.net', '555-2023'),
(39, NULL, 'matthew.ward.erased@blank.mail', '555-2024'),
(40, NULL, 'melissa.cox.vanished@gone.email', '555-2025'),
(41, NULL, 'patrick.diaz.missing@lost.com', '555-2026'),
(42, NULL, 'elizabeth.richardson.noway@unreachable.net', '555-2027'),
(43, NULL, 'christopher.wood.neverhere@never.email', '555-2028'),
(44, NULL, 'victoria.watson.zero@nullpoint.com', '555-2029'),
(45, NULL, 'andrew.brooks.voided@empty.mail', '555-2030'),
(46, NULL, 'angela.kelly.phantom@spirit.net', '555-2031'),
(47, NULL, 'thomas.sanders.wraith@ghostly.com', '555-2032'),
(48, NULL, 'christina.price.shade@shadowy.email', '555-2033'),
(49, NULL, 'jonathan.bennett.specter@haunted.net', '555-2034'),
(50, NULL, 'rebecca.gray.apparition@ethereal.com', '555-2035'),
(51, NULL, 'timothy.james.ethereal@vapor.email', '555-2036'),
(52, NULL, 'sara.reyes.mirage@illusion.net', '555-2037'),
(53, NULL, 'jose.cruz.fata.morgana@desert.mail', '555-2038'),
(54, NULL, 'laura.hughes.oblivion@forgotten.com', '555-2039'),
(55, NULL, 'william.myers.amnesia@memoryless.net', '555-2040'),
(56, NULL, 'deborah.long.blankslate@tabula.rasa', '555-2041'),
(57, NULL, 'daniel.foster.ghostwriter@uncredited.email', '555-2042'),
(58, NULL, 'michelle.morales.echo@reverberation.com', '555-2043'),
(59, NULL, 'nicholas.powell.reflection@mirror.net', '555-2044'),
(60, NULL, 'kimberly.sullivan.doppelganger@twin.email', '555-2045'),
(61, NULL, 'brandon.bell.alternate@parallel.com', '555-2046'),
(62, NULL, 'amber.murray.avatar@impersonator.net', '555-2047'),
(63, NULL, 'benjamin.wells.clone@replica.email', '555-2048'),
(64, NULL, 'katherine.fisher.counterpart@analogue.com', '555-2049'),
(65, NULL, 'jacob.simmons.imposter@fraud.net', '555-2050'),
(66, NULL, 'erica.webb.forgery@fakeid.email', '555-2051'),
(67, NULL, 'alexander.ross.deception@hoax.com', '555-2052'),
(68, NULL, 'julie.owens.ruse@trickery.net', '555-2053'),
(69, NULL, 'adam.stone.subterfuge@stealth.email', '555-2054'),
(70, NULL, 'lori.henderson.camouflage@disguise.com', '555-2055'),
(71, NULL, 'raymond.cole.masquerade@costume.net', '555-2056'),
(72, NULL, 'amy.jenkins.charade@pretense.email', '555-2057'),
(73, NULL, 'jerry.perry.facade@semblance.com', '555-2058'),
(74, NULL, 'tammy.butler.veneer@surface.net', '555-2059'),
(75, NULL, 'keith.barnes.guise@appearance.email', '555-2060');
SET IDENTITY_INSERT CustomerData OFF

DELETE FROM Tickets;
SET IDENTITY_INSERT Tickets ON
INSERT INTO Tickets (Id, OfScreeningId, CustomerId, Price, Seat, ByVendorId) VALUES
(1, 1, 1, 12.50, 'A15', 1),
(2, 1, 2, 10.00, 'B22', 2),
(3, 1, 3, 15.75, 'C5', 3),
(4, 2, 4, 8.25, 'D30', 4),
(5, 2, 5, 14.00, 'E10', 5),
(6, 2, 6, 9.50, 'F28', 1),
(7, 3, 7, 11.25, 'G3', 2),
(8, 3, 8, 16.00, 'H18', 3),
(9, 3, 9, 7.75, 'I45', 4),
(10, 4, 10, 13.50, 'J12', 5),
(11, 4, 11, 8.00, 'K25', 1),
(12, 4, 12, 17.25, 'L8', 2),
(13, 5, 13, 10.75, 'M33', 3),
(14, 5, 14, 15.50, 'N1', 4),
(15, 5, 15, 9.00, 'O20', 5),
(16, 6, 16, 12.00, 'P37', 1),
(17, 6, 17, 8.75, 'Q6', 2),
(18, 6, 18, 16.50, 'R23', 3),
(19, 7, 19, 11.00, 'S40', 4),
(20, 7, 20, 14.50, 'T14', 5),
(21, 7, 21, 9.75, 'U31', 1),
(22, 8, 22, 13.00, 'V4', 2),
(23, 8, 23, 17.00, 'W19', 3),
(24, 8, 24, 8.50, 'X36', 4),
(25, 9, 25, 11.50, 'Y9', 5),
(26, 9, 26, 15.00, 'Z27', 1),
(27, 9, 27, 9.25, 'A2', 2),
(28, 10, 28, 12.75, 'B42', 3),
(29, 10, 29, 18.00, 'C17', 4),
(30, 10, 30, 8.00, 'D34', 5),
(31, 11, 31, 10.50, 'E7', 1),
(32, 11, 32, 14.25, 'F24', 2),
(33, 11, 33, 9.75, 'G48', 3),
(34, 12, 34, 13.75, 'H11', 4),
(35, 12, 35, 17.50, 'I29', 5),
(36, 12, 36, 8.25, 'J16', 1),
(37, 13, 37, 11.75, 'K39', 2),
(38, 13, 38, 15.25, 'L50', 3),
(39, 13, 39, 9.50, 'M13', 4),
(40, 14, 40, 12.25, 'N32', 5),
(41, 14, 41, 18.50, 'O5', 1),
(42, 14, 42, 7.50, 'P21', 2),
(43, 15, 43, 10.25, 'Q46', 3),
(44, 15, 44, 14.75, 'R18', 4),
(45, 15, 45, 9.00, 'S35', 5),
(46, 16, 46, 13.25, 'T1', 1),
(47, 16, 47, 16.75, 'U43', 2),
(48, 16, 48, 8.75, 'V26', 3),
(49, 17, 49, 11.00, 'W15', 4),
(50, 17, 50, 15.50, 'X49', 5),
(51, 17, 51, 9.25, 'Y12', 1),
(52, 18, 52, 12.50, 'Z22', 2),
(53, 18, 53, 17.00, 'A3', 3),
(54, 18, 54, 8.00, 'B30', 4),
(55, 19, 55, 10.75, 'C41', 5),
(56, 19, 56, 14.00, 'D10', 1),
(57, 19, 57, 9.75, 'E28', 2),
(58, 20, 58, 13.50, 'F6', 3),
(59, 20, 59, 16.00, 'G23', 4),
(60, 20, 60, 8.50, 'H37', 5),
(61, 21, 61, 11.25, 'I17', 1),
(62, 21, 62, 14.25, 'J4', 2),
(63, 21, 63, 9.25, 'K26', 3),
(64, 22, 64, 12.00, 'L19', 4),
(65, 22, 65, 17.25, 'M36', 5),
(66, 22, 66, 7.75, 'N11', 1),
(67, 23, 67, 10.50, 'O29', 2),
(68, 23, 68, 15.00, 'P50', 3),
(69, 23, 69, 9.50, 'Q13', 4),
(70, 24, 70, 13.75, 'R32', 5),
(71, 24, 71, 18.00, 'S5', 1),
(72, 24, 72, 8.25, 'T21', 2),
(73, 25, 73, 11.75, 'U46', 3),
(74, 25, 74, 14.50, 'V18', 4),
(75, 25, 75, 9.00, 'W35', 5),
(76, 26, 1, 13.00, 'X1', 1),
(77, 26, 2, 16.50, 'Y43', 2),
(78, 26, 3, 8.75, 'Z26', 3),
(79, 27, 4, 11.00, 'A15', 4),
(80, 27, 5, 15.50, 'B49', 5),
(81, 27, 6, 9.25, 'C12', 1),
(82, 28, 7, 12.50, 'D22', 2),
(83, 28, 8, 17.00, 'E3', 3),
(84, 28, 9, 8.00, 'F30', 4),
(85, 29, 10, 10.75, 'G41', 5),
(86, 29, 11, 14.00, 'H10', 1),
(87, 29, 12, 9.75, 'I28', 2),
(88, 30, 13, 13.50, 'J6', 3),
(89, 30, 14, 16.00, 'K23', 4),
(90, 30, 15, 8.50, 'L37', 5),
(91, 31, 16, 11.25, 'M17', 1),
(92, 31, 17, 14.25, 'N4', 2),
(93, 31, 18, 9.25, 'O26', 3),
(94, 32, 19, 12.00, 'P19', 4),
(95, 32, 20, 17.25, 'Q36', 5),
(96, 32, 21, 7.75, 'R11', 1),
(97, 33, 22, 10.50, 'S29', 2),
(98, 33, 23, 15.00, 'T50', 3),
(99, 33, 24, 9.50, 'U13', 4),
(100, 34, 25, 13.75, 'V32', 5),
(101, 34, 26, 18.00, 'W5', 1),
(102, 34, 27, 8.25, 'X21', 2),
(103, 35, 28, 11.75, 'Y46', 3),
(104, 35, 29, 14.50, 'Z18', 4),
(105, 35, 30, 9.00, 'A35', 5),
(106, 36, 31, 13.00, 'B1', 1),
(107, 36, 32, 16.50, 'C43', 2),
(108, 36, 33, 8.75, 'D26', 3),
(109, 37, 34, 11.00, 'E15', 4),
(110, 37, 35, 15.50, 'F49', 5),
(111, 37, 36, 9.25, 'G12', 1),
(112, 38, 37, 12.50, 'H22', 2),
(113, 38, 38, 17.00, 'I3', 3),
(114, 38, 39, 8.00, 'J30', 4),
(115, 39, 40, 10.75, 'K41', 5),
(116, 39, 41, 14.00, 'L10', 1),
(117, 39, 42, 9.75, 'M28', 2),
(118, 40, 43, 13.50, 'N6', 3),
(119, 40, 44, 16.00, 'O23', 4),
(120, 40, 45, 8.50, 'P37', 5),
(121, 41, 46, 11.25, 'Q17', 1),
(122, 41, 47, 14.25, 'R4', 2),
(123, 41, 48, 9.25, 'S26', 3),
(124, 42, 49, 12.00, 'T19', 4),
(125, 42, 50, 17.25, 'U36', 5),
(126, 42, 51, 7.75, 'V11', 1),
(127, 43, 52, 10.50, 'W29', 2),
(128, 43, 53, 15.00, 'X50', 3),
(129, 43, 54, 9.50, 'Y13', 4),
(130, 44, 55, 13.75, 'Z32', 5),
(131, 44, 56, 18.00, 'A5', 1),
(132, 44, 57, 8.25, 'B21', 2),
(133, 45, 58, 11.75, 'C46', 3),
(134, 45, 59, 14.50, 'D18', 4),
(135, 45, 60, 9.00, 'E35', 5),
(136, 46, 61, 13.00, 'F1', 1),
(137, 46, 62, 16.50, 'G43', 2),
(138, 46, 63, 8.75, 'H26', 3),
(139, 47, 64, 11.00, 'I15', 4),
(140, 47, 65, 15.50, 'J49', 5),
(141, 47, 66, 9.25, 'K12', 1),
(142, 48, 67, 12.50, 'L22', 2),
(143, 48, 68, 17.00, 'M3', 3),
(144, 48, 69, 8.00, 'N30', 4),
(145, 49, 70, 10.75, 'O41', 5),
(146, 49, 71, 14.00, 'P10', 1),
(147, 49, 72, 9.75, 'Q28', 2),
(148, 50, 73, 13.50, 'R6', 3),
(149, 50, 74, 16.00, 'S23', 4),
(150, 50, 75, 8.50, 'T37', 5),
(151, 51, 1, 11.25, 'U17', 1),
(152, 51, 2, 14.25, 'V4', 2),
(153, 51, 3, 9.25, 'W26', 3),
(154, 52, 4, 12.00, 'X19', 4),
(155, 52, 5, 17.25, 'Y36', 5),
(156, 52, 6, 7.75, 'Z11', 1),
(157, 53, 7, 10.50, 'A29', 2),
(158, 53, 8, 15.00, 'B50', 3),
(159, 53, 9, 9.50, 'C13', 4),
(160, 54, 10, 13.75, 'D32', 5),
(161, 54, 11, 18.00, 'E5', 1),
(162, 54, 12, 8.25, 'F21', 2),
(163, 55, 13, 11.75, 'G46', 3),
(164, 55, 14, 14.50, 'H18', 4),
(165, 55, 15, 9.00, 'I35', 5),
(166, 56, 16, 13.00, 'J1', 1),
(167, 56, 17, 16.50, 'K43', 2),
(168, 56, 18, 8.75, 'L26', 3),
(169, 1, 19, 11.00, 'M15', 4),
(170, 2, 20, 15.50, 'N49', 5),
(171, 3, 21, 9.25, 'O12', 1),
(172, 4, 22, 12.50, 'P22', 2),
(173, 5, 23, 17.00, 'Q3', 3),
(174, 6, 24, 8.00, 'R30', 4),
(175, 7, 25, 10.75, 'S41', 5),
(176, 8, 26, 14.00, 'T10', 1),
(177, 9, 27, 9.75, 'U28', 2),
(178, 10, 28, 13.50, 'V6', 3),
(179, 11, 29, 16.00, 'W23', 4),
(180, 12, 30, 8.50, 'X37', 5),
(181, 13, 31, 11.25, 'Y17', 1),
(182, 14, 32, 14.25, 'Z4', 2),
(183, 15, 33, 9.25, 'A26', 3),
(184, 16, 34, 12.00, 'B19', 4),
(185, 17, 35, 17.25, 'C36', 5),
(186, 18, 36, 7.75, 'D11', 1),
(187, 19, 37, 10.50, 'E29', 2),
(188, 20, 38, 15.00, 'F50', 3),
(189, 21, 39, 9.50, 'G13', 4),
(190, 22, 40, 13.75, 'H32', 5),
(191, 23, 41, 18.00, 'I5', 1),
(192, 24, 42, 8.25, 'J21', 2),
(193, 25, 43, 11.75, 'K46', 3),
(194, 26, 44, 14.50, 'L18', 4),
(195, 27, 45, 9.00, 'M35', 5),
(196, 28, 46, 13.00, 'N1', 1),
(197, 29, 47, 16.50, 'O43', 2),
(198, 30, 48, 8.75, 'P26', 3),
(199, 31, 49, 11.00, 'Q15', 4),
(200, 32, 50, 15.50, 'R49', 5);
SET IDENTITY_INSERT Tickets OFF