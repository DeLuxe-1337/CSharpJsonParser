using JsonParser.Parsing.Nodes;
using JsonParser.Tokenizing;
using System;
using System.Collections.Generic;
using static JsonParser.Tokenizing.TokenTypes;

namespace JsonParser.Parsing
{
    internal class Parser
    {
        private readonly List<Token> Tokens;
        private int Current = 0;

        public Parser(List<Token> tokens)
        {
            Tokens = tokens;
        }

        public List<Node> Parse()
        {
            List<Node> nodes = new List<Node>();
            while (!End())
            {
                nodes.Add(Blocks());
            }

            return nodes;
        }

        private Node Blocks()
        {
            if (Match(BlockStart))
            {
                List<Node> nodes = new List<Node>();
                while (!Check(BlockEnd) && !End())
                {
                    nodes.Add(Blocks());
                }

                Consume(BlockEnd, "Expected ending block '}'");

                if (Peek().TokenType == Comma && Peek(1).TokenType == BlockStart)
                {
                    Consume(Comma, "Expected ',' if you want more children.");
                }

                return new BlockNode(nodes);
            }

            return Assignment();
        }
        private Node Assignment()
        {
            if (Check(TokenTypes.String) && Peek(1).TokenType == Semicolon)
            {
                Token name = Consume(TokenTypes.String, "Expected a string identifier.");
                Consume(Semicolon, "Expected an assignment operator.");
                Node init = Blocks();

                return new AssignmentNode(name.Value.ToString(), init);
            }

            return Values();
        }
        private Node Values()
        {
            if (Match(Number, False, True, TokenTypes.String))
            {
                object previous = Previous().Value;

                if (Peek().TokenType != BlockEnd)
                {
                    Consume(Comma, "Expected ',' if you want more children.");
                }

                return new ValueNode(previous);
            }

            Error("End of parse skipping node.");
            return null;
        }
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Parser error({message});\n\n{Tokens[Current]}");
            Console.ResetColor();

            throw new Exception("Runtime error during parsing JSON.\n" + $"Parser error({message});");
        }
        private Token Consume(TokenTypes type, string message)
        {
            if (Check(type))
            {
                return Advance();
            }

            Error(message);

            return null;
        }
        private bool Match(params TokenTypes[] types)
        {
            foreach (TokenTypes type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        private bool Check(TokenTypes type)
        {
            if (End())
            {
                return false;
            }

            return Peek().TokenType == type;
        }

        private Token Advance()
        {
            if (!End())
            {
                Current++;
            }

            return Previous();
        }

        private bool End()
        {
            return Peek().TokenType == TokenTypes.End;
        }

        private Token Peek()
        {
            return Tokens[Current];
        }
        private Token Peek(int amount)
        {
            return Tokens[Current + amount];
        }

        private Token Previous()
        {
            return Tokens[Current - 1];
        }
    }
}
