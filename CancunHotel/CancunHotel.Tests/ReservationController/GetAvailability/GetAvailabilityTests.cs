using CancunHotel.Business.Models;
using CancunHotel.Repository.Models;
using CancunHotel.Tests.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CancunHotel.Tests.ReservationController.GetAvailability
{
    public class GetAvailabilityTests : ReservationControllerTests
    {
        [Fact]
        public void Will_Return_Availability_Reservation()
        {
            #region Variables    
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var message = "The room is available for the requested dates.";
            DateTime StartDate = new DateTime(2023, 4, 11);
            DateTime EndDate = new DateTime(2023, 4, 13);

            #endregion

            #region Setup       
            MockService.Setup(s => s.GetAvailableReservations(StartDate, EndDate))
                .Returns(message)
                .Verifiable();
            #endregion

            #region Call                        
            var result = Controller.GetAvailability(StartDate, EndDate) as OkObjectResult;
            #endregion

            #region Verify
            MockService.Verify(s => s.GetAvailableReservations(StartDate, EndDate), Times.Once);
            #endregion

            #region Assert     
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result.Value);
            Assert.Equal(message, result.Value);
            #endregion
        }

        [Fact]
        public void Will_Return_Bad_Request_Parameters_Required()
        {
            #region Variables        
            var message = "StartDate and EndDate parameters are required.";
            var EndDate = new DateTime(2023, 4, 10);
            #endregion

            #region Setup       
            #endregion

            #region Call                        
            var result = Controller.GetAvailability(startDate: null, endDate: EndDate);
            #endregion

            #region Verify
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(message, conflictResult.Value);
            #endregion
        }

        [Fact]
        public void Will_Return_Bad_Request_EndDate_Greater_Than_StartDate()
        {
            #region Variables        
            var message = "EndDate must be greater than StartDate.";
            var StarDate = new DateTime(2023, 4, 10);
            var EndDate = new DateTime(2023, 4, 10);
            #endregion

            #region Setup       
            #endregion

            #region Call                        
            var result = Controller.GetAvailability(StarDate, EndDate);
            #endregion

            #region Verify
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(message, conflictResult.Value);
            #endregion
        }

        [Fact]
        public void Will_Return_Already_Requested_Dates_Reserved()
        {
            #region Variables        
            var exceptionMessage = "There is already a reservation for the requested dates.";
            var StarDate = new DateTime(2023, 4, 10);
            var EndDate = new DateTime(2023, 4, 12);
            #endregion

            #region Setup       
            MockService.Setup(s => s.GetAvailableReservations(StarDate, EndDate))
                .Throws(new InvalidOperationException(exceptionMessage))
                .Verifiable();
            #endregion

            #region Call                        
            var result = Controller.GetAvailability(StarDate, EndDate);
            #endregion

            #region Verify
            MockService.Verify(s => s.GetAvailableReservations(StarDate, EndDate), Times.Once);
            #endregion

            #region Assert     
            var conflictResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(exceptionMessage, conflictResult.Value);
            #endregion
        }
    }
}
