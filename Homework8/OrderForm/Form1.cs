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
        public OrderService s1;   //订单服务对象
        public int selectedIndex;   //订单index
        public Order selectedOrder;

        public Form1()
        {
            InitializeComponent();

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
            order1.AddItem(new OrderDetails(book, 3));
            order1.AddItem(new OrderDetails(phone, 1));

            //生成订单项
            OrderDetails od1 = new OrderDetails(cup, 5);
            OrderDetails od2 = new OrderDetails(book, 2);
            OrderDetails od3 = new OrderDetails(phone, 1);

            //生成订单项列表
            List<OrderDetails> odli = new List<OrderDetails>();
            odli.Add(od1);
            odli.Add(od2);

            //生成订单order2
            Order order2 = new Order(c2, "WHU University", odli, DateTime.Now.AddSeconds(15));
            order2.AddItem(od3);

            //生成订单order3
            Order order3 = new Order(c3, "Lucy's Home", DateTime.Now.AddDays(1));
            order3.AddItem(new OrderDetails(cup, 100));

            //生成订单服务对象s1
            s1 = new OrderService();
            s1.AddOrder(order1);
            s1.AddOrder(order2);
            s1.AddOrder(order3);

            orders = s1.Orders;
            OrdersBindingSource.DataSource = orders;
            DetailsBindingSource.DataSource = s1.Orders[0].Items;
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
                OrdersBindingSource.DataSource = s1.Orders;
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
                    OrdersBindingSource.DataSource = s1.Orders;
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
            OrdersBindingSource.DataSource = s1.Orders;
            OrdersBindingSource.ResetBindings(false);
        }
    }
}
