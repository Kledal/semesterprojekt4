using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Makerlab.Models
{
    public class PrinterCommand
    {
        public int Id { get; set; }
        public virtual Printer Printer { get; set; }
        public string Command { get; set; }

        public PrinterCommand()
        {
        }
    }
}