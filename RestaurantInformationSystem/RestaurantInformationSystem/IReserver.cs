﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public interface IReserver
    {
        void CreateReservation(DateTime time, int people, string name, string phoneNumber, string email);
        string RetreiveReservation();
        void DeleteReservation(int id);
        bool checkClashing(DateTime time);
    }
}
