using Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database
{
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Table for all users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        /// <summary>
        /// For setting special model-relations
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
