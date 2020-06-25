CREATE PROCEDURE [dbo].[vacation_getApprover]
	@BizNum varchar(10),
	@StaffID int,
	@SearchKey varchar(20),
	@SearchWord varchar(20),
	@Step_num varchar(1),
	@StaffList varchar(500)
AS
DECLARE @T TABLE(StaffID INT)

WHILE CHARINDEX(',', @StaffList)<>0
BEGIN 
	INSERT INTO @T(StaffID) VALUES( SUBSTRING(@StaffList, 1, CHARINDEX(',', @StaffList)-1) )
	SET @StaffList = SUBSTRING( @StaffList, CHARINDEX(',', @StaffList)+1, LEN(@StaffList) )
	IF CHARINDEX(',', @StaffList) = 0
	BEGIN
		INSERT INTO @T(StaffID) VALUES( SUBSTRING(@StaffList, 1, LEN(@StaffList)) )
	END
END


IF(@SearchKey = '부서')
	BEGIN
		SELECT Buseo, StaffName, StaffID
		FROM Login_infor
		WHERE BizNum = @BizNum AND Buseo LIKE  '%' + @SearchWord + '%'
			AND StaffID <> @StaffID AND StaffID <> @StaffList AND StaffID NOT IN( SELECT StaffID FROM @T )
	END
ELSE
	BEGIN
		SELECT Buseo, StaffName, StaffID
		FROM Login_infor
		WHERE BizNum = @BizNum AND StaffName = @SearchWord
			AND StaffID <> @StaffID AND StaffID NOT IN( SELECT StaffID FROM @T )
	END

