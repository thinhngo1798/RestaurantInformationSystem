using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class MenuItem
    {
        private int _id;
        private string _name;
        private double _price;
        private int _waitingTime;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set => _price = value; }
        public int WaitingTime { get => _waitingTime; set => _waitingTime = value; }

        public MenuItem(int id, string name, double price, int waitingTime)
        {
            Id = id;
            Name = name;
            Price = price;
            WaitingTime = waitingTime;
        }
    }
}
