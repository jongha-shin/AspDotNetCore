using EntityFrameworkCoreStudy.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreStudy.Data
{
    public class WorkDbContext : DbContext
    {
        public DbSet<Login_infors> Login_infor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GQCS1NV\SQLEXPRESS;Initial Catalog=hanbiza;User ID=***;Password=*******;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
