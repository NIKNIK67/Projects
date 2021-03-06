using Microsoft.EntityFrameworkCore;

namespace tictactoe
{
    public class EFContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public EFContext()
        {
           
                Database.EnsureCreated();
            
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalD;Initial Catalog=master;Integrated Security=True;Connect Timeout=1;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}