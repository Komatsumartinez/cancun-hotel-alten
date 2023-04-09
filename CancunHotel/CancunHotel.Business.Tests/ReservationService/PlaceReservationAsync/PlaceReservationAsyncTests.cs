using CancunHotel.Repository.Models;
using CancunHotels.Business.Tests.ReservationServiceTests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Moq;

namespace CancunHotel.Business.Tests.ReservationService.PlaceReservationAsync
{
    public class PlaceReservationAsyncTests : ReservationServiceTests
    {
        [Fact]
        public async Task Will_Return_Exception_Invalid_Dates()
        {
            #region Variables
            var reservation = new Reservation
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 10)
            };
            #endregion

            #region Setup
            #endregion

            #region Call
            #endregion

            #region Verify
            #endregion

            #region Assert
            await Assert.ThrowsAsync<ArgumentException>(() => Service.PlaceReservationAsync(reservation));
            #endregion
        }

        [Fact]
        public async Task Will_Return_Reservation_Added()
        {
            #region Variables
            var reservation = new Reservation
            {
                Id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                Name = "John Doe",
                StartDate = new DateTime(2023, 4, 10),
                EndDate = new DateTime(2023, 4, 13)
            };
            #endregion

            #region Setup
            MockReservationRepository.Setup(s => s.AddReservationAsync(reservation))
                .ReturnsAsync(reservation)
                .Verifiable();
            #endregion

            #region Call
            var results = await Service.PlaceReservationAsync(reservation);
            #endregion

            #region Verify
            MockReservationRepository.Verify(v => v.AddReservationAsync(reservation), Times.Once);
            #endregion

            #region Assert
            Assert.IsType<Reservation>(results);
            Assert.Equal(new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"), results.Id);
            Assert.Equal("John Doe", results.Name);
            Assert.Equal(new DateTime(2023, 4, 10), results.StartDate);
            Assert.Equal(new DateTime(2023, 4, 13), results.EndDate);
            #endregion
        }
    }
}
