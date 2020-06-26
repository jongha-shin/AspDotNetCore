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
	--SET @rs_StaffID = CONCAT('@', 'StaffID', @i)
	--SET @rs_Sign = CONCAT('@', 'Sign', @i)

	IF(@i = 1) SET @processPoint = 'Y'
	ELSE SET @processPoint = 'N'

	IF @i = 1 
	BEGIN
		INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
		VALUES(@Dname, @BizNum, @VacID, @StaffID1, @Sign1, @i, @processPoint, '', 'N', GETDATE())
		INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
		VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @StaffID1)
	END
	
	IF @i = 2
	BEGIN
		INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
		VALUES(@Dname, @BizNum, @VacID, @StaffID2, @Sign2, @i, @processPoint, '', 'N', GETDATE())
		INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
		VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @StaffID2)
	END

	IF @i = 3
	BEGIN
		INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
		VALUES(@Dname, @BizNum, @VacID, @StaffID3, @Sign3, @i, @processPoint, '', 'N', GETDATE())
		INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
		VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @StaffID3)
	END

	IF @i = 4
	BEGIN
		INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
		VALUES(@Dname, @BizNum, @VacID, @StaffID4, @Sign4, @i, @processPoint, '', 'N', GETDATE()) 
		INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
		VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @StaffID4)
	END

	IF @i = 5
	BEGIN
		INSERT INTO Vacation_Approve(Dname, BizNum, VacID, approveID, approveName, DeepNum, processPoint, RereaSon, AResult, Regdate)
		VALUES(@Dname, @BizNum, @VacID, @StaffID5, @Sign5, @i, @processPoint, '', 'N', GETDATE())
		INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
		VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @StaffID5)
	END

	SET @i = @i +1
END

