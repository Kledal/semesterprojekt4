using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    public class FrontendController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: /Frontend/
        public ActionResult Index()
        {
            //var user = db.User.Include(u => u.UserRole);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
