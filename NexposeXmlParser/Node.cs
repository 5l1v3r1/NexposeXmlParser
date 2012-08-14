using System.Collections.Generic;

namespace NexposeXmlParser
{
    public class Node
    {
        public string Host { get; set; }
        public IEnumerable<Port> Ports { get; set; }
    }
}

