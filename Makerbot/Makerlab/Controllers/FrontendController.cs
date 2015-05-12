using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Services;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Makerlab.DAL;
using Makerlab.Extensions;
using Makerlab.Models;
using ModelState = System.Web.ModelBinding.ModelState;

namespace Makerlab.Controllers
{
    public class FrontendController : ApplicationController
    {
        private MakerContext db = new MakerContext();
        // GET: /Frontend/
        public ActionResult Index()
        {
            var printerList = new List<object>();
            foreach (var printer in PrinterManager.Read().Where(printer => printer.IsBookable))
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

        [Auth("ApprovedUser", "Administrator")]
        public ActionResult MyBookings()
        {
            return View(db.Bookings.Where(b => b.UserId == CurrentUser.Id).ToList());
        }

        public ActionResult LogInd()
        {
            return View();
        }

        public ActionResult NewBooking()
        {
            return View();
        }

        public ActionResult Printers()
        {
            return View(PrinterManager.Read());
        }

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
        public ActionResult NewBooking(HttpPostedFileBase file_Uploader)
        {
            if (file_Uploader != null)
            {
                string fileName = string.Empty;
                string destinationPath = string.Empty;

                List<FileUploadModel> uploadFileModel = new List<FileUploadModel>();

                fileName = Path.GetFileName(file_Uploader.FileName);
                destinationPath = Path.Combine(Server.MapPath("~/App_Data/s3gfiler"), fileName);
                file_Uploader.SaveAs(destinationPath);
                if (Session["fileUploader"] != null)
                {
                    var isFileNameRepete = ((List<FileUploadModel>)Session["fileUploader"]).Find(x => x.FileName == fileName);
                    if (isFileNameRepete == null)
                    {
                        uploadFileModel.Add(new FileUploadModel { FileName = fileName, FilePath = destinationPath });
                        ((List<FileUploadModel>)Session["fileUploader"]).Add(new FileUploadModel { FileName = fileName, FilePath = destinationPath });
                        ViewBag.Message = "File Uploaded Successfully";
                    }
                    else
                    {
                        ViewBag.Message = "File is already exists";
                    }
                }
                else
                {
                    uploadFileModel.Add(new FileUploadModel { FileName = fileName, FilePath = destinationPath });
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
                    ((List<FileUploadModel>)Session["fileUploader"]).RemoveAll(x => x.FileName == fileName);
                    sessionFileCount = ((List<FileUploadModel>)Session["fileUploader"]).Count;
                    if (fileName != null || fileName != string.Empty)
                    {
                        FileInfo file = new FileInfo(Server.MapPath("~/App_Data/s3gfiler" + fileName));
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
