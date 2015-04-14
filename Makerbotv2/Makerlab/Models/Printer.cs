using System.Collections.Generic;

namespace Makerlab.Models
{
    public class Printer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public virtual ICollection<Booking> Bookings { get; set; }

        public Printer()
        {
            
        }
    }
}