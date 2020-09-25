using System;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
           foreach (var ystem in TL.Scan("sin(45)"))
            {
                Console.WriteLine(ystem.Value);
                Console.WriteLine(ystem.TokenType);
            }
            
            //сonsole.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.2+4"))));
        }
    }
}
