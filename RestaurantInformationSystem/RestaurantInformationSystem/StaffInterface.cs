using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class StaffInterface
    {
        // Encapsulation and abstraction.
        private Database _database;
        private string _stateForStatus;
        private string _stateForStatistic;
        private string _stateForPayment;
        private string _stateForDeleteAnOrder;
        private string _stateForDeleteAReservation;
        private int    _selectedID;
        private int    _selectedTask;
        private string _newStatus;
        private string _outputString;
        private string _reservationNotification;
        private string _orderNotification;
        private string _changeOrderNotification;

        // Using auto properties. 
        public Database Database { get => _database; set => _database = value; }
        public string StateForStatus { get => _stateForStatus; set => _stateForStatus = value; }
        public int SelectedID { get => _selectedID; set => _selectedID = value; }
        public string NewStatus { get => _newStatus; set => _newStatus = value; }
        public string OutputString { get => _outputString; set => _outputString = value; }
        public string OrderNotification { get => _orderNotification; set => _orderNotification = value; }
        public string ReservationNotification { get => _reservationNotification; set => _reservationNotification = value; }
        public string ChangeOrderNotification { get => _changeOrderNotification; set => _changeOrderNotification = value; }
        public string StateForStatistic { get => _stateForStatistic; set => _stateForStatistic = value; }
        public int SelectedTask { get => _selectedTask; set => _selectedTask = value; }
        public string StateForPayment { get => _stateForPayment; set => _stateForPayment = value; }
        public string StateForDeleteAnOrder { get => _stateForDeleteAnOrder; set => _stateForDeleteAnOrder = value; }
        public string StateForDeleteAReservation { get => _stateForDeleteAReservation; set => _stateForDeleteAReservation = value; }

        public enum StateName { receive, select, type, done };

        public StaffInterface(Database database)
        {
            Database = database;
            StateForStatus = "select";
            StateForStatistic = "select";
            StateForPayment = "select";
            StateForDeleteAnOrder = "select";
            StateForDeleteAReservation = "select";
        }
        /// <summary>
        /// Interacting with the input from GUI.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="restaurant"></param>
        public void getInput(string input, Restaurant restaurant = null)
        {
            if (Database.CurrentFunction == "deleteAReservationFunction")
            {
                if (input == "-1")
                {
                    OutputString = "You have been quit the current request";
                    StateForDeleteAReservation = "exit";
                }
                else
                {
                    if (StateForDeleteAReservation == "select")
                    {
                        if (isANumber(input))
                        {
                            SelectedID = int.Parse(input);
                            Database.RemovingReservation(Database.Reservations.ElementAt(SelectedID - 1));
                            OutputString = "Your reservation has been deleted.";

                        }
                        else
                        {
                            OutputString = "Your selected id is invalid. Please enter the id of the order again: ";
                        }
                    }
                    else
                    {
                        OutputString = "There is something wrong";
                    }
                }
            }
            else if (Database.CurrentFunction == "deleteAnOrderFunction")
            {
                if (input == "-1")
                {
                    OutputString = "You have been quit the current request";
                    StateForDeleteAnOrder = "exit";
                }
                else
                {
                    if (StateForDeleteAnOrder == "select")
                    {
                        if (isANumber(input))
                        {
                            SelectedID = int.Parse(input);
                            Database.RemovingOrder(Database.Orders.ElementAt(SelectedID - 1));
                            OutputString = "Your order has been deleted.";
                            
                        }
                        else
                        {
                            OutputString = "Your selected id is invalid. Please enter the id of the order again: ";
                        }
                    }
                    else
                    {
                        OutputString = "There is something wrong";
                    }
                }
            }
            else if (Database.CurrentFunction == "paymentFunction")
            {
                if (input == "-1")
                {
                    OutputString = "You have been quit the current request";
                    StateForPayment = "exit";
                }
                else
                {
                    if (StateForPayment == "select")
                    {
                        if (isANumber(input))
                        {
                            SelectedID = int.Parse(input);
                            StateForPayment = "done";
                            OutputString = "Please enter the payment method";
                        }
                        else
                        {
                            OutputString = "Your selected id is invalid. Please enter the id of the order again: ";
                        }
                    }
                    else if (StateForPayment == "done")
                    {
                        if (input == "card" || input == "cash")
                        {
                            OutputString = "Your request is done"
                                + Environment.NewLine + "And here is your receipt: "
                                + Environment.NewLine + "Order Id: " + SelectedID
                                + Environment.NewLine + "Payment Method: " + input
                                + Environment.NewLine + "Cost: " + Database.Orders.ElementAt(SelectedID - 1).TotalPrice + " $"
                                + Environment.NewLine + "Time: " + DateTime.Now;
                            NewStatus = "PAID";
                            changeOrderStatus();
                            StateForPayment = "select";
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
            else if (Database.CurrentFunction == "statisticFunction")
            {
                if (input == "-1")
                {
                    OutputString = "You have been quit the current request";
                    StateForStatistic = "exit";
                }
                else
                {
                    if (StateForStatistic == "select")
                    {
                        if (isANumber(input))
                        {
                            SelectedTask = int.Parse(input);
                            if (SelectedTask == 1)
                            {
                                OutputString = Database.Statistic.MostCommonMenuItem();
                            }
                            else if (SelectedTask == 2)
                            {
                                OutputString = Database.Statistic.ShowMostCommonOrderTime();
                            }
                        }
                        else
                        {
                            OutputString = "Your selected id is invalid. Please enter the id of the order again: ";
                        }
                    }
                    else
                    {
                        OutputString = "There is something wrong";
                    }
                }
            }
            else
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
                            StateForStatus = "select";
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
            
            Database.Orders.ElementAt(SelectedID-1).changeStatus(NewStatus);
            Database.UpdateOrderStatus(Database.Orders.ElementAt(SelectedID - 1));
        }
        /// <summary>
        /// Creating User interface.
        /// </summary>
        /// <returns></returns>
        public string renderUI()
        {
            if (Database.CurrentFunction == "deleteAReservationFunction")
            {
                if (StateForDeleteAReservation == "exit")
                {
                    StateForDeleteAnOrder = "select";
                    return OutputString;
                }
                else if (StateForDeleteAReservation == "select")
                {
                    if (OutputString != "")
                    {
                        return OutputString;
                    }
                    else
                    {
                        OutputString = "You have chosen the Delete A Reservation Function"
                        + Environment.NewLine + "Please enter the reservation Id that you want to delete: "
                        + Environment.NewLine + "Or enter -1 to exit this function"
                    + Environment.NewLine;
                    }
                }
            }
            else if (Database.CurrentFunction == "deleteAnOrderFunction")
            {
                if (StateForDeleteAnOrder == "exit")
                {
                    StateForDeleteAnOrder = "select";
                    return OutputString;
                }
                else if (StateForDeleteAnOrder == "select")
                {
                    if (OutputString != "")
                    {
                        return OutputString;
                    }
                    else
                    {
                        OutputString = "You have chosen the Delete An Order Function"
                        + Environment.NewLine + "Please enter the order Id that you want to delete: "
                        + Environment.NewLine + "Or enter -1 to exit this function"
                    + Environment.NewLine;
                    }
                }
            }
            else if (Database.CurrentFunction == "paymentFunction")
            {
                if (StateForPayment == "exit")
                {
                    StateForStatus = "select";
                    return OutputString;
                }
                else if (StateForPayment == "select")
                {
                    if (OutputString != "")
                    {
                        return OutputString;
                    }
                    else
                    {
                        OutputString = "You have chosen the Payment Function"
                        + Environment.NewLine + "Please enter the order Id that you want to pay: "
                        + Environment.NewLine + "Or enter -1 to exit this function"
                    + Environment.NewLine;
                    }
                }
            }
            else if (Database.CurrentFunction == "statisticFunction")
            {
                if (StateForStatistic == "exit")
                {
                    StateForStatistic = "select";
                    return OutputString;
                }
                else if (StateForStatistic == "select")
                {
                    if (OutputString != "")
                    {
                        return OutputString;
                    }
                    else
                    {
                        OutputString = "You have chosen the Basic Statistic Function"
                          + Environment.NewLine + "Please choose one of the function below: "
                          + Environment.NewLine + "1. To determine what is the most common meal of the restaurant:"
                      + Environment.NewLine + "2 . To determine what is the most busiest time of the restaurant:"
                      + Environment.NewLine + "Or enter -1 to exit this function";
                    }
                }
            }
            else
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
            }
            return OutputString;

        }
    }
}
