/*
SELECT Dname,BizNum, CompanyName, joinday, StaffName,StaffID,loginID,State 
FROM Login_infor 
WHERE (loginID = '" & loginID & "') AND PwdCompare('" & LCase(PassW) & "', passw) = 1
*/

CREATE PROCEDURE dbo.login_Process
	@loginID  VARCHAR(50),
	@passW VARCHAR(50)
AS
	SELECT Dname, BizNum, StaffID, StaffName, CONVERT(char(23), GETDATE(), 23) as LoginDate, loginID
	FROM Login_infor
	WHERE (loginID = @loginID) AND PWDCOMPARE(LOWER(@passW), PassW) = 1
Go