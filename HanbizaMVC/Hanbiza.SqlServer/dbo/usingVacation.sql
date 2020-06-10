/*
SELECT [날짜] as u_ymd,[반차] as b_cnt 
FROM 휴가대장 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
ORDER BY [날짜] ASC
*/
CREATE PROCEDURE [dbo].[usingVacation]
	@BizNum varchar(10),
	@StaffID int
AS
	SELECT 날짜, 반차, convert(int, ROW_NUMBER() OVER(ORDER BY 날짜)) as 번호
	FROM 휴가대장
	WHERE BizNum = @BizNum AND StaffID = @StaffID
	ORDER BY 날짜

