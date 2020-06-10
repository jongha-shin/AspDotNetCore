/*
SELECT [입사일] as ip_day,[연차발생일] as yc_day,[근속년수] as ay_cnt,[발생연차] as ah_cnt,[잔여일수] as re_cnt,[연차구분] as yc_gb,[Regdate] as up_day 
FROM 연차대장
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&"
*/
CREATE PROCEDURE [dbo].[countVacation]
	@BizNum varchar(10),
	@StaffID int
AS
	SELECT 입사일, 연차발생일, 근속년수, 발생연차, 잔여일수, 연차구분, Regdate
	FROM 연차대장
	WHERE BizNum = @BizNum AND StaffID = @StaffID

