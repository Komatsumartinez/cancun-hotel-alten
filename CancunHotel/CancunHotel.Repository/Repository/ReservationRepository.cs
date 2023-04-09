using CancunHotel.Repository.Context;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancunHotel.Repository
{
    /// <summary>
    /// Implementation of <see cref="ReservationRepository"/>.
    /// </summary>
    /// <seealso cref="ReservationRepository" />
    /// <seealso cref="IReservationRepository" />
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ReservationRepository(HotelDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get overlapping reservations by dates.
        /// </summary>
        /// <param name="startDate">The Start Date of Reservation.</param>
        /// <param name="endDate">The End Date of Reservation..</param>
        /// <returns>The <see cref="Reservation"/>.</returns>
        public IEnumerable<Reservation> GetOverlappingReservations(DateTime startDate, DateTime endDate)
        {
            return _context.Reservations
                .Where(r =>
                    (r.StartDate <= endDate && r.EndDate >= startDate) 
                    || (r.StartDate >= startDate && r.StartDate <= endDate) 
                    || (r.EndDate >= startDate && r.EndDate <= endDate) 
                )
                .ToList();
        }

        /// <summary>
        /// Get Reservation <see cref="Reservation"/> by id async.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <returns>The <see cref="Reservation"/>.</returns>
        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        /// <summary>
        /// Add Reservation <see cref="Reservation"/> by model async.
        /// </summary>
        /// <param name="reservation">The Reservation Model.</param>
        /// <returns>The <see cref="Reservation"/>.</returns>
        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        /// <summary>
        /// Update Reservation <see cref="Reservation"/> by model async.
        /// </summary>
        /// <param name="reservation">The Reservation Model.</param>
        /// <returns>The <see cref="Reservation"/>.</returns>
        public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reservation;
        }

        /// <summary>
        /// Delete Reservation by reservation id async.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <returns>The <see cref="Reservation"/>.</returns>
        public async Task<bool> DeleteReservationAsync(Guid id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
