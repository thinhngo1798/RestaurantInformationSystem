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
        private string _notification;
        public Database Database { get => _database; set => _database = value; }
        public string StateForStatus { get => _stateForStatus; set => _stateForStatus = value; }
        public int SelectedID { get => _selectedID; set => _selectedID = value; }
        public string NewStatus { get => _newStatus; set => _newStatus = value; }
        public string OutputString { get => _outputString; set => _outputString = value; }
        public string Notification { get => _notification; set => _notification = value; }

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
            Database.Orders.ElementAt(SelectedID-1).Status = NewStatus;
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
