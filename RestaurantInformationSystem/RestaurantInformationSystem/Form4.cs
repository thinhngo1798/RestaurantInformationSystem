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
    public partial class Form4 : Form
    {
        private Restaurant _restaurant;
        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }
        public Form4(Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;
            textBox2.Text = "this is the notification";
        }
        public string LabelTex
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "OrderFunction";
            output.Text = Restaurant.CashierTerminal.displayOrder();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Restaurant.KitchenTerminal.OutputString = "";
            output.Text = Restaurant.KitchenTerminal.renderUI();
        }

        private void enter_Click(object sender, EventArgs e)
        {
            if (Restaurant.Database.CurrentFunction == "OrderFunction")
            {
                Restaurant.TableTerminal.getInput(input.Text);
                output.Text = Restaurant.TableTerminal.renderUI();
            }
        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = Restaurant.Gui.Form2;
            f2.Show();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
