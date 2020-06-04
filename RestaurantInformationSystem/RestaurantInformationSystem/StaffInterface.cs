using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class StaffInterface : UserInterface
    {
        private Database _database;
        private string _stateForStatus;
        private int _selectedID;
        private string _newStatus;
        private string _outputString;
        private string _reservationNotification;
        private string _orderNotification;
        private string _changeOrderNotification;
        public Database Database { get => _database; set => _database = value; }
        public string StateForStatus { get => _stateForStatus; set => _stateForStatus = value; }
        public int SelectedID { get => _selectedID; set => _selectedID = value; }
        public string NewStatus { get => _newStatus; set => _newStatus = value; }
        public string OutputString { get => _outputString; set => _outputString = value; }
        public string OrderNotification { get => _orderNotification; set => _orderNotification = value; }
        public string ReservationNotification { get => _reservationNotification; set => _reservationNotification = value; }
        public string ChangeOrderNotification { get => _changeOrderNotification; set => _changeOrderNotification = value; }

        public enum StateName { receive, select, type, done };

        public StaffInterface(Database database)
        {
            Database = database;
            StateForStatus = "select";
        }
        public override void getInput(string input)
        {
            if (input == "-1")
            {
                OutputString = "You have been quit the current request";
                StateForStatus = "select";
            }
            else
            {
                if (StateForStatus == "select")
                {
                    SelectedID = int.Parse(input);
                    StateForStatus = "done";
                    OutputString = "Please enter the new status";
                }
                else if (StateForStatus == "done")
                {
                    OutputString = "Your request is done";
                    NewStatus = input;
                    changeOrderStatus();
                }
                else
                {
                    OutputString = "There is something wrong";
                }
            }

        }
        public string displayOrder()
        {
            string result ="";
            foreach (Order order in Database.Orders)
            {
                result += order.displayOrder();
            }
            return result;
        }
        public void changeOrderStatus()
        {
            Database.Orders.ElementAt(SelectedID-1).changeStatus( NewStatus);
        }
        public override void getNotify()
        {
            
        }
        public override string renderUI()
        {
          if (StateForStatus == "select")
            {
                OutputString = "You have chosen the Change Status Function"
                + Environment.NewLine + "Please enter the order Id: "
                + Environment.NewLine + "Or enter -1 to exit this function"
            + Environment.NewLine;
            }
            return OutputString;
        }
    }
}
