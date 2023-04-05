using System;
using System.Collections.Generic;
using System.Linq;
using CancunHotel.Repository.Context;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;

namespace CancunHotel.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Reservation GetReservation(Guid id)
        {
            return _dbContext.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return _dbContext.Reservations.ToList();
        }

        public void AddReservation(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();
        }

        public void UpdateReservation(Reservation reservation)
        {
            _dbContext.Reservations.Update(reservation);
            _dbContext.SaveChanges();
        }

        public void DeleteReservation(Guid id)
        {
            var reservation = GetReservation(id);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                _dbContext.SaveChanges();
            }
        }
    }
}