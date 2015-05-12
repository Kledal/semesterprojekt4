using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Makerlab.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string Type { get; set; }

        public bool Show { get; set; }
        
        public Message()
        {
            
        }

    }
}