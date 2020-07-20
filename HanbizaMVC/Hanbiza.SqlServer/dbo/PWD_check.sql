CREATE PROCEDURE [dbo].[PWD_check]
	@StaffID  VARCHAR(50),
	@BizNum CHAR(10),
	@checkPWD VARCHAR(50)
AS
	SELECT *
	FROM Login_infor
	WHERE (StaffID = @StaffID) AND PWDCOMPARE(LOWER(@checkPWD), PassW) = 1

