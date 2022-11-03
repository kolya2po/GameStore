using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.DAL
{
    public class GameStoreDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public GameStoreDbContext()
        {

        }
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options) {}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=KOLYAPC;Database=gamestoredb;Trusted_Connection=true;");
        //}

        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ContactInformation> ContactsInformation { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameStoreDbContext).Assembly);
        }
    }
}
