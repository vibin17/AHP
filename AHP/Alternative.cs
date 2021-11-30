using System;
using System.Collections.Generic;
using System.Text;

namespace AHP
{
    class Alternative
    {
        public string Name { get; set; }
        public Dictionary<string, string> Criterias { get; set; }
        public Alternative(string name, Dictionary<string, string> crits)
        {
            Name = name;
            Criterias = crits;
        }
    }
}
