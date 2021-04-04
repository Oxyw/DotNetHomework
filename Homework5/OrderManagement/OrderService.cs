using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    class OrderService
    {
        public List<Order> Orders { set; get; }

        public OrderService()
        {
            Orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            bool flag = false;
            foreach (Order o in Orders)
                if (o.Equals(order))
                    flag = true;
            if (flag)
                throw new ApplicationException($"添加错误：该订单已存在!");
            else
                Orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            bool flag = false;
            foreach (Order o in Orders)
                if (o.Equals(order))
                    flag = true;
            if (flag)
                Orders.Remove(order);
            else
                throw new ApplicationException($"删除错误：该订单不存在!");

        }

        //打印单个订单
        public void ShowOrder(Order o)
        {
            Console.WriteLine(o);
        }

        //打印所有订单
        public void ShowAll()
        {
            Orders.ForEach(o => Console.WriteLine(o));
        }

        //将订单按价格升序输出
        public void ShowByPrice()
        {
            var query = Orders.OrderBy(o => o.TotalPrice);
            query.ToList().ForEach(o => Console.WriteLine(o));
        }

        //对订单进行排序
        public void Sort()
        {
            Orders.Sort();
        }
        public void Sort(Comparison<Order> comparison)
        {
            Orders.Sort(comparison);
        }
    }
}
