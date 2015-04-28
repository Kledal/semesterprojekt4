using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Services;
using System.IO;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Makerlab.DAL;
using Makerlab.Models;
using ModelState = System.Web.ModelBinding.ModelState;

namespace Makerlab.Controllers
{
    public class FrontendController : ApplicationController
    {  
        // GET: /Frontend/
        public ActionResult Index()
        {
            //var users = UserManager.Read();
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

        public ActionResult NewBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewBooking(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/App_Data/s3gfiler"), fileName);
                file.SaveAs(path);
            }
            return RedirectToAction("NewBooking");
        }
    }
}
