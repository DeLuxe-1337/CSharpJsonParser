using JsonParser.Parsing;
using JsonParser.Parsing.Nodes;
using JsonParser.Tokenizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser
{
    public class JApi
    {
        public static List<Node> Parse(string data)
        {
            Lexer lexer = new Lexer(data);
            List<Token> tok = lexer.GetTokens();

            Parser parser = new(tok);
            List<Node> nodes = parser.Parse();

            return nodes;
        }
    }
}
