

CREATE PROCEDURE [dbo].[login_userDetail]
	@StaffID int
AS
	SELECT Dname, BizNum, StaffID, StaffName, CONVERT(char(23), GETDATE(), 23) as LoginDate
	FROM Login_infor
	WHERE StaffID = @StaffID

