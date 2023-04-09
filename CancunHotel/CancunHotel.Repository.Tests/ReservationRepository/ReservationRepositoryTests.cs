using CancunHotel.Repository;
using CancunHotel.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace CancunHotels.Repository.Tests.ReservationRepositoryTest
{
    public class ReservationRepositoryTests
    {
        protected DbContextOptions<HotelDBContext> Options;
        protected HotelDBContext HotelDBContext;
        protected ReservationRepository ReservationRepo;
        public ReservationRepositoryTests()
        {
            Options = new DbContextOptionsBuilder<HotelDBContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            HotelDBContext = new HotelDBContext(Options);
            ReservationRepo = new ReservationRepository(HotelDBContext);
        }
    }
}
