using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class OrderingInterface : UserInterface
    {
        private Database _database;
        private bool _dineInFlag = true;
        private string _deviceCode ="";
        private string _outputString = "";
        private string _inputstate ;
        private bool _cancelOrder;
        public bool DineInFlag { get => _dineInFlag; set => _dineInFlag = value; }
        public Database Database { get => _database; set => _database = value; }
        public string DeviceCode { get => _deviceCode; set => _deviceCode = value; }
        public string OutputString { get => _outputString; set => _outputString = value; }
        public string Inputstate { get => _inputstate; set => _inputstate = value; }
        public bool CancelOrder { get => _cancelOrder; set => _cancelOrder = value; }

        public OrderingInterface(Database database)
        {
            Database = database;
        }
        public override void getInput(string input, Restaurant restaurant)
        {
            if ((input == "cancel") && (Database.Orders.Count() != 0))
            {
                Database.Orders.RemoveAt(Database.Orders.Count() - 1);
                CancelOrder = true;
            }
            else
            {
                bool ANumberFlag = false;
                int i = 0;
                ANumberFlag = int.TryParse(input, out i);
                if (ANumberFlag)
                {
                    Inputstate = "valid";
                    createOrder(input, DineInFlag, restaurant);
                }
                else
                {
                    Inputstate = "invalid";
                    OutputString = " Your input is invalid. You must enter the id number only.";
                    
                }
            }
        }
        public override string renderUI()
        
        {
            int currentOrderId = Database.Orders.Count();
            string orderResult = "";
           
           if ( Inputstate == "invalid")
            {
                return OutputString;
            }
            else if (CancelOrder)
            {
                orderResult = "The order has been cancelled.";
                CancelOrder = false;
            }
            else
            {
                orderResult = "Id:" + currentOrderId +Environment.NewLine;
                orderResult += "Item:" +Environment.NewLine;
                
                foreach (MenuItem item in Database.Orders[(currentOrderId - 1)].MenuItems)
                {
                    orderResult += item.Name +"     Price:" + item.Price  + "       Waiting time:" + item.WaitingTime +" mins" +Environment.NewLine;
                }
                orderResult += "Status:" + Database.Orders[currentOrderId - 1].Status + Environment.NewLine;
                orderResult += "Transaction Status:" + Database.Orders[currentOrderId - 1].Transaction.Status + Environment.NewLine;
                orderResult += "Order has been recorded. Please press Comfirm to inform our staffs about your order."
           + Environment.NewLine + "Or type cancel to cancel the order.";
            }
            return orderResult;
            
        }
        
        public override void getNotify()
        {

        }

        public string menuDisplay()
        {
            string menuInformation = "";
            foreach (MenuItem item in Database.Menu.MenuList)
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
        public virtual void createOrder(string input, bool dineInFlag , Restaurant restaurant = null)
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
            Order newOrder = new Order(DeviceCode,orderId+1,dineInFlag, orderItems);
            Database.Orders.Add(newOrder);
        }
    }
}
