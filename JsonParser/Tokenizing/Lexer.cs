using System;
using System.Collections.Generic;

namespace JsonParser.Tokenizing
{
    internal class Lexer
    {
        private int Start = 0;
        private int Current = 0;
        private int Line = 1;

        private string Source { get; set; }

        public List<Token> Tokens = new();

        public Lexer(string source)
        {
            Source = source;
        }
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Lexer error({message});\nline({Line});\\nSkipping and continuing to parsing.");
            Console.ResetColor();

            //throw new Exception("Runtime error during parsing JSON.\n" + $"Lexer error({message});\nline({Line});");
        }
        private bool End()
        {
            return Current >= Source.Length;
        }

        private char Advance()
        {
            return Source[Current++];
        }
        public void AddToken(TokenTypes type)
        {
            AddToken(type, null);
        }
        public void AddToken(TokenTypes type, object value)
        {
            string text = Source.Substring(Start, Current - Start);
            Tokens.Add(new Token(type, value, Line));
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsAlpha(char c)
        {
            return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_';
        }

        private bool IsAlphaNumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        private bool Match(char expected)
        {
            if (End())
            {
                return false;
            }

            if (Source[Current] != expected)
            {
                return false;
            }

            Current++;
            return true;
        }

        private char Peek()
        {
            return End() ? '\0' : Source[Current];
        }

        private char PeekNext()
        {
            return Current + 1 >= Source.Length ? '\0' : Source[Current + 1];
        }
        private void String()
        {
            while (Peek() != '"' && !End())
            {
                if (Peek() == '\n')
                {
                    Line++;
                }

                Advance();
            }

            if (End())
            {
                Error("String unfinished.");
                return;
            }

            Advance();

            string value = Source.Substring(Start, Current - Start);

            value = value.Trim('\"');

            AddToken(TokenTypes.String, value);
        }

        private void Number()
        {
            while (IsDigit(Peek()))
            {
                Advance();
            }

            if (Peek() == '.' && IsDigit(PeekNext()))
            {
                Advance();

                while (IsDigit(Peek()))
                {
                    Advance();
                }
            }

            string value = Source.Substring(Start, Current - Start);
            AddToken(TokenTypes.Number, double.Parse(value));
        }

        private void Boolean()
        {
            while (IsAlpha(Peek()))
            {
                Advance();
            }

            string text = Source.Substring(Start, Current - Start);

            switch (text)
            {
                case "null":
                    {
                        AddToken(TokenTypes.Null);
                        break;
                    }
                case "false":
                    {
                        AddToken(TokenTypes.False, false);
                        break;
                    }
                case "true":
                    {
                        AddToken(TokenTypes.True, true);
                        break;
                    }
            }
        }

        private void ScanToken()
        {
            char c = Advance();

            switch (c)
            {
                case '[':
                case '{':
                    AddToken(TokenTypes.BlockStart);
                    break;
                case ']':
                case '}':
                    AddToken(TokenTypes.BlockEnd);
                    break;
                case ',':
                    AddToken(TokenTypes.Comma);
                    break;
                case ':':
                    AddToken(TokenTypes.Semicolon);
                    break;
                case '\n':
                    Line++;
                    break;
                case '"':
                    String();
                    break;
                case '\r':
                case ' ':
                    break;
                default:
                    {
                        if (IsDigit(c))
                        {
                            Number();
                        }
                        else if (IsAlpha(c))
                        {
                            Boolean();
                        }
                        else
                        {
                            Error($"Unexpected character({c});");
                        }

                        break;
                    }
            }
        }
        public List<Token> GetTokens()
        {
            while (!End())
            {
                Start = Current;
                ScanToken();
            }

            AddToken(TokenTypes.End);
            return Tokens;
        }
    }
}
