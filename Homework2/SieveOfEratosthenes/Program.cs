using System;

namespace SieveOfEratosthenes
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] a = new bool[100];
            Array.Clear(a, 0, 100); //数组全部置false
            a[0] = true; //排除1
            int i,j;
            for(i=2; i<=100; i++)
            {
                if(a[i-1]==false)
                {
                    j = 2*i;
                    while(j<=100)
                    {
                        a[j - 1] = true;
                        j += i;
                    }
                }
            }
            for(i = 1; i <= 100; i++)
            {
                if(a[i-1]==false)
                    Console.Write($"{i}\t");
            }
        }
    }
}
