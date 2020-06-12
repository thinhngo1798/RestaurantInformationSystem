using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RestaurantInformationSystem
{
    public class Database
    {
       
        private List<Order> _orders;
        private Menu _menu;
        private List<Reservation> _reservations;
        private string _currentFunction;
        private Statistic _statistic;

        // For MySQL Database
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string connectionString;
        private string sslM;

        internal List<Order> Orders { get => _orders; set => _orders = value; }
        internal Menu Menu { get => _menu; set => _menu = value; }
        public List<Reservation> Reservations { get => _reservations; set => _reservations = value; }
        public string CurrentFunction { get => _currentFunction; set => _currentFunction = value; }
        public Statistic Statistic { get => _statistic; set => _statistic = value; }
        /// <summary>
        /// This contructor has the responsibility to initialize orders, reservation, menu and 
        /// also saving or retrieving data to Database MariaDB MySql.
        /// </summary>
        public Database()
        {
            Menu = new Menu();
            Orders = new List<Order>();
            Reservations = new List<Reservation>();
            Statistic = new Statistic(this);
            CurrentFunction = "";
            Login();
            createDatabaseTableIfNeccessary();
            gettingOrderFromDataBase();
            gettingReservation();
        }
        /// <summary>
        /// Login into MariaDb
        /// </summary>
        public void Login()
        {

            server = "feenix-mariadb.swin.edu.au";
            database = "s101766060_db";
            user = "s101766060";
            password = "Thinh123";
            sslM = "none";

            connectionString = String.Format("server={0};database={1};userid={2}; password={3}; SslMode = {4}", server, database, user, password, sslM);

            connection = new MySqlConnection(connectionString);
        }
        /// <summary>
        /// Adding orders to database (offline) and MariaDb (online).
        /// </summary>
        /// <param name="order"></param>
        public void AddingOrder(Order order)
        {
            Orders.Add(order);
            saveOrder(order);
            foreach (MenuItem item in order.MenuItems)
            {
                saveItem(order.Id, item);
            }
        }
        /// <summary>
        /// Removing order from both database.
        /// </summary>
        /// <param name="order"></param>
        public void RemovingOrder(Order order)
        {
            Orders.Remove(order);
            removeOrder(order);
            removeItemOfAnOrder(order);
        }
        /// <summary>
        /// Adding reservations to both database.
        /// </summary>
        /// <param name="reservation"></param>
        public void AddingReservation(Reservation reservation)
        {
            Reservations.Add(reservation);
            saveReservation(reservation);
        }
        /// <summary>
        /// Removing Reservation
        /// </summary>
        /// <param name="reservation"></param>
        public void RemovingReservation(Reservation reservation)
        {
            Reservations.Remove(reservation);
            RemoveReservation(reservation);
        }
        /// <summary>
        /// Removing reservation online.
        /// </summary>
        /// <param name="reservation"></param>
        public void RemoveReservation(Reservation reservation)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to delete Reservation");
                connection.Open();
                string sqlTemplate = "DELETE FROM reservations WHERE id = {0};";
                string sql = string.Format(sqlTemplate, reservation.Id);
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Removing order online.
        /// </summary>
        /// <param name="order"></param>
        public void removeOrder(Order order)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to save Order");
                connection.Open();
                string sqlTemplate = "DELETE FROM orderRecords WHERE id = {0};";
                string sql = string.Format(sqlTemplate, order.Id);
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Save orders to online database.
        /// </summary>
        /// <param name="order"></param>
        public void saveOrder(Order order)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to save Order");
                connection.Open();
                string type = "";
                if (order.DineInFlag == true)
                {
                    type = "dine in";
                }
                string sqlTemplate = "INSERT INTO orderRecords (id,numberOfItem,type,status,transactionStatus,waitingTime,time) VALUES ({0},{1},'{2}','{3}','{4}',{5},'{6}');";
                string sql = string.Format(sqlTemplate,order.Id, order.MenuItems.Count(), type, order.Status, order.Transaction.Status, order.OrderWaitingTime, order.OrderTime);
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Saving menu item to online database.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="item"></param>
        public void saveItem(int orderId, MenuItem item)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to save menu item");
                connection.Open();
                string sqlTemplate = "INSERT INTO orderItems (orderId,id,name,price,waitingTime) VALUES ({0},{1},'{2}',{3},'{4}');";
                string sql = string.Format(sqlTemplate, orderId, item.Id , item.Name, item.Price, item.WaitingTime);
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void removeItemOfAnOrder(Order order)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to delete items of an order");
                connection.Open();
                string sqlTemplate = "DELETE FROM orderItems WHERE orderId = {0};";
                string sql = string.Format(sqlTemplate, order.Id);
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Saving reservation to online database.
        /// </summary>
        /// <param name="reservation"></param>
        public void saveReservation(Reservation reservation)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to save Order");
                connection.Open();

                string sqlTemplate = "INSERT INTO reservations (time,numberOfPeople,name,phone,email) VALUES ('{0}','{1}','{2}','{3}','{4}');";
                string sql = string.Format(sqlTemplate,reservation.Time.ToString("dd-MM-yyyy HH:mm tt"), reservation.NumberOfPeople, reservation.CustomerName, Convert.ToInt32(reservation.PhoneNumber), reservation.Email);
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Creating database if it does not exist. It helps preventing bugs.
        /// </summary>
        public void createDatabaseTableIfNeccessary()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();
                string sql = "CREATE TABLE IF NOT EXISTS orderRecords  (`id` int(11) NOT NULL,`numberOfItem` int(10) NOT NULL,`type` varchar(20) NOT NULL ,`status` varchar(20) NOT NULL,`transactionStatus` varchar(20) NOT NULL,`waitingTime` int(11) NOT NULL,`time` varchar(20) NOT NULL,`primaryId` int(11) auto_increment PRIMARY KEY)";
                string sql1 = "CREATE TABLE IF NOT EXISTS orderItems (`primaryId` int(11) NOT NULL auto_increment PRIMARY KEY,`orderId` int(10) NOT NULL,`id` int(10) NOT NULL,`name` varchar(20) NOT NULL,`price` float NOT NULL,`waitingTime` int(11) NOT NULL)";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
                MySqlCommand cmd1 = new MySqlCommand(sql1, connection);
                MySqlDataReader rdr1 = cmd1.ExecuteReader();
                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            connection.Close();
            Console.WriteLine("Done.");
        }
        /// <summary>
        /// Getting order from the database when starting the application.
        /// </summary>
        public void gettingOrderFromDataBase()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to get data");
                connection.Open();
                string sql = "SELECT * FROM orderRecords";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();

               
                // Information for an order.
                int[] _id = new int[10];
                int[] _numberOfItem = new int[10];
                string[] _status = new string[10];
                bool[] _dineInFlag = new bool[10];
                string[] _transactionStatus = new string[10];
                int[] _orderWaitingTime = new int[10];
                string[] _dateTimes = new string[10];
                // Information for an menu item.
                int[] id = new int[10];
                int counter = 0;
                while (rdr.Read())
                {
                    _id[counter] = Convert.ToInt32(rdr[0]);
                    _numberOfItem[counter] = Convert.ToInt32(rdr[1]);
                    _dineInFlag[counter] = ("dine in" == Convert.ToString(rdr[2]));
                    _status[counter] = Convert.ToString(rdr[3]);
                    _transactionStatus[counter] = Convert.ToString(rdr[4]);
                    _orderWaitingTime[counter] = Convert.ToInt32(rdr[5]);
                    _dateTimes[counter] = Convert.ToString(rdr[6]);
                    counter++;
                }
                rdr.Close();
                for (int i = 0; i < counter; i++)
                {
                    string template = "SELECT * FROM orderItems WHERE orderId = {0}";
                    string sql1 = String.Format(template, _id[i]);
                    MySqlCommand cmd1 = new MySqlCommand(sql1, connection);
                    MySqlDataReader rdr1 = cmd1.ExecuteReader();
                    int counter1 = 0;
                    while (rdr1.Read())
                    {
                        id[counter1] = Convert.ToInt32(rdr1[1]);
                        counter1++;
                    }
                    rdr1.Close();
                }   
                connection.Close();
                Console.WriteLine("Done.");
                for (int l = 0; l < counter; l++)
                {
                    createorder(_id[l], _numberOfItem[l], _status[l], _dineInFlag[l], _transactionStatus[l], _orderWaitingTime[l], _dateTimes[l],id);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Getting order from the database when starting the application.
        /// </summary>
        public void gettingReservation()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to get reservation");
                connection.Open();
                string sql = "SELECT * FROM reservations";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();

                // Information for an order.
                int[] _id = new int[10];
                string[] _dateTimes = new string[10];
                int[] _numberOfPeople = new int[10];
                string[] name = new string[10];
                int[] phoneNumber = new int[10];
                string[] email = new string[10];
                int counter = 0;
                while (rdr.Read())
                {
                    _id[counter] = Convert.ToInt32(rdr[0]);
                    _dateTimes[counter] = Convert.ToString(rdr[1]);
                    _numberOfPeople[counter] = Convert.ToInt32(rdr[2]);
                    name[counter] = Convert.ToString(rdr[3]);
                    phoneNumber[counter] = Convert.ToInt32(rdr[4]);
                    email[counter] = Convert.ToString(rdr[5]);
                    counter++;
                }
                rdr.Close();

                for (int l = 0; l < counter; l++)
                {
                    DateTime time = DateTime.ParseExact(_dateTimes[l], "dd-MM-yyyy HH:mm tt", null);
                    createReservation(time, _numberOfPeople[l], name[l], phoneNumber[l].ToString(), email[l]);
                }
                connection.Close();
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void createorder(int _id, int _numberOfItem, string _status, bool _dineInFlag, string transactionStatus, int _orderWaitingTime, string _dateTimes, int[] id)
        {
            List<MenuItem> orderItems = new List<MenuItem>();
            for (int i = 0; i < _numberOfItem; i++)
            {
                foreach (MenuItem item in Menu.MenuList)
                {
                    if (id[i] == item.Id)
                    {
                        orderItems.Add(item);
                    }
                }
            }
            Order neworder = new Order(null,_id, _dineInFlag, orderItems);
            Orders.Add(neworder);
        }


        public void createReservation(DateTime time, int people, string name, string phoneNumber, string email)
        {
            int id = Reservations.Count() + 1;
            Reservation newReservation = new Reservation(id, time, people, name, phoneNumber, email);
            Reservations.Add(newReservation);
        }
        public void UpdateOrderStatus(Order order)
        {
            RemovingOrder(order);
            AddingOrder(order);
        }
    }
}
