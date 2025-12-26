using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp
{
    public class OrderManager
    {
        public List<OrderClass> orders { get; private set; } = new List<OrderClass>();

        public void AddOrder(OrderClass order)
        {
            orders.Add(new OrderClass(orders.Count, order.products, order.comment, order.isClosed));
        }

        public void UpdateOrder(OrderClass order)
        {
            if (order.products.Count > 0)
            {
                OrderClass newOrder = new OrderClass(order.id, order.products, order.comment, order.isClosed);
                orders[order.id] = newOrder;
            }
            else orders.RemoveAt(order.id);
        }

        public List<OrderClass> CreateClosedOrdersList()
        {
            List<OrderClass> closed = new List<OrderClass>();
            foreach (OrderClass o in orders)
            {
                if (o.isClosed) closed.Add(o);
            }
            return closed;
        }
        public List<OrderClass> CreateOpenedOrdersList()
        {
            List<OrderClass> opened = new List<OrderClass>();
            foreach (OrderClass o in orders)
            {
                if (!o.isClosed) opened.Add(o);
            }
            return opened;
        }
    }
}
