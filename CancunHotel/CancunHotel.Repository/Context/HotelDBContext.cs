using CancunHotel.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CancunHotel.Repository.Context
{
    public class HotelDBContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }

        public HotelDBContext(DbContextOptions<HotelDBContext> options)
            : base(options)
        {
        }
    }
}
