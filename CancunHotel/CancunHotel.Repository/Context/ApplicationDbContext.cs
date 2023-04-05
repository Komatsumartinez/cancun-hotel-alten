using CancunHotel.Repository.Contracts;
using CancunHotel.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CancunHotel.Repository.Context
{
    /// <summary>
    /// Implementation of the DbContext needed to interact with the DB.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        }

        public DbSet<Reservation> Reservations { get; set; }

    }
}
