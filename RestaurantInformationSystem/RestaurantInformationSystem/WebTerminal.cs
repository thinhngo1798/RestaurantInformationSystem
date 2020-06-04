using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class WebTerminal : OrderingInterface
    {
        private string _webTerminalCode;

        public string WebTerminalCode { get => _webTerminalCode; set => _webTerminalCode = value; }

        public WebTerminal(string webTerminalCode, Database database) : base(database)
        {
            WebTerminalCode = webTerminalCode;
            DineInFlag = false;
        }
        /// <summary>
        /// input is a string of menuItem IDs
        /// gererating new orders and adding them in Orderslist.
        /// </summary>
        /// <param name="input"></param>
        public override void createOrder(string input, bool dineInFlag, Restaurant restaurant)
        {
            List<MenuItem> orderItems = new List<MenuItem>();
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
            Order newOrder = new Order(WebTerminalCode,orderId + 1, dineInFlag, orderItems);
            newOrder.Attach(restaurant.KitchenTerminal);
            //newOrder.Attach(restaurant.CashierTerminal);
            newOrder.Notify();
            Database.Orders.Add(newOrder);
        }
    }
}
