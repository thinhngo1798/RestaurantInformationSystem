using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Transaction
    {
        private string _status;
        private double _cost;
        private DateTime _createTime;
        private DateTime _paymentTime;
        /// <summary>
        /// Taking care of transaction and validation.
        /// It is not required in this assignment.
        /// </summary>
        /// <param name="orderItems"></param>
        public Transaction(List<MenuItem> orderItems)
        {
            Status = "PENDING";
            Cost = 0.0;
            foreach (MenuItem item in orderItems)
            {
                Cost += item.Price;
            }
            CreateTime = DateTime.Now;
           
        }
        public void payTransaction()
        {
            Status = "PAID";
            PaymentTime = DateTime.Now;
        }

        public string Status { get => _status; set => _status = value; }
        public double Cost { get => _cost; set => _cost = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime PaymentTime { get => _paymentTime; set => _paymentTime = value; }
    }
}
