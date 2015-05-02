using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Makerlab.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [ForeignKey("File")]
        public virtual int FileId { get; set; }
        
        public virtual File File { get; set; }

        [ForeignKey("User")]
        public virtual int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [ForeignKey("Printer")]
        public virtual int PrinterId { get; set; }
        public virtual Printer Printer { get; set; }
        public virtual ICollection<BookingPrintError> BookingPrintError { get; set; }
        
        public Booking()
        {
            
        }
    }
}