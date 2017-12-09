using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class ReversePolishNotation
    {
        private static int GetPriority(char operate)
        {
            switch(operate)
            {
                /*case '^':
                    return 3;*/
                case '/':
                    return 2;
                case '*':
                    return 2;
                case '+':
                    return 1;
                case '-':
                    return 1;
                default:
                    return 0;                
            }
        }//Вспомогательная функция для определения приоритета операции
        private static bool IsNumeric(string expression)
        {
            
            return Double.TryParse(expression, out var n);
        }// Вспомогательная функция для определения является ли строка числом
        private static double Calculate(List<string> myList)
        {
            Stack<double> stack1 = new Stack<double>();
            double a;
            double b;
            while(myList.Count()>0)
            {
                switch (myList[0])
                {
                    case "+":
                        a = stack1.Pop();
                        b = stack1.Pop();
                        stack1.Push(a + b);
                        break;
                    case "-":
                        a = stack1.Pop();
                        b = stack1.Pop();
                        stack1.Push(b - a);
                        break;
                    case "*":
                        a = stack1.Pop();
                        b = stack1.Pop();
                        stack1.Push(a * b);
                        break;
                    case "/":
                        a = stack1.Pop();
                        b = stack1.Pop();
                        stack1.Push(b / a);
                        break;
                    /*case "^":
                        a = stack1.Pop();
                        b = stack1.Pop();
                        stack1.Push(Math.Pow(b, a));
                        break;*/
                    default:
                        stack1.Push(Convert.ToDouble(myList[0]));
                        break;
                }
                myList.RemoveAt(0);
            }
            return stack1.Pop();
        }//Вычисляет лист поданный в обратной польской нотации(ОПН)
        private static List<String> StringToList(String str) //Переводит входную строку в лист, для преобразования из инфиксной записи в ОПН
        {
            List<String> result = new List<String>();
            string buf = "";
            for (int i = 0; i<str.Length; i++)
            {
                
                switch(str[i])
                {
                    case '1':
                        buf += '1';
                        break;
                    case '2':
                        buf += '2';
                        break;
                    case '3':
                        buf += '3';
                        break;
                    case '4':
                        buf += '4';
                        break;
                    case '5':
                        buf += '5';
                        break;
                    case '6':
                        buf += '6';
                        break;
                    case '7':
                        buf += '7';
                        break;
                    case '8':
                        buf += '8';
                        break;
                    case '9':
                        buf += '9';
                        break;
                    case '0':
                        buf += '0';
                        break;
                    case ',':
                        buf += ',';
                        break;
                    default:
                        if (buf != "")
                        {
                            result.Add(buf);
                        }
                        buf = "";
                        result.Add(str[i].ToString());
                        break;

                }
            }
            if (buf != "")
            {
                result.Add(buf);
            }


            return result;
        }
        private static List<string> ToRPN(List<string> operandlist)
        {
            List<string> result = new List<string>();
            Stack<char> stackOperators = new Stack<char>();
            for (int i = 0; i < operandlist.Count; i++)
            {
                if (IsNumeric(operandlist[i]))
                {
                    result.Add(operandlist[i]);
                    continue;
                }
                if (stackOperators.Count == 0)
                {
                    stackOperators.Push(operandlist[i][0]);
                    continue;
                }
                if (operandlist[i][0] == '(')
                {
                    stackOperators.Push('(');
                    continue;
                }
                if (operandlist[i][0] == ')')
                {
                    while (stackOperators.Peek() != '(')
                    {
                        result.Add(Convert.ToString(stackOperators.Pop()));
                    }
                    stackOperators.Pop();
                    continue;
                }
                if (GetPriority(operandlist[i][0]) <= GetPriority(stackOperators.Peek()))
                {
                    while (stackOperators.Count != 0 & GetPriority(operandlist[i][0]) <= GetPriority(stackOperators.Peek()))
                    {
                        result.Add(Convert.ToString(stackOperators.Pop()));
                      
                    }
                }
                else
                {
                    stackOperators.Push(operandlist[i][0]);
                }
            }
            while (stackOperators.Count > 0)
            {
                result.Add(Convert.ToString(stackOperators.Pop()));
            }
            return result;
        }//Переводит из инфиксной записи в обратную польскую нотацию
        public static void Calc()
        {
            Console.WriteLine(Calculate(ToRPN(StringToList("0"+Console.ReadLine()))));//  добавляется в начало строки для корректной обработки строк ввида "-n..."
        }
    }
}
