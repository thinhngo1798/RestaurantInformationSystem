using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformationSystem
{
    public interface ISubject
    {
        /// <summary>
        /// Attach an observver to the subject
        /// </summary>
        /// <param name="observer"></param>
        void Attach(IObserver observer);
        /// <summary>
        /// Detach an observer from the subject.
        /// </summary>
        /// <param name="observer"></param>
        void Detach(IObserver observer);

        /// <summary>
        /// Notify all observers about an event.
        /// </summary>
        void Notify();

    }
}
