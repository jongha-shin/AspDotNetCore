CREATE PROCEDURE [dbo].[attend_MonthlyRecord]
	@BizNum varchar(10),
	@StaffID int,
	@lastMonth varchar(10)
AS
	SELECT 기준일, 근무일, 결근일, 휴무일, 주휴일, 유급휴가_휴일, 유급휴일, 무급휴가_휴일, 유급주휴일 FROM 출퇴근기록_집계표
	WHERE StaffID = @StaffID AND BizNum = @BizNum AND SUBSTRING(CONVERT(char(10),시작일),1,7) = @lastMonth

