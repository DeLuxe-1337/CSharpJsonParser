using JsonParser.Parsing;
using JsonParser.Parsing.Nodes;
using JsonParser.Tokenizing;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using JsonParser.Extensions;

namespace JsonParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Type any show name to get statistics on it!");

            string movie = Console.ReadLine();
            movie = movie.Replace(" ", "+");

            string src = new WebClient().DownloadString($"http://api.tvmaze.com/singlesearch/shows?q={movie}&embed=episodes");

            Stopwatch sw = new();
            sw.Start();

            var nodes = Api.Parse(src);

            Console.WriteLine($"The show {nodes[0]["name"]} has a rating of {nodes[0]["rating"]["average"]}!");

            foreach (Node n in ((BlockNode)nodes[0]["_embedded"]["episodes"]).nodes)
            {
                Console.WriteLine($"Season {n["season"]} - episode {n["number"]} is named '{n["name"]}' has a rating of {n["rating"]["average"]}");
                Console.WriteLine($"Episode summary: {n["summary"].ToString().MassReplace("", "<p>", "</p>")}");
            }

            sw.Stop();

            Console.WriteLine($"It took {sw.ElapsedMilliseconds}ms to run!");

            Console.ReadLine();
        }
    }
}
