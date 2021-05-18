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
    public partial class Form1 : Form
    {
        public List<Order> orders; //显示的orders列表
        public OrderService s1 = new OrderService();   //订单服务对象
        public int selectedIndex;   //订单index
        public Order selectedOrder;

        public Form1()
        {
            InitializeComponent();

            /*
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
            order1.AddItem(new OrderDetails(1, book, 3));
            order1.AddItem(new OrderDetails(2, phone, 1));

            //生成订单项
            OrderDetails od1 = new OrderDetails(3, cup, 5);
            OrderDetails od2 = new OrderDetails(4, book, 2);
            OrderDetails od3 = new OrderDetails(5, phone, 1);

            //生成订单项列表
            List<OrderDetails> odli = new List<OrderDetails>();
            odli.Add(od1);
            odli.Add(od2);

            //生成订单order2
            Order order2 = new Order(c2, "WHU University", odli, DateTime.Now.AddMinutes(15));
            order2.AddItem(od3);

            //生成订单order3
            Order order3 = new Order(c3, "Lucy's Home", DateTime.Now.AddDays(1));
            order3.AddItem(new OrderDetails(6, cup, 100));

            //生成订单服务对象s1
            s1 = new OrderService();
            s1.AddOrder(order1);
            s1.AddOrder(order2);
            s1.AddOrder(order3);
            */

            using (var db = new OrderContext())
            {
                var p1 = new Product {ProductId = 1, pname = "cup", price = 43.21 };
                var p2 = new Product { ProductId = 7, pname = "book", price = 66.78 };
                var p3 = new Product { ProductId = 100, pname = "phone", price = 6743.21 };
                db.Products.Add(p1);
                db.Products.Add(p2);
                db.Products.Add(p3);

                /*
                var c1 = new Client { ClientId = 5007, cname = "Tom" };
                var c2 = new Client { ClientId = 5008, cname = "Jerry" };
                var c3 = new Client { ClientId = 5008, cname = "Lucy" };
                */
                Client c1 = new Client(5007, "Tom");
                Client c2 = new Client(5008, "Jerry");
                Client c3 = new Client(5009, "Lucy");
                db.Clients.Add(c1);
                db.Clients.Add(c2);
                db.Clients.Add(c3);

                /*
                var order1 = new Order { Client = c1, Address = "The Fifth Avenue", OrderTime = DateTime.Now };
                order1.Items = new List<OrderDetails>()
                {
                    new OrderDetails() { Id = 1, Product = p2, Quantity = 3 },
                    new OrderDetails() { Id = 2, Product = p3, Quantity = 1 },
                };
                */
                Order order1 = new Order(c1, "The Fifth Avenue", DateTime.Now);
                order1.AddItem(new OrderDetails(1, p2, 3));
                order1.AddItem(new OrderDetails(2, p3, 1));

                db.Orders.Add(order1);

                /*
                var od3 = new OrderDetails { Id = 3, Product = p1, Quantity = 5 };
                var od4 = new OrderDetails { Id = 4, Product = p2, Quantity = 2 };
                var od5 = new OrderDetails { Id = 5, Product = p3, Quantity = 1 };
                var od6 = new OrderDetails { Id = 6, Product = p1, Quantity = 100 };
                db.OrderItems.Add(od3);
                db.OrderItems.Add(od4);
                db.OrderItems.Add(od5);
                db.OrderItems.Add(od6);

                var odli = new List<OrderDetails>() { od3, od4, od5 };
                var order2 = new Order { Client = c2, Address = "WHU University", OrderTime = DateTime.Now.AddMinutes(15), Items = odli };
                */
                OrderDetails od3 = new OrderDetails(3, p1, 5);
                OrderDetails od4 = new OrderDetails(4, p2, 2);
                OrderDetails od5 = new OrderDetails(5, p3, 1);
                List<OrderDetails> odli = new List<OrderDetails>();
                odli.Add(od3);
                odli.Add(od4);
                odli.Add(od5);
                Order order2 = new Order(c2, "WHU University", odli, DateTime.Now.AddMinutes(15));

                db.Orders.Add(order2);

                /*
                var order3 = new Order { Client = c3, Address = "Lucy's Home", OrderTime = DateTime.Now.AddDays(1), Items = new List<OrderDetails>() { od6 } };
                */
                Order order3 = new Order(c3, "Lucy's Home", DateTime.Now.AddDays(1));
                order3.AddItem(new OrderDetails(6, p1, 100));

                db.Orders.Add(order3);

                s1.orders.Add(order1);
                s1.orders.Add(order2);
                s1.orders.Add(order3);

                db.SaveChanges();
            }

            orders = s1.orders;
            //OrdersBindingSource.DataSource = orders;
            //DetailsBindingSource.DataSource = s1.Orders[0].Items;

            OrdersBindingSource.DataSource = s1.QueryAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchInfo = textSearch.Text;
            orders = s1.SearchOrder(comboBox1.SelectedIndex + 1, searchInfo);
            OrdersBindingSource.DataSource = orders;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedIndex = e.RowIndex;
            selectedOrder = orders[selectedIndex];
            DetailsBindingSource.DataSource = selectedOrder.Items;
            DetailsBindingSource.ResetBindings(false);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = saveFileDialog1.FileName;
                s1.Export(fileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {
                String fileName = openFileDialog1.FileName;
                s1.Import(fileName);
                OrdersBindingSource.DataSource = s1.orders;
                DetailsBindingSource.ResetBindings(false);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(new Order(), s1);
            try
            {
                if (f2.ShowDialog() == DialogResult.OK)
                {
                    s1.AddOrder(f2.CurrentOrder);
                    OrdersBindingSource.DataSource = s1.orders;
                    OrdersBindingSource.ResetBindings(false);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Order theOrder = OrdersBindingSource.Current as Order;
            if (theOrder == null)
            {
                MessageBox.Show("请选择一个订单！");
                return;
            }
            Form2 f2 = new Form2(theOrder, s1);
            if (f2.ShowDialog() == DialogResult.OK)
            {
                OrdersBindingSource.ResetBindings(false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Order theOrder = OrdersBindingSource.Current as Order;
            if (theOrder == null)
            {
                MessageBox.Show("请选择一个订单!");
                return;
            }
            s1.RemoveOrder(theOrder);
            OrdersBindingSource.DataSource = s1.orders;
            OrdersBindingSource.ResetBindings(false);
        }
    }
}
