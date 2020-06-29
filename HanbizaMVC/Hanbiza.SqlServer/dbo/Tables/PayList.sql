CREATE TABLE [dbo].[PayList] (
    [SEQID]   INT           IDENTITY(1,1) NOT NULL,
    [Dname]   VARCHAR (50)  NOT NULL,
    [BizNum]  CHAR (10)     NOT NULL,
    [StaffID] INT           NOT NULL,
    [YYYYMM]  CHAR (6)      NOT NULL,
    [Gubun]   CHAR (1)      NULL,
    [SList]   NVARCHAR (50) NULL,
    [SValue]  FLOAT (53)    NULL,
    [Ncount]  INT           NULL,
    [등록인]     VARCHAR (50)  NULL,
    [Regdate] DATETIME      NULL,
    [Fsort]   INT           NULL
);

