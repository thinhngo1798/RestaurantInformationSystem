using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Database
    {
       
        private List<Order> _orders;
        private Menu _menu;

        internal List<Order> Orders { get => _orders; set => _orders = value; }
        internal Menu Menu { get => _menu; set => _menu = value; }
       
        public Database()
        {
            Menu = new Menu();
            Orders = new List<Order>();
        }

    }
}
