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
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(Guid id);
        Task<Reservation> AddReservationAsync(Reservation reservation);
        Task<Reservation> UpdateReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(Guid id);
        IEnumerable<Reservation> GetOverlappingReservations(DateTime startDate, DateTime endDate);
    }
}
