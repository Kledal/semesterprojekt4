using System.Web.Mvc;
using Kentor.AuthServices.Metadata;
using Kentor.AuthServices.StubIdp.Models;

namespace Kentor.AuthServices.StubIdp.Controllers
{
    public class FederationController : Controller
    {
        // GET: Federation
        public ActionResult Index()
        {
            return Content(
                MetadataModel.CreateFederationMetadata().ToXmlString(),
                "application/samlmetadata+xml");
        }

        public ActionResult BrowserFriendly()
        {
            return Content(
                MetadataModel.CreateFederationMetadata().ToXmlString(),
                "text/xml");
        }
    }
}