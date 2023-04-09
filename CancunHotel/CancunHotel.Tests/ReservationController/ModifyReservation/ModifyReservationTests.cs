using CancunHotel.Business.Models;
using CancunHotel.Repository.Models;
using CancunHotel.Tests.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CancunHotel.Tests.ReservationController.ModifyReservation
{
    public class ModifyReservationTests : ReservationControllerTests
    {
        [Fact]
        public async Task Will_Return_Modified_Reservation()
        {
            #region Variables    
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
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
                StartDate = new DateTime(2023, 4, 11),
                EndDate = new DateTime(2023, 4, 13)
            };

            #endregion

            #region Setup       
            MockService.Setup(s => s.ModifyReservationAsync(reservationId, It.IsAny<Reservation>()))
                .ReturnsAsync(reservation)
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.ModifyReservation(reservationId, reservationDto) as OkObjectResult;
            #endregion

            #region Verify
            MockService.Verify(s => s.ModifyReservationAsync(reservationId, It.IsAny<Reservation>()), Times.Once);
            #endregion

            #region Assert     
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            Assert.Equal("Reservation modified successfully.", result.Value);
            #endregion
        }

        [Fact]
        public async Task Will_Return_Not_Found_Reservation()
        {
            #region Variables        
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var exceptionMessage = "Reservation was not found.";

            var reservationDto = new ReservationDto
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 12)
            };
            #endregion

            #region Setup       
            MockService.Setup(s => s.ModifyReservationAsync(reservationId, It.IsAny<Reservation>()))
                .ThrowsAsync(new InvalidOperationException(exceptionMessage))
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.ModifyReservation(reservationId, reservationDto);
            #endregion

            #region Verify
            MockService.Verify(s => s.ModifyReservationAsync(reservationId, It.IsAny<Reservation>()), Times.Once);
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(exceptionMessage, conflictResult.Value);
            #endregion
        }

        [Fact]
        public async Task Will_Return_Invalid_Date_Range_Reservation()
        {
            #region Variables        
            var exceptionMessage = "Invalid date range or reservation period.";   
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");

            var reservationDto = new ReservationDto
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 12)
            };
            #endregion

            #region Setup       
            MockService.Setup(s => s.ModifyReservationAsync(reservationId, It.IsAny<Reservation>()))
                .ThrowsAsync(new ArgumentException(exceptionMessage))
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.ModifyReservation(reservationId, reservationDto);
            #endregion

            #region Verify
            MockService.Verify(s => s.ModifyReservationAsync(reservationId, It.IsAny<Reservation>()), Times.Once);
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(exceptionMessage, conflictResult.Value);
            #endregion
        }
    }
}
