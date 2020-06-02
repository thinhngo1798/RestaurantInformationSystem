using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Order
    {
        private List<MenuItem> _menuItem;
        private string _status;
        private bool _dineInFlag;
        private Transaction _transaction;

        public List<MenuItem> MenuItem { get => _menuItem; set => _menuItem = value; }
        public string Status { get => _status; set => _status = value; }
        internal Transaction Transaction { get => _transaction; set => _transaction = value; }
        public bool DineInFlag { get => _dineInFlag; set => _dineInFlag = value; }

        public Order (MenuItem orderItem, bool dineInFlag, List<MenuItem> orderItems)
        {
            // Need instantiate?
            MenuItem = orderItems;
            DineInFlag = dineInFlag;
            Status = "PENDING";
            Transaction = new Transaction(orderItem.Price);
        }
        public void changeStatus(string status)
        {
            Status = status;
        }
    }
}
