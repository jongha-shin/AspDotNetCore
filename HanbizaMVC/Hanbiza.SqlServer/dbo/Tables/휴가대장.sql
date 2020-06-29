CREATE TABLE [dbo].[휴가대장] (
    [SEQID]   INT          IDENTITY(1,1) NOT NULL,
    [Dname]   VARCHAR (50) NOT NULL,
    [BizNum]  CHAR (10)    NOT NULL,
    [StaffID] INT          NOT NULL,
    [날짜]      DATE         NOT NULL,
    [반차]      FLOAT (53)   NULL,
    [등록인]     VARCHAR (50) NULL,
    [Regdate] DATETIME     NULL
);

