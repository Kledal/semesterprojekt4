﻿using System.Collections.Generic;
using System.IO;

namespace Makerlab.Models
{
    public class Booking
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        //public int PrinterId { get; set; }

        // Navigation

        public virtual File File { get; set; }
        public virtual User User { get; set; }
        public virtual Printer Printer { get; set; }
        public virtual ICollection<BookingPrintError> BookingPrintError { get; set; }
        
        public Booking()
        {
            
        }
    }
}