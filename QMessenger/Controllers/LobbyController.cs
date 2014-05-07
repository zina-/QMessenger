using System.Web.Mvc;

namespace QMessenger.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        // GET: Lobby
        public ActionResult Index()
        {
            return View();
        }
    }
}