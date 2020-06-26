CREATE PROCEDURE [dbo].[vacation_insertEachApprover]
	@StaffID1 int,
	@StaffID2 int,
	@StaffID3 int,
	@StaffID4 int,
	@StaffID5 int,
	@Sign1 varchar(50),
	@Sign2 varchar(50),
	@Sign3 varchar(50),
	@Sign4 varchar(50),
	@Sign5 varchar(50),
	@ProCDeep int,
	@Dname varchar(50),
	@BizNum char(10),
	@StaffName varchar(50)
AS

DECLARE @VacID int, @i int, @processPoint char(1), @rs_StaffID varchar(10), @rs_Sign varchar(50) 
SET @VacID = (SELECT MAX(SEQID) FROM Vacation_List)
SET @i = 1

WHILE @i <= @ProCDeep 
BEGIN
	SET @rs_StaffID = CONCAT('@', 'StaffID', @i)
	SET @rs_Sign = CONCAT('@', 'Sign', @i)

	IF(@i = 1) SET @processPoint = 'Y'
	ELSE SET @processPoint = 'N'

	EXEC('INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
	VALUES('+ @Dname+ ','+ @BizNum+ ','+ @VacID+ ', @StaffID'+@i+ ','+ @rs_Sign+ ','+ @i+ ','+ @processPoint+ ', ,'+ 'N, GETDATE())')

	EXEC('INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
	VALUES('+ @Dname + ',' + @BizNum + ',' + @StaffName + '님의 휴가신청이 있습니다, 휴가결재에서 결재바랍니다,'+ @StaffName + ',' + @VacID + ', GETDATE(), @StaffID'+@i+')')
	
	
	/*
	INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
	VALUES(@Dname, @BizNum, @VacID, @rs_StaffID, @rs_Sign, @i, @processPoint, '', 'N', GETDATE())

	INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
	VALUES(@Dname, @BizNum, @StaffName+'님의 휴가신청이 있습니다', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @rs_StaffID)
	*/
	SET @i = @i +1
END

