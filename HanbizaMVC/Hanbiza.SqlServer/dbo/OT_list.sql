/*
SELECT * 
FROM AddTime_List 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
	AND substring(convert(varchar(10),[날짜],23),1,7)='"&s_ymd&"' 
ORDER BY ENal DESC

SELECT Gubun, CONVERT(CHAR(23), SNal, 121) as SNal, CONVERT(CHAR(23), ENal, 121) as ENal,Reason 
FROM AddTime_List 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
	AND DateAdd(mm,-6,GETDATE()) <= ENal 
ORDER BY ENal DESC

*/

CREATE PROCEDURE [dbo].[OT_list]
	@BizNum varchar(10),
	@StaffID int
AS
	SELECT Regdate, Reason, SUBSTRING(CONVERT(CHAR(23), SNal, 121),12,5) as SNals, SUBSTRING(CONVERT(CHAR(23), ENal, 121),12,5) as ENals
	FROM AddTime_List
	WHERE StaffID = @StaffID AND BizNum = @BizNum
		AND DATEADD(mm, -6, GETDATE()) <= ENal
	ORDER BY ENal DESC
