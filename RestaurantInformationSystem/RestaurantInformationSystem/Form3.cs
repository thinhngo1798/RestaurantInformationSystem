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
    public partial class Form3 : Form
    {
        private Restaurant _restaurant;
        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }

        public Form3(Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2(Restaurant);
            f2.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            output.Text = Restaurant.CashierTerminal.renderUI();
        }

        private void enter_Click(object sender, EventArgs e)
        {
            Restaurant.CashierTerminal.getInput(input.Text);
            output.Text = Restaurant.CashierTerminal.renderUI();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            output.Text =  Restaurant.CashierTerminal.displayOrder();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
