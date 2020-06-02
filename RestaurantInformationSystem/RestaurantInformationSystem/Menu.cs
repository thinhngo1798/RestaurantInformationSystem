using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Menu
    {
        private List<MenuItem> _menuList;

        public Menu()
        {
            MenuList = new List<MenuItem>();
            MenuItem item1 = new MenuItem(1, "chicken", 3.0, 5);
            MenuItem item2 = new MenuItem(2, "pork", 5.0, 10);
            MenuItem item3 = new MenuItem(3, "beff", 6.0, 7);
            MenuList.Add(item1);
            MenuList.Add(item2);
            MenuList.Add(item3);
        }

        internal List<MenuItem> MenuList { get => _menuList; set => _menuList = value; }
    }
}
