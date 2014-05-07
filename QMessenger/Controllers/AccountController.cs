using QMessenger.Repository;
using System.Web.Mvc;
using System.Web.Security;

namespace QMessenger.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Lobby");
            }
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult DoLogin(string userId, string passwordHash)
        {
            if (ModelState.IsValid)
            {
                using (RemoteMongoRepository remoteRepo = new RemoteMongoRepository())
                {
                    Models.User user = remoteRepo.GetUserByIdAndPasswordHash(userId, passwordHash);

                    if (user != null)
                    {
                        if (LocalCacheRepository.Instance.TryRepositoryAdd(userId, user))
                        {
                            FormsAuthentication.SetAuthCookie(userId, true);
                        }
                        return RedirectToAction("Index", "Lobby");
                    }
                    else
                    {
                        return View();
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }
    }
}