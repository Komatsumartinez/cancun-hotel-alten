using CancunHotel.Repository.Models;
using SelazarUserDB.Repository.Tests.ReservationRepositoryTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CancunHotel.Repository.Tests.ReservationRepository.GetOverlappingReservations
{
    public class GetOverlappingReservationsTests : ReservationRepositoryTests
    {
        [Fact]
        public void Will_Found_Reservation_By_Id()
        {
            #region Variables
            var StartDate = DateTime.Today.AddDays(1);
            var EndDate = DateTime.Today.AddDays(3);
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
            var result = ReservationRepo.GetOverlappingReservations(StartDate, EndDate);
            #endregion

            #region Verify
            //no verify calls needed 
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(new[] { new Guid("40031589-fe6e-4519-9b83-15b91974349d") }, result.Select(r => r.Id));
            Assert.Equal(new[] { "John Doe" }, result.Select(r => r.Name));
            Assert.Equal(new[] { DateTime.Today.AddDays(1) }, result.Select(r => r.StartDate));
            Assert.Equal(new[] { DateTime.Today.AddDays(3) }, result.Select(r => r.EndDate));
            #endregion             
        }

        [Fact]
        public void Will_Not_Found_Reservation_By_Id()
        {
            #region Variables
            var StartDate = DateTime.Today.AddDays(1);
            var EndDate = DateTime.Today.AddDays(3);
            #endregion

            #region Setup            
            #endregion

            #region Call
            var result = ReservationRepo.GetOverlappingReservations(StartDate, EndDate);
            #endregion

            #region Verify
            //no verify calls needed 
            #endregion

            #region Assert
            Assert.Empty(result);
            #endregion
        }

    }
}