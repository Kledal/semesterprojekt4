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
            var printerList = new List<object>();
            foreach (var printer in PrinterManager.Read())
            {
                printerList.Add(new {key = printer.Id, label = printer.Name});
            }
            ViewBag.printers = printerList;

            var bookingList = new List<object>();
            foreach (var booking in BookingManager.Read())
            {
                bookingList.Add(new { text = booking.User.FirstName +" "+ booking.User.LastName, start_date = booking.StartTime.ToString("g"), end_date = booking.EndTime.ToString("g"), printer_id = booking.PrinterId });
            }
            ViewBag.bookings = bookingList;
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
