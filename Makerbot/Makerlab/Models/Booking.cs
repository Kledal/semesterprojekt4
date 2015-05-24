using System;
using System.Collections.Generic;

namespace Makerlab.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int? FileId { get; set; }
        public virtual File File { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int PrinterId { get; set; }
        public virtual Printer Printer { get; set; }
        
        public Booking()
        {
            
        }
    }
}