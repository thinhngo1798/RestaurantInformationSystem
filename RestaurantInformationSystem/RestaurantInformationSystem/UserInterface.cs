using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public abstract class UserInterface
    {
        private List<string> _input;

        public List<string> Input { get => _input; set => _input = value; }

        public UserInterface()
        {
            Input = new List<string>();
        }
        public abstract void getInput(string input, Restaurant restaurant);
        public abstract string renderUI();
        public abstract void getNotify();
    }
}
