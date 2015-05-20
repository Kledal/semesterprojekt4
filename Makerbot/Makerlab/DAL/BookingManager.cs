using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Makerlab.Models;

namespace Makerlab.DAL
{
    public class BookingManager
    {
        public static Booking Create(Booking booking)
        {
            using (var db = new MakerContext())
            {
                db.Bookings.Add(booking);
                db.SaveChanges();

                return booking;
            }
        }

        public static IEnumerable<Booking> Read()
        {
            using (var db = new MakerContext())
            {
                var bookings = db.Bookings.Include(u => u.User).ToList();
                return bookings;
            }
        }

        public static Booking Update(Booking booking)
        {
            using (var db = new MakerContext())
            {
                db.Bookings.Attach(booking);

                var updatedPrinter = db.Entry(booking);
                updatedPrinter.State = EntityState.Modified;

                db.SaveChanges();

                return updatedPrinter.Entity;
            }
        }

        public static void Delete(Booking booking)
        {
            using (var db = new MakerContext())
            {
                db.Bookings.Attach(booking);
                db.Bookings.Remove(booking);
                db.SaveChanges();
            }
        }
    }
}