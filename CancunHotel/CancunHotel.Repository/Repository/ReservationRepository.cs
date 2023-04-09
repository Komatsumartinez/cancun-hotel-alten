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
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDBContext _context;

        public ReservationRepository(HotelDBContext context)
        {
            _context = context;
        }

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

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _context.Reservations.FindAsync(id);
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return reservation;
        }

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
