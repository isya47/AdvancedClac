using System;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();
            foreach (var ystem in TL.Scan("x+62*y+pow(2,z)+3"))
            {
                //Console.WriteLine(ystem.GetTokenType);
                Console.WriteLine(ystem.GetValue);
            }
        }
    }
}
