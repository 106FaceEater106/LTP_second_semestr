using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Program 
    { 
        static void Main() 
        { 
            var str = Console.ReadLine(); 
            var count = 0; 
            var dict = new Dictionary<StringBuilder, bool>(); 
            for (var position = 0; position < str.Length; position++) 
            { 
                var s = new StringBuilder(); 
                if (position != str.Length - 1 && dict.ContainsKey(new StringBuilder( str[position] + str[position + 1])))
                    continue;
                for (var tmp = position; tmp < str.Length; tmp++) 
                { 
                    s.Append(str[tmp]); 
                    if (dict.ContainsKey(s)) continue; 
                    dict[s] = true; 
                    count++; 

                }
            } 
            Console.WriteLine(count); 
        } 
    } 
}
