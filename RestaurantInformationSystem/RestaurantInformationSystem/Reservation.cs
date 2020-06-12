using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Reservation
    {
        private int _id;
        private DateTime _time;
        private int _numberOfPeople;
        private string _customerName;
        private string _phoneNumber;
        private string _email;

        public int Id { get => _id; set => _id = value; }
        public DateTime Time { get => _time; set => _time = value; }
        public int NumberOfPeople { get => _numberOfPeople; set => _numberOfPeople = value; }
        public string CustomerName { get => _customerName; set => _customerName = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }

        /// <summary>
        /// Initilize a new reservation instand by data provided.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="people"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        public Reservation (int id, DateTime time, int people, string name, string phoneNumber, string email)
        {
            Id = id;
            Time = time;
            NumberOfPeople = people;
            CustomerName = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
    }
}
