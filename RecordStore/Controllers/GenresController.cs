using Microsoft.AspNetCore.Mvc;
using RecordStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RecordStore.Controllers
{
  public class GenresController : Controller
  {
    private readonly RecordStoreContext _db;

    public GenresController(RecordStoreContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Genre> model = _db.Genres.ToList();
      return View(model);
    }
      public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Genre genre)
    {
      _db.Genres.Add(genre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Genre thisGenre = _db.Genres
        .Include(genre => genre.Albums)
        .ThenInclude(join => join.Album)
        .FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);  
    }

    public ActionResult Edit(int id)
    {
      Genre thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);
    }

    [HttpPost]
    public ActionResult Edit(Genre genre)
    {
      _db.Entry(genre).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
      // return RedirectToAction("Details", new { id = genre.GenreId});
    }
    
    public ActionResult Delete(int id)
    {
      Genre thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Genre thisGenre = _db.Genres.FirstOrDefault(genre => genre.GenreId == id);
      List<GenreAlbumSong> joins = _db.GenreAlbumSongs.Where(join => join.GenreId == id).ToList();
      foreach (GenreAlbumSong join in joins)
      {
        _db.GenreAlbumSongs.Remove(join);
      }
      _db.Genres.Remove(thisGenre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}  