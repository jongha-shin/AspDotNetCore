CREATE TABLE [dbo].[공지사항] (
    [SEQID]   INT            NOT NULL,
    [Dname]   VARCHAR (50)   NULL,
    [BizNum]  CHAR (10)      NULL,
    [제목]      NVARCHAR (50)  NULL,
    [내용]      NVARCHAR (100) NULL,
    [등록인]     VARCHAR (50)   NULL,
    [VacID]   INT            NULL,
    [Regdate] DATETIME       NULL,
    [LoginID] INT            NULL
);

