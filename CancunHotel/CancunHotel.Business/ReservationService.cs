using CancunHotel.Business.Contracts;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHotel.Business
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _reservationRepository.GetReservationByIdAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetAvailableReservationsAsync(DateTime startDate, DateTime endDate)
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync();
            return reservations.Where(r => (r.EndDate <= startDate || r.StartDate >= endDate));
        }

        // Implement methods for checking availability, placing, canceling, and modifying reservations
        public async Task<Reservation> PlaceReservationAsync(Reservation reservation)
        {
            if (reservation.StartDate < DateTime.Today.AddDays(1) ||
                reservation.StartDate > DateTime.Today.AddDays(30) ||
                (reservation.EndDate - reservation.StartDate).TotalDays > 3)
            {
                throw new ArgumentException("Invalid reservation dates");
            }

            var availableReservations = await GetAvailableReservationsAsync(reservation.StartDate, reservation.EndDate);
            if (!availableReservations.Any())
            {
                throw new InvalidOperationException("No available rooms");
            }

            return await _reservationRepository.AddReservationAsync(reservation);
        }

        public async Task<Reservation> CancelReservationAsync(Guid id)
        {
            if (await _reservationRepository.DeleteReservationAsync(id))
            {
                return null;
            }

            return await _reservationRepository.GetReservationByIdAsync(id);
        }

        public async Task<Reservation> ModifyReservationAsync(Guid id, Reservation updatedReservation)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return null;
            }

            reservation.StartDate = updatedReservation.StartDate;
            reservation.EndDate = updatedReservation.EndDate;
            if (reservation.StartDate < DateTime.Today.AddDays(1) ||
                reservation.StartDate > DateTime.Today.AddDays(30) ||
                (reservation.EndDate - reservation.StartDate).TotalDays > 3)
            {
                throw new ArgumentException("Invalid reservation dates");
            }

            var availableReservations = await GetAvailableReservationsAsync(reservation.StartDate, reservation.EndDate);
            if (!availableReservations.Any(r => r.Id == reservation.Id))
            {
                throw new InvalidOperationException("No available rooms");
            }

            return await _reservationRepository.UpdateReservationAsync(reservation);
        }
    }
}
