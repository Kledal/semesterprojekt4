using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Makerlab.Models;
using Makerlab;
using Makerlab.DAL;

namespace Makerlab.Controllers
{
    public class PrintersController : ApiController
    {
        private MakerContext db = new MakerContext();

        // GET api/Printers
        public IQueryable<Printer> GetPrinters()
        {
            return db.Printers.Include(p => p.PrinterCommands).Include(p => p.Bookings);
        }

        [AcceptVerbs("GET")]
        public PrinterCommand PrinterCommands(int printerId)
        {
            return db.PrinterCommands.Include(p => p.Printer).FirstOrDefault(pc => pc.Printer.Id == printerId);
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
            if (db.Printers.Any(p => printer.UuId == p.UuId))
                return CreatedAtRoute("DefaultApi", new {id = printer.Id}, printer);

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