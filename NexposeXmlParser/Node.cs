using System;
using System.Collections.Generic;
using System.Linq;

namespace NexposeXmlParser
{
	public class Node
	{
		public string Host { get; set; }
		public IEnumerable<Port> Ports { get;set; }
		
		public Node()
		{
			
		}
	}
}

