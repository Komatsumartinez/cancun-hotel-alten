using AutoMapper;
using CancunHotel.Business;
using CancunHotel.Business.Models;
using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CancunHotel.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(ReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet("availability")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAvailability([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var availableReservations = await _reservationService.GetAvailableReservationsAsync(startDate, endDate);
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(availableReservations));
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> PlaceReservation(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(createReservationDto);
            var placedReservation = await _reservationService.PlaceReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = placedReservation.Id }, _mapper.Map<ReservationDto>(placedReservation));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById(Guid id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReservationDto>(reservation));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyReservation(Guid id, UpdateReservationDto updateReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(updateReservationDto);
            reservation.Id = id;

            try
            {
                await _reservationService.ModifyReservationAsync(id, reservation);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            var canceledReservation = await _reservationService.CancelReservationAsync(id);
            if (canceledReservation == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
