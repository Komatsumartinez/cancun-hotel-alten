using AutoMapper;
using CancunHotel.Business;
using CancunHotel.Business.Contracts;
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
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController" /> class.
        /// </summary>
        /// <param name="reservationService">The integration service.</param>
        /// <param name="mapper">Mapper profiles</param>
        /// <exception cref="ArgumentNullException">service</exception>
        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// GetAvailability by dates.
        /// </summary>
        /// <param name="startDate">The start date of the reservation.</param>
        /// <param name="endDate">The end date of the reservation.</param>
        /// <returns>
        ///   <see cref="ReservationDto" /> Reserva.
        /// </returns>
        /// <response code="200">Returns the avilable message.</response>
        /// <response code="400">There was an error with the availability process.</response>
        /// <response code="401">User is not authorised.</response>
        /// <response code="409">There is already a reservation for the requested dates.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpGet("availability")]
        public ActionResult GetAvailability([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                if (startDate == null || endDate == null)
                {
                    return BadRequest("StartDate and EndDate parameters are required.");
                }

                if (endDate <= startDate)
                {
                    return BadRequest("EndDate must be greater than StartDate.");
                }

                var availability = _reservationService.GetAvailableReservations(startDate.Value, endDate.Value);

                return Ok(availability);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Create Reservation
        /// </summary>
        /// <param name="createReservationDto">The Reservation Info size  <see cref="ReservationDto" />.</param>
        /// <response code="200">Reservation created</response>
        /// <response code="400">There was an error processing the Reservation.</response>
        /// <response code="401">User is not authorised.</response>
        /// <response code="409">There is already a reservation for the requested dates.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpPost]
        public async Task<ActionResult> PlaceReservation(ReservationDto createReservationDto)
        {
            try
            {
                var reservation = _mapper.Map<Reservation>(createReservationDto);
                var placedReservation = await _reservationService.PlaceReservationAsync(reservation);
                return new ObjectResult(new { message = "Reservation made successfully.", placedReservation });
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Get Reservation by id.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <returns>
        ///   <see cref="ReservationDto" />.
        /// </returns>
        /// <response code="200">Returns the Reservartion</response>
        /// <response code="400">There was an error getting the reservation.</response>
        /// <response code="401">User is not authorised.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(Guid id)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return Ok("No reservation found.");
            }

            return Ok(_mapper.Map<ReservationDto>(reservation));
        }

        /// <summary>
        /// Modify Reservation by id.
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <param name="updateReservationDto">The Reservation dto. <see cref="ReservationDto" /></param>
        /// <response code="200">Reservation updated</response>
        /// <response code="400">There was an error processing the reservation.</response>
        /// <response code="401">User is not authorised.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyReservation(Guid id, ReservationDto updateReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(updateReservationDto);
            reservation.Id = id;

            try
            {
                var modifyReservation = await _reservationService.ModifyReservationAsync(id, reservation);
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Reservation modified successfully.");
        }

        /// <summary>
        /// Cancel Reservation by id
        /// </summary>
        /// <param name="id">The Reservation id.</param>
        /// <response code="200">Reservation canceled</response>
        /// <response code="400">There was an error canceling the Reservation.</response>
        /// <response code="401">User is not authorised.</response>
        /// <response code="409">Reservation was not found on database.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            try
            {
                var canceledReservation = await _reservationService.CancelReservationAsync(id);

                return Ok("Reservation cancelled successfully.");
                
            }
            catch (ArgumentException ex)
            {
                return Conflict(ex.Message);
            }

        }
    }
}
