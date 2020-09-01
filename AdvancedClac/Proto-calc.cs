using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


//Убрать ПОСТ
//переформатировать в токены
//
namespace AdvancedClac
{
    public class MathFunc
    {
        //Метод инициализации данных операторов. Это нужно для более простых вычислении о порядке оператаров.
        //Не уверен какую структуру тут надо использовать
        private enum precedence : ushort
        {
            Add=2,
            Substract=2,
            Multiply=3,
            Divide=3,
            Pow=4,
            Sin=4,
            Cos=4,
            Abs,
            Tan=4,
            
            Or,
            And,
            Xor,
        }

      
        internal int Exec(int num2, int result, char operator1){

            
            switch (operator1)
            {
                
                case '/':
                    result = result / num2;
                    break;
                case '*':
                    result = result * num2;
                    break;
                case '+':
                    result = result + num2;
                    break;
                case '-':
                    result = result - num2;
                    break;
                case '^':
                    int temp = result;
                    for (int i = 1; i < num2; i++)
                    {
                        //не знаю есть смысл или нет но тут можно сделать goto к кейсам 1 и 2
                        result *= temp;

                    }
                    break;
            }

            return result;
            }
//Функция исполняющая переведённое выражение 
        public string Eval(Queue operands)
        {
            Stack result=new Stack();
            if (operands.Count == 0)
                return ("NULL");
            do
            {
                if (result.Count == 1 && (operands.Peek() == (object) '-') && (operands.Peek() == (object) '+'))
                    result.Push(Exec(0, (int) result.Pop(), (char) operands.Dequeue()));
                else if (!(operands.Peek() is int))
                {
                    result.Push(Exec((int) result.Pop(), (int) result.Pop(), (char) operands.Dequeue()));
                }
                else if (operands.Peek() is int)
                {
                    result.Push(operands.Dequeue());
                }
                else
                {
                    return ("Syntax error");
                }


            } while (operands.Count != 0);

            return ( result.Pop().ToString());
        }
//Функция для переводе данных в обратную польскую запись: https://ru.wikipedia.org/wiki/Алгоритм_сортировочной_станции
        public Queue Parsing( IEnumerable<Token> stream)
        {
            //Стек для операторов, функции и скобок
            Stack operators = new Stack();
            //Очередь для чисел а так же то что возращяется из метода
            Queue operands= new Queue();
            //Счетчик: не то что бы нужен если использовать цыкл for но особо без разницы
            int count = 0;
            //Отсчитывает началокакого то числа для substring
            int andstart = 0;
            //Счетчик цифр
            int nums = 0;
            //Для tryparse
            int temp;
            //Алгоритм для преврощения строки в очередь в обратную польскую запись
            foreach (Token i in stream)
            {
                //Кейс оператор
                if(!Int32.TryParse(i.ToString(),out temp))
                {
                    //работа со скобками
                    if (i == '(')
                    {
                        operators.Push(i);
                    }
                    else if (i == ')')
                    {
                        operands.Enqueue(Int32.Parse(prop.Data.Substring(andstart, count-andstart)));
                        while ((char)operators.Peek() != '(')
                        {
                            if (operators.Count == 0)
                            {
                                prop.Data = "Fail, parenthesis mismatch";
                                return new Queue();
                            }

                            operands.Enqueue((char) operators.Pop());
                        }

                        operators.Pop();

                        
                    }
                    
                    //Если оператор найден, то цыфры до него переводятся
                    else if (precedence.ContainsKey(i))
                    {
                        if(andstart!=count)
                            operands.Enqueue(Int32.Parse(prop.Data.Substring(andstart, count-andstart)));
                        //работа с оператором в стаке для операторов, разбор приоретета оператора
                        while (operators.Count != 0 && precedence.ContainsKey((char) operators.Peek()) &&
                               (precedence[(char) operators.Peek()] > precedence[i] ||
                                (precedence[(char) operators.Peek()] == precedence[i] && i != '^')))
                        {
                            


                            operands.Enqueue(operators.Pop());
                            
                        }
                        operators.Push(i);
                    }


                    andstart = count;
                    andstart++;
                }

                count++;
                //Console.WriteLine("end");
                
            }
            //Добавление последнего числа в очередь
            if (count != prop.Data.Length)
            {
                operands.Enqueue(Int32.Parse(prop.Data.Substring(andstart, prop.Data.Length - andstart)));
            }

            //Любые оставшиеся операторы добавляются в конец очереди
            while (operators.Count != 0)
            {
                operands.Enqueue((char) operators.Pop());
            }
            /*Console.WriteLine(operands.Count);
            while(operands.Count!=0)
                Console.WriteLine(operands.Dequeue());
*/
            return(operands);
            
        }
    }
}