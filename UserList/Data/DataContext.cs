using Microsoft.EntityFrameworkCore;
using UserList.Models.Location;
using UserList.Models.Phone;
using UserList.Models.User;

namespace UserList.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<ConnectedUser> ConnectedUsers { get; set; }

        #endregion

        #region Location

        public DbSet<City> Cities { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>()
                .HasMany(u => u.Phones)
                .WithOne(p => p.User);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ConnectedUsers)
                .WithOne(cu => cu.User)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
