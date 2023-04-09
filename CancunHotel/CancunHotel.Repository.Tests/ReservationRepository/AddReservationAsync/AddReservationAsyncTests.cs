using CancunHotel.Repository.Models;
using SelazarUserDB.Repository.Tests.ReservationRepositoryTest;
using System;
using Xunit;

namespace CancunHotel.Repository.Tests.ReservationRepository.AddReservationAsync
{
    public class AddReservationAsyncTests : ReservationRepositoryTests
    {
        [Fact]
        public async void Will_Return_Reservation_Added()
        {
            #region Variables
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
            };
            #endregion

            #region Setup           
            #endregion

            #region Call
            var result = await ReservationRepo.AddReservationAsync(reservation);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.IsType<Reservation>(result);
            Assert.IsType<Guid>(result.Id);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(DateTime.Today.AddDays(1), result.StartDate);
            Assert.Equal(DateTime.Today.AddDays(3), result.EndDate);
            #endregion             
        }
    }
}