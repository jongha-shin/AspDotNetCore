CREATE PROCEDURE [dbo].[vacation_getApprover]
	@BizNum varchar(10),
	@SearchKey varchar(20),
	@SearchWord varchar(20)
AS
IF(@SearchKey = '부서')
	BEGIN
		SELECT Buseo, StaffName, StaffID
		FROM Login_infor
		WHERE BizNum = @BizNum AND Buseo LIKE  '%' + @SearchWord + '%'
	END
ELSE
	BEGIN
		SELECT Buseo, StaffName, StaffID
		FROM Login_infor
		WHERE BizNum = @BizNum AND StaffName = @SearchWord
	END

