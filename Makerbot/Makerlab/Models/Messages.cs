using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Makerlab.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Type { get; set; }

        public Messages()
        {
            
        }
    }
}