using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //生成商品
                Product cup = new Product(1, "cup", 43.21);
                Product book = new Product(7, "book", 66.78);
                Product phone = new Product(100, "phone", 6724.38);

                //生成客户
                Client c1 = new Client(5007, "Tom");
                Client c2 = new Client(5008, "Jerry");
                Client c3 = new Client(5009, "Lucy");

                //生成订单order1
                Order order1 = new Order(c1, "The Fifth Avenue", DateTime.Now);
                order1.AddItem(new OrderDetails(book, 3));
                order1.AddItem(new OrderDetails(phone, 1));

                //生成订单项
                OrderDetails od1 = new OrderDetails(cup, 5);
                OrderDetails od2 = new OrderDetails(book, 2);
                OrderDetails od3 = new OrderDetails(phone, 1);

                //生成订单项列表
                List<OrderDetails> odli = new List<OrderDetails>();
                odli.Add(od1);
                odli.Add(od2);

                //生成订单order2
                Order order2 = new Order(c2, "WHU University", odli, DateTime.Now.AddSeconds(15));
                order2.AddItem(od3);

                //生成订单order3
                Order order3 = new Order(c3, "Lucy's Home", DateTime.Now.AddDays(1));
                order3.AddItem(new OrderDetails(cup, 100));

                //生成订单服务对象s1
                OrderService s1 = new OrderService();
                s1.AddOrder(order1);
                s1.AddOrder(order2);
                s1.AddOrder(order3);

                //s1.ShowOrder(order2);

                Console.WriteLine("Show all the orders:");
                s1.ShowAll();

                Console.WriteLine("Order by the total price:");
                s1.ShowByPrice();

                Console.WriteLine("Query the order whose totalprice < 7000:");
                var query1 = from s in s1.Orders
                             where s.TotalPrice < 7000
                             orderby s.OrderTime descending
                             select s;
                query1.ToList().ForEach(o => Console.WriteLine(o));

                Console.WriteLine("Sort orders by client's name:");
                s1.Sort( (o1, o2) => o2.Client.cname.CompareTo(o1.Client.cname) );
                s1.ShowAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
