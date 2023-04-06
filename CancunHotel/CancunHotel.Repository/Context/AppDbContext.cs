using CancunHotel.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CancunHotel.Repository.Context
{
    /// <summary>
    /// Implementation of the DbContext needed to interact with the DB.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions<DbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Your Connection String");
        }
    }
}
