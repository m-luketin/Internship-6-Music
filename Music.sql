CREATE DATABASE Music

BEGIN TRANSACTION 
 
CREATE TABLE Musicians(
	MusicianId int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(30),
	Nationality nvarchar(20)
)

CREATE TABLE Albums( 
	AlbumId int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(30),
	YearOfRelease int,
	FK_Musician int,
	FOREIGN KEY (FK_Musician) REFERENCES Musicians(MusicianId)
) 

CREATE TABLE Songs(
	SongId int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(30),
	Duration time
)

CREATE TABLE Relation_Song_Album(
	AlbumId int FOREIGN KEY REFERENCES Albums(AlbumId),
	SongId int FOREIGN KEY REFERENCES Songs(SongId)
)


COMMIT TRANSACTION

BEGIN TRANSACTION 

INSERT INTO dbo.Musicians
(
    Name,
    Nationality
)
VALUES
('Eminem', 'American'), ('Ed Sheeran', 'British'), ('Logic', 'American');

INSERT INTO dbo.Songs
(
    Name,
    Duration
)
VALUES
('When I''m gone', '3:25'), ('Lose yourself', '3:55'), ('Role model', '2:22'), ('Sing for the moment', '3:10'), ('Without me', '3:20'),
('Not alike', '3:23'), ('Kamikaze', '3:51'), ('Headshot', '2:22'), ('Lucky you', '3:10'), ('Fack', '2:21'),
('You need me', '2:25'), ('I see fire', '2:45'), ('Thinking out loud', '3:28'), ('Lego house', '2:15'), ('The A team', '4:25'),
('Eraser', '4:15'), ('Supermarket flowers', '2:39'), ('New man', '3:35'), ('Shape of you', '2:26'), ('Dive', '2:46'),
('Overnight', '2:20'), ('Yuck', '3:50'), ('Contra', '3:30'), ('Warm it up', '2:50'), ('Wizard of Oz', '3:05'),
('Wu Tang Forever', '2:52'), ('Iconic', '3:23'),('YSIV', '3:50'), ('Everybody dies', '3:50'), ('The return', '3:32');

INSERT INTO dbo.Albums
(
    Name,
    YearOfRelease,
    FK_Musician
)
VALUES
('Slim shady EP', 2008, 1), ('Kamikaze', 2018, 1),
('Plus', 2011, 2), ('Divide', 2016, 2),
('Bobby Tarantino II', 2018, 3), ('Young Sinatra IV', 2019, 3);

INSERT INTO dbo.Relation_Song_Album
(
    AlbumId,
    SongId
)
VALUES
(1,1),(1,2),(1,3),(1,4),(1,5),(2,6),(2,7),(2,8),(2,9),(2,10),(3,11),(3,12),(3,13),(3,14),(3,15),
(4,16),(4,17),(4,18),(4,19),(4,20),(5,21),(5,22),(5,23),(5,24),(5,25),(6,26),(6,27),(6,28),(6,29),(6,30);

COMMIT TRANSACTION 