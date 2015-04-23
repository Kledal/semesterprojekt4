using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Makerlab;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    public class PrintersController : ApiController
    {
        private MakerContext db = new MakerContext();

        // GET: api/Printers
        public IQueryable<Printer> GetPrinters()
        {
            return db.Printers;
        }

        // GET: api/Printers/5
        [ResponseType(typeof(Printer))]
        public async Task<IHttpActionResult> GetPrinter(int id)
        {
            Printer printer = await db.Printers.FindAsync(id);
            if (printer == null)
            {
                return NotFound();
            }

            return Ok(printer);
        }

        // PUT: api/Printers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrinter(int id, Printer printer)
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
                await db.SaveChangesAsync();
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

        // POST: api/Printers
        [ResponseType(typeof(Printer))]
        public async Task<IHttpActionResult> PostPrinter(Printer printer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Printers.Add(printer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = printer.Id }, printer);
        }

        // DELETE: api/Printers/5
        [ResponseType(typeof(Printer))]
        public async Task<IHttpActionResult> DeletePrinter(int id)
        {
            Printer printer = await db.Printers.FindAsync(id);
            if (printer == null)
            {
                return NotFound();
            }

            db.Printers.Remove(printer);
            await db.SaveChangesAsync();

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