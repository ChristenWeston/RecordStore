using System.Collections.Generic;

namespace RecordStore.Models
{
  public class Album
  {
    public Album()
    {
      this.JoinEntries = new HashSet<GenreAlbumSong>();
    }
    public int AlbumId { get; set; }
    public string AlbumName { get; set; }
    public string ArtistName { get; set; }
    public string Year { get; set; }
    public virtual ICollection<GenreAlbumSong> JoinEntries { get; set; }// songs
  }
}