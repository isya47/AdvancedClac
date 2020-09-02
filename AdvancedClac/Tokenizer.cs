using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;

namespace AdvancedClac
{
    public class Tokenizer
    {
        static List<Token> tokens = new List<Token>();
        public IEnumerable<Token> Scan(string expression)
        {
            expression = expression.Trim();
            const string allowedSymbols = @"[- + * / | & ^ ~ ( ) , 0-9]";
            const string allowedCharacter = @"[a-z]";
            string concatedStr = "";
            for (var i = 0; i < expression.Length; i++)
            {
                
                if (Regex.IsMatch(expression[i].ToString(), allowedCharacter, RegexOptions.IgnoreCase))
                {
                    concatedStr = String.Concat(concatedStr, expression[i].ToString());
                }
                
                else if (Regex.IsMatch(expression[i].ToString(), allowedSymbols, RegexOptions.IgnoreCase))
                {
                    if (concatedStr.Length >= 1)
                    {
                        tokens.Add(new Token(0,concatedStr));
                        concatedStr = "";
                    }
                    tokens.Add(new Token(0, expression[i].ToString()));
                }
            }
            return tokens;
        }
    }
}