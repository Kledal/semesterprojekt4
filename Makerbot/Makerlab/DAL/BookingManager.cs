using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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