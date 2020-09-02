using System;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
            foreach (var ystem in TL.Scan("pow(y,x)+y+ztan(45)"))
            {
                Console.WriteLine(ystem.GetTokenType);
                //Console.WriteLine(ystem.GetValue);
            }
        }
    }
}
