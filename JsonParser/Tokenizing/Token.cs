namespace JsonParser.Tokenizing
{
    public enum TokenTypes
    {
        String,
        Number,

        BlockStart,
        BlockEnd,

        Comma,
        Semicolon,

        False,
        True,
        Null,

        End
    }
    internal class Token
    {
        public int Line { get; set; }
        public TokenTypes TokenType { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return $"{TokenType} - {Value} on line({Line});";
        }

        public Token(TokenTypes token, object value, int line)
        {
            TokenType = token;
            Value = value;
            Line = line;
        }
    }
}
