CREATE PROCEDURE [dbo].[vacation_process_allow]
	@approveID int,
	@VacID int,
	@Gubun int
AS

-- 결재자 정보 결재 단계 및 휴가번호
DECLARE @ProCDeep int, @DeepNum int

-- 최종 단계
SET @ProCDeep = (SELECT ProCDeep FROM Vacation_List WHERE SEQID = @VacID)
-- 현재 단계
SET @DeepNum = (SELECT DeepNum FROM Vacation_Approve WHERE VacID = @VacID AND approveID = @approveID)
-- 결재자 본인 결재단계(processPoint) 완료 및 승인 완료(AResult) 처리
UPDATE Vacation_Approve SET processPoint = 'N', AResult = 'Y' WHERE VacID = @VacID AND approveID = @approveID
-- 결재자 단계 마지막 단계일 경우 휴가정보 최종 승인 처리
	IF @ProCDeep = @DeepNum
	BEGIN
		UPDATE Vacation_List SET AllProCess = 'S' WHERE SEQID = @VacID
	END
-- 결재자 다음 단계 승인 가능 처리
UPDATE Vacation_Approve SET processPoint = 'Y' WHERE DeepNum = @DeepNum +1 AND VacID = @VacID
-- 결재자 본인 공지사항 통보내역 삭제
DELETE 공지사항 WHERE VacID = @VacID AND LoginID = @approveID

-- 마지막 결재가 아닐 경우 다음 결재자 공지사항 등록
IF @ProCDeep <> @DeepNum
BEGIN
	--INSERT INTO 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
	--VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @StaffID1)
	DECLARE @Dname VARCHAR(50), @BizNum CHAR(10), @LoginID INT, @StaffName NVARCHAR(50)
	SET @Dname = (SELECT Dname FROM Vacation_Approve WHERE VacID = @VacID)
	SET @BizNum = (SELECT BizNum FROM Vacation_Approve WHERE VacID = @VacID)
	SET @StaffName = (SELECT StaffName FROM Login_infor WHERE StaffID = (SELECT StaffID FROM Vacation_List WHERE SEQID = @VacID))
	SET @LoginID = (SELECT approveID FROM Vacation_Approve WHERE DeepNum = @DeepNum +1 AND VacID = @VacID)

	INSERT 공지사항(Dname, BizNum, 제목, 내용, 등록인, VacID, Regdate, LoginID)
	VALUES(@Dname, @BizNum, @StaffName+'님의 휴가 신청이 있습니다.', '휴가결재에서 결재바랍니다', @StaffName, @VacID, GETDATE(), @LoginID)
END