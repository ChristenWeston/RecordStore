using Microsoft.AspNetCore.Mvc;
using RecordStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecordStore.Controllers
{
  public class SongsController : Controller
  {
    private readonly RecordStoreContext _db;

    public SongsController(RecordStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Songs.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Song song)
    {
      _db.Songs.Add(song);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisSong= _db.Songs
          .Include(song => song.Albums)
          .ThenInclude(join => join.Album)
          .FirstOrDefault(song => song.SongId == id);
      return View(thisSong);
    }

    public ActionResult Edit(int id)
    {
      var thisSong= _db.Songs.FirstOrDefault(songs => songs.SongId == id);
      ViewBag.AlbumId = new SelectList(_db.Albums, "AlbumId", "Description");
      return View(thisSong);
    }

    [HttpPost]
    public ActionResult Edit(Song song, int AlbumId)
    {
      if (AlbumId != 0)
      {
        _db.GenreAlbumSongs.Add(new GenreAlbumSong() { AlbumId = AlbumId, SongId = song.SongId });
      }
      _db.Entry(song).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAlbum(int id)
    {
      var thisSong= _db.Songs.FirstOrDefault(songs => songs.SongId == id);
      ViewBag.AlbumId = new SelectList(_db.Albums, "AlbumId", "Description");
      return View(thisSong);
    }

    [HttpPost]
    public ActionResult AddAlbum(Song song, int AlbumId)
    {
      if (AlbumId != 0)
      {
        _db.GenreAlbumSongs.Add(new GenreAlbumSong() { AlbumId = AlbumId, SongId = song.SongId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisSong = _db.Songs.FirstOrDefault(songs => songs.SongId == id);
      return View(thisSong);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisSong= _db.Songs.FirstOrDefault(songs => songs.SongId == id);
      // Because SongId is set to a nullable type in our join entity, 
      // deleting a Songdoesn't cause expected "cascade delete" 
      // behavior where all associated join relationships with that 
      // Songare also deleted. In fact, cascade delete only works 
      // automatically with non-nullable type. So, we must manually 
      // find all join entities with the same SongId and delete them. 
      List<GenreAlbumSong> joins = _db.GenreAlbumSongs.Where(join => join.SongId == id).ToList();
      foreach ( GenreAlbumSong join in joins)
      {
        _db.GenreAlbumSongs.Remove(join);
      }
      _db.Songs.Remove(thisSong);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAlbum(int joinId)
    {
      var joinEntry = _db.GenreAlbumSongs.FirstOrDefault(entry => entry.GenreAlbumSongId == joinId);
      _db.GenreAlbumSongs.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}