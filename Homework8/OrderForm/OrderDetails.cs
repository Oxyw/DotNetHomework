using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderForm
{
    [Serializable]
    public class OrderDetails
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double ItemPrice { get; set; }

        public OrderDetails()
        {
        }
        public OrderDetails(Product p, int num)
        {
            Product = p;
            Quantity = num;
            ItemPrice = num * p.price;
        }

        public override bool Equals(object obj)
        {
            OrderDetails oddt = obj as OrderDetails;
            return oddt != null && this.Product.pid == oddt.Product.pid;
        }

        public override string ToString()
        {
            return "*" + '\t' + Product + '\t' + Quantity + '\t' + ItemPrice + '\t';
        }

        public override int GetHashCode()
        {
            return Product.GetHashCode();
        }
    }
}
