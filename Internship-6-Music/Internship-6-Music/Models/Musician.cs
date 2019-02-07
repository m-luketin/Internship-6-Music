using System;
using System.Collections.Generic;
using System.Text;

namespace Internship_6_Music.Models
{
    public class Musician
    {
        public Musician()
        {
            Albums = new List<Album>();
        }
        public int MusicianId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<Album> Albums { get; set; }
    }
}
