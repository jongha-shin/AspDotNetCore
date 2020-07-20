CREATE PROCEDURE [dbo].[PWD_change]
	@StaffID  VARCHAR(50),
	@BizNum CHAR(10),
	@changePWD VARCHAR(50)
AS
	UPDATE Login_infor
	SET PassW = PWDENCRYPT(@changePWD)
	WHERE StaffID = @StaffID AND BizNum = @BizNum