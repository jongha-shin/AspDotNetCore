/*
SELECT DISTINCT TOP 1  substring(convert(varchar(10),날짜,23),1,7) AS s_yymm 
FROM 출퇴근기록 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
ORDER BY substring(convert(varchar(10),날짜,23),1,7) DESC
*/

CREATE PROCEDURE [dbo].[lastMonth]
	@BizNum VARCHAR(10),
	@StaffID int
AS
	SELECT DISTINCT substring(convert(varchar(10),날짜,23),1,7) AS 월
	FROM 출퇴근기록 
	WHERE BizNum=@BizNum AND StaffID=@StaffID 
	ORDER BY substring(convert(varchar(10),날짜,23),1,7) DESC
Go
