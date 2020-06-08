CREATE TABLE [dbo].[AddTime_List] (
    [SEQID]     INT            NOT NULL,
    [DName]     VARCHAR (50)   NOT NULL,
    [BizNum]    CHAR (10)      NOT NULL,
    [StaffID]   INT            NOT NULL,
    [STaffName] VARCHAR (50)   NULL,
    [Gubun]     CHAR (1)       NOT NULL,
    [SNal]      DATETIME       NOT NULL,
    [ENal]      DATETIME       NOT NULL,
    [Reason]    NVARCHAR (100) NULL,
    [HbzMsend]  CHAR (1)       NULL,
    [HbzYYMMDD] DATETIME       NULL,
    [Regdate]   DATETIME       NULL
);

