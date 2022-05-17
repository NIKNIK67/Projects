using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace HospitalApp
{
    internal class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Prescription> Prescriptions { get; set; } = null!;
        internal EFContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer($@"Data Source=LAPTOP-J0HNFQ96\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message),Microsoft.Extensions.Logging.LogLevel.Error);
           
        }
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }
    }
}
