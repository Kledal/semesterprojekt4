using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Makerlab.Extensions;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class UserrolesController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        // GET: /Userroles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // GET: /Userroles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Userroles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,RoleName,CanCreateBooking,CanViewDashboard")] UserRole userrole)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userrole);
                db.SaveChanges();
                return RedirectToAction("Userroles", "Dashboard");
            }

            return View(userrole);
        }

        // GET: /Userroles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /Userroles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,RoleName,CanCreateBooking,CanViewDashboard")] UserRole userrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserRoles", "Dashboard");
            }
            return View(userrole);
        }

        // GET: /Userroles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userrole = db.UserRoles.Find(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /Userroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole userrole = db.UserRoles.Find(id);
            db.UserRoles.Remove(userrole);
            db.SaveChanges();
            return RedirectToAction("UserRoles", "Dashboard");
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
