using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

//переформатировать в токены
//Сделать умножение для переменных по умолчанию 
namespace AdvancedClac
{    public class Node
    {
        public readonly string type;
        public readonly string value;
        public unsafe void* parent;
        public unsafe void* left;
        public unsafe void* right;
    }
    public class MathFunc
    {
        private Dictionary<string,string> variables=new Dictionary<string, string>();
        //Метод инициализации данных операторов. Это нужно для более простых вычислении о порядке оператаров.
        private Dictionary<string, int> initial()
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

        internal double Unitary<T>(T a, string operator1)
        {
            double num1 = double.Parse(a);
            switch (operator1)
            {
                case "sin":
                    return Math.Sin(num1);
                case "cos":
                    return Math.Cos(num1);
                case "tan":
                    return Math.Tan(num1);
            }
        }
        
        internal T Antiunitary<T>(T a, T b, string operator1)
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
                    int temp = result;
                    for (int i = 1; i < num2; i++)
                    {
                        //не знаю есть смысл или нет но тут можно сделать goto к кейсам 1 и 2
                        result *= temp;

                    }
                    return result;
            }
        }
//Функция исполняющая переведённое выражение 
        public string Eval(Queue operands)
        {
            Dictionary<string, int> precedence = initial();
            Stack result=new Stack();
            if (operands.Count == 0)
                return ("NULL");
            while (operands.Count != 0)
            {
                if (operands.Peek() is long||operands.Peek() is double)
                {
                    result.Push(operands.Dequeue());
                }
                else if (!(operands.Peek() is long||operands.Peek() is double))
                {
                    result.Push(Antiunitary(result.Pop(), result.Pop(), (string) operands.Dequeue()));
                }
                else
                {
                    return ("Syntax error");
                }


            }

            return ( result.Pop().ToString());
        }
//Функция для переводе данных в обратную польскую запись: https://ru.wikipedia.org/wiki/Алгоритм_сортировочной_станции
        public Queue Parsing(IEnumerable<Token> stream)
        {
            Dictionary<string, int> precedence = initial();
            //Стек для операторов, функции и скобок
            Stack operators = new Stack();
            //Очередь для чисел а так же то что возращяется из метода
            Queue operands= new Queue();
            //Алгоритм для преврощения строки в очередь в обратную польскую запись
            foreach (var i in stream)
            {
                //работа со скобками
                    if (i.GetValue == "(")
                    {
                        operators.Push(i.GetValue);
                    }
                    else if (i.GetValue == ")"||i.GetValue==",")
                    {
                        while ((char)operators.Peek() != '(')
                        {
//Error check here for extra parentheses
                            operands.Enqueue((char) operators.Pop());
                        }
                        operators.Pop();
                        if(precedence[(string)operators.Peek()]==0)
                            operands.Enqueue(operators.Pop());
                        if(i.GetValue==",")
                            operators.Push("(");
                    }
                    else if (i.GetTokenType=="Operator")
                    {
                        //работа с оператором в стаке для операторов, разбор приоретета оператора
                        while (operators.Count != 0 && precedence.ContainsKey((string)operators.Peek()) &&
                               (precedence[(string)operators.Peek()] > precedence[i.GetValue] ||
                                (precedence[(string) operators.Peek()] == precedence[i.GetValue] && i.GetValue != "pow")))
                        {
                            operands.Enqueue(operators.Pop());
                            
                        }
                        operators.Push(i);
                    }
                    else if (i.GetTokenType == "Variable")
                    {
                        for (int l = 0; l < i.GetValue.Length; l++)
                        {
                            if (l+2 <i.GetValue.Length&&precedence.ContainsKey(i.GetValue.Substring(l,3)))
                            {
                                operators.Push(i.GetValue.Substring(l,3));
 
                            }
                            else
                            {
                                operands.Enqueue(i.GetValue[l]);
                                variables.Add(i.GetValue,"Null");
                            }
                        }
                    }
                    else if (i.GetTokenType == "Numbers")
                    {
                        if (i.GetValue.Contains('.'))
                        {
                            operands.Enqueue(double.Parse(i.GetValue));
                        }
                        else
                            operands.Enqueue(long.Parse(i.GetValue));
                    }
            }
            //Любые оставшиеся операторы добавляются в конец очереди
            while (operators.Count != 0)
            {
                operands.Enqueue((string)operators.Pop());
            }
            return(operands);
        }
    }
}

