CREATE PROCEDURE [dbo].[notice_getList]
	@LoginID varchar(50)
AS
	SELECT 제목, 내용, 등록인, Regdate FROM 공지사항
	WHERE LoginID = CONVERT(int, @LoginID)
