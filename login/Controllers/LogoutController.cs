using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace login.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login","LoginId");
        }
    }
}