using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makerlab.DAL;

namespace Makerlab.Controllers
{
    public class FrontendController : Controller
    {  
        // GET: /Frontend/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyBookings()
        {
            return View();
        }

        public ActionResult LogInd()
        {
            return View();
        }
    }
}
