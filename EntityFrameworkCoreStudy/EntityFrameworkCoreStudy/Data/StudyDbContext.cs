using EntityFrameworkCoreStudy.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreStudy.Data
{
    public class WorkDbContext : DbContext
    {
        public DbSet<Login_infors> Login_infor { get; set; }
        public DbSet<출퇴근기록_집계표s> 출퇴근기록_집계표 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0NJKR1H\SQLEXPRESS;Initial Catalog=***;User ID=***;Password=*****;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
