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
            return View();
        }

        public ActionResult Users()
        {
            return View(UserManager.Read());
        }

        public ActionResult Printers()
        {
            return View(PrinterManager.Read());
        }

        public ActionResult Userroles()
        {
            return View(db.UserRoles.ToList());
        }

        public ActionResult Messages()
        {
            return View();
        }

        // GET: /Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
