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
        private List<Reservation> _reservations;
        private string _currentFunction; 

        internal List<Order> Orders { get => _orders; set => _orders = value; }
        internal Menu Menu { get => _menu; set => _menu = value; }
        public List<Reservation> Reservations { get => _reservations; set => _reservations = value; }
        public string CurrentFunction { get => _currentFunction; set => _currentFunction = value; }

        public Database()
        {
            Menu = new Menu();
            Orders = new List<Order>();
            Reservations = new List<Reservation>();
            CurrentFunction = "";
        }

    }
}
