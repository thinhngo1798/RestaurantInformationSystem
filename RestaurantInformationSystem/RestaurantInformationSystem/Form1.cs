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
    public partial class Form1 : Form
    {
        private Restaurant _restaurant;
        private static Timer _timer;
        private int _currentOrderId;
        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }
        public static Timer Timer { get => _timer; set => _timer = value; }
        public int CurrentOrderId { get => _currentOrderId; set => _currentOrderId = value; }

        public Form1(Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Restaurant.TableTerminal.getInput(input.Text,Restaurant);
            output.Text = Restaurant.TableTerminal.renderUI();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            output.Text = Restaurant.TableTerminal.menuDisplay();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = Restaurant.Gui.Form2;
            f2.Show();
           // this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Restaurant.Gui.Form4.Show();
            Restaurant.Gui.Form3.Show();
            Restaurant.Gui.Form4.LabelTex = Restaurant.KitchenTerminal.Notification;
            Restaurant.Gui.Form3.LabelTex = Restaurant.CashierTerminal.Notification;

            // Notifying the order that has been waiting longer than expect.
            foreach (Order order in Restaurant.Database.Orders)
            {
                Timer = new System.Windows.Forms.Timer();
                Timer.Interval = (order.OrderWaitingTime) * 60 * 1000 / 10;
                CurrentOrderId = order.Id;
                Timer.Tick += label3_Click;
                Timer.Enabled = true;
            }

        }
        private void label3_Click(object sender, EventArgs e)
        {
            Restaurant.Gui.Form3.LabelTex = "Order Id: " + CurrentOrderId + " has been waiting longer than expected.";
        }

    }
}
