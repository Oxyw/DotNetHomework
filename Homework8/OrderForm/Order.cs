using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderForm
{
    [Serializable]
    public class Order : IComparable<Order>
    {
        public string OrderID { get; set; }
        public Client Client { get; set; }
        public string Address { get; set; }
        public List<OrderDetails> Items { get; set; }
        public double TotalPrice { get; set; }

        public DateTime OrderTime { get; set; }

        public Order()
        {
            OrderID = System.Guid.NewGuid().ToString(); //生成订单编号
            Items = new List<OrderDetails>();
            OrderTime = DateTime.Now;
        }
        public Order(Client c, string ad, DateTime time)
        {
            OrderID = System.Guid.NewGuid().ToString(); //生成订单编号
            Client = c;
            Address = ad;
            Items = new List<OrderDetails>();
            OrderTime = time;
            TotalPrice = 0;
        }
        public Order(Client c, string ad, List<OrderDetails> it, DateTime time)
        {
            OrderID = System.Guid.NewGuid().ToString(); //生成订单编号
            Client = c;
            Address = ad;
            Items = it;
            foreach (OrderDetails m in Items)
                TotalPrice += m.ItemPrice;
            OrderTime = time;
        }

        public void AddItem(OrderDetails item)
        {
            bool flag = false;
            foreach (OrderDetails m in Items)
                if (m.Equals(item))
                    flag = true;
            if(flag)
                throw new ApplicationException($"添加错误：该订单项已存在!");
            else
            {
                Items.Add(item);
                TotalPrice += item.ItemPrice;
            }
        }

        public void RemoveItem(OrderDetails item)
        {
            bool flag = false;
            foreach (OrderDetails m in Items)
                if (m.Equals(item))
                    flag = true;
            if(flag)
            {
                Items.Remove(item);
                TotalPrice -= item.ItemPrice;
            }
            else
                throw new ApplicationException($"删除错误：该订单项不存在!");
            
        }

        public int CompareTo(Order other)
        {
            if (other == null) return 0;
            int id1 = Convert.ToInt32(this.OrderID.Remove(0, 1));
            int id2 = Convert.ToInt32(other.OrderID.Remove(0, 1));
            return id1 - id2;
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            Order od = obj as Order;
            return od != null && this.OrderID == od.OrderID;
        }

        public override string ToString()
        {
            string s = "OrderID: " + OrderID + '\n' + Client + '\n' + "Address: " + Address + '\n';
            s += "[Item]" + '\t' +  "Product" + '\t' + "Unit Price" + '\t' + "Quantity" + '\t' +"ItemPrice" + '\t' + '\n';
            foreach (OrderDetails item in Items)
                s += item + "\n";
            s += "OrderPrice: " + TotalPrice + '\n' + "OrderTime:" + OrderTime + '\n';
            return s;
        }

        public override int GetHashCode()
        {
            return OrderID.GetHashCode();
        }
    }
}
