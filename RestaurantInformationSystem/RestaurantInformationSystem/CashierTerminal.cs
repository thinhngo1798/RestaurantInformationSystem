using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class CashierTerminal : StaffInterface, IReserver, IObserver
    {
        public const int MAX_RESERVATION_AT_A_TIME = 2;

        private string _cashierCode;
        private string _stateForReservation;
        private bool _successFlag;
        private bool _noErrorFlag = true;

        public string StateForReservation { get => _stateForReservation; set => _stateForReservation = value; }
        public string CashierCode { get => _cashierCode; set => _cashierCode = value; }
        public bool SuccessFlag { get => _successFlag; set => _successFlag = value; }
        public bool noErrorFlag { get => _noErrorFlag; set => _noErrorFlag = value; }

        public CashierTerminal(string cashierCode, Database database) : base(database)
        {
            CashierCode = cashierCode;
            StateForReservation = "stage1";
            SuccessFlag = true;
        }
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
                createReservation(time, numberOfPeople, customerName, phoneNumber, email);
            }

        }
        public void renderReservationUI()
        {
            if (StateForReservation =="stage1")
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
        public void createReservation(DateTime time, int people, string name, string phoneNumber, string email)
        {
            int id = Database.Reservations.Count() + 1;
            Reservation newReservation = new Reservation(id, time, people, name, phoneNumber,email);
            if (checkClashing(time))
            {
            Database.AddingReservation(newReservation);
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
            if (reservationCounter >= MAX_RESERVATION_AT_A_TIME)
            {
                result = false;
                SuccessFlag = false;
            }
            return result;
        }

        public void Update(ISubject subject)
        {
            if ((subject as Order).Status == "PENDING")
            {
                OrderNotification += "The list of order is: " + Environment.NewLine;
                OrderNotification += (subject as Order).displayOrder();
            }
            if ((subject as Order).Status != "PENDING")
            {
                ChangeOrderNotification += "The list of orders which have status changes are: " + Environment.NewLine;
                ChangeOrderNotification += (subject as Order).displayOrder();
            }
        }

    }
}
