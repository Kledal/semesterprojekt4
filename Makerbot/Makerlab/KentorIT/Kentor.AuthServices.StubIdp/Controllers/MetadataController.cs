using System.Web.Mvc;
using Kentor.AuthServices.Metadata;
using Kentor.AuthServices.StubIdp.Models;

namespace Kentor.AuthServices.StubIdp.Controllers
{
    public class MetadataController : Controller
    {
        // GET: Metadata
        public ActionResult Index()
        {
            return Content(
                MetadataModel.CreateIdpMetadata().ToXmlString(),
                "application/samlmetadata+xml");
        }

        public ActionResult BrowserFriendly()
        {
            return Content(
                MetadataModel.CreateIdpMetadata().ToXmlString(),
                "text/xml");
        }
    }
}