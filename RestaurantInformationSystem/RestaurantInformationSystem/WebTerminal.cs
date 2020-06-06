using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class WebTerminal : OrderingInterface, IReserver
    {
        private string _webTerminalCode;
        private string _stateForReservation;
        private bool _successFlag;
        //private string _outputString;

        public string WebTerminalCode { get => _webTerminalCode; set => _webTerminalCode = value; }
        public string StateForReservation { get => _stateForReservation; set => _stateForReservation = value; }
        public bool SuccessFlag { get => _successFlag; set => _successFlag = value; }
        //public string OutputString { get => _outputString; set => _outputString = value; }

        public WebTerminal(string webTerminalCode, Database database) : base(database)
        {
            WebTerminalCode = webTerminalCode;
            DineInFlag = false;
            // For reservation
            StateForReservation = "stage1";
            SuccessFlag = true;
            
        }
        /// <summary>
        /// input is a string of menuItem IDs
        /// gererating new orders and adding them in Orderslist.
        /// </summary>
        /// <param name="input"></param>
        public override void createOrder(string input, bool dineInFlag, Restaurant restaurant)
        {
            List<MenuItem> orderItems = new List<MenuItem>();
            for (int i = 0; i < input.Length; i++)
            {
                foreach (MenuItem item in Database.Menu.MenuList)
                {
                    char character = input[i];
                    if (character.ToString() == item.Id.ToString())
                        orderItems.Add(item);
                }
            }
            int orderId = Database.Orders.Count();
            Order newOrder = new Order(WebTerminalCode,orderId + 1, dineInFlag, orderItems);
            newOrder.Attach(restaurant.KitchenTerminal);
            //newOrder.Attach(restaurant.CashierTerminal);
            newOrder.Notify();
            Database.Orders.Add(newOrder);
        }

        // For reservation function
        public string retreiveReservation()
        {
            string reservationsString = "";
            foreach (Reservation re in Database.Reservations)
            {
                reservationsString += "Id: " + re.Id + Environment.NewLine
                    + "Reservation time: " + re.Time + Environment.NewLine
                    + "Number of people: " + re.NumberOfPeople + Environment.NewLine
                    + "Name of customer: " + re.CustomerName + Environment.NewLine
                    + "Phone number: " + re.PhoneNumber + Environment.NewLine
                    + "Email(if applicable): " + re.Email + Environment.NewLine;
            }
            return reservationsString;
        }

        public void deleteReservation(int id)
        {
            foreach (Reservation re in Database.Reservations)
            {
                if (re.Id == id)
                {
                    Database.Reservations.Remove(re);
                }
            }
        }

        public void getReservationInput(string input)
        {
            DateTime time = new DateTime();
            int numberOfPeople = 0;
            string customerName = "";
            string phoneNumber = "";
            string email = "";
            if (StateForReservation == "stage1")
            {
                string[] words = input.Split(',');
                if (words.Length >= 5)
                {
                    time = DateTime.ParseExact(words[0], "dd-MM-yyyy HH:mm tt", null);
                    numberOfPeople = int.Parse(words[1]);
                    customerName = words[2];
                    phoneNumber = words[3];
                    email = words[4];
                    StateForReservation = "stage2";
                }
                else
                {
                    OutputString = "Your input is invalid";
                }
            }
            if (StateForReservation == "stage2")
            {
                createReservation(time, numberOfPeople, customerName, phoneNumber, email);
            }

        }
        public void renderReservationUI()
        {
            if (StateForReservation == "stage1")
            {
                // restart the flag.
                SuccessFlag = true;
                OutputString = "Please enter your detail information for the reservation"
                    + Environment.NewLine + " in the following form format: "
                    + Environment.NewLine + " time,number of people, customer's name, phone number, email"
                    + Environment.NewLine + " Example: 03-06-2020 22:10 PM,3,Steven,041085610,thinhngo.1798@gmail.com";
            }
            if (StateForReservation == "stage2")
            {
                StateForReservation = "stage1";
                if (SuccessFlag)
                {
                    OutputString = "Your reservation has been successfully proceeded.";
                }
            }
        }
        /// <summary>
        /// Create a reservation if the number of reservation at a time is not exceeded the 
        /// max value.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="people"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        public void createReservation(DateTime time, int people, string name, string phoneNumber, string email)
        {
            int id = Database.Reservations.Count() + 1;
            Reservation newReservation = new Reservation(id, time, people, name, phoneNumber, email);
            if (checkClashing(time))
            {
                Database.Reservations.Add(newReservation);
            }
            else
            {
                OutputString = "There are no more space to book on that time. Please pick a different time";
            }
        }
        public bool checkClashing(DateTime time)
        {
            bool result = true;
            int reservationCounter = 0;
            foreach (Reservation re in Database.Reservations)
            {
                if (re.Time == time)
                    reservationCounter++;
            }
            if (reservationCounter >= CashierTerminal.MAX_RESERVATION_AT_A_TIME)
            {
                result = false;
                SuccessFlag = false;
            }
            return result;
        }
    }
}
