/*

If SEQID="" Then '저장
	strSql = "INSERT INTO 문서함 VALUES ( " 
	strSql = strSql &"'" & Request.Cookies("HanbizaCookie")("Dname") &"'," 
	strSql = strSql &"'" & Request.Cookies("HanbizaCookie")("BizNum") &"'," 
	strSql = strSql & Request.Cookies("HanbizaCookie")("StaffID") &"," 
	strSql = strSql &"'서명'," 
	strSql = strSql &"'" & ImageDir &"'," 
	strSql = strSql &"'" & ImageFileName &"'," 
	strSql = strSql &"'data:image/png;base64," & base64String &"'," 
	strSql = strSql &"'" & Request.Cookies("HanbizaCookie")("StaffName") &"'," 
	strSql = strSql &"getdate()," 
	strSql = strSql &"'Y'," 
	strSql = strSql &"'N'," 
	strSql = strSql &"'Y'," 
	strSql = strSql &"''," 
	strSql = strSql &"0)" 
Else '수정
	strSql = "UPDATE 문서함 SET "
	strSql = strSql &"Dname='" & Request.Cookies("HanbizaCookie")("Dname") &"',"
	strSql = strSql &"등록인='" & Request.Cookies("HanbizaCookie")("StaffName") &"',"
	strSql = strSql &"FilePath='" & ImageDir &"',"
	strSql = strSql &"FileName='" & ImageFileName &"',"
	strSql = strSql &"FileBlob='data:image/png;base64," & base64String &"'," 
	strSql = strSql &"Edit_yn='N'"
	strSql = strSql &"WHERE SEQID=" &SEQID
End If

*/


CREATE PROCEDURE [dbo].[file_SaveSignature]
	@SEQID int,
	@BizNum varchar(10),
	@StaffID int,
	@StaffName varchar(50),
	@Dname varchar(50),
	@ImageDir varchar(100),
	@ImageFileName varchar(100), 
	@Base64String varchar(max)
AS
	
	IF(@SEQID = 0)
	BEGIN
		INSERT INTO 문서함(Dname, BizNum, StaffID, Gubun, FilePath, FileName, FileBLOB, 등록인, Regdate, Signature, SignDown, Edit_yn, Ftype, FSize)
		VALUES(
			@Dname,
			@BizNum,
			@StaffID,
			'서명',
			@ImageDir,
			@ImageFileName,
			'data:image/png;base64,'+ @Base64String,
			@StaffName,
			GETDATE(),
			'Y', 'N', 'Y', '', 0
		)
	END
ELSE
	BEGIN
		UPDATE 문서함 
		SET Dname = @Dname, 
			등록인 = @StaffName, 
			FilePath = @ImageDir, 
			FileName = @ImageFileName, 
			FileBLOB = 'data:image/png;base64'+@Base64String, 
			Edit_yn = 'N'
		WHERE SeqID = @SEQID
	END