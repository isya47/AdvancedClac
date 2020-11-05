using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;
using static AdvancedClac.TokenTypeEnum;

//Перегрузить операции для всех типов чисел
//Сделать умножение для переменных по умолчанию 
namespace AdvancedClac
{    
    public class MathFunc
    {
        private static Dictionary<string, int> initial()
        {
            return(new Dictionary<string, int> {{"pow",4},{"sin",0},{"cos",0},{"tan",0},{"*",3},{"/",3},{"+",2},{"-",2},{"|",1},{"&",2},{"~",4},{"^",2}});
        }

        internal static void oplogic(ref Stack operators, string operatorflag)
        {
            if (operatorflag == (string) operators.Peek())
            {
                operators.Pop();
                operators.Push("+");
            }
            else 
            {
                operators.Pop();
                operators.Push("-");
            }
        }
        internal static void SYA(ref Queue operands,ref Stack operators,string temp)
        {
            Dictionary<string, int> precedence = initial();
            while (operators.Count != 0 && precedence.ContainsKey((string) operators.Peek()) &&
                   (precedence[(string) operators.Peek()] > precedence[temp] ||
                    (precedence[(string) operators.Peek()] == precedence[temp] && temp != "pow")))
            {
                operands.Enqueue(operators.Pop());
            }
            operators.Push(temp);
        }

        internal static dynamic NumParse(string a)
        {
            dynamic b = a;
            if (!b.Contains('-') && b.Contains('.'))
                return(decimal.Parse(b));
            if (b.Contains('-') && b.Contains('.'))
                return(decimal.Parse(b) * -1);
            if (b.Contains('-')&& !b.Contains('.'))
                return(Int64.Parse(b) * -1);
            return(Int64.Parse(b));
        }

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
                case "~":
                    return ~(int)num1;
            }

            return 0;
        }
        
        internal static T Antiunitary<T>(T a, T b, string operator1)
        {
            dynamic num2 = a;
            dynamic result = b;

             try
            {
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
                        return Math.Pow(double.Parse(result.ToString()), double.Parse(num2.ToString()));
                    case "|":
                        return result | num2;
                    case "&":
                        return result & num2;
                    case "^":
                        return result ^ num2;
                }
            }

            catch (Exception e)
            {
                throw new Exception("Invalid antiunitary operation");
            }

            return a;
        }
        
//Функция исполняющая переведённое выражение 
        public static string Eval(Queue operands,Dictionary<char,string> vari=null)
        {
            Dictionary<string, int> precedence = initial();
            Stack result=new Stack();
            if (operands.Count == 0)
                return (null);
            if (operands.Count == 1)
                return (operands.Dequeue().ToString());
            while (operands.Count != 0)
            {
                if (result.Count == 0 && !(operands.Peek() is long || operands.Peek() is decimal||(operands.Peek() is string&&!precedence.ContainsKey((string) operands.Peek()))))
                {
                    Console.WriteLine("Math error: missing operands or wrong order");
                    return ("NULL");
                }
                
                if (operands.Peek() is string&&!precedence.ContainsKey((string) operands.Peek()))
                {
                    string tempstr = vari[operands.Peek().ToString()[0]];
                    long temp = 0;
                    decimal temp2 = 0;
                    if (!(vari.ContainsKey( operands.Peek().ToString()[0])))
                    {
                        Console.WriteLine("Enter value for '{0}': ", operands.Peek());
                        tempstr = Console.ReadLine();
                        while (long.TryParse(tempstr, out temp) != true || decimal.TryParse(tempstr, out temp2) != true)
                        {    
                            Console.WriteLine("Input is not of the correct format, try again: ");
                            Console.WriteLine("Enter value for '{0}'", operands.Peek().ToString()[1]);
                            tempstr = Console.ReadLine();
                        }
                    }
                    if(operands.Peek().ToString().Length>1)
                        result.Push(NumParse(tempstr)*-1);
                    else
                        result.Push(NumParse(tempstr));
                    operands.Dequeue();
                }
                else if (operands.Peek() is long||operands.Peek() is decimal)
                {
                    result.Push(operands.Dequeue());
                }
                else if (precedence[(string)operands.Peek()]>0&&(string)operands.Peek()!="~")
                {
                    result.Push(Antiunitary(result.Pop(), result.Pop(), (string) operands.Dequeue()));
                }
                else if (!(precedence[(string)operands.Peek()]>0)||(string)operands.Peek()=="~")
                {
                    result.Push(Unitary(Double.Parse(result.Pop().ToString()),(string)operands.Dequeue()));
                }
                else
                {
                    return ("Math error");
                }

                
            }
            
             //for rounding off
             if (result.Peek() is decimal)
                 return (((decimal)result.Pop() / 1.000000000000000000000000000000000m).ToString());
             else
                 return (result.Pop().ToString());
        }
//Функция для переводе данных в обратную польскую запись: https://ru.wikipedia.org/wiki/Алгоритм_сортировочной_станции
        //https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        public static Queue Parsing(IEnumerable<Token> stream)
        {
            bool valueflag = false;
            bool impliflag = false;
            Dictionary<string, int> precedence = initial();
            //Стек для операторов, функции и скобок
            Stack operators = new Stack();
            //Очередь для чисел а так же то что возращяется из метода
            Queue operands= new Queue();
            string operatorflag = null;
            //Алгоритм для преврощения строки в очередь в обратную польскую запись
            foreach (var i in stream)
            {

                //Console.WriteLine(i.Value);
                //работа со скобками
                if (i.Value == "(")
                    
                {
                    if (operatorflag!=null)
                    {
                        oplogic(ref operators, operatorflag);
                    }
                    if (impliflag)
                        SYA(ref operands,ref operators, "*");
                    operators.Push(i.Value);
                    valueflag = false;
                    impliflag = false;
                }
                    else if (i.Value == ")"||i.Value==",")
                    {
                        while ((string)operators.Peek() != "(")
                        {
                            if (operators.Count == 0)
                            {
                                operands = new Queue();
                                operands.Enqueue("Syntax error");
                                return (operands);
                            }
                            operands.Enqueue( operators.Pop());
                            valueflag = true;
                        }
                        operators.Pop();
                        
                        if(operators.Count!=0&&precedence[(string)operators.Peek()]==0)
                            operands.Enqueue(operators.Pop());
                        if (i.Value == ",")
                        {
                            operators.Push("(");
                            valueflag = false;
                            impliflag = false;
                        }
                        else
                        {
                            impliflag = true;
                        }
                    }

                    else if (i.TokenType ==  Variables)
                    {
                        for (int l = 0; l < i.Value.Length; l++)
                        {
                            if (l+2 <i.Value.Length&&precedence.ContainsKey(i.Value.Substring(l,3)))
                            {
                                if (operatorflag!=null)
                                {
                                    oplogic(ref operators, operatorflag);
                                }
                                if(impliflag==true)
                                    SYA(ref operands,ref operators, "*");
                                operators.Push(i.Value.Substring(l,3));
                                l += 2;
                                impliflag = false;
                            }
                            else
                            {
                                //fix
                                if (impliflag == true)
                                {
                                    SYA(ref operands, ref operators, "*");
                                }
                                
                                if(operatorflag==null)
                                operands.Enqueue(i.Value[l].ToString());
                                else if(operatorflag=="-"&&valueflag==false)
                                    operands.Enqueue(i.Value[l].ToString()+'-');
                                /*
                                if (operatorflag != null && valueflag == false && (string) operators.Peek() == "-" &&
                                    !variables.ContainsKey(i.Value[l].ToString()) &&
                                    (variables[i.Value[l].ToString()] != "Null" && variables[i.Value[l].ToString()] != "-Null"))
                                {
                                    if (variables[i.Value[l].ToString()].Contains('.'))
                                        variables[i.Value[l].ToString()] = (decimal.Parse(variables[i.Value[l].ToString()]) * -1).ToString();
                                    else
                                        variables[i.Value[l].ToString()] = (Int64.Parse(variables[i.Value[l].ToString()]) * -1).ToString();
                                }
                                
                                if(operatorflag==null)
                                    variables.Add(i.Value[l]+counts[i.Value[l].ToString()].ToString(),"Null");
                                else if(operatorflag!=null&&valueflag==false)
                                    variables.Add(i.Value[l]+counts[i.Value[l].ToString()].ToString(),"-Null");
                                    */
                                operatorflag = null;
                                valueflag = true;
                                impliflag = true;
                            }
                        }
                    }
                else if (i.TokenType == Numbers)
                    {
                        if (operatorflag!=null && valueflag == false &&  operatorflag == "-" &&
                            i.Value.Contains('.'))
                        {
                            operands.Enqueue(decimal.Parse(i.Value) * -1);
                        }
                        else if (operatorflag!=null && valueflag == false && (string) operatorflag== "-" && !i.Value.Contains('.'))
                        {
                            operands.Enqueue(long.Parse(i.Value) * -1);
                        }
                        else if (!(operatorflag != null && valueflag == false && (string) operatorflag == "-") &&
                                 i.Value.Contains('.'))
                        {
                            operands.Enqueue(decimal.Parse(i.Value));
                        }
                        else if (!(operatorflag!=null && valueflag == false && (string) operatorflag== "-") &&
                                 !i.Value.Contains('.'))
                        {
                            operands.Enqueue(long.Parse(i.Value));

                        }
                        
                            operatorflag=null;
                        
                        valueflag = true;
                            impliflag = true;
                    }
                   else if (i.TokenType==TokenTypeEnum.Operators){
                    string temp = i.Value;
                    if (operatorflag=="+"||operatorflag=="-")
                    {
                        valueflag = false;
                        if (temp == operatorflag)
                            operatorflag = "+";
                        else
                            operatorflag = "-";

                    }
                    else
                    {
                        if (valueflag == true||temp=="~")
                        {
                            //Console.WriteLine(i.GetValue);
                            //работа с оператором в стаке для операторов, разбор приоретета оператора
                            SYA(ref operands, ref operators, temp);
                            valueflag = false;
                        }
                        else
                        {
                            operatorflag = temp;
                        }
                    }
                    impliflag = false;
                }
                //if(operators.Count!=0)
                    //Console.WriteLine("peek '{0}'",operators.Peek());
            }
            //Любые оставшиеся операторы добавляются в конец очереди
            while (operators.Count != 0)
            {
                operands.Enqueue((string)operators.Pop());
            }
        /*
            Console.WriteLine("start");
            while (operands.Count != 0)
            {
                Console.WriteLine(operands.Dequeue());
            }
            Console.WriteLine("end");
            */
            return(operands);
        }

        
        public static string[] MultExec(IEnumerable<Token> stream, Dictionary<char,string>[]vari)
        {
            Queue Compiled = MathFunc.Parsing(stream);
            string[] output = new string[] { };
            foreach (var l in vari)
            {
                Console.WriteLine(MathFunc.Eval(Compiled, l));
                output.Append(MathFunc.Eval(Compiled, l));
            }

            return (output);
        }
        
    }
}