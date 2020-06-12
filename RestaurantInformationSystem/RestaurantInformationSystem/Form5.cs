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
    public partial class Form5 : Form
    {
        private Restaurant _restaurant;
        private static Timer _timer;
        private int _currentOrderId;

        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }
        public static Timer Timer { get => _timer; set => _timer = value; }
        public int CurrentOrderId { get => _currentOrderId; set => _currentOrderId = value; }

        public Form5( Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;
            Restaurant.Database.CurrentFunction = "OrderFunction";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = Restaurant.Gui.Form2;
            f2.Show();
        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "OrderFunction";
            output.Text = Restaurant.WebTerminal.MenuDisplay();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }

        private void enter_Click(object sender, EventArgs e)
        {
            if (Restaurant.Database.CurrentFunction == "OrderFunction")
            {
                Restaurant.WebTerminal.getInput(input.Text, Restaurant,"");
                output.Text = Restaurant.WebTerminal.renderUI();
            }
            else if (Restaurant.Database.CurrentFunction == "ReservationFunction")
            {
                Restaurant.WebTerminal.getReservationInput(input.Text);
                Restaurant.WebTerminal.renderReservationUI();
                output.Text = Restaurant.WebTerminal.OutputString;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Restaurant.Gui.Form4.Show();
            Restaurant.Gui.Form3.Show();
            Restaurant.Gui.Form4.LabelTex = Restaurant.KitchenTerminal.OrderNotification;
            Restaurant.Gui.Form3.LabelTex = Restaurant.CashierTerminal.OrderNotification;

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

        private void button5_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "ReservationFunction";
            Restaurant.WebTerminal.OutputString = "";
            Restaurant.WebTerminal.renderReservationUI();
            output.Text = Restaurant.WebTerminal.OutputString;
        }
    }
}
