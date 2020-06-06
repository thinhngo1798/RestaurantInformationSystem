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
        private string port;
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
            port = "3306";
            sslM = "none";

            connectionString = String.Format("server={0};database={1};userid={2}; password={3}; SslMode = {4}", server, database, user, password, sslM);

            connection = new MySqlConnection(connectionString);
        }

        private void connect()
        {
            try
            {
                connection.Open();

                Console.WriteLine("successful connection");

                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message + connectionString);
            }
        }

        public Database()
        {
            Login();
            connect();
            Menu = new Menu();
            Orders = new List<Order>();
            Reservations = new List<Reservation>();
            CurrentFunction = "";
        }

    }
}
