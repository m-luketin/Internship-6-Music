using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Internship_6_Music.Models;

namespace Internship_6_Music
{
    class Program
    {
        static void Main()
        {
            var connectionString =
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Music;" +
                "Integrated Security=true;MultipleActiveResultSets=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                var musiciansList = connection.Query<Musician>("SELECT * FROM Musicians").ToList();
                var songsList = connection.Query<Song>("SELECT * FROM Songs").ToList();
                var albumList = connection.Query<Album>("SELECT * FROM Albums").ToList();
                var relationList = connection.Query<Relation_Song_Album>("SELECT * FROM Relation_Song_Album").ToList();

                //1 - musicians by name
                Console.WriteLine("Musicians by name:");
                foreach (var musician in musiciansList.OrderBy(musician => musician.Name))
                {
                    Console.WriteLine($"{musician.MusicianId} {musician.Name} {musician.Nationality}");
                }
                Console.WriteLine();

                //2 - musicians by inputted nationality
                Console.WriteLine("Input artist nationality:");
                var inputtedNationality = Console.ReadLine();
                var musiciansByNationality = from musician in musiciansList where musician.Nationality == inputtedNationality select musician;

                foreach (var musician in musiciansByNationality)
                {
                    Console.WriteLine($"{musician.Name} {musician.Nationality}");
                }

                Console.WriteLine();

                //3 albums grouped by year, with artists
                Console.WriteLine("Albums by year:");
                var albumsWithArtist = from album in albumList
                    join musician in musiciansList on album.FK_Musician equals musician.MusicianId
                    select new {Year = album.YearOfRelease, album.Name, Artist = musician.Name};

                var albumsByYearWithArtist = from album in albumsWithArtist group album by album.Year;

                foreach (var year in albumsByYearWithArtist)
                {
                    Console.WriteLine($"{year.Key}");
                    foreach (var album in year)
                    {
                        Console.WriteLine($"  {album.Artist} - {album.Name} ");
                    }
                }

                Console.WriteLine();

                //4 - albums by inputted part of name
                Console.WriteLine("Enter part of album name:");
                var inputAlbum = Console.ReadLine();

                var searchedAlbums = from album in albumList where album.Name.Contains(inputAlbum) select album;
                foreach (var album in searchedAlbums)
                {
                    Console.WriteLine($"{album.AlbumId} {album.Name} {album.YearOfRelease}"); 
                }

                Console.WriteLine();

                //5 - duration of all albums
                Console.WriteLine("Album duration:");
                var albumsWithDuration = from album in albumList
                    join relation in relationList on album.AlbumId equals relation.AlbumId
                    join song in songsList on relation.SongId equals song.SongId
                    select new {album.Name, song.Duration};

                var durationsByAlbum = from album in albumsWithDuration group album by album.Name;

                foreach (var album in durationsByAlbum)
                {
                    var totalDuration = new TimeSpan(0, 0, 0);

                    foreach (var duration in album)
                    {
                        totalDuration += duration.Duration;
                    }

                    totalDuration = totalDuration.Divide(60);
                    Console.WriteLine($"{album.Key} - {totalDuration}");
                    
                }
                Console.WriteLine();

                //6 - albums by song
                Console.WriteLine("Enter part of song name:");
                var songInput = Console.ReadLine();
                
                var songsMatching = from song in songsList where song.Name.Contains(songInput) select song;

                if (songsMatching.FirstOrDefault() != null && songInput != "")
                {
                    var songChosen = songsMatching.FirstOrDefault();

                    var albumsBySong = from album in albumList
                        join relation in relationList on album.AlbumId equals relation.AlbumId
                        join song in songsList on relation.SongId equals song.SongId
                        where song.Name == songChosen.Name
                        select new { song, album.Name };

                    foreach (var album in albumsBySong)
                    {
                        Console.WriteLine(album.Name);
                    }
                }
                else
                {
                    Console.WriteLine("No such song");
                }

                Console.WriteLine();

                //7 - songs by a selected artist, on albums after a selected year
                Console.WriteLine("Enter artist name");
                var artistInput = Console.ReadLine();
                Console.WriteLine("Enter year:");
                var yearInput = Console.ReadLine();

                if(artistInput == "" || yearInput == "")
                    Console.WriteLine("Input invalid");
                else
                {
                    var year = int.Parse(yearInput);

                    var songsByArtistAndYear = from song in songsList
                        join relation in relationList on song.SongId equals relation.SongId
                        join album in albumList on relation.AlbumId equals album.AlbumId
                        join musician in musiciansList on album.FK_Musician equals musician.MusicianId
                        where musician.Name == artistInput && album.YearOfRelease > year
                        select new { Musician = musician.Name, song.Name, Year = album.YearOfRelease };

                    var groupedSongs = from song in songsByArtistAndYear group song by song.Musician;

                    foreach (var group in groupedSongs)
                    {
                        Console.WriteLine($"{group.Key}");
                        foreach (var song in group)
                        {
                            Console.WriteLine($"{song.Name} - {song.Musician} - {song.Year}");
                        }
                    }
                }
                

                Console.WriteLine();
            }
        }
    }
}
