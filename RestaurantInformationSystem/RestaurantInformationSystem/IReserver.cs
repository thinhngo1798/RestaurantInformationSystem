using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public interface IReserver
    {
        List<Reservation> reservations { get; set; }
        void storeReservation();
        void createReservation();
        bool checkClashing();
    }
}
