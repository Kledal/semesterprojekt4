using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Makerlab.Models;
using Newtonsoft.Json;

namespace Makerlab.Controllers.api
{
    [RoutePrefix("api/Printers")]
    public class PrintersController : ApiController
    {
        private MakerContext db = new MakerContext();

        // GET api/Printers
        public List<Printer> GetPrinters()
        {
            return db.Printers.ToList();
        }

        // GET api/Printers/id/CancelPrint
        [Route("{id:int}/CancelPrint")]
        [HttpGet]
        public IHttpActionResult CancelPrint(int id)
        {
            
            var printer = db.Printers.Find(id);
            printer.CancelPrint();

            return Ok();
        }

        [Route("{id:int}/StartBooking/{bookingId:int}")]
        [HttpGet]
        public IHttpActionResult StartBooking(int id, int bookingId)
        {

            var printer = db.Printers.Find(id);

            if (printer == null)
            {
                return BadRequest();
            }

            var booking = db.Bookings.Find(bookingId);
            if (booking == null)
            {
                return BadRequest();
            }


            string uri = this.Url.Link("Default", new { controller = "Files", id = booking.FileId, action = "GetFile" });

            printer.StartBooking(booking.Id, uri);

            return Ok();
        }

        [Route("{id:int}/LastFrame")]
        [HttpGet]
        public string LastFrame(int id)
        {
            var printer = db.Printers.Find(id);
            return JsonConvert.SerializeObject(new { frame = printer.LastFrame });
        }

        // GET api/Printers/5
        [ResponseType(typeof(Printer))]
        public IHttpActionResult GetPrinter(int id)
        {
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return NotFound();
            }

            return Ok(printer);
        }
        
        // PUT api/Printers/5
        public IHttpActionResult PutPrinter(int id, Printer printer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != printer.Id)
            {
                return BadRequest();
            }

            db.Entry(printer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrinterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Printers
        [ResponseType(typeof(Printer))]
        public IHttpActionResult PostPrinter(Printer printer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Printers.Add(printer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = printer.Id }, printer);
        }

        // DELETE api/Printers/5
        [ResponseType(typeof(Printer))]
        public IHttpActionResult DeletePrinter(int id)
        {
            Printer printer = db.Printers.Find(id);
            if (printer == null)
            {
                return NotFound();
            }

            db.Printers.Remove(printer);
            db.SaveChanges();

            return Ok(printer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrinterExists(int id)
        {
            return db.Printers.Count(e => e.Id == id) > 0;
        }
    }
}