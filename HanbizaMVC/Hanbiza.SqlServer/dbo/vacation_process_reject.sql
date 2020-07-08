CREATE PROCEDURE [dbo].[vacation_process_reject]
	@approveID int,
	@VacID int,
	@RereaSon nvarchar(100)
AS
DECLARE @ProCDeep int, @DeepNum int

-- 최종 단계
SET @ProCDeep = (SELECT ProCDeep FROM Vacation_List WHERE SEQID = @VacID)
-- 현재 단계
SET @DeepNum = (SELECT DeepNum FROM Vacation_Approve WHERE VacID = @VacID AND approveID = @approveID)

-- 결재자 본인 결재단계(processPoint) 완료 및 승인 완료(AResult: 'F'), 반려사유 처리	
UPDATE Vacation_Approve SET processPoint = 'N', RereaSon = @RereaSon, AResult = 'F' WHERE VacID = @VacID AND approveID = @approveID

-- 결재자 다음 단계 모두 완료 처리
UPDATE Vacation_Approve SET AResult = 'Y' WHERE DeepNum > @DeepNum AND VacID = @VacID

-- 휴가정보 최종 반려 처리
UPDATE Vacation_List SET AllProCess = 'F' WHERE SEQID = @VacID

-- 공지사항 통보내역 삭제
DELETE FROM 공지사항 WHERE VacID = @VacID

