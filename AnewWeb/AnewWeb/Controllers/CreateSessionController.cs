using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnewWeb.Models;

namespace AnewWeb.Controllers
{
    public class CreateSessionController : Controller
    {
        //
        // GET: /CreateSession/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SessionKey()
        {
            SessionKey sessionKey = new SessionKey();
            return View(sessionKey);
        }

        [HttpPost]
        public ActionResult SessionKey(SessionKey model)
        {
            if (ModelState.IsValid)
                ViewBag.Message = model.getSessionKey();
            return View();
        }
    }
}
