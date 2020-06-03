using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    class TableTerminal :OrderingInterface
    {
        private int _tableNumber;
        

        public int TableNumber { get => _tableNumber; set => _tableNumber = value; }

        public TableTerminal(int tableNumber, Database database) : base(database)
        {
            TableNumber = tableNumber;
            DineInFlag = true;
        }
    }
}
