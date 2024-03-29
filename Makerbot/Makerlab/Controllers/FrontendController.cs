﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Makerlab.DAL;
using Makerlab.Extensions;

namespace Makerlab.Controllers
{
    public class FrontendController : ApplicationController
    {
        private MakerContext db = new MakerContext();
        // GET: /Frontend/
        public ActionResult Index()
        {
            var printerList = new List<object>();
            // db.Printers.Where(printer => printer.IsBookable)
            foreach (var printer in PrinterManager.Read().Where(printer => printer.IsBookable) )
            {
                  printerList.Add(new {key = printer.Id, label = printer.Name}); 
            }
            ViewBag.printers = printerList;

            var bookingList = new List<object>();
            foreach (var booking in BookingManager.Read())
            {
                bookingList.Add(new { text = booking.User.FirstName +" "+ booking.User.LastName,
                                      start_date = booking.StartTime.ToString("MM-dd-yyyy HH:mm:ss"),
                                      end_date = booking.EndTime.ToString("MM-dd-yyyy HH:mm:ss"),
                    printer_id = booking.PrinterId
                });
            }
            ViewBag.bookings = bookingList;
            return View();
        }

        [Auth("Godkendt Bruger", "Administrator")]
        public ActionResult MyBookings()
        {
            ViewBag.HistoryBookings = db.Bookings.Where(b => b.UserId == CurrentUser.Id && b.StartTime < DateTime.Now).ToList();
            return View(db.Bookings.Where(b => b.UserId == CurrentUser.Id && b.EndTime > DateTime.Now).ToList());
        }

        public ActionResult LogInd()
        {
            return View("LogInd");
        }

        public ActionResult NewBooking()
        {
            return View("NewBooking");
        }

        public ActionResult Printers()
        {
            return View("Printers",PrinterManager.Read());
        }
    }
}
