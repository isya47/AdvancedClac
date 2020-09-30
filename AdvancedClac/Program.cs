using System;
using System.Collections.Generic;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
            
             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("3&2"))));
             Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.2&1"),new Dictionary<char, string>{{'a',"2"},{'b',"4"}})));
             Console.WriteLine(~2);
        }
    }
}
