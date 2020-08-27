using System;
using AdvancedClac;
class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
            foreach (var ystem in TL.Scan("a*b|c+d "))
            {
                Console.WriteLine(ystem.GetOperation);
                Console.WriteLine(ystem.GetValue);
            }
        }
    }
