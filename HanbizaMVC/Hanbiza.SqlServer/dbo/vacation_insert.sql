CREATE PROCEDURE [dbo].[vacation_insert]
	@Dname varchar(50),
	@BizNum char(10),
	@StaffId int,
	@Vicname nvarchar(100),
	@UseTime float,
	@Snal date,
	@Enal date,
	@ProCDeep int,
	@VicReaSon nvarchar(100)
	 
AS
	INSERT INTO Vacation_List(Dname, BizNum, StaffID, Vicname, UseTime, SNal, Enal, 
								ProCDeep, AllProCess, VicReaSon, HbzMSend, HbzMYYMMDD, Regdate )
	VALUES(@Dname, @BizNum, @StaffId, @Vicname, @UseTime, @Snal, @Enal, 
			@ProCDeep, 'N', @VicReaSon, '', null, GETDATE())
