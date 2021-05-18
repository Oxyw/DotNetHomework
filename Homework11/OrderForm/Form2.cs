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
    public partial class Form2 : Form
    {
        public OrderService os;
        public Order CurrentOrder { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Order order, OrderService os)
        {
            InitializeComponent();
            var query1 = from od1 in os.orders
                         select od1.Client;
            clientBindingSource.DataSource = query1.ToList();
            this.os = os;
            this.CurrentOrder = order;
            OrderbindingSource.DataSource = CurrentOrder;
            labelTime.Text = CurrentOrder.OrderTime.ToString();
            textAddr.Text = CurrentOrder.Address;
            if(order.Client != null)
            {
                cmbClient.SelectedItem = order.Client;
            }
            if (order.Items != null)
            {
                ItemBindingSource.DataSource = order.Items;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            
            var query1 = from od1 in os.orders
                         select od1.Client;
            CurrentOrder.Client = query1.ToList()[cmbClient.SelectedIndex];
            CurrentOrder.Address = textAddr.Text;
            //os.AddOrder(CurrentOrder);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3(new OrderDetails());
            //f3.ShowDialog();
            try
            {
                if (f3.ShowDialog() == DialogResult.OK)
                {
                    CurrentOrder.AddItem(f3.Detail);
                    ItemBindingSource.DataSource = CurrentOrder.Items;
                    ItemBindingSource.ResetBindings(false);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            OrderDetails theDetail = ItemBindingSource.Current as OrderDetails;
            if (theDetail == null)
            {
                MessageBox.Show("请选择一个订单项！");
                return;
            }
            Form3 f3 = new Form3(theDetail);
            if (f3.ShowDialog() == DialogResult.OK)
            {
                ItemBindingSource.ResetBindings(false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OrderDetails theDetail = ItemBindingSource.Current as OrderDetails;
            if (theDetail == null)
            {
                MessageBox.Show("请选择一个订单项！");
                return;
            }
            CurrentOrder.RemoveItem(theDetail);
            ItemBindingSource.DataSource = CurrentOrder.Items;
            ItemBindingSource.ResetBindings(false);
        }
    }
}
