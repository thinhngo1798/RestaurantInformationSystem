using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public interface IReserver
    {
        void createReservation(int id, DateTime time, int people, string name, string phoneNumber, string email);
        string retreiveReservation();
        void deleteReservation(int id);
        bool checkClashing(DateTime time);
    }
}
