using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class GUI
    {
        private Form1 _form1;
        private Form2 _form2;
        private Form3 _form3;
        private Form4 _form4;
        private Form5 _form5;
        private Restaurant _restaurant;

        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }
        public Form1 Form1 { get => _form1; set => _form1 = value; }
        public Form2 Form2 { get => _form2; set => _form2 = value; }
        public Form3 Form3 { get => _form3; set => _form3 = value; }
        public Form4 Form4 { get => _form4; set => _form4 = value; }
        public Form5 Form5 { get => _form5; set => _form5 = value; }
        /// <summary>
        /// The Gui can access Restaurant to implement the User Interface.
        /// </summary>
        /// <param name="restaurant"></param>
        public GUI (Restaurant restaurant)
            {
            Restaurant = restaurant;
            Form1 = new Form1(restaurant);
            Form2 = new Form2(restaurant);
            Form3 = new Form3(restaurant);
            Form4 = new Form4(restaurant);
            Form5 = new Form5(restaurant);
        }
    }
}
