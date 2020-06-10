/*
SELECT DISTINCT YYYYMM, convert(varchar(1),Ncount),YYYYMM AS s_yymm, Ncount 
FROM PayList 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
ORDER BY  YYYYMM DESC,convert(varchar(1),Ncount) DESC
*/
CREATE PROCEDURE [dbo].[payment_lastMonth]
	@BizNum varchar(10),
	@StaffID int
AS
	SELECT DISTINCT YYYYMM, Ncount 
	FROM PayList
	WHERE BizNum=@BizNum AND StaffID=@StaffID 
	ORDER BY YYYYMM DESC, Ncount DESC

