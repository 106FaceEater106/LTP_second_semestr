using System;
using System.Collections.Generic;
using System.Linq;

namespace Leonardo
{
    namespace Leonardo
    {
        public class Program
        {
            static void Main()
            {
                Console.Write("Введите порядковый номер желаемого числа -> ");
                var leonardo = new Leonardo();
                Console.WriteLine(leonardo.GetLeonardoNumber(int.Parse(Console.ReadLine())).Last());
            }
        }

        public class Leonardo
        {
            private int prev = 1;
            private int prevPrev = 1;

            public IEnumerable<int> GetLeonardoNumber(int count)
            {
                for (var i = 0; i < count; i++)
                {
                    var number = prev + prevPrev + 1;
                    prevPrev = prev;
                    prev = number;
                    yield return number;
                }
            }
        }
    }
}
