using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    [Serializable]
    public class Order
    {
        public string OrderId { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public List<OrderDetails> Items { get; set; }
        public double TotalPrice { get => Items.Sum(d => d.ItemPrice); }

        public DateTime OrderTime { get; set; }

        public Order()
        {
            OrderId = Guid.NewGuid().ToString(); //生成订单编号
            Items = new List<OrderDetails>();
            OrderTime = DateTime.Now;
        }

        public Order(string name, string ad, DateTime time)
        {
            OrderId = System.Guid.NewGuid().ToString(); //生成订单编号
            ClientName = name;
            Address = ad;
            Items = new List<OrderDetails>();
            OrderTime = time;
        }
        public Order(string name, string ad, List<OrderDetails> it, DateTime time)
        {
            OrderId = System.Guid.NewGuid().ToString(); //生成订单编号
            ClientName = name;
            Address = ad;
            Items = it;
            OrderTime = time;
        }

        public override string ToString()
        {
            return $"订单号: {OrderId}, 订单价格: ￥{TotalPrice}, 客户名称: {ClientName}, 订单详情：[{string.Join<OrderDetails>(",", Items)}]";
        }

        public override int GetHashCode()
        {
            int hashCode = -1723691444;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClientName);
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<OrderDetails>>.Default.GetHashCode(Items);
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderId == order.OrderId;

        }
    }
}
