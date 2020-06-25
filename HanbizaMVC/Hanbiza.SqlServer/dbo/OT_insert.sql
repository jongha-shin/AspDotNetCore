
CREATE PROCEDURE [dbo].[OT_insert]
	@Dname varchar(50),
	@BizNum varchar(10),
	@StaffID int,
	@StaffName varchar(50),
	@Gubun char(1),
	@Snal datetime,
	@Enal datetime,
	@Reason nvarchar(100)

AS
	INSERT INTO AddTime_List(DName, BizNum, StaffID, STaffName, Gubun, SNal, ENal, Reason, HbzMsend, HbzYYMMDD, Regdate)
	VALUES(@Dname, @BizNum, @StaffID, @StaffName, @Gubun, @Snal, @Enal, @Reason, '', NULL, GETDATE())