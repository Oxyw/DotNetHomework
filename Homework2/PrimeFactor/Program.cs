using System;

namespace PrimeFactor
{
    class Prime
    {
        static void Main(string[] args)
        {
            int num=0;
            Console.WriteLine("Please enter a number:");
            try {
                num = int.Parse(Console.ReadLine());
            }catch(Exception e)
            {
                Console.WriteLine("解析错误" + e.Message);
            }
            Console.WriteLine($"The prime factors of {num}:");
            Operate(num);
        }
        public static bool FindPrime(int n)
        {
            int i;
            int j = (int)System.Math.Sqrt(n);
            bool flag = true;
            for (i = 2;i<=j;i++)
            {
                if(n%i==0)
                {
                    flag = false;
                    return flag;
                }
            }
            return flag;
        }

        public static void Operate(int n)
        {
            int i = 2;
            while(i<=n)
            {
                if(n%i==0 && FindPrime(i)==true)
                {
                    Console.WriteLine(i);
                }
                i++;
            }
        }
    }
}
