using System;

namespace NexposeXmlParser
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//TODO: Handle arguments better
			if (args.Length == 0)
			{
				ShowHelp();
				return;
			}
			
			Parser parser = new Parser();
			var nodes = parser.Parse(args[0]);
			
			// Debug print for now
			foreach(var node in nodes)
			{
				Console.WriteLine(node.Host);
				foreach(var endpoint in node.Ports)
				{
					Console.WriteLine("\t{0}:{1}", endpoint.Protocol, endpoint.PortNumber);	
				}
			}
			
			Console.WriteLine ("Done");
			Console.ReadLine();
		}
		
		public static void ShowHelp()
		{
			//TODO: Write help text
			Console.WriteLine("Help text goes here");	
		}
	}
}
