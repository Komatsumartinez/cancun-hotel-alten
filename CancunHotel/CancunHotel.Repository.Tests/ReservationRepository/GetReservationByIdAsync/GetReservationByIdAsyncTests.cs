using CancunHotel.Repository.Models;
using SelazarUserDB.Repository.Tests.ReservationRepositoryTest;
using System;
using Xunit;

namespace CancunHotel.Repository.Tests.ReservationRepository.GetReservationByIdAsync
{
    public class GetReservationByIdAsyncTests : ReservationRepositoryTests
    {
        [Fact]
        public async void Will_Found_Reservation_By_Id()
        {
            #region Variables
            var reservationId = new Guid("40031589-fe6e-4519-9b83-15b91974349d");
            var reservation = new Reservation
            {
                Id = new Guid("40031589-fe6e-4519-9b83-15b91974349d"),
                Name = "John Doe",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
            };
            #endregion

            #region Setup            
            HotelDBContext.AddRange(reservation);
            HotelDBContext.SaveChanges();
            #endregion

            #region Call
            var result = await ReservationRepo.GetReservationByIdAsync(reservationId);
            #endregion

            #region Verify
            //no verify calls needed 
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(new Guid("40031589-fe6e-4519-9b83-15b91974349d"), result.Id);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(DateTime.Today.AddDays(1), result.StartDate);
            Assert.Equal(DateTime.Today.AddDays(3), result.EndDate);
            #endregion             
        }

        [Fact]
        public async void Will_Not_Found_Reservation_By_Id()
        {
            #region Variables
            var reservationId = new Guid("40031589-fe6e-4519-9b83-15b91974349d");
            #endregion

            #region Setup            
            #endregion

            #region Call
            var result = await ReservationRepo.GetReservationByIdAsync(reservationId);
            #endregion

            #region Verify
            //no verify calls needed 
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }

    }
}
