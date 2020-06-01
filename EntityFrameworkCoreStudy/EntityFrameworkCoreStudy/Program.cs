using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkCoreStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new WorkDbContext();
            var userList = db.Login_infor;
            foreach (var user in userList)
            {
                Console.WriteLine(user.SeqId + " / " +user.StaffId);
            }
        }

        public class WorkDbContext : DbContext
        {
            public DbSet<Login_infors> Login_infor { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                //base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GQCS1NV\SQLEXPRESS;Initial Catalog=hanbiza;User ID=sa;Password=*******;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
    }
   
    public class Login_infors
    {
        public int SeqId { get; set; }
        public String StaffId { get; set; }
    }
}
