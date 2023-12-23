namespace LibraryApi
{
    using LibraryApi.Domain;
    using Microsoft.EntityFrameworkCore;

    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your SQL Server connection string here
            optionsBuilder.UseSqlServer("Data Source=KRISTINA-ASUS-2;Initial Catalog=LibraryDb;Integrated Security=True;TrustServerCertificate=True");
        }
    }

}
