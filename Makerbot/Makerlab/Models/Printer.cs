using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Makerlab.Models
{
    public class Printer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UuId { get; set; }
        public bool Active { get; set; }
        public bool UnderMantenance { get; set; }

        // Navigation
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<PrinterCommand> PrinterCommands { get; set; } 

        public Printer()
        {
            
        }
    }
}