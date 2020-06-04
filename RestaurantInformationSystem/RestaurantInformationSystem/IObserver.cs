using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }

}
