using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Makerlab.Extensions;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class PrintersController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        public ActionResult CancelPrint(int? id, string to)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }

            ViewBag.redirectTo = to;
            return View(printer);
        }

        [Auth("Administrator", "Godkendt Bruger")]
        [HttpPost, ActionName("CancelPrint")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelPrintConfirm(int id, string to)
        {
            var printer = db.Printers.Find(id);
            try
            {
                printer.CancelPrint();
            }
            catch (Exception e)
            {
                
            }
            if (to == "frontend")
            {
                return RedirectToAction("Printers", "Frontend");
            }
            return RedirectToAction("Printers", "Dashboard");
        }

        // GET: /Printers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Printers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,UuId,Active,UnderMantenance")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                db.Printers.Add(printer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printer);
        }

        // GET: /Printers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: /Printers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UuId,IsBookable")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                var dbPrinter = db.Printers.First(p => p.Id == printer.Id);
                dbPrinter.Name = printer.Name;
                dbPrinter.UuId = printer.UuId;
                dbPrinter.IsBookable = printer.IsBookable;
                db.SaveChanges();
                return RedirectToAction("Printers", "Dashboard");
            }
            return View(printer);
        }

        // GET: /Printers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: /Printers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Printer printer = db.Printers.Find(id);
            db.Printers.Remove(printer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
