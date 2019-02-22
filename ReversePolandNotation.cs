using System;
using System.Collections.Generic;
using System.Linq;

namespace RPN
{
    public class Stack<T>
    {
        private readonly LinkedList<T> myStack = new LinkedList<T>();

        public void Push(T item)
        {
            myStack.AddLast(item);
        }

        public T Pop()
        {
            var lastElement = myStack.ElementAt(Count - 1);
            myStack.RemoveLast();
            return lastElement;
        }

        public T Peek()
        {
            return myStack.ElementAt(Count - 1);
        }

        public int Count
        {
            get { return myStack.Count; }
        }
    }

    public class ReversePolandNotation
    {
        private readonly Dictionary<string, int> priority = new Dictionary<string, int>
            {{"+", 2}, {"-", 2}, {"*", 3}, {"/", 3}, {"(", 1}, {")", 1}};

        double parseElement;

        public static void Main()
        {
            var rpn = new ReversePolandNotation();
            rpn.GetReversePolandNotation();
        }

        private void GetReversePolandNotation()
        {
            var expression = Console.ReadLine();
            var reverse = ToRpn(expression);
            Console.WriteLine(reverse);
            var answer = FromRpn(reverse);
            Console.WriteLine(answer);
        }

        private string ToRpn(string expression)
        {
            var splitExpression = expression.Split();
            var stack = new Stack<string>();
            var braceFound = false;
            var reverse = new List<string>();
            for (var i = 0; i < splitExpression.Length; i++)
            {
                var indexOfDot = splitExpression[i].IndexOf('.');
                if (indexOfDot != -1)
                    splitExpression[i] = splitExpression[i].Replace('.', ',');
                if (double.TryParse(splitExpression[i], out parseElement))
                    reverse.Add(splitExpression[i]);
                else 
                    switch (splitExpression[i].Replace(" ", ""))
                    {
                        case "(":
                            stack.Push(splitExpression[i]);
                            braceFound = true;
                            break;
                        case ")":
                        {
                            if (!braceFound)
                            {
                                Console.WriteLine("Wrong input");
                                Environment.Exit(1);
                            }

                            while (stack.Peek() != "(")
                                reverse.Append(stack.Pop());
                            stack.Pop();
                            braceFound = false;
                            break;
                        }
                        default:
                        {
                            if (stack.Count == 0)
                                stack.Push(splitExpression[i]);
                            else
                            {
                                while (priority[stack.Peek()] > priority[splitExpression[i]])
                                    reverse.Append(stack.Pop());
                                stack.Push(splitExpression[i]);
                            }

                            break;
                        }
                    }
            }

            if (braceFound)
            {
                Console.WriteLine("Wrong input");
                Environment.Exit(1);
            }

            while (stack.Count > 0)
                reverse.Add(stack.Pop());
            return string.Join(' ', reverse);
        }

        private string FromRpn(string reverse)
        {
            var stack = new Stack<double>();
            var splitReverse = reverse.Split();
            foreach (var e in splitReverse)
            {
                if (double.TryParse(e, out parseElement))
                    stack.Push(parseElement);
                else
                    switch (e)
                    {
                        case "+":
                            stack.Push(stack.Pop() + stack.Pop());
                            break;
                        case "-":
                            stack.Push(-stack.Pop() + stack.Pop());
                            break;
                        case "*":
                            stack.Push(stack.Pop() * stack.Pop());
                            break;
                        case "/":
                            if (stack.Peek() < double.Epsilon)
                            {
                                Console.WriteLine("Wrong input");
                                Environment.Exit(1);
                            }

                            stack.Push(1 / stack.Pop() * stack.Pop());
                            break;
                        default:
                            throw new ArgumentException();
                    }
            }
            
            return stack.Pop().ToString();
        }
    }
}
