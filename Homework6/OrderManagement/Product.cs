using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    [Serializable]
    public class Product
    {
        public int pid;
        public string pname;
        public double price;

        public Product()
        {
        }
        public Product(int id, string name, double price)
        {
            pid = id;
            pname = name;
            this.price = price;
        }
        public override string ToString()
        {
            return (pname + "\t" + "￥" + price + "\t");
        }
    }
}
