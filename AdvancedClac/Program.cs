using System;
using System.Collections.Generic;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
            
             Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.5|0.2"))));
             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(2,a)"),new Dictionary<char, string>{{'a',"2"}})));
             Console.WriteLine(4&5);
        }
    }
}
