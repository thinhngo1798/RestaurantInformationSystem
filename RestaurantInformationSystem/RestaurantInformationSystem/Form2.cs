using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantInformationSystem
{
    public partial class Form2 : Form
    {
        private Restaurant _restaurant;
        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }

        public Form2( Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form1 f1 = Restaurant.Gui.Form1;
            f1.ShowDialog();
            //this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form3 f3 = Restaurant.Gui.Form3;
            f3.ShowDialog();
            //this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Form4 f4 = Restaurant.Gui.Form4;
            f4.Show();
            //this.Close();
        }
    }
}
