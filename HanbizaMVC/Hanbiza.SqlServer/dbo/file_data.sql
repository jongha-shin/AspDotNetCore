CREATE PROCEDURE [dbo].[file_data]
	@SeqID int
	
AS
	SELECT FileBLOB, FileName
	FROM 문서함
	WHERE SeqID = @SeqID

