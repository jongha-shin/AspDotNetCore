/*
1.
승인 대기중인 레코드
SELECT * 
FROM Vacation_Approve 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND approveID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
ORDER BY Regdate DESC

휴가 정보
SELECT * 
FROM Vacation_List 
WHERE SEQID=" &VacID

신청자 정보
SELECT StaffName 
FROM Login_infor 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&StaffID

*/

CREATE PROCEDURE [dbo].[approvalList]
	@BizNum varchar(10),
	@StaffID int
AS
	SELECT a.VacID, a.AResult, a.processPoint, a.RereaSon, b.StaffID, b.SNal, b.Enal, b.Vicname, b.UseTime, b.VicReaSon, c.StaffName
	FROM Vacation_Approve a
	JOIN Vacation_List b
		ON a.VacID = b.SEQID
	JOIN Login_infor c
		ON b.StaffID = c.StaffID
	WHERE a.BizNum = @BizNum AND a.approveID = @StaffID
	ORDER BY a.Regdate DESC

