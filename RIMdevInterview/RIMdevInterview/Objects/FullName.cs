using System;
using System.Collections.Generic;
using System.Text;

namespace RIMdevInterview.Objects
{
    public class FullName
    {
        public string First { get; set; }
        public string Last  { get; set; }
        public FullName ()
        {
            this.First = null;
            this.Last = null;
        }
    }
}
