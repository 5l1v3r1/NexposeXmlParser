using System;
using System.IO;
using System.Linq;

namespace NexposeXmlParser
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //TODO: Handle arguments better
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            Parser parser = new Parser();
            var nodes = parser.Parse(args[0]).ToList();

            // Debug print for now
            //foreach (var node in nodes)
            //{
            //    Console.WriteLine(node.Host);
            //    foreach (var endpoint in node.Ports)
            //    {
            //        Console.WriteLine("\t{0}:{1}", endpoint.Protocol, endpoint.PortNumber);
            //    }
            //}

            // Get a distinct list of port numbers
            foreach(var port in nodes.SelectMany(p => p.Ports).Select(p => p.PortNumber).Distinct())
            {
                int modifiedClosurePort = port;
                //TODO: Handle where to save output files
                using (StreamWriter stream = new StreamWriter(string.Format("{0}.txt", port)))
                {
                    // distinct list of IPs per port
                    foreach (var ip in nodes.Where(n => n.Ports.Any(p => p.PortNumber == modifiedClosurePort)).Distinct())
                    {
                        stream.WriteLine(ip.Host);
                    }
                    stream.Close();
                }
            }
            
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void ShowHelp()
        {
            //TODO: Write help text
            Console.WriteLine("Help text goes here");
        }
    }
}
