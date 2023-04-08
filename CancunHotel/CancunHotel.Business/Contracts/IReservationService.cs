using CancunHotel.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Business.Contracts
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationByIdAsync(Guid id);
        Task<IEnumerable<Reservation>> GetAvailableReservationsAsync(DateTime startDate, DateTime endDate);
        Task<Reservation> PlaceReservationAsync(Reservation reservation);
        Task<Reservation> CancelReservationAsync(Guid id);
        Task<Reservation> ModifyReservationAsync(Guid id, Reservation updatedReservation);

    }
}
