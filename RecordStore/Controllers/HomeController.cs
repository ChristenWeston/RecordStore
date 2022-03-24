using Microsoft.AspNetCore.Mvc;

namespace RecordStore.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}