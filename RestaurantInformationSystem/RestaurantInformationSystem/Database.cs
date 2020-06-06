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

        public Database()
        {
            Menu = new Menu();
            Orders = new List<Order>();
            Reservations = new List<Reservation>();
            CurrentFunction = "";
            Login();
            createDatabaseTableIfNeccessary();
            gettingOrderFromDataBase();
        }
        public void createDatabaseTableIfNeccessary()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connection.Open();
                string sql = "CREATE TABLE IF NOT EXISTS orderRecords  (`id` int(11) NOT NULL auto_increment PRIMARY KEY,`numberOfItem` int(10) NOT NULL,`type` varchar(20) NOT NULL ,`status` varchar(20) NOT NULL,`transactionStatus` varchar(20) NOT NULL,`waitingTime` int(11) NOT NULL,`time` datetime NOT NULL)";
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
        public void gettingOrderFromDataBase()
        {
            try
            {
                Console.WriteLine("Connecting to MySQL to get data");
                connection.Open();
                string sql = "SELECT * FROM orderRecords INNER JOIN orderItems ON orderRecords.id = orderItems.orderId ";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                
                // Information for an order.
                int[] _id = new int[5];
                int[] _numberOfItem = new int[5];
                string[] _status = new string[5];
                bool[] _dineInFlag = new bool[5];
                string[] _transactionStatus = new string[5];
                int[] _orderWaitingTime = new int[5];
                string[] _dateTimes = new string[5];
                // Information for an menu item.
                int[] orderId = new int[5];
                int[] id = new int[5];
                string[] name = new string[5];
                double[] price = new double[5];
                int[] waitingTime = new int[5];
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
                    if (rdr[7] != null)
                    {
                        orderId[counter] = Convert.ToInt32(rdr[7]);
                        id[counter] = Convert.ToInt32(rdr[8]);
                        name[counter] = Convert.ToString(rdr[9]);
                        price[counter] = Convert.ToInt32(rdr[10]);
                        waitingTime[counter] = Convert.ToInt32(rdr[11]);
                    }
                    counter++;
                }
                rdr.Close();
                for (int l = 1; l <= counter; l++)
                {
                    createorder(_id[l], _numberOfItem[l], _status[l], _dineInFlag[l], _transactionStatus[l], _orderWaitingTime[l], _dateTimes[l], orderId[l], id[l], name[l], price[l], waitingTime[l]);
                }
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
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
                int[] _id = new int[5];
                string[] _dateTimes = new string[5];
                int[] _numberOfPeople = new int[5];
                string[] name = new string[5];
                int[] phoneNumber = new int[5];
                string[] email = new string[5];
                int counter = 0;
                while (rdr.Read())
                {
                    _id[counter] = Convert.ToInt32(rdr[0]);
                    _dateTimes[counter] = Convert.ToString(rdr[1]);
                    _numberOfPeople[counter] = Convert.ToInt32(rdr[2]);
                    name[counter] = Convert.ToString(rdr[4]);
                    phoneNumber[counter] = Convert.ToInt32(rdr[5]);
                    email[counter] = Convert.ToString(rdr[6]);
                    counter++;
                }
                rdr.Close();

                for (int l = 1; l <= counter; l++)
                {
                    DateTime time = DateTime.ParseExact(_dateTimes[l], "dd-MM-yyyy HH:mm tt", null);
                    createReservation(time, _numberOfPeople[l], name[l], phoneNumber[l].ToString(), email[l]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void createorder(int _id, int _numberOfItem, string _status, bool _dineInFlag, string transactionStatus, int _orderWaitingTime, string _dateTimes, int orderId, int id, string name, double price, int waitingTime)
        {
            List<MenuItem> orderItems = new List<MenuItem>();
            for (int i = 0; i < _numberOfItem; i++)
            {
                foreach (MenuItem item in Menu.MenuList)
                {
                    if (id == item.Id)
                        orderItems.Add(item);
                }
            }
            Order neworder = new Order(null,_id, _dineInFlag, orderItems);
            Orders.Add(neworder);
        }
        public void createReservation(DateTime time, int people, string name, string phoneNumber, string email)
        {
            int id = Reservations.Count() + 1;
            Reservation newReservation = new Reservation(id, time, people, name, phoneNumber, email);
        }

    }
}
