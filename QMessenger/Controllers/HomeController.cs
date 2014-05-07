using System.Web.Mvc;

namespace QMessenger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Lobby");
        }
    }
}