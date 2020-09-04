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
                try
                {
                    //Проверка, является ли текущий элемент числом
                    if (Regex.IsMatch(expression[i].ToString(), allowedNumber, RegexOptions.IgnoreCase))
                    {
                        // Если является, то создается цикл от начала индекса до конца выражения (или пока каждый новый элемент
                        // является числом)
                        for (int j = i; j < expression.Length; j++)
                        {
                            if (Regex.IsMatch(expression[j].ToString(), allowedNumber, RegexOptions.IgnoreCase)
                                && flag == false)
                            {
                                //Если элемент - это число и флаг = false (то есть это число еще не "проверяли") оно добавляется
                                // в стринг, который затем возвратится
                                // Флаг сделан для того, что бы число 45 не токенезировалось как 45, а затем еще 5
                                concatedNumStr = String.Concat(concatedNumStr, expression[j]);
                            }
                            // Если не число, цикл ломается
                            else
                            {
                                break;
                            }
                        }
                    }
                    //Проверка числового стринга, если в нем что то есть, он токенезируется и флаг отмечается, что текущие цифры
                    //были проверенны
                    if (concatedNumStr.Length >= 1)
                    {
                        tokens.Add(new Token(concatedNumStr,"Numbers"));
                        //Стринг сбрасывается
                        concatedNumStr = "";
                        flag = true;
                    } 
                    //Проверка на буквенные неизвестные и функции (sin, cos, pow)
                    else if (Regex.IsMatch(expression[i].ToString(), allowedCharacter, RegexOptions.IgnoreCase))
                    {
                        //сброс флага, указывает на то, что псоле буквы будут встречаться новые числа
                        flag = false;
                        //Добавление буквы в стринг
                        concatedStr = String.Concat(concatedStr, expression[i].ToString());
                    
                        //Если вдруг наше уравнение состит только из букв
                        if (i == expression.Length-1)
                        {
                            tokens.Add(new Token(concatedStr,"Variables"));
                            concatedStr = "";
                        }
                    }
                
                    //Проверка на наличие операторов
                    else if (Regex.IsMatch(expression[i].ToString(), allowedSymbols, RegexOptions.IgnoreCase))
                    {
                        //Аналогичные действия с флагом
                        flag = false;
                        //Если буквенный стринг не пуст, он токенезируется
                        if (concatedStr.Length >= 1)
                        {
                            tokens.Add(new Token(concatedStr,"Variables"));
                            concatedStr = "";
                        }
                        tokens.Add(new Token(expression[i].ToString(), "Operator"));
                    }
                    else
                    {
                        throw new Exception("Unknown symbol while tokenizing"); 
                    }
                }
                
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }
            }
            
            //возвращается лист содержащий все токены
            return tokens;
        }
    }
}