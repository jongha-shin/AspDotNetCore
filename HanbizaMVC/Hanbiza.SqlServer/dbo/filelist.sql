/*
SELECT *,DATEDIFF(hour, getdate(), Regdate) as dd 
FROM 문서함 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
	AND FileBLOB IS NOT NULL AND Signature='N' 
	AND (Gubun='근로계약서' 
	AND DATEDIFF(hour, getdate(), Regdate) >= -24) 
ORDER BY SeqID DESC
*/

CREATE PROCEDURE [dbo].[filelist]
	@BizNum VARCHAR(10),
	@StaffID int

AS
	SELECT *, DATEDIFF(HOUR, GETDATE(), Regdate) as dd
	FROM 문서함
	WHERE BizNum = @BizNum AND StaffID = @StaffID AND FileBLOB IS NOT NULL AND Signature = 'N' 
	AND (Gubun = '근로계약서' AND DATEDIFF(HOUR, GETDATE(), Regdate) >= -24)
	ORDER BY SeqID DESC

