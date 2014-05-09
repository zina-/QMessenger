using QMessenger.Repository;
using System.Web.Mvc;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMessenger.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Lobby");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userId, string passwordHash)
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
        public JsonResult LoginAjax(string emailId, string passwordHash)
        {
            if (ModelState.IsValid)
            {
                using (RemoteMongoRepository remoteRepo = new RemoteMongoRepository())
                {
                    Models.User user = remoteRepo.GetUserByIdAndPasswordHash(emailId, passwordHash);

                    if (user != null)
                    {
                        if (LocalCacheRepository.Instance.TryRepositoryAdd(emailId, user))
                        {
                            FormsAuthentication.SetAuthCookie(emailId, true);
                        }
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
            }

            return Json(false);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }
    }
}