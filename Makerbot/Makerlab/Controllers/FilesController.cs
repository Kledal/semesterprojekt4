using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Web.Http;
using System.Web.Mvc;

namespace Makerlab.Controllers
{
    public class FilesController : Controller
    {
        private MakerContext db = new MakerContext();

        public FileResult GetFile(int id)
        {
            var file = db.Files.Find(id);

            return File(file.FilePath, MediaTypeNames.Application.Octet, file.FileName);
        }
    }
}