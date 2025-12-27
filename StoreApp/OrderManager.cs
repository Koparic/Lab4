using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoreApp
{
    public class OrderManager
    {
        public List<OrderClass> orders { get; private set; } = new List<OrderClass>();

        public struct orderInfo
        {
            public int id { get; private set; }
            public int numOfProd { get; private set; }
            public string address { get; private set; }

            public orderInfo(int id, int numOfProd, string address)
            {
                this.id = id;
                this.numOfProd = numOfProd;
                this.address = address;
            }

            public override string ToString()
            {
                return $"Order #{id}, Products: {numOfProd}, Address: {address}";
            }
        }
        public void SaveOrders()
        {
            string filePath = "OrdersHistory.json";
            using (StreamWriter writer = File.CreateText(filePath))
            {
                string json = JsonConvert.SerializeObject(orders, Newtonsoft.Json.Formatting.Indented);
                writer.Write(json);
            }
        }

        public void LoadOrders()
        {
            string filePath = "OrdersHistory.json";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = File.OpenText(filePath))
                {
                    string json = reader.ReadToEnd();
                    orders.Clear();
                    try
                    {
                        orders = JsonConvert.DeserializeObject<List<OrderClass>>(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Заказы не смогли загрузиться " + ex.Message);
                    }
                }
            }
        }

        public void AddOrder(OrderClass order)
        {
            orders.Add(new OrderClass(orders.Count, order.products, order.comment, order.adress, order.isClosed));
        }

        public void UpdateOrder(OrderClass order)
        {
            if (order.products.Count > 0)
            {
                OrderClass newOrder = new OrderClass(order.id, order.products, order.comment, order.adress, order.isClosed);
                orders[order.id] = newOrder;
            }
            else orders.RemoveAt(order.id);
        }

        public List<orderInfo> CreateClosedOrdersList()
        {
            List<orderInfo> closed = new List<orderInfo>();
            foreach (OrderClass o in orders)
            {

                if (o.isClosed) closed.Add(new orderInfo(o.id, o.products.Count(), o.adress));
            }
            return closed;
        }
        public List<orderInfo> CreateOpenedOrdersList()
        {
            List<orderInfo> opened = new List<orderInfo>();
            foreach (OrderClass o in orders)
            {
                if (!o.isClosed) opened.Add(new orderInfo(o.id, o.products.Count(), o.adress));
            }
            return opened;
        }

        public OrderClass FindOrderByID(int id)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].id == id) return orders[id];
            }
            return null;
        }
    }
}
