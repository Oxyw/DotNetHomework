using System;

namespace List
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0, max, min, n = 0;
            Console.Write("请输入链表元素个数：");
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("解析错误" + e.Message);
            }
            //n = int.Parse(Console.ReadLine());
            
            Random rd = new Random();
            GenericList<int> list = new GenericList<int>();
            Console.WriteLine("正在创建随机链表...");
            for (int i = 0; i < n; i++)
            {
                list.Add(rd.Next(0,100));
            }
            Console.WriteLine("链表已创建！");

            max = min = list.Head.Data;

            list.ForEach(n => Console.WriteLine(n));
            list.ForEach(n => sum += n);
            list.ForEach(n => { max = max > n ? max : n; });
            list.ForEach(n => { min = min < n ? min : n; });

            Console.WriteLine("该链表元素的最大值是：" + max);
            Console.WriteLine("该链表元素的最小值是：" + min);
            Console.WriteLine("该链表元素的总和是：" + sum);
        }
    }
}
