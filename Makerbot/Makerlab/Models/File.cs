using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

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

        private readonly string[] _allowedFileTypes = {".gcode"};

        public File()
        {
            
        }

        public bool ValidFilename()
        {
            var ext = Path.GetExtension(FileName);
            return _allowedFileTypes.Contains(ext);
        }
    }
}
