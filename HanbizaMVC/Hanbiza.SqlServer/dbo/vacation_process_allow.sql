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

UPDATE Vacation_Approve SET processPoint = 'N', AResult = 'Y' WHERE VacID = @VacID AND approveID = @approveID

	IF @ProCDeep = @DeepNum
	BEGIN
		UPDATE Vacation_List SET AllProCess = 'S' WHERE SEQID = @VacID
	END

UPDATE Vacation_Approve SET processPoint = 'Y' WHERE DeepNum = @DeepNum +1 AND VacID = @VacID
DELETE 공지사항 WHERE VacID = @VacID AND LoginID = @approveID