/*
SELECT Dname,BizNum, CompanyName, joinday, StaffName,StaffID,loginID,State 
FROM Login_infor 
WHERE (loginID = '" & loginID & "') AND PwdCompare('" & LCase(PassW) & "', passw) = 1
*/

CREATE PROCEDURE dbo.loginProcess
	@loginID  VARCHAR(50),
	@passW VARCHAR(50)
AS
	SELECT Dname, BizNum, CompanyName, JoinDay, StaffName, StaffID, loginID, State 
	FROM Login_infor
	WHERE (loginID = @loginID) AND PWDCOMPARE(LOWER(@passW), PassW) = 1
Go