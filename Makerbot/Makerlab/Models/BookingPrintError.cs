using System;

namespace Makerlab.Models
{
    public class BookingPrintError
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ErrorType { get; set; }

        // Navigation
        public virtual User User { get; set; }
        public virtual Booking Booking { get; set; }

        public BookingPrintError()
        {
            
        }
    }
}