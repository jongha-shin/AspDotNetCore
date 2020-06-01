using EntityFrameworkCoreStudy.Data;
using EntityFrameworkCoreStudy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCoreStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new WorkDbContext();

            // 1. SELECT 쿼리

            // 1) DbSet<Login_infors> selectList = db.Login_infor;
            // 2) List<Login_infors> selectList = db.Login_infor.ToList().Where().OrderBy();
            // 3) IEnumerable<Login_infors> selectList = db.Login_infor.AsEnumerable();
            // 4) IQueryable<Login_infors> selectList = from Login_infors in db.Login_infor select Login_infors;    // linq to Sql

            // IEnumerable vs IQueryable
            // Extension Query => 작성 가능
            // 1. IEnumerable => 쿼리 => 데이터 => Client com => Slow 
            // 2. IQueryable => 쿼리 => 데이터 => Server (메모리상에 있는 데이터를 가공) => Fast

             List<Login_infors> selectList = db.Login_infor.ToList();
             foreach (var item in selectList)
             {
                Console.WriteLine(item.StaffName);
             }

            // 2. INSERT 쿼리
            // db.Users.Add(User);
            // db.SaveChanges(); = insert, update, delete 실행 후 db에 반영 // commit

            // 3. UPDATE 쿼리
            // var user = new User{UserId=1, UserName="장길동"};
            // db.Entry(user).State = EntityState.Modified;
            // db.SaveChanges();

            // 4. DELETE 쿼리
            // DELETE FROM USer WHERE UserId = 2;
            // var user = new User { UserId = 2};
            // db.Users.Remove(user);
            // db.SaveChanges();
        }

    }

}
