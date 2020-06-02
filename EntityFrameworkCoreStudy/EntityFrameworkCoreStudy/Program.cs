using EntityFrameworkCoreStudy.Data;
using EntityFrameworkCoreStudy.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EntityFrameworkCoreStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new WorkDbContext();
            // Linq 분류(2가지)
            // 1. 쿼리 구문
            // from user in Users
            // where ....
            // select new ...

            // 2. 메서드 구문
            // db.Users.Where().ToList();


            // 1. SELECT 쿼리
            // 1) DbSet<Login_infors> selectList = db.Login_infor;
            // 2) List<Login_infors> selectList = db.Login_infor.ToList().Where().OrderBy();
            // 3) IEnumerable<Login_infors> selectList = db.Login_infor.AsEnumerable();
            // 4) IQueryable<Login_infors> selectList = from Login_infors in db.Login_infor select Login_infors;    // linq to Sql

            // IEnumerable vs IQueryable
            // Extension Query => 작성 가능
            // 1. IEnumerable => 쿼리 => 데이터 => Client com => Slow 
            // 2. IQueryable => 쿼리 => 데이터 => Server (메모리상에 있는 데이터를 가공) => Fast

            var selectList = db.Login_infor.ToList();
            foreach (var item in selectList)
            {
               Console.WriteLine(item.StaffName);
            }

            var user = new Login_infors{SeqId=4, StaffName="장길동"};
            
            // 2. INSERT 쿼리
            db.Login_infor.Add(user);
            db.SaveChanges(); // = insert, update, delete 실행 후 db에 반영 // commit

            // 3. UPDATE 쿼리
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            // 4. DELETE 쿼리
            // DELETE FROM USer WHERE UserId = 2;
            var user1 = new Login_infors { SeqId = 2};
            db.Login_infor.Remove(user1);
            db.SaveChanges();

            // # .WHere(), OrderBy()
            // 1. .Where() -> 조건절 - > 리스트가 가능
            // SELECT * FROM Users WHERE UserId = 1
            var list = db.Login_infor.ToList();

            var list1 = db.Login_infor.Where(i => i.CompanyName == "동천(주)");
            Console.WriteLine("시작");
            foreach (var user2 in list1)
            {
                Console.WriteLine($"{user2.SeqId}.{user2.StaffName}");
            }

            // # 특정 데이터 1개 가져오기
            // .First(), .FirstOrDefault(), .Single(), SingleOrDefault()
            var user3 = db.Login_infor.SingleOrDefault(u => u.CompanyName == "동천(주)");
            Console.WriteLine(user3.SeqId);
            // SingleOrDefault() vs FirstOrDefault()
            // SingleOrDefault() 권장.

            // # OrderBy()  1 2 3 4 5 6
            // # OrderByDecending() 6 5 4 3 2 1
            var list2 = db.Login_infor.OrderBy(i => i.StaffName).ToList();
            

        }

    }

}
