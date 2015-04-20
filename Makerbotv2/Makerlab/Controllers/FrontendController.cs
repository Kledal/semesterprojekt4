using System.Diagnostics;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.WebPages;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    public class FrontendController : ApplicationController
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

        public ActionResult NewBooking()
        {
            return View();
        }
    }
}
