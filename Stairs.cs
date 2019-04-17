using System;
using System.Numerics;

namespace Stairs
{
    class Program
    {
        static void Main()
        {
            var count = int.Parse(Console.ReadLine());
            var opt = new BigInteger[count + 1, count + 1];
            opt[0, 0] = 1;
            for (var i = 0; i <= count; i++)
            {
                for (var j = 1; j <= i; j++)
                    opt[i, j] += opt[i - j, j - 1];
                for (var j = 1; j <= count; j++)
                    opt[i, j] += opt[i, j - 1];
            }
            
            Console.WriteLine(opt[count, count] - 1);
        }
    }
}
