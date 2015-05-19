using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makerlab;
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

        //[HttpPost]
        //public ActionResult BookPrinter(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);

        //        var path = Path.Combine(Server.MapPath("~/App_Data/s3gfiler"), fileName);
        //        file.SaveAs(path);
        //    }
        //    return NewBooking(); // Vil dette ikke fjerne andet der er indtastet???
        //}

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase fileUploader)
        {
            if (fileUploader != null)
            {
                var uploadFileModel = new List<File>();

                var fileName = Path.GetFileName(fileUploader.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/App_Data/s3gfiler"), fileName);
                fileUploader.SaveAs(destinationPath);
                if (Session["fileUploader"] != null)
                {
                    var isFileNameRepete = ((List<File>)Session["fileUploader"]).Find(x => x.FileName == fileName);
                    if (isFileNameRepete == null)
                    {
                        uploadFileModel.Add(new File { FileName = fileName, FilePath = destinationPath });
                        ((List<File>)Session["fileUploader"]).Add(new File { FileName = fileName, FilePath = destinationPath });
                        ViewBag.Message = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "File is already exists";
                    }
                }
                else
                {
                    uploadFileModel.Add(new File { FileName = fileName, FilePath = destinationPath });
                    Session["fileUploader"] = uploadFileModel;
                    ViewBag.Message = "File Uploaded Successfully";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RemoveUploadFile(string fileName)
        {
            int sessionFileCount = 0;

            try
            {
                if (Session["fileUploader"] != null)
                {
                    ((List<File>)Session["fileUploader"]).RemoveAll(x => x.FileName == fileName);
                    sessionFileCount = ((List<File>)Session["fileUploader"]).Count;
                    if (fileName != null || fileName != string.Empty)
                    {
                        var file = new FileInfo(Server.MapPath("~/App_Data/s3gfiler" + fileName));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(sessionFileCount, JsonRequestBehavior.AllowGet);
        }
    }
}
