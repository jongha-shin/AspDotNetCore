CREATE PROCEDURE [dbo].[payment_getPayment]
	@BizNum varchar(10),
	@StaffID int,
	@Yyyymm varchar(10),
	@Ncount varchar(1)
AS
	SELECT SList, SValue, Gubun, Fsort
	FROM PayList
	WHERE BizNum = @BizNum AND StaffID = @StaffID AND YYYYMM = @Yyyymm AND Ncount =@Ncount
	ORDER BY SEQID
RETURN 0
