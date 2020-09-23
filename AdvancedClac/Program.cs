using System;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
           /* foreach (var ystem in TL.Scan("55 * x + 99 - 16/4"))
            {
                Console.WriteLine(ystem.GetValue);
                Console.WriteLine(ystem.GetTokenType);
            }*/
            
             Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("-3*pow(2+2,-4/2)"))));
        }
    }
}
