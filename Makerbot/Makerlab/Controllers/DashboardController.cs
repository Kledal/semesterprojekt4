using System.Linq;
using System.Web.Mvc;
using Makerlab.DAL;
using Makerlab.Extensions;

namespace Makerlab.Controllers
{
    [Auth("Administrator")]
    public class DashboardController : ApplicationController
    {
        private MakerContext db = new MakerContext();

        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Users()
        {
            ViewBag.NonApprovedUsers = db.Users.Where(u => u.UserRoleId == 3).ToList();
            ViewBag.AllUsers = db.Users.ToList();
            return View("Users");
        }

        public ActionResult Printers()
        {
            return View("Printers", PrinterManager.Read());
        }

        public ActionResult Userroles()
        {
            return View("Userroles", db.UserRoles.ToList());
        }

        public ActionResult Messages()
        {
            return View("Messages");
        }
    }
}
