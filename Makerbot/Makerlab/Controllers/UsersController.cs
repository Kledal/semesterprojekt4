using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Makerlab.Extensions;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class UsersController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.UserRoles = new SelectList(db.UserRoles, "Id", "RoleName");
            return View(user);
        }

        // POST: /Userroles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,StudieNummer,AccessCard,UserRoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Users", "Dashboard");
            }
            ViewBag.UserRoles = new SelectList(db.UserRoles.ToList(), "Id", "RoleName", user.UserRole.RoleName);
            return View(user);
        }

        // GET: /Userroles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Userroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Users", "Dashboard");
        }

        // GET: /Printers/Delete/5
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Messages/Delete/5
        [HttpPost, ActionName("Approve")]
        public ActionResult ApproveConfirmed(int id)
        {
            User user = db.Users.Find(id);
            user.UserRoleId = 2;
            db.SaveChanges();
            return RedirectToAction("Users", "Dashboard");
        }
    }
}