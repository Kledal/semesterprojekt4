﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makerlab.Models;
using Makerlab;
using Makerlab.Extensions;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class PrintersController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        // GET: /Printers/
        public ActionResult Index()
        {
            return View("Index",db.Printers.ToList());
        }

        public ActionResult CancelPrint(int id)
        {
            var printer = db.Printers.Find(id);
            try
            {
                printer.CancelPrint();
            }
            catch (Exception e)
            {
                
            }

            return RedirectToAction("Printers", "Dashboard");
        }

        // GET: /Printers/Details/5
        public ActionResult Details(int? id)
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
