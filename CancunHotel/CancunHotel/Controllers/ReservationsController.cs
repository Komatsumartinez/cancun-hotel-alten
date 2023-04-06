using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CancunHotel.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet]
        public IEnumerable<Reservation> GetReservations()
        {
            return _reservationRepository.GetReservations();
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(Guid id)
        {
            var reservation = _reservationRepository.GetReservation(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return reservation;
        }

        [HttpPost]
        public ActionResult<Reservation> CreateReservation(Reservation reservation)
        {
            if (reservation.StartDate < DateTime.Today.AddDays(1))
            {
                return BadRequest("Reservation start date must be at least tomorrow.");
            }
            if (reservation.StartDate.AddDays(3) < reservation.EndDate)
            {
                return BadRequest("Reservation can't be longer than 3 days.");
            }
            _reservationRepository.AddReservation(reservation);
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(Guid id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest("Invalid reservation ID.");
            }
            _reservationRepository.UpdateReservation(reservation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(Guid id)
        {
            _reservationRepository.DeleteReservation(id);
            return NoContent();
        }
    }
}
