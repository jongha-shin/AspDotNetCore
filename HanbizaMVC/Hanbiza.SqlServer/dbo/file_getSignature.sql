/*
SELECT a.* 
FROM 문서함 a 
WHERE a.BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND a.StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
	AND a.Signature = 'Y'
*/
CREATE PROCEDURE [dbo].[file_getSignature]
	@BizNum VARCHAR(10),
	@StaffID int
AS
	SELECT SeqID, FilePath, FileName, FileBLOB
	FROM 문서함
	WHERE BizNum = @BizNum AND StaffID = @StaffID AND Signature = 'Y'
RETURN 0
