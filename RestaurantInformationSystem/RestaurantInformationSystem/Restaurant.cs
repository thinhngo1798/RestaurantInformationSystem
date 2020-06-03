using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Restaurant
    {
        private Database _database;
        private TableTerminal _tableTerminal;
        private WebTerminal _webTerminal;
        private CashierTerminal _cashierTerminal;
        public Database Database { get => _database; set => _database = value; }
        public WebTerminal WebTerminal { get => _webTerminal; set => _webTerminal = value; }
        internal TableTerminal TableTerminal { get => _tableTerminal; set => _tableTerminal = value; }
        internal CashierTerminal CashierTerminal { get => _cashierTerminal; set => _cashierTerminal = value; }

        public Restaurant()
        {
            Database = new Database();
            WebTerminal = new WebTerminal(1,Database);
            TableTerminal = new TableTerminal(1,Database);
            CashierTerminal = new CashierTerminal(1, Database);
        }
    }
}
