using System;

namespace ArrayOperate
{
    class ArrayOp
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Now let's create an array.");
            Console.Write("Please enter the size of the array:");
            int n = 0;
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("解析错误" + e.Message);
            }
            int[] a = new int[n];
            InitializeArray(a);
            Operate1(a);
        }

        public static void InitializeArray(int[] a)
        {
            Console.WriteLine("Please enter some integer: ");
            for(int i=0; i<a.Length; i++)
            {
                try
                {
                    a[i] = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("解析错误" + e.Message);
                }
            }
            Console.WriteLine("Finished!");
        }

        public static void Operate1(int[] a)
        {
            int maxNum = a[0], minNum = a[0], sum = 0; ;
            foreach(int num in a)
            {
                sum += num;
                maxNum = (maxNum > num) ? maxNum : num;
                minNum = (minNum < num) ? minNum : num;
            }
            double average = (double)sum / a.Length;
            Console.WriteLine($"The max number of the array is: {maxNum}");
            Console.WriteLine($"The min number of the array is: {minNum}");
            Console.WriteLine($"The sum of the array is: {sum}");
            Console.WriteLine($"The average of the array is: {average}");
        }
    }
}
