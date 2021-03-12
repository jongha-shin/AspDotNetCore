using Microsoft.EntityFrameworkCore;

namespace HanbizaMVC.Models
{
    public partial class HanbizaContext : DbContext
    {
        public HanbizaContext()
        {
        }

        public HanbizaContext(DbContextOptions<HanbizaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddTimeList> AddTimeList { get; set; }
        public virtual DbSet<LoginInfor> LoginInfor { get; set; }
        public virtual DbSet<PayList> PayList { get; set; }
        public virtual DbSet<공지사항> 공지사항 { get; set; }
        public virtual DbSet<문서함> 문서함 { get; set; }
        public virtual DbSet<연차대장> 연차대장 { get; set; }
        public virtual DbSet<출퇴근기록> 출퇴근기록 { get; set; }
        public virtual DbSet<출퇴근기록집계표> 출퇴근기록집계표 { get; set; }
        public virtual DbSet<휴가대장> 휴가대장 { get; set; }
        public virtual DbSet<Vacation_Approve> Vacation_Approve { get; set; }
        public virtual DbSet<Vacation_List> Vacation_List { get; set; }
        public virtual DbSet<회사별메뉴> 회사별메뉴 { get; set; }
        public virtual DbSet<Login_Record> Login_Record { get; set; }
        public virtual DbSet<인사평가> 인사평가 { get; set; }
        public virtual DbSet<Etc_List> Etc_List { get; set; }
        public virtual DbSet<시설물> 시설물 { get; set; }
        public virtual DbSet<시설예약대장> 시설예약대장 { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddTimeList>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey("Seqid");

                entity.ToTable("AddTime_List");

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasColumnName("DName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Enal)
                    .HasColumnName("ENal")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gubun)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HbzMsend)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.HbzYymmdd)
                    .HasColumnName("HbzYYMMDD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.Snal)
                    .HasColumnName("SNal")
                    .HasColumnType("datetime");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.StaffName)
                    .HasColumnName("STaffName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<LoginInfor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Login_infor");

                entity.Property(e => e.Bbuseo)
                    .HasColumnName("BBuseo")
                    .HasMaxLength(50);

                entity.Property(e => e.Bigo).HasMaxLength(100);

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Buseo).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDay).HasColumnType("datetime");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasColumnName("loginID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mbuseo)
                    .HasColumnName("MBuseo")
                    .HasMaxLength(50);

                entity.Property(e => e.PassW).HasMaxLength(128);

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<PayList>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gubun)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.Slist)
                    .HasColumnName("SList")
                    .HasMaxLength(50);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.Svalue).HasColumnName("SValue");

                entity.Property(e => e.Yyyymm)
                    .IsRequired()
                    .HasColumnName("YYYYMM")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Vacation_Approve>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.SEQID);
                entity.Property(e => e.Dname);
                entity.Property(e => e.BizNum);
                entity.Property(e => e.VacID);
                entity.Property(e => e.approveID);
                entity.Property(e => e.approveName);
                entity.Property(e => e.DeepNum);
                entity.Property(e => e.processPoint);
                entity.Property(e => e.RereaSon);
                entity.Property(e => e.AResult);
                entity.Property(e => e.Regdate);
             

            });
            modelBuilder.Entity<Vacation_List>(entity =>
            {
                /* 
                SEQID, Dname, BizNum, StaffID, Vicname, UseTime, SNal, Enal, ProCDeep, AllProCess, VicReaSon, HbzMSend, HbzMYYMMDD, Regdate
                  DATETIME -> datetime
                 */
                entity.HasNoKey();

                entity.Property(e => e.SEQID);
                entity.Property(e => e.Dname);
                entity.Property(e => e.BizNum);
                entity.Property(e => e.StaffID);
                entity.Property(e => e.Vicname);
                entity.Property(e => e.UseTime);
                entity.Property(e => e.Snal).HasColumnName("SNal").HasColumnType("date");
                entity.Property(e => e.Enal).HasColumnType("date");
                entity.Property(e => e.ProCDeep);
                entity.Property(e => e.AllProCess);
                entity.Property(e => e.VicReason);
                entity.Property(e => e.HbzMSend);
                entity.Property(e => e.HbzMYYMMDD);
                entity.Property(e => e.Regdate).HasColumnType("datetime");
                
            });
            modelBuilder.Entity<공지사항>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BizNum)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.VacId).HasColumnName("VacID");

                entity.Property(e => e.내용).HasMaxLength(100);

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.제목).HasMaxLength(50);
            });
            modelBuilder.Entity<문서함>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EditYn)
                    .HasColumnName("Edit_yn")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FileBlob)
                    .HasColumnName("FileBLOB")
                    .HasColumnType("image");

                entity.Property(e => e.FileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fsize).HasColumnName("FSize");

                entity.Property(e => e.Ftype)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gubun)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.SeqId).HasColumnName("SeqID");

                entity.Property(e => e.SignDown)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Signature)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<연차대장>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.사용기간)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.연차구분)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.연차발생일).HasColumnType("date");

                entity.Property(e => e.입사일).HasColumnType("date");
            });
            modelBuilder.Entity<출퇴근기록>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.날짜).HasColumnType("date");

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.비고)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.일체크)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.출근)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.퇴근)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<출퇴근기록집계표>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("출퇴근기록_집계표");

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regdate)
                    .HasColumnName("regdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.무급휴가휴일).HasColumnName("무급휴가_휴일");

                entity.Property(e => e.시작일).HasColumnType("date");

                entity.Property(e => e.유급휴가휴일).HasColumnName("유급휴가_휴일");

                entity.Property(e => e.종료일).HasColumnType("date");
            });
            modelBuilder.Entity<휴가대장>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.BizNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Dname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regdate).HasColumnType("datetime");

                entity.Property(e => e.Seqid).HasColumnName("SEQID");

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.날짜).HasColumnType("date");

                entity.Property(e => e.등록인)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<회사별메뉴>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<Login_Record>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<인사평가>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<Etc_List>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<시설물>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<시설예약대장>(entity =>
            {
                entity.HasNoKey();
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
