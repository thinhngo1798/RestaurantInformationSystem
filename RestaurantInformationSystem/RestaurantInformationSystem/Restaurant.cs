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
        private KitchenTerminal _kitchenTerminal;
        private GUI _gui;

        public Database Database { get => _database; set => _database = value; }
        public WebTerminal WebTerminal { get => _webTerminal; set => _webTerminal = value; }
        internal TableTerminal TableTerminal { get => _tableTerminal; set => _tableTerminal = value; }
        internal CashierTerminal CashierTerminal { get => _cashierTerminal; set => _cashierTerminal = value; }
        public KitchenTerminal KitchenTerminal { get => _kitchenTerminal; set => _kitchenTerminal = value; }
        public GUI Gui { get => _gui; set => _gui = value; }

        public Restaurant()
        {
            Database = new Database();
            WebTerminal = new WebTerminal("WEB1",Database);
            TableTerminal = new TableTerminal("TAB1",Database);
            CashierTerminal = new CashierTerminal("SYSTEM1", Database);
            KitchenTerminal = new KitchenTerminal("KIT1", Database);
            Gui = new GUI(this);
        }
    }
}
