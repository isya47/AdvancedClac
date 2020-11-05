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
             //Console.WriteLine(MathFunc.Eval(MathFunc.Parsing(TL.Scan("pow(2,a)"),new Dictionary<char, string>{{'a',"2"}})));
             //Console.WriteLine(Math.Pow(0.5,0.3)); 
             var collection = new Dictionary<char, string>[]
             {
                 new Dictionary<char, string>
                 {
                     {'a',"0.5"}
                    
                 },
                 new Dictionary<char, string>
                 {
                     {'a', "2"}
                 },
                 new Dictionary<char, string>
                 {
                     {'a', "5"}
                 }
             };
             Queue Compiled = MathFunc.Parsing(TL.Scan("a*2"));
             Queue backup =new Queue(Compiled);
             for(int i=0;i<collection.Length;i++)
             {
                 Console.WriteLine(MathFunc.Eval(Compiled, collection[i]));
                 Compiled = new Queue(backup);
             }
        }
    }
}
