using JsonParser.Parsing;
using JsonParser.Parsing.Nodes;
using JsonParser.Tokenizing;
using System;
using System.Diagnostics;
using System.IO;

namespace JsonParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new();
            sw.Start();

            string src = File.ReadAllText(Environment.CurrentDirectory + "\\source.json");

            Lexer lexer = new Lexer(src);
            System.Collections.Generic.List<Token> tok = lexer.GetTokens();

            foreach (Token t in tok)
            {
                Console.WriteLine(t.ToString());
            }

            Console.WriteLine();

            Parser parser = new(tok);
            System.Collections.Generic.List<Node> nodes = parser.Parse();

            foreach(var n in ((BlockNode)nodes[0]).nodes)
            {
                Console.WriteLine(n["first_name"].ToString());
            }

            sw.Stop();

            Console.WriteLine($"It took {sw.ElapsedMilliseconds}ms to run!");

            Console.ReadLine();
        }
    }
}
