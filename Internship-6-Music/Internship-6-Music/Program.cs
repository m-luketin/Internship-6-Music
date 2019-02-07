using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Internship_6_Music.Models;

namespace Internship_6_Music
{
    internal class Program
    {
        private static void Main()
        {
            var connectionString =
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Music;" +
                "Integrated Security=true;MultipleActiveResultSets=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                var musiciansList = connection.Query<Musician>("SELECT * FROM Musicians").ToList();
                var songsList = connection.Query<Song>("SELECT * FROM Songs").ToList();
                var albumList = connection.Query<Album>("SELECT * FROM Albums").ToList();
                var relationList = connection.Query<RelationSongAlbum>("SELECT * FROM Relation_Song_Album").ToList();

                foreach (var musician in musiciansList)
                {
                    foreach (var album in albumList.Where(album =>  musician.MusicianId == album.FkMusician))
                    {
                        album.Musician = musician;
                        musician.Albums.Add(album);
                    }
                }

                foreach (var relation in relationList)
                {
                    foreach (var song in songsList.Where(song => song.SongId == relation.SongId).ToList())
                    {
                        relation.Song = song;
                        song.SongOnAlbums.Add(relation);
                    }
                }

                foreach (var relation in relationList)
                {

                    foreach (var album in albumList.Where(album => album.AlbumId == relation.AlbumId))
                    {
                        relation.Album = album;
                        album.SongOnAlbums.Add(relation);
                    }
                }

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

                foreach (var musician in musiciansList.Where(musician => musician.Nationality == inputtedNationality))
                {
                    Console.WriteLine($"{musician.Name} {musician.Nationality}");
                }

                Console.WriteLine();

                //3 albums grouped by year, with artists
                Console.WriteLine("Albums by year:");

                foreach (var year in albumList.GroupBy(album => album.YearOfRelease))
                {
                    Console.WriteLine($"{year.Key}");
                    foreach (var album in year)
                    {
                        Console.WriteLine($"  {album.Name} - {album.Musician.Name}");
                    }    
                
                }

                Console.WriteLine();

                //4 - albums by inputted part of name
                Console.WriteLine("Enter part of album name:");
                var inputAlbum = Console.ReadLine();
                
                Console.WriteLine();

                //5 - duration of all albums
                Console.WriteLine("Album duration:");
                
                Console.WriteLine();

                //6 - albums by song
                Console.WriteLine("Enter part of song name:");
                var songInput = Console.ReadLine();
                
                
                Console.WriteLine();

                //7 - songs by a selected artist, on albums after a selected year
                Console.WriteLine("Enter artist name");
                var artistInput = Console.ReadLine();
                Console.WriteLine("Enter year:");
                var yearInput = Console.ReadLine();

                
                Console.WriteLine();
            }
        }
    }
}
