using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Makerlab.Models
{
    public class File
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public byte[] FileBytes { get; set; }
        
        [MaxLength]
        public string ContentType { get; set; }

        public File()
        {
            
        }

    }
}