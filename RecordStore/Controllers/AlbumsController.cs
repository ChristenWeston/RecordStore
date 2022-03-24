// Album = Item
using Microsoft.AspNetCore.Mvc;
using RecordStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecordStore.Controllers
{
  public class AlbumsController : Controller
  {
    private readonly RecordStoreContext _db;

    public AlbumsController(RecordStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Albums.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "GenreName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Album album, int GenreId)
    {
      _db.Albums.Add(album);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Album thisAlbum = _db.Albums
          .Include(album => album.JoinEntries)
          .ThenInclude(join => join.Song)
          .Include(album => album.JoinEntries)
          .ThenInclude(join => join.Genre)
          .FirstOrDefault(album => album.AlbumId == id);
      return View(thisAlbum);
    }

    public ActionResult Edit(int id)
    {
      Album thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "Name");
      return View(thisAlbum);
    }

    [HttpPost]
    public ActionResult Edit(Album album)
    {
      _db.Entry(album).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddGenre(int id)
    {
      Album thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "Name");
      return View(thisAlbum);
    }

    [HttpPost]
    public ActionResult AddGenre(Album album, int GenreId)
    {
      if (GenreId != 0)
      {
        _db.GenreAlbumSongs.Add(new GenreAlbumSong() { GenreId = GenreId, AlbumId = album.AlbumId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = album.AlbumId });
    }

    public ActionResult AddSong(int id)
    {
      Album thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      ViewBag.TagId = new SelectList(_db.Songs, "SongId", "SongName");
      return View(thisAlbum);
    }

    [HttpPost]
    public ActionResult AddSong(Album album, int SongId)
    {
      if (SongId != 0)
      {
        _db.GenreAlbumSongs.Add(new GenreAlbumSong() { SongId = SongId, AlbumId = album.AlbumId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = album.AlbumId });
    }

    public ActionResult Delete(int id)
    {
      Album thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      return View(thisAlbum);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Album thisAlbum = _db.Albums.FirstOrDefault(albums => albums.AlbumId == id);
      _db.Albums.Remove(thisAlbum);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      GenreAlbumSong joinEntries = _db.GenreAlbumSongs.FirstOrDefault(entry => entry.GenreAlbumSongId == joinId);
      _db.GenreAlbumSongs.Remove(joinEntries);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}