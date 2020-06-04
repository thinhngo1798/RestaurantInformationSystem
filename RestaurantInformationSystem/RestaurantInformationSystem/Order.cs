using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Order : ISubject
    {
        private int _state = -0;
        private List<IObserver> _observers = new List<IObserver>();
        private string _deviceCode;
        private int _id;
        private List<MenuItem> _menuItem;
        private string _status;
        private bool _dineInFlag;
        private Transaction _transaction;

        public int Id { get => _id; set => _id = value; }
        public List<MenuItem> MenuItems { get => _menuItem; set => _menuItem = value; }
        public string Status { get => _status; set => _status = value; }
        internal Transaction Transaction { get => _transaction; set => _transaction = value; }
        public bool DineInFlag { get => _dineInFlag; set => _dineInFlag = value; }
        public int State { get => _state; set => _state = value; }
        public List<IObserver> Observers { get => _observers; set => _observers = value; }
        public string DeviceCode { get => _deviceCode; set => _deviceCode = value; }

        public Order(string devideCode,int id, bool dineInFlag, List<MenuItem> orderItems)
        {
            // Need instantiate?
            DeviceCode = devideCode;
            Id = id;
            MenuItems = orderItems;
            DineInFlag = dineInFlag;
            Status = "PENDING";
            Transaction = new Transaction(orderItems);
        }
        public void changeStatus(string status)
        {
            Status = status;
            Notify();
        }
        public void addItem(MenuItem item)
        {
            MenuItems.Add(item);
            Notify();
        }
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
        public void Attach(IObserver observer)
        {
            this.Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this.Observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this);
            }
        }
    }
}