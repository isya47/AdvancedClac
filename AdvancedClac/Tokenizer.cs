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
            try
            {
                expression = expression.Trim();
                if (expression.Length == 0)
                {
                    throw new Exception("Empty String");
                }
            }
            
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            
            const string allowedSymbols = @"[- + * / | & ^ ~ ( ) , ]";
            const string allowedCharacter = @"[a-z]";
            const string allowedNumber = @"[0-9]";
            string concatedStr = "";
            string concatedNumStr = "";
            bool flag = false;
            for (var i = 0; i < expression.Length; i++)
            {
                if (Regex.IsMatch(expression[i].ToString(), allowedNumber, RegexOptions.IgnoreCase))
                {
                    for (int j = i; j < expression.Length; j++)
                    {
                        if (Regex.IsMatch(expression[j].ToString(), allowedNumber, RegexOptions.IgnoreCase)
                        && flag == false)
                        {
                            concatedNumStr = String.Concat(concatedNumStr, expression[j]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (concatedNumStr.Length >= 1)
                {
                    tokens.Add(new Token(concatedNumStr,"Numbers"));
                    concatedNumStr = "";
                    flag = true;
                } 
                
                else if (Regex.IsMatch(expression[i].ToString(), allowedCharacter, RegexOptions.IgnoreCase))
                {
                    flag = false;
                    concatedStr = String.Concat(concatedStr, expression[i].ToString());
                    
                }
                else if (Regex.IsMatch(expression[i].ToString(), allowedSymbols, RegexOptions.IgnoreCase))
                {
                    flag = false;
                    if (concatedStr.Length >= 1)
                    {
                        tokens.Add(new Token(concatedStr,"Variables"));
                        concatedStr = "";
                    }
                    tokens.Add(new Token(expression[i].ToString(), "Operator"));
                }
                
            }
            return tokens;
        }
    }
}