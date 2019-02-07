using System;
using System.Collections.Generic;

namespace Internship_6_Music.Models
{
    public class Song
    {
        public Song()
        {
            SongOnAlbums = new List<RelationSongAlbum>();
        }
        public int SongId { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public List<RelationSongAlbum> SongOnAlbums { get; set; }
    }
}
