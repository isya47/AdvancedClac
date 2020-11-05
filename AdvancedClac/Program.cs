using System;
using System.Collections;
using System.Collections.Generic;

namespace AdvancedClac
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer TL = new Tokenizer();

            //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(0.5,0.3)"))));
            Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("a*b+c")), new Dictionary<char, string> {{'a', "5"},
                {'b',"3"},
                {'c',"-7"}}));
        //Console.WriteLine(Math.Pow(0.5,0.3)); 
 
             
        }
    }
}
