CREATE TABLE [dbo].[문서함] (
    [SeqID]     INT           NOT NULL,
    [Dname]     VARCHAR (50)  NOT NULL,
    [BizNum]    CHAR (10)     NOT NULL,
    [StaffID]   INT           NOT NULL,
    [Gubun]     VARCHAR (50)  NULL,
    [FilePath]  VARCHAR (100) NULL,
    [FileName]  VARCHAR (100) NULL,
    [FileBLOB]  IMAGE         NULL,
    [등록인]       VARCHAR (50)  NULL,
    [Regdate]   DATETIME      NULL,
    [Signature] CHAR (1)      NULL,
    [SignDown]  CHAR (1)      NULL,
    [Edit_yn]   CHAR (1)      NULL,
    [Ftype]     VARCHAR (20)  NULL,
    [FSize]     INT           NULL
);

