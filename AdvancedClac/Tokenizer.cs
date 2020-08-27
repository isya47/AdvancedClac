using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdvancedClac
{
    public class Tokenizer
    {
        static List<Token> tokens = new List<Token>();
        public enum TokenType : ushort
        {
            Number,
            Add,
            Substract,
            Multiply,
            Divide,
            Pow,
            Sin,
            Cos,
            Abs,
            Tan,
            
            Or,
            And,
            Xor,
            Bitwise
        }
        
        public IEnumerable<Token> Scan(string expression)
        { 
            var reader = new StringReader(expression);
            const string allowedCharacters = @"[a-z]";
            const string allowedOperators = @"[- + * / | & ^ ~]";
            var Num = TokenType.Number;

            while (reader.Peek() != -1)
            {
                var c = (char) reader.Peek();

                if (Char.IsWhiteSpace(c))
                {
                    reader.Read();
                    continue;
                }
                
                if (Regex.IsMatch(c.ToString(), allowedCharacters, RegexOptions.IgnoreCase))
                {
                    tokens.Add(new Token(0, c.ToString()));
                    reader.Read();
                }
                else if (Regex.IsMatch(c.ToString(), allowedOperators, RegexOptions.IgnoreCase))
                {
                    tokens.Add(new Token(0, c.ToString()));
                    reader.Read();
                }
                else

                    throw new Exception("Unknown character in expression: " + c);

            }
            return tokens;
        }
    }
}