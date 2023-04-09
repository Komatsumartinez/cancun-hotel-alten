using CancunHotel.Repository.Models;
using CancunHotels.Business.Tests.ReservationServiceTests;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CancunHotel.Business.Tests.ReservationService.GetAvailableReservations
{
    public class GetAvailableReservationsTests : ReservationServiceTests
    {
        [Fact]
        public void Will_Return_Room_Available()
        {
            #region Variables
            var message = "The room is available for the requested dates.";
            var StartDate = new DateTime(2023, 4, 10);
            var EndDate = new DateTime(2023, 4, 10);
            var reservation = new List<Reservation>{};
            #endregion

            #region Setup
            MockReservationRepository.Setup(s => s.GetOverlappingReservations(StartDate, EndDate))
             .Returns(reservation)
             .Verifiable();
            #endregion

            #region Call
            var result = Service.GetAvailableReservations(StartDate, EndDate);
            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.GetOverlappingReservations(StartDate, EndDate), Times.Once);
            #endregion

            #region Assert
            Assert.Equal(message, result);
            #endregion
        }

        [Fact]
        public void Will_Return_Exception_Not_Availability()
        {
            #region Variables
            var StartDate = new DateTime(2023, 4, 10);
            var EndDate = new DateTime(2023, 4, 10);
            var reservation = new List<Reservation>{
                new Reservation
                {
                    Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                    Name = "John Doe",
                    StartDate = new DateTime(2023, 4, 10),
                    EndDate = new DateTime(2023, 4, 10)
                }
            };
            #endregion

            #region Setup
            MockReservationRepository.Setup(s => s.GetOverlappingReservations(StartDate, EndDate))
                 .Returns(reservation)
                 .Verifiable();
            #endregion

            #region Call  and Assert
            Assert.Throws<InvalidOperationException>(() => Service.GetAvailableReservations(StartDate, EndDate));
            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.GetOverlappingReservations(StartDate, EndDate), Times.Once);
            #endregion
        }
    }
}