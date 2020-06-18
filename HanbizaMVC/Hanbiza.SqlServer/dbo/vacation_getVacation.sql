/*
SELECT * 
FROM Vacation_List 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
ORDER BY SNal DESC, SEQID DESC
*/

/*
WITH summary AS (
	SELECT a.SEQID, Vicname, UseTime, SNal, Enal, ProCDeep, AllProCess, VicReaSon, a.Regdate, b.DeepNum, b.processPoint, b.RereaSon, b.AResult, 
			ROW_NUMBER() OVER (PARTITION BY a.SEQID ORDER BY a.SEQID, b.AResult, b.DeepNum DESC) as rk
	FROM Vacation_List a 
	JOIN Vacation_Approve b ON a.SEQID = b.VacID
	WHERE a.BizNum = '1268122794 ' AND StaffID = 31559
	)
SELECT s.*
FROM summary s
WHERE s.rk = 1
ORDER BY Regdate desc
*/

CREATE PROCEDURE [dbo].[vacation_getVacation]
	@BizNum varchar(10),
	@StaffID int
AS
/*
	SELECT SEQID, Vicname, UseTime, SNal, Enal, ProCDeep, AllProCess, VicReaSon, Regdate
	FROM Vacation_List
	
	WHERE BizNum = @BizNum AND StaffID = @StaffID
	ORDER BY SNal DESC, SEQID DESC
*/
WITH summary AS (
	SELECT a.SEQID, Vicname, UseTime, SNal, Enal, ProCDeep, AllProCess, VicReaSon, a.Regdate, b.DeepNum,
			ROW_NUMBER() OVER (PARTITION BY a.SEQID ORDER BY a.SEQID, b.AResult, b.DeepNum DESC) as rk
	FROM Vacation_List a 
	JOIN Vacation_Approve b ON a.SEQID = b.VacID
	WHERE a.BizNum = @BizNum AND StaffID = @StaffID
	)
SELECT s.*
FROM summary s
WHERE s.rk = 1
ORDER BY Regdate desc


	
