/*
SELECT a.*,b.buseo 
FROM Vacation_Approve a, Login_infor b 
WHERE a.BizNum=b.BizNum 
	AND a.approveID=b.StaffID 
	AND a.BizNum='"&BizNum&"' 
	AND a.VacID="&VacID&" 
ORDER BY a.DeepNum AS
*/

CREATE PROCEDURE [dbo].[vacation_getDetail]
	@VacID int
AS
	SELECT AResult, processPoint, DeepNum, approveName, RereaSon, b.Buseo, c.VicReaSon
	FROM Vacation_Approve a 
		JOIN Login_infor b 
		ON a.BizNum = b.BizNum AND a.approveID = b.StaffID
		JOIN Vacation_List c
		ON a.VacID = c.SEQID
	WHERE a.VacID = @VacID
	ORDER BY a.DeepNum
