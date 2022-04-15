using Microsoft.EntityFrameworkCore;

namespace tictactoe
{
    public class EFContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public EFContext()
        {
            try
            {
                Database.EnsureCreated();
            }
            catch { }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalD;Initial Catalog=master;Integrated Security=True;Connect Timeout=1;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}