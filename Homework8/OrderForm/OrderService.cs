using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace OrderForm
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

        public List<Order> SearchOrder(int opt, string info)
        {
            switch (opt)
            {
                case 0: //所有订单
                    return Orders;
                case 1: //订单时间查询
                    var query1 = from od1 in Orders
                                 where od1.OrderTime.Date == Convert.ToDateTime(info).Date
                                 select od1;
                    return query1.ToList();
                case 2: //商品名称查询
                    var query2 = from od2 in Orders
                                 from items in od2.Items
                                 where items.Product.pname == info
                                 select od2;
                    return query2.ToList();
                case 3: //客户姓名查询
                    var query3 = from od3 in Orders
                                 where od3.Client.cname == info
                                 orderby od3.TotalPrice
                                 select od3;
                    return query3.ToList();
                case 4: //订单地址
                    var query4 = from od4 in Orders
                                 where od4.Address == info
                                 orderby od4.TotalPrice
                                 select od4;
                    return query4.ToList();
                default:
                    return null;
            }
        }
    }
}
