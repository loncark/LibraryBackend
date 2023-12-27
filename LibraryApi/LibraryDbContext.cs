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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var client = new SecretClient(vaultUri: new Uri("https://algebravault.vault.azure.net/"), credential: new VisualStudioCredential());
            var secret = client.GetSecret("DatabaseConnectionString");

            // Configure your SQL Server connection string here
            optionsBuilder.UseSqlServer(secret.Value.Value);
            
            // Connection String: Data Source=KRISTINA-ASUS-2;Initial Catalog=LibraryDb;Integrated Security=True;TrustServerCertificate=True
        }
    }

}
