using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace OrderApi.Models
{
    [Serializable]
    public class OrderDetails
    {
        public OrderDetails()
        {
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double ItemPrice { get; set; }

        public OrderDetails(string pName, int pNumber, double price)
        {
            ProductName = pName;
            Quantity = pNumber;
            UnitPrice = price;
            ItemPrice = price * Quantity;
        }

        public override bool Equals(object obj)
        {
            OrderDetails oddt = obj as OrderDetails;
            return oddt != null && this.ProductName == oddt.ProductName && this.Quantity == oddt.Quantity;
        }

        public override string ToString()
        {
            return $"商品名称：{ProductName} 商品数量：{Quantity} 单价：￥{UnitPrice} 明细价格：￥{ItemPrice}";
        }

        public override int GetHashCode()
        {
            int hashCode = 699931452;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }
    }
}
