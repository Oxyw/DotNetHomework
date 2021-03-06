using System;

namespace ConsoleApp1
{
    class Program
    {
        static int Main(string[] args)
        {
            double m, n = 0;
            double result = 0;
            string sign;
            Console.WriteLine("请输入第1个操作数：");
            m = double.Parse(Console.ReadLine());
            Console.WriteLine("请输入第2个操作数：");
            n = double.Parse(Console.ReadLine());
            Console.WriteLine("请输入操作符：");
            sign = Console.ReadLine();
            switch (sign)
            {
                case "+":
                    result = m + n;
                    break;
                case "-":
                    result = m - n;
                    break;
                case "*":
                    result = m * n;
                    break;
                case "/":
                    while(n==0)
                    {
                        Console.WriteLine("请输入正确的除数：");
                        n = double.Parse(Console.ReadLine());
                    }
                    result = m / n;
                    break;
                case "%":
                    result = m % n;
                    break;
                default:
                    Console.WriteLine("操作符错误！");
                    return 0;
            }
            Console.WriteLine($"{m}{sign}{n}={result}");
            return 0;
        }
    }
}