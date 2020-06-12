using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    class TableTerminal :OrderingInterface
    {
        private string _tableCode;
        

        public string TableCode { get => _tableCode; set => _tableCode = value; }

        public TableTerminal(string tableCode, Database database) : base(database)
        {
            TableCode = tableCode;
            DineInFlag = true;
        }
        public override void CreateOrder(string input, bool dineInFlag,string orderDeviceCode, Restaurant restaurant)
        {
            List<MenuItem> orderItems = new List<MenuItem>();
            Order newOrder;
            for (int i = 0; i < input.Length; i++)
            {
                foreach (MenuItem item in Database.Menu.MenuList)
                {
                    char character = input[i];
                    if (character.ToString() == item.Id.ToString())
                        orderItems.Add(item);
                }
            }
            int orderId = Database.Orders.Count();
            if (orderDeviceCode != "")
            {
                newOrder = new Order(orderDeviceCode, orderId + 1, dineInFlag, orderItems);
            }
            else
            {
                newOrder = new Order(DeviceCode, orderId + 1, dineInFlag, orderItems);
            }
            newOrder.Attach(restaurant.KitchenTerminal);
            newOrder.Attach(restaurant.CashierTerminal);
            newOrder.Notify();
            Database.AddingOrder(newOrder);
        }
    }
}
