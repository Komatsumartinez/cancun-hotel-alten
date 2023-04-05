using CancunHotel.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Repository.Contracts
{
    public interface IReservationRepository
    {
        Reservation GetReservation(Guid id);
        IEnumerable<Reservation> GetReservations();
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(Guid id);
    }
}
