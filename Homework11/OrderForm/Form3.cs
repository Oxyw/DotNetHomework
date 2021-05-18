using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderForm
{
    public partial class Form3 : Form
    {
        public OrderDetails Detail { get; set; }
        public Form3()
        {
            InitializeComponent();
        }
        public Form3(OrderDetails detail)
        {
            InitializeComponent();
            this.Detail = detail;
            if(detail.Product != null)
            {
                textpid.Text = detail.Product.ProductId.ToString();
                textpname.Text = detail.Product.pname;
                textprice.Text = detail.Product.price.ToString();
                textpnum.Text = detail.Quantity.ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Product p = new Product(int.Parse(textpid.Text), textpname.Text, double.Parse(textprice.Text));
            Detail.Product = p;
            Detail.Quantity = int.Parse(textpnum.Text);
            Detail.ItemPrice = Detail.Quantity * Detail.Product.price;
        }
    }
}
