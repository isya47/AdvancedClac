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
            
           dynamic num2 = 0.5;
           dynamic result = 0.2;
             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(3+1)"))));
             Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("a+0.2"))));
             //Console.WriteLine(num2|result);
        }
    }
}
