using System;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();

             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("sin(3+1)"))));
             Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("3*pow(3+2,9)a"))));
             //Console.WriteLine(num2|result);
        }
    }
}
