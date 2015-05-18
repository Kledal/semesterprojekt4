using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public string LastFrame { get; set; }

        // Navigation
        public virtual ICollection<Booking> Bookings { get; set; }

        public Printer()
        {
            
        }

        public void CancelPrint()
        {
            if (!MvcApplication.Redis.IsConnected) throw new Exception("Redis is not connected.");
            var redisDb = MvcApplication.Redis.GetDatabase();

            redisDb.Publish("commands", new NodeCommand("cancel_print", new { uuid = UuId }).ToString());
        }
    }
}