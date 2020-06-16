/*
SELECT * 
FROM Vacation_List 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
ORDER BY SNal DESC, SEQID DESC
*/

CREATE PROCEDURE [dbo].[vacation_getVacation]
	@BizNum varchar(10),
	@StaffID int
AS
	SELECT SEQID, Vicname, UseTime, SNal, Enal, ProCDeep, AllProCess, VicReaSon, Regdate
	FROM Vacation_List
	WHERE BizNum = @BizNum AND StaffID = @StaffID
