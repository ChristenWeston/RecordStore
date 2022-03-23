namespace RecordStore.Models
{
  public class GenreAlbumSong
  {
    public int GenreAlbumSongId { get; set; }
    public int? GenreId { get; set ; }
    public int? AlbumId { get; set; }
    public int? SongId { get; set; }

    public Genre Genre { get; set; }
    public Album Album { get; set; }
    public Song Song { get; set; }
  }
}