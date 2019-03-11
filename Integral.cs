using System;

namespace Integral
{
    class Program
    {
        static void Main(string[] args)
        {
            var epsilon = Convert.ToDouble(Console.ReadLine());
            var a = Convert.ToDouble(Console.ReadLine());
            var b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(MyIntegral.GetIntegralLeftRectangle(y => y * y, a, b, epsilon));
            Console.WriteLine(MyIntegral.GetIntegralRightRectangle(y => y * y, a, b, epsilon));
            Console.WriteLine(MyIntegral.GetIntegralTrapeze(y => y * y, a, b, epsilon));
        }
    }

    public class MyIntegral
    {   
        public static double GetIntegralLeftRectangle(Func<double, double> function, double a, double b, double epsilon)
        {
            var sum = function(a);
            var h = b - a;
            var square1 = 0.0;
            var square2 = h * sum;
            while (Math.Abs(square2 - square1) >= epsilon)
            {
                square1 = square2;
                for (var x = a + h / 2; x < b; x += h)
                    sum += function(x);
                h /= 2;
                square2 = sum * h;
            }

            return square2;
        }
        
        public static double GetIntegralRightRectangle(Func<double, double> function, double a, double b, double epsilon)
        {
            var sum = function(b);
            var h = b - a;
            var square1 = 0.0;
            var square2 = h * sum;
            while (Math.Abs(square2 - square1) >= epsilon)
            {
                square1 = square2;
                for (var x = b - h / 2; x > a; x -= h)
                    sum += function(x);
                h /= 2;
                square2 = sum * h;
            }

            return square2;
        }
        
        public static double GetIntegralTrapeze(Func<double, double> function, double a, double b, double epsilon)
        {
            var sum = (function(a) + function(b)) / 2;
            var h = b - a;
            var square1 = 0.0;
            var square2 = h * sum;
            while (Math.Abs(square2 - square1) >= epsilon)
            {
                square1 = square2;
                for (var x = a + h / 2; x < b; x += h)
                    sum += function(x);
                h /= 2;
                square2 = sum * h;
            }

            return square2;
        }
    }
}
