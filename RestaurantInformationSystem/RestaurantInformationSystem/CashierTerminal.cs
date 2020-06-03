using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class CashierTerminal : StaffInterface, IReserver
    {
        public const int MAX_RESERVATION_AT_A_TIME = 2;

        private int cashierId;
        private string _stateForReservation;


        public int CashierId { get => cashierId; set => cashierId = value; }
        public string StateForReservation { get => _stateForReservation; set => _stateForReservation = value; }

        public CashierTerminal(int id, Database database) : base(database)
        {
            CashierId = id;
            StateForReservation = "stage1";
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
            int id = 0;
            DateTime time = new DateTime();
            int numberOfPeople = 0;
            string customerName ="";
            string phoneNumber ="";
            string email="";
            if (StateForReservation == "stage1")
            {
            string[] words = input.Split(',');
                if (words.Length >= 5)
                {
                    id = int.Parse(words[0]);
                    time = DateTime.ParseExact(words[1], "dd-MM-yyyy HH:mm tt", null);
                    numberOfPeople = int.Parse(words[2]);
                    customerName = words[3];
                    phoneNumber = words[4];
                    email = words[5];
                    StateForReservation = "stage2";
                }
                else
                {
                    OutputString = "Your input is invalid";
                }
            }
            if (StateForReservation == "stage2")
            {
                createReservation(id, time, numberOfPeople, customerName, phoneNumber, email);    
            }

        }
        public void renderReservationUI()
        {
            if (StateForReservation =="stage1")
            {
                OutputString = "Please enter your detail information for the reservation"
                    + Environment.NewLine + " in the following form format: "
                    + Environment.NewLine + " id,time,number of people, customer's name, phone number, email"
                    + Environment.NewLine + " Example: 1,03-06-2020 22:10 PM,3,Steven,041085610,thinhngo.1798@gmail.com";
            }
            if (StateForReservation == "stage2")
            {
                StateForReservation = "stage1";
                OutputString = "Your reservation has been successfully proceeded.";
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
        public void createReservation(int id, DateTime time, int people, string name, string phoneNumber, string email)
        {
            Reservation newReservation = new Reservation(id, time, people, name, phoneNumber,email);
            if (checkClashing(time))
            {
            Database.Reservations.Add(newReservation);
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
            }
            return result;
        }

    }
}
