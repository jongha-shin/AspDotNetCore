CREATE TABLE [dbo].[Vacation_Approve] (
    [SEQID]        INT            IDENTITY(1,1) NOT NULL,
    [Dname]        VARCHAR (50)   NOT NULL,
    [BizNum]       CHAR (10)      NOT NULL,
    [VacID]        INT            NOT NULL,
    [approveID]    INT            NOT NULL,
    [approveName]  VARCHAR (50)   NULL,
    [DeepNum]      INT            NOT NULL,
    [processPoint] CHAR (1)       NULL,
    [RereaSon]     NVARCHAR (100) NULL,
    [AResult]      CHAR (1)       NULL,
    [Regdate]      DATETIME       NULL
);

