using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Internship_6_Music.Models;

namespace Internship_6_Music
{
    class Program
    {
        static void Main(string[] args)
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




            }
        }
    }
}
