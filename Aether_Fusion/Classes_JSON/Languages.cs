using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aether_Console.Classes_JSON
{
    public class Language
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Root2
    {
        public List<Language> languages { get; set; }
    }
}
