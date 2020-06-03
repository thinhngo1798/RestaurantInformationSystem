using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class CashierTerminal : StaffInterface
    {
        private int cashierId;

        public int CashierId { get => cashierId; set => cashierId = value; }
        public CashierTerminal(int id,Database database) :base(database)
        {
            CashierId = id;
        }
    }
}
