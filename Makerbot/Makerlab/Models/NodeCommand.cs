using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Makerlab.Models
{
    public class NodeCommand
    {
        public string name { get; set; }
        public object info { get; set; }

        public NodeCommand(string name, object val)
        {
            this.name = name;
            this.info = val;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}