using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NexposeXmlParser
{
    public class Parser
    {
        public IEnumerable<Node> Parse(string xmlFilePath)
        {
            if (!File.Exists(xmlFilePath))
            {
                throw new Exception(string.Format("File does not exist: {0}", xmlFilePath));
            }

            //Load xml
            XDocument xdoc = XDocument.Load(xmlFilePath);

            //Load data into structures from the Xml data
            var nodes = from node in xdoc.Descendants("node")
                        let xAddress = node.Attribute("address")
                        where xAddress != null
                        select new Node
                                   {
                                       Host = xAddress.Value,
                                       Ports = from endpoint in node.Descendants("endpoint")
                                               let xPortNumber = endpoint.Attribute("port")
                                               where xPortNumber != null
                                               let xProtocol = endpoint.Attribute("protocol")
                                               where xProtocol != null
                                               select new Port
                                                          {
                                                              PortNumber = ParsePortNumber(xPortNumber.Value),
                                                              Protocol = ParseProtocol(xProtocol.Value),
                                                          }
                                   };

            return nodes;
        }

        private int ParsePortNumber(string portNumberString)
        {
            int portNumber;
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

