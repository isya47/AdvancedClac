using System;
using System.Collections;
using System.Collections.Generic;
using static AdvancedClac.TokenTypeEnum;

//Перегрузить операции для всех типов чисел
//Сделать умножение для переменных по умолчанию 
namespace AdvancedClac
{    /*public class Node
    {
        public readonly string type;
        public readonly string value;
        public unsafe void* parent;
        public unsafe void* left;
        public unsafe void* right;
    }*/
    public class MathFunc
    {
        private static Dictionary<char,string> variables=new Dictionary<char, string>();
        //Метод инициализации данных операторов. Это нужно для более простых вычислении о порядке оператаров.
        private static Dictionary<string, int> initial()
        {
            return(new Dictionary<string, int> {{"pow",4},{"sin",0},{"cos",0},{"tan",0},{"*",3},{"/",3},{"+",2},{"-",2}});
        }
        /*
        private enum precedence : ushort
        {
            Add=2,
            Substract=2,
            Multiply=3,
            Divide=3,
            pow=4,
            sin=4,
            cos=4,
            tan=4,
        }
        */

        internal static double Unitary(double num1, string operator1)
        {
            //double num1 = double.Parse(a);
            switch (operator1)
            {
                case "sin":
                    return Math.Sin(num1);
                case "cos":
                    return Math.Cos(num1);
                case "tan":
                    return Math.Tan(num1);
            }

            return 0;
        }
        
        internal static T Antiunitary<T>(T a, T b, string operator1)
        {
            dynamic num2 = a;
            dynamic result = b;
            switch (operator1)
            {
                case "/":
                    return result / num2;
                case "*":
                    return result * num2;
                case "+":
                    return result + num2;
                case "-":
                    return result - num2;
                case "pow":
                    long temp = result;
                    for (int i = 1; i < num2; i++)
                    {
                        //не знаю есть смысл или нет но тут можно сделать goto к кейсам 1 и 2
                        result *= temp;

                    }
                    return result;
            }

            return a;
        }
        
//Функция исполняющая переведённое выражение 
        public static string Eval(Queue operands)
        {
            Dictionary<string, int> precedence = initial();
            Stack result=new Stack();
            if (operands.Count == 0)
                return ("NULL");
            while (operands.Count != 0)
            {
                //Console.WriteLine(operands.Peek());
                if (operands.Peek() is char && variables[(char)operands.Peek()] == "Null")
                {
                    Console.WriteLine("Enter value for '{0}'", operands.Peek());
                    variables[(char) operands.Peek()] = Console.ReadLine();
                    result.Push(double.Parse(variables[(char) operands.Dequeue()]));
                    Console.WriteLine(result.Peek());
                }
                else if (operands.Peek() is char && variables[(char) operands.Peek()] != "Null")
                {
                    result.Push(double.Parse(variables[(char) operands.Dequeue()]));
                }

                else if (operands.Peek() is long||operands.Peek() is double)
                {
                    result.Push(operands.Dequeue());
                }
                else if (precedence[(string)operands.Peek()]>0)
                {
                    result.Push(Antiunitary(result.Pop(), result.Pop(), (string) operands.Dequeue()));
                }
                else if (!(precedence[(string)operands.Peek()]>0))
                {
                    result.Push(Unitary(Double.Parse(result.Pop().ToString()),(string)operands.Dequeue()));
                }
                else
                {
                    return ("Syntax error");
                }

                
            }

            return ( result.Pop().ToString());
        }
//Функция для переводе данных в обратную польскую запись: https://ru.wikipedia.org/wiki/Алгоритм_сортировочной_станции
        //https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        public static Queue Parsing(IEnumerable<Token> stream)
        {
            bool valueflag = false;
            Dictionary<string, int> precedence = initial();
            //Стек для операторов, функции и скобок
            Stack operators = new Stack();
            //Очередь для чисел а так же то что возращяется из метода
            Queue operands= new Queue();
            //Алгоритм для преврощения строки в очередь в обратную польскую запись
            foreach (var i in stream)
            {
                //работа со скобками
                
                Console.WriteLine(i.GetValue);
                    if (i.GetValue == "(")
                    {
                        operators.Push(i.GetValue);
                    }
                    else if (i.GetValue == ")"||i.GetValue==",")
                    {
                        while ((string)operators.Peek() != "(")
                        {
//Error check here for extra parentheses
                            operands.Enqueue( operators.Pop());
                        }
                        operators.Pop();
                        
                        if(precedence[(string)operators.Peek()]==0)
                            operands.Enqueue(operators.Pop());
                        if(i.GetValue==",")
                            operators.Push("(");
                            
                    }

                    else if (i.GetTokenType == (Enum) Variables)
                    {
                        
                        valueflag = true;
                        //Console.WriteLine(i.GetTokenType);
                        for (int l = 0; l < i.GetValue.Length; l++)
                        {
                            if (l+2 <i.GetValue.Length&&precedence.ContainsKey(i.GetValue.Substring(l,3)))
                            {
                                operators.Push(i.GetValue.Substring(l,3));
                                l += 2;
                            }
                            else
                            {
                                operands.Enqueue((char)i.GetValue[l]);
                                variables.Add((char)i.GetValue[l],"Null");
                            }
                        }
                    }
                    else if (i.GetTokenType == (Enum)Numbers)
                    {
                        valueflag = true;
                        if (i.GetValue.Contains('.'))
                        {
                            operands.Enqueue(double.Parse(i.GetValue));
                        }
                        else
                            operands.Enqueue(long.Parse(i.GetValue));
                    }
                   else if (i.GetTokenType==(Enum) Operators||(valueflag==true&&i.GetTokenType==(Enum)Variables)){
                    /*
                        switch(valueflag)
                        case true:
                        string temp = "*";
                        default:
                        */
                    string temp = i.GetValue;
                            //Console.WriteLine(i.GetValue);
                        // Console.WriteLine(operators.Count);
                        
                        //работа с оператором в стаке для операторов, разбор приоретета оператора
                        while (operators.Count != 0 && precedence.ContainsKey((string)operators.Peek()) &&
                               (precedence[(string)operators.Peek()] > precedence[temp] ||
                                (precedence[(string) operators.Peek()] == precedence[temp] && temp != "pow")))
                        {
                            operands.Enqueue(operators.Pop());
                            
                        }
                        operators.Push(temp);
                    }
                  //  if(operators.Count!=0)
                   // Console.WriteLine(operators.Peek());
            }
            //Любые оставшиеся операторы добавляются в конец очереди
            while (operators.Count != 0)
            {
                operands.Enqueue((string)operators.Pop());
            }
/*
            while (operands.Count != 0)
            {
                Console.Write(operands.Dequeue());
            }

            Console.WriteLine("end");
         */   
            return(operands);
        }
    }
}

