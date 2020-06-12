using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class OrderingInterface 
    {
        // Using abstraction and encapsulation of data.
        private Database _database;
        private bool _dineInFlag = true;
        private string _deviceCode ="";
        private string _outputString = "";
        private string _inputState ;
        private bool _cancelOrder;

        public bool DineInFlag { get => _dineInFlag; set => _dineInFlag = value; }
        public Database Database { get => _database; set => _database = value; }
        public string DeviceCode { get => _deviceCode; set => _deviceCode = value; }
        public string OutputString { get => _outputString; set => _outputString = value; }
        public string Inputstate { get => _inputState; set => _inputState = value; }
        public bool CancelOrder { get => _cancelOrder; set => _cancelOrder = value; }

        /// <summary>
        /// Ordering interface is the parent of TableTerminal and WebTerminal.
        /// The database will be passed by derived classes.
        /// </summary>
        /// <param name="database"></param>
        public OrderingInterface(Database database)
        {
            Database = database;
        }
        /// <summary>
        /// Getting input from user.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="restaurant"></param>
        public void getInput(string input, Restaurant restaurant,string orderDeviceCode)
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
                    CreateOrder(input, DineInFlag, orderDeviceCode, restaurant);
                }
                else
                {
                    Inputstate = "invalid";
                    OutputString = " Your input is invalid. You must enter the id number only.";
                    
                }
            }
        }
        /// <summary>
        /// Generating User Interface
        /// </summary>
        /// <returns></returns>
        public string renderUI()
        
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
        /// <summary>
        /// Display all menu's items for customers.
        /// </summary>
        /// <returns></returns>
        public string MenuDisplay()
        {
            string menuInformation = "";
            foreach (MenuItem item in Database.Menu.MenuList)
            {
                menuInformation += item.Id +"       Name:" +item.Name + "     Price:" + item.Price + "$       Waiting time:" + item.WaitingTime + Environment.NewLine;
            }
            menuInformation += "Please choose one or multiple item that you want to order by inserting the item's id:";
            return menuInformation;
        }
        /// <summary>
        /// input is a string of menuItem IDs
        /// gererating new orders and adding them in Orderslist.
        /// </summary>
        /// <param name="input"></param>
        public virtual void CreateOrder(string input, bool dineInFlag , string orderDeviceCode, Restaurant restaurant = null)
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
            Database.AddingOrder(newOrder);
        }
    }
}
