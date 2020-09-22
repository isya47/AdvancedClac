using System;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
           foreach (var ystem in TL.Scan("13"))
            {
                Console.WriteLine(ystem.Value);
                Console.WriteLine(ystem.TokenType);
            }
            
            //onsole.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("0.2+4"))));
        }
    }
}
