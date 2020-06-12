using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public class Statistic
    {
        private int[] _itemArray;
        private Database _database;

        /// <summary>
        /// Array contains the frequency of being ordered of each menu item.
        /// </summary>
        public int[] ItemArray { get => _itemArray; set => _itemArray = value; }
        public Database Database { get => _database; set => _database = value; }
        /// <summary>
        /// Accessing database to make statistic summary.
        /// </summary>
        /// <param name="database"></param>
        public Statistic(Database database)
        {
            ItemArray = new int[7];
            Database = database;
        }

        /// <summary>
        /// Function 1: The meal that has been ordered the most.
        /// </summary>
        /// <returns></returns>
        public string MostCommonMenuItem()
        {
            string result = "";
            for (int g=0; g<=6; g++)
            {
                ItemArray[g] = 0;
            }
            foreach (Order order in Database.Orders)
            {
                foreach (MenuItem item in order.MenuItems)
                {
                    ItemArray[item.Id]++;
                }
            }
            bool Flag = false;
            for (int g = 0; g <= 6; g++)
            {
                if (ItemArray[g] != 0)
                {
                    Flag = true;
                }
            }
            if (Flag)
            {
                int max = ItemArray[1];
                int maxid = 1;
                for (int i = 2; i <= 6; i++)
                {
                    if (ItemArray[i] > max)
                    {
                        max = ItemArray[i];
                        maxid = i;
                    }
                }
                MenuItem mostCommonDish = Database.Menu.MenuList[maxid-1];
                result = "The most common menu item/dish is: " + mostCommonDish.Name + " with " + max + " times being ordered."
                + Environment.NewLine + " The detail of this menu item is: "
                + Environment.NewLine + "Price:" + mostCommonDish.Price + "$       Waiting time:" + mostCommonDish.WaitingTime + Environment.NewLine;
                return result;
            }
            else
            {
                return "There is no data";
            }
        }
        /// <summary>
        /// The busiest time of the date for the restaurant.
        /// </summary>
        /// <returns></returns>
        public string ShowMostCommonOrderTime()
        {
            for (int g = 0; g <= 6; g++)
            {
                ItemArray[g] = 0;
            }
            List<TimePeriod> timePeriods = new List<TimePeriod>();

            TimeSpan morning = new TimeSpan(8, 0, 0);
            TimeSpan noon = new TimeSpan(12, 0, 0);
            TimeSpan afternoon = new TimeSpan(16, 0, 0);
            TimeSpan evening = new TimeSpan(20, 0, 0);


            TimePeriod morningP = new TimePeriod(morning, "morning");
            TimePeriod noonP = new TimePeriod(noon, "noon");
            TimePeriod afternoonP = new TimePeriod(afternoon, "afternoon");
            TimePeriod eveningP = new TimePeriod(evening, "evening");

            timePeriods.Add(morningP);
            timePeriods.Add(noonP);
            timePeriods.Add(afternoonP);
            timePeriods.Add(eveningP);

            TimeSpan test = new TimeSpan(3, 0, 0);
            foreach (Order order in Database.Orders)
            {
                DateTime dateTime = DateTime.ParseExact(order.OrderTime, "dd-MM-yyyy HH:mm tt", null);
                TimeSpan x =  dateTime.TimeOfDay;
                foreach (TimePeriod p in timePeriods)
                {
                    if ((x - p.Time) < test)
                        p.Count++;
                }
            }
            int max = 0;
            TimePeriod maxPeriod = new TimePeriod(morning,"morning");
            foreach (TimePeriod p in timePeriods)
            {
                if (p.Count >max)
                {
                    max = p.Count;
                    maxPeriod = p;
                }
            }
            if (maxPeriod.Name == morningP.Name)
            {
                return "The busiest time of the shop is " + maxPeriod.Name;
            }
            else if (maxPeriod.Name == noonP.Name)
            {
                return "The busiest time of the shop is " + maxPeriod.Name;
            }
            else if (maxPeriod.Name == afternoonP.Name)
            {
                return "The busiest time of the shop is " + maxPeriod.Name;
            }
            else if (maxPeriod.Name == eveningP.Name)
            {
                return "The busiest time of the shop is " + maxPeriod.Name;
            }
            else
                return "There is something wrong.";

        }
    }
}
