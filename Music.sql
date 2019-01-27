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