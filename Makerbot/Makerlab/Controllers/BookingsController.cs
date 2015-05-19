using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Makerlab.Models;
using File = Makerlab.Models.File;

namespace Makerlab.Controllers
{
    public class BookingsController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        // GET: Bookings
        public async Task<ActionResult> Index()
        {
            var bookings = db.Bookings.Include(b => b.File).Include(b => b.Printer).Include(b => b.User);
            return View(await bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.PrinterId = new SelectList(db.Printers, "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StartTime,EndTime,PrinterId")] Booking booking)
        {
            booking.UserId = CurrentUser.Id;

            var overlappingBookings = 
                db.Bookings.Where(b => b.StartTime < booking.EndTime && booking.StartTime < b.EndTime).ToList();

            if (overlappingBookings.Count > 0)
            {
                ModelState.AddModelError("StartTime", "Din valgte tid er optaget.");
                ModelState.AddModelError("EndTime", "Din valgte tid er optaget.");
            }

            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                await db.SaveChangesAsync();
                return RedirectToAction("MyBookings", "Frontend");
            }

            ViewBag.PrinterId = new SelectList(db.Printers, "Id", "Name", booking.PrinterId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileId = new SelectList(db.Files, "Id", "FileName", booking.FileId);
            ViewBag.PrinterId = new SelectList(db.Printers, "Id", "Name", booking.PrinterId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", booking.UserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FileId,UserId,StartTime,EndTime,PrinterId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FileId = new SelectList(db.Files, "Id", "FileName", booking.FileId);
            ViewBag.PrinterId = new SelectList(db.Printers, "Id", "Name", booking.PrinterId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = await db.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Booking booking = await db.Bookings.FindAsync(id);
            db.Bookings.Remove(booking);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult UploadFile(int id)
        {
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

        //

        [HttpPost]
        public ActionResult UploadFile(int id, HttpPostedFileBase fileUploader)
        {
            if (fileUploader != null)
            {
                var fileName = Path.GetFileName(fileUploader.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/App_Data/s3gfiler"), fileName);
                fileUploader.SaveAs(destinationPath);
                var file = new File {FileName = fileName, FilePath = destinationPath};

                if (file.ValidFilename())
                {
                    db.Files.Add(file);
                    db.SaveChangesAsync();
                    return RedirectToAction("MyBookings", "Frontend");
                }
                else
                {
                    ViewBag.Message = "Forkert filtype";
                }

                
            }
            return View();
        }
    }
}
