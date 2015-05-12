using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Makerlab.Models;

namespace Makerlab.DAL
{
    public class PrinterManager
    {
        public static Printer Create(Printer printer)
        {
            using (var db = new MakerContext())
            {
                db.Printers.Add(printer);
                db.SaveChanges();

                return printer;
            }
        }

        public static Printer Update(Printer printer)
        {
            using (var db = new MakerContext())
            {
                db.Printers.Attach(printer);

                var updatedPrinter = db.Entry(printer);
                updatedPrinter.State = EntityState.Modified;

                db.SaveChanges();

                return updatedPrinter.Entity;
            }
        }
        
        public static List<Printer> Read()
        {
            using (var db = new MakerContext())
            {
                var printers = db.Printers.Include(u => u.Bookings).ToList();
                return printers;
            }
        }

        public static Printer Read(int id)
        {
            using (var db = new MakerContext())
            {
                var printer = db.Printers.Include(u => u.Bookings)
                    .Include(u => u.Bookings).SingleOrDefault(u => u.Id == id);

                return printer;
            }
        }

        public static Printer Read(string uuid)
        {
            using (var db = new MakerContext())
            {
                var printer = db.Printers.Include(u => u.Bookings)
                    .Include(u => u.Bookings).SingleOrDefault(u => u.UuId == uuid);

                return printer;
            }
        }

        public static void Delete(Printer printer)
        {
            using (var db = new MakerContext())
            {
                db.Printers.Attach(printer);
                db.Printers.Remove(printer);
                db.SaveChanges();
            }
        }
    }
}