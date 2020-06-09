/*
SELECT sum([총근로]) as t_sum,sum([소정근로]) as s_sum,sum([근태조정]) as g_sum,sum([초과근로]) as c_sum,sum([주휴]) as w_sum,sum([연장근로]) as e_sum,sum([야간근로]) as n_sum,sum([휴일근로]) as h_sum,sum([휴일연장]) as he_sum,sum([휴일연장]) as hn_sum 
FROM 출퇴근기록 
WHERE BizNum='"&Request.Cookies("HanbizaCookie")("BizNum")&"' 
	AND StaffID="&Request.Cookies("HanbizaCookie")("StaffID")&" 
	AND substring(convert(varchar(10),날짜,23),1,7)='"&s_ymd&"'
*/


CREATE PROCEDURE [dbo].[totalAttendence]
	@BizNum varchar(10),
	@StaffID int,
	@lastMonth varchar(10)
AS
	SELECT sum([총근로]) as 총근로, sum([소정근로]) as 소정근로, sum([근태조정]) as 근태조정, sum([초과근로]) as 초과근로, sum([연장근로]) as 연장근로, sum([야간근로]) as 야간근로, sum([휴일근로]) as 휴일근로, sum([휴일연장]) as 휴일연장, sum([휴일야간]) as 휴일야간
	FROM 출퇴근기록
	WHERE BizNum = @BizNum AND StaffID = @StaffID AND substring(convert(varchar(10),날짜,23),1,7) = @lastMonth


