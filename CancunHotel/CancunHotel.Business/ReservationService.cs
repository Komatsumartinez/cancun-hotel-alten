﻿using CancunHotel.Business.Contracts;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Business
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly JwtSecurityTokenHandler _jwtTokenHandler;
        private readonly IConfiguration _configuration;


        public ReservationService(IReservationRepository reservationRepository, IConfiguration configuration)
        {
            _reservationRepository = reservationRepository;
            _jwtTokenHandler = new JwtSecurityTokenHandler();
            _configuration = configuration;
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid id)
        {
            return await _reservationRepository.GetReservationByIdAsync(id);
        }

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