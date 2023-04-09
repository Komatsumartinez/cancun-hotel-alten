using CancunHotel.Repository.Models;
using CancunHotels.Business.Tests.ReservationServiceTests;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CancunHotel.Business.Tests.ReservationService.GetReservationByIdAsync
{
    public class GetReservationByIdAsyncTests : ReservationServiceTests
    {
        [Fact]
        public async Task Will_Return_Reservation_By_Id()
        {
            #region Variables
            var reservationId = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
            var reservation = new Reservation
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 13)
            };
            #endregion

            #region Setup
            MockReservationRepository.Setup(s => s.GetReservationByIdAsync(reservationId))
             .ReturnsAsync(reservation)
             .Verifiable();
            #endregion

            #region Call
            var results = await Service.GetReservationByIdAsync(reservationId);
            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.GetReservationByIdAsync(reservationId), Times.Once);
            #endregion

            #region Assert
            Assert.IsType<Reservation>(results);
            Assert.Equal(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), results.Id);
            Assert.Equal("John Doe", results.Name);
            Assert.Equal(new DateTime(2023, 4, 10), results.StartDate);
            Assert.Equal(new DateTime(2023, 4, 13), results.EndDate);
            #endregion
        }

        [Fact]
        public async Task Will_Return_Not_Found_Reservation()
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
            #endregion

            #region Call
            var results = await Service.GetReservationByIdAsync(reservationId);
            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.GetReservationByIdAsync(reservationId), Times.Once);
            #endregion

            #region Assert
            Assert.Null(results);
            #endregion
        }
    }
}