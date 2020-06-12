using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Order : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        private string _deviceCode;
        private int _id;
        private List<MenuItem> _menuItem;
        private string _status;
        private bool _dineInFlag;
        private Transaction _transaction;
        private int _orderWaitingTime;
        private string _orderTime;
        private double _totalPrice;

        public int Id { get => _id; set => _id = value; }
        public List<MenuItem> MenuItems { get => _menuItem; set => _menuItem = value; }
        public string Status { get => _status; set => _status = value; }
        internal Transaction Transaction { get => _transaction; set => _transaction = value; }
        public bool DineInFlag { get => _dineInFlag; set => _dineInFlag = value; }
        public List<IObserver> Observers { get => _observers; set => _observers = value; }
        public string DeviceCode { get => _deviceCode; set => _deviceCode = value; }
        public int OrderWaitingTime { get => _orderWaitingTime; set => _orderWaitingTime = value; }
        public string OrderTime { get => _orderTime; set => _orderTime = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        
        /// <summary>
        /// Constructor used to initialize a new order.
        /// </summary>
        /// <param name="devideCode"></param>
        /// <param name="id"></param>
        /// <param name="dineInFlag"></param>
        /// <param name="orderItems"></param>
        public Order(string devideCode,int id, bool dineInFlag, List<MenuItem> orderItems)
        {
            DeviceCode = devideCode;
            Id = id;
            MenuItems = orderItems;
            DineInFlag = dineInFlag;
            Status = "PENDING";
            OrderWaitingTime = calculateWaitingTime();
            TotalPrice = calculateTotalPrice();
            OrderTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm tt");
            Transaction = new Transaction(orderItems);
        }
        /// <summary>
        /// Total cost of this order.
        /// </summary>
        /// <returns></returns>
        public double calculateTotalPrice()
        {
            double price = 0.0;
            foreach (MenuItem item in MenuItems)
            {
                price += item.Price;
            }
            return price;
        }
        /// <summary>
        /// Calculating waiting time.
        /// </summary>
        /// <returns></returns>
        public int calculateWaitingTime()
        {
            int time = 0;
            foreach (MenuItem item in MenuItems)
            {
                time += item.WaitingTime;
            }
            return time;
        }
        /// <summary>
        /// Change the status of the current order and notify to all observer.
        /// </summary>
        /// <param name="status"></param>
        public void changeStatus(string status)
        {
            Status = status;
            string newStatus = status.ToUpper();
            if (newStatus == "PAID")
            {
                this.Transaction.payTransaction();
            }
            Notify();
        }
        /// <summary>
        /// Have notification when an item is added.
        /// </summary>
        /// <param name="item"></param>
        public void addItem(MenuItem item)
        {
            MenuItems.Add(item);
            Notify();
        }
        /// <summary>
        /// Display all menu item in this order.
        /// </summary>
        /// <returns></returns>
        public string displayOrder()
        {
            string result = "";
            result += "Order number " + Id + " is:" + Environment.NewLine;
            foreach (MenuItem item in MenuItems)
            {
                result += "Item Id: " + item.Id + "       Name:" + item.Name + "     Price:" + item.Price + "$       Waiting time:" + item.WaitingTime + Environment.NewLine;
            }
            result += "Order status is " + Status + Environment.NewLine;
            return result;
        }

        /// <summary>
        /// Observer Pattern
        /// </summary>
        /// <param name="observer"></param>
        public void Attach(IObserver observer)
        {
            this.Observers.Add(observer);
        }
        /// <summary>
        /// Observer Pattern
        /// </summary>
        /// <param name="observer"></param>
        public void Detach(IObserver observer)
        {
            this.Observers.Remove(observer);
        }
        /// <summary>
        /// Observer Pattern
        /// </summary>
        /// <param name="observer"></param>
        public void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }
    }
}