using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Makerlab.Models
{
    public class Printer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UuId { get; set; }
        public bool IsBookable { get; set; }
        public DateTime LastSeen { get; set; }

        public int BedTemperature { get; set; }
        public int NozzleTemperature { get; set; }
        public bool Printing { get; set; }
        public bool Paused { get; set; }
        public int CurrentLine { get; set; }

        public string LastFrame { get; set; }

        // Navigation
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<PrinterCommand> PrinterCommands { get; set; } 

        public Printer()
        {
            
        }
    }
}