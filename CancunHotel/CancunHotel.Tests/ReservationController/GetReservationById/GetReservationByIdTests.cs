using CancunHotel.Business.Models;
using CancunHotel.Repository.Models;
using CancunHotel.Tests.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CancunHotel.Tests.ReservationController.GetReservationById
{
    public class GetReservationByIdTests : ReservationControllerTests
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
            MockService.Setup(s => s.GetReservationByIdAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")))
                .ReturnsAsync(reservation)
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.GetReservationById(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")) as OkObjectResult;
            #endregion

            #region Verify
            MockService.Verify(s => s.GetReservationByIdAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")), Times.Once);
            #endregion

            #region Assert     
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            Assert.NotNull(((ReservationDto)result.Value));
            Assert.Equal(reservationDto.Id, ((ReservationDto)result.Value).Id);
            Assert.Equal(reservationDto.Name, ((ReservationDto)result.Value).Name);
            Assert.Equal(reservationDto.StartDate, ((ReservationDto)result.Value).StartDate);
            Assert.Equal(reservationDto.EndDate, ((ReservationDto)result.Value).EndDate);
            #endregion
        }

        [Fact]
        public async Task Will_Return_Not_Found_Reservation()
        {
            #region Variables        
            #endregion

            #region Setup       
            MockService.Setup(s => s.GetReservationByIdAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")))
                .ReturnsAsync((Reservation)null)
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.GetReservationById(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")) as OkObjectResult;
            #endregion

            #region Verify
            MockService.Verify(s => s.GetReservationByIdAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")), Times.Once);
            #endregion

            #region Assert     
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            Assert.Equal("No reservation found.", result.Value);
            #endregion
        }
    }
}
