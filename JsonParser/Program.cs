﻿using JsonParser.Parsing;
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

            //foreach (Token t in tok)
            //{
            //    Console.WriteLine(t.ToString());
            //}

            //Console.WriteLine();

            Parser parser = new(tok);
            System.Collections.Generic.List<Node> nodes = parser.Parse();

            Console.WriteLine(nodes[0][0].Index("repo > url"));

            foreach(var n in ((BlockNode)nodes[0]).nodes)
            {
                Console.WriteLine($"Repo name: {n["repo"]["name"]}\n" +
                    $"Repo url: {n["repo"]["url"]}\n" +
                    $"Repo publicity: {n["public"]}");
            }

            sw.Stop();

            Console.WriteLine($"It took {sw.ElapsedMilliseconds}ms to run!");

            Console.ReadLine();
        }
    }
}
