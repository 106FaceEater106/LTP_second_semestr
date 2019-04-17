using System.Numerics;

namespace Tickets
{
    public static class TicketsTask
    {
        public static BigInteger Solve(int totalLen, int totalSum)
        {
            if (totalSum % 2 != 0)
                return 0;
            totalSum /= 2;
            var table = new BigInteger[totalLen + 1, totalSum + 1];
            table[0, 0] = 1;
            for (var i = 0; i <= totalSum; i++)
                for (var j = 1; j <= totalLen; j++)
                    for (var k = 0; k < 10 && i - k >=0; k++)
                        table[j, i] += table[j - 1, i - k];
            return table[totalLen, totalSum] * table[totalLen, totalSum];
        }
    }
}

