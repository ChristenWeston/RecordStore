using Microsoft.EntityFrameworkCore;

namespace RecordStore.Models
{
  public class RecordStoreContext : DbContext
  {
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<GenreAlbumSong> GenreAlbumSongs { get; set; }

    public RecordStoreContext(DbContextOptions options) : base(options) { }
  }
}