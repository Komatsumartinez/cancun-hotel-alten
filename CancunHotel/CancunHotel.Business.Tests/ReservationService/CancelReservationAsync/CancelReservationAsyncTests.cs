using CancunHotel.Repository.Models;
using CancunHotels.Business.Tests.ReservationServiceTests;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CancunHotel.Business.Tests.ReservationService.CancelReservationAsync
{
    public class CancelReservationAsyncTests : ReservationServiceTests
    {
        [Fact]
        public async Task Will_Return_True_Reservation_Canceled()
        {
            #region Variables
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var reservation = new Reservation
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 10)
            };
            #endregion

            #region Setup
            MockReservationRepository.Setup(s => s.GetReservationByIdAsync(reservationId))
                 .ReturnsAsync(reservation)
                 .Verifiable();

            MockReservationRepository.Setup(s => s.DeleteReservationAsync(reservationId))
                 .ReturnsAsync(true)
                 .Verifiable();
            #endregion

            #region Call
            var result = await Service.CancelReservationAsync(reservationId);
            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.GetReservationByIdAsync(reservationId), Times.Once);
            MockReservationRepository.Verify(v => v.DeleteReservationAsync(reservationId), Times.Once);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }

        [Fact]
        public async Task Will_Return_Exception_Not_Cancel_Reservation()
        {
            #region Variables
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var reservation = new Reservation
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 10)
            };
            #endregion

            #region Setup
            MockReservationRepository.Setup(s => s.GetReservationByIdAsync(reservationId))
                 .ReturnsAsync((Reservation)null)
                 .Verifiable();

            MockReservationRepository.Setup(s => s.DeleteReservationAsync(reservationId))
                 .ReturnsAsync(false)
                 .Verifiable();
            #endregion

            #region Call  and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => Service.CancelReservationAsync(reservationId));

            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.GetReservationByIdAsync(reservationId), Times.Once);
            MockReservationRepository.Verify(v => v.DeleteReservationAsync(reservationId), Times.Never);
            #endregion
        }
    }
}