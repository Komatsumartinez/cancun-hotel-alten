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
        string GetAvailableReservations(DateTime startDate, DateTime endDate);
        Task<Reservation> PlaceReservationAsync(Reservation reservation);
        Task<bool> CancelReservationAsync(Guid id);
        Task<Reservation> ModifyReservationAsync(Guid id, Reservation updatedReservation);
    }
}
