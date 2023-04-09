using CancunHotel.Repository.Models;
using CancunHotels.Repository.Tests.ReservationRepositoryTest;
using System;
using Xunit;

namespace CancunHotel.Repository.Tests.ReservationRepository.DeleteReservationAsync
{
    public class DeleteReservationAsyncTests : ReservationRepositoryTests
    {
        [Fact]
        public async void Will_Return_True_Delete_Reservation()
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
            var result = await ReservationRepo.DeleteReservationAsync(reservationId);
            #endregion

            #region Verify
            #endregion

            #region Assert
            Assert.True(result);
            #endregion             
        }

        [Fact]
        public async void Will_Return_False_Not_Found_Reservation_By_Id()
        {
            #region Variables
            var reservationId = new Guid("40031589-fe6e-4519-9b83-15b91974349d");
            #endregion

            #region Setup            
            #endregion

            #region Call
            var result = await ReservationRepo.DeleteReservationAsync(reservationId);
            #endregion

            #region Verify
            //no verify calls needed 
            #endregion

            #region Assert
            Assert.False(result);
            #endregion        
        }
    }
}
