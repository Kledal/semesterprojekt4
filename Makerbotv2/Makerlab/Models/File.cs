using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Makerlab.Models
{
    public class File
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        // Navigation

        public File()
        {
            
        }

    }
}