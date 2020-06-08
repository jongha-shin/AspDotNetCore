CREATE TABLE [dbo].[Login_infor] (
    [SEQID]       INT             NOT NULL,
    [Dname]       VARCHAR (50)    NOT NULL,
    [BizNum]      CHAR (10)       NOT NULL,
    [CompanyName] NVARCHAR (50)   NULL,
    [StaffName]   NVARCHAR (50)   NOT NULL,
    [StaffID]     INT             NOT NULL,
    [loginID]     VARCHAR (50)    NOT NULL,
    [PassW]       VARBINARY (128) NULL,
    [JoinDay]     DATETIME        NULL,
    [State]       CHAR (1)        NULL,
    [Regdate]     DATETIME        NULL,
    [Bigo]        NVARCHAR (100)  NULL,
    [BBuseo]      NVARCHAR (50)   NULL,
    [MBuseo]      NVARCHAR (50)   NULL,
    [Buseo]       NVARCHAR (50)   NULL
);

