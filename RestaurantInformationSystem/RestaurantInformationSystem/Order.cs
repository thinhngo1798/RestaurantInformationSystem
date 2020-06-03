using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Order
    {
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

        public Order(int id, bool dineInFlag, List<MenuItem> orderItems)
        {
            // Need instantiate?
            Id = id;
            MenuItems = orderItems;
            DineInFlag = dineInFlag;
            Status = "PENDING";
            Transaction = new Transaction(orderItems);
        }
        public void changeStatus(string status)
        {
            Status = status;
        }
        public void addItem(MenuItem item)
        {
            MenuItems.Add(item);
        }
    }
}
