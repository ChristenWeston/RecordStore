using System.Collections.Generic;

namespace RecordStore.Models
{
  public class Genre
  {
    public Genre()
    {
      this.Albums = new HashSet<GenreAlbumSong>();
    }
    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public virtual ICollection<GenreAlbumSong> Albums { get; set; }
  }
}