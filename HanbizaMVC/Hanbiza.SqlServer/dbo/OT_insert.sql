CREATE PROCEDURE [dbo].[OT_insert]
	@BizNum varchar(10),
	@StaffID int
AS
	INSERT INTO AddTime_List(DName, BizNum, StaffID, STaffName, Gubun, SNal, ENal, Reason, HbzMsend, HbzYYMMDD, Regdate)
	VALUES('')