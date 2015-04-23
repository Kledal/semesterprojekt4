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
using Makerlab;
using Makerlab.Models;

namespace Makerlab.Controllers
{
    public class PrinterCommandsController : ApiController
    {
        private MakerContext db = new MakerContext();

        // GET: api/PrinterCommands
        public IQueryable<PrinterCommand> GetPrinter()
        {
            return db.Printer;
        }

        // GET: api/PrinterCommands/5
        [ResponseType(typeof(PrinterCommand))]
        public IHttpActionResult GetPrinterCommand(int id)
        {
            PrinterCommand printerCommand = db.Printer.Find(id);
            if (printerCommand == null)
            {
                return NotFound();
            }

            return Ok(printerCommand);
        }

        // PUT: api/PrinterCommands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrinterCommand(int id, PrinterCommand printerCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != printerCommand.Id)
            {
                return BadRequest();
            }

            db.Entry(printerCommand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrinterCommandExists(id))
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

        // POST: api/PrinterCommands
        [ResponseType(typeof(PrinterCommand))]
        public IHttpActionResult PostPrinterCommand(PrinterCommand printerCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Printer.Add(printerCommand);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = printerCommand.Id }, printerCommand);
        }

        // DELETE: api/PrinterCommands/5
        [ResponseType(typeof(PrinterCommand))]
        public IHttpActionResult DeletePrinterCommand(int id)
        {
            PrinterCommand printerCommand = db.Printer.Find(id);
            if (printerCommand == null)
            {
                return NotFound();
            }

            db.Printer.Remove(printerCommand);
            db.SaveChanges();

            return Ok(printerCommand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrinterCommandExists(int id)
        {
            return db.Printer.Count(e => e.Id == id) > 0;
        }
    }
}