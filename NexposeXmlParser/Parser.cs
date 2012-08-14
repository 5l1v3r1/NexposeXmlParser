using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace NexposeXmlParser
{
	public class Parser
	{
		public IEnumerable<Node> Parse (string xmlFilePath)
		{
			if (!File.Exists(xmlFilePath))
			{
				throw new Exception(string.Format ("File does not exist: {0}", xmlFilePath));
			}
			
			//Load xml
			XDocument xdoc = XDocument.Load(xmlFilePath);
			
			//Load data into structures from the Xml data
			var nodes = from node in xdoc.Descendants("node")
	           select new Node { 
	               Host = node.Attribute("address").Value,
	               Ports = from endpoint in node.Descendants("endpoint")
					select new Port {
						PortNumber = ParsePortNumber(endpoint.Attribute("port").Value),
						Protocol = ParseProtocol(endpoint.Attribute("protocol").Value),
				}
	           };
			
			return nodes;
		}
		
		private int ParsePortNumber(string portNumberString)
		{
			int portNumber = 0;
			Int32.TryParse(portNumberString, out portNumber);
			return portNumber;
		}
		
		private Protocol ParseProtocol(string protocolString)
		{
			switch (protocolString.ToUpper())
			{
			case "TCP":
				return Protocol.Tcp;
			case "UDP":
				return Protocol.Udp;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}

