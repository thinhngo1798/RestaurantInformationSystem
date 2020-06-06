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
        private static Timer _timer1;
        private Restaurant _restaurant;
        private string _oldMessage;
        public Restaurant Restaurant { get => _restaurant; set => _restaurant = value; }
        public static Timer Timer1 { get => _timer1; set => _timer1 = value; }
        public string LabelTex
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        public string OldMessage { get => _oldMessage; set => _oldMessage = value; }

        public Form3(Restaurant restaurant)
        {
            InitializeComponent();
            Restaurant = restaurant;
            OldMessage = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = Restaurant.Gui.Form2;
            f2.Show();
            //this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "OrderFunction";
            Restaurant.CashierTerminal.OutputString = "";
            output.Text = Restaurant.CashierTerminal.renderUI();
            //Start notifying if there is changes
            Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 5000;
            Timer1.Tick += label3_Click;
            Timer1.Enabled = true;

        }

        private void enter_Click(object sender, EventArgs e)
        {
            if (Restaurant.Database.CurrentFunction == "OrderFunction")
            {
                Restaurant.CashierTerminal.getInput(input.Text);
                output.Text = Restaurant.CashierTerminal.renderUI();
            }
            else if (Restaurant.Database.CurrentFunction == "ReservationFunction")
            {
                Restaurant.CashierTerminal.getReservationInput(input.Text);
                Restaurant.CashierTerminal.renderReservationUI();
                output.Text = Restaurant.CashierTerminal.OutputString;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "OrderFunction";
            output.Text =  Restaurant.CashierTerminal.displayOrder();
        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }

        private void output_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "ReservationFunction";
            output.Text = Restaurant.CashierTerminal.retreiveReservation();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Restaurant.Database.CurrentFunction = "ReservationFunction";
            Restaurant.CashierTerminal.OutputString = "";
            Restaurant.CashierTerminal.renderReservationUI();
            output.Text = Restaurant.CashierTerminal.OutputString;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (OldMessage != Restaurant.CashierTerminal.ChangeOrderNotification)
            {
                Restaurant.Gui.Form3.LabelTex = Restaurant.CashierTerminal.ChangeOrderNotification;
                OldMessage = Restaurant.CashierTerminal.ChangeOrderNotification;
            }
            }
    }
}
