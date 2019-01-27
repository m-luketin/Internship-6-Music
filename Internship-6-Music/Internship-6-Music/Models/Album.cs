using System;
using System.Collections.Generic;
using System.Text;

namespace Internship_6_Music.Models
{
    class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public int FK_Musician { get; set; }
    }
}
