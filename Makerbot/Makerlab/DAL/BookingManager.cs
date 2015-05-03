using System;
using System.Collections.Generic;
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