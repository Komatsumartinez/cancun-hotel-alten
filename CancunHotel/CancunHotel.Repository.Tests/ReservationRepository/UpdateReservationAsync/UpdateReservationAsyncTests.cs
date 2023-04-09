using CancunHotel.Repository.Models;
using SelazarUserDB.Repository.Tests.ReservationRepositoryTest;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CancunHotel.Repository.Tests.ReservationRepository.UpdateReservationAsync
{
    public class UpdateReservationAsyncTests : ReservationRepositoryTests
    {
        [Fact]
        public async void Will_Update_Reservation()
        {
            #region Variables
            var reservation = new Reservation
            {
                Id = new Guid("40031589-fe6e-4519-9b83-15b91974349d"),
                Name = "John Doe",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
            };
            #endregion

            #region Setup            
            HotelDBContext.Reservations.Add(reservation);
            await HotelDBContext.SaveChangesAsync();
            #endregion

            #region Call
            var result = await ReservationRepo.UpdateReservationAsync(reservation);
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
        
    }
}
