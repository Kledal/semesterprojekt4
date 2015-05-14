using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makerlab.DAL;
using Makerlab.Extensions;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class DashboardController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Users()
        {
            return View("Users", UserManager.Read());
        }

        public ActionResult Printers()
        {
            return View("Printers", PrinterManager.Read());
        }

        public ActionResult Userroles()
        {
            return View("Userroles", db.UserRoles.ToList());
        }

        public ActionResult Messages()
        {
            return View("Messages");
        }
    }
}
