using System;
using System.IO;
using System.Linq;

namespace NexposeXmlParser
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (!Enumerable.Range(1, 2).Contains(args.Length))
            {
                Console.WriteLine("Usage: {0} report_file [output_folder]", AppDomain.CurrentDomain.FriendlyName);
                return;
            }

            // Get output folder
            var outputPath = GetOutputPath(args[1]);

            // Check that the xml file exists
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Could not open file: {0}", args[0]);
                return;
            }

            // Parse the xml file
            Parser parser = new Parser();
            var nodes = parser.Parse(args[0]).ToList();

            // Get a distinct list of port numbers
            foreach(var port in nodes.SelectMany(p => p.Ports).Select(p => p.PortNumber).Distinct())
            {
                int modifiedClosurePort = port;
                using (StreamWriter stream = new StreamWriter(Path.Combine(outputPath, string.Format("{0}.txt", port))))
                {
                    // distinct list of IPs per port
                    foreach (var ip in nodes.Where(n => n.Ports.Any(p => p.PortNumber == modifiedClosurePort)).Distinct())
                    {
                        stream.WriteLine(ip.Host);
                    }
                    stream.Close();
                }
            }
        }

        private static string GetOutputPath(string outputArg)
        {
            string outputPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase; // default to .exe path
            if (outputArg != null)
            {
                // create it if it doesn't exist
                if (!Directory.Exists(outputArg))
                {
                    try
                    {
                        Directory.CreateDirectory(outputArg);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error: Could not create directory: {0}", outputArg);
                        return outputPath;
                    }
                }
                outputPath = outputArg;
            }
            return outputPath;
        }
    }
}
