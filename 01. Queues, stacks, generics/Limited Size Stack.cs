using System.Collections.Generic;
using System.Linq;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        public LinkedList<T> Stack = new LinkedList<T>();
        public int Size;
        public LimitedSizeStack(int limit)
        {
            Size = limit;
        }

        public void Push(T item)
        {
            Stack.AddLast(item);
            if (Stack.Count > Size) 
                Stack.RemoveFirst();
        }

        public T Pop()
        {
           var lastElement = Stack.ElementAt(Count - 1);
           Stack.RemoveLast();
           return lastElement;
        }

        public int Count
        {
            get
            {
                return Stack.Count();
            }
        }
    }
}
