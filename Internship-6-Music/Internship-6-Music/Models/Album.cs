using System;
using System.Collections.Generic;
using System.Text;

namespace Internship_6_Music.Models
{
    public class Album
    {
        public Album()
        {
            SongOnAlbums = new List<RelationSongAlbum>();
            Musician = new Musician();
        }
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public int FkMusician { get; set; }
        public Musician Musician { get; set; }
        public List<RelationSongAlbum> SongOnAlbums { get; set; }
    }
}
