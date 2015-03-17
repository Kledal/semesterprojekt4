using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Makerbot.Models;

namespace Makerbot.Controllers
{
    public class FrontendController : Controller
    {
        //
        // GET: /Frontend/

        public ActionResult Index()
        {
            var count = 0;
            using (var db = new UserContainer())
            {
                count = db.User.Count();
            }

            return View();
        }

    }
}
