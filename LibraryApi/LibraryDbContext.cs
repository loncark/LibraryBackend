namespace LibraryApi
{
    using Azure.Identity;
    using Azure.Security.KeyVault.Secrets;
    using LibraryApi.Domain;
    using Microsoft.EntityFrameworkCore;

    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            KeyVaultHandler vaultHandler = new KeyVaultHandler();
            optionsBuilder.UseSqlServer(vaultHandler.GetSecret("DatabaseConnectionString"));
            
            // Connection String: Data Source=KRISTINA-ASUS-2;Initial Catalog=LibraryDb;Integrated Security=True;TrustServerCertificate=True
        }
    }

}
