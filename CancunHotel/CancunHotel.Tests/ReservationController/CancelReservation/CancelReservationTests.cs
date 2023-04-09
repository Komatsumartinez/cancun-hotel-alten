using CancunHotel.Business.Models;
using CancunHotel.Repository.Models;
using CancunHotel.Tests.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
namespace CancunHotel.Tests.ReservationController.CancelReservation
{
    public class CancelReservationTests : ReservationControllerTests
    {
        [Fact]
        public async Task Will_Return_Canceled_Reservation()
        {
            #region Variables        

            #endregion

            #region Setup       
            MockService.Setup(s => s.CancelReservationAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")))
                .ReturnsAsync(true)
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.CancelReservation(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")) as OkObjectResult;
            #endregion

            #region Verify
            MockService.Verify(s => s.CancelReservationAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")), Times.Once);
            #endregion

            #region Assert     
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            Assert.Equal("Reservation cancelled successfully.", result.Value);
            #endregion
        }

        [Fact]
        public async Task Will_Return_Not_Found_Reservation()
        {
            #region Variables        
            var exceptionMessage = "Reservation not found.";
            #endregion

            #region Setup       
            MockService.Setup(s => s.CancelReservationAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")))
                .ThrowsAsync( new ArgumentException(exceptionMessage))
                .Verifiable();
            #endregion

            #region Call                        
            var result = await Controller.CancelReservation(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"));
            #endregion

            #region Verify
            MockService.Verify(s => s.CancelReservationAsync(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6")), Times.Once);
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(exceptionMessage, conflictResult.Value);
            #endregion
        }
    }
}
