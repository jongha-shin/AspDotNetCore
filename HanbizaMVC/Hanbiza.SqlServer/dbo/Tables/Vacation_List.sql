CREATE TABLE [dbo].[Vacation_List] (
    [SEQID]      INT            NOT NULL,
    [Dname]      VARCHAR (50)   NOT NULL,
    [BizNum]     CHAR (10)      NOT NULL,
    [StaffID]    INT            NOT NULL,
    [Vicname]    NVARCHAR (100) NOT NULL,
    [UseTime]    FLOAT (53)     NULL,
    [SNal]       DATE           NOT NULL,
    [Enal]       DATE           NOT NULL,
    [ProCDeep]   INT            NULL,
    [AllProCess] CHAR (1)       NULL,
    [VicReaSon]  NVARCHAR (100) NULL,
    [HbzMSend]   CHAR (1)       NULL,
    [HbzMYYMMDD] DATETIME       NULL,
    [Regdate]    DATETIME       NULL
);

