using System.Net;
using System.Web.Mvc;
using Makerlab.Extensions;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class MessagesController : ApplicationController
    {
        private MakerContext db = new MakerContext();
        
        // GET: Messages
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Content,Type,Show")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Messages", "Dashboard");
            }

            return View(message);
        }


        // GET: /Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: /Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Messages", "Dashboard");
        }

    }
}