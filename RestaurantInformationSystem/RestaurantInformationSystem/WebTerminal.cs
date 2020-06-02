using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class WebTerminal : OrderingInterface
    {
        private int _webTerminalNumber;

        public int WebTerminalNumber { get => _webTerminalNumber; set => _webTerminalNumber = value; }
        public WebTerminal(int webTerminalNumber)
        {
            WebTerminalNumber = webTerminalNumber;
            DineInFlag = false;
        }
    }
}
