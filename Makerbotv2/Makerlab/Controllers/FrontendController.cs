using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Services;
using System.Security.Claims;
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
            var users = UserManager.Read();

            var u = this.CurrentUser;
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
