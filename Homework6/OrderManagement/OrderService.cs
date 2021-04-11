using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace OrderManagement
{
    public class OrderService
    {
        public List<Order> Orders;

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
        public List<Order> ShowByPrice()
        {
            var query = Orders.OrderBy(o => o.TotalPrice);
            return query.ToList();
        }

        //对订单进行排序
        public void Sort(Comparison<Order> comparison)
        {
            Orders.Sort(comparison);
        }

        //将订单序列化为XML文件
        public void Export(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using(FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                xs.Serialize(fs, Orders);
            }
        }

        //从XML文件中载入订单
        public void Import(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                    Orders = (List<Order>)xs.Deserialize(fs);
            }
        }
    }
}
