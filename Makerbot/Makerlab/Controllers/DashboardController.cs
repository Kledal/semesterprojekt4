﻿using System;
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

        public ActionResult Messages()
        {
            return View();
        }

        // GET: /Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
