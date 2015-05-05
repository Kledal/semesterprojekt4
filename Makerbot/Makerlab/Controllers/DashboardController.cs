using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Makerlab.DAL;
using Makerlab.Extensions;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class DashboardController : ApplicationController
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Printers()
        {
            return View(PrinterManager.Read());
        }

        //
        // GET: /Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
