using CancunHotel.Business.Models;
using CancunHotel.Repository.Models;
using CancunHotel.Tests.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CancunHotel.Tests.ReservationController.PlaceReservation
{
    public class PlaceReservationTests : ReservationControllerTests
    {
        [Fact]
        public async Task Will_Return_Reservation()
        {
            #region Variables        

            var reservation = new Reservation
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 12)
            };

            var reservationDto = new ReservationDto
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 12)
            };

            #endregion

            #region Setup       
            MockService.Setup(s => s.PlaceReservationAsync(It.IsAny<Reservation>()))
                .ReturnsAsync(reservation)
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.PlaceReservation(reservationDto) as ObjectResult;
            #endregion

            #region Verify
            MockService.Verify(s => s.PlaceReservationAsync(It.IsAny<Reservation>()), Times.Once);
            #endregion

            #region Assert     
            Assert.IsType<ObjectResult>(result);
            Assert.Equal("Reservation made successfully.", result.Value.GetType().GetProperty("message").GetValue(result.Value, null));
            Assert.Equal(reservation, result.Value.GetType().GetProperty("placedReservation").GetValue(result.Value, null));
            #endregion
        }

        [Fact]
        public async Task Will_Return_Invalid_Dates()
        {
            #region Variables   
            var message = "Invalid date range or reservation period.";

            var reservationDto = new ReservationDto
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 12)
            };
            #endregion

            #region Setup       
            MockService.Setup(s => s.PlaceReservationAsync(It.IsAny<Reservation>()))
                .ThrowsAsync(new ArgumentException(message))
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.PlaceReservation(reservationDto);
            #endregion

            #region Verify
            MockService.Verify(s => s.PlaceReservationAsync(It.IsAny<Reservation>()), Times.Once);
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(message, conflictResult.Value);
            #endregion
        } 

        [Fact]
        public async Task Will_Return_Not_Availability()
        {
            #region Variables     
            var message = "There is already a reservation for the requested dates.";

            var reservationDto = new ReservationDto
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 12)
            };
            #endregion

            #region Setup       
            MockService.Setup(s => s.PlaceReservationAsync(It.IsAny<Reservation>()))
                .ThrowsAsync(new InvalidOperationException(message))
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.PlaceReservation(reservationDto);
            #endregion

            #region Verify
            MockService.Verify(s => s.PlaceReservationAsync(It.IsAny<Reservation>()), Times.Once);
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(message, conflictResult.Value);
            #endregion
        }

    }
}
