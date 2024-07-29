using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRSWordAddIn.Utils
{
    public class rule
    {
        public string name { get; set; }
        public int level { get; set; }
        public bool suggestion_must { get; set; }
        public string suggestion_custom { get; set; }
        public bool open { get; set; }
    }
}
