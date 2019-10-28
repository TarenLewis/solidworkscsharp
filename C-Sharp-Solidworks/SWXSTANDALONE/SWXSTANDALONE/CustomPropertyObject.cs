using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWXSTANDALONE
{
    internal class CustomPropertyObject
    {
        internal string Name { get; set; }
        internal string Value { get; set; }
        internal bool Delete { get; set; }
        internal string NewVal { get; set; }

        public CustomPropertyObject() { }

        internal CustomPropertyObject(string name, string value, string newVal)
        {
            Name = name;
            Value = value;
            NewVal = newVal; 
        }
            
    }
}
