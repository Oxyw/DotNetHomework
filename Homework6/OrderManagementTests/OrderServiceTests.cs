using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        //生成商品
        Product cup = new Product(1, "cup", 43.21);
        Product book = new Product(7, "book", 66.78);
        Product phone = new Product(100, "phone", 6724.38);
        Product flower = new Product(77, "flower", 9.9);

        //生成客户
        Client c1 = new Client(5007, "Tom");
        Client c2 = new Client(5008, "Jerry");
        Client c3 = new Client(5009, "Lucy");

        Order order1, order2, order3;
         OrderService s1 = new OrderService();

        [TestInitialize]
        public void TInitialize()
        {
            //生成订单项
            OrderDetails od1 = new OrderDetails(cup, 5);
            OrderDetails od2 = new OrderDetails(book, 2);
            OrderDetails od3 = new OrderDetails(phone, 1);

            //生成订单order1
            order1 = new Order(c1, "The Fifth Avenue", new DateTime(2021,04,09,18,59,10));
            order1.AddItem(new OrderDetails(book, 3));
            order1.AddItem(new OrderDetails(phone, 1));

            //生成订单项列表
            List<OrderDetails> odli = new List<OrderDetails>();
            odli.Add(od1);
            odli.Add(od2);

            //生成订单order2
            order2 = new Order(c2, "WHU University", odli, new DateTime(2021, 04, 09, 18, 59, 25));
            order2.AddItem(od3);

            //生成订单order3
            order3 = new Order(c3, "Lucy's Home", new DateTime(2021, 04, 10, 18, 59, 10));
            order3.AddItem(new OrderDetails(cup, 100));

            s1.AddOrder(order1);
            s1.AddOrder(order2);
            s1.AddOrder(order3);
        }

        [TestMethod()]
        public void AddOrderTest()
        {
            OrderDetails od = new OrderDetails(flower, 100);
            Order order4 = new Order(c3, "Lili's Home", new List<OrderDetails> { od }, DateTime.Now.AddDays(2));
            s1.AddOrder(order4);
            Assert.AreEqual(4, s1.Orders.Count);
            CollectionAssert.Contains(s1.Orders, order4);
        }

        [TestMethod()]
        public void RemoveOrderTest()
        {
            s1.RemoveOrder(order3);
            Assert.AreEqual(2, s1.Orders.Count);
        }

        [TestMethod()]
        public void ShowByPriceTest()
        {
            Assert.AreEqual(s1.Orders.Count, s1.ShowByPrice().Count);
        }

        [TestMethod()]
        public void ExportTest()
        {
            String file = "test.xml";
            s1.Export(file);
            Assert.IsTrue(File.Exists(file));
            List<String> expected = File.ReadLines("expected.xml").ToList();
            List<String> testoutput = File.ReadLines(file).ToList();
            Assert.AreEqual(expected.Count, testoutput.Count);
            for(int i=0; i < testoutput.Count; i++)
            {
                if(i != 3 && i != 33 && i != 72)
                    Assert.AreEqual(expected[i], testoutput[i]);
            }
        }

        [TestMethod()]
        public void ImportTest1()
        {
            s1.Export("temp.xml");
            OrderService s2 = new OrderService();
            s2.Import("temp.xml");
            Assert.AreEqual(3, s2.Orders.Count);
            //Assert.IsTrue(s2.Orders.SequenceEqual(s1.Orders));
            for (int i = 0; i < s2.Orders.Count; i++)
            {
                Assert.AreEqual(s2.Orders[i], s1.Orders[i]);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportTest2()
        {
            OrderService s2 = new OrderService();
            s2.Import("noSuchFile.xml");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ImportTest3()
        {
            OrderService s2 = new OrderService();
            s2.Import("errorFile.xml");
        }
    }
}