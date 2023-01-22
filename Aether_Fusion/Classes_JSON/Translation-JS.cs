using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aether_Console.Classes_JSON
{
 public class ExtraTranslation
{
    public string type { get; set; }
    public List<List> list { get; set; }
}

public class Info
{
    public Pronunciation pronunciation { get; set; }
    public List<object> definitions { get; set; }
    public List<object> examples { get; set; }
    public List<object> similar { get; set; }
    public List<ExtraTranslation> extraTranslations { get; set; }
}

public class List
{
    public string word { get; set; }
    public List<string> meanings { get; set; }
    public int frequency { get; set; }
}

public class Pronunciation
{
}

public class Root
{
    public string translation { get; set; }
    public Info info { get; set; }
}

}

