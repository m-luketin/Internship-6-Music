using System;
using System.Collections.Generic;
using System.Text;

namespace Internship_6_Music.Models
{
    public class RelationSongAlbum
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
