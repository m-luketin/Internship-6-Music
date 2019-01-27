using System;
using System.Collections.Generic;
using System.Text;

namespace Internship_6_Music.Models
{
    class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
