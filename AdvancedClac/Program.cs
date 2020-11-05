using System;
using System.Collections.Generic;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
            
             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(0.5,0.3)"))));
             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(2,a)"),new Dictionary<char, string>{{'a',"2"}})));
             //Console.WriteLine(Math.Pow(0.5,0.3)); 
             Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("a+b-c*(d/e)")),
                 new Dictionary<char, string>
                 {
                     {'a',"2"},
                     {'b',"4"},
                     {'c',"7"},
                     {'d',"4"},
                     {'e',"2"}
                 }));
        }
    }
}
