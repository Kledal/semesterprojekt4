using System.Collections.Generic;

namespace Makerlab.Models
{
    public class Printer
    {
        public int Id { get; set; }
        public int UuId { get; set; }
        public string Name { get; set; }

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