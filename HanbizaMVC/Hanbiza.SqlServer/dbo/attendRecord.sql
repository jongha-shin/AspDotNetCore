/*
SELECT [날짜] as ymd,[총근로] as t_wday,substring([출근],12,5) as s_time,substring([퇴근],12,5) as e_time 
FROM 출퇴근기록 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
	AND substring(convert(varchar(10),[날짜],23),1,7)='"&s_ymd&"' 
ORDER BY [날짜] ASC
*/

CREATE PROCEDURE [dbo].[attendRecord]
	@BizNum varchar(10),
	@StaffID int,
	@lastMonth varchar(10)
AS
	SELECT [날짜], [총근로], substring([출근],12,5) as 출근, substring([퇴근],12,5) as 퇴근, 
		case when (DATEPART(weekday, 날짜) = '1') THEN '(일)'
			when (DATEPART(weekday, 날짜) = '2') THEN '(월)'
			when (DATEPART(weekday, 날짜) = '3') THEN '(화)'
			when (DATEPART(weekday, 날짜) = '4') THEN '(수)'
			when (DATEPART(weekday, 날짜) = '5') THEN '(목)'
			when (DATEPART(weekday, 날짜) = '6') THEN '(금)'
			when (DATEPART(weekday, 날짜) = '7') THEN '(토)' END AS 요일
	FROM 출퇴근기록
	WHERE BizNum = @BizNum AND StaffID = @StaffID AND substring(convert(varchar(10),[날짜],23),1,7) = @lastMonth

