using CancunHotel.Business.Contracts;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHotel.Business
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationService"/> class.
        /// </summary>
        /// <param name="reservationRepository">The Reservation Repository.</param>
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Get a reservation by id.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <returns>The <see cref="Reservation"/> founded.</returns>
        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _reservationRepository.GetReservationByIdAsync(id);
        }

        /// <summary>
        /// Get Availability for the room
        /// </summary>
        /// <param name="startDate">Initial date to search.</param>
        /// <param name="endDate">Final date to search.</param>
        /// <returns>The availabiity message <see cref="string"/>.</returns>
        public string GetAvailableReservations(DateTime startDate, DateTime endDate)
        {
            var message = "The room is available for the requested dates.";
            
            var isOverlapping = _reservationRepository.GetOverlappingReservations(startDate, endDate);

            if (isOverlapping.Any())
            {
                throw new InvalidOperationException("There is already a reservation for the requested dates.");
            }

            return message;
        }

        /// <summary>
        /// Add a new reservation.
        /// </summary>
        /// <param name="reservation">The model <see cref="Reservation"/>.</param>
        /// <returns>The added <see cref="Reservation"/>.</returns>
        public async Task<Reservation> PlaceReservationAsync(Reservation reservation)
        {
            if (reservation.EndDate < reservation.StartDate.AddDays(1) || 
                reservation.EndDate > reservation.StartDate.AddDays(3) || 
                reservation.StartDate < DateTime.Today.AddDays(1) || 
                reservation.StartDate > DateTime.Today.AddDays(30))
            {

                throw new ArgumentException("Invalid date range or reservation period.");
            }

            var isAvailable = GetAvailableReservations(reservation.StartDate, reservation.EndDate);

            var reservationCreated = isAvailable != "" ? await _reservationRepository.AddReservationAsync(reservation) : new Reservation();

            return reservationCreated;
        }

        /// <summary>
        /// Cancel Reservation by id async.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <returns><see cref="Bool"/></returns>
        public async Task<bool> CancelReservationAsync(Guid id)
        { 
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation != null)
            {
                var canceled = await _reservationRepository.DeleteReservationAsync(id);

                return canceled;
            }

            throw new ArgumentException("Reservation was not found");
        }

        /// <summary>
        /// Modify Reservation by id async
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <param name="updatedReservation">The model <see cref="Reservation"/>.</param>
        /// <returns>The updated <see cref="Reservation"/>.</returns>
        public async Task<Reservation> ModifyReservationAsync(Guid id, Reservation updatedReservation)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                throw new InvalidOperationException("Reservation was not found.");
            }

            reservation.StartDate = updatedReservation.StartDate;
            reservation.EndDate = updatedReservation.EndDate;

            if (reservation.EndDate < reservation.StartDate.AddDays(1) ||
                 reservation.EndDate > reservation.StartDate.AddDays(3) ||
                 reservation.StartDate < DateTime.Today.AddDays(1) ||
                 reservation.StartDate > DateTime.Today.AddDays(30))
            {
                throw new ArgumentException("Invalid date range or reservation period.");
            }

            var isAvailable = GetAvailableReservations(reservation.StartDate, reservation.EndDate);

            var reservationCreated = isAvailable != "" ? await _reservationRepository.UpdateReservationAsync(reservation) : new Reservation();

            return reservationCreated;
        }
    }
}
