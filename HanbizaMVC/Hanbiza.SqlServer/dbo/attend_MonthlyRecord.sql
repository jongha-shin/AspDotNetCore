CREATE PROCEDURE [dbo].[attend_MonthlyRecord]
	@BizNum varchar(10),
	@StaffID int,
	@lastMonth varchar(10)
AS
	SELECT * FROM 출퇴근기록_집계표
	WHERE StaffID = @StaffID AND BizNum = @BizNum AND SUBSTRING(CONVERT(char(10),시작일),1,7) = @lastMonth

