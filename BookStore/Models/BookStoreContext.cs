using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BookStore.Models.Mapping;

namespace BookStore.Models
{
    public partial class BookStoreContext : DbContext
    {
        static BookStoreContext()
        {
            Database.SetInitializer<BookStoreContext>(null);
        }

        public BookStoreContext()
            : base("Name=BookStoreContext")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
