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
        /// <summary>
        /// The constructor is used to initialize the Menu with 6 items.
        /// </summary>
        public Menu()
        {
            MenuList = new List<MenuItem>();
            MenuItem item1 = new MenuItem(1, "Burger", 7.0, 4);
            MenuItem item2 = new MenuItem(2, "Chicken Pizza", 9.0, 10);
            MenuItem item3 = new MenuItem(3, "Fried Chicken", 7.0, 5);
            MenuItem item4 = new MenuItem(4, "Grilled Chicken", 10.0, 15);
            MenuItem item5 = new MenuItem(5, "Noodle", 4.0, 3);
            MenuItem item6 = new MenuItem(6, "Pasta", 11.0, 8);
            MenuList.Add(item1);
            MenuList.Add(item2);
            MenuList.Add(item3);
            MenuList.Add(item4);
            MenuList.Add(item5);
            MenuList.Add(item6);
        }

        internal List<MenuItem> MenuList { get => _menuList; set => _menuList = value; }
    }
}
