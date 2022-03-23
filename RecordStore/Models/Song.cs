using System.Collections.Generic;

namespace RecordStore.Models
{
  public class Song
  {
    public Song()
    {
      this.Albums = new HashSet<GenreAlbumSong>();
    }
    public int SongId { get; set; }
    public string SongName { get; set; }
    public virtual ICollection<GenreAlbumSong> Albums { get; set; }
  }
}