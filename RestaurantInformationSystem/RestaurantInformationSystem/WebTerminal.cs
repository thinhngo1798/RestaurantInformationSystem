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
        private bool _noErrorFlag = true;
        //private string _outputString;

        public string WebTerminalCode { get => _webTerminalCode; set => _webTerminalCode = value; }
        public string StateForReservation { get => _stateForReservation; set => _stateForReservation = value; }
        public bool SuccessFlag { get => _successFlag; set => _successFlag = value; }
        public bool noErrorFlag { get => _noErrorFlag; set => _noErrorFlag = value; }

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
        public override void CreateOrder(string input, bool dineInFlag, string orderDeviceCode, Restaurant restaurant)
        {
            List<MenuItem> orderItems = new List<MenuItem>();
            Order newOrder;
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
            if (orderDeviceCode != "")
            {
                newOrder = new Order(orderDeviceCode, orderId + 1, dineInFlag, orderItems);
            }
            else
            {
                newOrder = new Order(DeviceCode, orderId + 1, dineInFlag, orderItems);
            }
            newOrder.Attach(restaurant.KitchenTerminal);
            newOrder.Attach(restaurant.CashierTerminal);
            newOrder.Notify();
            Database.Orders.Add(newOrder);
        }

        /// <summary>
        ///  For reservation function
        /// </summary>
        /// <returns></returns>
        public string RetreiveReservation()
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

        public void DeleteReservation(int id)
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
            noErrorFlag = true;
            OutputString = "";
            DateTime time = new DateTime();
            int numberOfPeople = 0;
            string customerName = "";
            string phoneNumber = "";
            string email = "";
            int testPhoneNumber = 0;
            if (StateForReservation == "stage1")
            {

                string[] words = input.Split(',');
                words[0] = words[0].Trim();
                if (words.Length != 5)
                {
                    OutputString += " Your input is insufficient, please try again." + Environment.NewLine;
                    noErrorFlag = false;
                }
                else
                {
                    // Validation each input
                    if (words[0] == "")
                    {
                        OutputString += "Your time cannot be empty." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                    else if (!DateTime.TryParse(words[0], out time))
                    {
                        OutputString += " Your time input is invalid. Please try again." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                    if (words[1] == "")
                    {
                        OutputString += "Your number of people cannot be empty." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                    else if (!int.TryParse(words[1], out numberOfPeople))
                    {
                        OutputString += " Your number of people input is invalid. Please try again." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                    if (words[2] == "")
                    {
                        OutputString += "Your name cannot be empty." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                    if (words[3] == "")
                    {
                        OutputString += "Your phone number cannot be empty." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                    else if (!int.TryParse(words[3], out testPhoneNumber))
                    {
                        OutputString += " Your phone number input is invalid. Please try again." + Environment.NewLine;
                        noErrorFlag = false;
                    }
                }
                if (noErrorFlag)
                {
                    time = DateTime.ParseExact(words[0], "dd-MM-yyyy HH:mm tt", null);
                    numberOfPeople = int.Parse(words[1]);
                    customerName = words[2];
                    phoneNumber = words[3];
                    email = words[4];
                    StateForReservation = "stage2";
                }
            }
            if (StateForReservation == "stage2")
            {
                CreateReservation(time, numberOfPeople, customerName, phoneNumber, email);
            }

        }
        public void renderReservationUI()
        {
            if (StateForReservation == "stage1")
            {
                OutputString += "Please enter your detail information for the reservation"
                            + Environment.NewLine + " in the following form format: "
                            + Environment.NewLine + " time,number of people, customer's name, phone number, email"
                            + Environment.NewLine + " Example: 03-06-2020 22:10 PM,3,Steven,041085610,thinhngo.1798@gmail.com" + Environment.NewLine;
            }
            if (StateForReservation == "stage2")
            {
                StateForReservation = "stage1";
                if (noErrorFlag)
                    OutputString = "Your reservation has been successfully proceeded." + Environment.NewLine;
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
        public void CreateReservation(DateTime time, int people, string name, string phoneNumber, string email)
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
