using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class KitchenTerminal : StaffInterface , IObserver
    {
        private string _kitchenCode;

        public string KitchenCode { get => _kitchenCode; set => _kitchenCode = value; }

        public KitchenTerminal (string code, Database database) : base(database)
        {
            KitchenCode = code;
            Database = database;
        }

        public void Update(ISubject subject)
        {
            if ((subject as Order).Status == "PENDING")
            {
                OrderNotification += "The list of order is: " + Environment.NewLine;
                OrderNotification += (subject as Order).displayOrder();
            }
           
        }
    }
}
