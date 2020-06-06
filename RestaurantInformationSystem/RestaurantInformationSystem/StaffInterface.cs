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
        public override void getInput(string input, Restaurant restaurant = null)
        {
            if (input == "-1")
            {
                OutputString = "You have been quit the current request";
                StateForStatus = "exit";
            }
            else
            {
                if (StateForStatus == "select")
                {
                    if (isANumber(input))
                    {
                        SelectedID = int.Parse(input);
                        StateForStatus = "done";
                        OutputString = "Please enter the new status";
                    }
                    else
                    {
                        OutputString = "Your selected id is invalid. Please enter the id of the order again: ";
                    }
                }
                else if (StateForStatus == "done")
                {
                    if (isValid(input))
                    {
                        OutputString = "Your request is done";
                        NewStatus = input;
                        changeOrderStatus();
                    }
                    else
                    {
                        OutputString = "Your input status is invalid. Please enter the status of the order again: ";
                    }

                }
                else
                {
                    OutputString = "There is something wrong";
                }
            }

        }
        public bool isANumber(string input)
        {
            int i = 0;
            return int.TryParse(input, out i);
        }
        public bool isValid(string inputText)
        {
            string input = inputText.ToUpper();
            if ((input != "PENDING") && (input != "READY") && (input != "PAID") && (input != "CANCEL"))
            {
                return false;
            }
            return true;
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
            if (StateForStatus == "exit")
            {
                StateForStatus = "select";
                return OutputString;
            }
            else if (StateForStatus == "select")
            {
                if (OutputString != "")
                {
                    return OutputString;
                }
                else
                {
                    OutputString = "You have chosen the Change Status Function"
                    + Environment.NewLine + "Please enter the order Id: "
                    + Environment.NewLine + "Or enter -1 to exit this function"
                + Environment.NewLine;
                }
            }
            return OutputString;
        }
    }
}
