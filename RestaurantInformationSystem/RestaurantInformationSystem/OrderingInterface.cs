using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class OrderingInterface : UserInterface
    {
        private List<Order> _orders;
        private Menu _menu;
        private bool _dineInFlag = true;

        public bool DineInFlag { get => _dineInFlag; set => _dineInFlag = value; }
        internal List<Order> Orders { get => _orders; set => _orders = value; }
        internal Menu Menu { get => _menu; set => _menu = value; }

        public OrderingInterface()
        {
            Menu = new Menu();
            Orders = new List<Order>();
        }
        public override void getInput(string input)
        {
             createOrder(input, DineInFlag);
        }
        public override string renderUI()
        
        {
            int currentOrderId = Orders.Count();
            string orderResult = "";
            if (currentOrderId ==0 )
            {
                orderResult = "There is no order which has been proceeded";
            }
            else
            {
                orderResult = "Id:" + currentOrderId +Environment.NewLine;
                orderResult += "Item:" +Environment.NewLine;
                
                foreach (MenuItem item in Orders[(currentOrderId - 1)].MenuItem)
                {
                    orderResult += item.Name +"     Price:" + item.Price  + "       Waiting time:" + item.WaitingTime +" mins" +Environment.NewLine;
                }
                orderResult += "Status:" + Orders[currentOrderId - 1].Status + Environment.NewLine;
                orderResult += "Transaction Status:" + Orders[currentOrderId - 1].Transaction.Status + Environment.NewLine;
            }
            orderResult += "Order has been accepted"; 
            return orderResult;
            
        }
        
        public override void getNotify()
        {

        }
        public int calculateWaitingTime(MenuItem menuItem)
        {
            int time = 0;
            foreach (MenuItem item in Menu.MenuList)
            {
                if (item == menuItem)
                    time += item.WaitingTime;

            }
            return time;
        }
        public string menuDisplay()
        {
            string menuInformation = "";
            foreach (MenuItem item in Menu.MenuList)
            {
                menuInformation += item.Id +"       Name:" +item.Name + "     Price:" + item.Price + "$       Waiting time:" + item.WaitingTime + Environment.NewLine;
            }
            return menuInformation;
        }
        /// <summary>
        /// input is a string of menuItem IDs
        /// gererating new orders and adding them in Orderslist.
        /// </summary>
        /// <param name="input"></param>
        public void createOrder(string input, bool dineInFlag )
        {
            List<MenuItem> orderItems = new List<MenuItem>();
            Order newOrder =null;
            for (int i = 0; i < input.Length; i++)
            {
                foreach (MenuItem item in Menu.MenuList)
                {
                    char character = input[i];
                    if (character.ToString() == item.Id.ToString())
                        orderItems.Add(item);
                }
            }
            foreach (MenuItem item in orderItems)
            {
                newOrder = new Order(item, dineInFlag, orderItems); 
            }
            Orders.Add(newOrder);
        }
    }
}
