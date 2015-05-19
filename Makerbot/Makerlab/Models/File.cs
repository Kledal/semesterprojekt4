using System.ComponentModel.DataAnnotations;

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